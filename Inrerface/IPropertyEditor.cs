using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows;
using System.Reflection;

namespace WpfLibrary.Inrerface
{
    /// <summary>
    /// 根据属性生成控件
    /// </summary>
    public interface IPropertyEditor
    {
        FrameworkElement GetEditor(PropertyInfo propertyItem, object o);
    }
}
