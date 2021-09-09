using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;

namespace NetworkSystemFinder.Models.Parts
{
    public class BIOS
    {
        string serialNumber;
        string systemType;

        public BIOS()
        {
            SerialNumber = "Unknown";
            SystemType = "Unknown";
        }

        public string SerialNumber { get => serialNumber; set => serialNumber = value; }
        public string SystemType { get => systemType; set => systemType = value; }

        public void GetInformation(ManagementObject managementObject)
        {
            if (managementObject["SerialNumber"] != null)
                SerialNumber = managementObject["SerialNumber"].ToString();
        }

        public void GetSystemInformation(ManagementObject managementObject)
        {
            if (managementObject["SystemType"] != null)
                SystemType = managementObject["SystemType"].ToString();
        }
    }
}
