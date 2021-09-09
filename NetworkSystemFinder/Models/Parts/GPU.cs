using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;

namespace NetworkSystemFinder.Models.Parts
{
    public class GPU
    {
        float adapterRAM;
        string model;

        public GPU()
        {
            AdapterRAM = 0;
            Model = "Unknown";
        }

        public float AdapterRAM { get => adapterRAM; set => adapterRAM = value; }
        public string Model { get => model; set => model = value; }

        public void GetInformation(ManagementObject managementObject)
        {
            if (managementObject["Name"] != null)
                Model = managementObject["Name"].ToString();
            if (managementObject["AdapterRAM"] != null)
                AdapterRAM = (float)Math.Round(Int64.Parse(managementObject["AdapterRAM"].ToString()) / 1024d / 1024 / 1024,1);
        }
    }
}
