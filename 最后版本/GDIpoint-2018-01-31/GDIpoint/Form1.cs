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
        public String[] dataX()
        {
            String[] n = { " 10", " 20", " 30", " 40", " 50", " 60", " 70",
               " 80", " 90", " 100", "110", "120", "130", "140", "150", "160", "170"};
            return n;
        }
        public String[] dataY(DataTable dt)
        {
            string[] m = dt.AsEnumerable().Select(d => d.Field<string>("PF_Name")).ToArray();
            //String[] m = { "40", "35", "30", "25", "20", "15", "10", " 5" };
            return m;
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


            Graphics g = this.CreateGraphics();
            Pen mypenBlue = new Pen(Color.Blue, 1);//线条
            Pen mypenRed = new Pen(Color.Red, 1);//线条
            

            //绘制横向线条
            int y = 50;
            for (int i = 0; i < infoY.Length; i++)
            {
                g.DrawLine(mypenBlue, 80, y, 760, y);
                y = y + 15;
            }

            //绘制纵向线条
            int x = 120;
            for (int i = 0; i < 17; i++)
            {
                g.DrawLine(mypenBlue, x, 50, x, y);
                x = x + 40;
            }

            //设置字体颜色
            Font font = new System.Drawing.Font("Arial", 9, FontStyle.Regular);

            //x轴上对应的标记
            
            x = 110;
            for (int i = 0; i < 17; i++)
            {
                g.DrawString(infoX[i].ToString(), font, Brushes.Red, x, y); //设置文字内容及输出位置

                x = x + 30;
            }

            //y轴上对应的标记
            y = 50;
            for (int i = 0; i < infoY.Length; i++)
            {
                g.DrawString(infoY[i].ToString(), font, Brushes.Red, 10, y); //设置文字内容及输出位置
                y = y + 15;
            }


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
