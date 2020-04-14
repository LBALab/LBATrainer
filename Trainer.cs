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
using LBATrainer.objects;

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
            scan(LBA_ONE);
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
            Options opt = new Options();
            LBA1SG_Load(sender, e, opt);
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
            }
            base.WndProc(ref keyPressed);
        }
        #endregion

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
            memRoutines = new Mem();
            memRoutines.WriteVal(1, 0xE0A, (ushort)(LBA1AutoZoomToolStripMenuItem1.Checked ? 1 : 0), 1);
            flying1.RefreshConnection();
            flying2.RefreshConnection();
        }
        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new AboutBox1().ShowDialog();
        }
        #endregion

        private void btnGo_Click(object sender, EventArgs e)
        {
            if (null == tgi) tgi = new oTimerGetItems();

            //If doesn't exist add else remove
            if (!tgi.Contains(LBA2_WIZARD_POSITION))
                tgi.AddItem(updateWizardPos, new Item(LBA2_WIZARD_POSITION, 1));
            else
                tgi.RemoveIfExists(LBA2_WIZARD_POSITION);
        }

        
        private void updateWizardPos(ushort pos)
        {
            txtWizardLocation.Text = pos.ToString();
            if (93 <= pos) { rb95.Checked = true; return; }
            if (88 <= pos) { rb90.Checked = true; return; }
            if (82 <= pos) { rb85.Checked = true; return; }
            if (78 <= pos) { rb80.Checked = true; return; }
            if (73 <= pos) { rb75.Checked = true; return; }
            if (68 <= pos) { rb70.Checked = true; return; }
            if (63 <= pos) { rb65.Checked = true; return; }
            if (58 <= pos) { rb60.Checked = true; return; }
            if (53 <= pos) { rb55.Checked = true; return; }
            if (48 <= pos) { rb50.Checked = true; return; }
            if (43 <= pos) { rb45.Checked = true; return; }
            if (38 <= pos) { rb40.Checked = true; return; }
            if (33 <= pos) { rb35.Checked = true; return; }
            if (28 <= pos) { rb30.Checked = true; return; }
            if (23 <= pos) { rb25.Checked = true; return; }
            if (18 <= pos) { rb20.Checked = true; return; }
            if (13 <= pos) { rb15.Checked = true; return; }
            if (8 <= pos) { rb10.Checked = true; return; }
            if (3 <= pos) { rb5.Checked = true; return; }
            if (0 <= pos) { rb0.Checked = true; return; }
        }
    }
}
