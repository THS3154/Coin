using Language;
using Language.Lang;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using PublicColor;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using Upbit.Event;
using Upbit.UpbitFunctions;

namespace Upbit.ViewModels
{
    public class CoinOrderViewModel : BindableBase,ILanguage, IColors
    {
        #region Models
        private bool BBidAskType = true; //true => BId / False = Ask
        /// <summary>
        /// 매수 매도 확인을 위한 컬러 변경
        /// </summary>
        private Brush colorgrid;
        public Brush GridColor
        {
            get { return colorgrid; }
            set { SetProperty(ref colorgrid, value); }
        }


        //주문버튼 색상변경용
        private Brush btnborder;
        public Brush BtnBorder
        {
            get { return btnborder; }
            set { SetProperty(ref btnborder, value); }
        }

        private Brush btnback;
        public Brush BtnBack
        {
            get { return btnback; }
            set { SetProperty(ref btnback, value); }
        }

        //주문버튼 텍스트
        private string btntext;
        public string BtnText
        {
            get { return btntext; }
            set { SetProperty(ref btntext, value); }
        }

        //false = 지정가 / true = 시장가
        private bool ismarket;
        public bool IsMarket
        {
            get { return ismarket; }
            set { SetProperty(ref ismarket, value); }
        }

        private bool marketbuy;
        public bool MarketBuy
        {
            get { return marketbuy; }
            set { SetProperty(ref marketbuy, value);}
        }

        private bool marketsell;
        public bool MarketSell
        {
            get { return marketsell; }
            set {SetProperty(ref marketsell, value); 
            }
        }

        //구매 화폐
        private string currency;
        public string Currency
        {
            get { return currency; }
            set { SetProperty(ref currency, value); }
        }

        //구매 코인
        private string ordercoin;
        public string OrderCoin
        {
            get { return ordercoin; }
            set { SetProperty(ref ordercoin, value); }
        }

        //구매 수량
        private double quantity;
        public double Quantity
        {
            get { return GetDoubleValue(quantity); }
            set { SetProperty(ref quantity, value);}
        }

        //구매가격
        private double price;
        public double Price
        {
            get { return GetDoubleValue(price); }
            set { SetProperty(ref price, value);}
        }

        //최종 내가 내야할 금액
        private double totalprice;
        public double TotalPrice
        {
            get { return GetDoubleValue(totalprice); }
            set {
                SetProperty(ref totalprice, value);}
        }

        //텍스트박스 실수처리를 입력하기위해 스트링으로 받고 실수로 변환
        private string strprice;
        public string StrPrice
        {
            get { return strprice; }
            set {
                string str = GetNumberic(value);
                if (CheckedDot(str))
                {
                    SetProperty(ref strprice, str);
                    Price = str != "" ? double.Parse(str) : 0;
                    if (IsPrice)
                    {
                        IsPrice = false;
                    }
                    else
                    {
                        ChangePrice();
                    }
                }
            }
        }

        //텍스트박스 실수처리를 입력하기위해 스트링으로 받고 실수로 변환
        private string strquantity;
        public string StrQuantity
        {
            get { return strquantity; }
            set {
                string str = GetNumberic(value);
                if (CheckedDot(str))
                {
                    SetProperty(ref strquantity, str);
                    Quantity = str != "" ? double.Parse(str) : 0;
                    if (IsQuantity)
                    {
                        IsQuantity = false;
                    }
                    else
                    {
                        ChangeQuantity();
                    }
                }
            }
        }

        //텍스트박스 실수처리를 입력하기위해 스트링으로 받고 실수로 변환
        private string strtotalprice;
        public string StrTotalPrice
        {
            get { return strtotalprice; }
            set {
                string str = GetNumberic(value);
                if (CheckedDot(str))
                {
                    SetProperty(ref strtotalprice, str);
                    if (str == ".") str = "0";
                    TotalPrice = str != "" ? Convert.ToDouble(str) : 0;
                    if (IsTotalPrice)
                    {
                        IsTotalPrice = false;
                    }
                    else
                    {
                        ChangeTotalPrice();
                    }
                }
                
                 }
        }


        private bool orderview;
        public bool OrderView
        {
            get { return orderview; }
            set { SetProperty(ref orderview, value); }
        }
        private bool orderlistview;
        public bool OrderListView
        {
            get { return orderlistview; }
            set { SetProperty(ref orderlistview, value); }
        }

        //해당코인이 주문이 가능한 상태인지
        private bool activeorder;
        public bool ActiveOrder
        {
            get { return activeorder; }
            set { SetProperty(ref activeorder, value); }
        }

        private string activeordercurrency;
        public string AcitiveOrderCurrency
        {
            get { return activeordercurrency; }
            set { SetProperty(ref activeordercurrency, value); }
        }
        //주문가능금액
        private double activeorderprice;
        public double ActiveOrderPrice
        {
            get { return activeorderprice; }
            set { SetProperty(ref activeorderprice, value); }
        }

        private Structs.Chance chance;
        public Structs.Chance Chance
        {
            get { return chance; }
            set { SetProperty(ref chance, value); }
        }

        //주문리스트 저장
        private List<Structs.OrderList> orderlist;
        public List<Structs.OrderList> OrderList
        {
            get { return orderlist; }
            set { SetProperty(ref orderlist, value); }
        }

        //언어변경됐을때 주문리스트 갱신 기본값은 체결대기
        private string KeepOrderListType = "wait";

        private string BidAskType = "l";
        private bool IsPrice = false;
        private bool IsTotalPrice = false;
        private bool IsQuantity = false;
        private double OrderMin = 0;
        private double Fee = 0;
        private string SelectCoin = "";
        Order Orderfnc = new Order();

        #endregion Models

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

            SetGridColor();
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

        #region Lang

        private Language.Lang.PublicCoin lang;
        public Language.Lang.PublicCoin Lang
        {
            get { if (lang is null) lang = new Language.Lang.PublicCoin(); return lang; }
            set { SetProperty(ref lang, value); }
        }
        public virtual void SetLanguage()
        {
            string saveprice = OrderCoin;
            Lang.UpdateLang();
            if(KeepOrderListType != "" && Currency != "" && OrderCoin != "")
            {
                string SelectCoin = Currency + "-" + OrderCoin;
                OrderList = Task.Run(() => Orderfnc.OrderListAsync(SelectCoin, KeepOrderListType)).Result;
            }
            if (Lang.LPrice == saveprice)
            {
                OrderCoin = Lang.LPrice;
                Currency = Lang.LCurrency;
            }
        }

        #endregion Lang

        #region Prism
        private IEventAggregator _ea;
        #endregion Prism

        #region Event
        /// <summary>
        /// 매수 매도 버튼
        /// </summary>
        private DelegateCommand<string> _bidasktype;
        public DelegateCommand<string> CommandBidAskType =>
            _bidasktype ?? (_bidasktype = new DelegateCommand<string>(ExecuteBidAskType));

        void ExecuteBidAskType(string parameter)
        {
            if(parameter == "T")
            {
                BBidAskType = true;
                OrderListView = false;
                OrderView = true;
                GetChance(Currency + "-" + OrderCoin);
                ExecuteCommandIsMarket(BidAskType);
            }
            else if (parameter == "F")
            {
                BBidAskType = false;
                OrderView = true;
                OrderListView = false;
                GetChance(Currency + "-" + OrderCoin);
                ExecuteCommandIsMarket(BidAskType);
            }
            else
            {
                OrderListView = true;
                OrderView = false;
            }
            MarketBuy = MarketBuy;
            MarketSell = MarketSell;
            
            //현재 매수 매도타입을 필요한곳에 전달
            _ea.GetEvent<MessageBidAskCheckEvent>().Publish(BBidAskType);
            SetGridColor();
            

        }

        /// <summary>
        /// 지정가인지 시장가 주문인지 선택하는 이벤트
        /// </summary>
        private DelegateCommand<string> commandismarket;
        public DelegateCommand<string> CommandIsMarket =>
            commandismarket ?? (commandismarket = new DelegateCommand<string>(ExecuteCommandIsMarket));

        void ExecuteCommandIsMarket(string p)
        {
            if (p == "l")
            {
                IsMarket = false;//지정가
                MarketBuy = true;
                MarketSell = true;
            }
            else
            {
                IsMarket = true;//시장가
                if (BBidAskType)
                {
                    //매수
                    MarketBuy = true;
                    MarketSell = false;
                }
                else
                {
                    //매도
                    MarketBuy = false;
                    MarketSell = true;
                }
                
            }
            BidAskType = p;
        }

        private DelegateCommand<object> commandcoinorder;
        public DelegateCommand<object> CommandCoinOrder =>
            commandcoinorder ?? (commandcoinorder = new DelegateCommand<object>(ExecuteCommandCoinOrder));

        void ExecuteCommandCoinOrder(object parameter)
        {
            
            bool IsOrder = CoinOrder();


            if (IsOrder)
            {
                //주문성공
                _ea.GetEvent<MessageCoinOrderEvent>().Publish(IsOrder);
            }
            else
            {
                //주문 실패
            }
        }

        public bool CoinOrder()
        {

            Language.Lang.Message msg = new Language.Lang.Message();
            if (Quantity == 0 || Price == 0)
                return false;
            if ((Price * Quantity) < OrderMin)
            {
                MessageBox.Show(msg.LErrorOrderMinPrice + " -> " + OrderMin.ToString());
                return false;
            }
            if ((BBidAskType == false && Quantity > ActiveOrderPrice) || 
                (BBidAskType && (TotalPrice * (1 + Fee)) > ActiveOrderPrice))
            {
                MessageBox.Show(msg.LErrorOrderOverflowQuantity);
                return false;
            }

            string ordertype = BBidAskType ? "bid" : "ask";
            string markettype = IsMarket ? (BBidAskType ? "price" : "market") : "limit";

            var IsOrder = Task.Run(() => Orderfnc.Orders(Currency + "-" + OrderCoin,
                ordertype,
                Quantity,
                Price,
                markettype,
                Price * Quantity
                )).Result;

            return IsOrder;
        }


        /// <summary>
        /// 주문내역
        /// </summary>
        private DelegateCommand<string> selectedorderlist;
        public DelegateCommand<string> CommandSelectedOrderList =>
            selectedorderlist ?? (selectedorderlist = new DelegateCommand<string>(ExecuteSelectedOrderList));

        void ExecuteSelectedOrderList(string ordertype)
        {
            
            if (ordertype == "" || ordertype is null)
                return;

            GetOrderList(SelectCoin, ordertype);
            

            KeepOrderListType = ordertype;
        }


        /// <summary>
        /// 주문취소 버튼
        /// </summary>
        private DelegateCommand<string> ordercancel;
        public DelegateCommand<string> CommandOrderCancel =>
            ordercancel ?? (ordercancel = new DelegateCommand<string>(ExecuteCommandOrderCancel));

        void ExecuteCommandOrderCancel(string uuid)
        {
            Structs.OrderCancel a = Task.Run(() => Orderfnc.OrderCancelAsync(uuid)).Result;
            GetOrderList(SelectCoin, KeepOrderListType);
            _ea.GetEvent<MessageCoinOrderCancle>().Publish();
        }
        #endregion Event

        #region Functions
        private double GetDoubleValue(double val)
        {
            int places = 8;
            double value = Math.Floor(val * Math.Pow(10, places)) / Math.Pow(10, places); 
            return value;

        }
        private string GetNumberic(string str)
        {
            string val = "";
            
            string pattern = @"[^0-9.]";
            val = Regex.Replace(str, pattern, "");

            return val;
        }

        private bool CheckedDot(string str)
        {
            bool b = false;
            int count = str.Split(".").Length - 1;
            b = count >= 2 ? false : true;

            return b;
        }
        
        /// <summary>
        /// 가격 변경이 일어났을때 작동함수.
        /// </summary>
        private void ChangePrice()
        {
            if (Price == 0 || Quantity == 0)
                return;
            IsTotalPrice = true;
            TotalPrice = Price * Quantity;
            StrTotalPrice = TotalPrice.ToString("F8");
        }

        /// <summary>
        /// 수량 변경
        /// </summary>
        private void ChangeTotalPrice()
        {
            if (TotalPrice == 0 || Price == 0)
                return;
            IsQuantity = true;
            Quantity = TotalPrice / Price;
            StrQuantity = Quantity.ToString("F8");
        }

        /// <summary>
        /// 수량 변경
        /// </summary>
        private void ChangeQuantity()
        {
            if (Quantity == 0 || Price == 0)
                return;
            IsTotalPrice = true;
            TotalPrice = Price * Quantity;
            StrTotalPrice = TotalPrice.ToString("F8");
        }

        /// <summary>
        /// 매수 매도 그리드 컬러 변경해주는 함수
        /// </summary>
        private void SetGridColor()
        {
            if (BBidAskType)
            {
                GridColor = MyColors.ColorBidBack;
                BtnBorder = MyColors.ColorBid;
                BtnText = Lang.LCoinBuy;
            }
            else
            {
                GridColor = MyColors.ColorAskBack;
                BtnBorder = MyColors.ColorAsk;
                BtnText = Lang.LCoinSell;
            }
            if (OrderListView)
            {
                GridColor = MyColors.ColorBack;
                GetOrderList(SelectCoin, KeepOrderListType);
            }
            BtnBack = GridColor;

        }
        private void GetOrderList(string market, string listtype)
        {
            if (Currency != "" && OrderCoin != "")
            {
                OrderList = Task.Run(() => Orderfnc.OrderListAsync(market,listtype)).Result;
            }
        }
        private void GetMarket(Structs.MarketCodes market)
        {
            SetCoin(market.Market);
        }

        private void GetMarket(Structs.Accounts market)
        {
            SetCoin(market.Market);
        }

        /// <summary>
        /// 외부 이벤트로 발생했을때 셋팅
        /// </summary>
        /// <param name="market"></param>
        private void SetCoin(string market)
        {
            SelectCoin = market;
            string[] str = market.Split("-");
            Currency = str[0];
            OrderCoin = str[1];
            GetChance(market);

            GetOrderList(SelectCoin, KeepOrderListType);
        }


        /// <summary>
        /// 호가창에서 선택된 가격으로 변경
        /// </summary>
        /// <param name="mp"></param>
        private void GetPrice(Structs.MessageCoinPirce mp)
        {
            Price = mp.Price;
            StrPrice = Price.ToString();
        }

        private void GetChance(string market)
        {
            Order order = new Order();
            Chance = Task.Run(() => order.OrderChanceAsync(market)).Result;
            if (BBidAskType)
            {
                ActiveOrderPrice = chance.bid_account_balance;
                AcitiveOrderCurrency = Currency;
                OrderMin = chance.market_bid_min_total;
                Fee = chance.bid_fee;
            }
            else
            {
                ActiveOrderPrice = chance.ask_account_balance;
                AcitiveOrderCurrency = OrderCoin;
                OrderMin = chance.market_ask_min_total;
                Fee = chance.ask_fee;
            }
            //해당 코인이 주문 가능한 상태일경우
            GetActiveOrder(Chance.state);
        }

        /// <summary>
        /// 매수 매도 버튼비활성화
        /// </summary>
        /// <param name="type"></param>
        private void GetActiveOrder(string type)
        {
            if (type == "active")
                ActiveOrder = true;
            else
                ActiveOrder = false;
        }
        #endregion Functions

        #region Interface

        #endregion Interface
        public CoinOrderViewModel(IEventAggregator ea)
        {
            
            _ea = ea;
            _ea.GetEvent<MessageCoinNameEvent>().Subscribe(GetMarket);
            _ea.GetEvent<MessageAccountEvent>().Subscribe(GetMarket);
            _ea.GetEvent<MessageCoinPriceEvent>().Subscribe(GetPrice);

            OrderListView = false;
            OrderView = true;

            MarketBuy = true;
            MarketSell = true;

            OrderCoin = Lang.LPrice;
            Currency = Lang.LCurrency;
            AcitiveOrderCurrency = Lang.LCurrency;

            //언어 변경됐을경우 이벤트호출
            Language.Language.LangChangeEvent += (SetLanguage);
            SetLanguage();

            //색변경 이벤트 등록
            PublicColor.Colors.ColorUpdate += (EventUpdateColor);

            //그리드 배경색
            SetGridColor();
        }
    }
}
