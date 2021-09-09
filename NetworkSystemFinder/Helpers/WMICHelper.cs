using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;

namespace NetworkSystemFinder.Helpers
{
    static class WMICHelper
    {
        static string pathCMIV2 = "\\root\\CIMV2";
        static string pathSTORAGE = "\\root\\Microsoft\\Windows\\Storage";

        static ObjectQuery queryAccount = new ObjectQuery("SELECT * FROM Win32_UserAccount");
        static ObjectQuery queryCPU = new ObjectQuery("SELECT * FROM Win32_Processor");
        static ObjectQuery queryGPU = new ObjectQuery("SELECT * FROM Win32_VideoController");
        static ObjectQuery queryOS = new ObjectQuery("SELECT * FROM Win32_OperatingSystem");
        static ObjectQuery queryRAM = new ObjectQuery("SELECT * FROM Win32_PhysicalMemory");
        static ObjectQuery queryBIOS = new ObjectQuery("SELECT * FROM Win32_BIOS");
        static ObjectQuery queryStorage = new ObjectQuery("SELECT * FROM Win32_DiskDrive");
        static ObjectQuery queryStorageNew = new ObjectQuery("SELECT * FROM MSFT_PhysicalDisk");
        static ObjectQuery queryNetwork = new ObjectQuery("SELECT * FROM Win32_NetworkAdapter");
        static ObjectQuery queryMotherboard = new ObjectQuery("SELECT * FROM Win32_BaseBoard");
        static ObjectQuery queryComputerSystem = new ObjectQuery("SELECT * FROM Win32_ComputerSystem");

        static ObjectQuery queryTCPIPPrinter = new ObjectQuery("SELECT * FROM Win32_TCPIPPrinterPort");
        static ObjectQuery queryPrinter = new ObjectQuery("SELECT * FROM Win32_Printer");

        public static ObjectQuery QueryAccount { get => queryAccount; set => queryAccount = value; }
        public static ObjectQuery QueryCPU { get => queryCPU; set => queryCPU = value; }
        public static ObjectQuery QueryGPU { get => queryGPU; set => queryGPU = value; }
        public static ObjectQuery QueryOS { get => queryOS; set => queryOS = value; }
        public static ObjectQuery QueryRAM { get => queryRAM; set => queryRAM = value; }
        public static ObjectQuery QueryBIOS { get => queryBIOS; set => queryBIOS = value; }
        public static ObjectQuery QueryStorage { get => queryStorage; set => queryStorage = value; }
        public static ObjectQuery QueryStorageNew { get => queryStorageNew; set => queryStorageNew = value; }
        public static ObjectQuery QueryNetwork { get => queryNetwork; set => queryNetwork = value; }
        public static ObjectQuery QueryMotherboard { get => queryMotherboard; set => queryMotherboard = value; }
        public static ObjectQuery QueryTCPIPPrinter { get => queryTCPIPPrinter; set => queryTCPIPPrinter = value; }
        public static ObjectQuery QueryPrinter { get => queryPrinter; set => queryPrinter = value; }
        public static string PathSTORAGE { get => pathSTORAGE; set => pathSTORAGE = value; }
        public static string PathCMIV2 { get => pathCMIV2; set => pathCMIV2 = value; }
        public static ObjectQuery QueryComputerSystem { get => queryComputerSystem; set => queryComputerSystem = value; }
    }
}
