using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Upbit.Functions
{
    public class FncDateTime
    {
        public static DateTime IntToDateTime(int intDate)
        {
            return new DateTime(1970, 1, 1, 9, 0, 0).AddSeconds(intDate);
        }

        public static DateTime LongToDateTime(long intDate)
        {
            return new DateTime(1970, 1, 1, 9, 0, 0).AddMilliseconds(intDate);
        }

        public static UInt32 DateTimeToInt(DateTime theDate)
        {
            return (UInt32)(theDate - new DateTime(1970, 1, 1, 9, 0, 0)).TotalSeconds;
        }

        public static long DateTimeToIntMS(DateTime theDate)
        {
            return (long)(theDate - new DateTime(1970, 1, 1, 9, 0, 0)).TotalMilliseconds;
        }
    }
}
