using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Steganography;
using System.Drawing.Imaging;
using System.IO;
using System.Net;
using System.Net.Sockets;

namespace WindowsFormsApplication16
{
    public partial class WatermarkExtract : Form
    {
        Bitmap bmp = null;
        Bitmap bmp1 = null;
        string saveLocation = string.Empty;
        string extractedText = string.Empty;
        public static int ii = 1;
        public WatermarkExtract()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            bmp = (Bitmap)pictureBox1.Image;

            string extractedText = Helper.extractText(bmp);
            bmp1 = Helper.extractText1(bmp);
            if (encryptCheckBox.Checked)
            {
                try
                {
                    extractedText = Crypto.DecryptStringAES(extractedText, passwordTextBox.Text);
                }
                catch
                {
                    MessageBox.Show("Wrong password", "Error");

                    return;
                }
            }

            dataTextBox.Text = extractedText;
            Image img = Base64ToImage(dataTextBox.Text.ToString());
            img.Save(Directory.GetCurrentDirectory().ToString() + "\\extract.png");
            pictureBox2.Image = Image.FromFile(Directory.GetCurrentDirectory().ToString() + "\\extract.png");
            bmp1.Save(@"D:\Rcv\afterextract.png", ImageFormat.Png);
            pictureBox3.Image = Image.FromFile(@"D:\Rcv\afterextract.png");
        }
        void server()
        {
            
                MessageBox.Show("Start Received ");
                Socket sock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP);
                IPHostEntry ipHost = Dns.GetHostEntry(Dns.GetHostName());
                IPEndPoint ipEnd = new IPEndPoint(ipHost.AddressList[1], 5656);
                sock.Bind(ipEnd);
                sock.Listen(100);
                Socket clientSock = sock.Accept();
                byte[] clientData5 = new byte[1024 * 1000];

                int receivedBytesLen = clientSock.Receive(clientData5);
                int size = Convert.ToInt32(ASCIIEncoding.ASCII.GetString(clientData5));
                FileStream fileStream = new FileStream(@"D:\Rcv\aa" + (ii++) + ".png", FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite);
                NetworkStream ns = new NetworkStream(clientSock);
                bool flg = true;
                int j = 0;
                for (int i = 0; i < size; )
                {
                    byte[] fdata = new byte[1024 * 1000];

                    int ll = ns.Read(fdata, 0, fdata.Length);
                    fileStream.Flush();
                    fileStream.Write(fdata, 0, ll);
                    j = i;
                    i = i + ll;
                    if (j < size - 1 && i > size)
                    {
                        i = size - 1;
                    }

                }
                fileStream.Close();
                MessageBox.Show("Received successfully");
                clientSock.Close();
                sock.Close();
                  
            
        }
        public Image Base64ToImage(string base64String)
        {
            // Convert Base64 String to byte[]
            byte[] imageBytes = Convert.FromBase64String(base64String);
            MemoryStream ms = new MemoryStream(imageBytes, 0, imageBytes.Length);

            // Convert byte[] to Image
            ms.Write(imageBytes, 0, imageBytes.Length);
            Image image = Image.FromStream(ms, true);
            return image;

        }

        private void button2_Click(object sender, EventArgs e)
        {
            openCarrierImage();

        }
        private void openCarrierImage()
        {
            OpenFileDialog open_dialog = new OpenFileDialog();
            open_dialog.Filter = "Image Files (*.jpeg; *.png; *.bmp)|*.jpg; *.png; *.bmp";

            if (open_dialog.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.Image = Image.FromFile(open_dialog.FileName);
                textBox1.Text = open_dialog.FileName;

            }
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            BrowseImage bi = new BrowseImage();
            bi.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            server();
        }
    }
}
