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
    /// 类似浏览的标签可以删除可以添加
    /// </summary>
    public class NavigationViewAddableItem : NavigationViewItem
    {
        /// <summary>
        /// 关闭按钮的textblock
        /// </summary>
        private Border _closeTextBlock;

        /// <summary>
        /// 主要的边界
        /// </summary>
        private Border _mainBorder;

        /// <summary>
        /// 是否用来添加标签的Item
        /// </summary>
        public bool IsDefaultAddItem
        {
            get { return (bool)GetValue(IsDefaultAddItemProperty); }
            set { SetValue(IsDefaultAddItemProperty, value); }
        }
        public static readonly DependencyProperty IsDefaultAddItemProperty =
            DependencyProperty.Register("IsDefaultAddItem", typeof(bool), typeof(NavigationViewAddableItem), new PropertyMetadata(false));

        static NavigationViewAddableItem()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(NavigationViewAddableItem), new FrameworkPropertyMetadata(typeof(NavigationViewAddableItem)));
        }

        public override void OnApplyTemplate()
        {
            void _closeTextBlock_MouseLeave(object sender, MouseEventArgs e)
            {
                _closeTextBlock.Background = new SolidColorBrush(Colors.Transparent);
            }
            void _closeTextBlock_MouseEnter(object sender, MouseEventArgs e)
            {
                _closeTextBlock.Background = new SolidColorBrush(Color.FromArgb(0x44, 0x80, 0x80, 0x80));
            }
            void _closeTextBlock_MouseDown(object sender, MouseButtonEventArgs e)
            {
                _closeTextBlock.Background = new SolidColorBrush(Color.FromArgb(0xdd, 0x80, 0x80, 0x80));
            }

            base.OnApplyTemplate();
            _closeTextBlock = GetTemplateChild("CloseTB") as Border;
            _closeTextBlock.MouseEnter += _closeTextBlock_MouseEnter;
            _closeTextBlock.MouseLeave += _closeTextBlock_MouseLeave;
            _closeTextBlock.MouseLeftButtonDown += _closeTextBlock_MouseDown;
            _closeTextBlock.MouseLeftButtonUp += _closeTextBlock_MouseLeftButtonUp;

            _mainBorder = GetTemplateChild("MainBorder") as Border;
            if (IsDefaultAddItem)//如果是新建标签按钮
            {
                _closeTextBlock.Visibility = Visibility.Collapsed;
            }

        }

        private void _closeTextBlock_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;

            var parent = _parentNavigation;

            if (parent != null)
            {
                parent.NotifyNavigationViewDeleted(this);
            }
        }
    }
}
