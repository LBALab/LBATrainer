using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LBATrainer.objects
{
    public partial class frmOptions : Form
    {
        public frmOptions()
        {
            InitializeComponent();
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbdLBADir = new FolderBrowserDialog();
            fbdLBADir.ShowDialog();
            txtSaveFilePath.Text = fbdLBADir.SelectedPath;
            fbdLBADir.Dispose();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Options opt = new Options();

            //General
            opt.alwaysOnTop = chkAlwaysOnTop.Checked;
            int x = getInt(txtDefaultRefreshInterval.Text);
            opt.DefaultRefreshInterval = -1 == x ? 0 : x;
            //opt.language = cboLanguage.
            //LBA1
            opt.LBA1SaveFileDirectory = txtSaveFilePath.Text;

            //LBA2

            opt.save();
        }

        private int getInt(string value)
        {
            int val;
            if (!int.TryParse(value, out val)) return -1;
            return val;
        }
    }
}
