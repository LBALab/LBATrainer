using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LBATrainer
{
    public partial class frmTrainer
    {
        /*
         * Auto: 0x1278 = 4. 0xE1E = 1
         * Manual: 0x1278 = 2. 0xE1E = 0
         */
        /*bool manual = 0 == memRoutines.readVal(0x1278, 1);
            if (manual)
            {
                memRoutines.WriteVal(0x1278, 4, 1);
                memRoutines.WriteVal(0xE1E, 1, 1);
            }
            else
            {
                memRoutines.WriteVal(0x1278, 2, 1);
                memRoutines.WriteVal(0xE1E, 0, 1);
            }*/


        private void LBA1Debug_btnlDebugOffset1Set_Click(object sender, EventArgs e)
        {
            LBA1Debug_SetVal(LBA1Debug_txtDebugOffset1.Text, LBA1Debug_txtDebugValue1.Text);
        }

        private void LBA1Debug_btnlDebugOffset2Set_Click(object sender, EventArgs e)
        {
            LBA1Debug_SetVal(LBA1Debug_txtDebugOffset2.Text, LBA1Debug_txtDebugValue2.Text);
        }

        private void LBA1Debug_btnSetBoth_Click(object sender, EventArgs e)
        {
            LBA1Debug_btnlDebugOffset1Set_Click(null, null);
            LBA1Debug_btnlDebugOffset2Set_Click(null, null);
        }
        private void LBA1Debug_SetVal(string offset, string data)
        {
            memRoutines.WriteVal(int.Parse(offset, System.Globalization.NumberStyles.HexNumber), ushort.Parse(data, System.Globalization.NumberStyles.HexNumber), 1);
        }
    }
}