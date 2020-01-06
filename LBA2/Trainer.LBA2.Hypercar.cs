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
        const short LBA2HCBOTTOMGEARLIMIT = 380; //3800
        const short LBA2HCTOPGEARLIMIT = 6000; //60000
        const int LBA2HCREVERSESPEEDCAPOFFSET = -0x58EAB;
        const int LBA2HCFORWARDSPEEDCAPOFFSET = -0x58E9B;
        const int LBA2HCCURRENTSPEEDOFFSET = 0x52B9B;
        private HotKey LBA2HyperCar_hkZ;
        private HotKey LBA2HyperCar_hkX;
        private HotKey LBA2HyperCar_hkC;
        private HotKey LBA2HyperCar_hkV;
        byte gear = 1;
        private void LBA2HyperCar_ProcessHotKey(Keys k)
        {
            if (Keys.C == k)
            {
                /*int speed = memRoutines.readVal(0x52B9B, 2);
                speed -= 500;
                speed = 3800 <= speed ? speed : 0;
                memRoutines.WriteVal(0x52B9B, (ushort)speed, 2);*/
                memRoutines.WriteVal(LBA2HCCURRENTSPEEDOFFSET, 0, 2);
                return;
            }
            if (Keys.V == k)
            {
                int turbo = getInt(txtLBA2HyperCarTurbo.Text) <= LBA2HCTOPGEARLIMIT ? getInt(txtLBA2HyperCarTurbo.Text) : LBA2HCTOPGEARLIMIT;
                memRoutines.WriteVal(LBA2HCCURRENTSPEEDOFFSET, (ushort)(memRoutines.readVal(LBA2HCCURRENTSPEEDOFFSET, 2) + (getInt(txtLBA2HyperCarTurbo.Text)*10)), 2);
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
            if (LBA2HCBOTTOMGEARLIMIT > speed) speed = LBA2HCBOTTOMGEARLIMIT;
            if (LBA2HCTOPGEARLIMIT < speed) speed = LBA2HCTOPGEARLIMIT;
            memRoutines.WriteVal(LBA2HCFORWARDSPEEDCAPOFFSET, (ushort)(speed*10), 2);
        }
        private void LBA2HyperCar_registerHotKeys()
        {
            LBA2HyperCar_hkZ = registerHotKey(Keys.Z);
            LBA2HyperCar_hkX = registerHotKey(Keys.X);
            LBA2HyperCar_hkC = registerHotKey(Keys.C);
            LBA2HyperCar_hkV = registerHotKey(Keys.V);
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
                unregisterHotKey(LBA2HyperCar_hkV);
                LBA2HyperCar_hkV = null;
            }
            catch { };
        }

        private void LBA2btnHyperCarOnOff_Click(object sender, EventArgs e)
        {
            //If currently off
            if (null == LBA2HyperCar_hkX)
            {
                LBA2HyperCar_registerHotKeys();
                rbLBA2HyperCar1.Checked = true;
                setGear(); 
                memRoutines.WriteVal(LBA2HCREVERSESPEEDCAPOFFSET, (ushort)(getInt(txtHyperCarGearReverse.Text) * 10), 2);
            }
            else
            {
                LBA2HyperCar_unregisterHotkeys();
                //Set forward limit to current gear limit
                //memRoutines.WriteVal(-0x58e9B, 3800, 2);
                gear = 1;
                setGear();
                memRoutines.WriteVal(LBA2HCFORWARDSPEEDCAPOFFSET, 63535, 2);
            }

            btnHyperCarOnOff.Text  = null == LBA2HyperCar_hkX ? "Off": "On";
            toggleSpeedometer();
        }


        private void rbLBA2HyperCar1_Click(object sender, EventArgs e)
        {
            gear = 1;
            setGear();
        }
        private void rbLBA2HyperCar2_Click(object sender, EventArgs e)
        {
            gear = 2;
            setGear();
        }
        private void rbLBA2HyperCar3_Click(object sender, EventArgs e)
        {
            gear = 3;
            setGear();
        }
        private void rbLBA2HyperCar4_Click(object sender, EventArgs e)
        {
            gear = 4;
            setGear();
        }
        private void rbLBA2HyperCar5_Click(object sender, EventArgs e)
        {
            gear = 5;
            setGear();
        }


        Timer tmrLBA2HyperCarSpeedometer;

        private void tmrLBA2HyperCarSpeedometer_Tick(object sender, EventArgs e)
        {
            lblHyperCarActualSpeed.Text = (memRoutines.readVal(0x52B9B, 2)/10).ToString();
        }
        private void toggleSpeedometer()
        {
            if (null == tmrLBA2HyperCarSpeedometer)
            {
                tmrLBA2HyperCarSpeedometer = new Timer();
                tmrLBA2HyperCarSpeedometer.Interval = 10;
                tmrLBA2HyperCarSpeedometer.Tick += tmrLBA2HyperCarSpeedometer_Tick;
                tmrLBA2HyperCarSpeedometer.Start();
            }
            else
            {
                tmrLBA2HyperCarSpeedometer.Stop();
                tmrLBA2HyperCarSpeedometer = null;
            }
        }
        private void bPitLimit_Click(object sender, EventArgs e)
        {
            memRoutines.WriteVal(LBA2HCFORWARDSPEEDCAPOFFSET, 200, 2);
        }
    }
}
