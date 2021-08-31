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

namespace NetworkSystemFinder.Forms
{
    public partial class Logger : Form
    {
        public Logger()
        {
            InitializeComponent();
            Session.Instance.theme.ColorControl(this);
        }

        public string Log
        {
            set { richTextBoxLog.Text +=  value + "\n"; }
        }
    }
}
