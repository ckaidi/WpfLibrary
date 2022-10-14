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
    /// 导航栏集合
    /// </summary>
    [StyleTypedProperty(Property = "ItemContainerStyle", StyleTargetType = typeof(NavigationViewItem))]
    public class NavigationViewItems : ItemsControl
    {
        static NavigationViewItems()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(NavigationViewItems), new FrameworkPropertyMetadata(typeof(NavigationViewItems)));
        }
    }
}
