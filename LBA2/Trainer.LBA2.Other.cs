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
        Item[] skins;
        const int LBA2WEAPONADDRESS = 0x3D693;
        private void LBA2Othr_Start()
        {
            LBA2Othr_populateSkins();
        }

        private void LBA2Othr_populateSkins()
        {
            LBA2Othr_cboSkins.Items.Clear();
            LBA2Othr_cboSkins.Items.AddRange(items.LBA2Skins);
        }
        private oTimerSetItems otisSkin;
        //        private oTimerSetItems itemToggle(oTimerSetItems tsi, uint offset, ushort val, byte size, oTimerSetItems.LBAVersion LBAVer)
        private void LBA2Othr_chkSkins_CheckedChanged(object sender, EventArgs e)
        {
            if (-1 == LBA2Othr_cboSkins.SelectedIndex) return;
            Item itm = (Item)LBA2Othr_cboSkins.SelectedItem;
            if (LBA2Othr_chkSkin.Checked)
            {
                tsi.RemoveIfExists(itm.memoryOffset);
                tsi.AddItem(itm);
            }
            else
                tsi.RemoveIfExists(itm.memoryOffset);
            /*memRoutines.WriteVal(LBA_ONE, (Item)LBA1Othr_cboQuest.SelectedItem, val);

            uint outfitAddress = 0x57F51;*/
            //lblOtherSkin.Text = counter.ToString();
            //MessageBox.Show("Counter: " + counter);
            //memRoutines.WriteVal((int)outfitAddress, (ushort)counter++, 2);
            //tsi = itemToggle(tsi, outfitAddress, LBA2Othr_cboSkins.SelectedItem., 2, oTimerSetItems.LBAVersion.Two);
           
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

        private ushort getSelectedWeaponNumber()
        {
            return (ushort)getInt(LBA2Othr_Weapon.Text.Substring(0, 2));
        }
        private void LBA2Othr_cboWeapon_SelectedIndexChanged(object sender, EventArgs e)
        {
            LBA2Othr_chkWeapon.Checked = getSelectedWeaponNumber() == memRoutines.readAddress(2, LBA2WEAPONADDRESS, 1);
        }

        private void LBA2Othr_chkWeapon_CheckedChanged(object sender, EventArgs e)
        {
            if (LBA2Othr_chkWeapon.Checked) memRoutines.WriteVal(LBA2WEAPONADDRESS, getSelectedWeaponNumber(), 1);
        }
    }
}
