using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.ServiceProcess;
using System.Text;
using System.Threading;

namespace WindowsService1
{
    public partial class Service1 : ServiceBase
    {
        public Service1()
        {
            InitializeComponent();
        }
        readonly string  filePath = @"D:\MyServiceLog.txt";
        //定时器
        System.Timers.Timer tmBak = new System.Timers.Timer();
        System.Threading.Timer ThTimer;
        //服务器启动时写日志、开启定时器
        protected override void OnStart(string[] args)
        {
            using (FileStream stream = new FileStream(filePath, FileMode.Append))
            using (StreamWriter writer = new StreamWriter(stream))
            {
                writer.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "服务启动！");
            }
            SendMail();
            //到时间的时候执行事件 
            tmBak.Interval = 30000;//30S执行一次
            tmBak.AutoReset = true;//执行一次 false，一直执行true 
            //是否执行System.Timers.Timer.Elapsed事件 
            tmBak.Enabled = false;
            tmBak.Start();
            tmBak.Elapsed += new System.Timers.ElapsedEventHandler(WriteLog);

            //ThTimer = new System.Threading.Timer(new System.Threading.TimerCallback(WriteLogTh), null, 0, 30000);
        }

        protected void WriteLog(object source, System.Timers.ElapsedEventArgs e)
        {
            using (FileStream stream = new FileStream(filePath, FileMode.Append))
            using (StreamWriter writer = new StreamWriter(stream))
            {
                writer.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "执行定时任务写操作！");
                int Sint = new Random().Next(1, 20);
                Thread.Sleep(Sint * 1000);
                writer.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "结束执行定时任务写操作！" + Sint);
            }
            SendMail();
        }
        protected void WriteLogTh(object Ob)
        {
            using (FileStream stream = new FileStream(filePath, FileMode.Append))
            using (StreamWriter writer = new StreamWriter(stream))
            {
                writer.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "开始TH执行定时任务写操作！");
                int Sint = new Random().Next(1, 20);
                Thread.Sleep(Sint * 1000);
                writer.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "开始TH执行定时任务写操作！" + Sint);
                //ThTimer.Change(Sint * 1000,30000);
            }
        }
        //服务停止时写日志
        protected override void OnStop()
        {
            tmBak.Stop();
            using (FileStream stream = new FileStream(filePath, FileMode.Append))
            using (StreamWriter writer = new StreamWriter(stream))
            {
                writer.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "服务停止！");
            }
            //ThTimer.Dispose();
        }
        private static Stream DataTableToStream(System.Data.DataTable table)
        {
            var ms = new MemoryStream();
            var sw = new StreamWriter(ms);
            const string semiColon = "\t";
            foreach (DataColumn column in table.Columns)
            {
                sw.Write(column.ColumnName);
                sw.Write(semiColon);
            }
            sw.Write(Environment.NewLine);
            foreach (DataRow row in table.Rows)
            {
                for (int i = 0; i < table.Columns.Count; i++)
                {
                    sw.Write(row[i].ToString().Replace(semiColon, string.Empty));
                    sw.Write(semiColon);
                }
                sw.Write(Environment.NewLine);
            }
            return ms;
        }
        
        private bool SendMail()
        {
            DataTable dt = ExecuteDataTable("server=192.168.8.49;database=RYDATA;uid=xym;pwd=xym-123", "SELECT * FROM [dbo].[SolidBuyList] where ReceiveTime >='" + DateTime.Today.AddDays(-10).ToString("yyyy-MM-dd")+"'");
            if (dt.Rows.Count > 0)
                return SendMail(dt);
            else return false;
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
        private static bool SendMail(DataTable table)
        {
            try
            {
                Microsoft.Office.Interop.Excel.Application xlApp = new Microsoft.Office.Interop.Excel.Application();
                if (xlApp == null)
                {
                    return false;
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
                    for (int ia = 0; ia < table.Columns.Count; ia++)
                    {
                        worksheet.Cells[r + 2, ia + 1] = table.Rows[r][ia];
                    }
                }
                worksheet.Columns.EntireColumn.AutoFit();//列宽自适应
                workbook.Saved = true;
                workbook.SaveCopyAs(@"D:\物料入库.xlsx"); 
                xlApp.Quit();
                GC.Collect();//强行销毁  
                //const string attchmentName = "物料入库.xlsx";
                SmtpClient client = new SmtpClient("192.168.8.3", 25);
                client.Credentials = CredentialCache.DefaultNetworkCredentials;
                client.Credentials = new NetworkCredential("Erp@reyoung.com", "ERP1075+-*/");
                MailMessage mmsg = new MailMessage();
                mmsg.From = new MailAddress("Erp@reyoung.com");
                mmsg.To.Add("zuojinguo@reyoung.com");
                mmsg.Subject ="采购物料到货通知";
                mmsg.Body = "各位领导您好" + "" + "\n" +
                    "编码：\n" +
                    "固水事业部采购:,物料本次到货，请注意查收!" + "" + "\n" +
                    "此邮件为系统邮件，请勿回复!";
                //string ExcelContentType = "application/ms-excel";
                mmsg.Attachments.Add(new Attachment(@"D:\物料入库.xlsx"));
                client.Send(mmsg);
                mmsg.Dispose();
                
                return true;
            } 
            catch (Exception ex)
            { return false; }
        }
        public void test()
        {
            Hashtable ht = new Hashtable();
            ht.Add("A", "1");
            ht.Add("B", "2");
            ht.Add("C", "3");
            ht.Add("D", "4");
            ArrayList list = new ArrayList(ht.Keys);
            list.Sort();
            foreach (string key in list)
            {
                Console.WriteLine("Key : {0} ; Value : {1}.", key, ht[key]);
            }
        }
    }
}
