using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
namespace Cryptic_Watermarking
{
    public partial class Captcha : Form
    {
        BaseConnection con = new BaseConnection();
        Bitmap share1;
        Bitmap share2;
        Bitmap result;
        public static string susername = "";
        public static string spwd = "";

        public Captcha(string sharepath,string username,string pwd,string adminfilepath)
        {
            InitializeComponent();
            susername = username;
            spwd = pwd;
            pictureBox1.ImageLocation = sharepath;
            share1 = (Bitmap)Image.FromFile(sharepath);
            pictureBox2.ImageLocation = Application.StartupPath + "\\Shares\\" + adminfilepath;
            share2 = (Bitmap)Image.FromFile(Application.StartupPath + "\\Shares\\" + adminfilepath);
            reconstruct();
        }

        public void reconstruct()
        {
          
              result = new Bitmap(share1.Width, share1.Height);
              for (int x = 0; x < result.Width - 1; x += 1)
              {
                  for (int y = 0; y < result.Height; y += 1)
                  {
                      Color c1 = share1.GetPixel(x, y);
                      Color c2 = share2.GetPixel(x, y);

                      if (c1.ToArgb() != Color.Empty.ToArgb() && c2.ToArgb() == Color.Empty.ToArgb())
                      {
                          result.SetPixel(x, y, c1);
                      }
                      else if (c1.ToArgb() == Color.Empty.ToArgb() && c2.ToArgb() != Color.Empty.ToArgb())
                      {
                          result.SetPixel(x, y, c2);
                      }
                      pictureBox3.Image = (Image)result;

                  }
              }
       
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string query = "select * from Login where username='" + susername + "' and password='" + spwd + "' and SecretKey='" + secret.Text + "'";
            SqlDataReader sd = con.ret_dr(query);
            if (sd.Read())
            {
                MessageBox.Show("lOGIN sUCESS......");
                metropanel mp = new metropanel();
                mp.Show();
              
            }
            else
            {
                MessageBox.Show("Invalid Login");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            secret.Text = "";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
