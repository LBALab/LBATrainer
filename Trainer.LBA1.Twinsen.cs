using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LBATrainer
{
    public partial class frmTrainer
    {
        private void BtnLBA1MaxAll_Click(object sender, EventArgs e)
        {
            maxAll(LBA_ONE);
        }
        private void BtnLBA1Scan_Click(object sender, EventArgs e)
        {
            scan(LBA_ONE);
        }
        private void BtnLBA1MinAll_Click(object sender, EventArgs e)
        {
            MinAll(LBA_ONE);
        }
        private void CboLBA1Inventory_SelectedIndexChanged(object sender, EventArgs e)
        {
            Item itm = (Item)cboLBA1Inventory.SelectedItem;
            chkLBA1InventoryValue.Checked = 1 == memRoutines.getVal(LBA_ONE, itm);
        }
        private void ChkLBA1InventoryValue_CheckedChanged(object sender, EventArgs e)
        {
            ushort val;
            if (chkLBA1InventoryValue.Checked)
                val = 1;
            else
                val = 0;
            if (-1 == cboLBA1Inventory.SelectedIndex) return;
            memRoutines.WriteVal(LBA_ONE, (Item)cboLBA1Inventory.SelectedItem, val);
        }
        private void scanLBA1()
        {
            int val;
            items = new Items(LBA_ONE);
            memRoutines = new mem();
            val = memRoutines.getVal(LBA_ONE, items.MagicLevel);
            if (-1 == val) return;
            txtLBA1MagicLevel.Text = val.ToString();
            txtLBA1MagicPoints.Text = memRoutines.getVal(LBA_ONE, items.MagicPoints).ToString();
            txtLBA1Kashers.Text = memRoutines.getVal(LBA_ONE, items.Kashers).ToString();
            txtLBA1Keys.Text = memRoutines.getVal(LBA_ONE, items.Keys).ToString();
            txtLBA1Clovers.Text = memRoutines.getVal(LBA_ONE, items.Clovers).ToString();
            txtLBA1CloverBoxes.Text = memRoutines.getVal(LBA_ONE, items.CloverBoxes).ToString();
            txtLBA1Gas.Text = memRoutines.getVal(LBA_ONE, items.Gas).ToString();
            txtLBA1Health.Text = memRoutines.getVal(LBA_ONE, items.Health).ToString();
            cboLBA1Inventory.Items.Clear();
            cboLBA1Inventory.Items.AddRange(items.Inventory);
            populateLBA1Quest();
            populateLBA1Movies();
            teleportScan();
        }
        private void BtnLBA1Set_Click(object sender, EventArgs e)
        {
            memRoutines.WriteVal(LBA_ONE, items.CloverBoxes, txtLBA1CloverBoxes.Text);
            memRoutines.WriteVal(LBA_ONE, items.Clovers, txtLBA1Clovers.Text);
            memRoutines.WriteVal(LBA_ONE, items.MagicLevel, txtLBA1MagicLevel.Text);
            memRoutines.WriteVal(LBA_ONE, items.MagicPoints, txtLBA1MagicPoints.Text);
            memRoutines.WriteVal(LBA_ONE, items.Health, txtLBA1Health.Text);
            memRoutines.WriteVal(LBA_ONE, items.Keys, txtLBA1Keys.Text);
            memRoutines.WriteVal(LBA_ONE, items.Kashers, txtLBA1Kashers.Text);
            memRoutines.WriteVal(LBA_ONE, items.Gas, txtLBA1Gas.Text);
        }
        private void TcLBA1Inner_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (1 == tcLBA1Inner.SelectedIndex)
                populateLBA1Quest();
        }
    }
}
