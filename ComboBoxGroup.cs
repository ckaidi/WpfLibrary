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
        /// combobox选中的项改变事件
        /// </summary>
        public static readonly RoutedEvent SelectedObjectChangedEvent =
            EventManager.RegisterRoutedEvent("SelectedObjectChanged", RoutingStrategy.Bubble,
                typeof(SelectionChangedEventHandler), typeof(ComboBoxGroup));
        public event RoutedPropertyChangedEventHandler<object> SelectedObjectChanged
        {
            add
            {
                AddHandler(SelectedObjectChangedEvent, value);
            }
            remove
            {
                RemoveHandler(SelectedObjectChangedEvent, value);
            }
        }

        /// <summary>
        /// combobox选中的项
        /// </summary>
        [Bindable(true)]
        public object SelectedObject
        {
            get
            {
                return (object)GetValue(SelectedObjectProperty);
            }
            set
            {
                SetValue(SelectedObjectProperty, value);
            }
        }
        public static readonly DependencyProperty SelectedObjectProperty =
            DependencyProperty.Register("SelectedObject", typeof(object), typeof(ComboBoxGroup),
                new UIPropertyMetadata(null, new PropertyChangedCallback(OnSelectedItemChanged)));

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
        /// <summary>
        ///     SelectedItem DependencyProperty
        /// </summary>
        public static readonly DependencyProperty SelectedItemProperty =
                DependencyProperty.Register(
                        "SelectedItem",
                        typeof(object),
                        typeof(ComboBoxGroup),
                        new PropertyMetadata());

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
        /// 主要的combobox
        /// </summary>
        public ComboBox MainComboBox { get; private set; }

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

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            MainComboBox = GetTemplateChild("MainComboBox") as ComboBox;
            if (MainComboBox != null)
            {
                MainComboBox.SelectionChanged += MainComboBox_SelectionChanged;
            }
        }

        private void MainComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
        }

        /// <summary>
        /// Selector的OnSelectedItemChanged
        /// </summary>
        /// <param name="d"></param>
        /// <param name="e"></param>
        public static void OnSelectedItemChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            Selector s = null;
            //if (d is ComboBoxGroup comboBoxGroup)
            //{
            //    s = comboBoxGroup.MainComboBox;
            //}
            //else
            //    s = (Selector)d;
            //var selectionChange = SelectionChange.GetValue(s);
            //var isActive = IsActive.GetValue(selectionChange) as bool?;
            //if (isActive != null && isActive.Value)
            //{
            //    var temp = NewItemInfo.Invoke(s, new object[] { e.NewValue });
            //    SelectJustThisItem.Invoke(selectionChange, new object[] { temp, false });
            //}
        }
    }
}
