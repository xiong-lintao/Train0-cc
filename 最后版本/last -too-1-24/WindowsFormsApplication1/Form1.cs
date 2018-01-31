using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
        }
        private bool bConnected = false;
        private Thread tAcceptMsg = null;
        private IPEndPoint IPP = null;
        private Socket socket = null;
        private Socket clientSocket = null;
        private NetworkStream nStream = null;
        private TextReader tReader = null;
        private TextWriter wReader = null;
        public void AcceptMessage()
        {
            clientSocket = socket.Accept();
            if(clientSocket!= null)
            {
                bConnected = true;
                this.label1.Text = "与客户:" + clientSocket.RemoteEndPoint.ToString() + "成功建立连接";
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
                    if(sTemp.Length != 0)
                    {
                        lock (this)
                        {
                            object _pNew = new TrainInit();
                            SerializeTool.deserializeStrToObj(sTemp, out _pNew);
                            richTextBox1.Text = "客户机:" +  _pNew;
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

        private void button1_Click(object sender, System.EventArgs e)
        {
            IPP = new IPEndPoint(IPAddress.Any, 3000);
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            socket.Bind(IPP);
            socket.Listen(0);
            tAcceptMsg = new Thread(new ThreadStart(this.AcceptMessage));
            tAcceptMsg.Start();
            button1.Enabled = false;
        }

        private void richTextBox2_TextChanged(object sender, System.EventArgs e)
        {

        }

        private void richTextBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                if (bConnected)
                {
                    try
                    {
                        lock (this)
                        {
                            TrainInit _p = new TrainInit();
                            _p.DataBaseName = "SHL07_01";
                            _p.ProgramName = "SHL07_2017051201";
                            _p.TractionCurveName = "上海大学-杨高南路";
                            _p.PacketVersion = 1;
                            _p.DataTime = 2012123;
                            string result;
                            bool success = SerializeTool.serializeObjToStr(_p, out result);
                            richTextBox1.Text = "服务器" + result + richTextBox2.Text;
                            
                            wReader.WriteLine(result);
                            wReader.Flush();
                            richTextBox2.Text = "";
                            richTextBox2.Focus();
                        }
                    }
                    catch
                    {
                        MessageBox.Show("无法与客户机通讯");
                    }
                }
                else
                {
                    MessageBox.Show("未与与客户机建立连接，无法通讯");
                }
            }
        }



        private void Form1_FormClosing(object sender,FormClosedEventArgs e)
        {
            try
            {
                socket.Close();
                tAcceptMsg.Abort();
            }
            catch
            {

            }
        }



        //client
        private bool client_bConnected = false;
        private Thread client_tAcceptMsg = null;
        private IPEndPoint client_IPP = null;
        private Socket client_socket = null;
        private NetworkStream client_nStream = null;
        private TextReader client_tReader = null;
        private TextWriter client_wReader = null;
        private void Form1_Load(object sender, System.EventArgs e)
        {

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
                            object _pNew = new TrainInit();
                            SerializeTool.deserializeStrToObj(sTemp, out _pNew);
                            richTextBox3.Text = "服务器:" + _pNew.ToString();
                           
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
        private void button2_Click(object sender, System.EventArgs e)
        {
            try
            {
                client_IPP = new IPEndPoint(IPAddress.Parse(textBox_clientIPaddress.Text), int.Parse(textBox_clientPort.Text));
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
                    button1.Enabled = false;
                    MessageBox.Show("成功建立连接，可以通信");

                }
            }
            catch
            {
                MessageBox.Show("无法与服务器通讯");
            }
        }

        private void textBox2_TextChanged(object sender, System.EventArgs e)
        {

        }

        private void richTextBox4_TextChanged(object sender, System.EventArgs e)
        {

        }
     

        private void richTextBox4_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                if (client_bConnected)
                {
                    try
                    {
                        lock (this)
                        {
                            TrainInit _p = new TrainInit();
                            _p.DataBaseName = "SHL07_01";
                            _p.ProgramName = "SHL07_2017051201";
                            _p.TractionCurveName = "上海大学-杨高南路";
                            _p.PacketVersion = 001;
                            _p.DataTime = 563;
                            string result;
                            bool success = SerializeTool.serializeObjToStr(_p, out result);
                           
                            richTextBox3.Text = "客户机:" + result + richTextBox3.Text;
                            client_wReader.WriteLine(result);
                            client_wReader.Flush();
                            richTextBox4.Text = "";
                            richTextBox4.Focus();
                        }
                    }
                    catch
                    {
                        MessageBox.Show("与服务器连接断开");
                    }
                }
                else
                {
                    MessageBox.Show("未与与服务器建立连接，无法通讯");
                }
            }
        }
        
    }
}
