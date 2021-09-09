using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;

namespace NetworkSystemFinder.Models.Parts
{
    public class Account
    {
        string domain;
        string name;
        string fullName;
        string status;
        bool disabled;
        bool passwordRequired;

        public Account()
        {
            Domain = "Unknown";
            Name = "Unknown";
            FullName = "Unknown";
            Status = "Unknown";
            Disabled = true;
            PasswordRequired = false;
        }

        public string Domain { get => domain; set => domain = value; }
        public string Name { get => name; set => name = value; }
        public string FullName { get => fullName; set => fullName = value; }
        public string Status { get => status; set => status = value; }
        public bool Disabled { get => disabled; set => disabled = value; }
        public bool PasswordRequired { get => passwordRequired; set => passwordRequired = value; }

        public void GetInformation(ManagementObject managementObject)
        {
            if (managementObject["Domain"] != null)
                Domain = managementObject["Domain"].ToString();
            if (managementObject["Name"] != null)
                Name = managementObject["Name"].ToString();
            if (managementObject["FullName"] != null)
                FullName = managementObject["FullName"].ToString();
            if (managementObject["Status"] != null)
                Status = managementObject["Status"].ToString();
            if (managementObject["Disabled"] != null)
                Disabled = managementObject["Name"].ToString().ToLower() == "true" ? true : false;
            if (managementObject["PasswordRequired"] != null)
                PasswordRequired = managementObject["PasswordRequired"].ToString().ToLower() == "true" ? true : false;
        }
       
    }
}
