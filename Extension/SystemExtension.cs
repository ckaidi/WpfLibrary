using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using WpfLibrary.Util;

namespace WpfLibrary.Extension
{
    /// <summary>
    /// 系统扩展方法
    /// </summary>
    public static class SystemExtension
    {
        static SystemExtension()
        {
            var itemsControlType = typeof(ItemsControl);
            NewItemInfo = itemsControlType.GetMethod("NewItemInfo", BindingFlags.Instance | BindingFlags.NonPublic);
            var selectorType = typeof(Selector);
            SelectionChange = selectorType.GetProperty("SelectionChange", BindingFlags.Instance | BindingFlags.NonPublic);
            IsActive = SelectionChange.PropertyType.GetProperty("IsActive", BindingFlags.Instance | BindingFlags.NonPublic);
            SelectJustThisItem = SelectionChange.PropertyType.GetMethod("SelectJustThisItem", BindingFlags.Instance | BindingFlags.NonPublic);
            SkipCoerceSelectedItemCheck = selectorType.GetProperty("SkipCoerceSelectedItemCheck", BindingFlags.Instance | BindingFlags.NonPublic);
        }

        /// <summary>
        /// 圆角
        /// </summary>
        /// <param name="hwnd"></param>
        /// <param name="attribute"></param>
        /// <param name="pvAttribute"></param>
        /// <param name="cbAttribute"></param>
        /// <returns></returns>
        [DllImport("dwmapi.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern long DwmSetWindowAttribute(IntPtr hwnd, DWMWINDOWATTRIBUTE attribute, ref DWM_WINDOW_CORNER_PREFERENCE pvAttribute, uint cbAttribute);

        /// <summary>
        /// ItemsControl的NewItemInfo
        /// </summary>
        public static readonly MethodInfo NewItemInfo;

        /// <summary>
        /// selectionchanged的SelectJustThisItem方法
        /// </summary>
        public static readonly MethodInfo SelectJustThisItem;

        /// <summary>
        /// selector属性SelectionChange
        /// </summary>
        public static readonly PropertyInfo SelectionChange;

        /// <summary>
        /// SelectionChanger的IsActive的属性
        /// </summary>
        public static readonly PropertyInfo IsActive;

        /// <summary>
        /// selector属性SkipCoerceSelectedItemCheck
        /// </summary>
        public static readonly PropertyInfo SkipCoerceSelectedItemCheck;

        /// <summary>
        /// Selector的OnSelectedItemChanged
        /// </summary>
        /// <param name="d"></param>
        /// <param name="e"></param>
        public static void OnSelectedItemChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            Selector s = (Selector)d;
            var selectionChange = SelectionChange.GetValue(s);
            var isActive = IsActive.GetValue(selectionChange) as bool?;
            if (isActive != null && isActive.Value)
            {
                var temp = NewItemInfo.Invoke(s, new object[] { e.NewValue });
                SelectJustThisItem.Invoke(selectionChange, new object[] { temp, false });
            }
        }

        /// <summary>
        /// Selector的CoerceSelectedItem
        /// </summary>
        /// <param name="d"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static object CoerceSelectedItem(DependencyObject d, object value)
        {
            Selector s = null;
            if (d is ComboBoxGroup comboBoxGroup)
            {
                s = comboBoxGroup.MainComboBox;
            }
            else
                s = (Selector)d;
            if (s != null)
            {
                var skipCoerceSelectedItemCheck = SkipCoerceSelectedItemCheck.GetValue(s) as bool?;
                if (value == null || (skipCoerceSelectedItemCheck != null && skipCoerceSelectedItemCheck.Value))
                    return value;

                int selectedIndex = s.SelectedIndex;

                if ((selectedIndex > -1 && selectedIndex < s.Items.Count && s.Items[selectedIndex] == value)
                    || s.Items.Contains(value))
                {
                    return value;
                }
            }
            return DependencyProperty.UnsetValue;
        }
    }
}
