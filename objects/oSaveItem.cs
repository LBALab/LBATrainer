using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LBATrainer
{
    class SaveItem
    {
        public string description;
        public byte ID;
        public uint memoryOffsetStart;
        public uint memoryOffsetEnd;
        public uint fileOffsetStart;
        public uint fileOffsetEnd;
        public byte numOfBytes; //This should be either 1 or 2, or 0x00 for unknown i.e. filename
        public byte fixedValue;
        public byte[] data;

        public override string ToString()
        {
            return description;
        }
    }
}
