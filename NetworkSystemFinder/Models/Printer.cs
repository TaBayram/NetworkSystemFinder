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
            this.name = "?";
            this.serialNumber = "?";
            this.MAC = "?";
            this.ServerName = "?";
        }

        public new string IP { get => iP; set => iP = value; }
        public new string Name { get => name; set => name = value; }
        public new StatusType Status { get => status; set => status = value; }
        public new string SerialNumber { get => serialNumber; set => serialNumber = value; }
        public new string MAC { get => mAC; set => mAC = value; }
        public DateTime InstallDate { get => installDate; set => installDate = value; }
        public string ServerName { get => serverName; set => serverName = value; }

        string caption;

        public void SetCaption(string caption)
        {
            this.caption = caption;
        }
        public string GetCaption()
        {
            return this.caption;
        }

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
