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
        oTimerSetItems tsiLBA2;

        private void LBA2GodModeToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            tsiLBA2 = itemToggle(tsiLBA2, LBA2_HEALTH, 255, 1, oTimerSetItems.LBAVersion.Two);
        }

        private void LBA2InfiniteMagicToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tsiLBA2 = itemToggle(tsiLBA2, LBA2_MAGICPOINTS, (ushort) (memRoutines.readVal(LBA2_MAGICLEVEL, 1)*20),1, oTimerSetItems.LBAVersion.Two);
        }

    }
}
