using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkSystemFinder.Models
{
    class Computer
    {
        string iP;
        string pcName;
        Computer.StatusType status = Computer.StatusType.Dead;
        string oS;
        string cPU;
        string rAM;
        string gPU;
        string hDD;
        string ssD;
        string serialNumber;
        string mAC;
        string motherboard;

        public Computer(string IP)
        {
            this.iP = IP;
            this.pcName = "?";
            this.cPU = "?";
            this.oS = "?";
            this.rAM = "?";
            this.gPU = "?";
            this.hDD = "0";
            this.ssD = "0";
            this.serialNumber = "?";
            this.mAC = "?";
            this.motherboard = "?";

        }

        public string IP { get => iP; set => iP = value; }
        public string PcName { get => pcName; set => pcName = value; }
        public StatusType Status { get => status; set => status = value; }
        public string OS { get => oS; set => oS = value; }
        public string CPU { get => cPU; set => cPU = value; }
        public string RAM { get => rAM; set => rAM = value; }
        public string GPU { get => gPU; set => gPU = value; }
        public string HDD { get => hDD; set => hDD = value; }
        public string SSD { get => ssD; set => ssD = value; }
        public string SerialNumber { get => serialNumber; set => serialNumber = value; }
        public string MAC { get => mAC; set => mAC = value; }
        public string Motherboard { get => motherboard; set => motherboard = value; }

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
                    return this.hDD;
                case 8:
                    return this.ssD;
                case 9:
                    return this.serialNumber;
                case 10:
                    return this.MAC;
                case 11:
                    return this.motherboard;
                default:
                    return "";

            }
                

        }
    }
}
