using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;

namespace NetworkSystemFinder.Models.Parts
{
    public class OS
    {
        string model;
        string oSArchitecture;
        string version;
        string registeredUser;

        public OS()
        {
            Model = "Unknown";
            OSArchitecture = "Unknown";
            Version = "Unknown";
            RegisteredUser = "Unknown";
        }

        public string Model { get => model; set => model = value; }
        public string OSArchitecture { get => oSArchitecture; set => oSArchitecture = value; }
        public string Version { get => version; set => version = value; }
        public string RegisteredUser { get => registeredUser; set => registeredUser = value; }

        public void GetInformation(ManagementObject managementObject)
        {
            if (managementObject["Caption"] != null)
                Model = managementObject["Caption"].ToString();
            if (managementObject["OSArchitecture"] != null)
                OSArchitecture = managementObject["OSArchitecture"].ToString();
            if (managementObject["Version"] != null)
                Version = managementObject["Version"].ToString();
            if (managementObject["RegisteredUser"] != null)
                RegisteredUser = managementObject["RegisteredUser"].ToString();
        }
    }
}
