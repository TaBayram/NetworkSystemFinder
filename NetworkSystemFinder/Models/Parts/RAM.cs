using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;

namespace NetworkSystemFinder.Models.Parts
{
    public class RAM
    {
        string type;
        int capacity;
        int speed;
        public RAM()
        {
            type = "Unknown";
            capacity = 0;
            speed = 0;
        }

        public string Type { get => type; set => type = value; }
        public int Capacity { get => capacity; set => capacity = value; }
        public int Speed { get => speed; set => speed = value; }

        public void GetInformation(ManagementObject managementObject)
        {
            if (managementObject["MemoryType"] != null)
                Type = GetRamType(int.Parse(managementObject["MemoryType"].ToString()));
            if (managementObject["Capacity"] != null)
                capacity = (int)Math.Round(Int64.Parse(managementObject["Capacity"].ToString()) / 1024d / 1024 / 1024);
            if (managementObject["Speed"] != null)
                speed = int.Parse(managementObject["Speed"].ToString());
        }

        public string GetRamType(int type)
        {
            string outValue = string.Empty;
            switch (type)
            {
                case 0: outValue = "Unknown"; break;
                case 1: outValue = "Other"; break;
                case 2: outValue = "DRAM"; break;
                case 3: outValue = "Synchronous DRAM"; break;
                case 4: outValue = "Cache DRAM"; break;
                case 5: outValue = "EDO"; break;
                case 6: outValue = "EDRAM"; break;
                case 7: outValue = "VRAM"; break;
                case 8: outValue = "SRAM"; break;
                case 9: outValue = "RAM"; break;
                case 10: outValue = "ROM"; break;
                case 11: outValue = "Flash"; break;
                case 12: outValue = "EEPROM"; break;
                case 13: outValue = "FEPROM"; break;
                case 14: outValue = "EPROM"; break;
                case 15: outValue = "CDRAM"; break;
                case 16: outValue = "3DRAM"; break;
                case 17: outValue = "SDRAM"; break;
                case 18: outValue = "SGRAM"; break;
                case 19: outValue = "RDRAM"; break;
                case 20: outValue = "DDR"; break;
                case 21: outValue = "DDR2"; break;
                case 22: outValue = "DDR2 FB-DIMM"; break;
                case 23: outValue = "Undefined 23"; break;
                case 24: outValue = "DDR3"; break;
                case 25: outValue = "FBD2"; break;
                case 26: outValue = "DDR4"; break;
                default: outValue = "Undefined"; break;
            }

            return outValue;
        }
    }
}
