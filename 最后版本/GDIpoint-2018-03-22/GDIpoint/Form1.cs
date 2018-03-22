using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GDIpoint
{
    public partial class Form1 : Form
    {
        EXCELCONTROL excelControl = new EXCELCONTROL();
        public Form1()
        {
            InitializeComponent();
            // 添加颜色
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox1.Items.Add(new ColorName() { Color = Color.Green, Name = "绿色" });
            comboBox1.Items.Add(new ColorName() { Color = Color.Blue, Name = "蓝色" });
            comboBox1.Items.Add(new ColorName() { Color = Color.Red, Name = "红色" });
            comboBox1.SelectedIndex = 0;
            // 添加X轴坐标间距
            comboBox2.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox2.Items.Add(new Space() { Multiple = 15, Name = "150" });
            comboBox2.Items.Add(new Space() { Multiple = 25, Name = "250" });
            comboBox2.Items.Add(new Space() { Multiple = 30, Name = "300" });
            comboBox2.Items.Add(new Space() { Multiple = 50, Name = "500" });
            comboBox2.Items.Add(new Space() { Multiple = 75, Name = "750" });
            comboBox2.Items.Add(new Space() { Multiple = 100, Name = "1000" });
            comboBox2.Items.Add(new Space() { Multiple = 150, Name = "1500" });
            comboBox2.SelectedIndex = 0;
        }
        
        
        

        public String[] dataY(DataTable dt)
        {
            string[] m = dt.AsEnumerable().Select(d => d.Field<string>("PF_Name")).ToArray();
            return m;
        }
        public double[] spaceY(DataTable dt)
        {
            double[] iNums = dt.AsEnumerable().Select(d => d.Field<double>("PFCenterX")).ToArray();
            return iNums;
        }
        /// <summary>
        /// runtime到达各站的运行时间
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public double[] runtime(DataTable dt)
        {
            double[] s = dt.AsEnumerable().Select(d => d.Field<double>("RunTime")).ToArray();
            return s;
        }
        /// <summary>
        /// stoptime 每一站的停站时间
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// public double[] runtime(DataTable dt)
        public double[] stoptime(DataTable dt)
        {
            double[] s = dt.AsEnumerable().Select(d => d.Field<double>("StopTime")).ToArray();
            return s;
        }

        /// <summary>
        /// downtime 到达该站的下行时间
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// public double[] runtime(DataTable dt)
        public double[] downtime(DataTable dt)
        {
            double[] s = dt.AsEnumerable().Select(d => d.Field<double>("DownTime")).ToArray();
            return s;
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            String[] infoY;

            double[] space;
            double spaceMax;
            double spaceMin;
            double[] runtimes;
            double[] stoptimes;
            double[] downtimes;
            double spaceExtra;
            double proportionY;
            int mulit;
            int Xview;
            int sLength;
            bool flag = true;//用来选择加哪一个时间
            float X, Y;
            double sumX = 0, sumY = 0;
            XY xy;
            IList<XY> mList = new List<XY>();//上行
            IList<XY> dList = new List<XY>();//下行



            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.FileName = null;
            openFileDialog1.Filter = "Excel2007~2013文件(.xlsx)|*xlsx|Excel97~2003文件(.xls)|*.xls";

            if (openFileDialog1.ShowDialog() != DialogResult.OK)
                return;

            DataTable dt = excelControl.datafromexceltable("sevenData", openFileDialog1);
            dataGridView1.DataSource = dt;
            infoY = dataY(dt);
            space = spaceY(dt);
            spaceMax = space.Max();
            spaceMin = space.Min();
            spaceExtra = spaceMax - spaceMin;
            proportionY = 750 / spaceExtra;//比例值

            
            runtimes = runtime(dt);
            stoptimes = stoptime(dt);
            downtimes = downtime(dt);
            Array.Reverse(runtimes);
            Array.Reverse(stoptimes);



            Graphics g = this.CreateGraphics();
            g.Clear(this.BackColor);
            ColorName colorName = (ColorName)comboBox1.SelectedItem;
            Brush brush = new SolidBrush(colorName.Color);
            Pen mypenBlue = new Pen(brush, 1);//线条
            Pen mypenGreen = new Pen(Color.Green, 1);//线条
            Pen mypenRed = new Pen(Color.Red, 1);//线条
            Pen mypenDash = new Pen(Color.Gray, 1);//线条
            mypenDash.DashStyle = System.Drawing.Drawing2D.DashStyle.Custom;
            mypenDash.DashPattern = new float[] { 5, 5 };
            //设置字体颜色
            Font font = new System.Drawing.Font("Arial", 9, FontStyle.Regular);
            //绘制横向线条
            Space SpaceMultile = (Space)comboBox2.SelectedItem;
            mulit = SpaceMultile.Multiple;


            IList<int> XList = new List<int>();
            int b = 0;
            for (int i = 0; i < 2*runtimes.Length; i++)
            {
                b = b + 10;
                XList.Add(b);
            }


            float y = 50;
            for (int i = 0; i < infoY.Length; i++)
            {
                if (i > 0)
                {
                    y = y + (float)(proportionY * (space[i - 1] - space[i]));
                }
                g.DrawLine(mypenBlue, 80, y, 80+ XList.Count*40, y);
                //y轴上对应的标记
                g.DrawString(infoY[i].ToString(), font, Brushes.Green, 10, y - 5); //设置文字内容及输出位置         

            }

            //绘制纵向线条
            float x = 120;
            for (int i = 0; i < XList.Count; i++)
            {
                g.DrawLine(mypenBlue, x, 50, x, y);
                x = x + 40;
            }
            x = 80;
            for (int i = 0; i < XList.Count * 5; i++)
            {
                g.DrawLine(mypenDash, x, 50, x, y);
                x = x + 8;
            }



            //x轴上对应的标记
            x = 110;


            for (int i = 0; i < XList.Count; i++)
            {
                Xview = mulit * XList[i];
                g.DrawString(Xview.ToString(), font, Brushes.Green, x, y); //设置文字内容及输出位置

                x = x + 40;
            }


            ////y轴上对应的标记
            //y = 50;
            //for (int i = 0; i < infoY.Length; i++)
            //{

            //    y = y + 15;
            //}


            PointF startPoint = new PointF(80, y);
            PointF endPointX = new PointF(x+40, y);
            PointF endPointY = new PointF(80, 20);
            PointF endPointXtop = new PointF(x+20, y - 10);
            PointF endPointXbottom = new PointF(x+20, y + 10);
            PointF endPointYtop = new PointF(70, 40);
            PointF endPointYbottom = new PointF(90, 40);

            //X轴
            g.DrawLine(mypenGreen, startPoint, endPointX);
            g.DrawLine(mypenGreen, endPointX, endPointXtop);
            g.DrawLine(mypenGreen, endPointX, endPointXbottom);
            g.DrawString("(t/s)", font, Brushes.Green, x+20, y + 10);

            //Y轴
            g.DrawLine(mypenGreen, startPoint, endPointY);
            g.DrawLine(mypenGreen, endPointY, endPointYtop);
            g.DrawLine(mypenGreen, endPointY, endPointYbottom);
            g.DrawString("(s/km)", font, Brushes.Green, 30, 25);

            //组装上行数据
            sLength = stoptimes.Length + runtimes.Length;
            Array.Reverse(space);
            for (int i = 1; i <= sLength-2; i++)
            {
                if (flag)
                {
                    sumX = sumX + runtimes[(i+1)/2];
                    sumY = space[(i + 1) / 2];
                    flag = false; 
                }else
                {
                    sumX = sumX + stoptimes[i/2];
                    flag = true;
                }
                X = (float)(sumX * 40/ (mulit * XList[0]) + 80);
                Y = (float)(y - (sumY - spaceMin) * proportionY);
                xy = new XY(X, Y);
                mList.Add(xy);
            }

            PointF[] points = new PointF[mList.Count+1];
            points[0] = new PointF(80, y);


            for (int i = 0; i < mList.Count; i++)
            {
                points[i+1] = new PointF(Convert.ToSingle (mList[i].X) , Convert.ToSingle(mList[i].Y));
            }
                
                g.DrawLines(mypenRed, points);


            //组装下行数据、
            sLength = stoptimes.Length + downtimes.Length;
            Array.Reverse(space);
            Array.Reverse(stoptimes);
            flag = true;
            for (int i = 1; i <= sLength - 2; i++)
            {
                if (flag)
                {
                    sumX = sumX + downtimes[(i + 1) / 2];
                    sumY = space[(i + 1) / 2];
                    flag = false;
                }
                else
                {
                    sumX = sumX + stoptimes[i / 2];
                    flag = true;
                }
                X = (float)(sumX * 40 / (mulit * XList[0]) + 80);
                Y = (float)(y - (sumY - spaceMin) * proportionY);
                xy = new XY(X, Y);
                dList.Add(xy);
            }

            PointF[] pointDowns = new PointF[dList.Count + 1];
            pointDowns[0] = points[points.Length - 1];


            for (int i = 0; i < dList.Count; i++)
            {
                pointDowns[i + 1] = new PointF(Convert.ToSingle(dList[i].X), Convert.ToSingle(dList[i].Y));
            }

            g.DrawLines(mypenRed, pointDowns);
            pointDowns = null;
            points = null;

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        //颜色--名称类，用于ComboBox表示颜色
        class ColorName
        {
            public Color Color;
            public string Name;

            public override string ToString()
            {
                return Name;
            }
        }

        //X轴的倍数
        class Space
        {
            public int Multiple;
            public string Name;

            public override string ToString()
            {
                return Name;
            }
        }

        //XY坐标表示
        public struct XY
        {
            public double X;
            public double Y;

            public  XY(double x,double y)
            {
                this.X = x;
                this.Y = y;
            }

            public static implicit operator XY(Point v)
            {
                throw new NotImplementedException();
            }
        }
        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
