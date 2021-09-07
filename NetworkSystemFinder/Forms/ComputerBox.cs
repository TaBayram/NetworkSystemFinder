using NetworkSystemFinder.Models;
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
    public partial class ComputerBox : Form
    {
        public ComputerBox(Computer computer)
        {
            InitializeComponent();
            this.treeView1.Nodes.Add(computer.Name);
            this.treeView1.Nodes[0].Nodes.Add(computer.IP);
            this.treeView1.Nodes[0].Nodes.Add(computer.MAC);
            this.treeView1.Nodes.Add("OS");
            this.treeView1.Nodes[this.treeView1.Nodes.Count-1].Nodes.Add(computer.OS);
            this.treeView1.Nodes.Add("CPU");
            this.treeView1.Nodes[this.treeView1.Nodes.Count-1].Nodes.Add(computer.CPU);
            this.treeView1.Nodes.Add("RAM");
            this.treeView1.Nodes[this.treeView1.Nodes.Count - 1].Nodes.Add(computer.RAM.ToString());
            this.treeView1.Nodes.Add("GPU");
            this.treeView1.Nodes[this.treeView1.Nodes.Count - 1].Nodes.Add(computer.GPU);
        }
    }
}
