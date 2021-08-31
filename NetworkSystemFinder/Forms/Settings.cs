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
    public partial class Settings : Form
    {
        public string newIPStart;
        public string newIPEnd;
        public string newUser;
        public string newPassword;
        public int newTheme;
        public Settings()
        {
            InitializeComponent();
            Session session = Session.Instance;
            this.Text = session.TryTranslate(this.Text);
            session.ChangeControlLanguage(this);
            session.theme.ColorControl(this);
            this.BackColor = session.theme.mainBackground;

            foreach (Theme.Themes theme in Enum.GetValues(typeof(Theme.Themes)))
            {
                comboBoxTheme.Items.Add(session.TryTranslate("key"+theme));
            }
            comboBoxTheme.SelectedIndex = session.remTheme;

            foreach(object obj in comboBoxLanguage.Items)
            {
                string language = obj.ToString().Trim().ToLower();
                language = language.Substring(language.IndexOf('(')+1).Replace(")", "");
                if(session.remLanguage == language)
                {
                    comboBoxLanguage.SelectedItem = obj;
                    break;
                }
            }

            textBoxIPStart.Text = session.remIPStart;
            textBoxIPEnd.Text = session.remIPEnd;
            textBoxUser.Text = session.remUser;
        }


        private void buttonDone_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.Language = StripSelectedLanguage();
            Properties.Settings.Default.Theme = comboBoxTheme.SelectedIndex;
            Properties.Settings.Default.IPStart = textBoxIPStart.Text.Trim();
            Properties.Settings.Default.IPEnd = textBoxIPEnd.Text.Trim();
            Properties.Settings.Default.User = textBoxUser.Text.Trim();
            Properties.Settings.Default.Password = textBoxPassword.Text.Trim();
            Properties.Settings.Default.Save();

            Session session = Session.Instance;
            session.remLanguage = Properties.Settings.Default.Language;
            session.remTheme = Properties.Settings.Default.Theme;
            session.remIPStart = Properties.Settings.Default.IPStart;
            session.remIPEnd = Properties.Settings.Default.IPEnd;
            session.remUser = Properties.Settings.Default.User;
            session.remPassword = Properties.Settings.Default.Password;

            session.ApplySettings();
            this.Close();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public string StripSelectedLanguage()
        {
            string language = comboBoxLanguage.SelectedItem.ToString().Trim().ToLower();
            return language.Substring(language.IndexOf('(') + 1).Replace(")", "");
        }

        private void comboBoxLanguage_SelectedIndexChanged(object sender, EventArgs e)
        {
            labelWarningWARNING.Visible = true;
            if (Session.Instance.remLanguage == StripSelectedLanguage()) labelWarningWARNING.Visible = false;
        }
    }
}
