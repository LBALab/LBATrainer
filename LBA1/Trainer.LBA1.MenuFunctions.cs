using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
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

        //private HotKey LBA1DWD_hkF1Normal;
        //private HotKey LBA1DWD_hkF2Athletic;
        //private HotKey LBA1DWD_hkF3Aggressive;
        //private HotKey LBA1DWD_hkF4Discreet;
        private HotKey LBA1DWD_hkCapsLock;

        private void LBA1MenuFunctions_Load(object sender, EventArgs e, Options opt)
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

        #region Behaviour
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
        #endregion

        byte LBA1DWD_selectedBehaviour;
        byte LBA1DWD_currentAreacode;
        private void LBA1mnuDisableWallDamage_Click(object sender, EventArgs e)
        {
            if (LBA1Mnu_DisableWallDamage.Checked)
            {
                LBA1DWDDisableWallDamage();
                LBA1DWD_registerHotKeys();
                /*LBA1DWD_selectedBehaviour = (byte)memRoutines.readVal(LBA1_BEHAVIOUR, 1);
                memRoutines.WriteVal(LBA1_BEHAVIOUR, 0, 1);
                tgi.AddItem(LBA1DWD_Areacode, LBA1_AREACODE, 1);*/
            }
            else
            {
                LBA1DWDEnableWallDamage();
                LBA1DWD__unregisterHotkeys();
                /*tgi.RemoveIfExists(LBA1_BEHAVIOUR);
                memRoutines.WriteVal(LBA1_BEHAVIOUR, LBA1DWD_selectedBehaviour, 1);*/
            }
        }

        private void LBA1DWDEnableWallDamage()
        {
            memRoutines.WriteVal(LBA1_BEHAVIOUR, 1, 1); ;
        }

        private void LBA1DWDDisableWallDamage()
        {
            memRoutines.WriteVal(LBA1_BEHAVIOUR, 0, 1);
        }

       /* private void LBA1DWD_Areacode(ushort areacode)
        {
            //Called by oTimerGetItem - if areacode different re-set behaviour
            if (areacode != LBA1DWD_currentAreacode)
            {
                if(1 == memRoutines.readVal(LBA1_BEHAVIOUR, 1))
                    memRoutines.WriteVal(LBA1_BEHAVIOUR, LBA1DWD_selectedBehaviour, 1);
                Thread.Sleep(1000);
                memRoutines.WriteVal(LBA1_BEHAVIOUR, 0, 1);
                LBA1DWD_currentAreacode = (byte) areacode;
            }
        }*/

        private void LBA1DWD_registerHotKeys()
        {
            //LBA1DWDBehaviour_hkF1Normal = registerHotKey(Keys.F1);
            //LBA1DWDBehaviour_hkF2Athletic = registerHotKey(Keys.F2);
            //LBA1DWDBehaviour_hkF3Aggressive = registerHotKey(Keys.F3);
            //LBA1DWDBehaviour_hkF4Discreet = registerHotKey(Keys.F4);
            LBA1DWD_hkCapsLock = registerHotKey(Keys.CapsLock);
        }
        private void LBA1DWD__unregisterHotkeys()
        {
            try
            {
                unregisterHotKey(LBA1DWD_hkCapsLock);
                //unregisterHotKey(LBA1DWDBehaviour_hkF1Normal);
                //unregisterHotKey(LBA1DWDBehaviour_hkF2Athletic);
                //unregisterHotKey(LBA1DWDBehaviour_hkF3Aggressive);
                //unregisterHotKey(LBA1DWDBehaviour_hkF4Discreet);
            }
            catch { };
        }
        private void LBA1DWD_processHotkey(Keys k)
      {
            /*if (Keys.F2 == k)
            {
                Thread.Sleep(500);
                LBA1DWD_selectedBehaviour = (byte)memRoutines.readVal(LBA1_BEHAVIOUR, 1);
                memRoutines.WriteVal(LBA1_BEHAVIOUR, 0, 1);
            }*/

          if(Keys.CapsLock== k)
            {
                if (1 == memRoutines.readVal(LBA1_BEHAVIOUR, 1))
                    LBA1DWDDisableWallDamage();
                else
                    LBA1DWDEnableWallDamage();
            }
        }
    }

}
