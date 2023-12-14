﻿using System;
using System.Diagnostics;
using System.Globalization;
using System.Windows.Data;

namespace Upbit.Converter
{
    public class MultipleHeightConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            // 여기서 values 배열에 여러 개의 값이 포함됩니다.
            // 원하는 계산을 수행하고 계산 결과를 반환하세요.
            double result = 1; // 여기에 원하는 계산을 수행하세요
            try
            {
                double val1 = (double)values[0];
                double val2 = (double)values[1];
                result = val1 * val2;
                if (result < 1)
                    result = 1;

            }
            catch (Exception ex)
            {
                Debug.WriteLine("MultipleHeightConverter");
            }
            
            return result;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
