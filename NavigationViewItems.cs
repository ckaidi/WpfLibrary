using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfLibrary
{
    /// <summary>
    /// 导航栏集合
    /// </summary>
    [StyleTypedProperty(Property = "ItemContainerStyle", StyleTargetType = typeof(NavigationViewItem))]
    public class NavigationViewItems : ComboBox
    {
        /// <summary>
        /// 菜单是否是收起的状态
        /// </summary>
        public bool IsFold { get; set; }

        /// <summary>
        /// 主要的滚动条
        /// </summary>
        private ScrollViewer _mainScrollViewer;

        /// <summary>
        /// 选中项的指示栏
        /// </summary>
        private Rectangle _statusBar;

        /// <summary>
        /// 菜单栏展开时的宽度
        /// </summary>
        private double _unfoldWidth;

        static NavigationViewItems()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(NavigationViewItems), new FrameworkPropertyMetadata(typeof(NavigationViewItems)));
            var itemsControlType = typeof(ItemsControl);
            NewItemInfo = itemsControlType.GetMethod("NewItemInfo", BindingFlags.Instance | BindingFlags.NonPublic);
            var selectorType = typeof(ComboBox);
            SelectionChange = selectorType.GetProperty("SelectionChange", BindingFlags.Instance | BindingFlags.NonPublic);
            SelectJustThisItem = SelectionChange.PropertyType.GetMethod("SelectJustThisItem", BindingFlags.Instance | BindingFlags.NonPublic);
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        public NavigationViewItems()
        {
            this.Loaded += NavigationViewItems_Loaded;
        }

        /// <summary>
        /// 加载完成之后从内部模板获取控件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NavigationViewItems_Loaded(object sender, RoutedEventArgs e)
        {
            _mainScrollViewer = (ScrollViewer)Template.FindName("MainScrollViewer", this);
            _unfoldWidth = _mainScrollViewer.ActualWidth;
            _statusBar = (Rectangle)Template.FindName("statusBar", this);
        }

        /// <summary>
        /// 确定指定项是否可以作为自己的容器
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        protected override bool IsItemItsOwnContainerOverride(object item)
        {
            if (item is NavigationViewItem navigationViewItem)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 给显示的元素创建标识
        /// </summary>
        /// <returns></returns>
        protected override DependencyObject GetContainerForItemOverride()
        {
            return new NavigationViewItem();
        }

        /// <summary>
        /// 展开菜单栏
        /// </summary>
        public void Unfold()
        {
            var an = new DoubleAnimation()
            {
                From = 45,
                To = _unfoldWidth,
                Duration = new Duration(TimeSpan.FromSeconds(.1)),
            };
            foreach (NavigationViewItem item in Items)
            {
                item.MenuTextVisable = true;
            }
            _mainScrollViewer.BeginAnimation(ScrollViewer.WidthProperty, an);
            this.IsFold = false;
        }

        /// <summary>
        /// 收起菜单栏
        /// </summary>
        public void Fold()
        {
            var an = new DoubleAnimation()
            {
                From = _unfoldWidth,
                To = 45,
                Duration = new Duration(TimeSpan.FromSeconds(.1)),
            };
            foreach (NavigationViewItem item in Items)
            {
                item.MenuTextVisable = false;
            }
            _mainScrollViewer.BeginAnimation(ScrollViewer.WidthProperty, an);
            this.IsFold = true;
        }

        /// <summary>
        /// 反转菜单栏展开和收起状态
        /// </summary>
        public void ReverseFold()
        {
            if (this.IsFold)
            {
                Unfold();
            }
            else
            {
                Fold();
            }
        }

        /// <summary>
        /// itemscontrol方法的访问
        /// </summary>
        private static readonly MethodInfo NewItemInfo;

        /// <summary>
        /// selector属性SelectionChange
        /// </summary>
        private static readonly PropertyInfo SelectionChange;

        /// <summary>
        /// selectionchanged的SelectJustThisItem方法
        /// </summary>
        private static readonly MethodInfo SelectJustThisItem;

        /// <summary>
        /// 由子类通知被点击
        /// </summary>
        /// <param name="navigationViewItem"></param>
        internal void NotifyNavigationViewMouseUp(NavigationViewItem navigationViewItem)
        {
            object item = ItemContainerGenerator.ItemFromContainer(navigationViewItem);
            if (item != null)
            {
                var selectionChange = SelectionChange.GetValue(this);
                var temp = NewItemInfo.Invoke(this, new object[] { item, navigationViewItem, -1 });
                SelectJustThisItem.Invoke(selectionChange, new object[] { temp, true });
            }
        }

        /// <summary>
        /// 选择集改变时
        /// </summary>
        /// <param name="e"></param>
        protected override void OnSelectionChanged(SelectionChangedEventArgs e)
        {
            base.OnSelectionChanged(e);
            if (SelectedIndex >= 0 && _statusBar != null && e.AddedItems.Count == 1 && e.RemovedItems.Count == 1)//在窗口第一次打开的时候,会先运行OnSelectionChanged,在运行loaded
            {
                var nowSelected = e.AddedItems[0] as NavigationViewItem;
                var newMargin = new Thickness(5, SelectedIndex * 44 + 14, 0, 0);
                if (newMargin.Top > _statusBar.Margin.Top)//向下生长
                {
                    var an1 = new DoubleAnimation()
                    {
                        From = 16,
                        To = newMargin.Top - _statusBar.Margin.Top,
                        Duration = new Duration(TimeSpan.FromSeconds(0.1)),
                    };

                    an1.Completed += (a, b) =>
                    {
                        var an3 = new ThicknessAnimation
                        {
                            From = _statusBar.Margin,
                            To = newMargin,
                            Duration = new Duration(TimeSpan.FromSeconds(0.05)),
                        };
                        var an4 = new DoubleAnimation()
                        {
                            From = _statusBar.Height,
                            To = 16,
                            Duration = new Duration(TimeSpan.FromSeconds(0.05)),
                        };
                        _statusBar.BeginAnimation(Rectangle.MarginProperty, an3);
                        _statusBar.BeginAnimation(Rectangle.HeightProperty, an4);
                    };
                    _statusBar.BeginAnimation(Rectangle.HeightProperty, an1);
                }
                else//向上生长
                {
                    var an1 = new DoubleAnimation()
                    {
                        From = 16,
                        To = _statusBar.Margin.Top - newMargin.Top,
                        Duration = new Duration(TimeSpan.FromSeconds(0.1)),
                    };
                    var an3 = new ThicknessAnimation
                    {
                        From = _statusBar.Margin,
                        To = newMargin,
                        Duration = new Duration(TimeSpan.FromSeconds(0.1)),
                    };

                    an1.Completed += (a, b) =>
                    {
                        var an4 = new DoubleAnimation()
                        {
                            From = _statusBar.Height,
                            To = 16,
                            Duration = new Duration(TimeSpan.FromSeconds(0.05)),
                        };
                        _statusBar.BeginAnimation(Rectangle.HeightProperty, an4);
                    };
                    _statusBar.BeginAnimation(Rectangle.MarginProperty, an3);
                    _statusBar.BeginAnimation(Rectangle.HeightProperty, an1);
                }
            }
        }
    }
}
