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
        private void LBA2WizardGPS_Start_Click(object sender, EventArgs e)
        {
            if (null == tgi) tgi = new oTimerGetItems();

            //If doesn't exist add else remove
            if (!tgi.Contains(LBA2_WIZARD_POSITION))
                tgi.AddItem(LBA2WizardGPS_updateWizardPos, new Item(LBA2_WIZARD_POSITION, 1));
            else
                tgi.RemoveIfExists(LBA2_WIZARD_POSITION);
        }

        private void LBA2WizardGPS_updateWizardPos(ushort pos)
        {
            txtWizardLocation.Text = pos.ToString();
            if (93 <= pos) { rb95.Checked = true; return; }
            if (88 <= pos) { rb90.Checked = true; return; }
            if (82 <= pos) { rb85.Checked = true; return; }
            if (78 <= pos) { rb80.Checked = true; return; }
            if (73 <= pos) { rb75.Checked = true; return; }
            if (68 <= pos) { rb70.Checked = true; return; }
            if (63 <= pos) { rb65.Checked = true; return; }
            if (58 <= pos) { rb60.Checked = true; return; }
            if (53 <= pos) { rb55.Checked = true; return; }
            if (48 <= pos) { rb50.Checked = true; return; }
            if (43 <= pos) { rb45.Checked = true; return; }
            if (38 <= pos) { rb40.Checked = true; return; }
            if (33 <= pos) { rb35.Checked = true; return; }
            if (28 <= pos) { rb30.Checked = true; return; }
            if (23 <= pos) { rb25.Checked = true; return; }
            if (18 <= pos) { rb20.Checked = true; return; }
            if (13 <= pos) { rb15.Checked = true; return; }
            if (8 <= pos) { rb10.Checked = true; return; }
            if (3 <= pos) { rb5.Checked = true; return; }
            if (0 <= pos) { rb0.Checked = true; return; }
        }
    }
}
