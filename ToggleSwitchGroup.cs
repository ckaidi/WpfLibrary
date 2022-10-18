using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    /// toggleswitch组
    /// </summary>
    public class ToggleSwitchGroup : GroupControl
    {
        /// <summary>
        /// 是否显示文字
        /// </summary>
        public bool IsHasText
        {
            get { return (bool)GetValue(IsHasTextProperty); }
            set { SetValue(IsHasTextProperty, value); }
        }
        public static readonly DependencyProperty IsHasTextProperty =
            DependencyProperty.Register("IsHasText", typeof(bool), typeof(ToggleSwitchGroup), new PropertyMetadata(true));

        /// <summary>
        /// ComboBox IsChecked
        /// </summary>
        [Bindable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsChecked
        {
            get { return (bool)GetValue(IsCheckedProperty); }
            set { SetValue(IsCheckedProperty, value); }
        }
        public static readonly DependencyProperty IsCheckedProperty =
                DependencyProperty.Register(
                        "IsChecked",
                        typeof(bool),
                        typeof(ToggleSwitchGroup), new PropertyMetadata(false));

        /// <summary>
        /// 静态构造函数
        /// </summary>
        static ToggleSwitchGroup()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ToggleSwitchGroup), new FrameworkPropertyMetadata(typeof(ToggleSwitchGroup)));
        }
    }
}
