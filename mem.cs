using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Diagnostics;

namespace LBATrainer
{
    class mem
    {
        [DllImport("kernel32.dll")]
        private static extern IntPtr OpenProcess(int dwDesiredAccess, bool bInheritHandle, int dwProcessId);
        [DllImport("kernel32.dll")]
        private static extern bool ReadProcessMemory(int hProcess, uint lpBaseAddress, byte[] lpBuffer, int dwSize, ref int lpNumberOfBytesRead);
        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool WriteProcessMemory(int hProcess, uint lpBaseAddress, byte[] lpBuffer, int dwSize, ref int lpNumberOfBytesWritten);

        const int PROCESS_ALL_ACCESS = 0x1F0FFF;
        const int PROCESS_WM_READ = 0x0010;
        Process proc;
        IntPtr processHandle;
        private uint baseAddress = 0;

        public mem()
        {
            OpenProcess(PROCESS_ALL_ACCESS);
        }
        private uint getBaseAddress(uint lbaVer)
        {
            string baseString;
            if (0 != baseAddress) return baseAddress;
            uint readAddr;
            if(1 == lbaVer)
            {
                baseString = "Relent";
                readAddr = 0x0A000FC8;//Base address to start scanning from
            }
            else
            {
                baseString = "Run-Time system.";
                readAddr = 0x0B00003D;
            }

            byte[] b = new byte[baseString.Length];

            for (int i = 0; i <= 0xFFFF; readAddr += 0x1000, i++)
            {
                int bytesRead = 0;

                ReadProcessMemory((int)processHandle, readAddr, b, b.Length, ref bytesRead);
                if (baseString == System.Text.Encoding.UTF7.GetString(b).Trim())
				{
					baseAddress = readAddr;
                    return readAddr;
				}
            }
            return 0;
        }
        #region writeMemory
        private int writeProcess(uint addressToWrite, byte[] buffer, ushort size)
        {
            int bytesWritten = 0;
            WriteProcessMemory((int)processHandle, addressToWrite, buffer, size, ref bytesWritten);
            return bytesWritten;
        }
        public bool writeAddress(uint LBAVer, Item item, byte[] bytes)
        {
            uint addressToWrite = (uint)item.memoryOffset;
            uint baseAddr = getBaseAddress(LBAVer);
            if (0 == baseAddr)
                return false;
            else
                addressToWrite += baseAddr;
            return (!(0 >= writeProcess(addressToWrite, bytes, item.size)));
        }
        public bool WriteVal(uint LBAVer, Item itm, string value)
        {
            ushort val;
            if (!ushort.TryParse(value, out val)) return false;
            if (val > itm.maxVal) val = itm.maxVal;
            if (val < itm.minVal) val = itm.minVal;
            WriteVal(LBAVer, itm, val);
            return true;
        }
        public void WriteVal(uint LBAVer, Item itm, ushort val)
        {
            writeAddress(LBAVer, itm, BitConverter.GetBytes(val));
        }
        public void WriteVal(uint LBAVer, int offset, ushort val, ushort size)
        {
            writeProcess((uint) (getBaseAddress(LBAVer) + offset), BitConverter.GetBytes(val), size);
        }
        #endregion
        #region readMemory
        private bool readProcess(uint addressToRead, ref byte[] data)
        {
            try
            {
                int bytesRead = 0;
                return ReadProcessMemory((int)processHandle, addressToRead, data, data.Length, ref bytesRead);
            }
            catch { }
            return false;
        }
        public int readAddress(uint LBAVer, uint offsetToRead, uint size)
        {
            uint addressToRead = 0;
            byte[] bytes = new byte[size];
            uint baseAddr = getBaseAddress(LBAVer);
            if (0 == baseAddr)
                return -1;
            else
                addressToRead = (uint)(offsetToRead + baseAddr);
            if (readProcess(addressToRead, ref bytes))
            {
                if (1 == size)
                    return bytes[0];
                return BitConverter.ToInt16(bytes, 0);
            }
            return 0;
        }
        public int getVal(uint LBAVer, Item itm)
        {
            if (null == itm) return 0;
            return readAddress(LBAVer, itm.memoryOffset, itm.size);
        }
        public string getString(ushort LBAVer, uint startOffset)
        {
            string sVal = "";
            int iVal;
            for (int i = 0; 0 != (iVal = readAddress(LBAVer, startOffset++, 1)); i++)
                if (-1 == iVal)
                    return null;
                else
                    sVal += Char.ConvertFromUtf32(iVal);
            return sVal;
        }

        //This reads bytes until a null character is encountered
        public byte[] getByteArrayNull(ushort LBAVer, uint startOffset)
        {
            string s;
            if (null == (s = getString(LBAVer, startOffset))) return null;
            byte[] t = Encoding.UTF8.GetBytes(s);
            byte[] b = new byte[t.Length + 1];
            for (byte i = 0; i < t.Length; i++) b[i] = t[i];
            b[b.Length - 1] = 0;
            return b;
        }
        public byte[] getByteArray(ushort LBAVer, uint startOffset, ushort size)
        {
            byte[] b = new byte[size];
            readProcess(getBaseAddress(LBAVer) + startOffset, ref b);
            return b;
        }
        #endregion
        //Assigns to proc and ProcessHandle
        private void OpenProcess(int access)
        {
            Process[] p;
            p = Process.GetProcessesByName("DOSBox");
            if (1 != p.Length) return;
            proc = p[0];
            processHandle = OpenProcess(access, false, proc.Id); ;
        }
    }
}
