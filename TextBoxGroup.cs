using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfLibrary
{
    /// <summary>
    /// textboxgroup控件
    /// </summary>
    public class TextBoxGroup : GroupControl
    {
        /// <summary>
        /// 静态构造函数
        /// </summary>
        static TextBoxGroup()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(TextBoxGroup), new FrameworkPropertyMetadata(typeof(TextBoxGroup)));
        }
    }
}
