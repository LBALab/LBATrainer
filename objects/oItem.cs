using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LBAMemoryModule;

namespace LBATrainer
{
    class Item
    {
        public const ushort TYPE_BITFLAG = 0;
        public const ushort TYPE_VALUE = 1;
        public string name;
        public uint memoryOffset;
        public ushort maxVal;
        public ushort minVal;
        public ushort size; //Number of bytes needed to store value
        public ushort type; 
        public ushort lbaVersion; //1 for LBA1, or 2 for LBA2

        public override string ToString()
        {
            return name;
        }
    }
}
