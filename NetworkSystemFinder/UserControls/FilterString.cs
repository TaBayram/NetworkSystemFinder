using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NetworkSystemFinder.UserControls
{
    public partial class FilterString : UserControl
    {
        int index;
        string property;
        public GroupBox GroupBox { get => this.groupBox; }
        public string Property { get => property; set => property = value; }
        public string Input { get => this.textBox.Text.Trim().ToLower(); }
        public string[] CheckedList { get => this.checkedListBox.CheckedItems.OfType<string>().ToArray(); }
        public int Index { get => index; set => index = value; }

        public FilterString()
        {
            InitializeComponent();
        }

        public void SetCheckList()
        {
            if(property == "CPU")
            {

            }
        }
    }
}
