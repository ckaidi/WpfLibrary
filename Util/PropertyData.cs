using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace WpfLibrary.Util
{
    /// <summary>
    /// 属性数据
    /// </summary>
    public class PropertyData
    {
        /// <summary>
        /// 目录特性
        /// </summary>
        public string Category { get; set; }

        /// <summary>
        /// 展示名称特性
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// 描述特性
        /// </summary>
        public DescriptionAttribute Description { get; set; }

        /// <summary>
        /// 属性
        /// </summary>
        public PropertyInfo PropertyInfo { get; set; }

        /// <summary>
        /// 属性的值
        /// </summary>
        public object Value { get; set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="propertyInfo"></param>
        public PropertyData(PropertyInfo propertyInfo, object o)
        {
            Value = o;
            var categoryAttribute = propertyInfo.GetCustomAttribute<CategoryAttribute>();
            Category = categoryAttribute == null ? "其他" : categoryAttribute.Category;
            var displayNameAttribute = propertyInfo.GetCustomAttribute<DisplayNameAttribute>();
            DisplayName = displayNameAttribute == null ? propertyInfo.Name : displayNameAttribute.DisplayName;
            Description = propertyInfo.GetCustomAttribute<DescriptionAttribute>();
        }
    }
}
