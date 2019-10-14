using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LBATrainer
{
    public partial class getSaveFileNames : Form
    {
        public getSaveFileNames()
        {
            InitializeComponent();
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void BtnOkay_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtFilename.Text)) txtFilename.Text = txtInGameName.Text;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void GetSaveFileNames_Load(object sender, EventArgs e)
        {
            this.Focus();
        }
    }
}
