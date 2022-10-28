using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
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
            DataContextChanged += PropertyPanel_DataContextChanged;
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
        /// 刷新控件
        /// </summary>
        private void Update()
        {
            if (DataContext != null)
            {
                //根据目录把属性分类
                var categoryDics = new Dictionary<string, List<PropertyDescriptor>>();
                var properties = TypeDescriptor.GetProperties(DataContext);
                foreach (PropertyDescriptor property in properties)
                {
                    var borswableAttr = property.Attributes.OfType<BrowsableAttribute>().FirstOrDefault();
                    if (borswableAttr != null && !borswableAttr.Browsable) continue;

                    var categoryAttr = property.Attributes.OfType<CategoryAttribute>().FirstOrDefault();
                    if (categoryAttr == null)
                    {
                        if (categoryDics.ContainsKey("杂项"))
                            categoryDics["杂项"].Add(property);
                        else
                            categoryDics.Add("杂项", new List<PropertyDescriptor>() { property });
                    }
                    else
                    {
                        if (categoryDics.ContainsKey(categoryAttr.Category))
                        {
                            categoryDics[categoryAttr.Category].Add(property);
                        }
                        else
                        {
                            categoryDics.Add(categoryAttr.Category, new List<PropertyDescriptor>() { property });
                        }
                    }
                }
                Debug.Assert(categoryDics.Count > 0);

                //生成控件
                if (categoryDics.Count == 1)
                {
                    foreach (var kvp in categoryDics)
                    {
                        var grouPanel = new GroupPanel();
                        var list = kvp.Value;
                        foreach (var item in list)
                        {
                        }
                    }
                }
            }
        }

        /// <summary>
        /// DataContext改变的时候刷新控件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <exception cref="NotImplementedException"></exception>
        private void PropertyPanel_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            Update();
        }


        /// <summary>
        /// 加载函数获得MainStackPanel
        /// 同时根据datacontext生成控件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PropertyPanel_Loaded(object sender, RoutedEventArgs e)
        {
            Update();
        }
    }
}
