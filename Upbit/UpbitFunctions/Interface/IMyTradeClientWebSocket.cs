using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Upbit.UpbitFunctions.Interface
{
    public interface IMyTrade
    {
        void MyTradeEvent(Structs.MyTrade mytrade);
    }
}
