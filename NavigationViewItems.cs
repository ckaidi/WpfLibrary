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
using ComboBox = System.Windows.Controls.ComboBox;
using static WpfLibrary.Extension.SystemExtension;
using System.Collections.Specialized;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;

namespace WpfLibrary
{
    /// <summary>
    /// 导航栏集合
    /// </summary>
    [StyleTypedProperty(Property = "ItemContainerStyle", StyleTargetType = typeof(NavigationViewItem))]
    [StyleTypedProperty(Property = "ItemContainerStyle", StyleTargetType = typeof(NavigationViewAddableItem))]
    public class NavigationViewItems : Selector
    {
        /// <summary>
        /// 新增加子项的默认名字
        /// </summary>
        public string NewAddItemDefaultName
        {
            get { return (string)GetValue(NewAddItemDefaultNameProperty); }
            set { SetValue(NewAddItemDefaultNameProperty, value); }
        }
        public static readonly DependencyProperty NewAddItemDefaultNameProperty =
            DependencyProperty.Register("NewAddItemDefaultName", typeof(string), typeof(NavigationViewItems), new PropertyMetadata("Untitled"));

        /// <summary>
        /// 是否可以折叠
        /// </summary>
        public bool IsFoldable
        {
            get { return (bool)GetValue(IsFoldableProperty); }
            set { SetValue(IsFoldableProperty, value); }
        }
        public static readonly DependencyProperty IsFoldableProperty =
            DependencyProperty.Register("IsFoldable", typeof(bool), typeof(NavigationViewItems), new PropertyMetadata(true));

        /// <summary>
        /// 新建标签栏的提示文字
        /// </summary>
        public string ItemAddText
        {
            get { return (string)GetValue(ItemAddTextProperty); }
            set { SetValue(ItemAddTextProperty, value); }
        }
        public static readonly DependencyProperty ItemAddTextProperty =
            DependencyProperty.Register("ItemAddText", typeof(string), typeof(NavigationViewItems), new PropertyMetadata("新建标签"));

        /// <summary>
        /// 用户是否可以添加
        /// </summary>
        public bool CanUserAddItem
        {
            get { return (bool)GetValue(CanUserAddItemProperty); }
            set { SetValue(CanUserAddItemProperty, value); }
        }
        public static readonly DependencyProperty CanUserAddItemProperty =
            DependencyProperty.Register("CanUserAddItem", typeof(bool), typeof(NavigationViewItems), new PropertyMetadata(false));


        /// <summary>
        /// 菜单是否是收起的状态
        /// </summary>
        public bool IsFold { get; set; }

        /// <summary>
        /// 主要的滚动条
        /// </summary>
        private ScrollViewer _mainScrollViewer;

        /// <summary>
        /// 主要网格
        /// </summary>
        private Grid _mainGrid;

        /// <summary>
        /// 第一行高度定义
        /// </summary>
        private RowDefinition _firstRowDefinition;

        /// <summary>
        /// 选中项的指示栏
        /// </summary>
        private Rectangle _statusBar;

        /// <summary>
        /// 新建标签
        /// </summary>
        private NavigationViewAddableItem _addViewItem;

        /// <summary>
        /// 菜单栏展开时的宽度
        /// </summary>
        private double _unfoldWidth;

        /// <summary>
        /// 静态构造函数
        /// </summary>
        static NavigationViewItems()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(NavigationViewItems), new FrameworkPropertyMetadata(typeof(NavigationViewItems)));
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        public NavigationViewItems()
        {
            Loaded += NavigationViewItems_Loaded;
        }

        /// <summary>
        /// 保存展开时候的宽度
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NavigationViewItems_Loaded(object sender, RoutedEventArgs e)
        {
            _unfoldWidth = _mainScrollViewer.ActualWidth;
            if (!IsFoldable)
                _mainScrollViewer.MinWidth = _mainScrollViewer.ActualWidth;
        }

        /// <summary>
        /// 加载模板
        /// </summary>
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            _mainScrollViewer = GetTemplateChild("MainScrollViewer") as ScrollViewer;
            _statusBar = GetTemplateChild("statusBar") as Rectangle;
            _addViewItem = GetTemplateChild("AddViewItem") as NavigationViewAddableItem;
            _mainGrid = GetTemplateChild("MainGrid") as Grid;
            _firstRowDefinition = GetTemplateChild("FirstRowDefinition") as RowDefinition;
            if (CanUserAddItem && Items.Count == 0)//如果一开始为空要把状态条设为不可见
            {
                _statusBar.Visibility = Visibility.Collapsed;
            }
        }

        /// <summary>
        /// 确定指定项是否可以作为自己的容器
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        protected override bool IsItemItsOwnContainerOverride(object item)
        {
            if (item is NavigationViewAddableItem)
            {
                if (!CanUserAddItem)
                {
                    throw new Exception("请将CanUserAddItem置为true");
                }
                return true;
            }
            return item is NavigationViewItem;
        }

        /// <summary>
        /// 给显示的元素创建标识
        /// </summary>
        /// <returns></returns>
        protected override DependencyObject GetContainerForItemOverride()
        {
            return CanUserAddItem ? new NavigationViewAddableItem() : new NavigationViewItem();
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
            if (IsFoldable)
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
        /// 由子类通知被点击
        /// </summary>
        /// <param name="navigationViewItem"></param>
        internal void NotifyNavigationViewMouseUp(NavigationViewItem navigationViewItem)
        {
            object item = ItemContainerGenerator.ItemFromContainer(navigationViewItem);
            if (item != null && item is NavigationViewItem navigation)
            {
                if (navigation.IsSelectAble)
                {
                    var selectionChange = SelectionChange.GetValue(this);
                    var temp = NewItemInfo.Invoke(this, new object[] { item, navigationViewItem, -1 });
                    SelectJustThisItem.Invoke(selectionChange, new object[] { temp, true });
                }
            }
            else if (navigationViewItem is NavigationViewAddableItem addableItem && CanUserAddItem && addableItem.IsDefaultAddItem)//如果是新建标签页被点击
            {
                var nvis = new List<NavigationViewItem>();
                foreach (NavigationViewItem nvi in Items)
                {
                    nvis.Add(nvi);
                }
                var menuTexts = nvis.Select(x => x.MenuText).ToList();
                int count = Items.Count + 1;
                while (menuTexts.Contains($"{NewAddItemDefaultName}{count}"))
                {
                    count++;
                }
                Items.Insert(Items.Count, new NavigationViewAddableItem()
                {
                    MenuText = $"{NewAddItemDefaultName}{count}"
                });
            }
        }

        /// <summary>
        /// 由子类通知被删除
        /// </summary>
        /// <param name="navigationViewAddableItem"></param>
        internal void NotifyNavigationViewDeleted(NavigationViewAddableItem navigationViewAddableItem)
        {
            object item = ItemContainerGenerator.ItemFromContainer(navigationViewAddableItem);
            if (item != null)
            {
                var removeIndex = Items.IndexOf(item);
                if (removeIndex != -1)
                {
                    Items.Remove(item);
                    SelectionChangedEventArgs args = null;
                    if (SelectedIndex >= removeIndex)//当前选择的索引大于被删除的索引
                    {
                        var newItem = Items[SelectedIndex] as NavigationViewItem;
                        newItem.IsSelected = true;
                        args = new SelectionChangedEventArgs(ComboBox.SelectionChangedEvent, new List<object> { item }, new List<object>() { newItem });
                        OnSelectionChanged(args);
                    }
                    //当选中的索引为负一则说明列表清空了
                    if (SelectedIndex == -1)
                    {
                        if (Items.Count == 0)
                            _statusBar.Visibility = Visibility.Collapsed;
                    }
                }
            }
        }

        /// <summary>
        /// 当列表的物体被清空时
        /// </summary>
        /// <param name="e"></param>
        protected override void OnItemsChanged(NotifyCollectionChangedEventArgs e)
        {
            if (e.NewStartingIndex == 0 && CanUserAddItem && Items.Count == 1)
            {
                var item = e.NewItems[0] as NavigationViewItem;
                _statusBar.Visibility = Visibility.Visible;
                item.IsSelected = true;
            }
            if (_mainGrid != null)
            {
                _mainGrid.UpdateLayout();
                //网格高度大于整个控件高度的时候应该固定新建标签按钮
                if (_mainGrid.DesiredSize.Height >= ActualHeight)
                    _firstRowDefinition.Height = new GridLength(1, GridUnitType.Star);
                //网格高度小于整个控件高度的时候应该移动新建标签按钮
                else
                    _firstRowDefinition.Height = new GridLength(1, GridUnitType.Auto);
            }
            base.OnItemsChanged(e);
        }

        /// <summary>
        /// 选择集改变时
        /// </summary>
        /// <param name="e"></param>
        protected override void OnSelectionChanged(SelectionChangedEventArgs e)
        {
            base.OnSelectionChanged(e);
            //选择集改变时
            if (SelectedIndex >= 0 && _statusBar != null && e.AddedItems.Count == 1 && e.RemovedItems.Count == 1)//在窗口第一次打开的时候,会先运行OnSelectionChanged,在运行loaded
            {
                _statusBar.Visibility = Visibility.Visible;
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

        /// <summary>
        /// 返回拥有指定元素的容器
        /// </summary>
        /// <param name="container"></param>
        /// <returns></returns>
        public new static ItemsControl ItemsControlFromItemContainer(DependencyObject container)
        {
            UIElement uIElement = container as UIElement;
            if (uIElement == null)
            {
                return null;
            }

            ItemsControl itemsControl = LogicalTreeHelper.GetParent(uIElement) as ItemsControl;
            if (itemsControl != null)
            {
                var generatorHost = itemsControl;
                if (generatorHost.IsItemItsOwnContainer(uIElement))
                {
                    return itemsControl;
                }

                return null;
            }

            uIElement = VisualTreeHelper.GetParent(uIElement) as UIElement;
            var p = GetItemsOwner(uIElement);
            if (p == null)
            {
                while (uIElement != null)
                {
                    if (uIElement is NavigationViewItems)
                    {
                        return (NavigationViewItems)uIElement;
                    }

                    uIElement = VisualTreeHelper.GetParent(uIElement) as UIElement;
                }
            }
            return p;
        }
    }
}
