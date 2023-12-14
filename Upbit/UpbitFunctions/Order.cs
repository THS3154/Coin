using FileIO;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Upbit.Functions;

namespace Upbit.UpbitFunctions
{
    public class Order
    {
        private void Errors(string str)
        {
            JObject jmain = JObject.Parse(str);
            if (str != "")
            {
                JToken error = jmain.Value<JToken>("error");
                string errorname = error.Value<string>("name");
                Language.Lang.Upbit Lang = new Language.Lang.Upbit();
                string KeyError = Lang.LKeyError;
                string KeyComment = Lang.LKeyComment;
                string PrintMessage = "";
                if (errorname == "create_ask_error") PrintMessage = "주문 요청 정보가 올바르지 않습니다.";
                else if (errorname == "create_bid_error") PrintMessage = "주문 요청 정보가 올바르지 않습니다.";
                else if (errorname == "insufficient_funds_ask") PrintMessage = "매도 가능 잔고가 부족합니다.";
                else if (errorname == "insufficient_funds_bid") PrintMessage = "매수 가능 잔고가 부족합니다.";
                else if (errorname == "under_min_total_ask") PrintMessage = "주문 요청 금액이 최소주문금액 미만입니다.";
                else if (errorname == "under_min_total_bid") PrintMessage = "주문 요청 금액이 최소주문금액 미만입니다.";
                else if (errorname == "withdraw_address_not_registerd") PrintMessage = "허용되지 않은 출금 주소입니다.";
                else if (errorname == "validation_error") PrintMessage = "잘못된 API 요청입니다.";
                Logs.Loginstance.UpbitLog(Lang.LOrderChance);
            }
        }
        /// <summary>
        /// 주문 가능정보
        /// </summary>
        /// <param name="market"></param>
        /// <returns></returns>
        public async Task<Structs.Chance> OrderChanceAsync(string market)
        {
            URLs url = new URLs();
            Structs.Chance chance = new Structs.Chance();
            if (!Access.UpbitInstance.GetAccess())
            {
                return chance;
            }

            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(url.Chance + "?market=" + market),
                Headers =
                    {
                        { "accept", "application/json" },
                        { "Authorization", JWT.GetJWT("market:" + market) },
                    },
            };
            string keepbody = "";
            try
            {
                
                using (var response = await client.SendAsync(request))
                {
                    var body = await response.Content.ReadAsStringAsync();
                    keepbody = body;
                    response.EnsureSuccessStatusCode();
                    
                    JObject jsonArray = JObject.Parse(body);


                    string types = (string)jsonArray.GetValue("ty");

                    chance.bid_fee = (double)jsonArray.GetValue("bid_fee");
                    chance.ask_fee = (double)jsonArray.GetValue("ask_fee");
                    chance.maker_bid_fee = (double)jsonArray.GetValue("maker_bid_fee");
                    chance.maker_ask_fee = (double)jsonArray.GetValue("maker_ask_fee");

                    JToken jtokenmarket = jsonArray.GetValue("market");

                    chance.market_id = jtokenmarket.Value<string>("id");
                    chance.market_name = jtokenmarket.Value<string>("name");
                    chance.max_total = jtokenmarket.Value<double>("max_total");
                    chance.state = jtokenmarket.Value<string>("state");


                    JArray order_sides = jtokenmarket.Value<JArray>("order_sides");
                    string str_order_sides = "";
                    foreach (var item in order_sides.Children())
                    {
                        str_order_sides += item.ToString() + ",";

                    }
                    chance.market_order_sides = str_order_sides;


                    JArray bid_types = jtokenmarket.Value<JArray>("bid_types");
                    string str_bid_types = "";
                    foreach (var item in bid_types.Children())
                    {
                        str_bid_types += item.ToString() + ",";

                    }
                    chance.market_bid_types = str_bid_types;

                    JArray ask_types = jtokenmarket.Value<JArray>("ask_types");
                    string str_ask_types = "";
                    foreach (var item in ask_types.Children())
                    {
                        str_ask_types += item.ToString() + ",";

                    }
                    chance.market_ask_types = str_ask_types;


                    JToken bid = jtokenmarket.Value<JToken>("bid");
                    chance.market_bid_currency = bid.Value<string>("currency");
                    chance.market_bid_min_total = bid.Value<double>("min_total");
                    chance.market_bid_price_unit = bid.Value<double>("price_unit");

                    JToken ask = jtokenmarket.Value<JToken>("ask");
                    chance.market_ask_currency = ask.Value<string>("currency");
                    chance.market_ask_min_total = ask.Value<double>("min_total");
                    chance.market_ask_price_unit = bid.Value<double>("price_unit");

                    JToken bid_account = jsonArray.Value<JToken>("bid_account");
                    chance.bid_account_currency = bid_account.Value<string>("currency");
                    chance.bid_account_balance = bid_account.Value<double>("balance");
                    chance.bid_account_locked = bid_account.Value<double>("locked");
                    chance.bid_account_avg_buy_price = bid_account.Value<double>("avg_buy_price");
                    chance.bid_account_avg_buy_price_modified = bid_account.Value<bool>("avg_buy_price_modified");
                    chance.bid_account_unit_currency = bid_account.Value<string>("unit_currency");

                    JToken ask_account = jsonArray.Value<JToken>("ask_account");
                    chance.ask_account_currency = ask_account.Value<string>("currency");
                    chance.ask_account_balance = ask_account.Value<double>("balance");
                    chance.ask_account_locked = ask_account.Value<double>("locked");
                    chance.ask_account_avg_buy_price = ask_account.Value<double>("avg_buy_price");
                    chance.ask_account_avg_buy_price_modified = ask_account.Value<bool>("avg_buy_price_modified");
                    chance.ask_account_unit_currency = ask_account.Value<string>("unit_currency");

                    return chance;
                }
            }
            catch (Exception e)
            {

                
                
                Debug.WriteLine("주문가능정보 오류");
                Errors(keepbody);
                return chance;
            }
        }


        /// <summary>
        /// 코인주문
        /// </summary>
        /// <param name="market">코인 마켓아이디</param>
        /// <param name="side">주문종류 매수/매도</param>
        /// <param name="volume">주문수량</param>
        /// <param name="price">주문당 가격</param>
        /// <param name="ord_type">주문타입 지정가 시장가</param>
        /// <returns></returns>
        public async Task<bool> Orders(string market, string side, double volume, double price, string ord_type, double totalprice)
        {
            URLs url = new URLs();
            if (!Access.UpbitInstance.GetAccess())
            {
                return false;
            }

            string SetStrings = ("market:" + market + ",") +
                                ("side:" + side + ",") +
                                (ord_type != "price" ? ("volume:" + volume + ",") : "") +
                                (ord_type != "market" ? ("pirce:" + price + ",") : "") +
                                ("ord_type:" + ord_type);

            string SetURLStrings = ("?market=" + market + "&") +
                                ("side=" + side + "&") +
                                (ord_type != "price" ? ("volume=" + volume + "&") : "") +
                                (ord_type != "market" ? ("pirce=" + price + "&") : "") +
                                ("ord_type=" + ord_type);

            StringContent jsonContent = new(JsonSerializer.Serialize(new
            {
                market = market,
                side = side,
                volume = (ord_type != "price" ? volume.ToString() : ""),
                price = (ord_type != "market" ? (ord_type == "price" ? totalprice.ToString() : price.ToString()) : ""),
                ord_type = ord_type
            }),
            Encoding.UTF8,
            "application/json");

            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Post, (url.Order + SetURLStrings));
            request.Headers.Add("Accept", "application/json");
            request.Headers.Add("Authorization", JWT.GetJWT(SetStrings));
            request.Content = jsonContent;



            string keepbody = "";
            try
            {
                using (var response = await client.SendAsync(request))
                {
                    var body = await response.Content.ReadAsStringAsync();
                    keepbody = body;
                    response.EnsureSuccessStatusCode();
                    JObject jsonArray = JObject.Parse(body);

                    return true;
                }
            }
            catch (Exception e)
            {
                Errors(keepbody);
                return false;
            }
        }


        /// <summary>
        /// 주문리스트조회
        /// </summary>
        /// <param name="market"></param>
        /// <returns></returns>
        public async Task<List<Structs.OrderList>> OrderListAsync(string market, string state)
        {
            List<Structs.OrderList> orderlist = new List<Structs.OrderList>();
            string strstates = "";
            URLs url = new URLs();
            if (!Access.UpbitInstance.GetAccess())
            {
                return orderlist;
            }

            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(url.Order + "?page=1&limit=100&order_by=desc&state=" + state + "&market=" + market),
                Headers =
                    {
                        { "accept", "application/json" },
                        { "Authorization", JWT.GetJWT("page:1,limit:100,order_by:desc,state:" + state+ ",market:" + market) },
                    },
            };
            string body = "";
            try
            {
                using (var response = await client.SendAsync(request))
                {
                    body = await response.Content.ReadAsStringAsync();
                    response.EnsureSuccessStatusCode();

                    JArray jsonArray = JArray.Parse(body);

                    foreach (var item in jsonArray.Children())
                    {
                        Structs.OrderList list = new Structs.OrderList();
                        list.uuid = item.Value<string>("uuid");
                        list.side = item.Value<string>("side");
                        list.ord_type = item.Value<string>("ord_type");
                        list.price = item.Value<string>("price");
                        list.state = item.Value<string>("state");
                        list.market = item.Value<string>("market");
                        list.created_at = item.Value<string>("created_at");
                        list.volume = item.Value<string>("volume");
                        list.remaining_volume = item.Value<string>("remaining_volume");
                        list.reserved_fee = item.Value<string>("reserved_fee");
                        list.paid_fee = item.Value<string>("paid_fee");
                        list.locked = item.Value<string>("locked");
                        list.executed_volume = item.Value<string>("executed_volume");
                        list.trades_count = item.Value<string>("trades_count");

                        orderlist.Add(list);
                    }


                    return orderlist;
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine($"주문리스트조회 오류 {body}");
                return orderlist;
            }
        }

        /// <summary>
        /// 주문취소
        /// </summary>
        /// <param name="uuid"></param>
        /// <returns></returns>
        public async Task<Structs.OrderCancel> OrderCancelAsync(string uuid)
        {
            Structs.OrderCancel ordercancel = new Structs.OrderCancel();
            URLs url = new URLs();
            if (!Access.UpbitInstance.GetAccess())
            {
                return ordercancel;
            }

            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Delete,
                RequestUri = new Uri(url.Cancel + "?uuid=" + uuid),
                Headers =
                    {
                        { "accept", "application/json" },
                        { "Authorization", JWT.GetJWT("uuid:" + uuid) },
                    },
            };
            try
            {
                using (var response = await client.SendAsync(request))
                {
                    response.EnsureSuccessStatusCode();
                    var body = await response.Content.ReadAsStringAsync();

                    JArray jsonArray = JArray.Parse(body);



                    return ordercancel;
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine("주문캔슬 오류");
                return ordercancel;
            }
        }
    }
}
