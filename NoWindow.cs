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
        /// 动画播放时间
        /// </summary>
        private const double _duration = .1;

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
        /// 更多菜单按钮
        /// </summary>
        private Button _moreButton;

        /// <summary>
        /// 陈动岛Panel
        /// </summary>
        private MenuIsland _menuDockIsland;

        /// <summary>
        /// 标题栏左边
        /// 
        /// </summary>
        private StackPanel _leftPanel;

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
            _moreButton.Click += ((a, b) =>
            {
                _menuDockIsland.Show();
            });
            return;
        }

        /// <summary>
        /// 窗口改变时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <exception cref="NotImplementedException"></exception>
        private void NoWindow_StateChanged(object sender, EventArgs e)
        {

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
