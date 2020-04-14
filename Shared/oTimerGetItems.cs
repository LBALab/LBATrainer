using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LBAMemoryModule;

namespace LBATrainer
{
    class oTimerGetItems
    {
        byte LBAVer;
        Timer tmr = new Timer();
        Mem memory = new Mem();
        List<ActionItem> itemList = new List<ActionItem>();
        private int interval = 50;

        //Func x;
        //Func<string, string> myFunction;
        public enum LBAVersion { One = 1, Two = 2 }

        public oTimerGetItems()
        {
            objectLoad(interval);
        }

        public oTimerGetItems(int interval)
        {
            this.interval = interval;
            objectLoad(interval);
        }

        private void objectLoad(int interval)
        {
            tmr.Interval = interval;
            tmr.Tick += timer_Tick;
        }

        public void AddItem(Action<ushort> act, Item item)
        {
            itemAdded(new ActionItem(act, item));
        }

        public void AddItem(Action<ushort> act, uint memoryOffset, ushort size)
        {

            itemAdded(new ActionItem(act, new Item(memoryOffset, size)));
        }

        public void AddItem(ActionItem ai)
        {
            itemAdded(ai);
        }

        private void itemAdded(ActionItem ai)
        {
            //Handle event, start timer if not already active i.e. if first item added
            itemList.Add(ai);
            if (1 == itemList.Count())
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
            for (int i = 0; i < itemList.Count; i++)
            {
                ushort val = (ushort)memory.readVal(itemList[i].item.memoryOffset, itemList[i].item.size);
                itemList[i].act(val);
            }
        }

        public bool IsEmpty()
        {
            return 0 == itemList.Count;
        }

        public bool Contains(uint memoryOffSet)
        {
            for (int i = 0; i < itemList.Count; i++)
                if (itemList[i].item.memoryOffset == memoryOffSet)
                    return true;
            return false;
        }

        /* Returns true if the item is removed, else false */
        public bool RemoveIfExists(uint memoryOffset)
        {
            bool removed = false;
            for (int i = 0; i < itemList.Count; i++)
                if (itemList[i].item.memoryOffset == memoryOffset)
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
            for (int i = 0; i < itemList.Count; i++)
                if (itemList[i].item.memoryOffset == memoryOffset)
                {
                    itemList[i].item.maxVal = newVal;
                    return true;
                }
            return false;
        }

        public class ActionItem
        {
            public Item item;
            public Action<ushort> act;
            public ActionItem() { }
            public ActionItem(Action<ushort> act, Item item)
            {
                this.item = item;
                this.act = act;
            }
        }
    }
}
