using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Threading;
using Upbit.UpbitFunctions;
using System.Windows;
using Upbit.UpbitFunctions.Interface;
using Language;
using System.Windows.Media;
using PublicColor;
using Prism.Events;
using Upbit.Event;
using Upbit.Functions.Interface;
using Upbit.Functions;
using System.Windows.Controls.Primitives;
using System.Linq;
using Language.Lang;
using static Upbit.Structs;
using System.Diagnostics;
using System.Timers;

namespace Upbit.ViewModels
{
    public class BalanceViewModel : BindableBase,IAccess, ILanguage, IColors, IWebSocketTicker, IMyTrade
    {
        #region Lang
        private Language.Lang.Balance lang;
        public Language.Lang.Balance Lang
        {
            get { if (lang is null) lang = new Language.Lang.Balance(); return lang; }
            set { SetProperty(ref lang, value); }
        }
        public virtual void SetLanguage()
        {
            Lang.UpdateLang();
            SetCoinName();
        }
        
        #endregion

        #region Colors
        /// <summary>
        /// 색상 업데이트 이벤트
        /// </summary>
        public void EventUpdateColor()
        {
            MyColors.ColorText = PublicColor.Colors.Colorinstance.ColorText;
            MyColors.ColorBack = PublicColor.Colors.Colorinstance.ColorBack;
            MyColors.ColorBid = PublicColor.Colors.Colorinstance.ColorBid;
            mycolors.ColorAsk = PublicColor.Colors.Colorinstance.ColorAsk;
        }

        private PublicColor.Colors mycolors;
        public PublicColor.Colors MyColors
        {
            get {  if (mycolors is null) mycolors = new PublicColor.Colors();
                return mycolors; }
            set { SetProperty(ref mycolors, value); }
        }
        #endregion Colors

        #region Prism
        private IEventAggregator _ea;
        #endregion Prism

        #region Models

        private Timer OrderCheckTimer;

        private Structs.Accounts myaccount;
        public Structs.Accounts MyAccount
        {
            get { return myaccount; }
            set { SetProperty(ref myaccount, value); }
        }

        private Visibility noneaccessview;
        public Visibility NoneAccessView
        {
            get { return noneaccessview; }
            set { SetProperty(ref noneaccessview, value); }
        }

        public bool IsBalance { get; private set; } 

        private UpbitFunctions.Accounts FAccount = new UpbitFunctions.Accounts();

        private List<Structs.Accounts> accounts;
        public List<Structs.Accounts> Accounts
        {
            get { return accounts; }
            set { 
                SetProperty(ref accounts, value);
            }
        }



        private List<Structs.MarketCodes> Markets;
        private PublicFunctions pf = new PublicFunctions();
        #endregion Models

        /// <summary>
        /// 코인 선택 이벤트
        /// </summary>
        private DelegateCommand<object> _sendcoin;
        public DelegateCommand<object> CommandSendCoin =>
            _sendcoin ?? (_sendcoin = new DelegateCommand<object>(ExecuteCommandSendCoin));

        void ExecuteCommandSendCoin(object obj)
        {
            if (obj is null) return;
            Structs.Accounts marketCodes = (Structs.Accounts)obj;

            _ea.GetEvent<MessageAccountEvent>().Publish(marketCodes);

        }

        #region Functions
        public void TradeEvent(Structs.Trade tr)
        {
            throw new NotImplementedException();
        }

        public void OrderEvent(Structs.Orderbook ob)
        {
            throw new NotImplementedException();
        }

        public void TickerEvent(Structs.Ticker tick)
        {
            if(Accounts is not null)
            {
                try
                {
                    if (Accounts.Count > 0)
                    {
                        for (int i = 0; i < Accounts.Count; i++)
                        {
                            if (tick.cd == Accounts[i].Market)
                            {
                                Accounts[i].NowPrice = tick.tp;
                                Accounts[i].TotalPrice = tick.tp;
                                Accounts[i].Rate = tick.tp;
                                Accounts[i].Color = SetTextColor(Accounts[i].NowPrice, Convert.ToDouble(Accounts[i].AvgBuyPrice));

                                if (MyAccount is not null)
                                {
                                    //내 지갑
                                    SetMyAccount();
                                }

                                break;
                            }
                        }
                    }

                }
                catch(Exception ex) 
                {
                    Debug.WriteLine($"BalanceView TickerEvent : {ex.ToString()}");
                }
            }
        }

        private Brush SetTextColor(double now, double avg)
        {
            Brush val = Brushes.Transparent;

            if (now >= avg)
                val = MyColors.ColorBid;
            else
                val = MyColors.ColorAsk;


            return val;
        }

        /// <summary>
        /// key값을 전달받았을때 이벤트 
        /// </summary>
        /// <param name="b"></param>
        public void D_AccessEvent(bool b)
        {
            try
            {
                IsBalance = Access.UpbitInstance.GetAccess();
                if (IsBalance)
                {
                    NoneAccessView = Visibility.Collapsed;
                    Accounts = Task.Run(() => FAccount.AccountsAsync()).Result;
                    PullOutMyAccount(ref accounts);
                    SetCoinName();
                    OrderCheckTimer?.Start();
                }
                else
                {
                    Accounts = new List<Structs.Accounts>();
                    NoneAccessView = Visibility.Visible;
                    OrderCheckTimer?.Stop();
                }
            }
            catch(Exception ex)
            {
                Debug.WriteLine($"Balance D_AccessEvent {ex.Message}");
            }
        }

        private void SetCoinName()
        {
            if (Accounts is null)
                return;
            for(int i = 0; i < Accounts.Count; i++) 
            {
                foreach(var v in Markets)
                {
                    if(v.Market == Accounts[i].Market)
                    {
                        string str = "";
                        if (Language.Language.Lang.LangType == "kr")
                            str = v.Korean_name;
                        else
                            str = v.English_name;
                        Accounts[i].CoinName = str;
                        break;  
                    }
                }
            }
        }
        private double GetMyTotalPrice()
        {
            double value = 0;
            foreach(var v in Accounts)
            {
                value += v.TotalPrice;
            }

            return value;
        }
        private double GetMyTotalCost()
        {
            double value = 0;
            foreach (var v in Accounts)
            {
                value += v.Cost;
            }

            return value;
        }

        /// <summary>
        /// 내 잔고를 뺌과 동시에 내 지갑에있는 코인 현재가가없을경우 현재가를 출력해줌
        /// 23.12.04 -완
        /// </summary>
        /// <param name="list"></param>
        private void PullOutMyAccount(ref List<Structs.Accounts> list)
        {
            if (Accounts is null)
                return;
            try
            {
                int index = -1;
                for (int i = 0; i < list.Count; i++)
                {
                    if ("KRW" == list[i].Currency)
                    {
                        MyAccount = list[i];
                        index = i;
                    }
                    else
                    {
                        if (list[i].NowPrice == 0)
                        {
                            Structs.Orderbook now = pf.OrderBook(list[i].Market);

                            list[i].NowPrice = now.obu[0].bp;
                            list[i].TotalPrice = now.obu[0].bp;
                            list[i].Rate = now.obu[0].bp;
                            list[i].Color = SetTextColor(list[i].NowPrice, Convert.ToDouble(list[i].AvgBuyPrice));
                        }
                    }


                }
                if (index != -1)
                    list.RemoveAt(index);
            }
            catch(Exception e) 
            {
                Debug.WriteLine("PullOut");
            }


            SetMyAccount();

        }

        /// <summary>
        /// 내 KRW잔고 갱신
        /// </summary>
        private void SetMyAccount()
        {
            MyAccount.NowPrice = GetMyTotalPrice();
            MyAccount.AvgBuyPrice = GetMyTotalCost().ToString();
            MyAccount.Cost = GetMyTotalCost();
            MyAccount.Rate = MyAccount.NowPrice;
            MyAccount.Color = SetTextColor(MyAccount.NowPrice, Convert.ToDouble(MyAccount.AvgBuyPrice));
        }
        

        
        /// <summary>
        /// 잔고 갱신
        /// </summary>
        public void RefreshAccount()
        {
            try
            {
                List<Structs.Accounts> list = Task.Run(() => FAccount.AccountsAsync()).Result;
                PullOutMyAccount(ref list);

                for (int j = 0; j < list.Count; j++)
                {

                    for (int i = 0; i < Accounts.Count; i++)
                    {
                        if (Accounts[i].Market == list[j].Market)
                        {
                            list[j].CoinName = Accounts[i].CoinName;
                            list[j].NowPrice = Accounts[i].NowPrice;
                            list[j].TotalPrice = Accounts[i].NowPrice;
                            list[j].Color = SetTextColor(Accounts[i].NowPrice, Convert.ToDouble(Accounts[i].AvgBuyPrice));


                            break;
                        }
                    }
                }
                Accounts = list;
                SetCoinName();
            }catch(Exception ex)
            {
                Debug.WriteLine($"Balance RefreshAccount : {ex.ToString()}");
            }
        }

        /// <summary>
        /// 코인 주문 성공했을때 현재 잔고 갱긴을위해사용
        /// </summary>
        /// <param name="b"></param>
        public void RefreshAccount(bool b)
        {
            if (b)
            {
                RefreshAccount();
            }
        }



        public virtual void MyTradeEvent(Structs.MyTrade mytrade)
        {
            //원화는 그냥 불러와서 계산하는게 정확할거같아서 불러다씀
            RefreshAccount();
        }



        /// <summary>
        /// 오더가 들어왔는지 1초마다 체크해줌.
        /// 다른 기기에서 오더를했을때 들어오지 않는 문제해결을 위해.
        /// 23.12.08
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EventOrerCheck(object sender,ElapsedEventArgs e)
        {
            Dictionary<string,double> data = new Dictionary<string,double>();
            List<Structs.Accounts> list = Task.Run(() => FAccount.AccountsAsync()).Result;
            Structs.Accounts mykrw = new Structs.Accounts();
            bool _b = false;
            foreach(var item in list)
            {
                if(item.Currency == "KRW")
                {
                    mykrw = item;
                    list.Remove(item);
                    break;
                }
            }
            foreach (var item in Accounts)
            {
                data.Add(item.Market,Convert.ToDouble(item.Locked));
            }
            try
            {
                foreach (var item in list)
                {
                    double val = data[item.Market];
                    if (val != Convert.ToDouble(item.Locked))
                    {
                        _b = true;
                        break;
                    }
                }
                if(Convert.ToDouble(MyAccount.Locked) != Convert.ToDouble(mykrw.Locked))
                    _b = true;

                if (_b)
                {
                    //내 잔고 갱신 이거 처리안해주면 계속 이벤트보냄
                    DispatcherService.Invoke(() =>
                    {
                        _ea.GetEvent<MessageCoinOrderChangeEvent>().Publish();
                        RefreshAccount();
                    });
                    
                }
            }
            catch(Exception ex ) 
            {
                Debug.WriteLine($"BalaceView {ex.Message}");
            }
            
        }

        #endregion Functions


        public BalanceViewModel(IEventAggregator ea)
        {
            _ea = ea;

            EventUpdateColor();
            SetLanguage();

            Accounts = new List<Structs.Accounts>();

            //타이머 셋팅
            OrderCheckTimer = new Timer();
            OrderCheckTimer.Interval = 1000;
            OrderCheckTimer.Elapsed += EventOrerCheck;

            _ea.GetEvent<MessageCoinOrderEvent>().Subscribe(RefreshAccount);    //코인주문들어갔을때

            //Key값 이벤트 등록
            Access.CheckedAccess += (D_AccessEvent);

            PublicFunctions Upbitpf = new PublicFunctions();
            Markets = Task.Run(() => Upbitpf.MarketsAsync()).Result;

            //색변경 이벤트 등록
            PublicColor.Colors.ColorUpdate += (EventUpdateColor);

            //언어 변경시 이벤트 등록
            Language.Language.LangChangeEvent += (SetLanguage);

            //실시간 틱데이터를 받아오기위해 이벤트등록
            WebSocketTicker.TickerEvent += new WebSocketTicker.TickEventHandler(TickerEvent);

            MyTradeClientWebSocket.MyTradeEvent += new MyTradeClientWebSocket.MyTradeEventHandler(MyTradeEvent);
             
            IsBalance = Access.UpbitInstance.GetAccess();
            if (IsBalance)
            {
                NoneAccessView = Visibility.Collapsed;
                Accounts = Task.Run(() => FAccount.AccountsAsync()).Result;
                PullOutMyAccount(ref accounts);
                SetCoinName();
                OrderCheckTimer?.Start();

            }
            else
            {
                Accounts = new List<Structs.Accounts>();
                NoneAccessView = Visibility.Visible;
                OrderCheckTimer?.Stop();
            }
        }

    }
}
