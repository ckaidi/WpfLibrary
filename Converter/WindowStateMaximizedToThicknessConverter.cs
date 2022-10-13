using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace WpfLibrary
{
    public sealed class WindowStateMaximizedToThicknessConverter : DependencyObject, IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var zero = new Thickness();
            var def = parameter as Thickness? ?? new Thickness();

            if (value == null)
                return zero;

            var state = value as WindowState? ?? WindowState.Maximized;

            return state == WindowState.Maximized ? def : zero;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}