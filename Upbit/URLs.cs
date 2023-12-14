using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Upbit
{
    public class URLs
    {
        public string Account { get; private set; }
        public string Chance { get; private set; }
        public string Order { get; private set; }
        public string Cancel { get; private set; }
        public string Market { get; private set; } 
        public string TickMin { get; private set; }
        public string WebSocketTick { get; private set; }
        public URLs()
        {
            Account = "https://api.upbit.com/v1/accounts";

            //주문가능정보
            Chance = "https://api.upbit.com/v1/orders/chance";
            
            //주문
            Order = "https://api.upbit.com/v1/orders";

            //취소
            Cancel = "https://api.upbit.com/v1/order";

            //코인목록 주소
            Market = "https://api.upbit.com/v1/market/all";

            //시세조회 주소
            TickMin = "https://api.upbit.com/v1/candles/minutes/";

            WebSocketTick = "wss://api.upbit.com/websocket/v1";
        }

    }
}
