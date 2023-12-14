using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows;
using System.Runtime.CompilerServices;

namespace Upbit.Converter
{
    public class BidAskLangConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string type = (string)value;
            string val;
            Language.Lang.PublicCoin lang = new Language.Lang.PublicCoin();
            if(type == "ask")
            {
                val = lang.LCoinSell;
            }
            else
            {
                val = lang.LCoinBuy;
            }
            return val;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new InvalidOperationException("IsNullConverter can only be used OneWay.");
        }
    }
}
