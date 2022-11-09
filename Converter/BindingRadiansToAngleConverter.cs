using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace WpfLibrary
{
    /// <summary>
    /// 弧度转角度值转化器
    /// </summary>
    public class BindingRadiansToAngleConverter : IValueConverter
    {
        static double _factorTo;
        static double _factorBack;

        static BindingRadiansToAngleConverter()
        {
            _factorTo = 180 / Math.PI;
            _factorBack = Math.PI / 180;
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is double num)
            {
                return num * _factorTo;
            }
            return double.NaN;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is double num)
            {
                return num * _factorBack;
            }
            return double.NaN;
        }
    }
}
