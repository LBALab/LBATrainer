using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LBATrainer
{
    class oTimerSetItems
    {
        private bool healthEnabled = false;
        private bool magicEnabled = false;
        byte health;
        byte magicLevel;
        byte LBAVer;
        Timer tmr = new Timer();
        mem memory = new mem();
        public enum LBAVersion
        {
            One = 0,
            Two = 1
        }

        public enum LBAHealthOffset
        {
            LBA1 = 0xE10,
            LBA2 = 0xE20
        }
        public oTimerSetItems(LBAVersion lbaVer)
        {
            if (lbaVer == LBAVersion.One) ;
        }

        public oTimerSetItems(LBAVersion lbaVer, int i )
        {
            LBAVer = (byte) lbaVer;
        }

        public void UpdateMagicLevel()
        {
            //magicLevel = mL;
        }
        public void enableHealth()
        {

        }
    }
}
