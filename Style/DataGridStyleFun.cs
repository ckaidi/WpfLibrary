using System;
using System.Collections.Generic;
using System.Diagnostics;
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
                var celltype = typeof(DataGridCell);
                var dataGridPorperty = celltype.GetProperty("DataGridOwner", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
                var dataGrid = (DataGrid)dataGridPorperty.GetValue(dataGridCell);
                var isCurrentPorperty = celltype.GetProperty("IsCurrent", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
                var isCurrent = (bool)isCurrentPorperty.GetValue(dataGridCell);

                if (isCurrent)
                {
                    if (dataGridCell.Column is DataGridTemplateColumn dataGridTemplateColumn)
                    {
                        var control = dataGridTemplateColumn.GetCellContent(dataGrid.CurrentCell.Item) as FrameworkElement;
                        dataGridCell.Template.FindName("control", dataGridCell);
                        FrameworkElement root;
                        try
                        {
                            if (dataGridCell.IsEditing)
                                root = dataGridTemplateColumn.CellEditingTemplate.FindName("control", control) as FrameworkElement;
                            else
                                root = dataGridTemplateColumn.CellTemplate.FindName("control", control) as FrameworkElement;
                            if (root.IsEnabled)
                                dataGrid.BeginEdit();
                        }
                        catch (Exception ex)
                        {
                            Debug.WriteLine(ex.Source);
                            Debug.WriteLine(ex.StackTrace);
                        }
                    }
                    else
                        dataGrid.BeginEdit();
                }
            }
        }

        //private void DataGridCell_PreviewGotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        //{
        //    Debug.WriteLine("PreviewGotKeyboardFocus");
        //}

        //private void DataGridCell_LostFocus(object sender, RoutedEventArgs e)
        //{
        //    Debug.WriteLine("LostFocus");
        //}
    }
}
