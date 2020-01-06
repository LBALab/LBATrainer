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
        private void LBA1Othr_populateQuest()
        {
            LBA1Othr_cboQuest.Items.Clear();
            LBA1Othr_cboQuest.Items.AddRange(items.Quest);
        }
        private void LBA1Othr_populateMovies()
        {
            LBA1Othr_cboMovies.Items.Clear();
            LBA1Othr_cboMovies.Items.AddRange(items.Movies);
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
        private void CboLBA1OtherChapter_SelectedIndexChanged(object sender, EventArgs e)
        {
            string val = LBA1Othr_cboChapter.SelectedItem.ToString().Substring(0, 2);
            memRoutines.WriteVal(1, 0xE28, (ushort)getInt(val), 1);
        }
    }
}
