using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Imaging;
using System.IO;


namespace Cryptic_Watermarking
{
    public partial class Registration : Form
    {

        private Size IMAGE_SIZE = new Size(437, 106);
        private const int GENERATE_IMAGE_COUNT = 2;
        private Bitmap[] m_EncryptedImages;
        public static string filename = "";
        BaseConnection con = new BaseConnection();
        public Registration()
        {
            InitializeComponent();
            rad();
        }

        public void rad()
        {
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var stringChars = new char[6];
            var random = new Random();

            for (int i = 0; i < stringChars.Length; i++)
            {
                stringChars[i] = chars[random.Next(chars.Length)];
            }

            var finalString = new String(stringChars);
            randm.Text = finalString.ToString();
        }
        public void insertdb()
        {
            string query = "insert into Login values('" + name.Text + "','" + pwd.Text + "','" + randm.Text + "','" + filename + "','" + textBox1.Text + "','" + textBox2.Text + "','" + textBox3.Text + "')";
            if (con.exec1(query) > 0)
            {
                MessageBox.Show("Registration successfull....");
                this.Close();
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (name.Text!= "" && pwd.Text!="")
            {
                if (m_EncryptedImages != null)
                {
                    for (int i = m_EncryptedImages.Length - 1; i > 0; i--)
                    {
                        m_EncryptedImages[i].Dispose();
                    }
                    Array.Clear(m_EncryptedImages, 0, m_EncryptedImages.Length);
                }

                m_EncryptedImages = GenerateImage(randm.Text);

                panelCanvas.Invalidate();
            }
            insertdb();
        }
        private Bitmap[] GenerateImage(string inputText)
        {
            Bitmap finalImage = new Bitmap(IMAGE_SIZE.Width, IMAGE_SIZE.Height);
            Bitmap tempImage = new Bitmap(IMAGE_SIZE.Width / 2, IMAGE_SIZE.Height);
            Bitmap[] image = new Bitmap[GENERATE_IMAGE_COUNT];

            Random rand = new Random();
            SolidBrush brush = new SolidBrush(Color.Black);
            Point mid = new Point(IMAGE_SIZE.Width / 2, IMAGE_SIZE.Height / 2);

            Graphics g = Graphics.FromImage(finalImage);
            Graphics gtemp = Graphics.FromImage(tempImage);

            StringFormat sf = new StringFormat();
            sf.Alignment = StringAlignment.Center;
            sf.LineAlignment = StringAlignment.Center;
            Font font = new Font("Times New Roman", 48);
            Color fontColor;

            g.DrawString(inputText, font, brush, mid, sf);
            gtemp.DrawImage(finalImage, 0, 0, tempImage.Width, tempImage.Height);


            for (int i = 0; i < image.Length; i++)
            {
                image[i] = new Bitmap(IMAGE_SIZE.Width, IMAGE_SIZE.Height);
            }


            int index = -1;
            int width = tempImage.Width;
            int height = tempImage.Height;
            for (int x = 0; x < width; x += 1)
            {
                for (int y = 0; y < height; y += 1)
                {
                    fontColor = tempImage.GetPixel(x, y);
                    index = rand.Next(image.Length);
                    if (fontColor.Name == Color.Empty.Name)
                    {
                        for (int i = 0; i < image.Length; i++)
                        {
                            if (index == 0)
                            {
                                image[i].SetPixel(x * 2, y, Color.Black);
                                image[i].SetPixel(x * 2 + 1, y, Color.Empty);
                            }
                            else
                            {
                                image[i].SetPixel(x * 2, y, Color.Empty);
                                image[i].SetPixel(x * 2 + 1, y, Color.Black);
                            }
                        }
                    }
                    else
                    {
                        for (int i = 0; i < image.Length; i++)
                        {
                            if ((index + i) % image.Length == 0)
                            {
                                image[i].SetPixel(x * 2, y, Color.Black);
                                image[i].SetPixel(x * 2 + 1, y, Color.Empty);
                            }
                            else
                            {
                                image[i].SetPixel(x * 2, y, Color.Empty);
                                image[i].SetPixel(x * 2 + 1, y, Color.Black);
                            }
                        }
                    }
                }
            }
            if (Directory.Exists(Application.StartupPath + "\\Shares"))
            {
                int fileCount = Directory.GetFiles(Application.StartupPath + "\\Shares").Length;
                fileCount++;
                image[0].Save(Application.StartupPath + "\\Shares\\" + fileCount.ToString() + ".png");
                filename = fileCount.ToString() + ".png";
             
            }
          SaveFileDialog save = new SaveFileDialog();
          if (save.ShowDialog() == DialogResult.OK)
          {
              image[1].Save(save.FileName, System.Drawing.Imaging.ImageFormat.Png);
          }
            brush.Dispose();
            tempImage.Dispose();
            finalImage.Dispose();

            return image;
        }

        private void panelCanvas_Paint(object sender, PaintEventArgs e)
        {
            if (m_EncryptedImages != null)
            {
                Graphics g = e.Graphics;
                Rectangle rect = new Rectangle(0, 0, 0, 0);
                for (int i = 0; i < m_EncryptedImages.Length; i++)
                {
                    rect.Size = m_EncryptedImages[i].Size;
                    g.DrawImage(m_EncryptedImages[i], rect);
                    rect.Y += m_EncryptedImages[i].Height + 5;
                }

                g.DrawLine(new Pen(new SolidBrush(Color.Black), 1), rect.Location, new Point(rect.Width, rect.Y));
                rect.Y += 5;

                //for (int i = 0; i < m_EncryptedImages.Length; i++)
                //{
                //    rect.Size = m_EncryptedImages[i].Size;
                //    g.DrawImage(m_EncryptedImages[i], rect);
                //}
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
