using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace WpfLibrary
{

    /// <summary>
    /// 将属性展示在wpf的数据转换成其Description特性指定的形式
    /// </summary>
    public class BindingDescriptionConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return null;
            var f = value.GetType().GetField(value.ToString());
            var attrs = f.GetCustomAttributes(typeof(DescriptionAttribute), true);
            if (attrs.Length == 0) return null;
            var a = attrs[0];
            if (a is DescriptionAttribute description)
                return description.Description;
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            FieldInfo[] fs = null;
            var t = targetType;
            if (targetType.IsGenericType)
            {
                var gtypes = targetType.GetGenericArguments();
                if (gtypes != null && gtypes.Length == 1)
                {
                    t = gtypes[0];
                    fs = t.GetFields();
                }
            }
            else
                fs = targetType.GetFields();
            foreach (var f in fs)
            {
                var attrs = f.GetCustomAttributes(typeof(DescriptionAttribute), true);
                if (attrs.Length == 0) continue;
                if (attrs[0] is DescriptionAttribute description)
                {
                    if (description.Description == value.ToString())
                    {
                        return Enum.Parse(t, f.Name, true);
                    }
                }
            }
            return value;
        }
    }
}
