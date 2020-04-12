using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LBAMemoryModule;
using System.Windows.Forms;

namespace LBATrainer
{
    public partial class frmTrainer
    {
        private void LBA1Othr_Start()
        {
            LBA1Othr_populateQuest();
            LBA1Othr_populateMovies();
            LBA1Othr_populateInventoryUsed();
            LBA1Othr_populateHolomap();
        }
        #region Skin
        private void LBA1Othr_cboLBA1Skin_SelectedIndexChanged(object sender, EventArgs e)
        {
            //int val = memRoutines.readAddress(LBA_ONE, 0xD4EC, 1);
            LBA1Othr_chkSkinValue.Checked = LBA1Othr_cboLBA1Skin.SelectedIndex == memRoutines.readAddress(LBA_ONE, LBA1_SKIN, 1);
        }

        private void LBA1Othr_chkSkinValue_CheckedChanged(object sender, EventArgs e)
        {
            if (-1 != LBA1Othr_cboLBA1Skin.SelectedIndex && LBA1Othr_chkSkinValue.Checked) 
                memRoutines.WriteVal(1, (int)LBA1_SKIN, (ushort)LBA1Othr_cboLBA1Skin.SelectedIndex,1);
        }
        #endregion
        #region Weapon
        private void LBA1Othr_cboLBA1Weapon_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(-1 != LBA1Othr_cboWeapon.SelectedIndex)
                LBA1Othr_chkWeaponValue.Checked = LBA1Othr_cboWeapon.SelectedIndex == memRoutines.readAddress(LBA_ONE, LBA1_SELECTEDWEAPON, 1);
        }

        private void LBA1Othr_chkWeaponValue_CheckedChanged(object sender, EventArgs e)
        {
            if (-1 != LBA1Othr_cboWeapon.SelectedIndex && LBA1Othr_chkWeaponValue.Checked)
                memRoutines.WriteVal(1,(int)LBA1_SELECTEDWEAPON, (ushort)LBA1Othr_cboWeapon.SelectedIndex, 1);
        }
        #endregion
        #region Quest
        private void LBA1Othr_populateQuest()
        {
            LBA1Othr_cboQuest.Items.Clear();
            LBA1Othr_cboQuest.Items.AddRange(items.Quest);
        }
        private void LBA1Othr_ChkQuestValue_CheckedChanged(object sender, EventArgs e)
        {
            ushort val;
            if (LBA1Othr_chkQuestValue.Checked)
                val = 1;
            else
                val = 0;
            if (-1 == LBA1Othr_cboQuest.SelectedIndex) return;
            memRoutines.WriteVal(LBA_ONE, (Item)LBA1Othr_cboQuest.SelectedItem, val);
        }
        private void LBA1Othr_CboQuest_SelectedIndexChanged(object sender, EventArgs e)
        {
            Item itm = (Item)LBA1Othr_cboQuest.SelectedItem;
            LBA1Othr_chkQuestValue.Checked = 1 == memRoutines.getVal(LBA_ONE, itm);
        }
        #endregion
        #region Movies
        private void LBA1Othr_populateMovies()
        {
            LBA1Othr_cboMovies.Items.Clear();
            LBA1Othr_cboMovies.Items.AddRange(items.Movies);
        }
        private void cboLBA1Movies_SelectedIndexChanged(object sender, EventArgs e)
        {
            Item itm = (Item)LBA1Othr_cboMovies.SelectedItem;
            LBA1Othr_chkMoviesValue.Checked = 1 == memRoutines.getVal(LBA_ONE, itm);
        }
        private void ChkLBA1MoviesValue_CheckedChanged(object sender, EventArgs e)
        {
            ushort val;
            if (LBA1Othr_chkMoviesValue.Checked)
                val = 1;
            else
                val = 0;
            if (-1 == LBA1Othr_cboMovies.SelectedIndex) return;
            memRoutines.WriteVal(LBA_ONE, (Item)LBA1Othr_cboMovies.SelectedItem, val);
        }
        #endregion
        #region InvUsed
        private void LBA1Othr_populateInventoryUsed()
        {
            LBA1Othr_cboInvUsed.Items.Clear();
            LBA1Othr_cboInvUsed.Items.AddRange(items.InventoryUsed);
        }
        private void cboLBA1InvUsed_SelectedIndexChanged(object sender, EventArgs e)
        {
            Item itm = (Item)LBA1Othr_cboInvUsed.SelectedItem;
            LBA1Othr_chkInvUsedValue.Checked = 1 == memRoutines.getVal(LBA_ONE, itm);
        }
        private void LBA1Othr_chkInvUsedValue_CheckedChanged(object sender, EventArgs e)
        {
            ushort val;
            val = LBA1Othr_chkInvUsedValue.Checked ? (ushort) 1 : (ushort) 0;
            if (LBA1Othr_chkInvUsedValue.Checked)
                val = 1;
            else
                val = 0;
            if (-1 == LBA1Othr_cboInvUsed.SelectedIndex) return;
            memRoutines.WriteVal(LBA_ONE, (Item)LBA1Othr_cboInvUsed.SelectedItem, val);
        }
        #endregion
        #region Chapter

        private void LBA1Othr_cboLBA1Chapter_SelectedIndexChanged(object sender, EventArgs e)
        {
            LBA1Othr_chkChapterValue.Checked = LBA1Othr_cboChapter.SelectedIndex == memRoutines.readAddress(LBA_ONE, LBA1_CHAPTER, 1);
        }

        private void LBA1Othr_chkChapterValue_CheckedChanged(object sender, EventArgs e)
        {
            if (-1 != LBA1Othr_cboChapter.SelectedIndex && LBA1Othr_chkChapterValue.Checked)
                memRoutines.WriteVal(1, (int)LBA1_CHAPTER, (ushort)LBA1Othr_cboChapter.SelectedIndex, 1);
        }
        #endregion

        private void LBA1Othr_populateHolomap()
        {
            LBA1Othr_cboHolomap.Items.Clear();
            LBA1Othr_cboHolomap.Items.AddRange(items.Holomap);
        }



        private void LBA1Othr_cboQuest_TextChanged(object sender, EventArgs e)
        {
            filterCBO(LBA1Othr_cboQuest, items.Quest);
        }

        private void LBA1Othr_cboInvUsed_TextChanged(object sender, EventArgs e)
        {
            filterCBO(LBA1Othr_cboInvUsed, items.InventoryUsed);
        }
    }
}
