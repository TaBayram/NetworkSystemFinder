using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;

namespace NetworkSystemFinder.Models.Parts
{
    public class CPU
    {
        string model;
        int numberOfCores;
        int numberOfThreads;
        int maxClockSpeed;

        public CPU()
        {
            this.Model = "Unknown";
            this.NumberOfCores = 0;
            this.NumberOfThreads = 0;
            this.MaxClockSpeed = 0;
        }

        public string Model { get => model; set => model = value; }
        public int NumberOfCores { get => numberOfCores; set => numberOfCores = value; }
        public int NumberOfThreads { get => numberOfThreads; set => numberOfThreads = value; }
        public int MaxClockSpeed { get => maxClockSpeed; set => maxClockSpeed = value; }

        public void GetInformation(ManagementObject managementObject)
        {
            if (managementObject["Name"] != null)
                Model = managementObject["Name"].ToString();
            if (managementObject["NumberOfCores"] != null)
                NumberOfCores = int.Parse(managementObject["NumberOfCores"].ToString());
            if (managementObject["NumberOfLogicalProcessors"] != null)
                NumberOfThreads = int.Parse(managementObject["NumberOfLogicalProcessors"].ToString());
            if (managementObject["MaxClockSpeed"] != null)
                MaxClockSpeed = int.Parse(managementObject["MaxClockSpeed"].ToString());
        }
    }
}
