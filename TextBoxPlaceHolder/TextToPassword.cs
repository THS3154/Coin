using System;
using System.Diagnostics;
using System.Globalization;
using System.Windows.Data;

namespace TextBoxPlaceHolder
{
    internal class TextToPassword : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string result = "";
            try
            {
                if(value is not null)
                {
                    string str = (string)value;
                    for (int i = 0; i < str.Length; i++)
                    {
                        result += "*";
                    }
                }
                
            }catch(Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            return result; 
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
            throw new InvalidOperationException("IsNullConverter can only be used OneWay.");
        }
    }
}
