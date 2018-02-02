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
        }
        public String[] infoX;
        public String[] infoY;
        
        public double[] space;
        public double spaceMax;
        public double spaceMin;
        public double spaceExtra;
        public double proportionY;
        public String[] dataX()
        {
            String[] n = { " 10", " 20", " 30", " 40", " 50", " 60", " 70",
               " 80", " 90", " 100", "110", "120", "130", "140", "150", "160", "170"};
            return n;
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
        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.FileName = null;
            openFileDialog1.Filter = "Excel2007~2013文件(.xlsx)|*xlsx|Excel97~2003文件(.xls)|*.xls";

            if (openFileDialog1.ShowDialog() != DialogResult.OK)
                return;

            DataTable dt = excelControl.datafromexceltable("sevenData", openFileDialog1);
            dataGridView1.DataSource = dt;
            infoX = dataX();
            infoY = dataY(dt);
            space = spaceY(dt);
            spaceMax = space.Max();
            spaceMin = space.Min();
            spaceExtra = spaceMax - spaceMin;
            proportionY = 600/ spaceExtra;


            Graphics g = this.CreateGraphics();
            Pen mypenBlue = new Pen(Color.Blue, 1);//线条
            Pen mypenRed = new Pen(Color.Red, 1);//线条
            Pen mypenDash = new Pen(Color.Gray, 1);//线条
            mypenDash.DashStyle = System.Drawing.Drawing2D.DashStyle.Custom;
            mypenDash.DashPattern = new float[] { 5, 5 };
            //设置字体颜色
            Font font = new System.Drawing.Font("Arial", 9, FontStyle.Regular);
            //绘制横向线条
            int y = 50;
            for (int i = 0; i < infoY.Length; i++)
            {
                if (i > 0)
                {
                    y = y + (int)(proportionY * (space[i-1] - space[i]));
                }
                g.DrawLine(mypenBlue, 80, y, 760, y);
                //y轴上对应的标记
                g.DrawString(infoY[i].ToString(), font, Brushes.Red, 10, y-5); //设置文字内容及输出位置         

            }

            //绘制纵向线条
            int x = 120;
            for (int i = 0; i < 17; i++)
            {
                g.DrawLine(mypenBlue, x, 50, x, y);
                x = x + 40;
            }
            x = 80;
            for (int i = 0; i < 17*4; i++)
            {
                g.DrawLine(mypenDash, x, 50, x, y);
                x = x + 10;
            }
            

            

            //x轴上对应的标记
            
            x = 110;
            for (int i = 0; i < 17; i++)
            {
                g.DrawString(infoX[i].ToString(), font, Brushes.Red, x, y); //设置文字内容及输出位置

                x = x + 30;
            }

            ////y轴上对应的标记
            //y = 50;
            //for (int i = 0; i < infoY.Length; i++)
            //{
              
            //    y = y + 15;
            //}


            Point startPoint = new Point(80, y);
            Point endPointX = new Point(800, y);
            Point endPointY = new Point(80, 30);
            g.DrawLine(mypenRed, startPoint, endPointX);
            g.DrawLine(mypenRed, startPoint, endPointY);
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
