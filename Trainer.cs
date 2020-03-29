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
            //cboLBA2Inventory.Text
            //Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
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
            txtLBA2LocationFacing.Text = memRoutines.readAddress(2, 0x57F45, 2).ToString();
        }

        private void btnLBA2LocationSet_Click(object sender, EventArgs e)
        {
            memRoutines.WriteVal(2, 0x57F35, (ushort)getInt(txtLBA2LocationXPos.Text), 2);
            memRoutines.WriteVal(2, 0x57F39, (ushort)getInt(txtLBA2LocationZPos.Text), 2);
            memRoutines.WriteVal(2, 0x57F3D, (ushort)getInt(txtLBA2LocationYPos.Text), 2);
        }

        //Other Tab
        int counter = 255;
        private void button2_Click(object sender, EventArgs e)
        {
            uint outfitAddress = 0x57F51;
            //lblOtherSkin.Text = counter.ToString();
            //MessageBox.Show("Counter: " + counter);
            //memRoutines.WriteVal((int)outfitAddress, (ushort)counter++, 2);
            tsi = itemToggle(tsi, outfitAddress, (ushort)getInt(textBox1.Text), 2, oTimerSetItems.LBAVersion.Two);
        }

        //Other Tab: Instant car
        private void btnLBA2InstantCar_Click(object sender, EventArgs e)
        {
            //memRoutines.WriteVal(0x57F51, (ushort)getInt(textBox1.Text), 1);//Wizard Car
            memRoutines.WriteVal(0x57F51, 15, 1);//Wizard Car
            memRoutines.WriteVal(0x580EF, 13, 1);
            //memRoutines.WriteVal(0x8DABB, 13, 1);            
        }

        private void btnLBA2InstantCarTunic_Click(object sender, EventArgs e)
        {
            memRoutines.WriteVal(0x57F51, 14, 1);//Tunic Car


            /*memRoutines.WriteVal(0x3CD77, 6,    1);
            memRoutines.WriteVal(0x3CDDB, 6,    1);
            memRoutines.WriteVal(0x3D660, 0,    1);
            memRoutines.WriteVal(0x3D661, 37,   1);

            memRoutines.WriteVal(0x57F2F, 0,    1);
            memRoutines.WriteVal(0x580B9, 135,  1);
            memRoutines.WriteVal(0x580BA, 253,  1);
            memRoutines.WriteVal(0x580BB, 121,  1);

            memRoutines.WriteVal(0x580BC, 2,    1);
            memRoutines.WriteVal(0x580BF, 172,  1);
            memRoutines.WriteVal(0x580C0, 3,    1);
            memRoutines.WriteVal(0x580C1, 135,  1);

            memRoutines.WriteVal(0x580C2, 253,  1);
            memRoutines.WriteVal(0x580C3, 121,  1);
            memRoutines.WriteVal(0x580C4, 2,    1);
            memRoutines.WriteVal(0x580EF, 13, 1);

            memRoutines.WriteVal(0x5810E, 109, 1);*/

            memRoutines.WriteVal(0x580EF, 13, 1);
            //memRoutines.WriteVal(0x8DABB, 13, 1);
        }
        private void btnLBA2InstantCarDisabled_Click(object sender, EventArgs e)
        {
            memRoutines.WriteVal(0x57F51, 1, 1); //Dump outfit to 1

            /*memRoutines.WriteVal(0x3CD77, 3, 1);
            memRoutines.WriteVal(0x3CDDB, 0, 1);
            memRoutines.WriteVal(0x3D660, 255, 1);
            memRoutines.WriteVal(0x3D661, 36, 1);

            memRoutines.WriteVal(0x57F2F, 5, 1);
            memRoutines.WriteVal(0x580B9, 6, 1);
            memRoutines.WriteVal(0x580BA, 255, 1);
            memRoutines.WriteVal(0x580BB, 250, 1);

            memRoutines.WriteVal(0x580BC, 0, 1);
            memRoutines.WriteVal(0x580BF, 216, 1);
            memRoutines.WriteVal(0x580C0, 4, 1);
            memRoutines.WriteVal(0x580C1, 6, 1);

            memRoutines.WriteVal(0x580C2, 255, 1);
            memRoutines.WriteVal(0x580C3, 250, 1);
            memRoutines.WriteVal(0x580C4, 0, 1);
            memRoutines.WriteVal(0x580EF, 1, 1);

            memRoutines.WriteVal(0x5810E, 0, 1);*/
            memRoutines.WriteVal(0x580EF, 1, 1);
            /*memRoutines.WriteVal(0x8DABB, 1, 1);*/
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

        oTimerSetItems otis;
        private void btnCarSpeed_Click(object sender, EventArgs e)
        {
            if (null == otis)
            {
                otis = new oTimerSetItems(oTimerSetItems.LBAVersion.Two, 10);
                otis.AddItem(0x52B9B, (ushort)getInt(txtLBA2CarSpeed.Text), 2);
                btnCarSpeed.Text = "On";
            }
            else
            {
                otis.RemoveIfExists(0x52b9b);
                otis = null;
                btnCarSpeed.Text = "Off";
            }
        }
        private string getLBAFilesPath(ushort LBAVer)
        {
            return AppDomain.CurrentDomain.BaseDirectory + "files\\lba" + LBAVer.ToString() + "\\";
        }

        private void cboLBA2Inventory_TextChanged(object sender, EventArgs e)
        {
            filterCBO(cboLBA2Inventory, items.Inventory);
        }

        private void LBA2Othr_btnGetAreacode_Click(object sender, EventArgs e)
        {
            LBA2Othr_lblAreacodeTxt.Text = memRoutines.readVal(0x55C5F, 2).ToString();
        }
    }
}
