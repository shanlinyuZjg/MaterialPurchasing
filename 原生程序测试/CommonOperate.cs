using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Global.Helper;
using System.Data;
using System.Data.SqlClient;
using System.Data.OleDb;

using System.Drawing;
using System.IO;

using System.Runtime.InteropServices;

namespace Global
{
    public class CommonOperate
    {
        //用于同步工作站和数据库服务器的日期时间
        [DllImport("Kernel32.dll")]
        public static extern bool SetSystemTime(ref SystemTime sysTime);
        [DllImport("Kernel32.dll")]
        public static extern void GetSystemTime(ref SystemTime sysTime);
        [StructLayout(LayoutKind.Sequential)]
        public struct SystemTime
        {
            public ushort wYear;
            public ushort wMonth;
            public ushort wDayOfWeek;
            public ushort wDay;
            public ushort wHour;
            public ushort wMinute;
            public ushort wSecond;
        }

        public static void SyncTime(DateTime currentTime)
        {
            SystemTime sysTime = new SystemTime();
            sysTime.wYear = Convert.ToUInt16(currentTime.Year);
            sysTime.wMonth = Convert.ToUInt16(currentTime.Month);
            sysTime.wDay = Convert.ToUInt16(currentTime.Day);
            sysTime.wDayOfWeek = Convert.ToUInt16(currentTime.DayOfWeek);
            sysTime.wMinute = Convert.ToUInt16(currentTime.Minute);
            sysTime.wSecond = Convert.ToUInt16(currentTime.Second);
  //          sysTime.wMiliseconds = Convert.ToUInt16(currentTime.Millisecond);

            //处理北京时间 
            int nBeijingHour = currentTime.Hour - 8;
            if (nBeijingHour <= 0)
            {
                nBeijingHour = 24;
                sysTime.wDay = Convert.ToUInt16(currentTime.Day - 1);
                //sysTime.wDayOfWeek = Convert.ToUInt16(current.DayOfWeek - 1); 
            }
            else
            {
                sysTime.wDay = Convert.ToUInt16(currentTime.Day);
                sysTime.wDayOfWeek = Convert.ToUInt16(currentTime.DayOfWeek);
            }
            sysTime.wHour = Convert.ToUInt16(nBeijingHour);

            SetSystemTime(ref sysTime);//设置本机时间 
        }

        public static void SyncServerTime()
        {
            string sql = "Select  CONVERT(varchar(100), GETDATE(), 20)";
            object o = SQLHelper.ExecuteScalar(GlobalSpace.FSDBConnstr, sql);
            DateTime dt = Convert.ToDateTime(o);
            //LocalTimeSync.SyncTime(dt); 

            //比较时间一致性 
            if (DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss") != dt.ToString())
            {
                SyncTime(dt);
            }
        }

        //DGV控件展示数据
        public static void DataGridViewShow(string strSql,string conn,DataGridView dgv,params SqlParameter[] sp)
        {
            DataTable dt = SQLHelper.GetDataTable(conn, strSql, sp);
            dgv.DataSource = dt;
        }
        //DGV控件展示数据
        public static void DataGridViewOleDbShow(string strSql, string connOledb, DataGridView dgv, params OleDbParameter[] sp)
        {
            DataTable dt = SQLHelper.GetDataTableOleDb(connOledb, strSql, sp);
            dgv.DataSource = dt;
        }
        //清空DGV控件显示的内容
        public static void EmptyDataGridView(DataGridView dgv)
        {
            DataTable dtTemp = (DataTable)dgv.DataSource;
            if (dtTemp != null)
            {
                if (dtTemp.Rows.Count > 0)
                {
                    dtTemp.Rows.Clear();
                }
            }
            dgv.DataSource = dtTemp;
        }
        
        //清空DGV控件显示的内容
        /*
        public static void EmptyDataGridView(DataGridViewX dgv)
        {
            DataTable dtTemp = (DataTable)dgv.DataSource;
            if(dtTemp != null)
            {
                if (dtTemp.Rows.Count > 0)
                {
                    dtTemp.Rows.Clear();
                }
            }

            dgv.DataSource = dtTemp;
        }*/
        //光标焦点从一个TextBox控件跳到另一个TextBox控件
        public static void TextBoxNext(TextBox tbPre,TextBox tbPost, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                if (string.IsNullOrEmpty(tbPre.Text.Trim()))
                {
                    MessageBox.Show(tbPre.Tag+"不能为空！","提示");
                }
                else
                {
                    tbPost.Focus();
                }
            }
        }

        //获取四班中物料信息
        public static List<string> GetItemInfo(string itemNumber)
        {
            List<string> list = new List<string>();
            DataTable dtTemp = null;
            string sqlSelect = @"Select  ItemDescription,ItemUM From _NoLock_FS_Item Where ItemNumber='" + itemNumber + "'";
            dtTemp = SQLHelper.GetDataTableOleDb(GlobalSpace.oledbconnstrFSDBMR, sqlSelect);
            if (dtTemp.Rows.Count > 0)
            {
                list.Add(dtTemp.Rows[0]["ItemUM"].ToString());
                list.Add(dtTemp.Rows[0]["ItemDescription"].ToString());
            }
            return list;
        }
        //获取四班中物料的库管员
        public static string GetItemStockKeeper(string itemNumber)
        {
            string stockKeeper = "";
            string sqlSelect = @"Select (T2.UserID+'|'+T2.UserName) AS StockKeeper  From _NoLock_FS_Item T1,FS_UserAccess T2 Where T1.ItemReference3 = T2.UserID And ItemNumber='" + itemNumber + "'";
            DataTable dtTemp = SQLHelper.GetDataTableOleDb(GlobalSpace.oledbconnstrFSDBMR, sqlSelect);
            if (dtTemp.Rows.Count > 0)
            {
                stockKeeper = dtTemp.Rows[0]["StockKeeper"].ToString();
            }
            return stockKeeper;
        }
        //获取四班中供应商信息
        public static string GetVendorInfo(string vendorNumber)
        {
            string vendorName = string.Empty;
            DataTable dtTemp = null;
            string sqlSelect = @"Select  VendorName From _NoLock_FS_Vendor Where VendorID='" + vendorNumber + "'";
            dtTemp = SQLHelper.GetDataTableOleDb(GlobalSpace.oledbconnstrFSDBMR, sqlSelect);
            if (dtTemp.Rows.Count > 0)
            {
                vendorName = dtTemp.Rows[0]["VendorName"].ToString();
            }
            else
            {
                vendorName = "";
            }
            return vendorName;
        }
        //获取上级领导代码
        public static string GetSuperiorId(string struserid)
        {
            string supervisorID = string.Empty;
            string strSelect = @"Select SupervisorID from PurchaseDepartmentRBACByCMF where UserID='" + struserid + "'";
            DataTable dtTemp = SQLHelper.GetDataTable(GlobalSpace.FSDBConnstr, strSelect);
            if (dtTemp.Rows.Count > 0)
            {
                supervisorID = dtTemp.Rows[0]["SupervisorID"].ToString();
            }
            else
            {
                supervisorID = "";
            }
            return supervisorID;
        }
        //获取下级员工列表
        public static DataTable GetSubordinate(string struserid)
        {
            string sqlSelect = @"Select UserID,Name From PurchaseDepartmentRBACByCMF Where SupervisorID='"+struserid+"'";
            return SQLHelper.GetDataTable(GlobalSpace.FSDBConnstr, sqlSelect);          
        }
        //将查询到的数据绑定到ComboBox控件中
        public static void ComboBoxBind(ComboBox cbb,DataTable dt,string strDisplay,string strValue)
        {
            cbb.DisplayMember = strDisplay;
            cbb.ValueMember = strValue;
            cbb.DataSource = dt;
            cbb.SelectedIndex = -1;
        }
        //将查询到的数据绑定到ComboBoxEx控件中
      
        //库存物料对比
        public static DataTable InventoryItemReminder(DataTable dt)
        {
            DataTable dtTemp = null;
            DataTable dtReturn = new DataTable();
            dtReturn.Columns.Add(new DataColumn("物料代码", typeof(string)));
            dtReturn.Columns.Add(new DataColumn("物料描述", typeof(string)));
            dtReturn.Columns.Add(new DataColumn("合格可用数量", typeof(double)));
            dtReturn.Columns.Add(new DataColumn("待检数量", typeof(double)));
            dtReturn.Columns.Add(new DataColumn("库存总数量", typeof(double)));
            dtReturn.Columns.Add(new DataColumn("库存警戒数量", typeof(double)));

            if(dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    string sqlSelect = @"SELECT
	                                            T2.ItemNumber,
	                                            T1.InventoryQuantity,
	                                            T1.InventoryCategory
                                            FROM
	                                            _NoLock_FS_ItemInventory T1,
	                                            _NoLock_FS_Item T2
                                            WHERE
	                                            T1.ItemKey = T2.ItemKey
                                            AND
	                                            T2.ItemNumber='" + dr["ItemNumber"].ToString() + "'";
                    dtTemp = SQLHelper.GetDataTableOleDb(GlobalSpace.oledbconnstrFSDBMR, sqlSelect);
                    double quantityStatusO = 0, quantityStatusI = 0;

                    DataRow drNew = null;
                    if (dtTemp.Rows.Count > 0)
                    {
                        foreach (DataRow dr2 in dtTemp.Rows)
                        {
                            if (dr2["InventoryCategory"].ToString() == "O")
                            {
                                quantityStatusO += Convert.ToDouble(dr2["InventoryQuantity"]);
                            }
                            else if (dr2["InventoryCategory"].ToString() == "I")
                            {
                                quantityStatusI += Convert.ToDouble(dr2["InventoryQuantity"]);
                            }
                        }

                        if (quantityStatusO + quantityStatusI <= Convert.ToDouble(dr["MinimumQuantity"]))
                        {
                            drNew = dtReturn.NewRow();
                            drNew[0] = dr["ItemNumber"];
                            drNew[1] = dr["ItemDescription"];
                            drNew[2] = quantityStatusO;
                            drNew[3] = quantityStatusI;
                            drNew[4] = quantityStatusO + quantityStatusI;
                            drNew[5] = dr["MinimumQuantity"];
                            dtReturn.Rows.Add(drNew);
                        }
                    }

                }
            }


            return dtReturn;
        }
        //字符串进行Base64加密
        public static string Base64Encrypt(string strXYZ)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(strXYZ);
            return Convert.ToBase64String(bytes);
        }
        //Base64字符串解密
        public static string Base64Decrypt(string strXXX)
        {
            byte[] debytes = Convert.FromBase64String(strXXX);
            return Encoding.UTF8.GetString(debytes);
        }
        //获得SMTP服务器IP和端口号
        public static List<string> GetSMTPServerInfo()
        {
            List<string> list = new List<string>(2);
            string sqlSelect = @"Select SmtpServer,Port From PurchaseDepartmentSMTPServerByCMF";
            DataTable dt = SQLHelper.GetDataTable(GlobalSpace.FSDBConnstr, sqlSelect);
            if(dt.Rows.Count > 0)
            {
                list.Add(dt.Rows[0]["SmtpServer"].ToString());
                list.Add(dt.Rows[0]["Port"].ToString());
            }
            return list;
        }

        //将打开的窗体绑定到TabControl控件
        public static void BindFormToTabControl(System.Windows.Forms.TabControl tabControl, Form f, string strName, string strText)
        {
            if (!IsTabpageExist(strName, tabControl))
            {
                TabPage tab = new TabPage();
                tab.Name = strName;
                tab.Text = strText;
                tab.BackColor = Color.FromArgb(185, 215, 255);

                f.TopLevel = false;  //设置为非顶级控件
                f.Dock = DockStyle.Fill;
                f.FormBorderStyle = FormBorderStyle.None;
                tab.Controls.Add(f);
                tabControl.TabPages.Add(tab);
                f.Show();
                tabControl.SelectTab(strName);
            }
        }

        //检测是否已经打开tabpage，没有的话返回false，有的话返回true
        public static bool IsTabpageExist(string strName, System.Windows.Forms.TabControl tc)
        {
            foreach (TabPage tpPage in tc.TabPages)
            {
                if (tpPage.Name == strName)
                {
                    //   tc.SelectTab(tpPage);
                    //下边的代码同样能打开已经存在的tabpage
                    tc.SelectTab(strName);
                    return true;
                }
            }
            return false;
        }
       

        //
        public static void GetExcelFileInfo(TextBox tb,ComboBox cbb)
        {
            string fileExtension = string.Empty; ;
            string xlsConn = string.Empty; ;
            string xlsxConn = string.Empty;
            string strConn = string.Empty;

            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "EXCEL文件(*.xls,*.xlsx)|*.xls;*.xlsx|所有文件(*.*)|*.*";//用引号隔开多种格式
            ofd.FileName = "";
            ofd.FilterIndex = 1;//选中一个文件
            FileInfo file;
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                tb.Text = ofd.FileName.ToString();
                file = new FileInfo(tb.Text);
                fileExtension = file.Extension.ToUpper();
                switch (fileExtension)
                {
                    case ".XLS":
                        strConn = @"provider=Microsoft.Jet.OLEDB.4.0; data source=" + tb.Text.ToString().Trim() + "; extended properties='Excel 8.0; HDR=Yes; IMEX=1'";
                        xlsConn = strConn;
                        break;
                    case ".XLSX":
                        strConn = @"provider=Microsoft.ACE.OLEDB.12.0; data source=" + tb.Text.ToString().Trim() + "; extended properties='Excel 8.0; HDR=Yes; IMEX=1'";
                        xlsxConn = strConn;
                        break;
                    default:
                        strConn = @"provider=Microsoft.Jet.OLEDB.4.0; data source=" + tb.Text.ToString().Trim() + "; extended properties='Excel 8.0; HDR=Yes; IMEX=1'";
                        break;
                }
                OleDbConnection oleConn = new OleDbConnection(strConn);
                #region 获取表名并保存到ComboBox控件cbb内
                try
                {
                    oleConn.Open();
                    System.Data.DataTable dtSheetName = oleConn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                    string[] strTableNames = new string[dtSheetName.Rows.Count];
                    cbb.Items.Clear();
                    for (int k = 0; k < dtSheetName.Rows.Count; k++)
                    {
                        cbb.Items.Add(dtSheetName.Rows[k][2].ToString());
                    }
                    oleConn.Close();
                    oleConn.Dispose();
                    cbb.SelectedIndex = 0;
                }
                catch (Exception error)
                {
                    MessageBox.Show(error.Message, "错误提示！");
                }
                finally
                {
                    oleConn.Close();
                    oleConn.Dispose();
                }
             
                #endregion
            }
        }

        //
        public static void ImportExcelFile(string strsql,DataGridView dgv,TextBox tb)
        {
            string fileExtension = string.Empty; ;
            string xlsConn = string.Empty; ;
            string xlsxConn = string.Empty;
            string strConn = string.Empty;
            string sheetname = string.Empty;
            DataTable dtTemp = null;
            dgv.Columns.Clear();

            if(tb.Text.ToString().IndexOf(".xlsx") > 0)
            {
                fileExtension = ".xlsx";
            }
            else if(tb.Text.ToString().IndexOf(".xls") > 0)
            {
                fileExtension = ".xls";
            }

            switch (fileExtension)
            {
                case ".xls":
                    strConn = @"provider=Microsoft.Jet.OLEDB.4.0; data source=" + tb.Text.ToString() + "; extended properties='Excel 8.0; HDR=Yes; IMEX=1'";
                    xlsConn = strConn;
                    break;
                case ".xlsx":
                    strConn = @"provider=Microsoft.ACE.OLEDB.12.0; data source=" + tb.Text.ToString()+ "; extended properties='Excel 8.0; HDR=Yes; IMEX=1'";
                    xlsxConn = strConn;
                    break;
                default:
                    strConn = @"provider=Microsoft.Jet.OLEDB.4.0; data source=" + tb.Text.ToString()+ "; extended properties='Excel 8.0; HDR=Yes; IMEX=1'";
                    break;
            }

            OleDbConnection oleConn = new OleDbConnection(strConn);
            try
            {
                oleConn.Open();
                OleDbCommand oleCmd = new OleDbCommand(strsql, oleConn);
                OleDbDataAdapter da = new OleDbDataAdapter(oleCmd);
                DataSet ds = new DataSet();
                da.Fill(ds, "dtselectedExcelContent");
                dtTemp = ds.Tables["dtselectedExcelContent"];
                MessageBox.Show("共有" + dtTemp.Rows.Count + "条记录");

                dgv.DataSource = dtTemp;
                oleConn.Close();
                oleConn.Dispose();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                oleConn.Close();
                oleConn.Dispose();
            }
            //设置dgv中指定列的数字保留的小数位数
         //   dgv.Columns["数量"].DefaultCellStyle.Format = "000000000";
        }

        //
        public static DataTable ImportExcelFile(string strsql, TextBox tb)
        {
            string fileExtension = string.Empty; ;
            string xlsConn = string.Empty; ;
            string xlsxConn = string.Empty;
            string strConn = string.Empty;
            string sheetname = string.Empty;
            DataTable dtTemp = null;

            if (tb.Text.ToString().IndexOf(".xlsx") > 0)
            {
                fileExtension = ".xlsx";
            }
            else if (tb.Text.ToString().IndexOf(".xls") > 0)
            {
                fileExtension = ".xls";
            }

            switch (fileExtension)
            {
                case ".xls":
                    strConn = @"provider=Microsoft.Jet.OLEDB.4.0; data source=" + tb.Text.ToString() + "; extended properties='Excel 8.0; HDR=Yes; IMEX=1'";
                    xlsConn = strConn;
                    break;
                case ".xlsx":
                    strConn = @"provider=Microsoft.ACE.OLEDB.12.0; data source=" + tb.Text.ToString() + "; extended properties='Excel 8.0; HDR=Yes; IMEX=1'";
                    xlsxConn = strConn;
                    break;
                default:
                    strConn = @"provider=Microsoft.Jet.OLEDB.4.0; data source=" + tb.Text.ToString() + "; extended properties='Excel 8.0; HDR=Yes; IMEX=1'";
                    break;
            }

            OleDbConnection oleConn = new OleDbConnection(strConn);
            try
            {
                oleConn.Open();
                OleDbCommand oleCmd = new OleDbCommand(strsql, oleConn);
                OleDbDataAdapter da = new OleDbDataAdapter(oleCmd);
                DataSet ds = new DataSet();
                da.Fill(ds, "dtselectedExcelContent");
                dtTemp = ds.Tables["dtselectedExcelContent"];
                MessageBox.Show("共有" + dtTemp.Rows.Count + "条记录");
                oleConn.Close();
                oleConn.Dispose();

            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                oleConn.Close();
                oleConn.Dispose();
            }
            return dtTemp;
            //设置dgv中指定列的数字保留的小数位数
            //   dgv.Columns["数量"].DefaultCellStyle.Format = "000000000";
        }
        /// <summary>
        /// 将excel导入到datatable  
        /// </summary>
        /// <param name="filePath">Excel文件路径</param>
        /// <param name="sheetName">读取的Sheet的名字</param>
        /// <param name="isColumnName">第一行是否列名</param>
        /// <returns></returns>
    
   

        /// <summary>
        /// 清空List内的元素
        /// </summary>
        /// <param name="list">要清空的List</param>
        public static void EmptyList(List<string> list)
        {
            if(list.Count > 0)
            {
                list.Clear();
            }
        }

        /// <summary>
        /// 判断字符串是否为数字或英文字符
        /// </summary>
        /// <param name="str">传入字符串</param>
        /// <returns>返回bool类型</returns>
        public static bool IsNumberOrString(string str)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(str, @"(?i)^[0-9a-z]+$"))
            {
                return true;
            }
            return false;
        }
        /// <summary>
        /// 对比当前程序版本和数据库中程序版本
        /// </summary>
        /// <param name="applicationName">当前程序名称</param>
        /// <param name="currentApplictionVersion">当前程序版本</param>
        /// <returns></returns>
        public static bool IsApplicationVersionValid(string applicationName,string currentApplictionVersion)
        {
            string serverApplicationVersion = GetServerApplicationVersion(applicationName);
            if (Convert.ToDouble(serverApplicationVersion) <= Convert.ToDouble(currentApplictionVersion))
            {
                return true;
            }
            return false;
        }
        /// <summary>
        /// 获取当前程序在数据库中的版本信息
        /// </summary>
        /// <param name="applicationName">当前程序名称</param>
        /// <returns></returns>
        public static string GetServerApplicationVersion(string applicationName)
        {
            string returnValue = string.Empty;
            string sqlSelect = "SELECT Version From ApplicationVersionByCMF Where Name = '" + applicationName + "'";
            DataTable dt = SQLHelper.GetDataTable(GlobalSpace.FSDBConnstr, sqlSelect);
            if(dt.Rows.Count > 0)
            {
                returnValue = dt.Rows[0]["Version"].ToString();
            }
            return returnValue;
        }
    }
}
