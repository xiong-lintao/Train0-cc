using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System;

namespace TCPtool
{
    public class SendData
    {
        public object obj1;
        public bool bConnected;
        public TextReader tReader;
        public TextWriter wReader;
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
        public TextReader TReader
        {
            get { return tReader; }
            set { tReader = value; }
        }
        public TextWriter WReader
        {
            get { return wReader; }
            set { wReader = value; }
        }
    }
    public class TCP
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
        public object Serve_pNew;
        SendData sendata = new SendData();

        public SendData TCPserve(object _pNew)
        {
            Serve_pNew = _pNew;
            IPP = new IPEndPoint(IPAddress.Any, 3000);
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            socket.Bind(IPP);
            socket.Listen(0);
            tAcceptMsg = new Thread(new ThreadStart(this.TCPAcceptMessage));
            tAcceptMsg.Start();
            return sendata;
        }
        //public SendData Listener()
        //{
        //    tAcceptMsg = new Thread(new ThreadStart(this.TCPAcceptMessage));
        //    tAcceptMsg.Start();
        //    return sendata;
        //}
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
                sendata.BConnected = bConnected;
                sendata.TReader = tReader;
                sendata.WReader = wReader;
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


                        }
                    }
                }
                catch
                {
                    tAcceptMsg.Abort();

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
        public object client_pNew;
        SendData client_sendata = new SendData();
        public SendData TCPclient(string IP, string port, object _pNew)
        {
            try
            {
                client_pNew = _pNew;
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
                }
            }
            catch
            {
            }
            return client_sendata;
        }

        //public SendData Client_Listener()
        //{

        //    if (client_socket.Connected)
        //    {
        //        client_nStream = new NetworkStream(client_socket);
        //        client_tReader = new StreamReader(client_nStream);
        //        client_wReader = new StreamWriter(client_nStream);
        //        client_tAcceptMsg = new Thread(new ThreadStart(this.clientAcceptMessage));
        //        client_tAcceptMsg.Start();
        //        client_bConnected = true;
        //        MessageBox.Show("成功建立连接，可以通信");
        //    }
        //    return client_sendata;
        //}
        public void clientAcceptMessage()
        {
            string sTemp;
            while (client_bConnected)
            {
                client_sendata.BConnected = client_bConnected;
                client_sendata.TReader = client_tReader;
                client_sendata.WReader = client_wReader;
                try
                {
                    sTemp = client_tReader.ReadLine();
                    if (sTemp.Length != 0)
                    {
                        lock (this)
                        {
                            SerializeTool.deserializeStrToObj(sTemp, out client_pNew);
                            //richTextBox3.Text = "服务器:" + _pNew.ToString();
                            client_sendata.Obj1 = client_pNew;



                        }
                    }
                }
                catch
                {
                    client_tAcceptMsg.Abort();

                }
            }
            client_socket.Shutdown(SocketShutdown.Both);
            client_socket.Close();
            client_socket.Shutdown(SocketShutdown.Both);
            client_socket.Close();

        }
    }
}
