using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace NetworkSystemFinder.Helpers
{
    class MacPairer
    {
        static List<MacIpPair> macIPPairs;
        public string getMacByIp(string ip)
        {
            if(macIPPairs == null)
                macIPPairs = GetAllMacAddressesAndIppairs();
            int index = macIPPairs.FindIndex(x => x.IpAddress == ip);
            if (index >= 0)
            {
                return macIPPairs[index].MacAddress.ToUpper();
            }
            else
            {
                return ".";
            }
        }

        public List<MacIpPair> GetAllMacAddressesAndIppairs()
        {
            List<MacIpPair> mip = new List<MacIpPair>();
            System.Diagnostics.Process pProcess = new System.Diagnostics.Process();
            pProcess.StartInfo.FileName = "arp";
            pProcess.StartInfo.Arguments = "-a ";
            pProcess.StartInfo.UseShellExecute = false;
            pProcess.StartInfo.RedirectStandardOutput = true;
            pProcess.StartInfo.CreateNoWindow = true;
            pProcess.Start();
            string cmdOutput = pProcess.StandardOutput.ReadToEnd();
            string pattern = @"(?<ip>([0-9]{1,3}\.?){4})\s*(?<mac>([a-f0-9]{2}-?){6})";

            foreach (Match m in Regex.Matches(cmdOutput, pattern, RegexOptions.IgnoreCase))
            {
                mip.Add(new MacIpPair()
                {
                    MacAddress = m.Groups["mac"].Value,
                    IpAddress = m.Groups["ip"].Value
                });
            }

            return mip;
        }
        public struct MacIpPair
        {
            public string MacAddress;
            public string IpAddress;
        }
    }
}
