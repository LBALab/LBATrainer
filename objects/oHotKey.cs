using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LBATrainer
{
    class HotKey
    {
        /// <summary>
        /// Typical hotkey assigngments
        /// </summary>
        public enum fsModifiers
        {
            Alt = 0x0001,
            Control = 0x0002,
            Shift = 0x0004,
            Window = 0x0008,
        }

        private IntPtr hWnd;

        public HotKey(IntPtr hWnd)
        {
            this.hWnd = hWnd;
        }

        public void RegisterHotKeys(int id, uint modifiers, uint keys)
        {
            RegisterHotKey(hWnd, id, modifiers, keys);
        }

        public void RegisterHotKeys(uint key)
        {
            RegisterHotKeys((int)key, 0, key);
        }

        public void RegisterHotKeys(int id, uint keys)
        {
            RegisterHotKeys(id, 0, keys);
        }

        public void UnRegisterHotKeys()
        {
            UnregisterHotKey(hWnd, 1);
        }

        #region WindowsAPI
        [DllImport("user32.dll")]
        private static extern bool RegisterHotKey(IntPtr hWnd, int id, uint fsModifiers, uint vk);

        [DllImport("user32.dll")]
        private static extern bool UnregisterHotKey(IntPtr hWnd, int id);
        #endregion

    }
}
