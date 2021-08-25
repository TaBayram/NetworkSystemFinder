using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkSystemFinder.Models
{
    class Printer
    {
        string iP;
        string name;
        Printer.StatusType status = Printer.StatusType.Dead;
        string serialNumber;
        string mAC;

        public Printer(string IP)
        {
            this.iP = IP;
            this.name = "?";
            this.serialNumber = "?";
            this.MAC = "?";

        }

        public string IP { get => iP; set => iP = value; }
        public string Name { get => name; set => name = value; }
        public StatusType Status { get => status; set => status = value; }
        public string SerialNumber { get => serialNumber; set => serialNumber = value; }
        public string MAC { get => mAC; set => mAC = value; }

        public enum StatusType
        {
            Alive = 0,
            Dead = 1,
        }

        public string ColumnProperty(int index)
        {
            switch (index)
            {
                case 0:
                    return this.iP;
                case 1:
                    return this.name;
                case 2:
                    if (status == StatusType.Alive) return "alive";
                    return "dead";
                case 3:
                    return this.serialNumber;
                case 4:
                    return this.MAC;

                default:
                    return "";

            }


        }
    }
}
