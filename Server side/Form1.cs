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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            getid();
        }
        DBConnection con = new DBConnection();
        public static string str,id;

        public void getid()
        {
            str = "select isnull(max(ID),100)+1 from regfam";
            con.dr = con.ret_dr(str);
            if (con.dr.Read())
            {
                id = con.dr[0].ToString();
            }
        }
        private void button3_Click(object sender, EventArgs e)
        {
            str = "insert into regfam values('" + id + "','" + textBox1.Text + "','" + textBox6.Text + "','" + comboBox5.SelectedItem.ToString() + "','" + textBox2.Text + "','" + textBox3.Text + "','" + textBox4.Text + "',1)";
            int a = con.exec1(str);
            if (a > 0)
            {
                MessageBox.Show("inserted successfully");
                getid();

                textBox1.Text = "";
                textBox6.Text = "";
                textBox3.Text = "";
                textBox2.Text = "";
                textBox4.Text = "";
                comboBox5.SelectedIndex = -1;
            }
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboBox5_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
