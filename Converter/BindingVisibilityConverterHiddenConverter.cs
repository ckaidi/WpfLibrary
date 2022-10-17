using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace WpfLibrary
{
    /// <summary>
    /// 将可视和布尔值绑定起来
    /// true为Visible
    /// false为Hidden
    /// </summary>
    public class BindingVisibilityConverterHiddenConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool v)
            {
                if (v)
                {
                    return Visibility.Visible;
                }
            }
            return Visibility.Hidden;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Visibility visibility)
                switch (visibility)
                {
                    case Visibility.Visible: return true;
                    case Visibility.Hidden: return false;
                    default: return false;
                }
            return false;
        }
    }
}
