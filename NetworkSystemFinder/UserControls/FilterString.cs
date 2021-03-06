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

namespace NetworkSystemFinder.UserControls
{
    public partial class FilterString : UserControl
    {
        int index;
        string property;
        public GroupBox GroupBox { get => this.groupBox; }
        public string Property { get => property; set => property = value; }
        public string Input { get => this.textBox.Text.Trim().ToLower(); }
        public List<string> CheckedList { 
            get 
            {
                List<string> list = new List<string>();
                for (int i = 0; i < checkedListBox.CheckedItems.Count; i ++)
                {
                    list.Add(checkedListBox.CheckedItems[i].ToString());
                }
                return list;
            } 
        }

        public List<string> UnCheckedList
        {
            get
            {
                List<string> list = new List<string>();
                for(int i = 0; i < this.checkedListBox.Items.Count; i++)
                {
                    if (!checkedListBox.GetItemChecked(i))
                    {
                        list.Add(checkedListBox.Items[i].ToString());
                    }
                }
                return list;
            }
        }
        public int ListCount
        {
            get => this.checkedListBox.Items.Count;
        }
        public int Index { get => index; set => index = value; }

        public FilterString()
        {
            InitializeComponent();
            Session.Instance.theme.ColorControl(this);
            Session.Instance.ChangeControlLanguage(this);
        }

        

        public void AddItem(string item)
        {
            item = item.Trim();
            bool exists = false;
            foreach (object controlCollection in checkedListBox.Items)
            {
                if(controlCollection.ToString() == item)
                {
                    exists = true;
                    break;
                }
            }

            if(!exists)
                this.checkedListBox.Items.Add(item,true);

            if(this.checkedListBox.Items.Count == 1)
            {
                this.checkedListBox.MinimumSize = new Size(180, 15);
                this.checkedListBox.Size = this.checkedListBox.MinimumSize;
                buttonNone.Visible = true;
                buttonAll.Visible = true;
            }
        }

        private void checkedListBox_MouseEnter(object sender, EventArgs e)
        {
            checkedListBox.Size = new Size(180,90);
            buttonAll.Location =  new Point(buttonAll.Location.X, 47 + 90);
            buttonNone.Location = new Point(buttonNone.Location.X, 47 + 90);
        }

        private void checkedListBox_MouseLeave(object sender, EventArgs e)
        {
            timerMouseControl.Enabled = true;
        }

        private void timerMouseControl_Tick(object sender, EventArgs e)
        {
            Point pos = this.PointToClient(Cursor.Position);
            if (!this.DisplayRectangle.Contains(pos))
            {
                timerMouseControl.Enabled = false;
                checkedListBox.Size = new Size(180, 15);
                buttonAll.Location = new Point(buttonAll.Location.X, 47);
                buttonNone.Location = new Point(buttonNone.Location.X, 47);
            }
        }

        private void buttonNone_Click(object sender, EventArgs e)
        {
            for(int i = 0; i < checkedListBox.Items.Count; i++)
            {
                checkedListBox.SetItemChecked(i, false);
            }
        }

        private void buttonAll_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < checkedListBox.Items.Count; i++)
            {
                checkedListBox.SetItemChecked(i, true);
            }
        }
    }
}
