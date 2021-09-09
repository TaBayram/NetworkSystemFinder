using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;

namespace NetworkSystemFinder.Models.Parts
{
    public class Motherboard
    {
        string manufacturer;
        string model;

        public Motherboard()
        {
            this.Manufacturer = "Unknown";
            this.Model = "Unknown";
        }

        public string Manufacturer { get => manufacturer; set => manufacturer = value; }
        public string Model { get => model; set => model = value; }

        public void GetInformation(ManagementObject managementObject)
        {
            if (managementObject["Manufacturer"] != null)
                Manufacturer = managementObject["Manufacturer"].ToString();
            if (managementObject["Product"] != null)
                Model = managementObject["Product"].ToString();
        }
    }
}
