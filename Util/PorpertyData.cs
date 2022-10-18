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
    public class PorpertyData
    {
        /// <summary>
        /// 目录特性
        /// </summary>
        public string Category { get; set; }

        /// <summary>
        /// 展示名称特性
        /// </summary>
        public DisplayNameAttribute DisplayName { get; set; }

        /// <summary>
        /// 描述特性
        /// </summary>
        public DescriptionAttribute Description { get; set; }

        /// <summary>
        /// 属性
        /// </summary>
        public PropertyInfo PropertyInfo { get; set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="propertyInfo"></param>
        public PorpertyData(PropertyInfo propertyInfo)
        {
            var categoryAttribute = propertyInfo.GetCustomAttribute<CategoryAttribute>();
            Category = categoryAttribute == null ? "其他" : categoryAttribute.Category;
            DisplayName = propertyInfo.GetCustomAttribute<DisplayNameAttribute>();
            Description = propertyInfo.GetCustomAttribute<DescriptionAttribute>();
        }
    }
}
