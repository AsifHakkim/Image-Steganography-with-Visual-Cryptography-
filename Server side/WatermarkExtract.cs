using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
//using Steganography;
using System.Drawing.Imaging;
using System.IO;

namespace Cryptic_Watermarking
{
    public partial class WatermarkExtract : Form
    {
       
        public WatermarkExtract()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
           

          
        }
       
        private void button2_Click(object sender, EventArgs e)
        {
           

        }
   
        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            metropanel mp = new metropanel();
            mp.Show();
            this.Hide();
        }
    }
}
