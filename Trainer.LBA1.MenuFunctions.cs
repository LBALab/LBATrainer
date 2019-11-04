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
            tsiLBA1 = itemToggle(tsiLBA1, 0xD554, 50, 1, oTimerSetItems.LBAVersion.One);
        }

        private void LBA1MaxMagicToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tsiLBA1 = itemToggle(tsiLBA1, 0xE22, 80, 1, oTimerSetItems.LBAVersion.One);
        }
        private void LBA1AutoZoomToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            memRoutines.WriteVal(1, 0xE0A, (ushort)(LBA1AutoZoomToolStripMenuItem1.Checked ? 0 : 1), 1);
        }
    }
}
