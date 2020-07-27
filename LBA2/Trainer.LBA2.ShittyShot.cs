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
        const string LBA2SS_sBtnOn = "Status: On";
        const string LBA2SS_sBtnOff = "Status: Off";
        enum LBA2SS_Offset : uint { Circle = 0x57CB5, Triangle = 0x57CB7, Square = 0x57CB9 }

        private void LBA2SS_btnOnOff_Click(object sender, EventArgs e)
        {
            //If already contains
            if (tgi.Contains((uint)LBA2SS_Offset.Circle))
            {
                tgi.RemoveIfExists((uint)LBA2SS_Offset.Circle);
                tgi.RemoveIfExists((uint)LBA2SS_Offset.Square);
                tgi.RemoveIfExists((uint)LBA2SS_Offset.Triangle);
                LBA2SS_btnOnOff.Text = LBA2SS_sBtnOff;
            }
            else
            {
                tgi.AddItem(LBA2SS_setCircle, (uint)LBA2SS_Offset.Circle, 1);
                tgi.AddItem(LBA2SS_setSquare, (uint)LBA2SS_Offset.Square, 1);
                tgi.AddItem(LBA2SS_setTriangle, (uint)LBA2SS_Offset.Triangle, 1);
                LBA2SS_btnOnOff.Text = LBA2SS_sBtnOn;
            }
        }
        private void LBA2SS_setCircle(ushort val)
        {
            LBA2SS_toggleColour(1 == val, LBA2SS_btnCircle);
        }
        private void LBA2SS_setSquare(ushort val)
        {
            LBA2SS_toggleColour(1 == val, LBA2SS_btnSquare);
        }
        private void LBA2SS_setTriangle(ushort val)
        {
            LBA2SS_toggleColour(1 == val, LBA2SS_btnTriangle);
        }

        private void LBA2SS_toggleValue(uint offset)
        {
            byte val = (byte)memRoutines.readVal(offset, 1);
            val = 1 == val ? (byte)0 : (byte)1;
            memRoutines.WriteVal((int)offset, (ushort)val, 1);
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
