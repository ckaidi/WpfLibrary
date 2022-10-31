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
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfLibrary
{
    /// <summary>
    /// 代替wpf的groupbox
    /// </summary>
    public class GroupPanel : ItemsControl
    {
        /// <summary>
        /// 标签文字
        /// </summary>
        public string Label
        {
            get { return (string)GetValue(LabelProperty); }
            set { SetValue(LabelProperty, value); }
        }
        public static readonly DependencyProperty LabelProperty =
            DependencyProperty.Register("Label", typeof(string), typeof(GroupPanel), new PropertyMetadata(null));

        /// <summary>
        /// 列表显示
        /// </summary>
        private ItemsPresenter _itemsPresenter;

        /// <summary>
        /// 列表显示
        /// </summary>
        private TextBlock _labelTextBlock;

        /// <summary>
        /// 列表显示
        /// </summary>
        private Border _mainBorder;

        /// <summary>
        /// 静态构造
        /// </summary>
        static GroupPanel()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(GroupPanel), new FrameworkPropertyMetadata(typeof(GroupPanel)));
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            _itemsPresenter = GetTemplateChild("MainItemsPresenter") as ItemsPresenter;
            _labelTextBlock = GetTemplateChild("LabelTextBlock") as TextBlock;
            _mainBorder = GetTemplateChild("MainBorder") as Border;
            if (Label == null)
            {
                _labelTextBlock.Visibility = Visibility.Collapsed;
                _mainBorder.Margin = new Thickness(0);
            }
        }
    }
}
