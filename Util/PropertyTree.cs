using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.Design;

namespace WpfLibrary.Util
{
    /// <summary>
    /// 属性树,用以用反射来生成wpf
    /// </summary>
    public class PropertyTree
    {
        /// <summary>
        /// 子树
        /// </summary>
        public Dictionary<string, List<PropertyData>> Children { get; set; } = new Dictionary<string, List<PropertyData>>();

        /// <summary>
        /// 构造函数
        /// </summary>
        public PropertyTree()
        {
        }

        /// <summary>
        /// 设置属性
        /// </summary>
        /// <param name="propertyInfo"></param>
        /// <param name="o">属性所属的物体</param>
        public void SetProperty(PropertyInfo propertyInfo, object o)
        {
            var data = new PropertyData(propertyInfo, o);
            if (Children.ContainsKey(data.Category))
            {
                Children[data.Category].Add(data);
            }
            else
            {
                Children.Add(data.Category, new List<PropertyData>() { data });
            }
        }

        /// <summary>
        /// 对字典里的元素排序
        /// </summary>
        public void Sort()
        {
            var temp = new Dictionary<string, List<PropertyData>>();
            var sortedKeys = Children.OrderBy(x => x.Key).ToList();
            foreach (var key in sortedKeys)
            {
                var list = key.Value.OrderBy(x => x.DisplayName).ToList();
                temp.Add(key.Key, list);
            }
            Children = temp;
        }
    }
}
