using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
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
    /// 导航栏菜单按钮
    /// </summary>
    public class NavigationViewItem : ButtonBase
    {
        /// <summary>
        /// Segoe MDL2 Assets的icon
        /// </summary>
        public string Icon
        {
            get { return (string)GetValue(IconProperty); }
            set { SetValue(IconProperty, value); }
        }
        public static readonly DependencyProperty IconProperty =
            DependencyProperty.Register("Icon", typeof(string), typeof(NavigationViewItem), new PropertyMetadata("\xE700"));

        /// <summary>
        /// 图标的大小
        /// </summary>
        public double IconSize
        {
            get { return (double)GetValue(IconSizeProperty); }
            set { SetValue(IconSizeProperty, value); }
        }
        public static readonly DependencyProperty IconSizeProperty =
            DependencyProperty.Register("IconSize", typeof(double), typeof(NavigationViewItem), new PropertyMetadata(16.0));//加一个.0不然会引发类型不匹配的错误

        /// <summary>
        /// Segoe MDL2 Assets的icon
        /// </summary>
        public string MenuText
        {
            get { return (string)GetValue(MenuTextProperty); }
            set { SetValue(MenuTextProperty, value); }
        }
        public static readonly DependencyProperty MenuTextProperty =
            DependencyProperty.Register("MenuText", typeof(string), typeof(NavigationViewItem), new PropertyMetadata("More"));

        /// <summary>
        /// 菜单文字大小
        /// </summary>
        public double MenuTextSize
        {
            get { return (double)GetValue(MenuTextSizeProperty); }
            set { SetValue(MenuTextSizeProperty, value); }
        }
        public static readonly DependencyProperty MenuTextSizeProperty =
            DependencyProperty.Register("MenuTextSize", typeof(double), typeof(NavigationViewItem), new PropertyMetadata(14.0));//加一个.0不然会引发类型不匹配的错误

        /// <summary>
        /// 图标textblock
        /// </summary>
        public TextBlock IconTextBlock { get; private set; }

        /// <summary>
        /// 菜单按钮textBlock
        /// </summary>
        public TextBlock CommandTextBlock { get; private set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        static NavigationViewItem()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(NavigationViewItem), new FrameworkPropertyMetadata(typeof(NavigationViewItem)));
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        public NavigationViewItem()
        {
            this.Loaded += NavigationViewItem_Loaded;
            this.MouseDown += NavigationViewItem_MouseDown;
        }

        /// <summary>
        /// 加载完成之后从内部模板获取控件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NavigationViewItem_Loaded(object sender, RoutedEventArgs e)
        {
            IconTextBlock = (TextBlock)Template.FindName("IconTB", this);
            CommandTextBlock = (TextBlock)Template.FindName("CommandTB", this);
        }

        private void NavigationViewItem_MouseDown(object sender, MouseButtonEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
