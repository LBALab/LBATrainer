using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LBATrainer
{
    public partial class frmTrainer : Form
    {
        const ushort LBA_ONE = 1;
        const ushort LBA_TWO = 2;
        private Items items;
        private mem memRoutines;
        CheckBox chkLBA2InventoryValue;
        TextBox txtLBA2InventoryValue;
        private HotKey hotkeyF7;
        //private HotKey hotkeyF8;
        private HotKey hotkeyF9;
        public frmTrainer()
        {
            InitializeComponent();
            scan(LBA_ONE);
            SetDoubleBuffered(tcLBAVersion);
        }


        #region General
        #region setDoubleBuffered
        /**
         * Used to stop flickering on interface update
         * caused by constantly updating with times
         */
        public static void SetDoubleBuffered(Control control)
        {
            // set instance non-public property with name "DoubleBuffered" to true
            typeof(Control).InvokeMember("DoubleBuffered",
                BindingFlags.SetProperty | BindingFlags.Instance | BindingFlags.NonPublic,
                null, control, new object[] { true });
        }
        #endregion
        private void TcLBAVersion_SelectedIndexChanged(object sender, EventArgs e)
        {
            scan((uint)tcLBAVersion.SelectedIndex + 1);
        }
        private void maxAll(uint LBAVer)
        {
            for (int i = 0; i < items.Inventory.Count();    i++)
            {
                memRoutines.WriteVal(LBAVer, items.Inventory[i], items.Inventory[i].maxVal);
            }
            for (int i = 0; i < items.Twinsen.Count(); i++)
            {
                memRoutines.WriteVal(LBAVer, items.Twinsen[i], items.Twinsen[i].maxVal);
            }
            scan(LBAVer);
        }
        private void MinAll(uint LBAVer)
        {
            for (int i = 0; i < items.Inventory.Count(); i++)
            {
                memRoutines.WriteVal(LBAVer, items.Inventory[i], items.Inventory[i].minVal);
            }
            for (int i = 0; i < items.Twinsen.Count(); i++)
            {
                memRoutines.WriteVal(LBAVer, items.Twinsen[i], items.Twinsen[i].minVal);
            }
            scan(LBAVer);
        }
        private void scan(uint LBAVer)
        {
            if (1 == LBAVer)
                scanLBA1();
            else
                scanLBA2();
        }
        #endregion
        #region LBA1
        private void BtnLBA1MaxAll_Click(object sender, EventArgs e)
        {
            maxAll(LBA_ONE);
        }
        private void BtnLBA1Scan_Click(object sender, EventArgs e)
        {
            scan(LBA_ONE);
        }
        private void BtnLBA1MinAll_Click(object sender, EventArgs e)
        {
            MinAll(LBA_ONE);
        }        
        private void CboLBA1Inventory_SelectedIndexChanged(object sender, EventArgs e)
        {
            Item itm = (Item)cboLBA1Inventory.SelectedItem;
            chkLBA1InventoryValue.Checked = 1 == memRoutines.getVal(LBA_ONE, itm);
        }
        private void ChkLBA1InventoryValue_CheckedChanged(object sender, EventArgs e)
        {
            ushort val;
            if (chkLBA1InventoryValue.Checked)
                val = 1;
            else
                val = 0;
            if (-1 == cboLBA1Inventory.SelectedIndex) return;
            memRoutines.WriteVal(LBA_ONE, (Item)cboLBA1Inventory.SelectedItem, val);
        }
        private void scanLBA1()
        {
            int val;
            items = new Items(LBA_ONE);
            memRoutines = new mem();
            val = memRoutines.getVal(LBA_ONE, items.MagicLevel);
            if (-1 == val) return;
            txtLBA1MagicLevel.Text = val.ToString();
            txtLBA1MagicPoints.Text = memRoutines.getVal(LBA_ONE, items.MagicPoints).ToString();
            txtLBA1Kashers.Text = memRoutines.getVal(LBA_ONE, items.Kashers).ToString();
            txtLBA1Keys.Text = memRoutines.getVal(LBA_ONE, items.Keys).ToString();
            txtLBA1Clovers.Text = memRoutines.getVal(LBA_ONE, items.Clovers).ToString();
            txtLBA1CloverBoxes.Text = memRoutines.getVal(LBA_ONE, items.CloverBoxes).ToString();
            txtLBA1Gas.Text = memRoutines.getVal(LBA_ONE, items.Gas).ToString();
            txtLBA1Health.Text = memRoutines.getVal(LBA_ONE, items.Health).ToString();
            cboLBA1Inventory.Items.Clear();
            cboLBA1Inventory.Items.AddRange(items.Inventory);
            populateLBA1Quest();
            populateLBA1Movies();
            teleportScan();
        }
        private void BtnLBA1Set_Click(object sender, EventArgs e)
        {
            memRoutines.WriteVal(LBA_ONE, items.CloverBoxes, txtLBA1CloverBoxes.Text);
            memRoutines.WriteVal(LBA_ONE, items.Clovers, txtLBA1Clovers.Text);
            memRoutines.WriteVal(LBA_ONE, items.MagicLevel, txtLBA1MagicLevel.Text);
            memRoutines.WriteVal(LBA_ONE, items.MagicPoints, txtLBA1MagicPoints.Text);
            memRoutines.WriteVal(LBA_ONE, items.Health, txtLBA1Health.Text);
            memRoutines.WriteVal(LBA_ONE, items.Keys, txtLBA1Keys.Text);
            memRoutines.WriteVal(LBA_ONE, items.Kashers, txtLBA1Kashers.Text);
            memRoutines.WriteVal(LBA_ONE, items.Gas, txtLBA1Gas.Text);
        }
        private void TcLBA1Inner_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(1 == tcLBA1Inner.SelectedIndex)
                populateLBA1Quest();
        }

        #region LBA1OtherTab
        private void populateLBA1Quest()
        {
            cboLBA1Quest.Items.Clear();
            cboLBA1Quest.Items.AddRange(items.Quest);
        }

        private void populateLBA1Movies()
        {
            cboLBA1Movies.Items.Clear();
            cboLBA1Movies.Items.AddRange(items.Movies);
        }


        private void ChkLBA1QuestValue_CheckedChanged(object sender, EventArgs e)
        {
            ushort val;
            if (chkLBA1QuestValue.Checked)
                val = 1;
            else
                val = 0;
            if (-1 == cboLBA1Quest.SelectedIndex) return;
            memRoutines.WriteVal(LBA_ONE, (Item)cboLBA1Quest.SelectedItem, val);
        }

        private void CboLBA1Quest_SelectedIndexChanged(object sender, EventArgs e)
        {
            Item itm = (Item)cboLBA1Quest.SelectedItem;
            chkLBA1QuestValue.Checked = 1 == memRoutines.getVal(LBA_ONE, itm);
        }

        private void cboLBA1Movies_SelectedIndexChanged(object sender, EventArgs e)
        {
            Item itm = (Item)cboLBA1Movies.SelectedItem;
            chkLBA1MoviesValue.Checked = 1 == memRoutines.getVal(LBA_ONE, itm);
        }

        private void ChkLBA1MoviesValue_CheckedChanged(object sender, EventArgs e)
        {
            ushort val;
            if (chkLBA1MoviesValue.Checked)
                val = 1;
            else
                val = 0;
            if (-1 == cboLBA1Movies.SelectedIndex) return;
            memRoutines.WriteVal(LBA_ONE, (Item)cboLBA1Movies.SelectedItem, val);
        }
        #endregion

        #endregion
        #region LBA2
        private void BtnLBA2MaxAll_Click(object sender, EventArgs e)
        {
            maxAll(LBA_TWO);
        }
        private void BtnLBA2MinAll_Click(object sender, EventArgs e)
        {
            MinAll(LBA_TWO);
        }
        private void BtnLBA2Scan_Click(object sender, EventArgs e)
        {
            scan(LBA_TWO);
        }
        private void scanLBA2()
        {
            int val;
            items = new Items(LBA_TWO);
            memRoutines = new mem();
            val = memRoutines.getVal(LBA_TWO, items.MagicLevel);
            if (-1 == val) return;
            txtLBA2MagicLevel.Text = val.ToString();
            txtLBA2MagicPoints.Text = memRoutines.getVal(LBA_TWO, items.MagicPoints).ToString();
            txtLBA2Kashers.Text = memRoutines.getVal(LBA_TWO, items.Kashers).ToString();
            txtLBA2Keys.Text = memRoutines.getVal(LBA_TWO, items.Keys).ToString();
            txtLBA2Clovers.Text = memRoutines.getVal(LBA_TWO, items.Clovers).ToString();
            txtLBA2CloverBoxes.Text = memRoutines.getVal(LBA_TWO, items.CloverBoxes).ToString();
            txtLBA2Health.Text = memRoutines.getVal(LBA_TWO, items.Health).ToString();
            cboLBA2Inventory.Items.Clear();
            cboLBA2Inventory.Items.AddRange(items.Inventory);
            addLBA2InventoryCheckbox(null);
        }
        private void CboLBA2Inventory_SelectedIndexChanged(object sender, EventArgs e)
        {
            Item itm = (Item)cboLBA2Inventory.SelectedItem;
            if (Item.TYPE_BITFLAG == itm.type)
                addLBA2InventoryCheckbox(itm);
            else
                addLBA2InventoryTextbox(itm);
        }
        private void addLBA2InventoryCheckbox(Item itm)
        {
            if (tpLBA2Twinsen.Controls.Contains(chkLBA2InventoryValue))
            {
                chkLBA2InventoryValue.Checked = 1 == memRoutines.getVal(LBA_TWO, itm);
                return;
            }
            if(tpLBA2Twinsen.Controls.Contains(txtLBA2InventoryValue))
                tpLBA2Twinsen.Controls.Remove(txtLBA2InventoryValue);
            chkLBA2InventoryValue = new CheckBox();
            chkLBA2InventoryValue.Size = new Size(15, 14);
            chkLBA2InventoryValue.Location = new Point(284, 116);
            chkLBA2InventoryValue.CheckedChanged += new System.EventHandler(this.ChkLBA2InventoryValue_CheckedChanged);
            if(null != itm)chkLBA2InventoryValue.Checked = 1 == memRoutines.getVal(LBA_TWO, itm);
            tpLBA2Twinsen.Controls.Add(chkLBA2InventoryValue);
        }
        private void addLBA2InventoryTextbox(Item itm)
        {
            if (tpLBA2Twinsen.Controls.Contains(txtLBA2InventoryValue))
            {
                txtLBA2InventoryValue.Text = memRoutines.getVal(LBA_TWO, itm).ToString();
                return;
            }
            if (tpLBA2Twinsen.Controls.Contains(chkLBA2InventoryValue))
                tpLBA2Twinsen.Controls.Remove(chkLBA2InventoryValue);
            txtLBA2InventoryValue = new TextBox();
            txtLBA2InventoryValue.Text = memRoutines.getVal(LBA_TWO, itm).ToString();
            txtLBA2InventoryValue.Size = new Size(44, 20);
            txtLBA2InventoryValue.Location = new Point(284, 114);
            txtLBA2InventoryValue.TextChanged += new System.EventHandler(this.TxtLBA2InventoryValue_TextChanged);
            tpLBA2Twinsen.Controls.Add(txtLBA2InventoryValue);
        }
        private void TxtLBA2InventoryValue_TextChanged(object sender, EventArgs e)
        {
            if (-1 == cboLBA2Inventory.SelectedIndex) return;
            if (0 >= txtLBA2InventoryValue.Text.Length) return;
            memRoutines.WriteVal(LBA_TWO, (Item)cboLBA2Inventory.SelectedItem, txtLBA2InventoryValue.Text);
        }
        private void ChkLBA2InventoryValue_CheckedChanged(object sender, EventArgs e)
        {
            ushort val;
            if (chkLBA2InventoryValue.Checked)
                val = 1;
            else
                val = 0;
            if (-1 == cboLBA2Inventory.SelectedIndex) return;
            memRoutines.WriteVal(LBA_TWO, (Item)cboLBA2Inventory.SelectedItem, val);
        }        
        private void BtnLBA2TwinsenSet_Click(object sender, EventArgs e)
        {
            memRoutines.WriteVal(LBA_TWO, items.CloverBoxes, txtLBA2CloverBoxes.Text);
            memRoutines.WriteVal(LBA_TWO, items.Clovers, txtLBA2Clovers.Text);
            memRoutines.WriteVal(LBA_TWO, items.MagicLevel, txtLBA2MagicLevel.Text);
            memRoutines.WriteVal(LBA_TWO, items.MagicPoints, txtLBA2MagicPoints.Text);
            memRoutines.WriteVal(LBA_TWO, items.Health, txtLBA2Health.Text);
            memRoutines.WriteVal(LBA_TWO, items.Keys, txtLBA2Keys.Text);
            memRoutines.WriteVal(LBA_TWO, items.Kashers, txtLBA2Kashers.Text);
            memRoutines.WriteVal(LBA_TWO, items.Zilitos, txtLBA2Zilitos.Text);
        }
        #endregion

        #region LBA1Teleport

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
            memRoutines.WriteVal(LBA_ONE, 0xD51E, val,2);
        }
        int getInt(string value)
        {
            ushort val;
            if (!ushort.TryParse(value, out val)) return -1;
            return val;
        }

        #endregion

        #endregion

        private void RefreshToolStripMenuItem_Click(object sender, EventArgs e)
        {
            memRoutines = new mem();
        }
        private void BtnSetSaveFileDir_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbdLBADir = new FolderBrowserDialog();
            fbdLBADir.ShowDialog();
            txtLBA1SaveFileDirectory.Text = fbdLBADir.SelectedPath;
            fbdLBADir.Dispose();
            Options opt = new Options();
            opt.LBADir = txtLBA1SaveFileDirectory.Text;
            opt.save();
        }

        private void FrmTrainer_FormClosed(object sender, FormClosedEventArgs e)
        {
            unregisterHotkeys();

            string[] readOnlySaves = getReadonlySaves();
            if (null != readOnlySaves && 0 != readOnlySaves.Length)
                if (DialogResult.Yes == MessageBox.Show("There are currently " + readOnlySaves.Length + " save files marked as readonly, would you like to remove this?", "Remove Readonly", MessageBoxButtons.YesNo))
                {
                    for (int i = 0; i < readOnlySaves.Length; i++)
                        new FileInfo(readOnlySaves[i]) { IsReadOnly = false }.Refresh(); ;
                }
            Options opt = new Options();
            opt.LBA1TeleportTabRefreshInterval = getInt(txtLBA1TeleportRefreshInterval.Text);
            opt.save();
        }

        private void registerHotKeys()
        {
            hotkeyF7 = new HotKey(this.Handle);
            hotkeyF7.RegisterHotKeys((int)Keys.F7, (uint)Keys.F7);
            /*
            hotkeyF8 = new HotKey(this.Handle);
            hotkeyF8.RegisterHotKeys((int)Keys.F8, (uint)Keys.F8);*/
            hotkeyF9 = new HotKey(this.Handle);
            hotkeyF9.RegisterHotKeys((int)Keys.F9, (uint)Keys.F9);
        }

        private string[] getReadonlySaves()
        {

            if (string.IsNullOrEmpty(txtLBA1SaveFileDirectory.Text)) return null;
            if (string.IsNullOrWhiteSpace(txtLBA1SaveFileDirectory.Text)) return null;
            if (!System.IO.Directory.Exists(txtLBA1SaveFileDirectory.Text)) return null;
            int readOnlyCount = 0;
            string[] filePaths = Directory.GetFiles(txtLBA1SaveFileDirectory.Text, "*.lba");
            for (int i = 0; i < filePaths.Length; i++)
                if (new FileInfo(filePaths[i]).IsReadOnly) readOnlyCount++;
            string[] readOnly = new string[readOnlyCount];
            for (int i = 0, j = 0; i < filePaths.Length; i++)
                if (new FileInfo(filePaths[i]).IsReadOnly)
                {
                    readOnly[j] = filePaths[i];
                    j++;
                }

            return readOnly;
        }

        private void FrmTrainer_Load(object sender, EventArgs e)
        {
            Options opt = new Options();
            txtLBA1SaveFileDirectory.Text = opt.LBADir;
            txtLBA1TeleportRefreshInterval.Text = opt.LBA1TeleportTabRefreshInterval.ToString();
        }

        protected override void WndProc(ref Message keyPressed)
        {
            if (keyPressed.Msg == 0x0312)
            {
                if (118 == (int)keyPressed.WParam)
                {
                    SaveGame sg = new SaveGame();
                    if (!sg.save(txtLBA1SaveFileDirectory.Text)) MessageBox.Show("Unable to save game, is DOSBox running?");
                }
                /*if (119 == (int)keyPressed.WParam)
                    MessageBox.Show("F8");*/
                //Add a key :-)
                if (120 == (int)keyPressed.WParam)
                    memRoutines.WriteVal(LBA_ONE, 0xE26, 1, 1);
            }
            base.WndProc(ref keyPressed);
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
        private void TmrLBA1TeleportTabRefresh_Tick(object sender, EventArgs e)
        {
            teleportScan();
        }

        private void unregisterHotkeys()
        {
            try
            {
                hotkeyF7.UnRegisterHotKeys();
                //hotkeyF8.UnRegisterHotKeys();
                hotkeyF9.UnRegisterHotKeys();
            }
            catch { };
        }
        private void BtnLBA1SaveGameEnableDisable_Click(object sender, EventArgs e)
        {
            if (null == hotkeyF7)
            {
                registerHotKeys();
                btnLBA1SaveGameEnableDisable.Text = "Disable";
            }
            else
            {
                unregisterHotkeys();
                btnLBA1SaveGameEnableDisable.Text = "Enable";
            }

        }
    }
}
