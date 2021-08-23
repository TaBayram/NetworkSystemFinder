using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkSystemFinder.Models
{
    class Machine
    {
        string iP;
        string pcName;
        Machine.StatusType status = Machine.StatusType.Dead;
        string oS;
        string cPU;
        string rAM;
        string gPU;
        string storage;
        string serialNumber;
        string mAC;

        public Machine(string IP)
        {
            this.iP = IP;
            this.pcName = "?";
            this.cPU = "?";
            this.oS = "?";
            this.rAM = "?";
            this.gPU = "?";
            this.storage = "?";
            this.serialNumber = "?";
            this.MAC = "?";

        }

        public string IP { get => iP; set => iP = value; }
        public string PcName { get => pcName; set => pcName = value; }
        public StatusType Status { get => status; set => status = value; }
        public string OS { get => oS; set => oS = value; }
        public string CPU { get => cPU; set => cPU = value; }
        public string RAM { get => rAM; set => rAM = value; }
        public string GPU { get => gPU; set => gPU = value; }
        public string Storage { get => storage; set => storage = value; }
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
                    return this.pcName;
                case 2:
                    if (status == StatusType.Alive) return "alive";
                    return "dead";
                case 3:
                    return this.oS;
                case 4:
                    return this.cPU;
                case 5:
                    return this.rAM;
                case 6:
                    return this.gPU;
                case 7:
                    return this.storage;
                case 8:
                    return this.serialNumber;
                case 9:
                    return this.MAC;

                default:
                    return "";

            }
                

        }
    }
}
