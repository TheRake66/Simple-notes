using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Simple_notes
{
    public partial class Aide : Form
    {
        // ====================================================================
        public Aide()
        {
            // -------------------------------------
            InitializeComponent();
            Program.SetWindowLong(this.Handle, -0x14/*GWL_EXSTYLE */, 0x00000080/*WS_EX_TOOLWINDOW*/);
            // -------------------------------------
        }
        // ====================================================================



        // ====================================================================
        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // -------------------------------------
            try
            {
                Process.Start("https://github.com/TheRake66");
            } catch { }
            // -------------------------------------
        }
        // ====================================================================



        // ====================================================================
        private void button1_Click(object sender, EventArgs e)
        {
            // -------------------------------------
            if (File.Exists(Program.configFile)) try { File.Delete(Program.configFile); } catch { }
            Application.Exit();
            // -------------------------------------
        }
        // ====================================================================
    }
}
