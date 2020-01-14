using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LBATrainer
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            /*try
            {*/
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new frmTrainer());
            /*}
            catch (Exception e)
            {
                MessageBox.Show("Error\n\n" + e.Message + "\n\n" +  e.StackTrace);
            }*/
        }
    }
}
