using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
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
using WpfLibrary.Util;

namespace WpfLibrary
{
    /// <summary>
    /// 根据属性生成panel
    /// </summary>
    public class PropertyPanel : Control
    {
        /// <summary>
        /// 最主要的panel
        /// </summary>
        private StackPanel _mainStackPanel;

        /// <summary>
        /// 圆角按钮
        /// </summary>
        public CornerRadius CornerRadius
        {
            get { return (CornerRadius)GetValue(CornerRadiusProperty); }
            set { SetValue(CornerRadiusProperty, value); }
        }
        public static readonly DependencyProperty CornerRadiusProperty =
            DependencyProperty.Register("CornerRadius", typeof(CornerRadius), typeof(PropertyPanel), new PropertyMetadata(new CornerRadius()));

        /// <summary>
        /// 静态构造函数
        /// </summary>
        static PropertyPanel()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(PropertyPanel), new FrameworkPropertyMetadata(typeof(PropertyPanel)));
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        public PropertyPanel()
        {
            Loaded += PropertyPanel_Loaded;
        }

        public PropertyPanel(object o) : this()
        {
            DataContext = o;
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            _mainStackPanel = GetTemplateChild("MainStackPanel") as StackPanel;
        }

        /// <summary>
        /// 加载函数获得MainStackPanel
        /// 同时根据datacontext生成控件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PropertyPanel_Loaded(object sender, RoutedEventArgs e)
        {
            if (DataContext != null)
            {
                //生成控件
                var tc = TypeDescriptor.GetConverter(DataContext);

                var properties = TypeDescriptor.GetProperties(DataContext);
                foreach (PropertyDescriptor property in properties)
                {

                }
            }
        }
    }
}
