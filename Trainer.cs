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
using LBAMemoryModule;

namespace LBATrainer
{
    /**
     * Shared stuff goes in here
     */
    public partial class frmTrainer : Form
    {
        const byte LBA_ONE = 1;
        const byte LBA_TWO = 2;
        private Items items;
        private Mem memRoutines;
        oTimerSetItems tsi;
        oTimerGetItems tgi;
        public frmTrainer()
        {
            InitializeComponent();
            memRoutines = new Mem();
            scan(memRoutines.DetectLBAVersion());
            SetDoubleBuffered(tcLBAVersion);
            tsi = new oTimerSetItems(oTimerSetItems.LBAVersion.One);
            tgi = new oTimerGetItems();
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
        #endregion

        private void FrmTrainer_Load(object sender, EventArgs e)
        {
            Reload(sender, e);
        }
        private void FrmTrainer_FormClosed(object sender, FormClosedEventArgs e)
        {
            LBA1SG_FormClosed(sender, e);
        }
        private int getInt(string value)
        {
            int val;
            if (!int.TryParse(value, out val)) return -1;
            return val;
        }
        //To be triggered from TextChanged event
        private void filterCBO(ComboBox cb, Item[] itms)
        {
            //If not entering data i.e. empty field
            if (-1 != cb.SelectedIndex) return;

            cb.Items.Clear();
            for (int i = 0; i < itms.Length; i++)
                if (itms[i].name.ToLower().Contains(cb.Text.ToLower()))
                    cb.Items.Add(itms[i]);

            cb.SelectionStart = cb.Text.Length;
            cb.SelectionLength = 0;
        }

        #region hotkey
        private HotKey registerHotKey(Keys k)
        {
            try
            {
                HotKey hk = new HotKey(this.Handle);
                hk.RegisterHotKeys((uint)k);
                return hk;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            return null;
        }
        private void unregisterHotKey(HotKey hk)
        {
            try
            {
                if (null != hk)
                {
                    hk.UnRegisterHotKeys();
                    hk = null;
                }
            }
            catch(Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        protected override void WndProc(ref Message keyPressed)
        {
            if (keyPressed.Msg == 0x0312)
            {
                LBA1SG_processHotkey((Keys)keyPressed.WParam);
                //LBA1Menu_processHotkey((Keys)keyPressed.WParam);
                LBA2HyperCar_ProcessHotKey((Keys)keyPressed.WParam);
                LBA1Behaviour_processHotkey((Keys)keyPressed.WParam);
            }
            base.WndProc(ref keyPressed);
        }
        #endregion
        //This should be integrated into oTimerSetItems
        private oTimerSetItems itemToggle(oTimerSetItems tsi, uint offset, ushort val, byte size, oTimerSetItems.LBAVersion LBAVer)
        {
            if (null == tsi) tsi = new oTimerSetItems(LBAVer);
            if (!tsi.RemoveIfExists(offset))
                tsi.AddItem(offset, val, size);
            if (tsi.IsEmpty()) tsi = null;
            return tsi;
        }

        private string getLBAFilesPath(ushort LBAVer)
        {
            return AppDomain.CurrentDomain.BaseDirectory + "files\\lba" + LBAVer.ToString() + "\\";
        }

        #region MenuItems
        private void RefreshToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Reload(sender, e);
        }

        private void Reload(object sender, EventArgs e)
        {
            Options opt = new Options();
            memRoutines = new Mem();
            byte ver = memRoutines.DetectLBAVersion();
            //Load even if game not running
            if (0 == ver)
            {
                ;
            }
            //Load only if LBA1 running
            if (1 == ver)
            {
                memRoutines.WriteVal(1, (int)LBA1_AUTOZOOM, (ushort)(LBA1AutoZoomToolStripMenuItem1.Checked ? 1 : 0), 1);
                LBA1SG_Load(sender, e, opt);
                LBA1Behaviour_Load(sender, e, opt);
                FlyingLBA1.RefreshConnection();
                ucTeleportLBA1.RefreshConnection();
                return;
            }
            //Load only if LBA2 running
            if (2 == ver)
            {
                LBA2Slate_Load();
                FlyingLBA2.RefreshConnection();
                ucTeleportLBA2.RefreshConnection();
                LBA2Othr_Load();
                return;
            }

        }
        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new AboutBox1().ShowDialog();
        }

        #endregion

        private void LBA2Misc_btnBlowtron_Click(object sender, EventArgs e)
        {
            byte val = (byte) memRoutines.readVal(LBA2_BLOWTRON_LOCATION, 1);

            LBA2Misc_rbBlowtron0.Checked = (0 == val || 3 == val);
            LBA2Misc_rbBlowtron1.Checked = (1 == val);
            LBA2Misc_rbBlowtron2.Checked = (2 == val);
        }

        private void LBA1Fly_chkWalkingInAir_CheckedChanged(object sender, EventArgs e)
        {
            if (LBA1Fly_chkWalkingInAir.Checked)
                tsi.AddItem(0xD54D, 0, 1);
            else
            {
                tsi.RemoveIfExists(0xD54D);
                memRoutines.WriteVal(0xD54D, 8, 1);
            }
        }

        private void LBA2Flying_chkDisableGravity_CheckedChanged(object sender, EventArgs e)
        {
            if (LBA2Flying_chkDisableGravity.Checked)
                tsi.AddItem(0x580FA, 0, 2);
            else
            {
                tsi.RemoveIfExists(0x580FA);
                memRoutines.WriteVal(0x580FA, 8, 1);
            }
        }
    }
    public class NameValue
    {
        public byte val;
        public string name;

        public NameValue() { }
        public NameValue(byte val, string name)
        {
            this.val = val;
            this.name = name;
        }
        public override string ToString()
        {
            return name;
        }
    }
}
