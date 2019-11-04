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
        private void LBA1Tel_BtnScan_Click(object sender, EventArgs e)
        {
            LBA1Tel_teleportScan();
        }
        private void LBA1Tel_Load(object sender, EventArgs e, Options opt)
        {
            LBA1Tel_txtRefreshInterval.Text = opt.LBA1TeleportTabRefreshInterval.ToString();
            txtLBA1TeleportMovementPixels.Text = numberOfPixelsToMove.ToString();
        }
        private void LBA1Tel_FormClosed(object sender, EventArgs e)
        {
            Options opt = new Options();
            opt.LBA1TeleportTabRefreshInterval = getInt(LBA1Tel_txtRefreshInterval.Text);
            opt.save();
        }
        private void LBA1Tel_BtntSet_Click(object sender, EventArgs e)
        {
            bool timerEnabled = tmrLBA1TeleportTabRefresh.Enabled;
            if (timerEnabled) LBA1Tel_toggleTimer();
            short val = (short)getInt(LBA1Tel_txtZPos.Text);
            if (-1 != val) LBA1SetZPos((ushort)val);

            val = (short)getInt(LBA1Tel_txtXPos.Text);
            if (-1 != val) LBA1SetXPos((ushort)val);

            val = (short)getInt(LBA1Tel_txtYPos.Text);
            if (-1 != val) LBA1SetYPos((ushort)val);

            val = (short)getInt(LBA1Tel_txtFacing.Text);
            if (-1 != val) LBA1SetFacing((ushort)val);

            if (timerEnabled) LBA1Tel_toggleTimer();
        }
        private void LBA1Tel_TmrTabRefresh_Tick(object sender, EventArgs e)
        {
            LBA1Tel_teleportScan();
        }
        private void LBA1Tel_teleportScan()
        {
            LBA1Tel_txtXPos.Text = getXPos().ToString();
            LBA1Tel_txtYPos.Text = getYPos().ToString();
            LBA1Tel_txtZPos.Text = getZPos().ToString();
            LBA1Tel_txtFacing.Text = getFacing().ToString();
        }
        private void BtnLBA1StartStopRefresh_Click(object sender, EventArgs e)
        {
            LBA1Tel_toggleTimer();
        }
        private void LBA1Tel_toggleTimer()
        {
            if (!tmrLBA1TeleportTabRefresh.Enabled)
            {
                int interval;
                if (-1 == (interval = getInt(LBA1Tel_txtRefreshInterval.Text))) return;
                tmrLBA1TeleportTabRefresh.Interval = interval;
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
            if (LBA1Tel_chkLock.Checked)
                LBA1Tel_txtZPos.Text = getZPos().ToString();
            if (tmrHeightLock.Enabled = LBA1Tel_chkLock.Checked)
                tmrHeightLock.Start();
            else
                tmrHeightLock.Stop();
        }
        private void LBA1Tel_TmrHeightLock_Tick(object sender, EventArgs e)
        {
            LBA1SetZPos((ushort)getInt(LBA1Tel_txtZPos.Text));
        }

    }
}
