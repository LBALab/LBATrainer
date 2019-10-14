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
        Timer tmrGodMagic = null;
        private void RefreshToolStripMenuItem_Click(object sender, EventArgs e)
        {
            memRoutines = new mem();
            memRoutines.WriteVal(1, 0xE0A, (ushort)(enabledToolStripMenuItem.Checked ? 1 : 0), 1);
        }

        private void EnabledToolStripMenuItem_Click(object sender, EventArgs e)
        {
            memRoutines.WriteVal(1, 0xE0A, (ushort)(enabledToolStripMenuItem.Checked ? 0 : 1), 1);
            enabledToolStripMenuItem.Checked = !enabledToolStripMenuItem.Checked;
        }

        private Timer setUpTimer()
        {
            Timer t = new Timer();
            t.Interval = 100; //Tenth of second
            t.Tick += tmrGodMagic_Tick;
            t.Enabled = true;
            return t;
        }

        private void tmrGodMagic_Tick(object sender, EventArgs e)
        {
            if (mnuGodModeEnabled.Checked)
                memRoutines.WriteVal(LBA_ONE, items.Health, items.Health.maxVal);
            if (mnuMaxMagicEnabled.Checked)
                memRoutines.WriteVal(LBA_ONE, items.MagicPoints, (getInt(txtLBA1MagicLevel.Text) * 20).ToString());
        }

        private void MnuGodModeEnabled_Click(object sender, EventArgs e)
        {
            if (mnuGodModeEnabled.Checked)
            {
                mnuGodModeEnabled.Checked = false;
                if (!mnuGodModeEnabled.Checked && !mnuMaxMagicEnabled.Enabled)
                    tmrGodMagic = null;
            }
            else
            {
                mnuGodModeEnabled.Checked = true;
                if (null == tmrGodMagic)
                {
                    tmrGodMagic = setUpTimer();
                }
            }
        }

        private void MnuMaxMagicEnabled_Click(object sender, EventArgs e)
        {
            if (mnuMaxMagicEnabled.Checked)
            {
                mnuMaxMagicEnabled.Checked = false;
                if (!mnuGodModeEnabled.Checked && !mnuMaxMagicEnabled.Enabled)
                    tmrGodMagic = null;
            }
            else
            {
                mnuMaxMagicEnabled.Checked = true;
                if (null == tmrGodMagic)
                {
                    tmrGodMagic = setUpTimer();
                }
            }
        }


    }
}
