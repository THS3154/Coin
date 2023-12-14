using System;
using System.Diagnostics;
using System.Globalization;
using System.Windows.Data;

namespace Upbit.Converter
{
    class SubConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            // 여기서 values 배열에 여러 개의 값이 포함됩니다.
            // 원하는 계산을 수행하고 계산 결과를 반환하세요.
            double result = 0; // 여기에 원하는 계산을 수행하세요
            bool b = true;
            try
            {
                foreach (var item in values) 
                {
                    if (b)
                    {
                        result = System.Convert.ToDouble(item);
                        b = false;
                    }
                    else
                    {
                        result -= System.Convert.ToDouble(item);
                    }
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine("SubConverter");
            }

            return result.ToString().Split(".")[0];
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
