using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Chart
{
    public class MultipleYConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            // 여기서 values 배열에 여러 개의 값이 포함됩니다.
            // 원하는 계산을 수행하고 계산 결과를 반환하세요.
            double result = 0; // 여기에 원하는 계산을 수행하세요
            double max = (double)values[0];
            double hp = (double)values[1];
            double rate = (double)values[2];
            result = (max - hp) * rate;
            return Math.Abs(result);
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
