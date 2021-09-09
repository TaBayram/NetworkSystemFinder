using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;

namespace NetworkSystemFinder.Models.Parts
{
    public class Storage
    {
        string iD;
        string model;
        string name;
        StorageType type;
        int capacity;

        public Storage()
        {
            this.ID = "Unknown";
            this.Model = "Unknown";
            this.Name = "Unknown";
            this.Type = StorageType.HDD;
            this.Capacity = 0;
        }

        public string ID { get => iD; set => iD = value; }
        public string Model { get => model; set => model = value; }
        public string Name { get => name; set => name = value; }
        public int Capacity { get => capacity; set => capacity = value; }
        public StorageType Type { get => type; set => type = value; }

        public enum StorageType
        {
            HDD = 0,
            SSD = 1
        }

        public void GetInformation(ManagementObject managementObject,bool isNew)
        {
            if (isNew)
            {
                if(Convert.ToInt16(managementObject["MediaType"]) == 4)
                    Type = StorageType.SSD;
                if (managementObject["DeviceId"] != null)
                    ID = managementObject["DeviceId"].ToString();
                if (managementObject["Description"] != null)
                    Model = managementObject["Description"].ToString();
                if (managementObject["FriendlyName"] != null)
                    Name = managementObject["FriendlyName"].ToString();
                if (managementObject["Size"] != null)
                    Capacity = (int)(Convert.ToUInt64(managementObject["Size"]) / (1024 * 1024 * 1024));
            }
            else
            {
                if (managementObject["DeviceID"] != null)
                    ID = managementObject["DeviceID"].ToString();
                if (managementObject["Model"] != null)
                    Model = managementObject["Model"].ToString();
                if (managementObject["Name"] != null)
                    Name = managementObject["Name"].ToString();
                if (managementObject["Size"] != null)
                    Capacity = (int)(Convert.ToUInt64(managementObject["Size"]) / (1024 * 1024 * 1024));
            }
            
        }
    }
}
