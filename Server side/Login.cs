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
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }
        BaseConnection con = new BaseConnection();
        private void splitContainer1_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            //Form1 mtpan = new Form1();
            //mtpan.Show();

            //this.Hide();
            Registration reg = new Registration();
            reg.Show();

        }

        private void Login_Load(object sender, EventArgs e)
        {
            label4.Text = "";
        }
     
        private void button3_Click(object sender, EventArgs e)
        {
           //str = "Select * from regfam where username='" + textBox1.Text + "' and password='" + textBox2.Text + "'";
           // con.dr = con.ret_dr(str);
           // if (con.dr.Read())
           // {
           //     Program.uname = con.dr[5].ToString();
           //     int a = Convert.ToInt32(con.dr[7].ToString());
           //     switch (a)
           //     {
           //         case 0:
           //             MessageBox.Show("Admin Not Considered Your Request");
           //             textBox1.Text = "";
           //             textBox2.Text = "";
           //             break;
           //         case 1:
           //             metropanel f = new metropanel();
                       
           //             f.Show();
           //             break;
           //     }
           // }
           // else
           // {
           //     MessageBox.Show("Register to Login");
           //     textBox1.Text = "";
           //     textBox2.Text = "";
           // }
            string query = "select * from login where Username='" + name.Text + "' and Password='" + pwd.Text + "'";
            SqlDataReader sd = con.ret_dr(query);
            if (sd.Read())
            {
                Captcha obj = new Captcha(sharepath.Text, name.Text, pwd.Text, sd[3].ToString());
                obj.Show();
            }
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            DialogResult result = openFileDialog1.ShowDialog();
            if (result == DialogResult.OK) // Test result.
            {
                sharepath.Text = openFileDialog1.FileName;
            }
        }
    }
}

