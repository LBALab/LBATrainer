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
        private HotKey LBA1Behaviour_hkD1Normal;
        private HotKey LBA1Behaviour_hkD2Athletic;
        private HotKey LBA1Behaviour_hkD3Aggressive;
        private HotKey LBA1Behaviour_hkD4Discreet;

        private void LBA1Behaviour_Load(object sender, EventArgs e, Options opt)
        {
            ;
        }


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

        private void tsmiLBA1BehaviourSwitchWith14_Click(object sender, EventArgs e)
        {
            if (tsmiLBA1BehaviourSwitchWith14.Checked)
                LBA1Behaviour_registerHotKeys();
            else
                LBA1Behaviour__unregisterHotkeys();
        }
        private void LBA1Behaviour_registerHotKeys()
        {
            LBA1Behaviour_hkD1Normal = registerHotKey(Keys.D1);
            LBA1Behaviour_hkD2Athletic = registerHotKey(Keys.D2);
            LBA1Behaviour_hkD3Aggressive = registerHotKey(Keys.D3);
            LBA1Behaviour_hkD4Discreet = registerHotKey(Keys.D4);
        }
        private void LBA1Behaviour__unregisterHotkeys()
        {
            try
            {
                unregisterHotKey(LBA1Behaviour_hkD1Normal);
                unregisterHotKey(LBA1Behaviour_hkD2Athletic);
                unregisterHotKey(LBA1Behaviour_hkD3Aggressive);
                unregisterHotKey(LBA1Behaviour_hkD4Discreet);
            }
            catch { };
        }
        private void LBA1Behaviour_processHotkey(Keys k)
        {
            if (Keys.D1 == k)
            {
                LBA1Behaviour.SetNormal();
            }
            if (Keys.D2 == k)
            {
                LBA1Behaviour.SetAthletic();
            }
            if (Keys.D3 == k)
            {
                LBA1Behaviour.SetAggressive();
            }
            if (Keys.D4 == k)
            {
                LBA1Behaviour.SetDiscreet();
            }

        }
    }
}
