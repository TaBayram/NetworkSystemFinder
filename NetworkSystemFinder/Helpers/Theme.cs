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
        public Color textBoxBackground;
        public Color buttonBackground;
        public Color buttonHover;
        public Color buttonPress;
        public Color textHeadline;
        public Color textLine;
        public Color textLineInverted;
        public Color textPositive;
        public Color textNegative;

        public Theme() {
            if (Properties.Settings.Default.Theme == 0)
                SetLightMode();
            else
                SetDarkMode();
        }


        void SetLightMode(){
            mainBackground = Color.FromArgb(255, 245, 245, 245);
            minorBackground = Color.FromArgb(255, 227, 227, 227);
            textBoxBackground = Color.FromArgb(255, 240, 246, 255);
            buttonBackground = Color.FromArgb(255, 0, 37, 79);
            buttonHover = Color.FromArgb(255, 0, 17, 34);
            buttonPress = Color.FromArgb(255, 35, 100, 200);
            textHeadline = Color.FromArgb(255, 31, 39, 48);
            textLine = Color.FromArgb(255, 25, 25, 25);
            textLineInverted = Color.FromArgb(255, 225, 225, 255);
            textPositive = Color.FromArgb(255, 50, 255, 50);
            textNegative = Color.FromArgb(255, 255, 50, 50);

        }


        void SetDarkMode() {
        }


        public void ColorButton(Button button)
        {
            button.BackColor = buttonBackground;
            button.ForeColor = textLineInverted;
            button.FlatAppearance.MouseOverBackColor = buttonHover;
            button.FlatAppearance.MouseDownBackColor = buttonPress;
        }


        enum Themes{
            LightMode = 0,
            DarkMode = 1
        }

    }

}
