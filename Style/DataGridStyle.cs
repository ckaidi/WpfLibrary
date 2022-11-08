using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace WpfLibrary
{
    public partial class DataGridStyle
    {

        /// <summary>
        /// 选中同行的时候
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void DataGridCell_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (sender is DataGridCell dataGrid)
            {
                if (!dataGrid.IsReadOnly)
                {

                }
            }
        }
    }
}
