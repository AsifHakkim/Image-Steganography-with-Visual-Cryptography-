using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WindowsFormsApplication1;
using System.IO;
using System.Net;
using System.Net.Sockets;

namespace Cryptic_Watermarking
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }
       // FTClientCode fs = new FTClientCode();
        private void button1_Click(object sender, EventArgs e)
        {
            client(textBox1.Text, textBox2.Text);
        }
        void client(string path, string ipadd)
        {
            MessageBox.Show("start sended");
            String filePath = path;
            FileInfo fin = new FileInfo(filePath);
            FileStream fstream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            byte[] clientData1 = ASCIIEncoding.ASCII.GetBytes(fin.Length.ToString());
            IPAddress ip = IPAddress.Parse(ipadd);
            IPEndPoint ipEnd = new IPEndPoint(ip, 5656);
            Socket clientSock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP);
            clientSock.Connect(ipEnd);
            clientSock.Send(clientData1);
            NetworkStream nfs = new NetworkStream(clientSock);
            int pcksize = 1025 * 1000;
            for (int i = 0; i < fin.Length && nfs.CanWrite; i += pcksize)
            {
                byte[] fdata;
                if ((fin.Length - i) / pcksize == 0)
                {
                    fdata = new byte[fin.Length - i];
                }
                else
                {
                    fdata = new byte[pcksize];
                }
                fstream.Position = long.Parse(i.ToString());
                fstream.Read(fdata, 0, fdata.Length);
                nfs.Write(fdata, 0, fdata.Length);
            }
            MessageBox.Show("sended");

            clientSock.Close();

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            metropanel mp = new metropanel();
            mp.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                textBox1.Text = dlg.FileName;
            }
        }
    }
}
