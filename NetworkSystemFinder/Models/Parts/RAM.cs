using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkSystemFinder.Models.Parts
{
    public class RAM
    {
        string type;
        int capacity;
        int speed;
        public RAM()
        {
            type = "Unkown";
            capacity = 0;
            speed = 0;
        }

        public string Type { get => type; set => type = value; }
        public int Capacity { get => capacity; set => capacity = value; }
        public int Speed { get => speed; set => speed = value; }
    }
}
