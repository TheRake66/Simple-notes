using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Post_It
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //this.BackgroundImage = SetTransparentForm(Properties.Resources.image);
        }


        private Bitmap SetTransparentForm(Bitmap pImage)
        {
            this.BackColor = Color.FromArgb(0, 255, 0);
            this.TransparencyKey = this.BackColor;

            Bitmap tImage = new Bitmap(pImage);

            for (int x = 0; x < tImage.Width; x++)
            {
                for (int y = 0; y < tImage.Height; y++)
                {
                    Color tCol = tImage.GetPixel(x, y);

                    if (tCol.R + tCol.G + tCol.B == 0)
                    {
                        Color newColor = Color.FromArgb(255, 0, 255, 0);
                        tImage.SetPixel(x, y, newColor);
                    }
                    else
                    {
                        Color newColor = Color.FromArgb(255, tCol.R, tCol.G, tCol.B);
                        tImage.SetPixel(x, y, newColor);
                    }

                }
            }

            return tImage;

        }
    }
}
