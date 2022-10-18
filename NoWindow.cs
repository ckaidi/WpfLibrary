using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfLibrary.Util;
using static WpfLibrary.Extension.SystemExtension;

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
        /// 导航栏按钮点击事件
        /// </summary>
        public event RoutedEventHandler NavigationButtonClick;
        public static readonly RoutedEvent NavigationButtonClickEvent;

        /// <summary>
        /// 静态构造函数
        /// </summary>
        static NoWindow()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(NoWindow), new FrameworkPropertyMetadata(typeof(NoWindow)));
            NavigationButtonClickEvent = EventManager.RegisterRoutedEvent("NavigationButtonClick", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(ButtonBase));
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        public NoWindow()
        {
            StateChanged += NoWindow_StateChanged;
        }

        /// <summary>
        /// 当控件加载模板时
        /// </summary>
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            _mainBorder = GetTemplateChild("MainBorder") as Border;

            _layoutRoot = _mainBorder.Child as Grid;

            _dockPanel = GetTemplateChild("TitleDock") as DockPanel;

            _navigationButton = GetTemplateChild("NavigationButton") as Button;
            _navigationButton.Click += ((a, b) =>
            {
                NavigationButtonClick.Invoke(a, b);
            });

            _closeButton = GetTemplateChild("CloseBtn") as Button;
            _closeButton.Click += ((a, b) =>
            {
                Close();
            });

            _maximizeButton = GetTemplateChild("MaximizeButton") as Button;
            _maximizeButton.Click += ((a, b) =>
            {
                WindowState = WindowState == WindowState.Maximized ? WindowState.Normal : WindowState.Maximized;
                _maximizeButton.Tag = WindowState == WindowState.Maximized ? "Max" : "Normal";
            });

            _minizeButton = GetTemplateChild("MinizeButton") as Button;
            _minizeButton.Click += ((a, b) =>
            {
                WindowState = WindowState.Minimized;
            });

            _topButton = GetTemplateChild("TopButton") as Button;
            _topButton.Click += ((a, b) =>
            {
                Topmost = !Topmost;
                _topButton.Tag = Topmost ? "Top" : "Auto";
            });

            _menuDockIsland = GetTemplateChild("MenuDockIsland") as MenuIsland;
            _leftPanel = GetTemplateChild("DockLeftPanel") as StackPanel;

            _moreButton = GetTemplateChild("MoreButton") as Button;
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

            ///圆角
            IntPtr hWnd = new WindowInteropHelper(GetWindow(this)).EnsureHandle();
            var attribute = DWMWINDOWATTRIBUTE.DWMWA_WINDOW_CORNER_PREFERENCE;
            var preference = DWM_WINDOW_CORNER_PREFERENCE.DWMWCP_ROUND;
            DwmSetWindowAttribute(hWnd, attribute, ref preference, sizeof(uint));
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
