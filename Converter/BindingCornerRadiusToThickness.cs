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
    /// 将CornerRadius绑定到Thickness
    /// </summary>
    public class BindingCornerRadiusToThickness : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is CornerRadius corner)
            {
                return new Thickness(corner.TopLeft, corner.TopLeft, corner.TopRight, corner.TopRight);
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
