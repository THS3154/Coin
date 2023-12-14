using System;
using System.Diagnostics;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Upbit.Converter
{
    public class CandleMarginConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            // 여기서 values 배열에 여러 개의 값이 포함됩니다.
            // 원하는 계산을 수행하고 계산 결과를 반환하세요.
            Thickness result = new Thickness(); // 여기에 원하는 계산을 수행하세요
            try
            {
                double height = (double)values[0];
                double rate = (double)values[1];
                double top = height * rate;
                result = new Thickness { Top = top };
            }
            catch (Exception ex) 
            {
                Debug.WriteLine("CandleMarginConverter");
            }
            return result;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
