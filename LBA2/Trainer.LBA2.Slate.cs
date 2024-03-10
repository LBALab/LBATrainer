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


        public void LBA2Slate_Load()
        {
            LBA2Slate_populateCBO(LBA2Slate_cb0);
            LBA2Slate_populateCBO(LBA2Slate_cb1);
            LBA2Slate_populateCBO(LBA2Slate_cb2);
            LBA2Slate_populateCBO(LBA2Slate_cb3);
            LBA2Slate_populateCBO(LBA2Slate_cb4);
        }

        private string LBA2Slate_GetDescriptionFromID(byte ID)
        {
            if (5 == ID) return "Citadel Sewers";
            if (7 == ID) return "Dome of the Slate";
            if (9 == ID) return "Emerald Moon";
            if (21 == ID) return "Zeelich cross section";
            if (33 == ID) return "Island CX Base";
            return "None";
        }
        private void LBA2Slate_populateCBO(ComboBox cb)
        {
            NameValue[] sv = new NameValue[6];
            sv[0] = new NameValue(255, LBA2Slate_GetDescriptionFromID(255));
            sv[1] = new NameValue(5, LBA2Slate_GetDescriptionFromID(5));
            sv[2] = new NameValue(7, LBA2Slate_GetDescriptionFromID(7));
            sv[3] = new NameValue(9, LBA2Slate_GetDescriptionFromID(9));
            sv[4] = new NameValue(21, LBA2Slate_GetDescriptionFromID(21));
            sv[5] = new NameValue(33, LBA2Slate_GetDescriptionFromID(33));
            cb.Items.AddRange(sv);
            cb.Tag = sv;
        }

        private byte LBA2Slate_getNumOfMaps()
        {
            byte val = LBA2Slate_cbIsEmpty(LBA2Slate_cb0);
            val += LBA2Slate_cbIsEmpty(LBA2Slate_cb1);
            val += LBA2Slate_cbIsEmpty(LBA2Slate_cb2);
            val += LBA2Slate_cbIsEmpty(LBA2Slate_cb3);
            val += LBA2Slate_cbIsEmpty(LBA2Slate_cb4);
            return val;
        }

        private byte LBA2Slate_cbIsEmpty(ComboBox cb)
        {
            if (-1 == cb.SelectedIndex) return 0;
            return (byte) (255 == ((NameValue[])cb.Tag)[cb.SelectedIndex].val ? 0 : 1);
        }
        private void LBA2Slate_btnSet_Click(object sender, EventArgs e)
        {
            memRoutines.WriteVal(LBA2SLATE_CURRENTMAPINDEX,0, 1);

            memRoutines.WriteVal(LBA2SLATE_NUMOFMAPS, LBA2Slate_getNumOfMaps(), 1);
            byte[] data = LBA2Slate_getArray();
            for (int i = 0; i < 5; i++)
                memRoutines.WriteVal((uint)(LBA2SLATE_ARRAYBASE + i),(ushort)data[i] , 1);
        }
        private byte[] LBA2Slate_getArray()
        {
            byte[] sourceVals = new byte[5];
            byte[] outputVals = new byte[5];
            sourceVals[0] = LBA2Slate_getComboVal(LBA2Slate_cb0);
            sourceVals[1] = LBA2Slate_getComboVal(LBA2Slate_cb1);
            sourceVals[2] = LBA2Slate_getComboVal(LBA2Slate_cb2);
            sourceVals[3] = LBA2Slate_getComboVal(LBA2Slate_cb3);
            sourceVals[4] = LBA2Slate_getComboVal(LBA2Slate_cb4);
            int j = 0;
            for (int i = 0; i < 5; i++)
                if (255 != sourceVals[i])
                    outputVals[j++] = sourceVals[i];
            for (; j < 5; j++) outputVals[j] = 0xFF;

            return outputVals;
        }

        private byte LBA2Slate_getComboVal(ComboBox cb)
        {
            if (-1 == cb.SelectedIndex) return 0xFF;
            return ((NameValue[])cb.Tag)[cb.SelectedIndex].val;
        }

        private byte LBA2Slate_getIndexForMapID(byte ID)
        {
            if (5 == ID) return 1;
            if (7 == ID) return 2;
            if (9 == ID) return 3;
            if (21 == ID) return 4;
            if (33 == ID) return 5;
            return 0;
        }
        private void LBA2Slate_btnScan_Click(object sender, EventArgs e)
        {
            byte[] slates = new byte[5];
            slates = memRoutines.getByteArray(LBA2SLATE_ARRAYBASE, 5);
            if (5 != LBA2Slate_cb0.Items.Count)
            {
                LBA2Slate_cb0.SelectedIndex = LBA2Slate_getIndexForMapID(slates[0]);
                LBA2Slate_cb1.SelectedIndex = LBA2Slate_getIndexForMapID(slates[1]);
                LBA2Slate_cb2.SelectedIndex = LBA2Slate_getIndexForMapID(slates[2]);
                LBA2Slate_cb3.SelectedIndex = LBA2Slate_getIndexForMapID(slates[3]);
                LBA2Slate_cb4.SelectedIndex = LBA2Slate_getIndexForMapID(slates[4]);
            }
        }
    }


}
