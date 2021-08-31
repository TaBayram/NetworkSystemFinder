using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NetworkSystemFinder.Languages;

namespace NetworkSystemFinder.Helpers
{
    //Singleton class containing all session related information for the other classes to get from.
    class Session{
        private static Session instance;
        public static Session Instance{
            get { 
                if(instance == null){
                    instance = new Session();
                }
                return instance;
            }
        }

        private Session() {
            remLanguage = Properties.Settings.Default.Language;
            remTheme = Properties.Settings.Default.Theme;
            remIPStart = Properties.Settings.Default.IPStart;
            remIPEnd = Properties.Settings.Default.IPEnd;
            remUser = Properties.Settings.Default.User;
            remPassword = Properties.Settings.Default.Password;

            ApplySettings();
        }

        public void ChangeControlLanguage(Control control)
        {
            foreach (Control con in control.Controls)
            {
                con.Text = TryTranslate(con.Text);
                if (con.Controls.Count > 0) ChangeControlLanguage(con);
            }
        }

        public string TryTranslate(string text)
        {
            string translated = resourceManager.GetString(text);
            if (translated != "" && translated != null)
                text = translated;
            return text;
        }

        public ResourceManager resourceManager;
        public Theme theme;

        public string remLanguage;
        public string remIPStart;
        public string remIPEnd;
        public string remUser;
        public string remPassword;
        public int remTheme;


        public void ApplySettings()
        {
            theme = new Theme(remTheme);
            resourceManager = new ResourceManager(nameof(NetworkSystemFinder) + ".Languages" + "." + remLanguage, Assembly.GetExecutingAssembly());
        }
    }
}
