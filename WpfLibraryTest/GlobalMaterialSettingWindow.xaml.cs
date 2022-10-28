using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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
using WpfLibrary;

namespace WpfLibraryTest
{
    /// <summary>
    /// 全局参数设置的页面
    /// </summary>
    public partial class GlobalMaterialSettingWindow : NoWindow
    {
        /// <summary>
        /// 视觉模型
        /// </summary>
        public class ViewModel : INotifyPropertyChanged
        {
            /// <summary>
            /// 属性更改通知
            /// </summary>
            public event PropertyChangedEventHandler PropertyChanged;

            // 此方法由每个属性的集合访问器调用  
            // 应用于可选propertyName的CallerMemberName属性
            // 参数导致调用方的属性名称被替换为参数。
            private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }

            /// <summary>
            /// 是否为木材
            /// </summary>
            public bool IsWoodMaterial
            {
                get => this._isWoodMaterial;
                set
                {
                    if (this._isWoodMaterial != value)
                    {
                        this._isWoodMaterial = value;
                        this.NotifyPropertyChanged();
                    }
                }
            }
            private bool _isWoodMaterial = false;

            /// <summary>
            /// 全干密度
            /// </summary>
            public double TotalDryRelativeDensity
            {
                get => this._totalDryRelativeDensity;
                set
                {
                    if (this._totalDryRelativeDensity != value)
                    {
                        this._totalDryRelativeDensity = value;
                        this.NotifyPropertyChanged();
                    }
                }
            }
            private double _totalDryRelativeDensity;

            /// <summary>
            /// sap中的材料名称
            /// </summary>
            public string SapMaterialName
            {
                get => this._sapMaterialName;
                set
                {
                    if (this._sapMaterialName != value)
                    {
                        this._sapMaterialName = value;
                        this.NotifyPropertyChanged();
                    }
                }
            }
            private string _sapMaterialName;

            /// <summary>
            /// 胶合木类型的索引
            /// </summary>
            public int TimberTypeIndex
            {
                get
                {
                    return this._timberTypeIndex;
                }
                set
                {
                    if (this._timberTypeIndex != value)
                    {
                        this._timberTypeIndex = value;
                        this.NotifyPropertyChanged();
                        this.NotifyPropertyChanged("StrengthGrages");
                        MyStrengthGrade = 0;
                        this.NotifyPropertyChanged("MyStrengthGrade");
                    }
                }
            }
            private int _timberTypeIndex = -1;

            /// <summary>
            /// 树种等级序号
            /// </summary>
            public int TreeGradeIndex
            {
                get
                {
                    return this._treeGradeIndex;
                }
                set
                {
                    if (this._treeGradeIndex != value)
                    {
                        this._treeGradeIndex = value;
                        this.NotifyPropertyChanged();
                    }
                }
            }
            private int _treeGradeIndex = -1;

            /// <summary>
            /// 树种等级选择index
            /// </summary>
            public int MyStrengthGrade
            {
                get => this._strengthGrade;
                set
                {
                    if (this._strengthGrade != value)
                    {
                        this._strengthGrade = value;
                        this.NotifyPropertyChanged();
                    }
                }
            }
            private int _strengthGrade = -1;

            /// <summary>
            /// 序号
            /// </summary>
            public int idx
            {
                get => this._idx;
                set
                {
                    if (this._idx != value)
                    {
                        this._idx = value;
                        this.NotifyPropertyChanged();
                    }
                }
            }
            private int _idx;

        }

        public List<ViewModel> Materials { get; set; }

        private void ComboBoxTimberType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (sender is ComboBox comboBox)
            {
                if (comboBox.Tag is int index)
                {
                    switch (comboBox.SelectedItem)
                    {
                        case "同等组合胶合木":
                            this.Materials[index].TimberTypeIndex = 0;
                            break;
                        case "对称异等组合胶合木":
                            this.Materials[index].TimberTypeIndex = 1;
                            break;
                        case "非对称异等组合胶合木":
                            this.Materials[index].TimberTypeIndex = 2;
                            break;
                    }
                }
            }
        }

        /// <summary>
        /// 树种等级下拉菜单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ComboBoxTreeGrade_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (sender is ComboBox comboBox)
            {
                if (comboBox.Tag is int index)
                {
                    this.Materials[index].TreeGradeIndex = comboBox.SelectedIndex;
                }
            }
        }

        private void cellComboBox0_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (sender is TextBox comboBox)
            {
                if (comboBox.Tag is int index)
                {
                    if (double.TryParse(comboBox.Text, out var value))
                        this.Materials[index].TotalDryRelativeDensity = value;
                }
            }
        }

        /// <summary>
        /// 材料等级选择集变化时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cellComboBox0_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (sender is ComboBox comboBox)
            {
                if (comboBox.Tag is int index)
                {
                    this.Materials[index].MyStrengthGrade = comboBox.SelectedIndex;
                }
            }
        }

        public void SetObj(object o)
        {
            //MainPropertyPanel
        }

        /// <summary>
        /// 取消按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }
    }
}
