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
    /// 没有标题的对话框
    /// </summary>
    public class NoDialog : Window
    {
        static NoDialog()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(NoDialog), new FrameworkPropertyMetadata(typeof(NoDialog)));
        }
    }
}
