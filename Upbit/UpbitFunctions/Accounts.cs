using FileIO;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows;
using Upbit.Functions;
using System.Resources;
using static Upbit.Structs;
using PublicColor;

namespace Upbit.UpbitFunctions
{
    public class Accounts
    {
        public async Task<List<Structs.Accounts>> AccountsAsync()
        {
            URLs url = new URLs();
            List<Structs.Accounts> ListResult = new List<Structs.Accounts>();
            if (!Access.UpbitInstance.GetAccess())
            {
                return ListResult;
            }

            try
            {
                var client = new HttpClient();
                var request = new HttpRequestMessage
                {
                    Method = HttpMethod.Get,
                    RequestUri = new Uri(url.Account),
                    Headers =
                    {
                        { "accept", "application/json" },
                        { "Authorization", JWT.GetJWT() },
                    },
                };


                using (var response = await client.SendAsync(request))
                {
                    response.EnsureSuccessStatusCode();
                    var body = await response.Content.ReadAsStringAsync();

                    JArray jsonArray = JArray.Parse(body);


                    foreach (var item in jsonArray.Children())
                    {
                        Structs.Accounts lr = new Structs.Accounts();

                        lr.UnitCurrency = item["unit_currency"].ToString();
                        lr.Currency = item["currency"].ToString();
                        lr.Balance = item["balance"].ToString();
                        lr.Locked = item["locked"].ToString();
                        lr.AvgBuyPrice = item["avg_buy_price"].ToString();
                        lr.AvgBuyPriceModified = (bool)item["avg_buy_price_modified"];
                        lr.Color = Colors.Colorinstance.ColorText;
                        ListResult.Add(lr);
                    }

                    return ListResult;
                }
            }
            catch (Exception e)
            {
                Language.Lang.Upbit Lang = new Language.Lang.Upbit();
                string KeyError = Lang.LKeyError;
                string KeyComment = Lang.LKeyComment;
                MessageBox.Show(KeyError + "\n" + KeyComment);
                //MessageBox.Show(e.ToString());
                Logs.Loginstance.UpbitLog(KeyError);
                Access.UpbitInstance.ExceptionKey();
                return ListResult;
            }


        }

        

        public double GetAccountCoinPrice(string Market)
        {
            double value = -1;
            string[] str = Market.Split("-");
            List<Structs.Accounts> accounts = Task.Run(() => AccountsAsync()).Result;
            foreach(Structs.Accounts v in accounts)
            {
                if(v.UnitCurrency == str[0] && v.Currency == str[1])
                {
                    value = Convert.ToDouble(v.AvgBuyPrice);
                    break;
                }
            }

            return value;
        }
    }
}
