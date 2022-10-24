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
    /// 
    /// </summary>
    public class CheckBoxGroup : GroupControl
    {
        /// <summary>
        /// ComboBox SelectedItem
        /// </summary>
        [Bindable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool? IsChecked
        {
            get { return (bool?)GetValue(IsCheckedProperty); }
            set { SetValue(IsCheckedProperty, value); }
        }
        public static readonly DependencyProperty IsCheckedProperty =
            DependencyProperty.Register("IsChecked", typeof(bool?), typeof(CheckBoxGroup),
                new FrameworkPropertyMetadata(
                    null,
                    FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
                    new PropertyChangedCallback(OnCheckedChanged)));

        /// <summary>
        /// checkbox
        /// </summary>
        private CheckBox _checkBox;

        static CheckBoxGroup()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(CheckBoxGroup), new FrameworkPropertyMetadata(typeof(CheckBoxGroup)));
        }

        /// <summary>
        /// Selector的OnSelectedItemChanged
        /// </summary>
        /// <param name="d"></param>
        /// <param name="e"></param>
        public static void OnCheckedChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
             var comboBoxGroup = d as CheckBoxGroup;
            if (comboBoxGroup != null && comboBoxGroup.IsInitialized)
            {
            }
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            _checkBox = (CheckBox)GetTemplateChild("MainCheckBox");
            _checkBox.Checked += _checkBox_Checked;
            _checkBox.Unchecked += _checkBox_Unchecked;
        }

        private void _checkBox_Unchecked(object sender, RoutedEventArgs e)
        {
            if (sender is CheckBox check)
                IsChecked = check.IsChecked;
        }

        private void _checkBox_Checked(object sender, RoutedEventArgs e)
        {
            if (sender is CheckBox check)
                IsChecked = check.IsChecked;
        }
    }
}
