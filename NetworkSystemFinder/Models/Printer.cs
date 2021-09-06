using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkSystemFinder.Models
{
    public class Printer: Machine
    {
        DateTime installDate = DateTime.MinValue;
        string serverName;
        public Printer(string IP)
        {
            this.iP = IP;
            this.status = StatusType.Alive;
            this.name = "?";
            this.serialNumber = "?";
            this.MAC = "?";
            this.ServerName = "?";
            this.caption = "?";
            this.caption2 = "?";
            installDate = DateTime.Now;
            
        }

        public new string IP { get => iP == null ? "Null": iP; set => iP = value; }
        public new string Name { get => name == null ? "Null" : name; set => name = value; }
        public new StatusType Status { get => status; set => status = value; }
        public new string SerialNumber { get => serialNumber == null ? "Null" : serialNumber; set => serialNumber = value; }
        public new string MAC { get => mAC == null ? "Null" : mAC; set => mAC = value; }
        public DateTime InstallDate { get => DateTime.Now; set => installDate = value; }
        public string ServerName { get => serverName == null ? "Null" : serverName; set => serverName = value; }
        public string Caption { get => caption == null ? "Null" : caption; set => caption = value; }
        public string Caption2 { get => caption2 == null ? "Null" : caption2; set => caption2 = value; }

        string caption;
        string caption2;

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
                    return this.serialNumber;
                case 4:
                    return this.MAC;

                default:
                    return "";

            }


        }

    }
}
