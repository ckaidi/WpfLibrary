using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfLibrary
{
    /// <summary>
    /// 无边框窗口
    /// </summary>
    public class NoWindow : Window
    {
        /// <summary>
        /// 最外层的边框
        /// </summary>
        private Border _mainBorder;

        /// <summary>
        /// 布局根网格
        /// </summary>
        private Grid _layoutRoot;

        /// <summary>
        /// 窗口的Dock栏
        /// </summary>
        private DockPanel _dockPanel;

        /// <summary>
        /// 关闭按钮
        /// </summary>
        private Button _closeButton;

        /// <summary>
        /// 最大化按钮
        /// </summary>
        private Button _maximizeButton;

        /// <summary>
        /// 最小化按钮
        /// </summary>
        private Button _minizeButton;

        /// <summary>
        /// 置顶按钮
        /// </summary>
        private Button _topButton;

        /// <summary>
        /// 更多菜单按钮(陈动岛)
        /// </summary>
        private Button _moreButton;

        /// <summary>
        /// 左侧菜单导航栏按钮
        /// </summary>
        private Button _navigationButton;

        /// <summary>
        /// 陈动岛Panel
        /// </summary>
        private MenuIsland _menuDockIsland;

        /// <summary>
        /// 标题栏左边
        /// </summary>
        private StackPanel _leftPanel;

        /// <summary>
        /// 是否显示更多按钮(陈动岛)
        /// </summary>
        public bool IsMoreButton
        {
            get { return (bool)GetValue(IsMoreButtonProperty); }
            set { SetValue(IsMoreButtonProperty, value); }
        }
        public static readonly DependencyProperty IsMoreButtonProperty =
            DependencyProperty.Register("IsMoreButton", typeof(bool), typeof(NoWindow), new PropertyMetadata(false));

        /// <summary>
        /// 是否显示导航菜单按钮
        /// </summary>
        public bool IsNavigationButton
        {
            get { return (bool)GetValue(IsNavigationButtonProperty); }
            set { SetValue(IsNavigationButtonProperty, value); }
        }
        public static readonly DependencyProperty IsNavigationButtonProperty =
            DependencyProperty.Register("IsNavigationButton", typeof(bool), typeof(NoWindow), new PropertyMetadata(false));

        /// <summary>
        /// 菜单栏按钮
        /// </summary>
        public NavigationViewItems MenuItems
        {
            get { return (NavigationViewItems)GetValue(MenuItemsProperty); }
            set { SetValue(MenuItemsProperty, value); }
        }
        //默认值需要时线程安全的,但NavigationViewItems不是
        public static readonly DependencyProperty MenuItemsProperty =
            DependencyProperty.Register("MenuItems", typeof(NavigationViewItems), typeof(NoWindow), new PropertyMetadata(new NavigationViewItems()));


        /// <summary>
        /// 静态构造函数
        /// </summary>
        static NoWindow()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(NoWindow), new FrameworkPropertyMetadata(typeof(NoWindow)));
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        public NoWindow()
        {
            StateChanged += NoWindow_StateChanged;
            Loaded += NoWindow_Loaded;
        }

        private void NoWindow_Loaded(object sender, RoutedEventArgs e)
        {
            _mainBorder = (Border)Template.FindName("MainBorder", this);
            _layoutRoot = _mainBorder.Child as Grid;
            _dockPanel = (DockPanel)Template.FindName("TitleDock", this);

            _navigationButton = (Button)Template.FindName("NavigationButton", this);
            _closeButton = (Button)Template.FindName("CloseBtn", this);
            _closeButton.Click += ((a, b) =>
            {
                Close();
            });

            _maximizeButton = (Button)Template.FindName("MaximizeButton", this);
            _maximizeButton.Click += ((a, b) =>
            {
                WindowState = WindowState == WindowState.Maximized ? WindowState.Normal : WindowState.Maximized;
                _maximizeButton.Tag = WindowState == WindowState.Maximized ? "Max" : "Normal";
            });

            _minizeButton = (Button)Template.FindName("MinizeButton", this);
            _minizeButton.Click += ((a, b) =>
            {
                WindowState = WindowState.Minimized;
            });

            _topButton = (Button)Template.FindName("TopButton", this);
            _topButton.Click += ((a, b) =>
            {
                Topmost = !Topmost;
                _topButton.Tag = Topmost ? "Top" : "Auto";
            });

            _menuDockIsland = (MenuIsland)Template.FindName("MenuDockIsland", this);
            _leftPanel = (StackPanel)Template.FindName("DockLeftPanel", this);

            _moreButton = (Button)Template.FindName("MoreButton", this);
            if (this.IsMoreButton)
            {
                _moreButton.Click += ((a, b) =>
                {
                    _menuDockIsland.Show();
                });
            }
            else
            {
                _moreButton.Visibility = Visibility.Collapsed;
            }
            return;
        }

        /// <summary>
        /// 窗口改变时
        /// 自适应的改变边距大小
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NoWindow_StateChanged(object sender, EventArgs e)
        {
            switch (this.WindowState)
            {
                case WindowState.Normal:
                    this._layoutRoot.Margin = new Thickness(0);
                    break;
                case WindowState.Minimized:
                    break;
                case WindowState.Maximized:
                    this._layoutRoot.Margin = new Thickness(6);
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// 将控件添加到dock栏上
        /// </summary>
        /// <param name="control"></param>
        /// <param name="dock">在dock的左边还是右边</param>
        public Button AddDockButton(string content, Dock dock)
        {
            var style = FindResource("CaptionButtonStyle") as Style;
            //添加左边按钮
            var result = new Button
            {
                Content = content,
                FontFamily = new FontFamily("Segoe MDL2 Assets"),
                Style = style
            };
            DockPanel.SetDock(result, dock);
            if (dock == Dock.Left)
                _leftPanel.Children.Add(result);
            return result;
        }
    }
}
