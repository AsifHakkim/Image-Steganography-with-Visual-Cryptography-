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
    public partial class ResizeImage : Form
    {
        public ResizeImage()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            openimage();

        }

        
        private void button3_Click(object sender, EventArgs e)
        {
            getResizeImage(textBox1.Text, Convert.ToInt32(textBox2.Text), Convert.ToInt32(textBox3.Text));
        }
       
        private void button2_Click(object sender, EventArgs e)
        {
            metropanel mp = new metropanel();
            mp.Show();
            this.Hide();
        }
        private void openimage()
        {
            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.Image = Image.FromFile(ofd.FileName);
                textBox1.Text = ofd.FileName;
                
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
        private void getResizeImage(String path, int width, int height)
        {
            Bitmap imgIn = new Bitmap(path);
            double y = imgIn.Height;
            double x = imgIn.Width;
            textBox4.Text = x.ToString()+" "+y.ToString();
            double factor = 1;
            if (width > 0)
            {
                factor = width / x;

            }
            else if (height > 0)
            {
                factor = height / y;
            }
            System.IO.MemoryStream outStream = new System.IO.MemoryStream();
            Bitmap imgOut = new Bitmap((int)(x * factor), (int)(y * factor));
            imgOut.SetResolution(72, 72);
            Graphics g = Graphics.FromImage(imgOut);
            g.Clear(Color.White);
            g.DrawImage(imgIn,new Rectangle(0,0,(int)(factor*x), (int)(factor*y)),
                new Rectangle(0,0,(int)x,(int) y),GraphicsUnit.Pixel);
                SaveFileDialog save_dialog=new SaveFileDialog();
                if (save_dialog.ShowDialog() == DialogResult.OK)
                {
                    imgOut.Save(save_dialog.FileName + ".jpeg", ImageFormat.Jpeg);
                    Bitmap imgIn1 = new Bitmap(save_dialog.FileName + ".jpeg");
                    double y1 = imgIn1.Height;
                    double x1 = imgIn1.Width;
                    textBox4.Text = " ";
                    textBox4.Text = x1.ToString() + " " + y1.ToString();

                }   
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }

}
