using FileIO;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using WebSocketSharp;
using WebSocket = WebSocketSharp.WebSocket;

namespace Upbit.Functions
{
    public class WebSocketTicker
    {

        public delegate void TickEventHandler(Structs.Ticker connect);
        public static event TickEventHandler TickerEvent;

        public delegate void OrderEventHandler(Structs.Orderbook connect);
        public static event OrderEventHandler OrderEvent;

        public delegate void TradeEventHandler(Structs.Trade connect);
        public static event TradeEventHandler TradeEvent;

        


        private string CoinName = "";
        private WebSocket? ws = null;
        private List<string>? Coin;
        object bid;
        object ask;

        URLs url = new URLs();

        //실시간 차트를 하기위해 차트 오브젝트 생성
        object MarketView;

        bool Connect = false;


        public WebSocketTicker(List<string> coin = null)
        {
            //코인정보를 넘겨받음
            Coin = coin;

            if(coin is null)
            {
                Logs.Loginstance.WebSocketLog("생성될때 전달받은 코인값이 없습니다.");
            }
            else
            {
                SetWebSocket();
            }
            
        }

        private void SetWebSocket()
        {
            if(ws is null) 
            {
                ws = new WebSocket(url.WebSocketTick);
                ws.OnOpen += ws_open;
                ws.OnClose += ws_close;
                ws.OnMessage += ws_message;
                ws.Connect();
            }
            else
            {
                Logs.Loginstance.WebSocketLog("웹 소켓이 생성되어있습니다.");
            }
        }

        public void SetChangeCoin(List<string> coin)
        {
            //다른 코인을 불러올때 사용.
            //미리 생성했던 소켓을 종료하지않고 다시 생성해서
            //이전 데이터랑 같이 불러ㅎ와지는 문제가 생겨서 추가함
            ws.Close();

            Coin = coin;
            SetWebSocket();
        }

        public void CloseWebsocket()
        {
            ws.Close();
        }
        private void ws_connect()
        {
            
        }

        private void ws_open(object sender, EventArgs e)
        {

            Logs.Loginstance.WebSocketLog("업비트 실시간 소켓 연결");

            JArray array = new JArray();
            JArray array1 = new JArray();
            JObject ticket = new JObject();
            JObject trade = new JObject();
            JObject orderbook = new JObject();
            JObject format = new JObject();
            JObject ticker = new JObject();

            string str;

            foreach (var coin in Coin)
            {
                array.Add(coin + ".10");
                array1.Add(coin);
            }

            Logs.Loginstance.WebSocketLog($"{CoinName}코인 실시간 소켓 연결");

            ticket["ticket"] = Guid.NewGuid().ToString();//UUID

            orderbook["type"] = "orderbook";
            orderbook["codes"] = array;
            orderbook["isOnlyRealtime"] = "true";        //실시간 시세만 요청

            trade["type"] = "trade";
            trade["codes"] = array1;
            trade["isOnlyRealtime"] = "true";        //실시간 시세만 요청

            ticker["type"] = "ticker";
            ticker["codes"] = array1;
            //obj5["isOnlyRealtime"] = "true";


            format["format"] = "SIMPLE";



            str = string.Format("[{0},{1},{2},{3},{4}]", ticket.ToString(), trade.ToString(), orderbook.ToString(), ticker.ToString(), format.ToString());

            //str = string.Format("[{0},{1},{2},{3},{4},{5}]", obj1.ToString(), obj2.ToString(), obj3.ToString(), obj5.ToString(), obj6.ToString(), obj4.ToString());
            ws.Send(str);
            //tradeserver.Enabled = true;
        }

        private void ws_close(object sender, EventArgs e)
        {
            Connect = false;

            //소켓 연결종료 로그 남김
            Logs.Loginstance.WebSocketLog("업비트 실시간 소켓 종료");
        }

        private void ws_message(object sender, MessageEventArgs e)
        {
            Connect = true;
            bool bTrade = false;
            bool bOrderbook = false;
            JObject jsonObject = JObject.Parse(Encoding.UTF8.GetString(e.RawData));



            string types = (string)jsonObject.GetValue("ty");


            if (types == "trade")
            {
                Structs.Trade obj = new Structs.Trade();
                obj.ty = (string)jsonObject.GetValue("ty");
                obj.cd = (string)jsonObject.GetValue("cd");
                obj.tp = (double)jsonObject.GetValue("tp");
                obj.tv = (double)jsonObject.GetValue("tv");
                obj.ab = (string)jsonObject.GetValue("ab");
                obj.pcp = jsonObject.GetValue("pcp").Type == JTokenType.Null ?  0 : (double)jsonObject.GetValue("pcp");
                obj.c = jsonObject.GetValue("c").Type == JTokenType.Null ? "" : (string)jsonObject.GetValue("c");
                obj.cp = jsonObject.GetValue("cp").Type == JTokenType.Null ? 0 : (double)jsonObject.GetValue("cp");
                obj.td = (string)jsonObject.GetValue("td");
                obj.ttm = (string)jsonObject.GetValue("ttm");
                obj.ttms = (long)jsonObject.GetValue("ttms");
                obj.tms = (long)jsonObject.GetValue("tms");
                obj.sid = (long)jsonObject.GetValue("sid");
                obj.st = (string)jsonObject.GetValue("st");

                if(TradeEvent != null && obj.st == "SNAPSHOT")
                    TradeEvent(obj);
            }
            else if (types == "orderbook")
            {
                Structs.Orderbook obj = new Structs.Orderbook();
                obj.ty = (string)jsonObject.GetValue("ty");
                obj.cd = (string)jsonObject.GetValue("cd");
                obj.tas = (double)jsonObject.GetValue("tas");
                obj.tbs = (double)jsonObject.GetValue("tbs");
                obj.obu = new List<Structs.OrderbookUnit>();
                obj.tms = (long)jsonObject.GetValue("tms");

                foreach (var items in jsonObject.Children())
                {
                    //호가 데이터 영역
                    if (items.Path == @"obu")
                    {
                        JArray jsonArray_child = JArray.Parse(items.First.ToString());

                        foreach (var item in jsonArray_child.Children())
                        {
                            Structs.OrderbookUnit obj_unit = new Structs.OrderbookUnit();
                            obj_unit.ap = Convert.ToDouble(item["ap"].ToString());
                            obj_unit._as = Convert.ToDouble(item["as"].ToString());
                            obj_unit.bp = Convert.ToDouble(item["bp"].ToString());
                            obj_unit.bs = Convert.ToDouble(item["bs"].ToString());
                            obj.obu.Add(obj_unit);
                        }

                    }

                }

                if (OrderEvent != null)
                    OrderEvent(obj);

            }
            else if (types == "ticker")
            {
                Structs.Ticker obj = new Structs.Ticker();
                obj.ty = (string)jsonObject.GetValue("ty");             //타입
                obj.cd = (string)jsonObject.GetValue("cd");             //마켓코드

                obj.op = (double)jsonObject.GetValue("op");             //시가
                obj.hp = (double)jsonObject.GetValue("hp");             //고가
                obj.lp = (double)jsonObject.GetValue("lp");             //저가
                obj.tp = (double)jsonObject.GetValue("tp");             //현재가
                obj.pcp = (double)jsonObject.GetValue("pcp");           //전일 종가

                obj.c = (string)jsonObject.GetValue("c");               //전일대비 올랐는지 안올랐는지

                obj.cp = (double)jsonObject.GetValue("cp");             //부호없는 전일대비값
                obj.scp = (double)jsonObject.GetValue("scp");           //전일대비 값
                obj.cr = (double)jsonObject.GetValue("cr");             //부호없는 전일대비 등락율
                obj.scr = (double)jsonObject.GetValue("scr");           //전일대비 등략율
                obj.tv = (double)jsonObject.GetValue("tv");             //최근거래량
                obj.atv = (double)jsonObject.GetValue("atv");           //누적거래량
                obj.atv24h = (double)jsonObject.GetValue("atv24h");     //24시간 누적거래량
                obj.atp = (double)jsonObject.GetValue("atp");           //누적거래대금
                obj.atp24h = (double)jsonObject.GetValue("atp24h");     //24시간 누적 거래대금

                obj.tdt = (string)jsonObject.GetValue("tdt");           //최근거래일자
                obj.ttm = (string)jsonObject.GetValue("ttm");           //최근거래시각
                obj.ttms = (long)jsonObject.GetValue("ttms");           //체결타임 스탬프

                obj.ab = (string)jsonObject.GetValue("ab");             //매수/매도 구분

                obj.aav = (double)jsonObject.GetValue("aav");           //누적매도량
                obj.abv = (double)jsonObject.GetValue("abv");           //누적매수량
                obj.h52wp = (double)jsonObject.GetValue("h52wp");       //52주 최저가
                obj.h52wdt = (string)jsonObject.GetValue("h52wdt");     //52주 최고가 달성일
                obj.l52wp = (double)jsonObject.GetValue("l52wp");       //52주 최저가
                obj.l52wdt = (string)jsonObject.GetValue("l52wdt");     //52주 최저가 달성일

                obj.ts = (string)jsonObject.GetValue("ts");             //거래상태
                obj.ms = (string)jsonObject.GetValue("ms");             //거래상태
                obj.msfi = (string)jsonObject.GetValue("msfi");         //거래상태
                obj.its = (bool)jsonObject.GetValue("its");             //거래 정지 여부
                //obj.dd = (string)jsonObject.GetValue("dd");             //상장폐지일
                obj.mw = (string)jsonObject.GetValue("mw");             //유의종목
                obj.tms = (long)jsonObject.GetValue("tms");             //타임스탬프
                obj.st = (string)jsonObject.GetValue("st");             //스트림타입

                if(TickerEvent != null)
                    TickerEvent(obj);
            }
            else if (types == "myTrade")
            {
                Structs.MyTrade obj = new Structs.MyTrade();
                obj.ty = (string)jsonObject.GetValue("ty");         //myTrade
                obj.cd = (string)jsonObject.GetValue("cd");         //마켓코드
                obj.ab = (string)jsonObject.GetValue("ab");         //매수 매도 구분
                obj.p = (double)jsonObject.GetValue("p");           //체결가격
                obj.v = (double)jsonObject.GetValue("v");           //체결량
                obj.ouid = (string)jsonObject.GetValue("ouid");     //주문의 고유 아이디
                obj.ot = (string)jsonObject.GetValue("ot");         //주문타입
                obj.ab = (string)jsonObject.GetValue("ab");         //체결의 고유 아이디
                obj.tuid = (string)jsonObject.GetValue("tuid");     //체결의 고유 아이디
                obj.ttms = (long)jsonObject.GetValue("ttms");       //체결타임스탬프
                obj.st = (string)jsonObject.GetValue("st");         //스트림타입


            }



        }
    }
}
