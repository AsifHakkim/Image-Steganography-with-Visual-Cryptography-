using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WindowsFormsApplication16;

namespace WindowsFormsApplication16
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public static string str;
        DBConnection con = new DBConnection();
        FTServerCode fs = new FTServerCode();
        private void button3_Click(object sender, EventArgs e)
        {
            str = "select Username, Password from Login where Username='" + textBox1.Text + "'";
            con.dr = con.ret_dr(str);
            if (con.dr.Read())
            {
                WatermarkExtract we = new WatermarkExtract();
                we.Show();              
            }
            else
            {
                MessageBox.Show("Invalid Login");              
            }
            
           
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            backgroundWorker1.RunWorkerAsync();
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            fs.StartServer();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
    }
}
