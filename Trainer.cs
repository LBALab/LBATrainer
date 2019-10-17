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
    /**
     * Shared stuff goes in here
     */
    public partial class frmTrainer : Form
    {
        const ushort LBA_ONE = 1;
        const ushort LBA_TWO = 2;
        private Items items;
        private mem memRoutines;

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
        #endregion

        private void FrmTrainer_FormClosed(object sender, FormClosedEventArgs e)
        {
            Savegame_FormClosed(sender, e);
            Teleport_FormClosed(sender, e);
            Flying_FormClosed(sender, e);
        }
        private void FrmTrainer_Load(object sender, EventArgs e)
        {
            Options opt = new Options();
            Savegame_Load(sender, e, opt);
            Teleport_Load(sender, e, opt);
            Flying_Load(sender, e, opt);
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
                processHotkeySavegame((Keys)keyPressed.WParam);
                processHotkeyFlying((Keys)keyPressed.WParam);
            }
            base.WndProc(ref keyPressed);
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new AboutBox1().ShowDialog();
        }
    }
}
