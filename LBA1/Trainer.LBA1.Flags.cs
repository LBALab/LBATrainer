using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.Design;

namespace LBATrainer
{
    public partial class frmTrainer
    {
        //D500 - Twinsen Armour

        bool LBA1Flag_Enabled;

        private void LBA1Flags_Load(object sender, EventArgs e, Options opt)
        {
            LBA1Flags_Scan();
        }

        private void LBA1Flags_Scan()
        {
            LBA1Flag_Enabled = false; //Used to stop the checkedchange events having an effect when we're toggling them programmatically
            LBA1Flags_SetByte1Flags();
            LBA1Flags_SetByte2Flags();
            LBA1Flag_chkArmour.Checked = 51 == (byte)memRoutines.readVal(LBA1_ARMOUR, 1);
            LBA1Flag_Enabled = true;
        }

        private void LBA1Flags_SetByte1Flags()
        {
            byte val = (byte)memRoutines.readVal(LBA1_FLAGBYTE1, 1);
            LBA1Flag_chkObjCol.Checked = 1 == (1 & val);
            LBA1Flag_chkBrkCol.Checked = 2 == (2 & val);
            LBA1Flag_chkDetectZone.Checked = 4 == (4 & val);
            LBA1Flag_chkUsesClipping.Checked = 8 == (8 & val);
            LBA1Flag_chkCanBePushed.Checked = 16 == (16 & val);
            LBA1Flag_chkLowCol.Checked = 32 == (32 & val);
            LBA1Flag_chkTestFloor.Checked = 64 == (64 & val);
        }
        private void LBA1Flags_SetByte2Flags()
        {
            byte val = (byte)memRoutines.readVal(LBA1_FLAGBYTE2, 1);
            LBA1Flag_chkInvisible.Checked = 2 == (2 & val);
            LBA1Flag_chkIsSprite.Checked = 4 == (4 & val);
            LBA1Flag_chkGravity.Checked = 8 == (8 & val);
            LBA1Flag_chkShadow.Checked = 16 == (16 & val);
            LBA1Flag_chkBkgrnded.Checked = 32 == (32 & val);
            LBA1Flag_chkCarrier.Checked = 64 == (64 & val);
            LBA1Flag_chkMiniZV.Checked = 128 == (128 & val);
        }

        #region FlagByteOne
        private void LBA1Flag_chkObjCol_CheckedChanged(object sender, EventArgs e)
        {
            LBA1Flag_toggleBit(LBA1_FLAGBYTE1, 0);
        }
        private void LBA1Flag_chkBrkCol_CheckedChanged(object sender, EventArgs e)
        {
            LBA1Flag_toggleBit(LBA1_FLAGBYTE1, 1);
        }
        private void LBA1Flag_chkDetectZone_CheckedChanged(object sender, EventArgs e)
        {
            LBA1Flag_toggleBit(LBA1_FLAGBYTE1, 2);
        }
        private void LBA1Flag_chkUsesClipping_CheckedChanged(object sender, EventArgs e)
        {
            LBA1Flag_toggleBit(LBA1_FLAGBYTE1, 3);
        }
        private void LBA1Flag_chkCanBePushed_CheckedChanged(object sender, EventArgs e)
        {
            LBA1Flag_toggleBit(LBA1_FLAGBYTE1, 4);
        }
        private void LBA1Flag_chkLowCol_CheckedChanged(object sender, EventArgs e)
        {
            LBA1Flag_toggleBit(LBA1_FLAGBYTE1, 5);
        }
        private void LBA1Flag_chkTestFloor_CheckedChanged(object sender, EventArgs e)
        {
            LBA1Flag_toggleBit(LBA1_FLAGBYTE1, 6);
        }
        #endregion

        #region FlagByteTwo        
        private void LBA1Flag_chkInvisible_CheckedChanged(object sender, EventArgs e)
        {
            LBA1Flag_toggleBit(LBA1_FLAGBYTE2, 1);
        }
        private void LBA1Flag_chkIsSprite_CheckedChanged(object sender, EventArgs e)
        {
            LBA1Flag_toggleBit(LBA1_FLAGBYTE2, 2);
        }
        private void LBA1Flag_chkGravity_CheckedChanged(object sender, EventArgs e)
        {
                LBA1Flag_toggleBit(LBA1_FLAGBYTE2, 3);
        }
        private void LBA1Flag_chkShadow_CheckedChanged(object sender, EventArgs e)
        {
            LBA1Flag_toggleBit(LBA1_FLAGBYTE2, 4);
        }
        private void LBA1Flag_chkBkgrnded_CheckedChanged(object sender, EventArgs e)
        {
            LBA1Flag_toggleBit(LBA1_FLAGBYTE2, 5);
        }
        private void LBA1Flag_chkCarrier_CheckedChanged(object sender, EventArgs e)
        {
            LBA1Flag_toggleBit(LBA1_FLAGBYTE2, 6);
        }
        private void LBA1Flag_chkMiniZV_CheckedChanged(object sender, EventArgs e)
        {
            LBA1Flag_toggleBit(LBA1_FLAGBYTE2, 7);
        }
        #endregion
        private void LBA1Flag_chkArmour_CheckedChanged(object sender, EventArgs e)
        {
            if (!LBA1Flag_Enabled) return;
            if (LBA1Flag_chkArmour.Checked)
                tsi.AddItem(LBA1_ARMOUR, 51, 1);
            else
            {
                tsi.RemoveIfExists(LBA1_ARMOUR);
                memRoutines.WriteVal(LBA1_ARMOUR, 1, 1);
            }
        }
        private void LBA1Flag_toggleBit(uint offset, byte bitNumber)
        {
            if (!LBA1Flag_Enabled) return;

            byte data = (byte) memRoutines.readVal(offset, 1);
            data ^= (byte)Math.Pow(2, bitNumber);

            if (tsi.Contains(offset))
                tsi.UpdateItem(offset, data);
            else
                tsi.AddItem(offset, data, 1);           
        }


        private void LBA1Flags_restoreDefault_Click(object sender, EventArgs e)
        {
            //Disable armour - this triggers the checked changed event if applicable
            LBA1Flag_chkArmour.Checked = false;
            tsi.RemoveIfExists(LBA1_FLAGBYTE1);
            memRoutines.WriteVal(LBA1_FLAGBYTE1, 71, 1);
            tsi.RemoveIfExists(LBA1_FLAGBYTE2);
            memRoutines.WriteVal(LBA1_FLAGBYTE2, 8, 1);
            LBA1Flags_Scan();
        }
    }
}
