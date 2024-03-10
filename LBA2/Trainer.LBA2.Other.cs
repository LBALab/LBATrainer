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
        Movies movies;

        const int LBA2WEAPONADDRESS = 0x3D693;
        private void LBA2Othr_Load()
        {
            LBA2Othr_populateSkins();
            LBA2Othr_populateQuests();
            LBA2Othr_populateTritonHorn();
            LBA2Othr_populateWeapons();
            LBA2Othr_populateMovies();
        }

        private void LBA2Othr_populateMovies()
        {
            movies = new Movies(getLBAFilesPath(2));
            LBA2Othr_cboMovies.Items.Clear();
            LBA2Othr_cboMovies.Items.AddRange(movies.movies);
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

        private void LBA2Othr_populateTritonHorn()
        {
            byte  val = (byte)memRoutines.readVal(LBA2_HORN_TRITON_HEALTH, 1);
            LBA2Othr_hsbHornHealth.Value = val;
            LBA2Othr_lblHornHealthValue.Text = val.ToString();
        }

        private void LBA2Othr_populateWeapons()
        {
            LBA2Othr_cboWeapon.Items.Clear();
            NameValue[] nv = new NameValue[8];
            nv[0] = new NameValue(0, "No Weapon");
            nv[1] = new NameValue(1, "Magic Ball");
            nv[2] = new NameValue(2, "Darts");
            nv[3] = new NameValue(9, "Lazer Pistol");
            nv[4] = new NameValue(10, "Emporers Sword");
            nv[5] = new NameValue(11, "Wannies Glove");
            nv[6] = new NameValue(22, "Horn of triton");
            nv[7] = new NameValue(23, "Blowgun/Blowtron");
            LBA2Othr_cboWeapon.Items.AddRange(nv);
            LBA2Othr_cboWeapon.Tag = nv;
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
            LBA2Othr_cboSubquest.Items.AddRange(((LBA2Quest)LBA2Othr_cboQuest.SelectedItem).subquests);
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
            return ((NameValue)LBA2Othr_cboWeapon.SelectedItem).val;
        }
        private void LBA2Othr_cboWeapon_SelectedIndexChanged(object sender, EventArgs e)
        {
            LBA2Othr_chkWeapon.Checked = LBA2Othr_getSelectedWeaponNumber() == memRoutines.readAddress(2, LBA2WEAPONADDRESS, 1);
        }

        private void LBA2Othr_chkWeapon_CheckedChanged(object sender, EventArgs e)
        {
            if (LBA2Othr_chkWeapon.Checked) memRoutines.WriteVal(LBA2WEAPONADDRESS, LBA2Othr_getSelectedWeaponNumber(), 1);
        }


        private void LBA2Othr_hsbHornHealth_Scroll(object sender, ScrollEventArgs e)
        {
            memRoutines.WriteVal(LBA2_HORN_TRITON_HEALTH, (byte)LBA2Othr_hsbHornHealth.Value, 1);
            LBA2Othr_lblHornHealthValue.Text = LBA2Othr_hsbHornHealth.Value.ToString();
        }

        private void LBA2Othr_lblHornHealthValue_Click(object sender, EventArgs e)
        {
            memRoutines.WriteVal(LBA2_HORN_TRITON_HEALTH, 100, 1);
        }

        private void LBA2Othr_filterQuestCBO(ComboBox cb, LBA2Quest[] quests)
        {
            //If not entering data i.e. empty field
            if (-1 != cb.SelectedIndex) return;

            cb.Items.Clear();
            LBA2Othr_cboSubquest.Items.Clear();
            for (int i = 0; i < quests.Count(); i++)
                if (quests[i].name.ToLower().Contains(cb.Text.ToLower()))
                    cb.Items.Add(quests[i]);
                else
                    for(int j = 0; j < quests[i].subquests.Count();j++)
                        if(quests[i].subquests[j].name.ToLower().Contains(cb.Text.ToLower()))
                        {
                            cb.Items.Add(quests[i]);
                            break;
                        }

            cb.SelectionStart = cb.Text.Length;
            cb.SelectionLength = 0;
        }

        private void LBA2Othr_cboQuest_TextChanged(object sender, EventArgs e)
        {
            LBA2Othr_filterQuestCBO(LBA2Othr_cboQuest, LBA2Other_quests.quests);
        }

        private void LBA2Othr_cboMovies_SelectedIndexChanged(object sender, EventArgs e)
        {
            LBA2Othr_chkMovie.Checked = LBA2Othr_IsMovieSelected();
        }

        private bool LBA2Othr_IsMovieSelected()
        {
            return ((Movies.Movie)LBA2Othr_cboMovies.SelectedItem).IsEnabled();
        }
        private void LBA2Othr_chkMovie_CheckedChanged(object sender, EventArgs e)
        {
            if (-1 == LBA2Othr_cboMovies.SelectedIndex) return;
            if (LBA2Othr_chkMovie.Checked)
                ((Movies.Movie)LBA2Othr_cboMovies.SelectedItem).Enable();
            else
                ((Movies.Movie)LBA2Othr_cboMovies.SelectedItem).Disable();
        }

        private void LBA2Othr_filterMovieCBO(ComboBox cb, Movies.Movie[] movies)
        {
            //If not entering data i.e. empty field
            if (-1 != cb.SelectedIndex) return;

            cb.Items.Clear();
            LBA2Othr_cboSubquest.Items.Clear();
            for (int i = 0; i < movies.Count(); i++)
                if (movies[i].ToString().ToLower().Contains(cb.Text.ToLower()))
                    cb.Items.Add(movies[i]);

            cb.SelectionStart = cb.Text.Length;
            cb.SelectionLength = 0;
        }
        private void LBA2Othr_cboMovies_TextChanged(object sender, EventArgs e)
        {
            LBA2Othr_filterMovieCBO(LBA2Othr_cboMovies, movies.movies);
        }
    }
}
