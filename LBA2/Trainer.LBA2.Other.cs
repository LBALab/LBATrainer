using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LBAMemoryModule;
using LBATrainer.objects;
using System.Windows.Forms;

namespace LBATrainer
{
    public partial class frmTrainer
    {
        Item[] skins;
        LBA2Quests LBA2Other_quests;

        const int LBA2WEAPONADDRESS = 0x3D693;
        private void LBA2Othr_Start()
        {
            LBA2Othr_populateSkins();
            LBA2Othr_populateQuests();
        }

        private void LBA2Othr_populateSkins()
        {
            LBA2Othr_cboSkins.Items.Clear();
            LBA2Othr_cboSkins.Items.AddRange(items.LBA2Skins);
        }
        
        private void LBA2Othr_populateQuests()
        {
            LBA2Other_quests = new LBA2Quests(getLBAFilesPath(2));
            LBA2Othr_cboQuest.Items.Clear();
            LBA2Othr_cboQuest.Items.AddRange(LBA2Other_quests.quests);
        }

        private void LBA2Othr_cboQuest_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (-1 == LBA2Othr_cboQuest.SelectedIndex)
            {
                LBA2Othr_cboSubquest.Items.Clear();
                return;
            }
            LBA2Othr_cboSubquest.Items.Clear();
            LBA2Othr_cboSubquest.Text = "";
            LBA2Othr_cboSubquest.Items.AddRange(LBA2Other_quests.quests[LBA2Othr_cboQuest.SelectedIndex].subquests);
        }

        private void LBA2Othr_cboSubquest_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (-1 == LBA2Othr_cboSubquest.SelectedIndex) return;
            int val = memRoutines.readVal
            (
                ((LBA2Quest)LBA2Othr_cboQuest.Items[LBA2Othr_cboQuest.SelectedIndex]).memoryOffset,
                ((LBA2Quest)LBA2Othr_cboQuest.Items[LBA2Othr_cboQuest.SelectedIndex]).size
            );
            LBA2Othr_chkSubquest.Checked = LBA2Othr_cboSubquest.SelectedIndex == val;
        }

        private void LBA2Othr_chkSubquest_CheckedChanged(object sender, EventArgs e)
        {
            if (-1 == LBA2Othr_cboSubquest.SelectedIndex) return;
            if(LBA2Othr_chkSubquest.Checked)
                memRoutines.WriteVal
                (
                    (int)((LBA2Quest)LBA2Othr_cboQuest.Items[LBA2Othr_cboQuest.SelectedIndex]).memoryOffset,
                    ((LBA2Quest.Subquest) LBA2Othr_cboSubquest.Items[LBA2Othr_cboSubquest.SelectedIndex]).value,
                    (byte)((LBA2Quest)LBA2Othr_cboQuest.Items[LBA2Othr_cboQuest.SelectedIndex]).size
                );
        }

        private void LBA2Othr_chkSkins_CheckedChanged(object sender, EventArgs e)
        {
            if (-1 == LBA2Othr_cboSkins.SelectedIndex) return;
            Item itm = (Item)LBA2Othr_cboSkins.SelectedItem;
            tsi.RemoveIfExists(itm.memoryOffset);
            if (LBA2Othr_chkSkin.Checked)
                tsi.AddItem(itm);
        }

        private void LBA2Othr_cboBehaviour_SelectedIndexChanged(object sender, EventArgs e)
        {
            LBA2Othr_chkBehaviourValue.Checked = LBA2Othr_cboBehaviour.SelectedIndex == memRoutines.readAddress(LBA_TWO, 0x57DF3, 1);
        }

        private void LBA2Othr_chkBehaviourValue_CheckedChanged(object sender, EventArgs e)
        {
            if (-1 != LBA2Othr_cboBehaviour.SelectedIndex && LBA2Othr_chkBehaviourValue.Checked)
                memRoutines.WriteVal(2, 0x57DF3, (ushort)LBA2Othr_cboBehaviour.SelectedIndex, 1);
        }

        private void LBA2Othr_CboSkins_SelectedIndexChanged(object sender, EventArgs e)
        {
            Item itm = (Item)LBA2Othr_cboSkins.SelectedItem;
            LBA2Othr_chkSkin.Checked = itm.maxVal == memRoutines.getVal(LBA_TWO, itm);
        }

        private void LBA2Othr_cboSkins_TextChanged(object sender, EventArgs e)
        {
            filterCBO(LBA2Othr_cboSkins, items.LBA2Skins);
        }

        private ushort LBA2Othr_getSelectedWeaponNumber()
        {
            return (ushort)getInt(LBA2Othr_cboWeapon.Text.Substring(0, 2));
        }
        private void LBA2Othr_cboWeapon_SelectedIndexChanged(object sender, EventArgs e)
        {
            LBA2Othr_chkWeapon.Checked = LBA2Othr_getSelectedWeaponNumber() == memRoutines.readAddress(2, LBA2WEAPONADDRESS, 1);
        }

        private void LBA2Othr_chkWeapon_CheckedChanged(object sender, EventArgs e)
        {
            if (LBA2Othr_chkWeapon.Checked) memRoutines.WriteVal(LBA2WEAPONADDRESS, LBA2Othr_getSelectedWeaponNumber(), 1);
        }

        private void LBA2Othr_chkMisc_CheckedChanged(object sender, EventArgs e)
        {
            //If nothing selected do nothing
            if (-1 == LBA2Othr_cboMisc.SelectedIndex) return;

            switch (LBA2Othr_cboMisc.SelectedIndex)
            {
                case 0: toggleRain(LBA2Othr_chkMisc.Checked);
                        break;
                case 1: frictionlessFerry(LBA2Othr_chkMisc.Checked);
                        break;
            }
        }

        private void toggleRain(bool rainOff)
        {
            if(rainOff)
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

        private void frictionlessFerry(bool freeFerry)
        {
            if (freeFerry)
            {
                if (null == tsiLBA2) tsiLBA2 = new oTimerSetItems(oTimerSetItems.LBAVersion.Two);
                tsiLBA2.AddItem(LBA2_FERRY_TICKET, 1, 1);
                //tsiLBA2.AddItem(AFTER_FERRY_TICKET, 1, 1);
                memRoutines.WriteVal(LBA2_SHOWN_FERRY_TICKET, 1, 1);
            }
            else
            {
                if (null == tsiLBA2) return;
                tsiLBA2.RemoveIfExists(LBA2_FERRY_TICKET);
                //tsiLBA2.RemoveIfExists(AFTER_FERRY_TICKET);
                tsiLBA2.RemoveIfExists(LBA2_SHOWN_FERRY_TICKET);
            }
        }
        /*
         const int SHOWN_FERRY_TICKET = 0x78743;
        const int AFTER_FERRY_TICKET = 0x57BAC;
        */
    }
}
