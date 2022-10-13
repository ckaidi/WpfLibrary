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
    /// SearchControl.xaml 的交互逻辑
    /// </summary>
    public partial class SearchControl : UserControl
    {
        public SearchControl()
        {
            InitializeComponent();
        }

        public string TipText
        {
            get { return (string)GetValue(TipTextProperty); }
            set { SetValue(TipTextProperty, value); }
        }

        public static readonly DependencyProperty TipTextProperty =
            DependencyProperty.Register("TipText", typeof(string), typeof(SearchControl), new PropertyMetadata("搜索数据库"));
    }
}
