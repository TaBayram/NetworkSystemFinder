using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace NetworkSystemFinder.Models
{
    public abstract class Machine
    {
        protected string iP;
        protected string name;
        protected Machine.StatusType status = StatusType.Dead;
        protected string mAC;
        protected string serialNumber;

        [Browsable(false)]  public string IP { get => iP; set => iP = value; }
        [Browsable(false)]  public string Name { get => name; set => name = value; }
        [Browsable(false)]  public StatusType Status { get => status; set => status = value; }
        [Browsable(false)]  public string MAC { get => mAC; set => mAC = value; }
        [Browsable(false)]  public string SerialNumber { get => serialNumber; set => serialNumber = value; }


        public void SetIP()
        {
            string ip = iP.ToString().Replace(".", "");
            bool tryParse = int.TryParse(ip, out _);
            if (tryParse) return;

            IPHostEntry hostEntry = Dns.GetHostEntry(this.iP);
            foreach (IPAddress iP in hostEntry.AddressList)
            {
                ip = iP.ToString().Replace(".","");
                tryParse = int.TryParse(ip, out int _);
                if (tryParse)
                {
                    this.IP = iP.ToString();
                    break;
                }
            }
                
        }

        public enum StatusType
        {
            Alive = 0,
            Dead = 1,
        }

    }
}
