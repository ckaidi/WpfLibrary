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
    public class PropertyPanel : Control, ISupportInitialize, INotifyPropertyChanged
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
            DependencyProperty.Register("CornerRadius", typeof(CornerRadius), typeof(NoWindow), new PropertyMetadata(new CornerRadius()));

        /// <summary>
        /// 属性改变事件
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

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
            _mainStackPanel = (StackPanel)Template.FindName("MainStackPanel", this);
            if (DataContext != null)
            {
                var type = DataContext.GetType();
                var properties = type.GetProperties();
                var propertyTree = new PropertyTree();
                foreach (var property in properties)
                {
                    var browsableAttr = property.GetCustomAttribute<BrowsableAttribute>();
                    if (browsableAttr == null || browsableAttr.Browsable)//如果为空或者可见
                    {
                        propertyTree.SetProperty(property, property.GetValue(DataContext));
                    }
                }
                propertyTree.Sort();

                //生成控件
            }
        }
    }
}
