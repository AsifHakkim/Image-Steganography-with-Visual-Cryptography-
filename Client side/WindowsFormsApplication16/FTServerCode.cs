using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.IO;

namespace WindowsFormsApplication16
{
    class FTServerCode
    {
        IPEndPoint ipEnd;
        Socket sock;
        public FTServerCode()
        {
            IPAddress[] ipadd = Dns.GetHostAddresses(Dns.GetHostName());
            IPHostEntry ipHost = Dns.GetHostEntry(Dns.GetHostName());
           IPEndPoint ipEnd = new IPEndPoint(ipHost.AddressList[1], 5002);
            sock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP);
            sock.Bind(ipEnd);
        }
        public static string receivedPath = "E:\\RCv";
        public static string curMsg = "Stopped";
        public void StartServer()
        {
            try
            {
                curMsg = "Starting...";
                sock.Listen(100);

                curMsg = "Running and waiting to receive file.";
                Socket clientSock = sock.Accept();

                byte[] clientData = new byte[1024 * 5000];

                int receivedBytesLen = clientSock.Receive(clientData);
                curMsg = "Receiving data...";

                int fileNameLen = BitConverter.ToInt32(clientData, 0);
                string fileName = Encoding.ASCII.GetString(clientData, 4, fileNameLen);

                BinaryWriter bWrite = new BinaryWriter(File.Open(receivedPath + "/" + fileName, FileMode.Append)); ;
                bWrite.Write(clientData, 4 + fileNameLen, receivedBytesLen - 4 - fileNameLen);

                curMsg = "Saving file...";

                bWrite.Close();
                clientSock.Close();
                curMsg = "Reeived & Saved file; Server Stopped.";
            }
            catch (Exception ex)
            {
                curMsg = "File Receving error.";
            }
        }

    }
}
