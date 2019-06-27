using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Global.Helper;
using System.Data;
using System.Data.SqlClient;
using System.Data.OleDb;
using DevComponents.DotNetBar.Controls;
using System.Drawing;
using System.IO;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using NPOI.HSSF.UserModel;
using System.Runtime.InteropServices;
using SoftBrands.FourthShift.Transaction;

namespace Global
{
    public class CommonOperate
    {
        //定义简体中文和西欧文编码字符集
        public static Encoding GB2312 = Encoding.GetEncoding("gb2312");
        public static Encoding ISO88591 = Encoding.GetEncoding("iso-8859-1");

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

        public static bool WriteFSErrorLog(string type, ITransaction transaction,FSTIError fstiError, string id)
        {
            StringBuilder error = new StringBuilder();
            for (int i = 0; i < fstiError.NumberOfFieldsInError; i++)
            {
                int field = fstiError.GetFieldNumber(i);
               error.Append(String.Format("字段[{0}]: {1}", i, field));
                ITransactionField myField = transaction.get_Field(field);
                error.Append(String.Format("字段名称: {0}", myField.Name));
            }
            string sqlInsert = @"Insert Into FSErrorLogByCMF (Type,ErrorContent,Operator) Values ('"+type+"','"+GB2312.GetString(ISO88591.GetBytes(fstiError.Description)) +"','"+id+"')";

            if(SQLHelper.ExecuteNonQuery(GlobalSpace.FSDBConnstr,sqlInsert) )
            {
                return true;
            }
            return false;
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
        }
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
        public static void ComboBoxBindEx(ComboBoxEx cbbEx, DataTable dt, string strDisplay, string strValue)
        {
            cbbEx.DisplayMember = strDisplay;
            cbbEx.ValueMember = strValue;
            cbbEx.DataSource = dt;
            cbbEx.SelectedIndex = -1;
        }
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
       
        //将打开的窗体绑定到TabControl控件
        public static void BindFormToTabControlX(DevComponents.DotNetBar.TabControl tabControl, DevComponents.DotNetBar.Office2007Form frm, string strName, string strText)
        {
            if (!IsTabpageExistX(strName, tabControl))
            {
                DevComponents.DotNetBar.TabItem ti = tabControl.CreateTab(strText);
                DevComponents.DotNetBar.TabControlPanel tcp = new DevComponents.DotNetBar.TabControlPanel();
                ti.AttachedControl = tcp;
                ti.Name = strName;
                ti.Text = strText;
                tcp.TabItem = ti;
                tcp.Dock = DockStyle.Fill;
                tcp.Name = 
                tcp.Name = strName+"TabCTLPanel";
                frm.Dock = DockStyle.Fill;
                frm.WindowState = FormWindowState.Maximized;
                frm.TopLevel = false;
                frm.FormBorderStyle = FormBorderStyle.None;
                
                tcp.Controls.Add(frm);               
                tabControl.Controls.Add(tcp);
                frm.Show();
                tabControl.SelectedTab = ti;
            }
        }
        //检测是否已经打开tabitem，没有的话返回false，有的话返回true
        public static bool IsTabpageExistX(string tabName,DevComponents.DotNetBar.TabControl tc)
        {
            bool isOpened = false;
            foreach (DevComponents.DotNetBar.TabItem ti in tc.Tabs)
            {
                if (ti.Name == tabName)
                {
                    isOpened = true;
                    tc.SelectedTab = ti;
                    break;
                }
            }
            return isOpened;
        }

        //将打开的窗体绑定到TabControl控件
        public static void BindFormToTabControl(DevComponents.DotNetBar.TabControl tabControl, DevComponents.DotNetBar.Office2007Form frm, string strName, string strText)
        {
            /*
            if (!IsTabpageExist(strName, tabControl))
            {
                DevComponents.DotNetBar.TabItem ti = tabControl.CreateTab(strText);
                DevComponents.DotNetBar.TabControlPanel tcp = new DevComponents.DotNetBar.TabControlPanel();
                ti.AttachedControl = tcp;
                ti.Name = strName;
                ti.Text = strText;
                tcp.TabItem = ti;
                tcp.Dock = DockStyle.Fill;
                tcp.Location = new System.Drawing.Point(0, 0);
                tcp.Name = strName + "TabCTLPanel";
                frm.Dock = DockStyle.Fill;
                frm.WindowState = FormWindowState.Maximized;
                frm.TopLevel = false;
                frm.FormBorderStyle = FormBorderStyle.None;

                tcp.Controls.Add(frm);
                tabControl.Controls.Add(tcp);
                frm.Show();
                tabControl.SelectedTab = ti;
            }*/
            DevComponents.DotNetBar.TabItem tp = new DevComponents.DotNetBar.TabItem();
            DevComponents.DotNetBar.TabControlPanel tcp = new DevComponents.DotNetBar.TabControlPanel();
            tcp.Dock = System.Windows.Forms.DockStyle.Fill;
            tcp.Location = new System.Drawing.Point(0, 0);

            frm.TopLevel = false;
            
            frm.FormBorderStyle = FormBorderStyle.None;
            
            frm.Dock = System.Windows.Forms.DockStyle.Fill;
            tcp.Controls.Add(frm);
            frm.Show();
            tp.Text = strText;
            tp.Name = strName;
           

            if (!IsOpenTab(strName, tabControl))
            {
                tcp.TabItem = tp;
                tp.AttachedControl = tcp;
                tabControl.Controls.Add(tcp);
                tabControl.Tabs.Add(tp);
                tabControl.SelectedTab = tp;
            }
            tabControl.Refresh();
        }
        //检测是否已经打开tabitem，没有的话返回false，有的话返回true
        public static bool IsOpenTab(string tabName, DevComponents.DotNetBar.TabControl tc)
        {
            bool isOpened = false;
            foreach (DevComponents.DotNetBar.TabItem ti in tc.Tabs)
            {
                if (ti.Name == tabName)
                {
                    isOpened = true;
                    tc.SelectedTab = ti;
                    break;
                }
            }
            return isOpened;
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
        public static DataTable ExcelToDataTable(string filePath, string sheetName,bool isColumnName)
        {
            DataTable dataTable = null;
            FileStream fs = null;
            DataColumn column = null;
            DataRow dataRow = null;
            IWorkbook workbook = null;
            ISheet sheet = null;
            IRow row = null;
            ICell cell = null;
            int startRow = 0;
            string fileExtension = string.Empty;
            try
            {
                using (fs = File.OpenRead(filePath))
                {
                    // 2007版本  
                    if (filePath.IndexOf(".xlsx") > 0)
                    {
                        workbook = new XSSFWorkbook(fs);
                        fileExtension = ".xlsx";
                    }                      
                    // 2003版本  
                    else if (filePath.IndexOf(".xls") > 0)
                    {
                        workbook = new HSSFWorkbook(fs);
                        fileExtension = ".xls";
                    }

                    if (workbook != null)
                    {
                        //sheet = workbook.GetSheetAt(0);//读取第一个sheet，当然也可以循环读取每个sheet  
                        sheet = workbook.GetSheet(sheetName);//通过Sheet名字读取指定的sheet
                        string sss = sheetName;
                        dataTable = new DataTable();
                        if (sheet != null)
                        {
                            int rowCount = sheet.LastRowNum;//总行数  
                            if (rowCount > 0)
                            {
                                IRow firstRow = sheet.GetRow(0);//第一行  
                                int cellCount = firstRow.LastCellNum;//列数  

                                //构建datatable的列  
                                if (isColumnName)
                                {
                                    startRow = 1;//如果第一行是列名，则从第二行开始读取  
                                    for (int i = firstRow.FirstCellNum; i < cellCount; ++i)
                                    {
                                        cell = firstRow.GetCell(i);
                                        if (cell != null)
                                        {
                                            if (cell.StringCellValue != null)
                                            {
                                                column = new DataColumn(cell.StringCellValue);
                                                dataTable.Columns.Add(column);
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    for (int i = firstRow.FirstCellNum; i < cellCount; ++i)
                                    {
                                        column = new DataColumn("column" + (i + 1));
                                        dataTable.Columns.Add(column);
                                    }
                                }

                                //填充行  
                                for (int i = startRow; i <= rowCount; ++i)
                                {
                                    row = sheet.GetRow(i);
                                    if (row == null) continue;

                                    dataRow = dataTable.NewRow();
                                    for (int j = row.FirstCellNum; j < cellCount; ++j)
                                    {
                                        cell = row.GetCell(j);
                                        if (cell == null)
                                        {
                                            dataRow[j] = "";
                                        }
                                        else
                                        {
                                            //CellType(Unknown = -1,Numeric = 0,String = 1,Formula = 2,Blank = 3,Boolean = 4,Error = 5,)  
                                            switch (cell.CellType)
                                            {
                                                case CellType.Blank:
                                                    dataRow[j] = "";
                                                    break;
                                                case CellType.Numeric:
                                                    short format = cell.CellStyle.DataFormat;

                                                    //对时间格式（2015.12.5、2015/12/5、2015-12-5等）的处理  
                                                    if (format == 14 || format == 31 || format == 57 || format == 58)
                                                        dataRow[j] = cell.DateCellValue;
                                                    else
                                                        dataRow[j] = cell.NumericCellValue;
                                                    break;
                                                    //如果读取到的单元格里包含的是excel公式，则直接获取公式计算后的数值
                                                case CellType.String:
                                                    dataRow[j] = cell.StringCellValue;
                                                    break;
                                                    //此处代码为自己查找添加的，针对cell中为excel公式时，如何进行处理
                                                case CellType.Formula:
                                                    if(fileExtension == ".xlsx")
                                                    {
                                                        XSSFFormulaEvaluator eva = new XSSFFormulaEvaluator(workbook);
                                                        if (eva.Evaluate(row.GetCell(j)).CellType == CellType.Numeric)
                                                        {
                                                            if (HSSFDateUtil.IsCellDateFormatted(row.GetCell(j)))//日期类型
                                                            {
                                                                dataRow[j] = row.GetCell(j).DateCellValue.ToString("yyyy-MM-dd");
                                                            }
                                                            else//其他数字类型
                                                            {
                                                                dataRow[j] = row.GetCell(j).NumericCellValue;
                                                            }
                                                        }
                                                        else
                                                        {
                                                            dataRow[j] = eva.Evaluate(row.GetCell(j)).StringValue;
                                                        }
                                                    }
                                                    else
                                                    {
                                                        HSSFFormulaEvaluator eva = new HSSFFormulaEvaluator(workbook);
                                                        if (eva.Evaluate(row.GetCell(j)).CellType == CellType.Numeric)
                                                        {
                                                            if (HSSFDateUtil.IsCellDateFormatted(row.GetCell(j)))//日期类型
                                                            {
                                                                dataRow[j] = row.GetCell(j).DateCellValue.ToString("yyyy-MM-dd");
                                                            }
                                                            else//其他数字类型
                                                            {
                                                                dataRow[j] = row.GetCell(j).NumericCellValue;
                                                            }
                                                        }
                                                        else
                                                        {
                                                            dataRow[j] = eva.Evaluate(row.GetCell(j)).StringValue;
                                                        }
                                                    }
                                                   
                                                    break;
                                            }
                                        }
                                    }
                                    dataTable.Rows.Add(dataRow);
                                }
                            }
                        }
                    }
                }
                return dataTable;
            }
            catch (Exception)
            {
                if (fs != null)
                {
                    fs.Close();
                }
                return null;
            }
        }
   

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

        public static void EmptyNumericList(List<double> list)
        {
            if (list.Count > 0)
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
