using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkSystemFinder.Models.Parts
{
    class Storage
    {
        string iD;
        string model;
        string name;
        Type type;
        int capacity;

        enum Type
        {
            HDD = 0,
            SSD = 1
        }
    }
}
