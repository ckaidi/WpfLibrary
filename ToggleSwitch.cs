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
    /// winui3的toggleswitch
    /// </summary>
    public class ToggleSwitch : CheckBox
    {
        public static readonly DependencyProperty TextProperty = DependencyProperty.Register(
            "Text", typeof(string), typeof(ToggleSwitch), new PropertyMetadata("Off"));

        /// <summary>
        /// 默认文本（未选中）
        /// </summary>
        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }
        public static readonly DependencyProperty CheckedTextProperty = DependencyProperty.Register(
            "CheckedText", typeof(string), typeof(ToggleSwitch), new PropertyMetadata("On"));

        /// <summary>
        /// 选中状态文本
        /// </summary>
        public string CheckedText
        {
            get { return (string)GetValue(CheckedTextProperty); }
            set { SetValue(CheckedTextProperty, value); }
        }
        public static readonly DependencyProperty CheckedForegroundProperty =
            DependencyProperty.Register("CheckedForeground", typeof(Brush), typeof(ToggleSwitch), new PropertyMetadata(Brushes.WhiteSmoke));

        /// <summary>
        /// 选中状态前景样式
        /// </summary>
        public Brush CheckedForeground
        {
            get { return (Brush)GetValue(CheckedForegroundProperty); }
            set { SetValue(CheckedForegroundProperty, value); }
        }
        public static readonly DependencyProperty CheckedBackgroundProperty =
            DependencyProperty.Register("CheckedBackground", typeof(Brush), typeof(ToggleSwitch), new PropertyMetadata(Brushes.LimeGreen));

        /// <summary>
        /// 选中状态背景色
        /// </summary>
        public Brush CheckedBackground
        {
            get { return (Brush)GetValue(CheckedBackgroundProperty); }
            set { SetValue(CheckedBackgroundProperty, value); }
        }

        /// <summary>
        /// 是否显示文字
        /// </summary>
        public bool IsHasText
        {
            get { return (bool)GetValue(IsHasTextProperty); }
            set { SetValue(IsHasTextProperty, value); }
        }
        public static readonly DependencyProperty IsHasTextProperty =
            DependencyProperty.Register("IsHasText", typeof(bool), typeof(ToggleSwitch), new PropertyMetadata(true));

        /// <summary>
        /// 静态构造函数
        /// </summary>
        static ToggleSwitch()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ToggleSwitch), new FrameworkPropertyMetadata(typeof(ToggleSwitch)));
        }
    }
}
