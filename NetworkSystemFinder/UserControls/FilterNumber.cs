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
    public partial class FilterNumber : UserControl
    {
        int index;
        string property;
        public GroupBox GroupBox { get => this.groupBox; }
        public int Index { get => index; set => index = value; }
        public string Property { get => property; set => property = value; }
        public int InputMin 
        { 
            get 
            {
                int min = 0;
                bool hasParsed = int.TryParse(this.textBoxMin.Text, out min);
                return hasParsed ? min : 0;
            }
        }
        public int InputMax
        {
            get
            {
                int max = int.MaxValue;
                bool hasParsed = int.TryParse(this.textBoxMax.Text, out max);
                return hasParsed ? max : int.MaxValue;
            }
        }

        

        public FilterNumber()
        {
            InitializeComponent();
        }
    }
}
