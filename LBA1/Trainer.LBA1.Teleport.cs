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
        /*oTeleport tel = new oTeleport();
        private void LBA1Tel_BtnScan_Click(object sender, EventArgs e)
        {
            LBA1Tel_teleportScan();
        }

        private void LBA1Tel_BtntSet_Click(object sender, EventArgs e)
        {
            bool timerEnabled = tmrLBA1TeleportTabRefresh.Enabled;
            if (timerEnabled) LBA1Tel_toggleTimer();
            short val = (short)getInt(LBA1Tel_txtZPos.Text);
            if (-1 != val) tel.SetZPos((ushort)val);
            else return;
            val = (short)getInt(LBA1Tel_txtXPos.Text);
            if (-1 != val) tel.SetXPos((ushort)val);

            val = (short)getInt(LBA1Tel_txtYPos.Text);
            if (-1 != val) tel.SetYPos((ushort)val);

            val = (short)getInt(LBA1Tel_txtFacing.Text);
            if (-1 != val) tel.SetFacing((ushort)val);

            if (timerEnabled) LBA1Tel_toggleTimer();
        }
        private void LBA1Tel_TmrTabRefresh_Tick(object sender, EventArgs e)
        {
            LBA1Tel_teleportScan();
        }
        private void LBA1Tel_teleportScan()
        {
            LBA1Tel_txtXPos.Text = tel.GetXPos().ToString();
            LBA1Tel_txtYPos.Text = tel.GetYPos().ToString();
            LBA1Tel_txtZPos.Text = tel.GetZPos().ToString();
            LBA1Tel_txtFacing.Text = tel.GetFacing().ToString();
        }
        private void BtnLBA1StartStopRefresh_Click(object sender, EventArgs e)
        {
            LBA1Tel_toggleTimer();
        }
        private void LBA1Tel_toggleTimer()
        {
            if (!tmrLBA1TeleportTabRefresh.Enabled)
            {
                tmrLBA1TeleportTabRefresh.Interval = 50;
                tmrLBA1TeleportTabRefresh.Enabled = true;
                LBA1Tel_btnStartStopRefresh.Text = "Stop";
            }
            else
            {
                tmrLBA1TeleportTabRefresh.Enabled = false;
                LBA1Tel_btnStartStopRefresh.Text = "Start";
            }
        }
        private void LBA1Tel_ChkLock_CheckedChanged(object sender, EventArgs e)
        {
            tel.toggleHeightLock((ushort)getInt(LBA1Tel_txtZPos.Text));
        }
        private void LBA1Tel_TmrHeightLock_Tick(object sender, EventArgs e)
        {
            tel.toggleHeightLock((ushort)getInt(LBA1Tel_txtZPos.Text));
        }

        private void LBA1Tel_Update_Click(object sender, EventArgs e)
        {
            tel.UpdateHeight((ushort)getInt(LBA1Tel_txtZPos.Text));
        }*/

    }
}
