using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using RestSharp;
using static Upbit.Structs;
using PublicColor;

namespace Upbit.UpbitFunctions
{
    public class PublicFunctions
    {
        URLs url = new URLs();

        /// <summary>
        /// 호가 확인
        /// </summary>
        /// <param name="market">호가 확인이 필요한 마켓코드</param>
        /// <returns></returns>
        public Structs.Orderbook OrderBook(string market)
        {
            Structs.Orderbook orderbook = new Structs.Orderbook();

            var client = new RestClient($"https://api.upbit.com/v1/orderbook?markets={market}");
            var request = new RestRequest();
            request.AddHeader("accept", "application/json");

            try
            {
                RestResponse response = client.Execute(request);
                JArray jsonArray = JArray.Parse(response.Content);

                foreach (var item in jsonArray.Children())
                {

                    orderbook.cd = item["market"].ToString();
                    orderbook.tas = Convert.ToDouble(item["total_ask_size"].ToString());
                    orderbook.tbs = Convert.ToDouble(item["total_bid_size"].ToString());
                    orderbook.obu = new List<Structs.OrderbookUnit>();
                    orderbook.tms = Convert.ToInt64(item["timestamp"].ToString());

                    JArray jsonArray_child = JArray.Parse(item["orderbook_units"].ToString());
                    foreach (var item1 in jsonArray_child.Children())
                    {
                        Structs.OrderbookUnit obj_unit = new Structs.OrderbookUnit();
                        obj_unit.ap = Convert.ToDouble(item1["ask_price"].ToString());
                        obj_unit._as = Convert.ToDouble(item1["ask_size"].ToString());
                        obj_unit.bp = Convert.ToDouble(item1["bid_price"].ToString());
                        obj_unit.bs = Convert.ToDouble(item1["bid_size"].ToString());
                        orderbook.obu.Add(obj_unit);
                    }
                }


                return orderbook;
            }
            catch (Exception e)
            {
                Debug.WriteLine("호가정보조회 오류");
                return orderbook;
            }
        }

        /// <summary>
        /// 코인목록 읽어옴
        /// </summary>
        /// <returns></returns>
        public async Task<List<Structs.MarketCodes>> MarketsAsync()
        {
            List<Structs.MarketCodes> ListResult = new List<Structs.MarketCodes>();

            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(url.Market),
                Headers =
                    {
                        { "accept", "application/json" },
                        //{ "Authorization", JWT.GetJWT() },
                    },
            };
            
            try
            {
                using (var response = await client.SendAsync(request))
                {
                    response.EnsureSuccessStatusCode();
                    var body = await response.Content.ReadAsStringAsync();
                    Debug.WriteLine(body);

                    JArray jsonArray = JArray.Parse(body);

                    foreach (var item in jsonArray.Children())
                    {
                        Structs.MarketCodes lr = new Structs.MarketCodes();

                        lr.Market = item["market"].ToString();
                        lr.Korean_name = item["korean_name"].ToString();
                        lr.English_name = item["english_name"].ToString();
                        lr.Market_warning = item["market_warning"] is null ? "X" : "O";
                        

                        ListResult.Add(lr);
                    }

                    return ListResult;
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine("코인목록 오류");
                return ListResult;
            }


        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type">분,일,월</param>
        /// <param name="unit">분으로 호출됐을때 분 값</param>
        /// <param name="market">코인명</param>
        /// <param name="time">해당시간기준으로 데이터호출</param>
        /// <param name="cnt">최대호출가능숫자 200</param>
        /// <returns></returns>
        public List<Candle> Candles(string type = "minutes", int unit = 1, string market = "KRW-BTC", string time = "", int cnt = 200)
        {
            List<Candle> candles = new List<Candle>();

            var client = new RestClient($"https://api.upbit.com/v1/candles/{((type == "minutes") ? (type + "/" + unit + "?") : (type + "?"))}market={market}&count={cnt}&to={time}");
            var request = new RestRequest();
            request.AddHeader("accept", "application/json");


            try
            {
                RestResponse response = client.Execute(request);
                JArray jsonArray = JArray.Parse(response.Content);
                foreach (var item in jsonArray.Children())
                {
                    Candle i = new Candle();
                    i.utc = item["candle_date_time_utc"].ToString();
                    i.kst = item["candle_date_time_kst"].ToString();

                    i.op = Convert.ToDouble(item["opening_price"].ToString());
                    i.hp = Convert.ToDouble(item["high_price"].ToString());
                    i.lp = Convert.ToDouble(item["low_price"].ToString());
                    i.tp = Convert.ToDouble(item["trade_price"].ToString());

                    i.timestamp = Convert.ToInt64(item["timestamp"].ToString());

                    //i.change_rate = Convert.ToDouble(item["change_rate"].ToString());
                    

                    i.candle_acc_trade_price = Convert.ToDouble(item["candle_acc_trade_price"].ToString());
                    i.candle_acc_trade_volume = Convert.ToDouble(item["candle_acc_trade_volume"].ToString());

                    candles.Add(i);

                }


                return candles;
            }
            catch (Exception e)
            {
                Debug.WriteLine($"캔들 읽어오기 오류 마켓코드 : {market}");
                return candles;
            }
        }
    }
}
