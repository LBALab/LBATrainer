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
        private void LBA1Twin_BtnMaxAll_Click(object sender, EventArgs e)
        {
            maxAll(LBA_ONE);
        }
        private void LBA1Twin_BtnScan_Click(object sender, EventArgs e)
        {
            scan(LBA_ONE);
        }
        private void LBA1Twin_BtnMinAll_Click(object sender, EventArgs e)
        {
            MinAll(LBA_ONE);
        }
        private void LBA1Twin_CboInventory_SelectedIndexChanged(object sender, EventArgs e)
        {
            Item itm = (Item)LBA1Twin_cboInventory.SelectedItem;
            LBA1Twin_chkInventoryValue.Checked = 1 == memRoutines.getVal(LBA_ONE, itm);
        }
        private void ChkLBA1InventoryValue_CheckedChanged(object sender, EventArgs e)
        {
            ushort val;
            if (LBA1Twin_chkInventoryValue.Checked)
                val = 1;
            else
                val = 0;
            if (-1 == LBA1Twin_cboInventory.SelectedIndex) return;
            memRoutines.WriteVal(LBA_ONE, (Item)LBA1Twin_cboInventory.SelectedItem, val);
        }
        private void scanLBA1()
        {
            int val;
            items = new Items(getLBAFilesPath(LBA_ONE),LBA_ONE);
            memRoutines = new Mem();
            val = memRoutines.getVal(LBA_ONE, items.MagicLevel);
            if (-1 == val) return;
            LBA1Twin_txtMagicLevel.Text = val.ToString();
            LBA1Twin_txtMagicPoints.Text = memRoutines.getVal(LBA_ONE, items.MagicPoints).ToString();
            LBA1Twin_txtKashers.Text = memRoutines.getVal(LBA_ONE, items.Kashers).ToString();
            LBA1Twin_txtKeys.Text = memRoutines.getVal(LBA_ONE, items.Keys).ToString();
            LBA1Twin_txtClovers.Text = memRoutines.getVal(LBA_ONE, items.Clovers).ToString();
            LBA1Twin_txtCloverBoxes.Text = memRoutines.getVal(LBA_ONE, items.CloverBoxes).ToString();
            LBA1Twin_txtGas.Text = memRoutines.getVal(LBA_ONE, items.Gas).ToString();
            LBA1Twin_txtHealth.Text = memRoutines.getVal(LBA_ONE, items.Health).ToString();
            LBA1Twin_cboInventory.Items.Clear();
            LBA1Twin_cboInventory.Items.AddRange(items.Inventory);
            LBA1Othr_Start();
            //LBA1Tel_teleportScan();
        }
        private void LBA1Twin_BtnSet_Click(object sender, EventArgs e)
        {
            memRoutines.WriteVal(LBA_ONE, items.CloverBoxes, LBA1Twin_txtCloverBoxes.Text);
            memRoutines.WriteVal(LBA_ONE, items.Clovers, LBA1Twin_txtClovers.Text);
            memRoutines.WriteVal(LBA_ONE, items.MagicLevel, LBA1Twin_txtMagicLevel.Text);
            memRoutines.WriteVal(LBA_ONE, items.MagicPoints, LBA1Twin_txtMagicPoints.Text);
            memRoutines.WriteVal(LBA_ONE, items.Health, LBA1Twin_txtHealth.Text);
            memRoutines.WriteVal(LBA_ONE, items.Keys, LBA1Twin_txtKeys.Text);
            memRoutines.WriteVal(LBA_ONE, items.Kashers, LBA1Twin_txtKashers.Text);
            memRoutines.WriteVal(LBA_ONE, items.Gas, LBA1Twin_txtGas.Text);
        }
        private void TcLBA1Inner_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (1 == tcLBA1Inner.SelectedIndex)
                LBA1Othr_populateQuest();
        }

        private void LBA1Twin_cboInventory_TextChanged(object sender, EventArgs e)
        {
            filterCBO(LBA1Twin_cboInventory, items.Inventory);
        }
    }
}
