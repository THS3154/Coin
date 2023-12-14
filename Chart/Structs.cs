using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chart
{
    public class Structs
    {
        public struct Tick
        {
            public double X { get; set; }
            public double Y { get; set; }
            public string utc { get; set; }
            public string kst { get; set; }
            public double op { get; set; }//시가
            public double hp { get; set; }//고가
            public double lp { get; set; }//저가
            public double tp { get; set; }//종가
            public long timestamp { get; set; }//해당캔들 마지막 틱저장시간
            //public double EHight { get; set; }
            //public double ELow { get; set; }
            //public double RectWidth { get; set; }
            //public double RectHeight { get; set; }
            //public double RectEWidth { get; set; }
            //public double RectEHeight { get; set; }
        }
    }
}
