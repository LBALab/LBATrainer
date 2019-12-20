using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LBAMemoryModule;

namespace LBATrainer
{
    class oTimerSetItems
    {
        byte LBAVer;
        Timer tmr = new Timer();
        Mem memory = new Mem();
        List<Item> itemList = new List<Item>();
        private int interval = 50;
        public enum LBAVersion {One = 1, Two = 2 }

        public oTimerSetItems(LBAVersion lbaVer)
        {
            objectLoad(lbaVer, interval);
            //tmr.Interval = 50;
            //tmr.Tick += timer_Tick;
        }

        public oTimerSetItems(LBAVersion lbaver, int interval)
        {
            this.interval = interval;
            objectLoad(lbaver, interval);
        }

        private void objectLoad(LBAVersion lbaVer, int interval)
        {
            LBAVer = (byte)lbaVer;
            tmr.Interval = interval;
            tmr.Tick += timer_Tick;
        }

        public void AddItem(Item item)
        {
            itemAdded(item);
        }

        public void AddItem(uint memoryOffset, ushort maxVal, ushort size )
        {
            Item item = new Item();
            item.lbaVersion = LBAVer;
            item.maxVal = maxVal;
            item.minVal = maxVal;
            item.memoryOffset = memoryOffset;
            item.name = memoryOffset.ToString();
            item.size = size;
            item.type = 1;
            itemAdded(item);
        }

        private void itemAdded(Item item)
        {
            //Handle event, start timer if not already active i.e. if first item added
            itemList.Add(item);
            if(1 == itemList.Count())
            {
                StartTimer();
            }
        }

        private void StartTimer()
        {
            tmr.Enabled = true;
        }
        private void StopTimer()
        {
            tmr.Enabled = false;
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            for(int i = 0; i < itemList.Count;i++)
                memory.WriteVal(itemList[i].lbaVersion, (LBAMemoryModule.Item) itemList[i], itemList[i].maxVal);
        }
        
        public bool IsEmpty()
        {
            return 0 == itemList.Count;
        }

        public bool Contains(uint memoryOffSet)
        {
            for (int i = 0; i < itemList.Count; i++)
                if (itemList[i].memoryOffset == memoryOffSet)
                    return true;
            return false;
        }

        /* Returns true if the item is removed, else false */
        public bool RemoveIfExists(uint memoryOffset)
        {
            bool removed = false;
            for (int i = 0; i < itemList.Count; i++)
                if (itemList[i].memoryOffset == memoryOffset)
                {
                    itemList.RemoveAt(i);
                    removed = true;
                    break;
                }
            if (0 == itemList.Count) StopTimer();
            return removed;
        }

        public bool UpdateItem(uint memoryOffset, ushort newVal)
        {
            for(int i = 0; i < itemList.Count;i++)
                if(itemList[i].memoryOffset == memoryOffset)
                {
                    itemList[i].maxVal = newVal;
                    return true;
                }
            return false;
        }
    }
}
