using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfLibrary
{
    /// <summary>
    /// NavigationViewItems设置为用户可以添加时的参数
    /// </summary>
    public class NavigationAddTabSetting
    {
        /// <summary>
        /// 新建标签栏的提示
        /// </summary>
        public string AddTip { get; set; } = "新建项";

        /// <summary>
        /// 新建项的默认名称
        /// </summary>
        public string DefaultNewText { get; set; } = "Untitled";
    }
}
