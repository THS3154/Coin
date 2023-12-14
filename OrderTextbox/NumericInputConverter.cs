using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows;
using System.Text.RegularExpressions;

namespace OrderTextbox
{
    class NumericInputConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {

            string input = value as string;
            return input;
            string result = new string(input.Where(c => Char.IsDigit(c) || c == '.').ToArray());
            if (result.Count(c => c == '.') <= 1)
            {
                return result;
            }
            return "";
        }
    }
}
