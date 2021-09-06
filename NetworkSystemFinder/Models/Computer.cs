using NetworkSystemFinder.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkSystemFinder.Models
{
    class Computer:Machine
    {
        string oS;
        string cPU;
        int rAM;
        string rAMType;
        string gPU;
        int hDD;
        int ssD;
        string motherboard;

        public Computer(string IP)
        {
            this.iP = IP;
            this.name = "?";
            this.cPU = "?";
            this.oS = "?";
            this.rAM = 0;
            this.rAMType = "?";
            this.gPU = "?";
            this.hDD = 0;
            this.ssD = 0;
            this.serialNumber = "?";
            this.mAC = "?";
            this.motherboard = "?";

        }

        public new string IP { get => iP; set => iP = value; }
        public new string Name { get => name; set => name = value; }
        public new Machine.StatusType Status { get => status; set => status = value; }
        public string OS { get => oS; set => oS = value; }
        public string CPU { get => cPU; set => cPU = value; }
        public int RAM { get => rAM; set => rAM = value; }
        public string RAMType { get => rAMType; set => rAMType = value; }
        public string GPU { get => gPU; set => gPU = value; }
        public int HDD { get => hDD; set => hDD = value; }
        public int SSD { get => ssD; set => ssD = value; }
        public new string SerialNumber { get => serialNumber; set => serialNumber = value; }
        public new string MAC { get => mAC; set => mAC = value; }
        public string Motherboard { get => motherboard; set => motherboard = value; }


        public override string ColumnProperty(int index)
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
                    return this.oS;
                case 4:
                    return this.cPU;
                case 5:
                    return this.rAM.ToString();
                case 6:
                    return this.gPU;
                case 7:
                    return this.hDD.ToString();
                case 8:
                    return this.ssD.ToString();
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

        public string[] SplitName()
        {
            string[] name = new string[2];
            string wholeName = this.cPU;

            int numberCounter = 0;
            int index = 0;
            bool hasFound = false;
            for(int i = 0; i < wholeName.Length; i++)
            {
                char c = wholeName[i];
                int outInt = 0;
                bool isInt = int.TryParse(""+c,out outInt);
                if (isInt) 
                {
                    if(numberCounter == 0)
                    {
                        index = i;
                    }
                    numberCounter++;
                }
                else numberCounter = 0;
                if(numberCounter >= 3)
                {
                    hasFound = true;
                    index--;
                    break;
                }
            }

            if (hasFound)
            {
                while (wholeName[index] != ' ' && wholeName[index] != '-') index--;
                name[0] = wholeName.Substring(0, index);
                name[1] = wholeName.Substring(index+1);
            }

            return name;
        }

    }
}
