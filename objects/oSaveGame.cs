using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Windows.Forms;
using System.Globalization;
using LBAMemoryModule;
namespace LBATrainer
{
    class SaveGame
    {
        const int actualFileNameOffset = 0x1CAA4;
        private SaveItem[] saveGame;

        public SaveGame()
        {
            saveGame = loadItems();
        }
        private SaveItem[] loadItems()
        {
            //AppDomain.CurrentDomain.BaseDirectory
            string filePath = AppDomain.CurrentDomain.BaseDirectory  + "files\\saveGame.xml";
            XmlDocument doc = new XmlDocument();
            doc.Load(filePath);
            XmlNodeList nodes = doc.DocumentElement.SelectNodes("/savegame/item");
            SaveItem[] items = new SaveItem[nodes.Count];
            for (int i = 0; i < items.Length; i++)
                items[i] = getItem(nodes[i]);
            return items;
        }
        private SaveItem getItem(XmlNode xn)
        {
            SaveItem item = new SaveItem();
            item.ID = byte.Parse(xn.Attributes["id"].Value.Trim());
            item.description = xn.Attributes["description"].Value;
            item.memoryOffsetStart = getValOrZero(xn.SelectSingleNode("memoryOffsetStart").InnerText.Trim());
            item.memoryOffsetEnd = getValOrZero(xn.SelectSingleNode("memoryOffsetEnd").InnerText.Trim());
            item.fileOffsetStart = getValOrZero(xn.SelectSingleNode("fileOffsetStart").InnerText.Trim());
            item.fileOffsetEnd = getValOrZero(xn.SelectSingleNode("fileOffsetEnd").InnerText.Trim());
            item.numOfBytes = (byte)getValOrZero(xn.SelectSingleNode("numOfBytes").InnerText.Trim());
            item.fixedValue = (byte)getValOrZero(xn.SelectSingleNode("fixedValue").InnerText.Trim());
            return item;
        }
        //Returns either the integer value of val, or 0 if conversion fails
        private uint getValOrZero(string val)
        {   
            if(uint.TryParse(val, System.Globalization.NumberStyles.HexNumber, CultureInfo.InvariantCulture, out uint result)) return result;
            return 0;
        }
        public bool save(string saveFilePath)
        {
            Mem m = new Mem();
            string fileNameDisk = m.getString(1, actualFileNameOffset);
            for(ushort i = 0; i < saveGame.Length;i++)
                saveGame[i].data = getData(m, saveGame[i]);

            if (null == fileNameDisk) return false;
            saveFilePath += "\\" +  fileNameDisk.ToLower();
            writeFile(saveFilePath, saveGame);
            return true;
        }

        public bool saveAs(string saveFilePath, string fileNameDisk, string fileNameInternal)
        {
            Mem m = new Mem();
            for (ushort i = 0; i < saveGame.Length; i++)
            {
                if (1 == i)
                    saveGame[i].data = stringToByteArray(fileNameInternal);
                else
                    saveGame[i].data = getData(m, saveGame[i]);
            }
            return writeFile(saveFilePath += "\\" + fileNameDisk.ToLower(), saveGame);
        }
        private byte[] stringToByteArray(string s)
        {
            byte[] b = new byte[s.Length+1];
            for (int i = 0; i < s.Length; i++)
                b[i] = (byte)s[i];
            b[s.Length] = 0;
            return b;
        }

        private bool writeFile(string path, SaveItem[] saveGame)
        {
            if(File.Exists(path))new FileInfo(path) { IsReadOnly = false }.Refresh();
            FileStream fsFile = new FileStream(path, FileMode.OpenOrCreate,
            FileAccess.Write);

            for (int i = 0; i < saveGame.Length; i++)
            {
                for(int j=0; j< saveGame[i].data.Length;j++)
                    fsFile.WriteByte(saveGame[i].data[j]);
            }
            fsFile.Flush();
            fsFile.Close();
            new FileInfo(path) { IsReadOnly = true }.Refresh();
            return true;
        }
        private byte[] getData(Mem m, SaveItem item)
        {
            //If fixedValue
            if (0 == item.memoryOffsetStart)
                return getConstant(item);
            //If Filename - if end offset is 0 and we have a start object this is the filename
            if (item.memoryOffsetEnd < item.memoryOffsetStart)
                return getFilename(m, item);
            if (0 != item.memoryOffsetStart && 0 != item.memoryOffsetEnd)
                return getByteArray(m, item);
            return null;
        }
        private byte[] getConstant(SaveItem item)
        {
            byte[] data;
            ushort arraySize = (ushort)((item.fileOffsetEnd - item.fileOffsetStart) + 1); //(11 - 1 = 10, we need to be inclusive)
            data = new byte[arraySize];
            for (ushort i = 0; i < data.Length; i++)
                data[i] = item.fixedValue;
            return data;
        }
        private byte[] getFilename(Mem m, SaveItem item)
        {
            return m.getByteArrayNull(1, item.memoryOffsetStart);            
        }
        private byte[] getByteArray(Mem m, SaveItem item)
        {
            byte arraySize = (byte)((item.fileOffsetEnd - item.fileOffsetStart) + 1); //(11 - 1 = 10, we need to be inclusive)
            return m.getByteArray(1, item.memoryOffsetStart, arraySize);
        }

    }

}
