using LBAMemoryModule;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LBATrainer
{
    public partial class frmTrainer
    {
        CheckBox chkLBA2InventoryValue;
        TextBox txtLBA2InventoryValue;
        private void BtnLBA2MaxAll_Click(object sender, EventArgs e)
        {
            maxAll(LBA_TWO);
        }
        private void BtnLBA2MinAll_Click(object sender, EventArgs e)
        {
            MinAll(LBA_TWO);
        }
        private void BtnLBA2Scan_Click(object sender, EventArgs e)
        {
            scan(LBA_TWO);
        }
        private void scanLBA2()
        {
            int val;
            items = new Items(getLBAFilesPath(LBA_TWO), LBA_TWO);
            memRoutines = new Mem();
            val = memRoutines.getVal(LBA_TWO, items.MagicLevel);
            if (-1 == val)
            {
                return;
            }
            txtLBA2MagicLevel.Text = val.ToString();
            txtLBA2MagicPoints.Text = memRoutines.getVal(LBA_TWO, items.MagicPoints).ToString();
            txtLBA2Kashers.Text = memRoutines.getVal(LBA_TWO, items.Kashers).ToString();
            txtLBA2Keys.Text = memRoutines.getVal(LBA_TWO, items.Keys).ToString();
            txtLBA2Clovers.Text = memRoutines.getVal(LBA_TWO, items.Clovers).ToString();
            txtLBA2CloverBoxes.Text = memRoutines.getVal(LBA_TWO, items.CloverBoxes).ToString();
            txtLBA2Health.Text = memRoutines.getVal(LBA_TWO, items.Health).ToString();
            txtLBA2Zilitos.Text = memRoutines.getVal(LBA_TWO, items.Zilitos).ToString();
            cboLBA2Inventory.Items.Clear();
            cboLBA2Inventory.Items.AddRange(items.Inventory);
            addLBA2InventoryCheckbox(null);
        }
        #region inventory
        private void CboLBA2Inventory_SelectedIndexChanged(object sender, EventArgs e)
        {
            Item itm = (Item)cboLBA2Inventory.SelectedItem;
            if (Item.TYPE_BITFLAG == itm.type)
                addLBA2InventoryCheckbox(itm);
            else
                addLBA2InventoryTextbox(itm);
        }
        private void addLBA2InventoryCheckbox(Item itm)
        {
            if (tpLBA2Twinsen.Controls.Contains(chkLBA2InventoryValue))
            {
                chkLBA2InventoryValue.Checked = !(0 == memRoutines.getVal(LBA_TWO, itm));
                return;
            }
            if (tpLBA2Twinsen.Controls.Contains(txtLBA2InventoryValue))
                tpLBA2Twinsen.Controls.Remove(txtLBA2InventoryValue);
            chkLBA2InventoryValue = new CheckBox();
            chkLBA2InventoryValue.Size = new Size(15, 14);
            chkLBA2InventoryValue.Location = new Point(284, 116);
            chkLBA2InventoryValue.CheckedChanged += new System.EventHandler(this.ChkLBA2InventoryValue_CheckedChanged);
            if (null != itm) chkLBA2InventoryValue.Checked = !(0 == memRoutines.getVal(LBA_TWO, itm));
            tpLBA2Twinsen.Controls.Add(chkLBA2InventoryValue);
        }
        private void addLBA2InventoryTextbox(Item itm)
        {
            if (tpLBA2Twinsen.Controls.Contains(txtLBA2InventoryValue))
            {
                txtLBA2InventoryValue.Text = memRoutines.getVal(LBA_TWO, itm).ToString();
                return;
            }
            if (tpLBA2Twinsen.Controls.Contains(chkLBA2InventoryValue))
                tpLBA2Twinsen.Controls.Remove(chkLBA2InventoryValue);
            txtLBA2InventoryValue = new TextBox();
            txtLBA2InventoryValue.Text = memRoutines.getVal(LBA_TWO, itm).ToString();
            txtLBA2InventoryValue.Size = new Size(44, 20);
            txtLBA2InventoryValue.Location = new Point(284, 114);
            txtLBA2InventoryValue.TextChanged += new System.EventHandler(this.TxtLBA2InventoryValue_TextChanged);
            tpLBA2Twinsen.Controls.Add(txtLBA2InventoryValue);
        }
        private void TxtLBA2InventoryValue_TextChanged(object sender, EventArgs e)
        {
            if (-1 == cboLBA2Inventory.SelectedIndex) return;
            if (0 >= txtLBA2InventoryValue.Text.Length) return;
            memRoutines.WriteVal(LBA_TWO, (Item)cboLBA2Inventory.SelectedItem, txtLBA2InventoryValue.Text);
        }
        private void ChkLBA2InventoryValue_CheckedChanged(object sender, EventArgs e)
        {
            ushort val;

            if (-1 == cboLBA2Inventory.SelectedIndex) return;
            if (chkLBA2InventoryValue.Checked)
                val = ((Item)cboLBA2Inventory.SelectedItem).maxVal;
            else
                val = ((Item)cboLBA2Inventory.SelectedItem).minVal;

            memRoutines.WriteVal(LBA_TWO, (Item)cboLBA2Inventory.SelectedItem, val);
        }
        #endregion
        private void BtnLBA2TwinsenSet_Click(object sender, EventArgs e)
        {
            memRoutines.WriteVal(LBA_TWO, items.CloverBoxes, txtLBA2CloverBoxes.Text);
            memRoutines.WriteVal(LBA_TWO, items.Clovers, txtLBA2Clovers.Text);
            memRoutines.WriteVal(LBA_TWO, items.MagicLevel, txtLBA2MagicLevel.Text);
            memRoutines.WriteVal(LBA_TWO, items.MagicPoints, txtLBA2MagicPoints.Text);
            memRoutines.WriteVal(LBA_TWO, items.Health, txtLBA2Health.Text);
            memRoutines.WriteVal(LBA_TWO, items.Keys, txtLBA2Keys.Text);
            memRoutines.WriteVal(LBA_TWO, items.Kashers, txtLBA2Kashers.Text);
            memRoutines.WriteVal(LBA_TWO, items.Zilitos, txtLBA2Zilitos.Text);
        }

    }
}
