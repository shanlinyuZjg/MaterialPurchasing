using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SpecialOperate
{
    public partial class Form1 : Form
    {
        public static readonly string FSDBConnstr = "server=192.168.8.11;database=FSDB;uid=xym;pwd=xym-123";

        public static readonly string FSDBMRConnstr = "server=192.168.8.11;database=fsdbmr;uid=program;pwd=program";
        public static readonly string oledbconnstrFSDBMR = "Provider=SQLOLEDB.1;Persist Security Info=True;Use Procedure for Prepare=1;Auto Translate=false;Data Source=192.168.8.11;Initial Catalog=fsdbmr;User ID=program;Password=program";
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            DataTable dt = SQLHelper.GetDataTable(FSDBConnstr, "SELECT * FROM [dbo].[ManufacturerNumberName]");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                object ob = SQLHelper.OleDBExecuteScalar(oledbconnstrFSDBMR, "SELECT VendorName FROM [dbo].[_NoLock_FS_Vendor] where VendorID ='" + dt.Rows[i]["ManufacturerNumber"].ToString() + "'");
                string VendorName = ob is null?"":ob.ToString();
                SQLHelper.ExecuteScalar(FSDBConnstr, "UPDATE [dbo].[ManufacturerNumberName] SET  [UpdateHistory] = '"+VendorName+"' WHERE [ManufacturerNumber] = '"+dt.Rows[i]["ManufacturerNumber"].ToString() +"'");
            }
            MessageBox.Show("完成");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Encoding EncodingLD = Encoding.GetEncoding("ISO-8859-1");
            Encoding EncodingCH = Encoding.GetEncoding("GB2312");
            DataTable dt = SQLHelper.GetDataTable(FSDBConnstr, "SELECT * FROM [dbo].[ManufacturerNumberName]");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                object ob = SQLHelper.OleDBExecuteScalar(oledbconnstrFSDBMR, "SELECT VendorID FROM [dbo].[_NoLock_FS_Vendor] where VendorName ='" + EncodingLD.GetString(EncodingCH.GetBytes(dt.Rows[i]["ManufacturerName"].ToString())) + "'");
                string VendorName = ob is null ? "" : ob.ToString();
                SQLHelper.ExecuteScalar(FSDBConnstr, "UPDATE [dbo].[ManufacturerNumberName] SET  Remark = '" + VendorName + "' WHERE [ManufacturerName] = '" + dt.Rows[i]["ManufacturerName"].ToString() + "'");
            }
            MessageBox.Show("完成");
        }
    }
}
