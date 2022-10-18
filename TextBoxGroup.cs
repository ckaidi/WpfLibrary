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
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TextBox = System.Windows.Controls.TextBox;

namespace WpfLibrary
{
    /// <summary>
    /// textboxgroup控件
    /// </summary>
    public class TextBoxGroup : GroupControl
    {
        /// <summary>
        /// 内部TextBox
        /// </summary>
        private TextBox _innerTextBox;

        /// <summary>
        /// 用于防止嵌套之间重新进入的标志
        /// OnTextPropertyChanged/OnTextContainerChanged callbacks.
        /// </summary>
        private bool _isInsideTextContentChange;
        private object _newTextValue = DependencyProperty.UnsetValue;

        /// <summary>
        /// 文字
        /// </summary>
        [Bindable(true)]
        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }
        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register("Text", typeof(string), typeof(TextBoxGroup),
                        new FrameworkPropertyMetadata(string.Empty,
                            FrameworkPropertyMetadataOptions.BindsTwoWayByDefault | // Flags
                                    FrameworkPropertyMetadataOptions.Journal));

        /// <summary>
        /// 文字改变事件
        /// </summary>
        public static readonly RoutedEvent TextChangedEvent =
            EventManager.RegisterRoutedEvent("TextChanged", RoutingStrategy.Bubble, typeof(TextChangedEventHandler), typeof(TextBoxGroup));
        public event TextChangedEventHandler TextChanged
        {
            add
            {
                AddHandler(TextBoxGroup.TextChangedEvent, value);
            }
            remove
            {
                RemoveHandler(TextBoxGroup.TextChangedEvent, value);
            }
        }

        /// <summary>
        /// 静态构造函数
        /// </summary>
        static TextBoxGroup()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(TextBoxGroup), new FrameworkPropertyMetadata(typeof(TextBoxGroup)));
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            _innerTextBox = GetTemplateChild("MainTextBox") as TextBox;
            if (_innerTextBox != null)
            {
                _innerTextBox.TextChanged += _innerTextBox_TextChanged;
            }
        }

        /// <summary>
        /// 内部事件触发外部事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _innerTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            e.RoutedEvent = TextChangedEvent;
            Text = _innerTextBox.Text;
            RaiseEvent(e);
        }
    }
}
