using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LBATrainer.objects
{
    class LBA2Quest
    {
        public string name;
        public uint memoryOffset;
        public ushort size; //Number of bytes needed to store value
        public Subquest[] subquests;

        public override string ToString()
        {
            return name;
        }

        public class Subquest
        {
            public string name;
            public ushort value;

            public override string ToString()
            {
                return name;
            }
        }
    }
}
