using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows;
using System.ComponentModel;
using static Upbit.Structs;
using System.Windows.Controls.Primitives;
using Newtonsoft.Json.Linq;

namespace Upbit
{
    public class Structs
    {
        public struct MessageCoinPirce
        {
            
            public bool Check { get; set; }     //매수 = true / 매도 = false
            public double Price { get; set; }   //가격
        }
        public class Accounts : INotifyPropertyChanged
        {
            private Brush color;
            public Brush Color
            {
                get { return color; }
                set 
                {
                    color = value;
                    OnPropertyChanged(nameof(color));
                }
            }
            private string coinname;
            public string CoinName
            {
                get { return coinname; }
                set 
                {
                    coinname = value;
                    OnPropertyChanged(nameof(coinname));
                }
            }
            private string currency;//코인
            public string Currency
            {
                get { return currency; }
                set { 
                    currency = value;
                    OnPropertyChanged(nameof(currency));
                }
            }

            private string unitcurruncy;
            public string UnitCurrency//화폐
            {
                get { return unitcurruncy; }
                set { 
                    unitcurruncy = value;
                    OnPropertyChanged(nameof(unitcurruncy));
                }
            }

            public string Market { 
                get {
                    return UnitCurrency + "-" + Currency;
                } 
            }

            private string balance;//주문가능금액/수량
            public string Balance
            {
                get { return balance; }
                set { 
                    balance = value;
                    Cost = Convert.ToDouble(AvgBuyPrice) * (Convert.ToDouble(Balance) + Convert.ToDouble(Locked));
                    OnPropertyChanged(nameof(balance));
                }
            }
            private string locked;
            public string Locked//주문들어가있는
            {
                get { return locked; }
                set {
                    locked = value;
                    Cost = Convert.ToDouble(AvgBuyPrice) * (Convert.ToDouble(Balance) + Convert.ToDouble(Locked));
                    OnPropertyChanged(nameof(locked));
                }
            }
            private string avgbuyprice;//매수평균가
            public string AvgBuyPrice
            {
                get { return avgbuyprice; }
                set {
                    avgbuyprice = value;
                    Cost = Convert.ToDouble(AvgBuyPrice) * (Convert.ToDouble(Balance) + Convert.ToDouble(Locked));
                    OnPropertyChanged(nameof(avgbuyprice));
                }
            }
            private bool avgbuypricemodified;//매수평균가 수정여부
            public bool AvgBuyPriceModified
            {
                get { return avgbuypricemodified; }
                set { 
                    avgbuypricemodified = value;
                    OnPropertyChanged(nameof(avgbuypricemodified));
                }
            }
            private double rate;
            public double Rate
            {
                get { return rate; }
                set {
                    double r = value / Convert.ToDouble(AvgBuyPrice);
                    r -= 1;
                    rate = r * 100;
                    OnPropertyChanged(nameof(rate));
                }
            }

            private double totalprice;
            public double TotalPrice
            {
                get { return totalprice; }
                set { 
                    double volume = value * (Convert.ToDouble(Balance) + Convert.ToDouble(Locked));
                    totalprice = volume;
                    OnPropertyChanged(nameof(totalprice));
                }
            }
            
            private double cost;
            public double Cost
            {
                get { return cost; }
                set 
                { 
                    cost = value;
                    OnPropertyChanged(nameof(cost));
                }
            }

            private double totalbalance;
            public double TotalBalance
            {
                get { return Convert.ToDouble(Locked) + Convert.ToDouble(Balance); }
            }

            private double nowprice = 0;//현재가 저장
            public double NowPrice
            {
                get { return nowprice; }
                set { 
                    nowprice = value;
                    OnPropertyChanged(nameof(nowprice));
                }
            }

            public event PropertyChangedEventHandler PropertyChanged;

            protected virtual void OnPropertyChanged(string propertyName)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }

        }
        public class MarketCodes : INotifyPropertyChanged
        {
            private Brush color;
            public Brush Color
            {
                get { return color; }
                set
                {
                    color = value;
                    OnPropertyChanged(nameof(color));
                }
            }

            private string printname;
            public string Print_name
            {
                get { return printname; }
                set 
                {
                    printname = value;
                    OnPropertyChanged(nameof(printname));
                }
            }
            public string Market { get; set; }//시장정보 ex)KRW-BTC
            public string Korean_name { get; set; }//한글이름
            public string English_name { get; set; }//영어이름

            private double rate;
            public double Rate
            {
                get { return rate; }
                set
                {
                    rate = value;
                    OnPropertyChanged(nameof(rate));
                }
            }
            public string Market_warning { get; set; }//투자주의 NONE / Caution



            public event PropertyChangedEventHandler PropertyChanged;

            protected virtual void OnPropertyChanged(string propertyName)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        /// <summary>
        /// 현재가
        /// </summary>
        public struct Ticker
        {
            public string ty { get; set; }
            public string cd { get; set; }
            public double op { get; set; }
            public double hp { get; set; }
            public double lp { get; set; }
            public double tp { get; set; }
            public double pcp { get; set; }
            public string c { get; set; }
            public double cp { get; set; }
            public double scp { get; set; }
            public double cr { get; set; }
            public double scr { get; set; }
            public double tv { get; set; }
            public double atv { get; set; }
            public double atv24h { get; set; }
            public double atp { get; set; }
            public double atp24h { get; set; }
            public string tdt { get; set; }
            public string ttm { get; set; }
            public long ttms { get; set; }
            public string ab { get; set; }
            public double aav { get; set; }

            public double abv { get; set; }
            public double h52wp { get; set; }
            public string h52wdt { get; set; }
            public double l52wp { get; set; }
            public string l52wdt { get; set; }
            public string ts { get; set; }
            public string ms { get; set; }
            public string msfi { get; set; }
            public bool its { get; set; }

            public string dd { get; set; }
            public string mw { get; set; }
            public long tms { get; set; }
            public string st { get; set; }

        }

        public struct MyTrade
        {
            public string ty { get; set; }//myTrade
            public string cd { get; set; }//마켓코드
            public string ab { get; set; }//매수매도구분
            public double p { get; set; }//체결가격
            public double v { get; set; }//체결량
            public string ouid { get; set; }//주문고유아이디
            public string ot { get; set; }//주문타입
            public string tuid { get; set; }//체결고유아이디
            public long ttms { get; set; }//체결타임스탬프
            public string st { get; set; }//스트림타임

        }

        /// <summary>
        /// 체결
        /// </summary>
        public struct Trade
        {
            public string ty { get; set; }
            public string cd { get; set; }
            public double tp { get; set; }
            public double tv { get; set; }
            public string ab { get; set; }
            public double pcp { get; set; }
            public string c { get; set; }
            public double cp { get; set; }
            public string td { get; set; }
            public string ttm { get; set; }
            public long ttms { get; set; }
            public long tms { get; set; }
            public long sid { get; set; }
            public string st { get; set; }
        }

        /// <summary>
        /// 호가
        /// </summary>
        public struct Orderbook
        {
            public string ty { get; set; }
            public string cd { get; set; }
            public double tas { get; set; }
            public double tbs { get; set; }
            public List<OrderbookUnit> obu { get; set; }
            public long tms { get; set; }
        }

        public struct OrderbookUnit
        {
            public double ap { get; set; }
            public double _as { get; set; }

            public double bp { get; set; }
            public double bs { get; set; }
        }

        public struct BidAskUnit
        {
            public string unitp { get; set; }
            public string units { get; set; }
            public bool Check { get; set; } // 매수 = true / 매도 = false;
        }

        public struct Chance
        {
            public double bid_fee { get; set; }
            public double ask_fee { get; set; }
            public double maker_bid_fee { get; set; }
            public double maker_ask_fee { get; set; }
            public string market_id { get; set; }
            public string market_name { get; set; }
            public string market_order_types { get; set; }
            public string market_ask_types { get; set; }
            public string market_bid_types { get; set; }
            public string market_order_sides { get; set; }

            public string market_bid_currency { get; set; }
            public double market_bid_price_unit { get; set; }
            public double market_bid_min_total { get; set; }

            public string market_ask_currency { get; set; }
            public double market_ask_price_unit { get; set; }
            public double market_ask_min_total { get; set; }
            public double max_total { get; set; }
            public string state { get; set; }

            public string bid_account_currency { get; set; }
            public double bid_account_balance { get; set; }
            public double bid_account_locked { get; set; }
            public double bid_account_avg_buy_price { get; set; }
            public bool bid_account_avg_buy_price_modified { get; set; }
            public string bid_account_unit_currency { get; set; }

            public string ask_account_currency { get; set; }
            public double ask_account_balance { get; set; }
            public double ask_account_locked { get; set; }
            public double ask_account_avg_buy_price { get; set; }
            public bool ask_account_avg_buy_price_modified { get; set; }
            public string ask_account_unit_currency { get; set; }

        }


        /// <summary>
        ///  주문리스트 조회
        /// </summary>
        public struct OrderList
        {
            public string uuid { get; set; }//주문의 고유 아이디
            public string side { get; set; }//주문 종류
            public string ord_type { get; set; }//주문 방식
            public string price { get; set; }//주문 당시 화폐 가격
            public string state { get; set; }//주문 상태
            public string market { get; set; }//	마켓의 유일키
            public string created_at { get; set; }//	주문 생성 시간
            public string volume { get; set; }//사용자가 입력한 주문 양
            public string remaining_volume { get; set; }//	체결 후 남은 주문 양
            public string reserved_fee { get; set; }//	수수료로 예약된 비용
            public string remaining_fee { get; set; }//남은 수수료
            public string paid_fee { get; set; }//사용된 수수료
            public string locked { get; set; }//거래에 사용중인 비용
            public string executed_volume { get; set; }//체결된 양
            public string trades_count { get; set; }//	해당 주문에 걸린 체결 수
        }

        public struct OrderCancel
        {
            public string uuid { get; set; }            //주문고유아이디
            public string side { get; set; }            //주문종류
            public string ord_type { get; set; }        //주문방식
            public string price { get; set; }           //주문당시 화폐가격
            public string state { get; set; }           //주문상태
            public string market { get; set; }          //마켓
            public string created_at { get; set; }      //주문생성시간    
            public string volume { get; set; }          //주문양
            public string remaining_valume { get; set; }//체결후 남은 양
            public string reserved_fee { get; set; }    //수수료 예약 비용
            public string remaining_fee { get; set; }   //남은 수수료
            public string paid_fee { get; set; }        //사용된 수수료
            public string locked { get; set; }          //거래에 사용중인 비용
            public string executed_volume { get; set; } //체결된 양
            public int trades_count { get; set; }       //해당 주문에 걸린 체결수
        }

        public class Tick : INotifyPropertyChanged
        {
            private double x;
            public double X
            {
                get { return x; }
                set {
                    x = value;
                    OnPropertyChanged(nameof(X)); }
            }
            public double Y { get; set; }

            private string utc;
            public string UTC
            {
                get { return utc; }
                set
                {
                    utc = value;
                    OnPropertyChanged(nameof(utc));
                }
            }


            private string kst;
            public string KST
            {
                get { return kst; }
                set
                {
                    kst = value;
                    OnPropertyChanged(nameof(kst));
                }
            }

            

            private double hp;//고가
            public double HP
            {
                get { return hp; }
                set
                {
                    hp = value;
                    OnPropertyChanged(nameof(hp));
                }
            }

            private double op;//시가
            public double OP
            {
                get { return op; }
                set
                {
                    op = value;
                    OnPropertyChanged(nameof(op));
                }
            }

            private double lp;//저가
            public double LP
            {
                get { return lp; }
                set
                {
                    lp = value;
                    OnPropertyChanged(nameof(lp));
                }
            }

            private double tp;//종가
            public double TP
            {
                get { return tp; }
                set
                {
                    tp = value;
                    OnPropertyChanged(nameof(tp));
                }
            }
            private double candlestartpoint;
            public double CandleStartPoint
            {
                get
                {
                    
                    return candlestartpoint;
                }
                set {

                    double result;
                    double val;
                    if (OP >= TP)
                    {
                        val = OP;
                    }
                    else
                    {
                        val = TP;
                    }
                    result = value - val;
                    candlestartpoint = Math.Abs(result);
                    OnPropertyChanged(nameof(candlestartpoint)); }
            }

            private double volume;
            public double Volume
            {
                get { return volume; }

                set {
                    volume = value;
                    OnPropertyChanged(nameof(volume));
                }
            }
            private double candle;
            public double Candle { 
                get {
                    return Math.Abs(OP - TP);
                }
                set {
                    OnPropertyChanged(nameof(candle)); }
            }
            private double tail;
            public double Tail
            {
                get
                {
                    return Math.Abs(HP - LP);
                }
                set {
                    tail = Math.Abs(HP - LP);
                    OnPropertyChanged(nameof(tail)); }
            }

            private double timestamp;//해당캔들 마지막 틱저장시간
            public double Timestamp
            {
                get { return timestamp; }
                set
                {
                    timestamp = value;
                    OnPropertyChanged(nameof(timestamp));
                }
            }

            private Brush colors;//색상
            public Brush Colors
            {
                get { return colors; }
                set
                {
                    colors = value;
                    OnPropertyChanged(nameof(colors));
                }
            }
            public void PreNumber(int num = 1)
            {
                this.X -= num;
            }
            public void NextNumber(int num = 1)
            {
                this.X += num;
            }
            public event PropertyChangedEventHandler PropertyChanged;

            protected virtual void OnPropertyChanged(string propertyName)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public class ChartSideCursor : INotifyPropertyChanged
        {
            private double x;
            public double X
            {
                get { return x; }
                set
                {
                    x = value;
                    OnPropertyChanged(nameof(x));
                }
            }

            private double y;
            public double Y
            {
                get { return y; }
                set
                {
                    y = value;
                    OnPropertyChanged(nameof(y));
                }
            }

            private string cursorvalue;
            public string CursorValue
            {
                get { return cursorvalue; }
                set
                {
                    cursorvalue = value;
                    OnPropertyChanged(nameof(cursorvalue));
                }
            }
            private Brush backcolor;//색상
            public Brush BackColor
            {
                get { return backcolor; }
                set
                {
                    backcolor = value;
                    OnPropertyChanged(nameof(backcolor));
                }
            }

            private Brush color;//색상
            public Brush Color
            {
                get { return color; }
                set
                {
                    color = value;
                    OnPropertyChanged(nameof(color));
                }
            }
            public string Type { get; set; }
            public event PropertyChangedEventHandler PropertyChanged;

            protected virtual void OnPropertyChanged(string propertyName)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        public struct Candle
        {
            //공용데이터
            public string utc { get; set; }
            public string kst { get; set; }
            public double op { get; set; }//시가
            public double hp { get; set; }
            public double lp { get; set; }
            public double tp { get; set; }
            public long timestamp { get; set; }//해당캔들 마지막 틱저장시간
            public double candle_acc_trade_price { get; set; }
            public double candle_acc_trade_volume { get; set; }

            public double X { get; set; }
            public double Y { get; set; }
            public double TickHeight { get; set; }
            public Thickness TickMargin { get; set; }
            public double TailHeight { get; set; }

            //일 
            public double prev_closing_price { get; set; }
            public double change_price { get; set; }
            public double change_rate { get; set; }
            public double converted_trade_price { get; set; }

            //주 / 월
            public string first_day_of_period { get; set; }
        }


        public struct GridDataValue
        {
            public double val { get; set; }
            public int index { get; set; }
        }
        public class GridData
        {
            public double unit = 1000;//기본 Y축 단위값
            public double Timestemp;//X축 시간 단위
            public double Max, Min, Mid;
            public int Cursor = 0;
            private int KeepDataCnt = 5;
            public List<GridDataValue> ListMax = new List<GridDataValue>();
            public List<GridDataValue> ListMin = new List<GridDataValue>();
            public GridData()
            {
                Max = 0.0f;
                Min = 9999999999.0f;
                Mid = 0.0f;
            }
            public bool SetMax(double val)
            {
                if (Max < val)
                {
                    Max = val;
                    GridDataValue gridDataValue = new GridDataValue();
                    gridDataValue.val = val;
                    return true;
                }
                return false;

            }
            public bool SetMin(double val)
            {
                if (val < Min)
                {
                    Min = val;
                    Mid = (Min + Max) / 2;
                    unit = (Max - Min) / 20;
                    GridDataValue gridDataValue = new GridDataValue();
                    gridDataValue.val = val;

                    return true;
                }
                return false;
            }
            public bool SetMax(double val, int index)
            {
                if (Max < val)
                {
                    Max = val;
                    GridDataValue gridDataValue = new GridDataValue();
                    gridDataValue.val = val;
                    gridDataValue.index = index;
                    if (ListMax.Count >= KeepDataCnt)
                    {
                        ListMax.RemoveAt(KeepDataCnt - 1);
                        ListMax.Insert(0, gridDataValue);
                    }
                    ListMax.Insert(0, gridDataValue);
                    return true;
                }
                return false;

            }
            public bool SetMin(double val, int index)
            {
                if (val < Min)
                {
                    Min = val;
                    Mid = (Min + Max) / 2;
                    unit = (Max - Min) / 20;
                    GridDataValue gridDataValue = new GridDataValue();
                    gridDataValue.val = val;
                    gridDataValue.index = index;
                    if (ListMin.Count >= KeepDataCnt)
                    {
                        ListMin.RemoveAt(KeepDataCnt - 1);
                        ListMin.Insert(0, gridDataValue);
                    }
                    ListMin.Insert(0, gridDataValue);

                    return true;
                }
                return false;
            }
            public bool RemoveMin()
            {
                if (ListMin.Count >= 1)
                {
                    ListMin.RemoveAt(0);
                }
                Min = ListMin[0].val;
                Mid = (Min + Max) / 2;
                unit = (Max - Min) / 20;

                if (ListMin.Count == 0)
                    return true;
                else
                    return false;
            }
            public bool RemoveMax()
            {
                if (ListMax.Count >= 1)
                {
                    ListMax.RemoveAt(0);
                }
                Max = ListMax[0].val;
                Mid = (Min + Max) / 2;
                unit = (Max - Min) / 20;

                if (ListMax.Count == 0)
                    return true;
                else
                    return false;
            }


            public void AddIndex(int index)
            {
                for (int i = 0; i < ListMax.Count; i++)
                {
                    GridDataValue gridDataValue = ListMax[i];
                    gridDataValue.index += index;
                    ListMax[i] = gridDataValue;
                }
                for (int i = 0; i < ListMin.Count; i++)
                {
                    GridDataValue gridDataValue = ListMin[i];
                    gridDataValue.index += index;
                    ListMin[i] = gridDataValue;
                }
            }
        }
    }
}
