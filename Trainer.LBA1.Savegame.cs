using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LBATrainer
{
    public partial class frmTrainer
    {
        const ushort LBA1SG_Offset_Key = 0xE26;
        private HotKey LBA1SG_hkF7;
        private HotKey LBA1SG_hkF8;
        private HotKey LBA1SG_hkF9;
        ItemCheckedEventHandler LBA1SG_icehLVChecked;

        private void LBA1SG_Load(object sender, EventArgs e, Options opt)
        {
            LBA1SG_txtSaveFileDirectory.Text = opt.LBADir;
            LBA1SG_icehLVChecked = new System.Windows.Forms.ItemCheckedEventHandler(this.LBA1SG_LvSaveGames_ItemChecked);
        }
        private void LBA1SG_FormClosed(object sender, FormClosedEventArgs e)
        {
            LBA1SG_unregisterHotkeys();
        }
        private void LBA1SG_LoadSaves()
        {
            this.LBA1SG_lvSaveGames.ItemChecked -= LBA1SG_icehLVChecked;
            LBA1SG_lvSaveGames.Items.Clear();
            if (string.IsNullOrEmpty(LBA1SG_txtSaveFileDirectory.Text)) return;
            if (string.IsNullOrWhiteSpace(LBA1SG_txtSaveFileDirectory.Text)) return;
            if (!System.IO.Directory.Exists(LBA1SG_txtSaveFileDirectory.Text)) return;
            string[] filePaths = Directory.GetFiles(LBA1SG_txtSaveFileDirectory.Text, "*.lba");
            ListViewItem lviFile;
            FileInfo fi;
            for (int i = 0; i < filePaths.Length; i++)
            {
                lviFile = new ListViewItem();
                fi = new FileInfo(filePaths[i]);
                fi.LastWriteTime.ToString();
                lviFile.Checked = new FileInfo(filePaths[i]).IsReadOnly;
                lviFile.SubItems.Add(LBA1SG_getFriendlyFileName(filePaths[i]));
                lviFile.SubItems.Add(fi.LastWriteTime.ToString());
                lviFile.Tag = filePaths[i];
                LBA1SG_lvSaveGames.Items.Add(lviFile);
            }
            this.LBA1SG_lvSaveGames.ItemChecked += LBA1SG_icehLVChecked;
            LBA1SG_lvSaveGames.Columns[0].Width = -2;
            LBA1SG_lvSaveGames.Columns[2].Width = -2;
        }
        private void BtnLBA1SaveGameEnableDisable_Click(object sender, EventArgs e)
        {
            LBA1SG_LoadSaves();

            if (null == LBA1SG_hkF7|| null == LBA1SG_hkF8 || null == LBA1SG_hkF9 )
            {
                LBA1SG_registerHotKeys();
                LBA1SG_btnEnableDisable.Text = "Disable";
            }
            else
            {
                LBA1SG_unregisterHotkeys();
                LBA1SG_btnEnableDisable.Text = "Enable";
            }
        }
        private void LBA1SG_processHotkey(Keys k)
        {
            if (Keys.F7 == k)
            {
                SaveGame sg = new SaveGame();
                if (!sg.save(LBA1SG_txtSaveFileDirectory.Text)) MessageBox.Show("Unable to save game, is DOSBox running?");
            }
            if (Keys.F8 == k)
            {
                SaveGame sg = new SaveGame();
                getSaveFileNames getSaveFileName = new getSaveFileNames();
                //If cancelled do nothing
                getSaveFileName.TopMost = true;
                getSaveFileName.Focus();
                if (!(getSaveFileName.ShowDialog(this) == DialogResult.Cancel))
                {
                    if (!sg.saveAs(LBA1SG_txtSaveFileDirectory.Text + "\\savePack", getSaveFileName.txtFilename.Text + ".lba", getSaveFileName.txtInGameName.Text))
                        MessageBox.Show("Unable to save game, is DOSBox running?");
                }
                
                getSaveFileName.Dispose();
             }
            if (Keys.F9 == k)
                memRoutines.WriteVal(LBA_ONE, LBA1SG_Offset_Key, 1, 1);
        }
        //changes the status of the file pointed to by filePath and returns the new status
        private bool LBA1SG_toggleReadOnly(string filePath)
        {
            FileInfo fi = new FileInfo(filePath);
            fi.IsReadOnly = !fi.IsReadOnly;
            fi.Refresh();
            return fi.IsReadOnly;
        }
        private void LBA1SG_LvSaveGames_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            e.Item.Checked = LBA1SG_toggleReadOnly((string)e.Item.Tag);
        }

        private void LBA1SG_BtnRefresh_Click(object sender, EventArgs e)
        {
            LBA1SG_LoadSaves();
        }
        private string LBA1SG_getFriendlyFileName(string filePath)
        {
            if (!File.Exists(filePath)) return null;
            FileStream fsStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);

            char b = (char)fsStream.ReadByte();//Read and discard the opening byte (03)
            string friendlyName = "";

            while (0 != (b = (char)fsStream.ReadByte()))
                friendlyName += b;

            fsStream.Close();
            fsStream.Dispose();
            return friendlyName;
        }
        private void LBA1SG_BtnSetSaveFileDir_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbdLBADir = new FolderBrowserDialog();
            fbdLBADir.ShowDialog();
            LBA1SG_txtSaveFileDirectory.Text = fbdLBADir.SelectedPath;
            fbdLBADir.Dispose();
            Options opt = new Options();
            opt.LBADir = LBA1SG_txtSaveFileDirectory.Text;
            opt.save();
        }
        private void LBA1SG_registerHotKeys()
        {
            LBA1SG_hkF7 = registerHotKey(Keys.F7);
            LBA1SG_hkF8 = registerHotKey(Keys.F8);
            LBA1SG_hkF9 = registerHotKey(Keys.F9);
        }
        private void LBA1SG_unregisterHotkeys()
        {
            try
            {
                unregisterHotKey(LBA1SG_hkF7);
                unregisterHotKey(LBA1SG_hkF8);
                unregisterHotKey(LBA1SG_hkF9);
            }
            catch { };
        }

        private void LBA1SG_BtnDeleteSaves_Click(object sender, EventArgs e)
        {
            if (DialogResult.OK == (MessageBox.Show("This will delete all non-readonly save files", "Delete Saves", MessageBoxButtons.OKCancel)))
                LBA1SG_DeleteSaves();
        }
        private void LBA1SG_DeleteSaves()
        {
            DirectoryInfo di = new DirectoryInfo(LBA1SG_txtSaveFileDirectory.Text);
            FileInfo[] files = di.GetFiles("*.LBA");
            //                                 .Where(p => p.Extension == ".LBA").ToArray();
            foreach (FileInfo file in files)
                try
                {
                    //file.Attributes = FileAttributes.Normal;
                    File.Delete(file.FullName);
                }
                catch { }
            LBA1SG_LoadSaves();
        }
    }
}
