using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Simple_notes
{
    static class Program
    {
        // -------------------------------------
        public static string configFile = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\Simple notes.ini";
        // -------------------------------------



        // ====================================================================
        [DllImport("User32.dll")]
        public static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);
        // ====================================================================



        // ====================================================================
        [STAThread]
        static void Main()
        {
            // -------------------------------------
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Menu());
            // -------------------------------------
        }
        // ====================================================================
    }
}
