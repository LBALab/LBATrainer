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
        private HotKey LBA1MF_hkNormal;
        private HotKey LBA1MF_hkAthletic;
        private HotKey LBA1MF_hkAggressive;
        private HotKey LBA1MF_hkDiscreet;
        Keys LBA1MF_kNormal = Keys.D1;
        Keys LBA1MF_kAthletic = Keys.D2;
        Keys LBA1MF_kAggressive = Keys.D3;
        Keys LBA1MF_kDiscreet = Keys.D4;

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
            ushort ZoomOffset = 0xE0A;
            byte newVal = (byte)(1 == memRoutines.readVal(ZoomOffset, 1) ? 0 : 1);
            memRoutines.WriteVal(1, ZoomOffset, newVal, 1);
            LBA1AutoZoomToolStripMenuItem1.Checked = 1 == newVal;
        }
/*

        private void LBA1FastBehaviourSwitchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(LBA1FastBehaviourSwitchToolStripMenuItem.Checked)
            {
                registerHotKey(LBA1MF_kNormal);
                registerHotKey(LBA1MF_kAthletic);
                registerHotKey(LBA1MF_kAggressive);
                registerHotKey(LBA1MF_kDiscreet);
            }
            else
            {
                unregisterHotKey(LBA1MF_hkNormal);
                unregisterHotKey(LBA1MF_hkAthletic);
                unregisterHotKey(LBA1MF_hkAggressive);
                unregisterHotKey(LBA1MF_hkDiscreet);
            }
        }

        private void LBA1MFsetBehaviour(ushort b)
        {
            memRoutines.WriteVal(0xE08, b, 1);
        }
        private void LBA1Menu_processHotkey(Keys k)
        {
            if (LBA1MF_kNormal == k)
            {
                LBA1MFsetBehaviour(0);
                return;
            }
            if (LBA1MF_kAthletic == k)
            {
                LBA1MFsetBehaviour(1);
                return;
            }
            if (LBA1MF_kAggressive == k)
            {
                LBA1MFsetBehaviour(2);
                return;
            }
            if (LBA1MF_kDiscreet == k)
            {
                LBA1MFsetBehaviour(3);
                return;
            }
        }*/
    }
}
