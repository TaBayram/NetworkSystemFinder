using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkSystemFinder.Helpers
{
    static class ModelNamer
    {
        public static string[] CPUManufacturers = new string[] { "Intel(R) Core(TM)", "AMD" };
        public static string[] CPUAbbreviation = new string[] { "Intel", "AMD" };

        public static string[] GPUManufacturers = new string[] { "Intel(R)", "AMD","NVIDIA"};
        

        public enum CPU : int
        {
            Intel = 0,
            Amd = 1,
        }

        public enum GPU : int
        {
            Intel = 0,
            Amd = 1,
            NVIDIA = 2,
        }


    }
}
