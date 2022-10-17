﻿using System;
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
    /// Interaction logic for TextBoxGroup.xaml
    /// </summary>
    public partial class TextBoxGroup : UserControl
    {
        /// <summary>
        /// 标签文字
        /// </summary>
        public string Label
        {
            get { return (string)GetValue(LabelProperty); }
            set { SetValue(LabelProperty, value); }
        }
        public static readonly DependencyProperty LabelProperty =
            DependencyProperty.Register("Label", typeof(string), typeof(TextBoxGroup), new PropertyMetadata("Label"));

        /// <summary>
        /// 内圈偏移
        /// </summary>
        public Thickness InnerMargin
        {
            get { return (Thickness)GetValue(InnerMarginProperty); }
            set { SetValue(InnerMarginProperty, value); }
        }
        public static readonly DependencyProperty InnerMarginProperty =
            DependencyProperty.Register("InnerMargin", typeof(Thickness), typeof(TextBoxGroup), new PropertyMetadata(new Thickness(18.0)));

        /// <summary>
        /// 构造函数
        /// </summary>
        public TextBoxGroup()
        {
            InitializeComponent();
        }
    }
}
