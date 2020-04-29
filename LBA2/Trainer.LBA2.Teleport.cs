using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LBATrainer
{
        public partial class frmTrainer
        {

            /*private void btnLBA2LocationScan_Click(object sender, EventArgs e)
            {
                txtLBA2LocationXPos.Text = memRoutines.readAddress(2, 0x57F35, 2).ToString();
                txtLBA2LocationZPos.Text = memRoutines.readAddress(2, 0x57F39, 2).ToString();
                txtLBA2LocationYPos.Text = memRoutines.readAddress(2, 0x57F3D, 2).ToString();
                txtLBA2LocationFacing.Text = memRoutines.readAddress(2, 0x57F45, 2).ToString();
            }

            private void btnLBA2LocationSet_Click(object sender, EventArgs e)
            {
                memRoutines.WriteVal(2, 0x57F35, (ushort)getInt(txtLBA2LocationXPos.Text), 2);
                memRoutines.WriteVal(2, 0x57F39, (ushort)getInt(txtLBA2LocationZPos.Text), 2);
                memRoutines.WriteVal(2, 0x57F3D, (ushort)getInt(txtLBA2LocationYPos.Text), 2);
            }*/

        /*private void BtnLBA1TeleportScan_Click(object sender, EventArgs e)
        {
            teleportScan();
        }
        private void Teleport_Load(object sender, EventArgs e, Options opt)
        {
            txtLBA1TeleportRefreshInterval.Text = opt.LBA1TeleportTabRefreshInterval.ToString();
            txtLBA1TeleportMovementPixels.Text = numberOfPixelsToMove.ToString();
        }
        private void Teleport_FormClosed(object sender, EventArgs e)
        {
            Options opt = new Options();
            opt.LBA1TeleportTabRefreshInterval = getInt(txtLBA1TeleportRefreshInterval.Text);
            opt.save();
        }
        private void BtnLBA1TeleportSet_Click(object sender, EventArgs e)
        {
            bool timerEnabled = tmrLBA1TeleportTabRefresh.Enabled;
            if (timerEnabled) toggleTimer();
            short val = (short)getInt(txtLBA1TeleportZPos.Text);
            if (-1 != val) LBA1SetZPos((ushort)val);

            val = (short)getInt(txtLBA1TeleportXPos.Text);
            if (-1 != val) LBA1SetXPos((ushort)val);

            val = (short)getInt(txtLBA1TeleportYPos.Text);
            if (-1 != val) LBA1SetYPos((ushort)val);

            val = (short)getInt(txtLBA1TeleportFacing.Text);
            if (-1 != val) LBA1SetFacing((ushort)val);

            if (timerEnabled) toggleTimer();
        }
        private void TmrLBA1TeleportTabRefresh_Tick(object sender, EventArgs e)
        {
            teleportScan();
        }
        private void teleportScan()
        {
            txtLBA1TeleportXPos.Text = getXPos().ToString();
            txtLBA1TeleportYPos.Text = getYPos().ToString();
            txtLBA1TeleportZPos.Text = getZPos().ToString();
            txtLBA1TeleportFacing.Text = getFacing().ToString();
        }
        private void BtnLBA1StartStopRefresh_Click(object sender, EventArgs e)
        {
            toggleTimer();
        }
        private void toggleTimer()
        {
            if (!tmrLBA1TeleportTabRefresh.Enabled)
            {
                int interval;
                if (-1 == (interval = getInt(txtLBA1TeleportRefreshInterval.Text))) return;
                tmrLBA1TeleportTabRefresh.Interval = interval;
                tmrLBA1TeleportTabRefresh.Enabled = true;
                btnLBA1TeleportStartStopRefresh.Text = "Stop";
            }
            else
            {
                tmrLBA1TeleportTabRefresh.Enabled = false;
                btnLBA1TeleportStartStopRefresh.Text = "Start";
            }
        }
        private void ChkLBA1TeleportLock_CheckedChanged(object sender, EventArgs e)
        {
            if (chkLBA1TeleportLock.Checked)
                txtLBA1TeleportZPos.Text = getZPos().ToString();
            if (tmrHeightLock.Enabled = chkLBA1TeleportLock.Checked)
                tmrHeightLock.Start();
            else
                tmrHeightLock.Stop();
        }
        private void TmrHeightLock_Tick(object sender, EventArgs e)
        {
            LBA1SetZPos((ushort)getInt(txtLBA1TeleportZPos.Text));
        }*/

    }
    }
