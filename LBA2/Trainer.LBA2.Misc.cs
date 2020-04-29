using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LBAMemoryModule;

namespace LBATrainer
{
    public partial class frmTrainer
    {
        private void LBA2Othr_chkMisc_CheckedChanged(object sender, EventArgs e)
        {
            //If nothing selected do nothing
            if (-1 == LBA2Othr_cboMisc.SelectedIndex) return;

            switch (LBA2Othr_cboMisc.SelectedIndex)
            {
                case 0:
                    LBA2Misc_toggleRain(LBA2Othr_chkMisc.Checked);
                    break;
                case 1:
                    LBA2Misc_frictionlessFerry(LBA2Othr_chkMisc.Checked);
                    break;
            }
        }

        private void LBA2Misc_toggleRain(bool rainOff)
        {
            if (rainOff)
            {
                memRoutines.WriteVal(LBA2_RAIN0, 4, 1);
                memRoutines.WriteVal(LBA2_RAIN1, 1, 1);
                memRoutines.WriteVal(LBA2_RAIN2, 2, 1);
            }
            else
            {
                memRoutines.WriteVal(LBA2_RAIN0, 0, 1);
                memRoutines.WriteVal(LBA2_RAIN1, 0, 1);
                memRoutines.WriteVal(LBA2_RAIN2, 0, 1);
            }            
        }

        private void LBA2Misc_frictionlessFerry(bool freeFerry)
        {
            if (freeFerry)
            {
                if (null == tsiLBA2) tsiLBA2 = new oTimerSetItems(oTimerSetItems.LBAVersion.Two);
                tsiLBA2.AddItem(LBA2_FERRY_TICKET, 1, 1);
                memRoutines.WriteVal(LBA2_SHOWN_FERRY_TICKET, 1, 1);
            }
            else
            {
                if (null == tsiLBA2) return;
                tsiLBA2.RemoveIfExists(LBA2_FERRY_TICKET);
                //tsiLBA2.RemoveIfExists(LBA2_SHOWN_FERRY_TICKET);
            }
        }

    }
}
