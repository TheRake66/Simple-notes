using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Simple_notes
{
    public partial class Menu : Form
    {
        // ====================================================================
        public Menu()
        {
            // -------------------------------------
            InitializeComponent();
            Program.SetWindowLong(this.Handle, -0x14/*GWL_EXSTYLE */, 0x00000080/*WS_EX_TOOLWINDOW*/);

            if (!File.Exists(Program.configFile))
            {
                FileStream file = File.Create(Program.configFile);
                file.Close();
                File.SetAttributes(Program.configFile, FileAttributes.Hidden);
            }
            // -------------------------------------
        }
        // ====================================================================




        // ====================================================================
        private void Menu_Load(object sender, EventArgs e)
        {
            // -------------------------------------
            try
            {
                Ini file = new Ini(Program.configFile);

                string[] fond = file.ReadKey("Configuration", "CouleurFond", "254,216,0").Split(',');
                this.richTextBox1.BackColor = Color.FromArgb(
                    Convert.ToInt32(fond[0]),
                    Convert.ToInt32(fond[1]),
                    Convert.ToInt32(fond[2])
                );
                string[] police = file.ReadKey("Configuration", "CouleurPolice", "0,0,0").Split(',');
                this.richTextBox1.ForeColor = Color.FromArgb(
                    Convert.ToInt32(police[0]),
                    Convert.ToInt32(police[1]),
                    Convert.ToInt32(police[2])
                );
                this.richTextBox1.Font = (new FontConverter()).ConvertFromInvariantString(file.ReadKey("Configuration", "Police", "MV Boli, 14pt")) as Font;
                this.richTextBox1.Text = Encoding.UTF8.GetString(Convert.FromBase64String(file.ReadKey("Configuration", "Texte", "")));
                this.richTextBox1.Text = Encoding.UTF8.GetString(Convert.FromBase64String(file.ReadKey("Configuration", "Texte", "")));

                string[] taille = file.ReadKey("Configuration", "Taille", "350,350").Split(',');
                this.Size = new Size(
                    Convert.ToInt32(taille[0]),
                    Convert.ToInt32(taille[1])
                    );
                string[] point = file.ReadKey("Configuration", "Position", (Screen.FromControl(this).WorkingArea.Width - this.Width).ToString() + ",0").Split(',');
                this.Location = new Point(
                    Convert.ToInt32(point[0]),
                    Convert.ToInt32(point[1])
                    );
            }
            catch { }
            // -------------------------------------
        }
        private void Menu_FormClosing(object sender, FormClosingEventArgs e)
        {
            // -------------------------------------
            try
            {
                File.SetAttributes(Program.configFile, FileAttributes.Normal);
                Ini file = new Ini(Program.configFile);

                file.WriteKey("Configuration", "CouleurFond", this.richTextBox1.BackColor.R + "," + this.richTextBox1.BackColor.G + "," + this.richTextBox1.BackColor.B);
                file.WriteKey("Configuration", "CouleurPolice", this.richTextBox1.ForeColor.R + "," + this.richTextBox1.ForeColor.G + "," + this.richTextBox1.ForeColor.B);
                file.WriteKey("Configuration", "Police", (new FontConverter()).ConvertToInvariantString(this.richTextBox1.Font));
                file.WriteKey("Configuration", "Texte", Convert.ToBase64String(Encoding.UTF8.GetBytes(this.richTextBox1.Text)));

                file.WriteKey("Configuration", "Taille", this.Width + "," + this.Height);
                file.WriteKey("Configuration", "Position", this.Location.X + "," + this.Location.Y);

                File.SetAttributes(Program.configFile, FileAttributes.Hidden);
            }
            catch { }
            // -------------------------------------
        }
        // ====================================================================



        // ====================================================================
        private void richTextBox1_MouseDown(object sender, MouseEventArgs e)
        {
            // -------------------------------------
            if (e.Button == MouseButtons.Right)
            {
                ContextMenu menu = new ContextMenu();

                MenuItem couleur = new MenuItem("Couleur");
                    MenuItem fond = new MenuItem("Fond");
                    MenuItem police = new MenuItem("Police");
                MenuItem police2 = new MenuItem("Police");
                MenuItem effacer = new MenuItem("Effacer");
                MenuItem boot = new MenuItem("Démarrer avec Windows");
                MenuItem aide = new MenuItem("Aide");

                menu.MenuItems.Add(couleur);
                    couleur.MenuItems.Add(fond);
                    couleur.MenuItems.Add(police);
                menu.MenuItems.Add(police2);
                menu.MenuItems.Add("-");
                menu.MenuItems.Add(effacer);
                menu.MenuItems.Add("-");
                menu.MenuItems.Add(boot);
                menu.MenuItems.Add(aide);


                fond.Click += new EventHandler((a, b) =>
                {
                    ColorDialog diag = new ColorDialog();
                    diag.AllowFullOpen = true;
                    diag.Color = this.richTextBox1.BackColor;
                    if (diag.ShowDialog() == DialogResult.OK) this.richTextBox1.BackColor = diag.Color;
                });
                police.Click += new EventHandler((a, b) =>
                {
                    ColorDialog diag = new ColorDialog();
                    diag.AllowFullOpen = true;
                    diag.Color = this.richTextBox1.BackColor;
                    if (diag.ShowDialog() == DialogResult.OK) this.richTextBox1.ForeColor = diag.Color;
                });
                police2.Click += new EventHandler((a, b) =>
                {
                    FontDialog diag = new FontDialog();
                    diag.Font = this.richTextBox1.Font;
                    if (diag.ShowDialog() == DialogResult.OK) this.richTextBox1.Font = diag.Font;
                });
                effacer.Click += new EventHandler((a, b) => { this.richTextBox1.Text = ""; });

                string shortcut = Environment.GetFolderPath(Environment.SpecialFolder.Startup) + @"\Simple notes.bat";
                if (File.Exists(shortcut)) boot.Checked = true;
                boot.Click += new EventHandler((a, b) =>
                {
                    try
                    {
                        if (boot.Checked) File.Delete(shortcut);
                        else File.WriteAllText(shortcut, @"start """" """ + Application.ExecutablePath + @""" --hide");
                    }
                    catch { }
                });
                aide.Click += new EventHandler((a, b) => { (new Aide()).ShowDialog(); });


                menu.Show((Control)sender, e.Location);
            }
            // -------------------------------------
        }
        // ====================================================================
    }
}
