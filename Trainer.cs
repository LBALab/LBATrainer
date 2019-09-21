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
        }
        private void FrmTrainer_Load(object sender, EventArgs e)
        {
            Options opt = new Options();
            Savegame_Load(sender, e, opt);
            Teleport_Load(sender, e, opt);
        }


        private void BtnGetChapter_Click(object sender, EventArgs e)
        {
            if (tmrHeightLock.Enabled)
                tmrHeightLock.Stop();
            else
                tmrHeightLock.Start();
        }




        /*
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
        */
    }
}
