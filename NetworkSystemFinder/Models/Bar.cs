using NetworkSystemFinder.Helpers;
using NetworkSystemFinder.UserControls;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NetworkSystemFinder.Models
{
    public abstract class Bar: UserControl
    {
        protected Main main;
        protected DataGridView dataGrid;
        protected Stopwatch stopWatch;
        protected CountdownEvent countdown;
        protected int pingTotal = 0;
        protected int pingCurrent = 0;

        protected Stack<UserControl> filterStack = new Stack<UserControl>();

        protected Label rowCount;
        public Bar()
        {
        }
        protected void Initialize(Main main,Label labelCount)
        {
            this.main = main;
            InitializeGrid();
            this.rowCount = labelCount;
        }
        protected int RowCount
        {
            set { if(rowCount != null) rowCount.Text = value + " " + Session.Instance.resourceManager.GetString("keyRows"); }
        }


        private void InitializeGrid()
        {
            DataGridViewCellStyle cellStyle = new DataGridViewCellStyle();
            dataGrid = new DataGridView();
            dataGrid.AllowUserToAddRows = false;
            dataGrid.AllowUserToDeleteRows = false;
            dataGrid.AllowUserToOrderColumns = true;
            dataGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
            dataGrid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGrid.EditMode = DataGridViewEditMode.EditProgrammatically;
            dataGrid.RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.Sunken;
            cellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            cellStyle.BackColor = SystemColors.Control;
            cellStyle.Font = new Font("Microsoft Sans Serif", 3F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            cellStyle.ForeColor = SystemColors.WindowText;
            cellStyle.SelectionBackColor = SystemColors.Highlight;
            cellStyle.SelectionForeColor = SystemColors.HighlightText;
            cellStyle.WrapMode = DataGridViewTriState.False;
            dataGrid.RowHeadersDefaultCellStyle = cellStyle;
            dataGrid.RowHeadersVisible = false;
            dataGrid.RowsAdded += new DataGridViewRowsAddedEventHandler(dataGrid_RowsAdded);
            dataGrid.RowsRemoved += new DataGridViewRowsRemovedEventHandler(dataGrid_RowsRemoved);

            dataGrid.Parent = main.RightPanel;
            dataGrid.Dock = DockStyle.Fill;
            Session.Instance.theme.ColorDataGridView(dataGrid);
        }

        private void dataGrid_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            RowCount = e.RowCount;
        }
        private void dataGrid_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            RowCount = e.RowCount;
        }
        protected void LoadDataToGrid(object data)
        {
            var source = new BindingSource(data, null);
            dataGrid.DataSource = source;
            RowCount = dataGrid.RowCount;
        }

        protected void SplitIP(string ip,out string[] ipSplitString, out int[] ipSplitInt)
        {
            ipSplitString = ip.Split('.');
            ipSplitInt = new int[Math.Max(ipSplitString.Length,4)];
            for (int i = 0; i < ipSplitString.Length; i++) int.TryParse(ipSplitString[i], out ipSplitInt[i]);
        }

        protected void WriteLog(string text)
        {
            if (main.Logger != null)
                main.Logger.Log = text; 
        }

        protected void SetFilters(FlowLayoutPanel flowLayoutPanel,CheckedListBox checkedListBox)
        {
            if (filterStack.Count != 0) return;
            foreach (DataGridViewTextBoxColumn column in dataGrid.Columns)
            {
                if (column.Name == "Status")
                {
                    FilterCombobox filterCombobox = new FilterCombobox();
                    filterCombobox.Anchor = (AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top);
                    filterCombobox.Property = column.Name;
                    filterCombobox.Index = column.Index;
                    filterCombobox.GroupBox.Text = column.Name;
                    flowLayoutPanel.Controls.Add(filterCombobox);
                    filterStack.Push(filterCombobox);
                }
                else if (column.Name != "RAM" && column.Name != "HDD" && column.Name != "SSD")
                {
                    FilterString filterString = new FilterString();
                    filterString.Anchor = (AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top);
                    filterString.Property = column.Name;
                    filterString.Index = column.Index;
                    filterString.GroupBox.Text = column.Name;
                    flowLayoutPanel.Controls.Add(filterString);
                    filterStack.Push(filterString);
                }
                else
                {
                    FilterNumber filterNumber = new FilterNumber();
                    filterNumber.Anchor = (AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top);
                    filterNumber.Property = column.Name;
                    filterNumber.Index = column.Index;
                    filterNumber.GroupBox.Text = column.Name;
                    flowLayoutPanel.Controls.Add(filterNumber);
                    filterStack.Push(filterNumber);
                }

            }

            foreach (DataGridViewTextBoxColumn column in dataGrid.Columns)
            {
                checkedListBox.Items.Add(column.Name);
                checkedListBox.SetItemChecked(checkedListBox.Items.Count - 1, true);
            }
        }


        protected bool CheckComboboxFilter(object obj)
        {
            bool hasDeleted = false;
            Type type = obj.GetType();
            foreach (FilterCombobox filterCombobox in filterStack.OfType<FilterCombobox>())
            {
                if (hasDeleted) break;
                if (filterCombobox.SelectedItem == "" || filterCombobox.SelectedItem == "ALL") continue;
                if (type.GetProperty(filterCombobox.Property).GetValue(obj, null).ToString() != filterCombobox.SelectedItem)
                {
                    hasDeleted = true;
                    break;
                }
            }

            return hasDeleted;
        }

        protected bool CheckStringFilter(object obj)
        {
            bool hasDeleted = false;
            Type type = obj.GetType();
            foreach (FilterString filterString in filterStack.OfType<FilterString>())
            {
                if (hasDeleted) break;
                List<string> checkedList = filterString.CheckedList;
                if (filterString.Input == "" && checkedList.Count == filterString.ListCount) continue;
                string value = type.GetProperty(filterString.Property).GetValue(obj, null).ToString().ToLower();

                string[] names = filterString.Input.ToLower().Split(' ');
                bool inputDelete = false;
                foreach (string str in names)
                {
                    if (!value.Contains(str))
                    {
                        inputDelete = true;
                        break;
                    }
                }

                bool itemDelete = true;
                if (checkedList.Count != filterString.ListCount)
                {
                    foreach (string item in checkedList)
                    {
                        if (value.StartsWith(item.ToLower()))
                        {
                            itemDelete = false;
                            break;
                        }
                    }
                }
                if ((filterString.Input != "" && inputDelete) || (checkedList.Count != filterString.ListCount && itemDelete))
                {
                    hasDeleted = true;
                }
            }

            return hasDeleted;
        }

        protected bool CheckNumberFilter(object obj)
        {
            bool hasDeleted = false;
            Type type = obj.GetType();
            foreach (FilterNumber filterNumber in filterStack.OfType<FilterNumber>())
            {
                if (hasDeleted) break;
                if (filterNumber.InputMax == int.MaxValue && filterNumber.InputMin == 0) continue;
                int value = 0;
                bool hasParsed = int.TryParse(type.GetProperty(filterNumber.Property).GetValue(obj, null).ToString().Trim(), out value);
                if (!hasParsed) continue;

                if (value < filterNumber.InputMin || value > filterNumber.InputMax)
                {
                    hasDeleted = true;
                    break;
                }
            }

            return hasDeleted;
        }

        protected string GetRamType(int type)
        {
            string outValue = string.Empty;

            switch (type)
            {
                case 0: outValue = "Unknown"; break;
                case 1: outValue = "Other"; break;
                case 2: outValue = "DRAM"; break;
                case 3: outValue = "Synchronous DRAM"; break;
                case 4: outValue = "Cache DRAM"; break;
                case 5: outValue = "EDO"; break;
                case 6: outValue = "EDRAM"; break;
                case 7: outValue = "VRAM"; break;
                case 8: outValue = "SRAM"; break;
                case 9: outValue = "RAM"; break;
                case 10: outValue = "ROM"; break;
                case 11: outValue = "Flash"; break;
                case 12: outValue = "EEPROM"; break;
                case 13: outValue = "FEPROM"; break;
                case 14: outValue = "EPROM"; break;
                case 15: outValue = "CDRAM"; break;
                case 16: outValue = "3DRAM"; break;
                case 17: outValue = "SDRAM"; break;
                case 18: outValue = "SGRAM"; break;
                case 19: outValue = "RDRAM"; break;
                case 20: outValue = "DDR"; break;
                case 21: outValue = "DDR2"; break;
                case 22: outValue = "DDR2 FB-DIMM"; break;
                case 23: outValue = "Undefined 23"; break;
                case 24: outValue = "DDR3"; break;
                case 25: outValue = "FBD2"; break;
                case 26: outValue = "DDR4"; break;
                default: outValue = "Undefined"; break;
            }

            return outValue;
        }

        protected void ToggleColumn(CheckedListBox checkedListBox, ItemCheckEventArgs e)
        {
            bool exists = false;
            string property = checkedListBox.Items[e.Index].ToString();
            DataGridViewTextBoxColumn column = null;
            foreach (DataGridViewTextBoxColumn col in dataGrid.Columns)
            {
                if (col.Name == property)
                {
                    exists = true;
                    column = col;
                }
            }
            if (e.NewValue == CheckState.Unchecked && exists)
            {
                dataGrid.Columns.Remove(column);
            }
            else if (e.NewValue == CheckState.Checked && !exists)
            {
                column = new DataGridViewTextBoxColumn();
                column.DataPropertyName = property;
                column.Name = property;
                dataGrid.Columns.Add(column);
                dataGrid.Columns[property].DisplayIndex = Math.Min(e.Index, dataGrid.Columns.Count - 1);
            }
        }

    }
}
