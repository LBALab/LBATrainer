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
        public frmTrainer()
        {
            InitializeComponent();
            scan(LBA_ONE);
            SetDoubleBuffered(tcLBAVersion);
            tsi = new oTimerSetItems(oTimerSetItems.LBAVersion.One);
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
#if !DEBUG
            tcLBA1Inner.TabPages.Remove(tcLBA1Inner.TabPages[tcLBA1Inner.TabPages.Count - 1]);
            tcLBA2Inner.TabPages.Remove(tcLBA2Inner.TabPages[1]);
            tcLBA2Inner.TabPages.Remove(tcLBA2Inner.TabPages[tcLBA2Inner.TabPages.Count - 1]);
#endif
        }
        private void FrmTrainer_FormClosed(object sender, FormClosedEventArgs e)
        {
            LBA1SG_FormClosed(sender, e);
        }
        private int getInt(string value)
        {
            ushort val;
            if (!ushort.TryParse(value, out val)) return -1;
            return val;
        }
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
            }
            base.WndProc(ref keyPressed);
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new AboutBox1().ShowDialog();
        }

        private oTimerSetItems itemToggle(oTimerSetItems tsi, uint offset, ushort val, byte size, oTimerSetItems.LBAVersion LBAVer)
        {
            if (null == tsi) tsi = new oTimerSetItems(LBAVer);
            if (!tsi.RemoveIfExists(offset))
                tsi.AddItem(offset, val, size);
            if (tsi.IsEmpty()) tsi = null;
            return tsi;
        }
        private void RefreshToolStripMenuItem_Click(object sender, EventArgs e)
        {
            memRoutines = new Mem();
            memRoutines.WriteVal(1, 0xE0A, (ushort)(LBA1AutoZoomToolStripMenuItem1.Checked ? 1 : 0), 1);
            flying1.RefreshConnection();
            flying2.RefreshConnection();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            tsi = itemToggle(tsi, 0x57F39, (ushort)(getInt(txtLBA2LocationZPos.Text)), 2, oTimerSetItems.LBAVersion.Two);
        }
        private void btnLBA2LocationScan_Click(object sender, EventArgs e)
        {
            txtLBA2LocationXPos.Text = memRoutines.readAddress(2, 0x57F35, 2).ToString();
            txtLBA2LocationZPos.Text = memRoutines.readAddress(2, 0x57F39, 2).ToString();
            txtLBA2LocationYPos.Text = memRoutines.readAddress(2, 0x57F3D, 2).ToString();
        }

        private void btnLBA2LocationSet_Click(object sender, EventArgs e)
        {
            memRoutines.WriteVal(2, 0x57F35, (ushort)getInt(txtLBA2LocationXPos.Text), 2);
            memRoutines.WriteVal(2, 0x57F39, (ushort)getInt(txtLBA2LocationZPos.Text), 2);
            memRoutines.WriteVal(2, 0x57F3D, (ushort)getInt(txtLBA2LocationYPos.Text), 2);
        }


        private void button2_Click(object sender, EventArgs e)
        {
            uint outfitAddress = 0x57F51;
            tsi = itemToggle(tsi, outfitAddress, (ushort)getInt(textBox1.Text), 2, oTimerSetItems.LBAVersion.Two);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            memRoutines.WriteVal(1,  0xD580, 65535, 2);
        }
        Timer fruity;

        private void tmrLBA2FruitMachine_Tick(object sender, EventArgs e)
        {
            lblFruitMachineCount.Text = memRoutines.readVal(0x57BC9, 2).ToString();
        }
        private void btnFruitMachineStart_Click(object sender, EventArgs e)
        {
            if(null == fruity)
            {
                fruity = new Timer();
                fruity.Interval = 10;
                fruity.Tick += tmrLBA2FruitMachine_Tick;
                fruity.Start();
            }
            else
            {
                fruity.Stop();
                fruity = null;
            }
        }

        private void lblFruitMachineCount_Click(object sender, EventArgs e)
        {            
            memRoutines.WriteVal(0x57BC9,(ushort) (getInt(lblFruitMachineCount.Text) + 1), 2);
        }
    }
}
