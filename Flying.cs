using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LBATrainer
{
    class Flying //: IDisposable
    {
        #region variables
        bool disposed = false;
        HotKey hkFlyingN;
        HotKey hkFlyingS;
        HotKey hkFlyingE;
        HotKey hkFlyingW;
        HotKey hkFlyingHigher;
        HotKey hkFlyingLower;
        HotKey hkFlyingNW;
        HotKey hkFlyingNE;
        HotKey hkFlyingSW;
        HotKey hkFlyingSE;
        HotKey hkToggleLock;
        const Keys keyFlyingNW = Keys.NumPad7;
        const Keys keyFlyingN = Keys.NumPad8;
        const Keys keyFlyingNE = Keys.NumPad9;
        const Keys keyFlyingW = Keys.NumPad4;
        const Keys keyFlyingToggleLock = Keys.NumPad5;
        const Keys keyFlyingE = Keys.NumPad6;
        const Keys keyFlyingSW = Keys.NumPad1;
        const Keys keyFlyingS = Keys.NumPad2;
        const Keys keyFlyingSE = Keys.NumPad3;
        const Keys keyFlyingHigher = Keys.Add;
        const Keys keyFlyingLower = Keys.Subtract;
        /*const Keys keyFlyingSW = Keys.Z;
        const Keys keyFlyingNE = Keys.E;
        const Keys keyFlyingSE = Keys.C;
        const Keys keyFlyingNW = Keys.Q;
        const Keys keyFlyingHigher = Keys.R;
        const Keys keyFlyingLower = Keys.F;
        const Keys keyFlyingW = Keys.A;
        const Keys keyFlyingS = Keys.X;
        const Keys keyFlyingN = Keys.W;
        const Keys keyFlyingE = Keys.D;
        const Keys keyFlyingToggleLock = Keys.S;*/
        ushort numberOfPixelsToMove = 100;
        bool flyingEnabled = false;
        int flyingHeight;
        const int flyingTimerInterval = 50;
        #endregion
        //#region events


        public Flying()
        {
            //flyingNameButtons();
        }

        public Flying(ushort pixelsToMove)
        {
            numberOfPixelsToMove = pixelsToMove;
        }
        /*
        private void LBA1Flying_FormClosed(object sender, EventArgs e)
        {
            unregisterHotkeysLBA1Flying();
        }
        private void LBA1TmrFlying_Tick(object sender, EventArgs e)
        {
            LBA1SetZPos((ushort)flyingHeight);
            //setZPos((ushort)(getGroundHeight() + flyingHeight));
        }
        private void BtnLBA1TeleportSetMovementPixels_Click(object sender, EventArgs e)
        {
            numberOfPixelsToMove = (ushort)getInt(txtLBA1TeleportMovementPixels.Text);
        }
        private void BtnLBA1FlyingEnabled_Click(object sender, EventArgs e)
        {
            if (flyingEnabled = !flyingEnabled)
            {
                btnLBA1FlyingEnabled.Text = "Enabled";
                registerHotKeysLBA1Flying();
            }
            else
            {
                btnLBA1FlyingEnabled.Text = "Disabled";
                unregisterHotkeysLBA1Flying();
            }
        }
        private void ChkLBA1FlyingHeightLocked_CheckedChanged(object sender, EventArgs e)
        {
            if (chkLBA1FlyingHeightLocked.Checked)
            {
                flyingHeight = getZPos();
            }
            LBA1FlyingToggleTimer();
        }
        #endregion
        private void flyingNameButtons()
        {
            btnLBA1FlyingN.Text = "N: " + keyLBA1FlyingN.ToString();
            btnLBA1FlyingE.Text = "E: " + keyLBA1FlyingE.ToString();
            btnLBA1FlyingS.Text = "S: " + keyLBA1FlyingS.ToString();
            btnLBA1FlyingW.Text = "W: " + keyLBA1FlyingW.ToString();
            btnLBA1FlyingNW.Text = "NW: " + keyLBA1FlyingNW.ToString();
            btnLBA1FlyingNE.Text = "NE: " + keyLBA1FlyingNE.ToString();
            btnLBA1FlyingSW.Text = "SW: " + keyLBA1FlyingSW.ToString();
            btnLBA1FlyingSE.Text = "SE: " + keyLBA1FlyingSE.ToString();
            btnLBA1FlyingLockHeight.Text = "Lock: " + keyLBA1FlyingToggleLock.ToString();
            btnLBA1FlyingHeightHigher.Text = "Higher: " + keyLBA1FlyingHigher.ToString();
            btnLBA1FlyingHeightLower.Text = "Lower: " + keyLBA1FlyingLower.ToString();
        }
        #region movementFunctions
        private void moveNorth()
        {
            LBA1SetYPos((ushort)(getYPos() - numberOfPixelsToMove));
            LBA1SetXPos((ushort)(getXPos() - numberOfPixelsToMove));
        }
        private void moveWest()
        {
            LBA1SetYPos((ushort)(getYPos() + numberOfPixelsToMove));
            LBA1SetXPos((ushort)(getXPos() - numberOfPixelsToMove));
        }
        private void moveSouth()
        {
            LBA1SetYPos((ushort)(getYPos() + numberOfPixelsToMove));
            LBA1SetXPos((ushort)(getXPos() + numberOfPixelsToMove));
        }
        private void moveEast()
        {
            LBA1SetYPos((ushort)(getYPos() - numberOfPixelsToMove));
            LBA1SetXPos((ushort)(getXPos() + numberOfPixelsToMove));
        }
        private void moveNorthWest()
        {
            LBA1SetXPos((ushort)(getXPos() - numberOfPixelsToMove));
        }
        private void moveNorthEast()
        {
            LBA1SetYPos((ushort)(getYPos() - numberOfPixelsToMove));
        }
        private void moveSouthEast()
        {
            LBA1SetXPos((ushort)(getXPos() + numberOfPixelsToMove));
        }
        private void moveSouthWest()
        {
            LBA1SetYPos((ushort)(getYPos() + numberOfPixelsToMove));
        }
        private void moveHigher()
        {
            flyingHeight += numberOfPixelsToMove;
        }
        private void moveLower()
        {
            flyingHeight -= numberOfPixelsToMove;
        }
        #endregion
        #region hotkeyHandling
        private void unregisterHotkeysLBA1Flying()
        {
            unregisterHotKey(hkLBA1FlyingN);
            unregisterHotKey(hkLBA1FlyingS);
            unregisterHotKey(hkLBA1FlyingW);
            unregisterHotKey(hkLBA1FlyingE);
            unregisterHotKey(hkLBA1FlyingHigher);
            unregisterHotKey(hkLBA1FlyingLower);
            unregisterHotKey(hkLBA1FlyingNW);
            unregisterHotKey(hkLBA1FlyingNE);
            unregisterHotKey(hkLBA1FlyingSW);
            unregisterHotKey(hkLBA1FlyingSE);
            unregisterHotKey(hkLBA1ToggleLock);
        }
        private void registerHotKeysLBA1Flying()
        {
            hkLBA1FlyingN = registerHotKey(keyLBA1FlyingN);
            hkLBA1FlyingS = registerHotKey(keyLBA1FlyingS);
            hkLBA1FlyingW = registerHotKey(keyLBA1FlyingW);
            hkLBA1FlyingE = registerHotKey(keyLBA1FlyingE);
            hkLBA1FlyingHigher = registerHotKey(keyLBA1FlyingHigher);
            hkLBA1FlyingLower = registerHotKey(keyLBA1FlyingLower);
            hkLBA1FlyingNW = registerHotKey(keyLBA1FlyingNW);
            hkLBA1FlyingNE = registerHotKey(keyLBA1FlyingNE);
            hkLBA1FlyingSW = registerHotKey(keyLBA1FlyingSW);
            hkLBA1FlyingSE = registerHotKey(keyLBA1FlyingSE);
            hkLBA1ToggleLock = registerHotKey(keyLBA1FlyingToggleLock);
        }
        private void processHotkeyLBA1Flying(Keys k)
        {
            switch (k)
            {
                case keyLBA1FlyingN:
                    moveNorth();
                    break;
                case keyLBA1FlyingW:
                    moveWest();
                    break;
                case keyLBA1FlyingNW:
                    moveNorthWest();
                    break;
                case keyLBA1FlyingNE:
                    moveNorthEast();
                    break;
                case keyLBA1FlyingS:
                    moveSouth();
                    break;
                case keyLBA1FlyingSW:
                    moveSouthWest();
                    break;
                case keyLBA1FlyingSE:
                    moveSouthEast();
                    break;
                case keyLBA1FlyingE:
                    moveEast();
                    break;
                case keyLBA1FlyingHigher:
                    moveHigher();
                    break;
                case keyLBA1FlyingLower:
                    moveLower();
                    break;
                case keyLBA1FlyingToggleLock:
                    chkLBA1FlyingHeightLocked.Checked = !chkLBA1FlyingHeightLocked.Checked;
                    break;
                default:
                    ;
                    break;
            }
        }
        #endregion

        private void LBA1FlyingToggleTimer()
        {
            if (!tmrFlying.Enabled)
            {
                tmrFlying.Interval = flyingTimerInterval;
                tmrFlying.Enabled = true;
                tmrFlying.Start();
            }
            else
            {
                tmrFlying.Enabled = false;
                tmrFlying.Stop();
            }
        }

        public void Dispose()
        {
            // Dispose of unmanaged resources.
            Dispose(true);
        }

        // Protected implementation of Dispose pattern.
        protected virtual void Dispose(bool disposing)
        {
            
            if (disposed)
                return;

            if (disposing)
            {
                unregisterHotkeysLBA1Flying();
            }

            
            disposed = true;
        }*/
    }
}
