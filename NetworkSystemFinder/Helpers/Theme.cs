using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NetworkSystemFinder.Helpers
{
    //Class for program colors
    class Theme{
        public Color mainBackground;
        public Color minorBackground;
        public Color majorBackground;
        public Color textBoxBackground;
        public Color buttonBackground;
        public Color buttonHover;
        public Color buttonPress;
        public Color textHeadline;
        public Color textLine;
        public Color textLineInverted;
        public Color textPositive;
        public Color textNegative;
        public Color textWarning;
        public Color cellBackground;
        public Color cellBackgroundAlternate;
        public Color cellText;

        public BorderStyle borderStyle;

        public Theme(int theme) {
            if (theme == 0)
                SetLightMode();
            else
                SetDarkMode();
        }


        private void SetLightMode(){
            borderStyle = BorderStyle.FixedSingle;
            mainBackground = Color.FromArgb(255, 245, 245, 245);
            minorBackground = Color.FromArgb(255, 227, 227, 227);
            majorBackground = Color.FromArgb(255, 235, 235, 235);
            textBoxBackground = Color.FromArgb(255, 240, 246, 255);
            buttonBackground = Color.FromArgb(255, 0, 37, 79);
            buttonHover = Color.FromArgb(255, 0, 17, 34);
            buttonPress = Color.FromArgb(255, 35, 100, 200);
            textHeadline = Color.FromArgb(255, 31, 39, 48);
            textLine = Color.FromArgb(255, 25, 25, 25);
            textLineInverted = Color.FromArgb(255, 225, 225, 255);
            textPositive = Color.FromArgb(255, 50, 255, 50);
            textNegative = Color.FromArgb(255, 255, 50, 50);
            textWarning = Color.FromArgb(255, 240, 105, 10);

            cellBackground = Color.FromArgb(255, 250, 250, 250);
            cellBackgroundAlternate = Color.FromArgb(255, 230, 230, 230);
            cellText = Color.FromArgb(255, 10, 10, 25);
        }


        private void SetDarkMode() {
            borderStyle = BorderStyle.None;
            mainBackground = Color.FromArgb(255,45, 45, 45);
            minorBackground = Color.FromArgb(255,60, 60, 60);
            majorBackground = Color.FromArgb(255, 80, 80, 80);
            textBoxBackground = Color.FromArgb(255, 25, 25, 25);
            buttonBackground = Color.FromArgb(255, 0, 107, 222);
            buttonHover = Color.FromArgb(255, 0, 123, 255);
            buttonPress = Color.FromArgb(255, 35, 166, 255);
            textHeadline = Color.FromArgb(255, 217, 217, 250);
            textLine = Color.FromArgb(255, 206, 206, 236);
            textLineInverted = Color.FromArgb(255, 25, 25, 25);
            textPositive = Color.FromArgb(255, 50, 255, 50);
            textNegative = Color.FromArgb(255, 255, 50, 50);
            textWarning = Color.FromArgb(255, 240, 105, 10);

            cellBackground = Color.FromArgb(255, 30, 30, 30);
            cellBackgroundAlternate = Color.FromArgb(255, 20, 20, 20);
            cellText = Color.FromArgb(255, 240, 240, 250);
        }


        public void ColorButton(Button button)
        {
            if (button.BackColor == Color.Transparent) return;
            button.BackColor = buttonBackground;
            button.ForeColor = textLineInverted;
            button.FlatAppearance.MouseOverBackColor = buttonHover;
            button.FlatAppearance.MouseDownBackColor = buttonPress;
        }

        public void ColorTextBox(TextBox textBox)
        {
            textBox.BorderStyle = borderStyle;
            textBox.BackColor = textBoxBackground;
            textBox.ForeColor = textLine;
        }

        public void ColorLabel(Label label)
        {
            if (label.Name.Contains("NEGATIVE"))
                label.ForeColor = textNegative;
            else if (label.Name.Contains("POSITIVE"))
                label.ForeColor = textPositive;
            else if (label.Name.Contains("WARNING"))
                label.ForeColor = textWarning;
            else
                label.ForeColor = textLine;
        }
        
        public void ColorComboBox(ComboBox comboBox)
        {
            comboBox.BackColor = textBoxBackground;
            comboBox.ForeColor = textLine;
        }

        public void ColorDataGridView(DataGridView dataGridView)
        {
            dataGridView.BackgroundColor = majorBackground;

            //dataGridView.DefaultCellStyle.Font = new Font("Tahoma", 15);
            dataGridView.DefaultCellStyle.ForeColor = cellText;
            dataGridView.DefaultCellStyle.BackColor = cellBackground;
            dataGridView.DefaultCellStyle.SelectionForeColor = textLineInverted;
            dataGridView.DefaultCellStyle.SelectionBackColor = buttonBackground;

            dataGridView.ColumnHeadersDefaultCellStyle.BackColor = majorBackground;
            dataGridView.ColumnHeadersDefaultCellStyle.ForeColor = textHeadline;
            dataGridView.ColumnHeadersDefaultCellStyle.SelectionBackColor = buttonPress;
            dataGridView.ColumnHeadersDefaultCellStyle.SelectionForeColor = textLineInverted;

            dataGridView.RowsDefaultCellStyle.BackColor = cellBackground;
            dataGridView.AlternatingRowsDefaultCellStyle.BackColor = cellBackgroundAlternate;
        }

        public void ColorCheckListBox(CheckedListBox checkedList)
        {
            checkedList.BackColor = minorBackground;
            checkedList.ForeColor = textLine;
        }

        public void ColorCheckBox(CheckBox checkBox)
        {
            checkBox.ForeColor = textLine;
        }

        public void ColorGroupBox(GroupBox groupBox)
        {
            groupBox.BackColor = minorBackground;
            groupBox.ForeColor = textHeadline;
        }

        public void ColorControl(Control control)
        {
            foreach (Control con in control.Controls)
            {
                Type type = con.GetType();
                if (type == typeof(Button))
                    ColorButton((Button)con);
                else if (type == typeof(TextBox))
                    ColorTextBox((TextBox)con);
                else if (type == typeof(ComboBox))
                    ColorComboBox((ComboBox)con);
                else if (type == typeof(Label))
                    ColorLabel((Label)con);
                else if (type == typeof(GroupBox))
                    ColorGroupBox((GroupBox)con);
                else if (type == typeof(DataGridView))
                    ColorDataGridView((DataGridView)con);
                else if (type == typeof(CheckedListBox))
                    ColorCheckListBox((CheckedListBox)con);
                else if (type == typeof(CheckBox))
                    ColorCheckBox((CheckBox)con);
                else if (con is UserControl || con is Form || con is Panel)
                    con.BackColor = mainBackground;
                else
                {
                    con.BackColor = mainBackground;
                    con.ForeColor = textHeadline;
                }

                if (con.Controls.Count > 0) ColorControl(con);
            }
        }


        public enum Themes: int{
            Light = 0,
            Dark = 1
        }

    }

}
