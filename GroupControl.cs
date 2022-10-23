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
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfLibrary
{
    /// <summary>
    /// 组控件
    /// </summary>
    public class GroupControl : ContentControl
    {
        /// <summary>
        /// 标签的textblock
        /// </summary>
        private TextBlock _labelTextBolck;

        /// <summary>
        /// 内容显示
        /// </summary>
        private ContentPresenter _contentPresenter;

        /// <summary>
        /// 标签文字
        /// </summary>
        public string Label
        {
            get { return (string)GetValue(LabelProperty); }
            set { SetValue(LabelProperty, value); }
        }
        public static readonly DependencyProperty LabelProperty =
            DependencyProperty.Register("Label", typeof(string), typeof(GroupControl), new PropertyMetadata("Label"));

        /// <summary>
        /// 内圈偏移
        /// </summary>
        public Thickness InnerMargin
        {
            get { return (Thickness)GetValue(InnerMarginProperty); }
            set { SetValue(InnerMarginProperty, value); }
        }
        public static readonly DependencyProperty InnerMarginProperty =
            DependencyProperty.Register("InnerMargin", typeof(Thickness), typeof(GroupControl), new PropertyMetadata(new Thickness(18.0)));

        /// <summary>
        /// 内圈偏移
        /// </summary>
        public LabelAllignment LabelAllignment
        {
            get { return (LabelAllignment)GetValue(LabelAllignmentProperty); }
            set { SetValue(LabelAllignmentProperty, value); }
        }
        public static readonly DependencyProperty LabelAllignmentProperty =
            DependencyProperty.Register("LabelAllignment", typeof(LabelAllignment), typeof(GroupControl), new PropertyMetadata(LabelAllignment.Left));

        /// <summary>
        /// 内容的宽度
        /// </summary>
        public double ContentWidth
        {
            get { return (double)GetValue(ContentWidthProperty); }
            set { SetValue(ContentWidthProperty, value); }
        }
        public static readonly DependencyProperty ContentWidthProperty =
            DependencyProperty.Register("ContentWidth", typeof(double), typeof(GroupControl), new PropertyMetadata(240.0));

        /// <summary>
        /// 内容的宽度
        /// </summary>
        public HorizontalAlignment LabelHorizontalAlignment
        {
            get { return (HorizontalAlignment)GetValue(LabelHorizontalAlignmentProperty); }
            set { SetValue(LabelHorizontalAlignmentProperty, value); }
        }
        public static readonly DependencyProperty LabelHorizontalAlignmentProperty =
            DependencyProperty.Register("LabelHorizontalAlignment", typeof(HorizontalAlignment), typeof(GroupControl), new PropertyMetadata(HorizontalAlignment.Center));

        /// <summary>
        /// 静态函数
        /// </summary>
        static GroupControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(GroupControl), new FrameworkPropertyMetadata(typeof(GroupControl)));
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            _labelTextBolck = (TextBlock)GetTemplateChild("tb");
            _contentPresenter = (ContentPresenter)GetTemplateChild("cp");
            if (LabelAllignment == LabelAllignment.Right)
            {
                _labelTextBolck.SetValue(Grid.ColumnProperty, 1);
                _contentPresenter.SetValue(Grid.ColumnProperty, 0);
            }
        }
    }

    /// <summary>
    /// 文字标签布局在左边还是右边
    /// </summary>
    public enum LabelAllignment
    {
        Left,
        Right,
    }
}
