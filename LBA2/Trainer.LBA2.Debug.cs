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
        private void LBA2Debug_btnGetAreacode_Click(object sender, EventArgs e)
        {
            LBA2Othr_lblAreacodeTxt.Text = memRoutines.readVal(0x55C5F, 2).ToString();
        }

        #region FruitMachine
        /*
        private void tmrLBA2FruitMachine_Tick(object sender, EventArgs e)
        {
            lblFruitMachineCount.Text = memRoutines.readVal(0x57BC9, 2).ToString();
        }


        Timer fruity;

        private void btnFruitMachineStart_Click(object sender, EventArgs e)
        {
            if (null == fruity)
            {
                fruity = new Timer();
                fruity.Interval = 10;
                fruity.Tick += tmrLBA2FruitMachine_Tick;
                fruity.Start();
            }
            else
            {
                fruity.Stop();
                fruity = null;
            }
        }*/
        #endregion
        //Other Tab: Instant car
        private void btnLBA2InstantCar_Click(object sender, EventArgs e)
        {
            becomeACar(15);     
        }

        private void btnLBA2InstantCarTunic_Click(object sender, EventArgs e)
        {
            becomeACar(14);
        }

        private void becomeACar(ushort skinID)
        {
            memRoutines.WriteVal(0x580EF, 13, 1); //Set behaviour to car
            memRoutines.WriteVal(0x57F51, skinID, 1);
            memRoutines.WriteVal(0x52B98, 4, 1); //Can turn car
        }
        private void btnLBA2InstantCarDisabled_Click(object sender, EventArgs e)
        {
            memRoutines.WriteVal(0x52B98, 0, 1); //Can turn car
            memRoutines.WriteVal(0x57F51, 1, 1); //Dump outfit to 1
            memRoutines.WriteVal(0x580EF, 0, 1); //Set behaviour to normal
        }

    }
}
