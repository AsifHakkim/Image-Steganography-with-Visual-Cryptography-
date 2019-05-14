using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Cryptic_Watermarking
{
    public partial class BrowseImage : Form
    {
        public BrowseImage()
        {
            InitializeComponent();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel1_Click(object sender, EventArgs e)
        {
            try
            {
                System.Windows.Forms.OpenFileDialog ofd = new OpenFileDialog();
                ofd.Filter = ofd.Filter = "Jpeg Images(*.jpg)|*.jpg";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    orginalpic.ImageLocation = ofd.FileName;
                    orginalpic.BackgroundImageLayout = ImageLayout.Stretch;
                    orginalpic.Dock = DockStyle.Fill;
                    Program.imagepath = ofd.FileName;
                    Program.orginal = new Bitmap(ofd.FileName);                  
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("error....");
            }
        }

        private void panel2_Click(object sender, EventArgs e)
        {

            try
            {
                System.Windows.Forms.OpenFileDialog ofd = new OpenFileDialog();
                ofd.Filter = "Jpeg Images(*.jpg)|*.jpg";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    targetpic.ImageLocation = ofd.FileName;
                    targetpic.BackgroundImageLayout = ImageLayout.Stretch;

                    targetpic.Dock = DockStyle.Fill;
                    Program.targetimagepath = ofd.FileName;

                    Program.suspected = new Bitmap(ofd.FileName);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("error....");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ImageAnalysis obj = new ImageAnalysis();
            ActiveForm.Hide();
            obj.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
