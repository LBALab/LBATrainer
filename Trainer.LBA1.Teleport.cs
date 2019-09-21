using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LBATrainer
{
    public partial class frmTrainer
    {
        private void Teleport_Load(object sender, EventArgs e, Options opt)
        {
            txtLBA1TeleportRefreshInterval.Text = opt.LBA1TeleportTabRefreshInterval.ToString();
        }
        private void Teleport_FormClosed(object sender, EventArgs e)
        {
            Options opt = new Options();
            opt.LBA1TeleportTabRefreshInterval = getInt(txtLBA1TeleportRefreshInterval.Text);
            opt.save();
        }

        #region getValues
        private void BtnLBA1TeleportScan_Click(object sender, EventArgs e)
        {
            teleportScan();
        }
        private void teleportScan()
        {
            txtXPos.Text = getXPos().ToString();
            txtYPos.Text = getYPos().ToString();
            txtZPos.Text = getZPos().ToString();
            txtFacing.Text = getFacing().ToString();
        }
        private int getXPos()
        {
            return memRoutines.readAddress(LBA_ONE, 0xD506, 2);
        }
        private int getYPos()
        {
            return memRoutines.readAddress(LBA_ONE, 0xD50A, 2);
        }
        private int getZPos()
        {
            return memRoutines.readAddress(LBA_ONE, 0xD508, 2);
        }
        private int getFacing()
        {
            return memRoutines.readAddress(LBA_ONE, 0xD51E, 2);
        }
        #endregion
        #region setValues
        private void BtnSet_Click(object sender, EventArgs e)
        {
            bool timerEnabled = tmrLBA1TeleportTabRefresh.Enabled;
            if (timerEnabled) toggleTimer();
            short val = (short)getInt(txtZPos.Text);
            if (-1 != val) setZPos((ushort)val);

            val = (short)getInt(txtXPos.Text);
            if (-1 != val) setXPos((ushort)val);

            val = (short)getInt(txtYPos.Text);
            if (-1 != val) setYPos((ushort)val);

            val = (short)getInt(txtFacing.Text);
            if (-1 != val) setFacing((ushort)val);

            if (timerEnabled) toggleTimer();
        }
        //public void writeVal(uint LBAVer, int offset, ushort val, ushort size)
        private void setXPos(ushort val)
        {
            memRoutines.WriteVal(LBA_ONE, 0xD506, val, 2);
        }
        private void setYPos(ushort val)
        {
            memRoutines.WriteVal(LBA_ONE, 0xD50A, val, 2);
        }
        private void setZPos(ushort val)
        {
            memRoutines.WriteVal(LBA_ONE, 0xD508, val, 2);
        }
        private void setFacing(ushort val)
        {
            memRoutines.WriteVal(LBA_ONE, 0xD51E, val, 2);
        }
        int getInt(string value)
        {
            ushort val;
            if (!ushort.TryParse(value, out val)) return -1;
            return val;
        }

        #endregion

        private void TmrLBA1TeleportTabRefresh_Tick(object sender, EventArgs e)
        {
            teleportScan();
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
                btnLBA1StartStopRefresh.Text = "Stop";
            }
            else
            {
                tmrLBA1TeleportTabRefresh.Enabled = false;
                btnLBA1StartStopRefresh.Text = "Start";
            }
        }


        private void ChkLBA1TeleportLock_CheckedChanged(object sender, EventArgs e)
        {
            if (tmrHeightLock.Enabled = chkLBA1TeleportLock.Checked)
                tmrHeightLock.Start();
            else
                tmrHeightLock.Stop();
        }

        private void TmrHeightLock_Tick(object sender, EventArgs e)
        {
            setZPos((ushort)getInt(txtZPos.Text));
        }
    }
}
