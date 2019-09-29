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
        const ushort LBA1_Offset_Key = 0xE26;
        private HotKey hotkeyF7;
        private HotKey hotkeyF8;
        private HotKey hotkeyF9;
        ItemCheckedEventHandler icehLVLBA1SGChecked;

        private void Savegame_Load(object sender, EventArgs e, Options opt)
        {
            txtLBA1SaveFileDirectory.Text = opt.LBADir;
            icehLVLBA1SGChecked = new System.Windows.Forms.ItemCheckedEventHandler(this.LvLBA1SaveGames_ItemChecked);
        }
        private void Savegame_FormClosed(object sender, FormClosedEventArgs e)
        {
            unregisterHotkeysSavegame();
        }
        private void LBA1SGLoadSaves()
        {
            this.lvLBA1SaveGames.ItemChecked -= icehLVLBA1SGChecked;
            lvLBA1SaveGames.Items.Clear();
            if (string.IsNullOrEmpty(txtLBA1SaveFileDirectory.Text)) return;
            if (string.IsNullOrWhiteSpace(txtLBA1SaveFileDirectory.Text)) return;
            if (!System.IO.Directory.Exists(txtLBA1SaveFileDirectory.Text)) return;
            string[] filePaths = Directory.GetFiles(txtLBA1SaveFileDirectory.Text, "*.lba");
            ListViewItem lviFile;
            FileInfo fi;
            for (int i = 0; i < filePaths.Length; i++)
            {
                lviFile = new ListViewItem();
                fi = new FileInfo(filePaths[i]);
                fi.LastWriteTime.ToString();
                lviFile.Checked = new FileInfo(filePaths[i]).IsReadOnly;
                lviFile.SubItems.Add(getLBA1FriendlyFileName(filePaths[i]));
                lviFile.SubItems.Add(fi.LastWriteTime.ToString());
                lviFile.Tag = filePaths[i];
                lvLBA1SaveGames.Items.Add(lviFile);
            }
            this.lvLBA1SaveGames.ItemChecked += icehLVLBA1SGChecked;
            lvLBA1SaveGames.Columns[0].Width = -2;
            lvLBA1SaveGames.Columns[2].Width = -2;
        }
        private void BtnLBA1SaveGameEnableDisable_Click(object sender, EventArgs e)
        {
            LBA1SGLoadSaves();

            if (null == hotkeyF7|| null == hotkeyF8 || null == hotkeyF9 )
            {
                registerHotKeysSavegame();
                btnLBA1SaveGameEnableDisable.Text = "Disable";
            }
            else
            {
                unregisterHotkeysSavegame();
                btnLBA1SaveGameEnableDisable.Text = "Enable";
            }

        }
        private void processHotkeySavegame(Keys k)
        {
            if (Keys.F7 == k)
            {
                SaveGame sg = new SaveGame();
                if (!sg.save(txtLBA1SaveFileDirectory.Text)) MessageBox.Show("Unable to save game, is DOSBox running?");
            }
            if (Keys.F8 == k)
            {
                SaveGame sg = new SaveGame();
                getSaveFileNames getSaveFileName = new getSaveFileNames();
                //If cancelled do nothing
                getSaveFileName.TopMost = true;
                if (!(getSaveFileName.ShowDialog(this) == DialogResult.Cancel))
                {
                    if (!sg.saveAs(txtLBA1SaveFileDirectory.Text, getSaveFileName.txtFilename.Text + ".lba", getSaveFileName.txtInGameName.Text))
                        MessageBox.Show("Unable to save game, is DOSBox running?");
                }
                
                getSaveFileName.Dispose();
             }
            if (Keys.F9 == k)
                memRoutines.WriteVal(LBA_ONE, LBA1_Offset_Key, 1, 1);
        }
        //changes the status of the file pointed to by filePath and returns the new status
        private bool toggleReadOnly(string filePath)
        {
            FileInfo fi = new FileInfo(filePath);
            fi.IsReadOnly = !fi.IsReadOnly;
            fi.Refresh();
            return fi.IsReadOnly;
        }
        private void LvLBA1SaveGames_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            e.Item.Checked = toggleReadOnly((string)e.Item.Tag);
        }

        private void BtnLBA1SGRefresh_Click(object sender, EventArgs e)
        {
            LBA1SGLoadSaves();
        }
        private string getLBA1FriendlyFileName(string filePath)
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
        private void unregisterHotkeysSavegame()
        {
            try
            {
                unregisterHotKey(hotkeyF7);
                unregisterHotKey(hotkeyF8);
                unregisterHotKey(hotkeyF9);
            }
            catch { };
        }
        private void registerHotKeysSavegame()
        {
            hotkeyF7 = registerHotKey(Keys.F7);
            hotkeyF8 = registerHotKey(Keys.F8);
            hotkeyF9 = registerHotKey(Keys.F9);
        }

        private void BtnSGDeleteSaves_Click(object sender, EventArgs e)
        {
            if (DialogResult.OK == (MessageBox.Show("This will delete all non-readonly save files", "Delete Saves", MessageBoxButtons.OKCancel)))
                SavegameDeleteSaves();
        }
        private void SavegameDeleteSaves()
        {
            DirectoryInfo di = new DirectoryInfo(txtLBA1SaveFileDirectory.Text);
            FileInfo[] files = di.GetFiles("*.LBA")
                                 .Where(p => p.Extension == ".LBA").ToArray();
            foreach (FileInfo file in files)
                try
                {
                    //file.Attributes = FileAttributes.Normal;
                    File.Delete(file.FullName);
                }
                catch { }
            LBA1SGLoadSaves();
        }
    }
}
