using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;

namespace NetworkSystemFinder.Models.Parts
{
    public class Network
    {
        string mACAddress;
        string netConnectionID;
        bool physicalAdapter;
        bool netEnabled;

        public Network()
        {
            MACAddress = "Unknown";
            NetConnectionID = "Unknown";
            PhysicalAdapter = false;
            NetEnabled = false;
        }

        public string MACAddress { get => mACAddress; set => mACAddress = value; }
        public string NetConnectionID { get => netConnectionID; set => netConnectionID = value; }
        public bool PhysicalAdapter { get => physicalAdapter; set => physicalAdapter = value; }
        public bool NetEnabled { get => netEnabled; set => netEnabled = value; }

        public void GetInformation(ManagementObject managementObject)
        {
            if (managementObject["MACAddress"] != null)
                MACAddress = managementObject["MACAddress"].ToString();
            if (managementObject["NetConnectionID"] != null)
                NetConnectionID = managementObject["NetConnectionID"].ToString();
            if (managementObject["PhysicalAdapter"] != null)
                PhysicalAdapter = (managementObject["PhysicalAdapter"].ToString()).ToLower() == "true" ? true : false;
            if (managementObject["NetEnabled"] != null)
                NetEnabled = (managementObject["NetEnabled"].ToString()).ToLower() == "true" ? true : false;

        }
    }
}
