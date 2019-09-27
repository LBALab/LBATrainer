using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LBATrainer
{
    public partial class frmTrainer
    {
        private void populateLBA1Quest()
        {
            cboLBA1Quest.Items.Clear();
            cboLBA1Quest.Items.AddRange(items.Quest);
        }
        private void populateLBA1Movies()
        {
            cboLBA1Movies.Items.Clear();
            cboLBA1Movies.Items.AddRange(items.Movies);
        }
        private void ChkLBA1QuestValue_CheckedChanged(object sender, EventArgs e)
        {
            ushort val;
            if (chkLBA1QuestValue.Checked)
                val = 1;
            else
                val = 0;
            if (-1 == cboLBA1Quest.SelectedIndex) return;
            memRoutines.WriteVal(LBA_ONE, (Item)cboLBA1Quest.SelectedItem, val);
        }
        private void CboLBA1Quest_SelectedIndexChanged(object sender, EventArgs e)
        {
            Item itm = (Item)cboLBA1Quest.SelectedItem;
            chkLBA1QuestValue.Checked = 1 == memRoutines.getVal(LBA_ONE, itm);
        }
        private void cboLBA1Movies_SelectedIndexChanged(object sender, EventArgs e)
        {
            Item itm = (Item)cboLBA1Movies.SelectedItem;
            chkLBA1MoviesValue.Checked = 1 == memRoutines.getVal(LBA_ONE, itm);
        }
        private void ChkLBA1MoviesValue_CheckedChanged(object sender, EventArgs e)
        {
            ushort val;
            if (chkLBA1MoviesValue.Checked)
                val = 1;
            else
                val = 0;
            if (-1 == cboLBA1Movies.SelectedIndex) return;
            memRoutines.WriteVal(LBA_ONE, (Item)cboLBA1Movies.SelectedItem, val);
        }
        private void CboLBA1OtherChapter_SelectedIndexChanged(object sender, EventArgs e)
        {
            memRoutines.WriteVal(1, 0xE28, (ushort)getInt(cboLBA1OtherChapter.SelectedItem.ToString()), 1);
        }
    }
}
