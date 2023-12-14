using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using PublicColor;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using Upbit.Event;
using Upbit.Functions;
using Upbit.Functions.Interface;
using Upbit.UpbitFunctions;
using static Upbit.Structs;

namespace Upbit.ViewModels
{
    public class BidAskListViewModel : BindableBase,IWebSocketTicker, IColors
    {

        PublicFunctions upbit = new PublicFunctions();
        #region Models
        /// <summary>
        /// 매수 호가 목록
        /// </summary>
        private ObservableCollection<Structs.BidAskUnit> bidunits;
        public ObservableCollection<Structs.BidAskUnit> BidUnits
        {
            get { return bidunits; }
            set { SetProperty(ref bidunits, value); }
        }
        

        /// <summary>
        /// 매도 호가창
        /// </summary>
        private ObservableCollection<Structs.BidAskUnit> askunits;
        public ObservableCollection<Structs.BidAskUnit> AskUnits
        {
            get { return askunits; }
            set { SetProperty(ref askunits, value); }
        }


        /// <summary>
        /// 현재 코인명
        /// </summary>
        private string coinname;
        public string CoinName
        {
            get { return coinname; }
            set { SetProperty(ref coinname, value); }
        }

        /// <summary>
        /// 등락율
        /// </summary>
        private string rate;
        public string Rate
        {
            get { return rate; }
            set { SetProperty(ref rate, value); }
        }


        private double acwidth;
        public double AcWidth
        {
            get { return acwidth; }
            set { SetProperty(ref acwidth, value); }
        }

        private bool BidAskType = true;
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
            MyColors.ColorAsk = PublicColor.Colors.Colorinstance.ColorAsk;
            MyColors.ColorBidBack = PublicColor.Colors.Colorinstance.ColorBidBack;
            MyColors.ColorAskBack = PublicColor.Colors.Colorinstance.ColorAskBack;
        }

        private PublicColor.Colors mycolors;
        public PublicColor.Colors MyColors
        {
            get
            {
                if (mycolors is null) mycolors = new PublicColor.Colors();
                return mycolors;
            }
            set { SetProperty(ref mycolors, value); }
        }
        #endregion Colors

        #region PrismEvent
        private IEventAggregator _ea;


        #endregion PrismEvent


        #region Event

        /// <summary>
        /// 선택된 금액으로 매수 매도가 변경
        /// </summary>
        private DelegateCommand<object> listviewdoubleclick;
        public DelegateCommand<object> CommandListViewDoubleClick =>
            listviewdoubleclick ?? (listviewdoubleclick = new DelegateCommand<object>(ExecuteCommandListViewDoubleClick));

        void ExecuteCommandListViewDoubleClick(object obj)
        {
            if (obj is null)
                return;

            Structs.BidAskUnit unit = (BidAskUnit)obj;

            Structs.MessageCoinPirce mp = new MessageCoinPirce();
            mp.Check = unit.Check;
            mp.Price = Convert.ToDouble(unit.unitp);
             
            _ea.GetEvent<MessageCoinPriceEvent>().Publish(mp);
        }

        #endregion

        #region InterfaceFunctions
        public void TradeEvent(Trade tr)
        {
            throw new NotImplementedException();
        }

        public void OrderEvent(Orderbook ob)
        {
            if (CoinName == ob.cd)
            {
                SetOrderBook(ob);
            }
        }

        public void TickerEvent(Ticker tick)
        {
            try
            {
                if (CoinName == tick.cd)
                {
                    string Change = tick.c;
                    string str = "";

                    if (Change == "RISE")
                        str = "+";
                    else if (Change == "FALL")
                        str = "-";

                    Rate = $"{str}{Math.Round(tick.cr * 100, 2)}%";
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine("BidAskTickerEvent");
            }
            
        }

        #endregion InterfaceFunctions

        #region Functions

        /// <summary>
        /// 호가창 셋팅
        /// </summary>
        /// <param name="ob"></param>
        private void SetOrderBook(Structs.Orderbook ob)
        {
            
            try
            {
                
                ObservableCollection<BidAskUnit> listask = new ObservableCollection<BidAskUnit>();
                ObservableCollection<BidAskUnit> listbid = new ObservableCollection<BidAskUnit>();

                string len = ob.obu[0].ap.ToString().Split(".")[0];
                string unitp = len.Length > 1 ? (len.Length > 2 ? "F0" : "F4") : "F8";
                len = ob.obu[0]._as.ToString().Split(".")[0];
                string units = len.Length > 1 ? (len.Length > 2 ? "F0" : "F4") : "F8";
                foreach (var item in ob.obu)
                {

                    Structs.BidAskUnit ask = new Structs.BidAskUnit();
                    Structs.BidAskUnit bid = new Structs.BidAskUnit();
                    ask.unitp = item.ap.ToString(unitp);
                    ask.units = item._as.ToString(units);

                    bid.unitp = item.bp.ToString(unitp);
                    bid.units = item.bs.ToString(units);
                    listask.Add(ask);
                    listbid.Add(bid);

                }
                BidUnits = new ObservableCollection<Structs.BidAskUnit>();
                AskUnits = new ObservableCollection<Structs.BidAskUnit>();
                foreach (var item in listask.Reverse())
                {
                    AskUnits.Add(item);
                }

                foreach (var item in listbid)
                {
                    BidUnits.Add(item);
                }

            }
            catch(Exception e)
            {
                Debug.WriteLine("SetOrderBook");
            }
            
        }

        private void GetCoinName(Structs.MarketCodes coinname)
        {
            try
            {
                CoinName = coinname.Market;
                Rate = coinname.Rate.ToString();
                SetOrderBook(upbit.OrderBook(CoinName));
                Structs.MessageCoinPirce mcp = new Structs.MessageCoinPirce();
                if (AskUnits.Count > 0 && BidUnits.Count > 0)
                {
                    if (BidAskType == true)
                    {
                        //매수 타입

                        mcp.Price = Convert.ToDouble(AskUnits[AskUnits.Count - 1].unitp);
                    }
                    else
                    {
                        //매도타입
                        mcp.Price = Convert.ToDouble(BidUnits[0].unitp);
                    }

                    mcp.Check = BidAskType;
                    _ea.GetEvent<MessageCoinPriceEvent>().Publish(mcp);
                }
            }
            catch(Exception ex)
            {
                Debug.WriteLine($"BidAsk CoinName : {ex.Message}");
            }
            
            
            

        }
        private void GetCoinName(Structs.Accounts coinname)
        {
            try
            {
                CoinName = coinname.Market;
                Rate = coinname.Rate.ToString();
                SetOrderBook(upbit.OrderBook(CoinName));
                Structs.MessageCoinPirce mcp = new Structs.MessageCoinPirce();
                if (AskUnits.Count > 0 && BidUnits.Count > 0)
                {
                    if (BidAskType == true)
                    {
                        //매수 타입

                        mcp.Price = Convert.ToDouble(AskUnits[AskUnits.Count - 1].unitp);
                    }
                    else
                    {
                        //매도타입
                        mcp.Price = Convert.ToDouble(BidUnits[0].unitp);
                    }

                    mcp.Check = BidAskType;
                    _ea.GetEvent<MessageCoinPriceEvent>().Publish(mcp);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"BidAsk CoinName : {ex.Message}");
            }

        }

        private void GetBidAskType(bool b)
        {
            BidAskType = b;
        }
        #endregion Functions

        public BidAskListViewModel(IEventAggregator ea)
        {
            _ea = ea;

            _ea.GetEvent<MessageCoinNameEvent>().Subscribe(GetCoinName);
            _ea.GetEvent<MessageAccountEvent>().Subscribe(GetCoinName);
            _ea.GetEvent<MessageBidAskCheckEvent>().Subscribe(GetBidAskType);

            

            BidUnits = new ObservableCollection<Structs.BidAskUnit>();
            AskUnits = new ObservableCollection<Structs.BidAskUnit>();

            //웹소켓에서 받아온 OrderBook이벤트 발생
            WebSocketTicker.OrderEvent += new WebSocketTicker.OrderEventHandler(OrderEvent);
            WebSocketTicker.TickerEvent += new WebSocketTicker.TickEventHandler(TickerEvent);

            //색변경 이벤트 등록
            PublicColor.Colors.ColorUpdate += (EventUpdateColor);
        }


        
    }
}
