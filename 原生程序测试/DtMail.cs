using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 原生程序测试
{
    public partial class DtMail : Form
    {
        public DtMail()
        {
            InitializeComponent();
        }

        private void DtMail_Load(object sender, EventArgs e)
        {

        }
        private static void DataTableToStream(System.Data.DataTable table)
        {
            Microsoft.Office.Interop.Excel.Application xlApp = new Microsoft.Office.Interop.Excel.Application();
            if (xlApp == null)
            {
                return;
            }
            Microsoft.Office.Interop.Excel.Workbooks workbooks = xlApp.Workbooks;
            Microsoft.Office.Interop.Excel.Workbook workbook = workbooks.Add(Microsoft.Office.Interop.Excel.XlWBATemplate.xlWBATWorksheet);
            Microsoft.Office.Interop.Excel.Worksheet worksheet = (Microsoft.Office.Interop.Excel.Worksheet)workbook.Worksheets[1];//取得sheet1 
            //写入标题             
            for (int i = 0; i < table.Columns.Count; i++)
            { worksheet.Cells[1, i + 1] = table.Columns[i].ColumnName; }
            //写入数值
            for (int r = 0; r < table.Rows.Count; r++)
            {
                for (int i = 0; i < table.Columns.Count; i++)
                {
                    worksheet.Cells[r + 2, i + 1] = table.Rows[r][i];
                }
            }
            worksheet.Columns.EntireColumn.AutoFit();//列宽自适应
            workbook.Saved = true;
            workbook.SaveCopyAs(Environment.CurrentDirectory+@"\物料入库.xlsx");
            xlApp.Quit();
            GC.Collect();//强行销毁  
            //var ms = new MemoryStream();
            //var sw = new StreamWriter(ms);


            //const string semiColon = ";";
            //foreach (DataColumn column in table.Columns)
            //{
            //    sw.Write(column.ColumnName);
            //    sw.Write(semiColon);
            //}
            //sw.Write(Environment.NewLine);
            //foreach (DataRow row in table.Rows)
            //{
            //    for (int i = 0; i < table.Columns.Count; i++)
            //    {
            //        sw.Write(row[i].ToString().Replace(semiColon, string.Empty));
            //        sw.Write(semiColon);
            //    }
            //    sw.Write(Environment.NewLine);
            //}
            //return ms;
        }

        public static DataTable ExecuteDataTable(string connstr, string cmdText, params SqlParameter[] para)
        {
            SqlConnection conn = new SqlConnection(connstr);
            SqlCommand cmd = new SqlCommand(cmdText, conn);
            cmd.Parameters.AddRange(para);
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            return dt;
        }
        private void SendMail()
        {
            try
            {
                DataTable dt = ExecuteDataTable("server=192.168.8.49;database=RYDATA;uid=xym;pwd=xym-123", "SELECT * FROM [dbo].[SolidBuyList] where ReceiveTime >='" + DateTime.Today.AddDays(-10).ToString("yyyy-MM-dd") + "'");
                if (dt.Rows.Count > 0)
                { DataTableToStream(dt); }
                else MessageBox.Show("邮件未发送");
                //const string attchmentName = "物料入库.xlsx";
                SmtpClient client = new SmtpClient("192.168.8.3", 25);
                client.Credentials = CredentialCache.DefaultNetworkCredentials;
                client.Credentials = new NetworkCredential("Erp@reyoung.com", "ERP1075+-*/");
                MailMessage mmsg = new MailMessage();
                mmsg.From = new MailAddress("Erp@reyoung.com");
                mmsg.To.Add("zuojinguo@reyoung.com");
                mmsg.Subject = "采购物料到货通知";
                mmsg.Body = "各位领导您好" + "" + "\n" +
                    "编码：\n" +
                    "固水事业部采购:,物料本次到货，请注意查收!" + "" + "\n" +
                    "此邮件为系统邮件，请勿回复!";
                //string ExcelContentType = "application/ms-excel";
                mmsg.Attachments.Add(new Attachment(@"物料入库.xlsx"));
                client.Send(mmsg);
                mmsg.Dispose();
                MessageBox.Show("邮件发送成功");
            }
            catch (Exception ex)
            { MessageBox.Show(""+ex.Message); }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SendMail();
        }
        public void WriteExcel(DataSet ds, string path)
        {
            try
            {
                StreamWriter sw = new StreamWriter(path, false, Encoding.GetEncoding("gb2312"));
                StringBuilder sb = new StringBuilder();
                for (int k = 0; k < ds.Tables[0].Columns.Count; k++)
                {
                    sb.Append(ds.Tables[0].Columns[k].ColumnName.ToString() + "\t");
                }
                sb.Append(Environment.NewLine);

                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    for (int j = 0; j < ds.Tables[0].Columns.Count; j++)
                    {
                        sb.Append(ds.Tables[0].Rows[i][j].ToString() + "\t");
                    }
                    sb.Append(Environment.NewLine);//每写一行数据后换行
                }
                sw.Write(sb.ToString());
                sw.Flush();
                sw.Close();//释放资源
                MessageBox.Show("已经生成指定Excel文件!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


    }
}
