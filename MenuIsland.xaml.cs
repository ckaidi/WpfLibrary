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
    /// 菜单陈动岛
    /// </summary>
    public partial class MenuIsland : UserControl
    {
        /// <summary>
        /// 动画持续时间
        /// </summary>
        private const double _duration = .1;

        public MenuIsland()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 陈动岛弹出
        /// </summary>
        public void Show()
        {
            if (this.MenuDock.Visibility == Visibility.Collapsed)//显示
            {
                this.MenuDock.Visibility = Visibility.Visible;
                Panel.SetZIndex(this, 999);

                var animationOpacity = new DoubleAnimation()
                {
                    From = 0,
                    To = 1,
                    Duration = new Duration(TimeSpan.FromSeconds(_duration))
                };

                this.MenuIslandScaleTransform.CenterX = this.MenuDock.Width / 2;
                this.MenuDockScaleTransform.CenterX = this.MenuDock.Width / 2;

                this.MenuIslandScaleTransform.CenterY = -10;
                this.MenuDockScaleTransform.CenterY = -10;

                this.MenuDockScaleTransform.BeginAnimation(ScaleTransform.ScaleXProperty, animationOpacity);
                this.MenuDockScaleTransform.BeginAnimation(ScaleTransform.ScaleYProperty, animationOpacity);
                this.MenuIslandScaleTransform.BeginAnimation(ScaleTransform.ScaleXProperty, animationOpacity);
                this.MenuIslandScaleTransform.BeginAnimation(ScaleTransform.ScaleYProperty, animationOpacity);
            }
            else
            {
                this.Hide();
            }
        }

        /// <summary>
        /// 陈动岛隐藏
        /// </summary>
        public void Hide()
        {
            var animationOpacity = new DoubleAnimation()
            {
                From = 1,
                To = 0,
                Duration = new Duration(TimeSpan.FromSeconds(_duration))
            };

            this.MenuIslandScaleTransform.CenterX = this.MenuDock.Width / 2;
            this.MenuDockScaleTransform.CenterX = this.MenuDock.Width / 2;
            animationOpacity.Completed += ((a1, b1) =>
            {
                this.MenuDock.Visibility = Visibility.Collapsed;
                Panel.SetZIndex(this, -1);
            });

            this.MenuDockScaleTransform.BeginAnimation(ScaleTransform.ScaleXProperty, animationOpacity);
            this.MenuDockScaleTransform.BeginAnimation(ScaleTransform.ScaleYProperty, animationOpacity);
            this.MenuIslandScaleTransform.BeginAnimation(ScaleTransform.ScaleXProperty, animationOpacity);
            this.MenuIslandScaleTransform.BeginAnimation(ScaleTransform.ScaleYProperty, animationOpacity);
        }
    }
}
