using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LBAMemoryModule;

namespace LBATrainer
{
    class oTeleport
    {
        LBAMemoryModule.Mem memRoutines = new Mem();
        oTimerSetItems otsiTeleport;
        byte LBAVer;
        enum LBA1Offsets : uint { XPos = 0xD506, YPos = 0xD50A, ZPos = 0xD508, Facing = 0xD51E }
        enum LBA2Offsets : uint { XPos = 0x57F3D, YPos = 0x57F35, ZPos = 0x57F39, Facing = 0x12FFBA }
        public oTeleport()
        {
            initialLoad( memRoutines.DetectLBAVersion());
        }
        public oTeleport(byte LBAVer)
        {
            initialLoad(LBAVer);
        }

        private void initialLoad(byte LBAVer)
        {
            this.LBAVer = LBAVer;
            otsiTeleport = new oTimerSetItems((oTimerSetItems.LBAVersion) LBAVer);
        }

        #region getValues
        public int GetXPos()
        {
            return memRoutines.readAddress(LBAVer, XOffset(), 2);
        }
        public int GetYPos()
        {
            return memRoutines.readAddress(LBAVer, YOffset(), 2);
        }
        public int GetZPos()
        {
            return memRoutines.readAddress(LBAVer, ZOffset(), 2);
        }
        public int GetFacing()
        {
            return memRoutines.readAddress(LBAVer, FacingOffset(), 2);
        }
        #endregion
        private uint XOffset()
        {
            return 1 == LBAVer ? (uint)LBA1Offsets.XPos : (uint)LBA2Offsets.XPos;
        }
        private uint YOffset()
        {
            return 1 == LBAVer ? (uint)LBA1Offsets.YPos : (uint)LBA2Offsets.YPos; ;
        }
        public uint ZOffset()
        {
            return 1 == LBAVer ? (uint)LBA1Offsets.ZPos : (uint)LBA2Offsets.ZPos;
        }
        private uint FacingOffset()
        {
            return 1 == LBAVer ? (uint)LBA1Offsets.Facing : (uint)LBA2Offsets.Facing;
        }
        #region setValues
        public void SetXPos(ushort val)
        {
            memRoutines.WriteVal(LBAVer, (int)XOffset(), val, 2);
        }
        public void SetYPos(ushort val)
        {
            memRoutines.WriteVal(LBAVer, (int)YOffset(), val, 2);
        }
        public void SetZPos(ushort val)
        {
            memRoutines.WriteVal(LBAVer, (int)ZOffset(), val, 2);
        }
        public void SetFacing(ushort val)
        {
            memRoutines.WriteVal(LBAVer, (int)FacingOffset(), val, 2);
        }

        public void toggleHeightLock(ushort val)
        {
            if(!otsiTeleport.RemoveIfExists(ZOffset())) 
                otsiTeleport.AddItem(ZOffset(), val, 2);
        }

        public void UpdateHeight(ushort val)
        {
            otsiTeleport.UpdateItem(ZOffset(), val);
        }
        #endregion
    }
}
