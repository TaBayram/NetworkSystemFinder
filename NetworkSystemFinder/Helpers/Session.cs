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
            theme = new Theme();
            resourceManager = new ResourceManager(nameof(NetworkSystemFinder)+".Languages"+".tr", Assembly.GetExecutingAssembly());
        }

        public void ChangeControlLanguage(Control control)
        {
            foreach (Control con in control.Controls)
            {
                string translated = resourceManager.GetString(con.Text);
                if(translated != "" && translated != null)
                    con.Text = translated;
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
    }
}
