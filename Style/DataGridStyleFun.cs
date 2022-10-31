using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using Control = System.Windows.Controls.Control;
using DataGrid = System.Windows.Controls.DataGrid;
using DataGridCell = System.Windows.Controls.DataGridCell;

namespace WpfLibrary
{
    public partial class DataGridStyleFun
    {
        /// <summary>
        /// 单元获得焦点的时候开启编辑
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DataGridCell_GotFocus(object sender, RoutedEventArgs e)
        {
            if (sender is DataGridCell dataGridCell)
            {
                var parent = VisualTreeHelper.GetParent(dataGridCell);
                while (parent != null && parent.GetType() != typeof(DataGrid))
                {
                    parent = VisualTreeHelper.GetParent(parent);
                }
                var dataGrid = parent as DataGrid;
                if (dataGrid.CurrentCell.Column == dataGridCell.Column)
                {
                    if (dataGridCell.Column is DataGridTemplateColumn dataGridTemplateColumn)
                    {
                        var control = dataGridTemplateColumn.GetCellContent(dataGrid.CurrentCell.Item) as FrameworkElement;
                        FrameworkElement root;
                        if (dataGridCell.IsEditing)
                            root = dataGridTemplateColumn.CellEditingTemplate.FindName("control", control) as FrameworkElement;
                        else
                            root = dataGridTemplateColumn.CellTemplate.FindName("control", control) as FrameworkElement;
                        if (root.IsEnabled)
                            dataGrid.BeginEdit();
                    }
                    else
                        dataGrid.BeginEdit();
                }
            }
        }
    }
}
