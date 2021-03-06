﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace Peržiūros_Programa
{
    public partial class BuyForm : Form
    {
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();

        public BuyForm(Image imageForSale)
        {
            InitializeComponent();
            this.Width = pboxWebsite.Image.Width;
            this.Height = pboxWebsite.Image.Height;

            pboxImageForSale.Image = imageForSale;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void pboxWebsite_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left 
                && e.X < this.Width - 50 && e.Y < 50)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }
    }
}
