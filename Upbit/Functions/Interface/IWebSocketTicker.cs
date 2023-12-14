using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Upbit.Functions.Interface
{
    public interface IWebSocketTicker
    {
        void TradeEvent(Structs.Trade tr);
        void OrderEvent(Structs.Orderbook ob);
        void TickerEvent(Structs.Ticker tick);
    }
}
