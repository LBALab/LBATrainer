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

        private HotKey LBA2HyperCar_hkZ;
        private HotKey LBA2HyperCar_hkX;
        private HotKey LBA2HyperCar_hkC;
        byte gear = 1;
        bool hyperCarOn = false;
        private void LBA2HyperCar_ProcessHotKey(Keys k)
        {
            if (Keys.C == k)
            {
                memRoutines.WriteVal(0x52B9B, 0, 2);
                return;
            }
            if (Keys.X == k)
            {
                if (5 > gear)
                {
                    gear++;
                }
            }
            if (Keys.Z == k)
            {
                if (1 < gear)
                {
                    gear--;
                }
            }
            setGear();
        }

        private void setGear()
        {
            int speed = 0;
            switch (gear)
            {
                case 1:
                    rbLBA2HyperCar1.Checked = true;
                    speed = getInt(txtHyperCarGear1.Text);
                    break;
                case 2:
                    rbLBA2HyperCar2.Checked = true;
                    speed = getInt(txtHyperCarGear2.Text);
                    break;
                case 3:
                    rbLBA2HyperCar3.Checked = true;
                    speed = getInt(txtHyperCarGear3.Text);
                    break;
                case 4:
                    rbLBA2HyperCar4.Checked = true;
                    speed = getInt(txtHyperCarGear4.Text);
                    break;
                case 5:
                    rbLBA2HyperCar5.Checked = true;
                    speed = getInt(txtHyperCarGear5.Text);
                    break;
            }
            //public void WriteVal(int offset, ushort data, byte size);
            memRoutines.WriteVal(-0x58e9B, (ushort)speed, 2);
        }
        private void LBA2HyperCar_registerHotKeys()
        {
            LBA2HyperCar_hkZ = registerHotKey(Keys.Z);
            LBA2HyperCar_hkX = registerHotKey(Keys.X);
            LBA2HyperCar_hkX = registerHotKey(Keys.C);
        }
        private void LBA2HyperCar_unregisterHotkeys()
        {
            try
            {
                unregisterHotKey(LBA2HyperCar_hkX);
                LBA2HyperCar_hkX = null;
                unregisterHotKey(LBA2HyperCar_hkZ);
                LBA2HyperCar_hkZ = null;
                unregisterHotKey(LBA2HyperCar_hkC);
                LBA2HyperCar_hkC = null;
            }
            catch { };
        }

        private void LBA2btnHyperCarOnOff_Click(object sender, EventArgs e)
        {
            if (null == LBA2HyperCar_hkX)
            {
                LBA2HyperCar_registerHotKeys();
                memRoutines.WriteVal(-0x58e9B, 3800, 2);
                rbLBA2HyperCar1.Checked = true;
            }
            else
                LBA2HyperCar_unregisterHotkeys();

            btnHyperCarOnOff.Text  = null == LBA2HyperCar_hkX ? "Off": "On";
        }


    }
}
