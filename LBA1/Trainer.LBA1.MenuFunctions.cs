using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LBATrainer
{
    public partial class frmTrainer
    {
        oTimerSetItems tsiLBA1;

        private void LBA1GodModeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tsiLBA1 = itemToggle(tsiLBA1, LBA1_HEALTH, 50, 1, oTimerSetItems.LBAVersion.One);
        }
        private void LBA1MaxMagicToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tsiLBA1 = itemToggle(tsiLBA1, LBA1_MAGICPOINTS, 80, 1, oTimerSetItems.LBAVersion.One);
        }
        private void LBA1AutoZoomToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            byte newVal = (byte)(1 == memRoutines.readVal(LBA1_AUTOZOOM, 1) ? 0 : 1);
            memRoutines.WriteVal((int)LBA1_AUTOZOOM, newVal, 1);
            LBA1AutoZoomToolStripMenuItem1.Checked = 1 == newVal;
        }

        private void LBA1ModeSwitchWith14ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }


    }
}
