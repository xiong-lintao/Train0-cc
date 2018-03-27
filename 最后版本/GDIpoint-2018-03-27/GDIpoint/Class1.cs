using System;
using System.Windows.Forms;
using System.Data.OleDb;
using System.Data;

namespace GDIpoint
{
    class EXCELCONTROL
    {
        #region 从EXCEL中读取基础源数据文档
        public DataView dv;
        public System.Data.DataTable datafromexceltable(string strSheetName, OpenFileDialog openFileDialog1)
        {
            bool openok = false;
            System.Data.DataTable datafromexceltable = null;
            string strConn = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + openFileDialog1.FileName + ";Extended Properties='Excel 12.0 Xml;HDR=Yes;IMEX=1'";
            //  string strConn = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source='" + openFileDialog1.FileName + "';Extended Properties='Excel 12.0;HDR=YES;'";
            //选择页面

            try
            {
                OleDbConnection OleConn = new OleDbConnection(strConn);
                OleConn.Open();
                string sql = "SELECT PF_Name,PFCenterX,StopTime,RunTime,DownTime FROM[" + strSheetName + "$]";//可是更改Sheet名称，比如sheet2，等等    
                OleDbDataAdapter OleDaExcel = new OleDbDataAdapter(sql, OleConn);
                DataSet OleDsExcle = new DataSet();
                OleDaExcel.Fill(OleDsExcle, strSheetName);
                OleConn.Close();
                datafromexceltable = OleDsExcle.Tables[0];
                dv = new DataView(datafromexceltable);
                dv.Sort = "PFCenterX desc";
                datafromexceltable = dv.ToTable(true, "PF_Name", "PFCenterX", "StopTime", "RunTime", "DownTime");

            }
            catch (Exception ex)
            {
                if (strSheetName != "d2dinfo")
                {
                    MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    MessageBox.Show("无法打开文档，请确定此文件是否被占用或路径出错！ERROR:002");
                }

            }

            return datafromexceltable;

        }
        #endregion
    }
}
