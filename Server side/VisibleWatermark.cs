using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;

namespace Cryptic_Watermarking
{
    public partial class VisibleWatermark : Form
    {
        public VisibleWatermark()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            opencarrierimage();
        }
       
        private void button4_Click(object sender, EventArgs e)
        {
            opensecretimage();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Image image = System.Drawing.Image.FromFile(textBox1.Text.ToString());
            Image logo = Image.FromFile(textBox2.Text.ToString());
            Graphics g = System.Drawing.Graphics.FromImage(image);
            Bitmap TransparentLogo = new Bitmap(logo.Width, logo.Height);
            Graphics TGraphics = Graphics.FromImage(TransparentLogo);
            ColorMatrix ColorMatrix = new ColorMatrix();
            ColorMatrix.Matrix33 = 0.25F;
            ImageAttributes ImgAttributes = new ImageAttributes();
            ImgAttributes.SetColorMatrix(ColorMatrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);
            TGraphics.DrawImage(logo, new Rectangle(0, 0, TransparentLogo.Width, TransparentLogo.Height), 0, 0, TransparentLogo.Width, TransparentLogo.Height, GraphicsUnit.Pixel, ImgAttributes);
            TGraphics.Dispose();
            g.DrawImage(TransparentLogo, image.Width / 4, image.Height / 4);
            image.Save(@"C:\Watermark\Test.jpeg", ImageFormat.Jpeg);
            pictureBox2.Image = Image.FromFile(@"C:\Watermark\Test.jpeg");

        }
       

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            metropanel mp = new metropanel();
            mp.Show();
            this.Hide();
        }

        private void buttton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void opencarrierimage()
        {
            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.Image = Image.FromFile(ofd.FileName);
                
                textBox1.Text = ofd.FileName;

            }
        }
        private void opensecretimage()
        {
            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                pictureBox3.Image = Image.FromFile(ofd.FileName);

                textBox2.Text = ofd.FileName;
                

            }
        }

    }
}
