using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkSystemFinder.Models
{
    abstract class Machine
    {
        string iP;
        string pcName;
        Machine.StatusType status = StatusType.Dead;
        string mAC;
        string serialNumber;

        public string IP { get => iP; set => iP = value; }
        public string PcName { get => pcName; set => pcName = value; }
        public StatusType Status { get => status; set => status = value; }
        public string MAC { get => mAC; set => mAC = value; }
        public string SerialNumber { get => serialNumber; set => serialNumber = value; }

        public abstract string ColumnProperty(int index);

        public enum StatusType
        {
            Alive = 0,
            Dead = 1,
        }

    }
}
