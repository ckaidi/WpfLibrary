using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    /// ImageButton.xaml 的交互逻辑
    /// </summary>
    public partial class ImageButton : UserControl
    {
        /// <summary>
        /// 点击事件
        /// </summary>
        [Category("Behavior")]
        public event RoutedEventHandler Click { add { AddHandler(ClickEvent, value); } remove { RemoveHandler(ClickEvent, value); } }
        public static readonly RoutedEvent ClickEvent = EventManager.RegisterRoutedEvent("Click", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(ImageButton));

        /// <summary>
        /// 圆角设置
        /// </summary>
        public CornerRadius CornerRadius
        {
            get { return (CornerRadius)GetValue(CornerRadiusProperty); }
            set { SetValue(CornerRadiusProperty, value); }
        }
        public static readonly DependencyProperty CornerRadiusProperty =
            DependencyProperty.Register("CornerRadius", typeof(CornerRadius), typeof(ImageButton), new PropertyMetadata(new CornerRadius(10)));

        /// <summary>
        /// 圆角设置
        /// </summary>
        public Thickness ImageMargin
        {
            get { return (Thickness)GetValue(ImageMarginProperty); }
            set { SetValue(ImageMarginProperty, value); }
        }
        public static readonly DependencyProperty ImageMarginProperty =
            DependencyProperty.Register("ImageMargin", typeof(Thickness), typeof(ImageButton), new PropertyMetadata(new Thickness(0)));

        /// <summary>
        /// 圆角设置
        /// </summary>
        public double Rotation
        {
            get { return (double)GetValue(RotationProperty); }
            set { SetValue(RotationProperty, value); }
        }
        public static readonly DependencyProperty RotationProperty =
            DependencyProperty.Register("Rotation", typeof(double), typeof(ImageButton), new PropertyMetadata(0.0));

        /// <summary>
        /// 按钮图片
        /// </summary>
        public ImageSource Source
        {
            get { return (ImageSource)GetValue(SourceProperty); }
            set { SetValue(SourceProperty, value); }
        }
        public static readonly DependencyProperty SourceProperty =
                DependencyProperty.Register(
                        "Source",
                        typeof(ImageSource),
                        typeof(ImageButton),
                        new FrameworkPropertyMetadata(
                                null,
                                FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsRender,
                                new PropertyChangedCallback(OnSourceChanged),
                                null),
                        null);

        public ImageButton()
        {
            InitializeComponent();
            Loaded += ImageButton_Loaded;
            SizeChanged += ImageButton_SizeChanged;
        }

        private void ImageButton_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            ImageScaleTransform.CenterX = ActualWidth / 2;
            ImageScaleTransform.CenterY = ActualHeight / 2;
        }

        private void ImageButton_Loaded(object sender, RoutedEventArgs e)
        {
            ImageScaleTransform.CenterX = ActualWidth / 2;
            ImageScaleTransform.CenterY = ActualHeight / 2;

            ImageRotateTransform.CenterX = ActualWidth / 2;
            ImageRotateTransform.CenterY = ActualHeight / 2;
        }

        private static void OnSourceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
        }

        private void Button_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var an1 = new DoubleAnimation()
            {
                From = 1,
                To = 0.5,
                Duration = new Duration(TimeSpan.FromSeconds(0.1)),
            };
            ImageScaleTransform.BeginAnimation(ScaleTransform.ScaleXProperty, an1);
            ImageScaleTransform.BeginAnimation(ScaleTransform.ScaleYProperty, an1);
        }

        private void Button_MouseUp(object sender, MouseButtonEventArgs e)
        {
            var an2 = new DoubleAnimation()
            {
                From = 0.5,
                To = 1,
                Duration = new Duration(TimeSpan.FromSeconds(0.1)),
            };
            ImageScaleTransform.BeginAnimation(ScaleTransform.ScaleXProperty, an2);
            ImageScaleTransform.BeginAnimation(ScaleTransform.ScaleYProperty, an2);
        }

        /// <summary>
        /// 进入按钮的时候背景改变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Border_MouseEnter(object sender, MouseEventArgs e)
        {
            MainBorder.Background = new SolidColorBrush(new Color()
            {
                A = 0x33,
                R = 0X00,
                G = 0X00,
                B = 0X00,
            });
            MainBorder.BorderBrush = new SolidColorBrush(new Color()
            {
                A = 0x33,
                R = 0X00,
                G = 0X00,
                B = 0X00,
            });
        }

        private void Border_MouseLeave(object sender, MouseEventArgs e)
        {
            MainBorder.Background = Background;
            MainBorder.BorderBrush = new SolidColorBrush(Colors.Gray);
        }

        protected override void OnMouseLeftButtonUp(MouseButtonEventArgs e)
        {
            RoutedEventArgs newEvent = new RoutedEventArgs(ClickEvent, this);
            RaiseEvent(newEvent);
        }
    }
}
