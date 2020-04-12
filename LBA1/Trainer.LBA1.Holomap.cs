using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LBAMemoryModule;

namespace LBATrainer
{
    public partial class frmTrainer
    {
        private void LBA1Othr_cboHolomap_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (-1 == LBA1Othr_cboHolomap.SelectedIndex) return;
            Item itm = (Item)LBA1Othr_cboHolomap.SelectedItem;
            int val = memRoutines.getVal(LBA_ONE, itm);
            LBA1Holo_chk0.Checked = 0 != (val & 0b00000001);
            LBA1Holo_chk1.Checked = 0 != (val & 0b00000010);
            LBA1Holo_chk2.Checked = 0 != (val & 0b00000100);
            LBA1Holo_chk3.Checked = 0 != (val & 0b00001000);
            LBA1Holo_chk4.Checked = 0 != (val & 0b00010000);
            LBA1Holo_chk5.Checked = 0 != (val & 0b00100000);
            LBA1Holo_chk6.Checked = 0 != (val & 0b01000000);
            LBA1Holo_chk7.Checked = 0 != (val & 0b10000000);
        }

        private int boolToInt(bool val)
        {
            return val ? 1 : 0;
        }
        private void LBA1Holo_btnSet_Click(object sender, EventArgs e)
        {
            Item itm = (Item)LBA1Othr_cboHolomap.SelectedItem;
            int val;
            val = boolToInt(LBA1Holo_chk0.Checked);
            val += boolToInt(LBA1Holo_chk1.Checked) * 2;
            val += boolToInt(LBA1Holo_chk2.Checked) * 4;
            val += boolToInt(LBA1Holo_chk3.Checked) * 8;
            val += boolToInt(LBA1Holo_chk4.Checked) * 16;
            val += boolToInt(LBA1Holo_chk5.Checked) * 32;
            val += boolToInt(LBA1Holo_chk6.Checked) * 64;
            val += boolToInt(LBA1Holo_chk7.Checked) * 128;
            memRoutines.WriteVal(LBA_ONE, itm, (ushort)val);
        }
    }
}
