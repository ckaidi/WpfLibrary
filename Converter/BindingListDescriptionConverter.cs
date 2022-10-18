using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace WpfLibrary
{
    /// <summary>
    /// 将属性展示在wpf的数据转换成其Description特性指定的形式
    /// </summary>
    public class BindingListDescriptionConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var result = new List<object>();
            if (value is Array array)
            {
                foreach (var item in array)
                {
                    var t = item.GetType();
                    var f = t.GetField(item.ToString());
                    var a = f.GetCustomAttributes(typeof(DescriptionAttribute), true)[0];
                    if (a is DescriptionAttribute description)
                        result.Add(description.Description);
                }
            }
            return result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
