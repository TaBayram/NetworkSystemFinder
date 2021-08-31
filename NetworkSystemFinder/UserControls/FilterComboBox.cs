using NetworkSystemFinder.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NetworkSystemFinder.Models;

namespace NetworkSystemFinder.UserControls
{
    public partial class FilterCombobox : UserControl
    {
        int index;
        string property;
        public GroupBox GroupBox { get => this.groupBox; }
        public int Index { get => index; set => index = value; }
        public string Property { get => property; 
            set 
            {
                property = value;
                if(property == "Status")
                {
                    foreach(Computer.StatusType statusType in Enum.GetValues(typeof(Computer.StatusType)))
                    {
                        comboBox.Items.Add(statusType);
                    }
                }

                comboBox.Items.Add("ALL");
                comboBox.SelectedItem = "ALL";
            } 
        }
        public string SelectedItem
        {
            get => this.comboBox.SelectedItem != null ? this.comboBox.SelectedItem.ToString() : "";
        }

        public int SelectedIndex
        {
            get => this.comboBox.SelectedIndex;
        }

        public FilterCombobox()
        {
            InitializeComponent();
            Session.Instance.ChangeControlLanguage(this);
            Session.Instance.theme.ColorControl(this);
        }
    }
}
