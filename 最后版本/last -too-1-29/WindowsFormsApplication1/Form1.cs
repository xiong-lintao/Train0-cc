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
using System;
using System.Timers;



namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
        }
        TCP tcp;
        SendData serve_sendata;



        private void button1_Click(object sender, System.EventArgs e)
        {
            tcp = new TCP();
            serve_sendata = tcp.TCPserve();
            System.Timers.Timer timer = new System.Timers.Timer();
            timer.Enabled = true;
            timer.Interval = 30;//执行间隔时间,单位为毫秒;此时时间间隔为30ms   
            timer.Start();
            timer.Elapsed += new System.Timers.ElapsedEventHandler(serve_test);
        }
        public void serve_test(object source, ElapsedEventArgs e)
        {
            if (serve_sendata.Obj1!=null) {
                richTextBox1.Text = "服务器:" + serve_sendata.Obj1.ToString();
            }

           

        }

        private void richTextBox2_TextChanged(object sender, System.EventArgs e)
        {

        }

        private void richTextBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                if (serve_sendata.BConnected)
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
                            serve_sendata.WReader.WriteLine(result);
                            serve_sendata.WReader.Flush();
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
                //socket.Close();
                //tAcceptMsg.Abort();
            }
            catch
            {

            }
        }



        //client
        TCP client_tcp;
        SendData client_sendata;
        private void Form1_Load(object sender, System.EventArgs e)
        {

        }

       
        private void button2_Click(object sender, System.EventArgs e)
        {
            try
            {
                client_tcp = new TCP();
                client_sendata = client_tcp.TCPclient(textBox_clientIPaddress.Text, textBox_clientPort.Text);
                System.Timers.Timer timer = new System.Timers.Timer();
                timer.Enabled = true;
                timer.Interval = 30;//执行间隔时间,单位为毫秒;此时时间间隔为30ms  
                timer.Start();
                timer.Elapsed += new System.Timers.ElapsedEventHandler(client_test);
            }
            catch
            {
                MessageBox.Show("无法与服务器通讯");
            }
        }

        public void client_test(object source, ElapsedEventArgs e)
        {
            if (client_sendata.Obj1 != null)
            {
                richTextBox3.Text = "服务器:" + client_sendata.Obj1.ToString();
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
                if (client_sendata.BConnected)
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
                            client_sendata.WReader.WriteLine(result);
                            client_sendata.WReader.Flush();
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
