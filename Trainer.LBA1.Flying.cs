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
        #region variables
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
        const Keys keyFlyingSW = Keys.NumPad1;
        const Keys keyFlyingNE = Keys.NumPad9;
        const Keys keyFlyingSE = Keys.NumPad3;
        const Keys keyFlyingNW = Keys.NumPad7;
        const Keys keyFlyingHigher = Keys.Add;
        const Keys keyFlyingLower = Keys.Subtract;
        const Keys keyFlyingW = Keys.NumPad4;
        const Keys keyFlyingS = Keys.NumPad2;
        const Keys keyFlyingN = Keys.NumPad8;
        const Keys keyFlyingE = Keys.NumPad6;
        const Keys keyFlyingToggleLock = Keys.NumPad5;
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
        #region events
        private void Flying_Load(object sender, EventArgs e, Options opt)
        {
            //txtLBA1TeleportRefreshInterval.Text = opt.LBA1TeleportTabRefreshInterval.ToString();
            txtLBA1TeleportMovementPixels.Text = numberOfPixelsToMove.ToString();
            flyingNameButtons();
        }
        private void Flying_FormClosed(object sender, EventArgs e)
        {
            unregisterHotkeysFlying();
        }
        private void TmrFlying_Tick(object sender, EventArgs e)
        {
            setZPos((ushort)flyingHeight);
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
                registerHotKeysFlying();
            }
            else
            {
                btnLBA1FlyingEnabled.Text = "Disabled";
                unregisterHotkeysFlying();
            }
        }
        private void ChkLBA1FlyingHeightLocked_CheckedChanged(object sender, EventArgs e)
        {
            if (chkLBA1FlyingHeightLocked.Checked)
            {
                flyingHeight = getZPos();
            }
            flyingToggleTimer();
        }
        #endregion
        private void flyingNameButtons()
        {
            btnLBA1FlyingN.Text = "N: " + keyFlyingN.ToString();
            btnLBA1FlyingE.Text = "E: " + keyFlyingE.ToString();
            btnLBA1FlyingS.Text = "S: " + keyFlyingS.ToString();
            btnLBA1FlyingW.Text = "W: " + keyFlyingW.ToString();
            btnLBA1FlyingNW.Text = "NW: " + keyFlyingNW.ToString();
            btnLBA1FlyingNE.Text = "NE: " + keyFlyingNE.ToString();
            btnLBA1FlyingSW.Text = "SW: " + keyFlyingSW.ToString();
            btnLBA1FlyingSE.Text = "SE: " + keyFlyingSE.ToString();
            btnLBA1FlyingLockHeight.Text = "Lock: " + keyFlyingToggleLock.ToString();
            btnLBA1FlyingHeightHigher.Text = "Higher: " + keyFlyingHigher.ToString();
            btnLBA1FlyingHeightLower.Text = "Lower: " + keyFlyingLower.ToString();
        }
        #region movementFunctions
        private void moveNorth()
        {
            setYPos((ushort)(getYPos() - numberOfPixelsToMove));
            setXPos((ushort)(getXPos() - numberOfPixelsToMove));
        }
        private void moveWest()
        {
            setYPos((ushort)(getYPos() + numberOfPixelsToMove));
            setXPos((ushort)(getXPos() - numberOfPixelsToMove));
        }
        private void moveSouth()
        {
            setYPos((ushort)(getYPos() + numberOfPixelsToMove));
            setXPos((ushort)(getXPos() + numberOfPixelsToMove));
        }
        private void moveEast()
        {
            setYPos((ushort)(getYPos() - numberOfPixelsToMove));
            setXPos((ushort)(getXPos() + numberOfPixelsToMove));
        }
        private void moveNorthWest()
        {
            setXPos((ushort)(getXPos() - numberOfPixelsToMove));
        }
        private void moveNorthEast()
        {
            setYPos((ushort)(getYPos() - numberOfPixelsToMove));
        }
        private void moveSouthEast()
        {
            setXPos((ushort)(getXPos() + numberOfPixelsToMove));
        }
        private void moveSouthWest()
        {
            setYPos((ushort)(getYPos() + numberOfPixelsToMove));
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
        private void unregisterHotkeysFlying()
        {
            unregisterHotKey(hkFlyingN);
            unregisterHotKey(hkFlyingS);
            unregisterHotKey(hkFlyingW);
            unregisterHotKey(hkFlyingE);
            unregisterHotKey(hkFlyingHigher);
            unregisterHotKey(hkFlyingLower);
            unregisterHotKey(hkFlyingNW);
            unregisterHotKey(hkFlyingNE);
            unregisterHotKey(hkFlyingSW);
            unregisterHotKey(hkFlyingSE);
            unregisterHotKey(hkToggleLock);
        }
        private void registerHotKeysFlying()
        {
            hkFlyingN = registerHotKey(keyFlyingN);
            hkFlyingS = registerHotKey(keyFlyingS);
            hkFlyingW = registerHotKey(keyFlyingW);
            hkFlyingE = registerHotKey(keyFlyingE);
            hkFlyingHigher = registerHotKey(keyFlyingHigher);
            hkFlyingLower = registerHotKey(keyFlyingLower);
            hkFlyingNW = registerHotKey(keyFlyingNW);
            hkFlyingNE = registerHotKey(keyFlyingNE);
            hkFlyingSW = registerHotKey(keyFlyingSW);
            hkFlyingSE = registerHotKey(keyFlyingSE);
            hkToggleLock = registerHotKey(keyFlyingToggleLock);
        }
        private void processHotkeyFlying(Keys k)
        {
            switch (k)
            {
                case keyFlyingN:
                    moveNorth();
                    break;
                case keyFlyingW:
                    moveWest();
                    break;
                case keyFlyingNW:
                    moveNorthWest();
                    break;
                case keyFlyingNE:
                    moveNorthEast();
                    break;
                case keyFlyingS:
                    moveSouth();
                    break;
                case keyFlyingSW:
                    moveSouthWest();
                    break;
                case keyFlyingSE:
                    moveSouthEast();
                    break;
                case keyFlyingE:
                    moveEast();
                    break;
                case keyFlyingHigher:
                    moveHigher();
                    break;
                case keyFlyingLower:
                    moveLower();
                    break;
                case keyFlyingToggleLock:
                    chkLBA1FlyingHeightLocked.Checked = !chkLBA1FlyingHeightLocked.Checked;
                    break;
                default:
                    ;
                    break;
            }
        }
        #endregion


        private void BtnLBA1FlyingNW_Click(object sender, EventArgs e)
        {

        }
        private void flyingToggleTimer()
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
    }
}
