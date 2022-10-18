using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
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
using static WpfLibrary.Extension.SystemExtension;

namespace WpfLibrary
{
    /// <summary>
    /// ComboBoxGroup
    /// </summary>
    public class ComboBoxGroup : GroupControl
    {
        /// <summary>
        /// ComboBox SelectedItem
        /// </summary>
        [Bindable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int SelectedIndex
        {
            get { return (int)GetValue(SelectedIndexProperty); }
            set { SetValue(SelectedIndexProperty, value); }
        }
        public static readonly DependencyProperty SelectedIndexProperty =
                DependencyProperty.Register(
                        "SelectedIndex",
                        typeof(int),
                        typeof(ComboBoxGroup), new PropertyMetadata(0));

        /// <summary>
        /// ComboBox SelectedItem
        /// </summary>
        [Bindable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public object SelectedItem
        {
            get { return GetValue(SelectedItemProperty); }
            set { SetValue(SelectedItemProperty, value); }
        }
        public static readonly DependencyProperty SelectedItemProperty =
                DependencyProperty.Register(
                        "SelectedItem",
                        typeof(object),
                        typeof(ComboBoxGroup),
                        new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        /// <summary>
        /// 绑定到ComboboxItemsSource
        /// </summary>
        [Bindable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public IEnumerable ItemsSource
        {
            get { return (IEnumerable)GetValue(ItemsSourceProperty); }
            set { SetValue(ItemsSourceProperty, value); }
        }
        public static readonly DependencyProperty ItemsSourceProperty =
            DependencyProperty.Register(
                "ItemsSource",
                typeof(IEnumerable),
                typeof(ComboBoxGroup),
                new PropertyMetadata());

        /// <summary>
        /// 文字改变事件
        /// </summary>
        public static readonly RoutedEvent SelectionChangedEvent =
            EventManager.RegisterRoutedEvent("SelectionChanged", RoutingStrategy.Bubble, typeof(SelectionChangedEventHandler), typeof(ComboBoxGroup));
        public event SelectionChangedEventHandler SelectionChanged
        {
            add
            {
                AddHandler(ComboBoxGroup.SelectionChangedEvent, value);
            }
            remove
            {
                RemoveHandler(ComboBoxGroup.SelectionChangedEvent, value);
            }
        }

        /// <summary>
        /// 主要的combobox
        /// </summary>
        private ComboBox _mainComboBox;

        /// <summary>
        /// 静态构造函数
        /// </summary>
        static ComboBoxGroup()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ComboBoxGroup), new FrameworkPropertyMetadata(typeof(ComboBoxGroup)));
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        public ComboBoxGroup()
        {
        }

        /// <summary>
        /// 加载模板
        /// </summary>
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            _mainComboBox = GetTemplateChild("MainComboBox") as ComboBox;
            if (_mainComboBox != null)
            {
                _mainComboBox.SelectionChanged += _mainComboBox_SelectionChanged;
            }
        }

        /// <summary>
        /// combobox选择集改变时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <exception cref="NotImplementedException"></exception>
        private void _mainComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            e.RoutedEvent = SelectionChangedEvent;
            if (sender is ComboBox comboBox)
            {
                SelectedIndex = comboBox.SelectedIndex;
                SelectedItem = comboBox.SelectedItem;
            }
            RaiseEvent(e);
        }
    }
}
