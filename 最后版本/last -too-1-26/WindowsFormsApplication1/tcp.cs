using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Windows.Forms;
using System;

namespace WindowsFormsApplication1
{
    class SendData
    {
        public object obj1;
        public bool bConnected;
        public object tReader;
        public object wReader;
        public object Obj1
        {
            get { return obj1; }
            set { obj1 = value; }
        }
        public bool BConnected
        {
            get { return bConnected; }
            set { bConnected = value; }
        }
        public object TReader
        {
            get { return tReader; }
            set { tReader = value; }
        }
        public object WReader
        {
            get { return wReader; }
            set { wReader = value; }
        }
    }
    class TCP
    {
        //serve端
        private bool bConnected = false;
        private Thread tAcceptMsg = null;
        private IPEndPoint IPP = null;
        private Socket socket = null;
        private Socket clientSocket = null;
        private NetworkStream nStream = null;
        private TextReader tReader = null;
        private TextWriter wReader = null;
        object Serve_pNew = new TrainInit();
        SendData sendata = new SendData();

        public SendData TCPserve()
        {
            IPP = new IPEndPoint(IPAddress.Any, 3000);
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            socket.Bind(IPP);
            socket.Listen(0);
            tAcceptMsg = new Thread(new ThreadStart(this.TCPAcceptMessage));
            tAcceptMsg.Start();            
            return sendata;
        }
        public void TCPAcceptMessage()
        {
            clientSocket = socket.Accept();
            if (clientSocket != null)
            {
                bConnected = true;
                //this.label1.Text = "与客户:" + clientSocket.RemoteEndPoint.ToString() + "成功建立连接";
            }
            nStream = new NetworkStream(clientSocket);
            tReader = new StreamReader(nStream);
            wReader = new StreamWriter(nStream);
            string sTemp;
            while (bConnected)
            {
                try
                {
                    sTemp = tReader.ReadLine();
                    if (sTemp.Length != 0)
                    {
                        lock (this)
                        {

                            SerializeTool.deserializeStrToObj(sTemp, out Serve_pNew);
                            //richTextBox1.Text = "客户机:" + _pNew;
                            sendata.Obj1 = Serve_pNew;
                            sendata.BConnected = bConnected;
                            sendata.TReader = tReader;
                            sendata.WReader = wReader;
                        }
                    }
                }
                catch
                {
                    tAcceptMsg.Abort();
                    MessageBox.Show("无法与客户机通讯");

                }
            }
            clientSocket.Shutdown(SocketShutdown.Both);
            clientSocket.Close();
            socket.Shutdown(SocketShutdown.Both);
            socket.Close();

        }

        //client端
        private bool client_bConnected = false;
        private Thread client_tAcceptMsg = null;
        private IPEndPoint client_IPP = null;
        private Socket client_socket = null;
        private NetworkStream client_nStream = null;
        private TextReader client_tReader = null;
        private TextWriter client_wReader = null;
        object client_pNew = new TrainInit();
        SendData client_sendata = new SendData();
        public SendData TCPclient(string IP, string port)
        {
            try
            {
                client_IPP = new IPEndPoint(IPAddress.Parse(IP), int.Parse(port));
                client_socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                client_socket.Connect(client_IPP);
                if (client_socket.Connected)
                {
                    client_nStream = new NetworkStream(client_socket);
                    client_tReader = new StreamReader(client_nStream);
                    client_wReader = new StreamWriter(client_nStream);
                    client_tAcceptMsg = new Thread(new ThreadStart(this.clientAcceptMessage));
                    client_tAcceptMsg.Start();
                    client_bConnected = true;
                    MessageBox.Show("成功建立连接，可以通信");
                }
            }
            catch
            {
                MessageBox.Show("无法与服务器通讯");
            }
            return client_sendata;
        }

        public void clientAcceptMessage()
        {
            string sTemp;
            while (client_bConnected)
            {
                try
                {
                    sTemp = client_tReader.ReadLine();
                    if (sTemp.Length != 0)
                    {
                        lock (this)
                        {
                            SerializeTool.deserializeStrToObj(sTemp, out client_pNew);
                            //richTextBox3.Text = "服务器:" + _pNew.ToString();
                            sendata.Obj1 = client_pNew;
                            sendata.BConnected = client_bConnected;
                            sendata.TReader = client_tReader;
                            sendata.WReader = client_wReader;

                        }
                    }
                }
                catch
                {
                    client_tAcceptMsg.Abort();
                    MessageBox.Show("无法与服务器通讯");

                }
            }
            client_socket.Shutdown(SocketShutdown.Both);
            client_socket.Close();
            client_socket.Shutdown(SocketShutdown.Both);
            client_socket.Close();

        }
    }
}
