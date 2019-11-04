using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LBAMemoryModule;

namespace LBATrainer
{
    public partial class frmTrainer
    {
        Color LBA2SS_COLOUR_ON = Color.Green;
        Color LBA2SS_COLOUR_OFF = Color.Red;
        Timer LBA2SS_tmrGetValues = null;
        enum LBA2SS_Offset : uint { Circle = 0x57CB5, Triangle = 0x57CB7, Square = 0x57CB9 }

        private void LBA2SS_btnOnOff_Click(object sender, EventArgs e)
        {
            if (!LBA2SS_tmrGetValues.Enabled)
            {
                LBA2SS_tmrGetValues = new Timer();
                LBA2SS_tmrGetValues.Interval = 50;
                LBA2SS_tmrGetValues.Tick += tmrLBA2SSGetValues_Tick;
                LBA2SS_tmrGetValues.Start();
                LBA2SS_btnOnOff.Text = "On";
            }
            else
            {
                LBA2SS_tmrGetValues.Stop();
                LBA2SS_btnOnOff.Text = "Off";
            }
        }
        #region getValues
        private bool LBA2SS_getStatus(uint offset)
        {
            return 1 == memRoutines.readVal(offset, 1);
        }
        private bool LBA2SS_getCircleStatus()
        {
            return LBA2SS_getStatus((uint)LBA2SS_Offset.Circle);
        }
        private bool LBA2SS_getTriangleStatus()
        {
            return LBA2SS_getStatus((uint)LBA2SS_Offset.Triangle);
        }
        private bool LBA2SS_getSquareStatus()
        {
            return LBA2SS_getStatus((uint)LBA2SS_Offset.Square);
        }
        #endregion

        private void LBA2SS_toggleValue(uint offset)
        {
            byte val = (byte)memRoutines.readVal(offset, 1);
            val = 1 == val ? (byte)0 : (byte)1;
            memRoutines.WriteVal((int)offset, (ushort)val, 1);
        }
        private void tmrLBA2SSGetValues_Tick(object sender, EventArgs e)
        {
            LBA2SS_toggleColour(LBA2SS_getCircleStatus(), LBA2SS_btnCircle);
            LBA2SS_toggleColour(LBA2SS_getTriangleStatus(), LBA2SS_btnTriangle);
            LBA2SS_toggleColour(LBA2SS_getSquareStatus(), LBA2SS_btnSquare);
        }

        private void LBA2SS_toggleColour(bool on, Button b)
        {
            if (on) b.BackColor = LBA2SS_COLOUR_ON;
            else b.BackColor = LBA2SS_COLOUR_OFF;
        }

        private void LBA2SS_btnCircle_Click(object sender, EventArgs e)
        {
            LBA2SS_toggleValue((uint)LBA2SS_Offset.Circle);
        }

        private void LBA2SS_btnTriangle_Click(object sender, EventArgs e)
        {
            LBA2SS_toggleValue((uint)LBA2SS_Offset.Triangle);
        }

        private void LBA2SS_btnSquare_Click(object sender, EventArgs e)
        {
            LBA2SS_toggleValue((uint)LBA2SS_Offset.Square);
        }
    }
}
