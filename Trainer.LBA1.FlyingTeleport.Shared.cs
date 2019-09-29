using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LBATrainer
{
    public partial class frmTrainer
    {
        #region setValues
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
        private void setGroundHeight(ushort val)
        {
            memRoutines.WriteVal(LBA_ONE, 0xC2BA, val, 2);
        }
        #endregion
        #region getValues


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
        private int getGroundHeight()
        {
            return memRoutines.readAddress(LBA_ONE, 0xC2BA, 2);
        }
        #endregion
    }
}
