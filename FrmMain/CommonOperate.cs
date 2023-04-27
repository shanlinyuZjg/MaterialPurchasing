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
using DevComponents.DotNetBar;

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

        //获取物料在四班中的标准价格
        public static double GetItemStandardPrice(string itemNumber)
        {
            string sqlSelect = @"SELECT
	                                            T2.TotalRolledCost
                                            FROM
	                                            _NoLock_FS_Item T1,
	                                            _NoLock_FS_ItemCost T2
                                            WHERE
	                                            T1.ItemKey = T2.ItemKey
                                            AND T2.CostType = 0
                                            AND T1.ItemNumber = '" + itemNumber + "'";
            return Convert.ToDouble( SQLHelper.ExecuteScalar(GlobalSpace.FSDBMRConnstr, sqlSelect));
        }

        //获取订单号的Guid
        public static string GetPOGuid(string ponumber)
        {
            string sqlSelect = @"Select Guid From PurchaseOrderRecordByCMF Where PONumber = '" + ponumber+"' And IsPurePO = 1";
            return SQLHelper.ExecuteScalar(GlobalSpace.FSDBConnstr, sqlSelect).ToString();
        }
        public static bool WriteFSErrorLog(string type, ITransaction transaction,FSTIError fstiError, string id, string Content = "")
        {
            StringBuilder error = new StringBuilder();

            for (int i = 0; i < fstiError.NumberOfFieldsInError; i++)
            {
                int field = fstiError.GetFieldNumber(i);
                error.Append(String.Format("Field[{0}]: {1} ", i, field));
                ITransactionField myField = transaction.get_Field(field);              
                error.Append(String.Format("Field name: {0}", myField.Name));
            }

            string sqlInsert = @"Insert Into FSErrorLogByCMF (Type,ErrorContent,Operator) Values ('"+type+"','"+Content.Replace("'","")+" "+error.ToString().Replace("'", "") + " "+fstiError.Description.Replace("'", "") + "','"+id+"')";

 //           MessageBox.Show(sqlInsert);
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
            string sqlSelect = @"Select  ItemDescription,ItemUM,IsInspectionRequired,PreferredStockroom,PreferredBin From _NoLock_FS_Item Where ItemNumber='" + itemNumber + "'";
            dtTemp = SQLHelper.GetDataTableOleDb(GlobalSpace.oledbconnstrFSDBMR, sqlSelect);
            if (dtTemp.Rows.Count > 0)
            {
                list.Add(dtTemp.Rows[0]["IsInspectionRequired"].ToString());
                list.Add(dtTemp.Rows[0]["PreferredStockroom"].ToString());
                list.Add(dtTemp.Rows[0]["PreferredBin"].ToString());
                list.Add(dtTemp.Rows[0]["ItemDescription"].ToString());
                list.Add(dtTemp.Rows[0]["ItemUM"].ToString());
            }
            return list;
        }
        //批量获取四班中物料信息
        public static DataTable GetBatchItemInfo(List<string> itemList)
        {
            string sqlSelect = @"Select ItemNumber,ItemDescription,ItemUM AS UM ,IsInspectionRequired ,PreferredStockroom AS Stock,PreferredBin AS Bin From _NoLock_FS_Item Where ItemNumber In ('{0}')";
            sqlSelect = string.Format(sqlSelect, string.Join("','", itemList.ToArray()));
            return SQLHelper.GetDataTableOleDb(GlobalSpace.oledbconnstrFSDBMR, sqlSelect);
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
        //获取上级领导姓名和邮箱
        public static List<string> GetSuperiorNameAndEmail(string struserid)
        {
            string supervisorID = string.Empty;
            List<string> list = new List<string>();
            string strSelect = @"SELECT
	                                            Name,
	                                            Email
                                            FROM
	                                            PurchaseDepartmentRBACByCMF
                                            WHERE
	                                            UserID IN (
		                                            SELECT
			                                            SupervisorID
		                                            FROM
			                                            PurchaseDepartmentRBACByCMF
		                                            WHERE
			                                            UserID = '"+struserid+"')";
            DataTable dtTemp = SQLHelper.GetDataTable(GlobalSpace.FSDBConnstr, strSelect);
            if (dtTemp.Rows.Count > 0)
            {
                list.Add(dtTemp.Rows[0]["Name"].ToString());
                list.Add(dtTemp.Rows[0]["Email"].ToString());
            }
            return list;
        }

        //获取上级领导代码
        public static string  GetSuperiorID(string struserid)
        {
            string supervisorID = string.Empty;
            string strSelect = @"SELECT
	                                            UserID
                                            FROM
	                                            PurchaseDepartmentRBACByCMF
                                            WHERE
	                                            UserID IN (
		                                            SELECT
			                                            SupervisorID
		                                            FROM
			                                            PurchaseDepartmentRBACByCMF
		                                            WHERE
			                                            UserID = '" + struserid + "')";
            DataTable dtTemp = SQLHelper.GetDataTable(GlobalSpace.FSDBConnstr, strSelect);
            if (dtTemp.Rows.Count > 0)
            {
                supervisorID = dtTemp.Rows[0]["UserID"].ToString();
            }
            else
            {
                supervisorID = "NotExist";
            }
            return supervisorID;
        }
        //获取下级员工列表
        public static DataTable GetSubordinate(string struserid)
        {
            string sqlSelect = @"Select UserID,Name,Email From PurchaseDepartmentRBACByCMF Where SupervisorID='"+struserid+"'";
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
        public static void BindFormToTabControl(DevComponents.DotNetBar.TabControl tabControl, Form frm, string strName, string strText)
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
        /// 判断字符串是否为数字和英文字符
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

        //判断字符串是否为纯数字
        public static bool IsNumber(string str)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(str, @"(?i)^[0-9]+$"))
            {
                return true;
            }
            return false;
        }
        //判断字符串是否为纯字母
        public static bool IsString(string str)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(str, @"(?i)^[a-z]+$"))
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
        //导出DataTable至Excel文件
        public static void ExportDataTableToExcel(string filePath,string sheetName,DataTable dt)
        {
            XSSFWorkbook workbook = new XSSFWorkbook();
            ISheet sheet = workbook.CreateSheet(sheetName);
            IRow rowHead = sheet.CreateRow(0);
            ICell cell;

            //填写表头
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                cell = rowHead.CreateCell(i, CellType.String);
                cell.SetCellValue(dt.Columns[i].Caption);
                //    cell.CellStyle = cellstyle;
            }
            //填写内容
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                IRow row = sheet.CreateRow(i + 1);

                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    cell = row.CreateCell(j, CellType.String);
                    if(dt.Rows[i][j] == DBNull.Value)
                    {
                        cell.SetCellValue("");
                    }
                    else
                    {
                        cell.SetCellValue(dt.Rows[i][j].ToString());
                    }
                }


            }
            for (int j = 0; j < dt.Columns.Count; j++)
            {
                sheet.AutoSizeColumn(j);
            }

            if (!File.Exists(filePath))
            {
                try
                {
                    using (FileStream fs = new FileStream(filePath, FileMode.Create, FileAccess.Write))
                    {
                        workbook.Write(fs);
                        fs.Close();
                    }
                    Custom.MsgEx("导出数据成功！");
                }
                catch (Exception)
                {
                    throw;
                }
            }
            else
            {
                if (MessageBox.Show("当前同名文件已存在，是否覆盖该文件？", "提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    try
                    {
                        using (FileStream fs = new FileStream(filePath, FileMode.Create, FileAccess.Write))
                        {
                            workbook.Write(fs);
                            fs.Close();
                        }


                        Custom.MsgEx("导出数据成功！" + filePath);
                    }
                    catch (Exception)
                    {
                        throw;
                    }
                }
                else
                {
                    return;
                }

            }

            GC.Collect();
        }
        //对DataTable中某一列去重后返回该列的数组
        public static string[]  GetDistinctNamesFromDataTable(DataTable dataTable,string columnName)
        {
            DataView dv = dataTable.DefaultView;
            dataTable = dv.ToTable(true, columnName);
            string[] names = new string[dataTable.Rows.Count];
            for (int i = 0; i < names.Length;i++)
            {
                names[i] =dataTable.Rows[i][0].ToString();
            }
            return names;
        }
        //对DataTable中某一列去重后返回所需要列
        public DataTable GetDataTableByDistinctColumn(DataTable dataTable, string columnName,string[] keepedColumns)
        {
            DataTable dt = dataTable.Clone();
            string[] names = GetDistinctNamesFromDataTable(dataTable, columnName);

            foreach(DataRow dr in dataTable.Rows)
            {
                DataRow[] drs = dataTable.Select("VendorNumber = '"+columnName+"'");
                DataRow dr2 = dt.NewRow();
                dr2 = dr;
                dt.Rows.Add(dr2);

            }

            return dt;
        }
        //对TextBox控件的回车按键进行判断处理，如果为文本框的内容为空或者按下的不是回车键，则提示并返回
        public static bool TextBoxCheck(TextBox tb, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                if (string.IsNullOrEmpty(tb.Text))
                {
                    Custom.MsgEx("内容不能为空！");                  
                }
                else
                {
                    return true;
                }
            }
            return false;
        }
        //通过Guid更新订单中的物料状态
        public static bool UpdatePOItemStatusByGuid(string guid,int status)
        {
            string sqlUpdate = @"Update PurchaseOrderRecordByCMF Set POStatus = "+status+" Where Guid = '"+guid+"'";
            if(SQLHelper.ExecuteNonQuery(GlobalSpace.FSDBConnstr,sqlUpdate))
            {
                return true;
            }
            return false;
        }
        //通过Guid批量更新订单中的物料状态
        public static bool BatchUpdatePOItemStatusByGuid(List<string> guidList, int status)
        {
            List<string> sqlUpdateList = new List<string>();
            for(int i = 0; i < guidList.Count; i++ )
            {
                string sqlUpdate = @"Update PurchaseOrderRecordByCMF Set POStatus = " + status + ",ActualDeliveryDate ='"+DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss")+ "'   Where Guid = '" + guidList[i] + "'";
                sqlUpdateList.Add(sqlUpdate);
            }
           
            if (SQLHelper.BatchExecuteNonQuery(GlobalSpace.FSDBConnstr,sqlUpdateList))
            {
                #region 回写计划状态
                try
                {
                    if (status == 4)
                    {
                        DataTable dt = SQLHelper.GetDataTable(GlobalSpace.FSDBConnstr, "select distinct GSID  from PurchaseOrderRecordByCMF Where Guid in ('" + string.Join("','", guidList) + "') and GSID !='0'");
                        if (dt.Rows.Count >0)
                        {
                            IEnumerable<String> lstr = dt.Rows.Cast<DataRow>().Select(r => r["GSID"].ToString());
                            if (lstr.Count() > 0)
                            {
                                SQLHelper.ExecuteNonQuery(GlobalSpace.RYData, "update  [dbo].[SolidBuyList] set Flag=2  where ID in (" + string.Join(",", lstr).Replace("|", ",") + ")");
                            }
                        }
                    }
                }
                catch(Exception ex)
                {
                    MessageBoxEx.Show("回写物料需求计划状态失败，请联系软件服务处！"+ex.Message);
                }
                #endregion
                return true;
            }
            return false;
        }
        public static bool BatchUpdatePOItemStatusByGuid(List<string> guidList, int status,string qualityStandard)
        {
            List<string> sqlUpdateList = new List<string>();
            for (int i = 0; i < guidList.Count; i++)
            {
                string sqlUpdate = @"Update PurchaseOrderRecordByCMF Set POStatus = " + status + ",ActualDeliveryDate ='" + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss") + "',QualityCheckStandard='"+ qualityStandard + "'   Where Guid = '" + guidList[i] + "'";
                sqlUpdateList.Add(sqlUpdate);
            }

            if (SQLHelper.BatchExecuteNonQuery(GlobalSpace.FSDBConnstr, sqlUpdateList))
            {
                return true;
            }
            return false;
        }
        //通过Guid批量更新订单中的物料状态
        public static bool UpdatePOItemStatusByGuidTimes(List<string> guidList, int status)
        {
            List<string> sqlUpdateList = new List<string>();
            for (int i = 0; i < guidList.Count; i++)
            {
                string sqlUpdate = @"Update PurchaseOrderRecordByCMF Set POStatus = " + status + ",ActualDeliveryDate ='" + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss") + "',IsDeliverTimes = 1    Where Guid = '" + guidList[i] + "'";
                sqlUpdateList.Add(sqlUpdate);
            }

            if (SQLHelper.BatchExecuteNonQuery(GlobalSpace.FSDBConnstr, sqlUpdateList))
            {
                return true;
            }
            return false;
        }

        //获取BOM中子项物料耗用量
        public static DataTable GetBOMCompomnentItemQuantity(string componentItemNumber)
        {
            string sqlSelect = @"SELECT
	                                        T2.ItemNumber,
	                                        T2.ItemDescription,
	                                        T1.RequiredQuantity
                                        FROM
	                                        _NoLock_FS_BillOfMaterial T1,
	                                        _NoLock_FS_Item T2
                                        WHERE
	                                        T1.ParentItemKey = T2.ItemKey
                                                                                    AND
	                                            T1.ComponentItemNumber = '" + componentItemNumber+"'";
            return SQLHelper.GetDataTableOleDb(GlobalSpace.oledbconnstrFSDBMR, sqlSelect);
        }
        //根据供应商代码和物料信息，生成订单
        public static void PlacePurchaseOrder(List<string> vendorlist, DataTable dtItem)
        {

        }

        //下达单个订单
        public static bool PlaceOrder(string ponumber,string vendornumber,string vendorname,string buyerid,string supervisorid)
        {
            string sqlInsert = @"Insert Into PurchaseOrdersByCMF (PONumber,VendorNumber,VendorName,Buyer,Supervisor,ParentGuid) Values ('"+ponumber+"','"+vendornumber+"','"+vendorname+"','"+buyerid+"','"+supervisorid+"','"+Guid.NewGuid().ToString("N")+"')";
            if(SQLHelper.ExecuteNonQuery(GlobalSpace.FSDBConnstr,sqlInsert))
            {
                return true;
            }
            return false;
        }
        //批量下达订单和添加物料到订单中
        public static bool PlaceOrderWithItemDetail(string poType,DataTable dtVendorL, DataTable dtItem, string buyerName, string buyerID, string supervisorID,int poStatus,double taxRate)
        {
            List<string> sqlList = new List<string>();
            int sequenceNumber = 0;
            int sequenceNumberFS = 0;
            string latestPONumber = string.Empty;
            string strSequenceNumber = string.Empty;
            string strPOSequenceNumber = string.Empty;
            string latestPONumberFS = string.Empty;
            string strSequenceNumberFS = string.Empty;
            string strPOSequenceNumberFS = string.Empty;
            string dateNow = DateTime.Now.ToString("MMddyy");
            DataTable dtVendor = dtVendorL.DefaultView.ToTable(true, "供应商代码","供应商名称");

            if (dtVendor.Rows.Count > 0)
            {
                string sqlSelectLatest = @"Select Distinct Id,PONumber From PurchaseOrderRecordByCMF Where POItemPlacedDate='" + DateTime.Now.ToString("yyyy-MM-dd") + "' And Left(PONumber,2) = '"+poType+"' And Buyer = '"+ buyerID + "' And IsPurePO = 1  Order By Id DESC";
                string sqlSelectFSPO = @" SELECT
	                            T1.PONumber
                            FROM
	                            _NoLock_FS_POHeader T1
                            WHERE                               
                                T1.Buyer ='" + buyerID + "' AND T1.PONumber LIKE '%" + dateNow + "%'  ORDER BY T1.PONumber DESC";
                DataTable dtLatest = SQLHelper.GetDataTable(GlobalSpace.FSDBConnstr, sqlSelectLatest);
                DataTable dtLatestFS = SQLHelper.GetDataTable(GlobalSpace.FSDBMRConnstr, sqlSelectFSPO);
                if (dtLatest.Rows.Count > 0)
                {
                    latestPONumber = dtLatest.Rows[0]["PONumber"].ToString();
                    strSequenceNumber = latestPONumber.Substring(10);
                    sequenceNumber = Convert.ToInt32(strSequenceNumber);
                }

                if (dtLatestFS.Rows.Count > 0)
                {
                    latestPONumberFS = dtLatestFS.Rows[0]["PONumber"].ToString();
                    strSequenceNumberFS = latestPONumberFS.Substring(10);
                    sequenceNumberFS = Convert.ToInt32(strSequenceNumberFS);
                }
                
                if(sequenceNumberFS > sequenceNumber)
                {
                    sequenceNumber = sequenceNumberFS;
                }


                for (int i =0;i < dtVendor.Rows.Count;i++)
                {
                    sequenceNumber = sequenceNumber +1;
                    string tempPONumber = string.Empty;

                    if (sequenceNumber.ToString().Length == 1)
                    {
                        strPOSequenceNumber = "00" + sequenceNumber.ToString();
                    }
                    else if (sequenceNumber.ToString().Length == 2)
                    {
                        strPOSequenceNumber = "0" + sequenceNumber.ToString();
                    }
                    else
                    {
                        strPOSequenceNumber = sequenceNumber.ToString();
                    }
              
                     tempPONumber = poType + "-" + DateTime.Now.ToString("MMddyy") + "-" + strPOSequenceNumber;
                    
                    
                    string parentGuid = Guid.NewGuid().ToString("N");
                    string sqlVendorInsert = @"Insert Into PurchaseOrderRecordByCMF (PONumber,VendorNumber,VendorName,ManufacturerNumber,ManufacturerName,Buyer,Superior,Guid,IsPurePO) Values ('" + tempPONumber + "','" + dtVendor.Rows[i]["供应商代码"].ToString() + "','" + dtVendor.Rows[i]["供应商名称"].ToString() + "','" + dtVendor.Rows[i]["供应商代码"].ToString() + "','" + dtVendor.Rows[i]["供应商名称"].ToString() + "','" + buyerID + "','" + supervisorID + "','" + parentGuid + "',1)";
                    sqlList.Add(sqlVendorInsert);
                    DataRow[] drs = dtItem.Select(" 供应商代码 ='"+ dtVendor.Rows[i]["供应商代码"].ToString()+ "'");

                    foreach (DataRow dr in drs)
                    {
                        string itemKeeper = GetItemStockKeeper(dr["物料代码"].ToString().ToUpper());
                        List<string> ItemInfoList = GetItemInfo(dr["物料代码"].ToString().ToUpper());
                        string sqlPOItemInsert = string.Empty;
                        //string demandDeliveryDate = "20" + dr["承诺到货"].ToString().Substring(4, 2) + "-" + dr["承诺到货"].ToString().Substring(0, 2) + "-" + dr["承诺到货"].ToString().Substring(2, 2);
                        sqlPOItemInsert = @"INSERT INTO PurchaseOrderRecordByCMF (
	                                            PONumber,
	                                            VendorNumber,
	                                            VendorName,
	                                            ManufacturerNumber,
	                                            ManufacturerName,
	                                            ItemNumber,
	                                            ItemDescription,
	                                            LineUM,
	                                            Buyer,
                                                BuyerName,
	                                            Superior,
	                                            DemandDeliveryDate,	                                        
	                                            POStatus,	                                         	                                         
	                                            UnitPrice,
                                                PricePreTax,
	                                            LineType,
	                                            LineStatus,
                                                NeededDate,
                                                PromisedDate,
                                                POItemQuantity,
                                                StockKeeper,
                                                Stock,Bin,InspectionPeriod,Guid,TaxRate,ParentGuid,POItemConfirmer,ItemReceiveType,LotNumberAssign
                                            )
                                            VALUES
	                                            (
	                                            '" + tempPONumber + "','" + dr["供应商代码"].ToString() + "', '" + dr["供应商名称"].ToString() + "', '" + dr["供应商代码"].ToString() + "', '" + dr["供应商名称"].ToString() + "', '" + dr["物料代码"].ToString().ToUpper() + "', '" + dr["物料描述"].ToString() + "','" + ItemInfoList[4] + "', '" + buyerID + "', '" + buyerName + "','" + supervisorID + "','" + dr["承诺到货"].ToString() + "',1," + Math.Round(Convert.ToDouble(dr["采购单价"]) /(1+taxRate), 9) + "," + Convert.ToDouble(dr["采购单价"]) + ",'P',4, '" + dr["承诺到货"].ToString() + "','" + dr["承诺到货"].ToString() + "'," + Convert.ToDouble(dr["采购数量"]) + ",'" + itemKeeper.Trim() + "','" + ItemInfoList[1] + "','" + ItemInfoList[2] + "','" + ItemInfoList[0] + "','" + Guid.NewGuid().ToString() + "','"+taxRate.ToString()+"','" + parentGuid + "','"+ dr["确认人"].ToString() + "','"+ PurchaseUser.ItemReceiveType+"','C')"; 
                        sqlList.Add(sqlPOItemInsert);
                    }
                }
            }
            bool sus = true;
            for(int m = 0; m < sqlList.Count;m++)
            {
                try
                {
                    if(!SQLHelper.ExecuteNonQuery(GlobalSpace.FSDBConnstr,sqlList[m]))
                    {
                        sus = false;
                    }
                }
                catch (Exception ex)
                {
                    sus = false;
                    throw ex;
                }
            }
            return sus;
        }
        public static bool PlaceOrderWithItemDetail(string poType, DataTable dtVendorL, DataTable dtItem, string buyerName, string buyerID, string supervisorID, int poStatus)
        {
            List<string> sqlList = new List<string>();
            int sequenceNumber = 0;
            int sequenceNumberFS = 0;
            string latestPONumber = string.Empty;
            string strSequenceNumber = string.Empty;
            string strPOSequenceNumber = string.Empty;
            string latestPONumberFS = string.Empty;
            string strSequenceNumberFS = string.Empty;
            string strPOSequenceNumberFS = string.Empty;
            string dateNow = DateTime.Now.ToString("MMddyy");
            DataTable dtVendor = dtVendorL.DefaultView.ToTable(true, "供应商码", "供应商名");

            if (dtVendor.Rows.Count > 0)
            {
                string sqlSelectLatest = @"Select Distinct Id,PONumber From PurchaseOrderRecordByCMF Where POItemPlacedDate='" + DateTime.Now.ToString("yyyy-MM-dd") + "' And Left(PONumber,2) = '" + poType + "' And Buyer = '" + buyerID + "' And IsPurePO = 1  Order By Id DESC";
                string sqlSelectFSPO = @" SELECT
	                            T1.PONumber
                            FROM
	                            _NoLock_FS_POHeader T1
                            WHERE                               
                                T1.Buyer ='" + buyerID + "' AND T1.PONumber LIKE '%" + dateNow + "%'  ORDER BY T1.PONumber DESC";
                DataTable dtLatest = SQLHelper.GetDataTable(GlobalSpace.FSDBConnstr, sqlSelectLatest);
                DataTable dtLatestFS = SQLHelper.GetDataTable(GlobalSpace.FSDBMRConnstr, sqlSelectFSPO);
                if (dtLatest.Rows.Count > 0)
                {
                    latestPONumber = dtLatest.Rows[0]["PONumber"].ToString();
                    strSequenceNumber = latestPONumber.Substring(10);
                    sequenceNumber = Convert.ToInt32(strSequenceNumber);
                }

                if (dtLatestFS.Rows.Count > 0)
                {
                    latestPONumberFS = dtLatestFS.Rows[0]["PONumber"].ToString();
                    strSequenceNumberFS = latestPONumberFS.Substring(10);
                    sequenceNumberFS = Convert.ToInt32(strSequenceNumberFS);
                }

                if (sequenceNumberFS > sequenceNumber)
                {
                    sequenceNumber = sequenceNumberFS;
                }


                for (int i = 0; i < dtVendor.Rows.Count; i++)
                {
                    sequenceNumber = sequenceNumber + 1;
                    string tempPONumber = string.Empty;

                    if (sequenceNumber.ToString().Length == 1)
                    {
                        strPOSequenceNumber = "00" + sequenceNumber.ToString();
                    }
                    else if (sequenceNumber.ToString().Length == 2)
                    {
                        strPOSequenceNumber = "0" + sequenceNumber.ToString();
                    }
                    else
                    {
                        strPOSequenceNumber = sequenceNumber.ToString();
                    }

                    tempPONumber = poType + "-" + DateTime.Now.ToString("MMddyy") + "-" + strPOSequenceNumber;


                    string parentGuid = Guid.NewGuid().ToString("N");
                    string sqlVendorInsert = @"Insert Into PurchaseOrderRecordByCMF (PONumber,VendorNumber,VendorName,ManufacturerNumber,ManufacturerName,Buyer,Superior,Guid,IsPurePO) Values ('" + tempPONumber + "','" + dtVendor.Rows[i]["供应商码"].ToString() + "','" + dtVendor.Rows[i]["供应商名"].ToString() + "','" + dtVendor.Rows[i]["供应商码"].ToString() + "','" + dtVendor.Rows[i]["供应商名"].ToString() + "','" + buyerID + "','" + supervisorID + "','" + parentGuid + "',1)";
                    sqlList.Add(sqlVendorInsert);
                    DataRow[] drs = dtItem.Select(" 供应商码 ='" + dtVendor.Rows[i]["供应商码"].ToString() + "'");

                    foreach (DataRow dr in drs)
                    {
                        string itemKeeper = GetItemStockKeeper(dr["物料代码"].ToString().ToUpper());
                        List<string> ItemInfoList = GetItemInfo(dr["物料代码"].ToString().ToUpper());
                        string sqlPOItemInsert = string.Empty;
                        //string demandDeliveryDate = "20" + dr["承诺到货"].ToString().Substring(4, 2) + "-" + dr["承诺到货"].ToString().Substring(0, 2) + "-" + dr["承诺到货"].ToString().Substring(2, 2);
                        sqlPOItemInsert = @"INSERT INTO PurchaseOrderRecordByCMF (
	                                            PONumber,
	                                            VendorNumber,
	                                            VendorName,
	                                            ManufacturerNumber,
	                                            ManufacturerName,
	                                            ItemNumber,
	                                            ItemDescription,
	                                            LineUM,
	                                            Buyer,
                                                BuyerName,
	                                            Superior,
	                                            DemandDeliveryDate,	                                        
	                                            POStatus,	                                         	                                         
	                                            UnitPrice,
                                                PricePreTax,
	                                            LineType,
	                                            LineStatus,
                                                NeededDate,
                                                PromisedDate,
                                                POItemQuantity,
                                                StockKeeper,
                                                Stock,Bin,InspectionPeriod,Guid,TaxRate,ParentGuid,POItemConfirmer,ItemReceiveType,LotNumberAssign,
GSID,QualityCheckStandard,RequireDept,Comment1,ForeignNumber
                                            )
                                            VALUES
	                                            (
	                                            '" + tempPONumber + "','" + dr["供应商码"].ToString() + "', '" + dr["供应商名"].ToString() + "', '" + dr["生产商码"].ToString() + "', '" + dr["生产商名"].ToString() + "', '" + dr["物料代码"].ToString().ToUpper() + "', '" + dr["物料描述"].ToString() + "','" + ItemInfoList[4] + "', '" + buyerID + "', '" + buyerName + "','" + supervisorID + "','" + Convert.ToDateTime(dr["需求日期"].ToString()).ToString("MMddyy") + "',"+poStatus+"," + dr["税后价格"].ToString() + "," + dr["税前价格"].ToString() + ",'P',4, '" + Convert.ToDateTime(dr["需求日期"].ToString()).ToString("MMddyy") + "','" + Convert.ToDateTime(dr["需求日期"].ToString()).ToString("MMddyy") + "'," + dr["需求数量"].ToString() + ",'" + itemKeeper.Trim() + "','" + ItemInfoList[1] + "','" + ItemInfoList[2] + "','" + ItemInfoList[0] + "','" + Guid.NewGuid().ToString() + "',"+ dr["税率"].ToString() + ",'" + parentGuid + "','" + dr["确认员"].ToString() + "','" + PurchaseUser.ItemReceiveType + "','C','"+(string.IsNullOrWhiteSpace(dr["提报序号"].ToString())?"0": dr["提报序号"].ToString()) + "','" + dr["检验标准"].ToString() + "','" + dr["事业部"].ToString() + dr["需求车间"].ToString() + "','" + dr["备注"].ToString() + "','" + dr["联系单号"].ToString() + "')";
                        sqlList.Add(sqlPOItemInsert);
                    }
                }
            }
            bool sus = true;
            for (int m = 0; m < sqlList.Count; m++)
            {
                try
                {
                    if (!SQLHelper.ExecuteNonQuery(GlobalSpace.FSDBConnstr, sqlList[m]))
                    {
                        sus = false;
                    }
                }
                catch (Exception ex)
                {
                    sus = false;
                    throw ex;
                }
            }
            return sus;
        }
        //批量下达订单和添加物料到订单中
        public static bool PlaceForeignOrderWithItemDetail(string poType, DataTable dtVendorL, DataTable dtItem, string buyerName, string buyerID, string supervisorID, int poStatus)
        {
            List<string> sqlList = new List<string>();
            int sequenceNumber = 0;
            string latestPONumber = string.Empty;
            string strSequenceNumber = string.Empty;
            string strPOSequenceNumber = string.Empty;

            DataTable dtVendor = dtVendorL.DefaultView.ToTable(true, "供应商代码", "供应商名称");

            if (dtVendor.Rows.Count > 0)
            {
                string sqlSelectLatest = @"Select Distinct Id,PONumber From PurchaseOrderRecordByCMF Where POItemPlacedDate='" + DateTime.Now.ToString("yyyy-MM-dd") + "' And Left(PONumber,2) = '" + poType + "'  Order By Id DESC";

                DataTable dtLatest = SQLHelper.GetDataTable(GlobalSpace.FSDBConnstr, sqlSelectLatest);
                if (dtLatest.Rows.Count >=100)
                {
                    latestPONumber = dtLatest.Rows[0]["PONumber"].ToString();
                    strSequenceNumber = latestPONumber.Substring(10);
                    sequenceNumber = Convert.ToInt32(strSequenceNumber);
                }
                else
                {
                    sequenceNumber = 100;
                }

                for (int i = 0; i < dtVendor.Rows.Count; i++)
                {
                    sequenceNumber = sequenceNumber + 1;
                    string tempPONumber = string.Empty;
                    
                    strPOSequenceNumber = sequenceNumber.ToString();
                    

                    tempPONumber = poType + "-" + DateTime.Now.ToString("MMddyy") + "-" + strPOSequenceNumber;
                    string parentGuid = Guid.NewGuid().ToString("N");
                    string sqlVendorInsert = @"Insert Into PurchaseOrderRecordByCMF (PONumber,VendorNumber,VendorName,ManufacturerNumber,ManufacturerName,Buyer,Superior,Guid,IsPurePO) Values ('" + tempPONumber + "','" + dtVendor.Rows[i]["供应商代码"].ToString() + "','" + dtVendor.Rows[i]["供应商名称"].ToString() + "','" + dtVendor.Rows[i]["供应商代码"].ToString() + "','" + dtVendor.Rows[i]["供应商名称"].ToString() + "','" + buyerID + "','" + supervisorID + "','" + parentGuid + "',1)";
                    sqlList.Add(sqlVendorInsert);
                    DataRow[] drs = dtItem.Select(" 供应商代码 ='" + dtVendor.Rows[i]["供应商代码"].ToString() + "'");

                    foreach (DataRow dr in drs)
                    {
                        string itemKeeper = GetItemStockKeeper(dr["物料代码"].ToString());
                        List<string> ItemInfoList = GetItemInfo(dr["物料代码"].ToString());
                        string sqlPOItemInsert = @"INSERT INTO PurchaseOrderRecordByCMF (
	                                            PONumber,
	                                            VendorNumber,
	                                            VendorName,
	                                            ManufacturerNumber,
	                                            ManufacturerName,
	                                            ItemNumber,
	                                            ItemDescription,
	                                            LineUM,
	                                            Buyer,
                                                BuyerName,
	                                            Superior,
	                                            DemandDeliveryDate,	                                        
	                                            POStatus,	                                         	                                         
	                                            UnitPrice,
	                                            LineType,
	                                            LineStatus,
                                                NeededDate,
                                                PromisedDate,
                                                POItemQuantity,
                                                PricePreTax,
                                                StockKeeper,
                                                Stock,Bin,InspectionPeriod,Guid,TaxRate,ParentGuid
                                            )
                                            VALUES
	                                            (
	                                            '" + tempPONumber + "','" + dr["供应商代码"].ToString() + "', '" + dr["供应商名称"].ToString() + "', '" + dr["供应商代码"].ToString() + "', '" + dr["供应商名称"].ToString() + "', '" + dr["物料代码"].ToString() + "', '" + dr["物料描述"].ToString() + "','" + ItemInfoList[4] + "', '" + buyerID + "', '" + buyerName + "','" + supervisorID + "','" + DateTime.Now.AddDays(15).ToString("MMddyy") + "',1," + Math.Round(Convert.ToDouble(dr["采购单价"]) / 1.13, 8) + ",'P',4, '" + DateTime.Now.AddDays(15).ToString("MMddyy") + "','" + DateTime.Now.AddDays(15).ToString("MMddyy") + "'," + Convert.ToDouble(dr["采购数量"]) + "," + Convert.ToDouble(dr["采购单价"]) + ",'" + itemKeeper.Trim() + "','" + ItemInfoList[1] + "','" + ItemInfoList[2] + "','" + ItemInfoList[0] + "','" + Guid.NewGuid().ToString() + "',0,'" + parentGuid + "')";

                        sqlList.Add(sqlPOItemInsert);
                    }
                }
            }
            /*
            if (SQLHelper.BatchExecuteNonQuery(GlobalSpace.FSDBConnstr, sqlList))
            {
                return true;
            }*/
            bool sus = true;
            for (int m = 0; m < sqlList.Count; m++)
            {
                try
                {
                    if (!SQLHelper.ExecuteNonQuery(GlobalSpace.FSDBConnstr, sqlList[m]))
                    {
                        sus = false;
                    }
                }
                catch (Exception ex)
                {
                    sus = false;
                    throw;
                }
            }
            return sus;
        }
        //批量下达订单和添加物料到订单中
        public static bool PlaceAssistantItemOrderWithItemDetail(string poType, DataTable dtVendorL, DataTable dtItem, string buyerName, string buyerID, string supervisorID, int poStatus,out List<string> guidList,double TaxRate)
        {
            guidList = new List<string>();
            List<string> sqlList = new List<string>();
            int sequenceNumber = 0;
            int sequenceNumberFS = 0;
            string latestPONumber = string.Empty;
            string strSequenceNumber = string.Empty;
            string strPOSequenceNumber = string.Empty;
            string latestPONumberFS = string.Empty;
            string strSequenceNumberFS = string.Empty;
            string strPOSequenceNumberFS = string.Empty;
            string dateNow = DateTime.Now.ToString("MMddyy");
            DataTable dtVendor = dtVendorL.DefaultView.ToTable(true, "供应商代码", "供应商名称");

            if (dtVendor.Rows.Count > 0)
            {
                string sqlSelectLatest = @"Select Distinct Id,PONumber From PurchaseOrderRecordByCMF Where POItemPlacedDate='" + DateTime.Now.ToString("yyyy-MM-dd") + "' And Left(PONumber,2) = '" + poType + "'  And IsPurePO = 1  Order By Id DESC";
                string sqlSelectFSPO = @" SELECT
	                            T1.PONumber
                            FROM
	                            _NoLock_FS_POHeader T1
                            WHERE                               
                                 T1.PONumber LIKE '%" + poType+"-"+ dateNow + "%'  ORDER BY T1.PONumber DESC";
                DataTable dtLatest = SQLHelper.GetDataTable(GlobalSpace.FSDBConnstr, sqlSelectLatest);
                DataTable dtLatestFS = SQLHelper.GetDataTable(GlobalSpace.FSDBMRConnstr, sqlSelectFSPO);
                if (dtLatest.Rows.Count > 0)
                {
                    latestPONumber = dtLatest.Rows[0]["PONumber"].ToString();
                    strSequenceNumber = latestPONumber.Substring(10);
                    sequenceNumber = Convert.ToInt32(strSequenceNumber);
                }

                if (dtLatestFS.Rows.Count > 0)
                {
                    latestPONumberFS = dtLatestFS.Rows[0]["PONumber"].ToString();
                    strSequenceNumberFS = latestPONumberFS.Substring(10);
                    sequenceNumberFS = Convert.ToInt32(strSequenceNumberFS);
                }

                if (sequenceNumberFS > sequenceNumber)
                {
                    sequenceNumber = sequenceNumberFS;
                }


                for (int i = 0; i < dtVendor.Rows.Count; i++)
                {
                    sequenceNumber = sequenceNumber + 1;
                    string tempPONumber = string.Empty;

                    if (sequenceNumber.ToString().Length == 1)
                    {
                        strPOSequenceNumber = "00" + sequenceNumber.ToString();
                    }
                    else if (sequenceNumber.ToString().Length == 2)
                    {
                        strPOSequenceNumber = "0" + sequenceNumber.ToString();
                    }
                    else
                    {
                        strPOSequenceNumber = sequenceNumber.ToString();
                    }

                    tempPONumber = poType + "-" + DateTime.Now.ToString("MMddyy") + "-" + strPOSequenceNumber;


                    string parentGuid = Guid.NewGuid().ToString("N");
                    string sqlVendorInsert = @"Insert Into PurchaseOrderRecordByCMF (PONumber,VendorNumber,VendorName,ManufacturerNumber,ManufacturerName,Buyer,Superior,Guid,IsPurePO) Values ('" + tempPONumber + "','" + dtVendor.Rows[i]["供应商代码"].ToString() + "','" + dtVendor.Rows[i]["供应商名称"].ToString() + "','" + dtVendor.Rows[i]["供应商代码"].ToString() + "','" + dtVendor.Rows[i]["供应商名称"].ToString() + "','" + buyerID + "','" + supervisorID + "','" + parentGuid + "',1)";
                    sqlList.Add(sqlVendorInsert);
                    DataRow[] drs = dtItem.Select(" 供应商代码 ='" + dtVendor.Rows[i]["供应商代码"].ToString() + "'");

                    for (int k = 0; k < drs.Length;k++)
                    {
                        string itemKeeper = GetItemStockKeeper(drs[k]["物料代码"].ToString().ToUpper());
                        List<string> ItemInfoList = GetItemInfo(drs[k]["物料代码"].ToString().ToUpper());
                        guidList.Add(drs[k]["Guid"].ToString());
                        if(ItemInfoList.Count == 0)
                        {
                            continue;
                        }
                        string sqlPOItemInsert = string.Empty;
                        //string demandDeliveryDate = "20" + dr["承诺到货"].ToString().Substring(4, 2) + "-" + dr["承诺到货"].ToString().Substring(0, 2) + "-" + dr["承诺到货"].ToString().Substring(2, 2);
                        sqlPOItemInsert = @"INSERT INTO PurchaseOrderRecordByCMF (
	                                            PONumber,
	                                            VendorNumber,
	                                            VendorName,
	                                            ManufacturerNumber,
	                                            ManufacturerName,
	                                            ItemNumber,
	                                            ItemDescription,
	                                            LineUM,
	                                            Buyer,
                                                BuyerName,
	                                            Superior,
	                                            DemandDeliveryDate,	                                        
	                                            POStatus,	                                         	                                         
	                                            UnitPrice,
                                                PricePreTax,
	                                            LineType,
	                                            LineStatus,
                                                NeededDate,
                                                PromisedDate,
                                                POItemQuantity,
                                                StockKeeper,
                                                Stock,Bin,InspectionPeriod,Guid,TaxRate,ParentGuid,POItemConfirmer,ItemUsedPoint,Comment1,ItemReceiveType
                                            )
                                            VALUES
	                                            (
	                                            '" + tempPONumber + "','" + drs[k]["供应商代码"].ToString() + "', '" + drs[k]["供应商名称"].ToString() + "', '" + drs[k]["供应商代码"].ToString() + "', '" + drs[k]["供应商名称"].ToString() + "', '" + drs[k]["物料代码"].ToString() + "', '" + drs[k]["物料描述"].ToString() + "','" + ItemInfoList[4] + "', '" + buyerID + "', '" + buyerName + "','" + supervisorID + "','" + DateTime.Now.AddMonths(1).ToString("MMddyy") + "',2," + Math.Round(Convert.ToDouble(drs[k]["采购单价"]) / (1+TaxRate), 9) + "," + Convert.ToDouble(drs[k]["采购单价"]) + ",'P',4, '" + DateTime.Now.AddMonths(1).ToString("MMddyy") + "','" + DateTime.Now.AddMonths(1).ToString("MMddyy") + "'," + Convert.ToDouble(drs[k]["采购数量"]) + ",'" + itemKeeper.Trim() + "','" + ItemInfoList[1] + "','" + ItemInfoList[2] + "','" + ItemInfoList[0] + "','" + Guid.NewGuid().ToString() + "','"+TaxRate.ToString()+"','" + parentGuid + "','P07','"+ drs[k]["需求部门"].ToString() + "','"+ drs[k]["备注"].ToString() + "','"+ PurchaseUser.ItemReceiveType+"')";
                        sqlList.Add(sqlPOItemInsert);
                    }
                }
            }
            
            if (SQLHelper.BatchExecuteNonQuery(GlobalSpace.FSDBConnstr, sqlList))
            {
                return true;
            }
            
            return false;
        }
        //获取用户信息
        public static DataTable GetUserInfo(string struserid)
        {
            string strSelect = @"Select UserID,Name,SupervisorID,Status,Email,Name,Password,PurchaseType,POType,PriceCompare,POItemOthersConfirm,PONumberSequenceNumberRange,POTypeWithRange,POTogether  from PurchaseDepartmentRBACByCMF where UserID='" + struserid + "'";
            return SQLHelper.GetDataTable(GlobalSpace.FSDBConnstr, strSelect);
        }
        //将DotNetBar中MessageBox的Button的标题修改为中文，默认为英文的yes，ok，cancel等
        public static void LocalizationKeys_LocalizeString(object sender, LocalizeEventArgs e)
        {
            if (e.Key == DevComponents.DotNetBar.LocalizationKeys.MessageBoxCancelButton)
            {
                e.LocalizedValue = "取消";
                e.Handled = true;
            }
            if (e.Key == DevComponents.DotNetBar.LocalizationKeys.MessageBoxNoButton)
            {
                e.LocalizedValue = "取消";
                e.Handled = true;
            }
            if (e.Key == DevComponents.DotNetBar.LocalizationKeys.MessageBoxOkButton)
            {
                e.LocalizedValue = "确定";
                e.Handled = true;
            }
            if (e.Key == DevComponents.DotNetBar.LocalizationKeys.MessageBoxYesButton)
            {
                e.LocalizedValue = "确定";
                e.Handled = true;
            }
            if (e.Key == DevComponents.DotNetBar.LocalizationKeys.MonthCalendarClearButtonText)
            {
                e.LocalizedValue = "清除";
                e.Handled = true;
            }
            if (e.Key == DevComponents.DotNetBar.LocalizationKeys.MonthCalendarTodayButtonText)
            {
                e.LocalizedValue = "今天";
                e.Handled = true;
            }
            if (e.Key == DevComponents.DotNetBar.LocalizationKeys.TimeSelectorHourLabel)
            {
                e.LocalizedValue = "时";
                e.Handled = true;
            }
            if (e.Key == DevComponents.DotNetBar.LocalizationKeys.TimeSelectorMinuteLabel)
            {
                e.LocalizedValue = "分";
                e.Handled = true;
            }
            if (e.Key == DevComponents.DotNetBar.LocalizationKeys.TimeSelectorClearButton)
            {
                e.LocalizedValue = "清除";
                e.Handled = true;
            }
            if (e.Key == DevComponents.DotNetBar.LocalizationKeys.TimeSelectorOkButton)
            {
                e.LocalizedValue = "确定";
                e.Handled = true;
            }
        }

        //价格区间比对，10%，5%
        public static string  CompareItemPriceToStandardPrice(double price,double standardPrice)
        {
            string dreturn = string.Empty;
            //2021.03.10 沈传荣打电话，反馈丁处确认临时调整为5倍；与丁处电话沟通后，同时五金材料也临时调整为5倍。
            if ((price - standardPrice) / standardPrice >= 5 || (price - standardPrice) / standardPrice <= -5)
            {
                dreturn = "5";
            }
            else if ((price - standardPrice) / standardPrice > 0.15 || (price - standardPrice) / standardPrice < -0.15)
            {
                dreturn = "0.15";
            }
            else if ((price - standardPrice) / standardPrice > 0.1 || (price - standardPrice) / standardPrice < -0.1)
            {
                dreturn = "0.1";
            }
            else if ((price - standardPrice) / standardPrice > 0.05 || (price - standardPrice) / standardPrice < -0.05)
            {
                dreturn = "0.05";
            }
            else
            {
                dreturn = "0";
            }
            return dreturn;
        }
    }

    public class POItem
    {
        private string poNumber;
        private string itemNumber;
        private string itemDescription;
        private string vendorNumber;
        private string vendorDescription;
        private string manufacturerNumber;
        private string manufacturerDescription;
        private double pricePreTax;
        private double pricePostTax;
        private double taxRate;
        private string deliveryDate;
        private string foreignNumber;

        public string PoNumber
        {
            get
            {
                return poNumber;
            }

            set
            {
                poNumber = value;
            }
        }

        public string ItemNumber
        {
            get
            {
                return itemNumber;
            }

            set
            {
                itemNumber = value;
            }
        }

        public string ItemDescription
        {
            get
            {
                return itemDescription;
            }

            set
            {
                itemDescription = value;
            }
        }

        public string VendorNumber
        {
            get
            {
                return vendorNumber;
            }

            set
            {
                vendorNumber = value;
            }
        }

        public string VendorDescription
        {
            get
            {
                return vendorDescription;
            }

            set
            {
                vendorDescription = value;
            }
        }

        public string ManufacturerNumber
        {
            get
            {
                return manufacturerNumber;
            }

            set
            {
                manufacturerNumber = value;
            }
        }

        public string ManufacturerDescription
        {
            get
            {
                return manufacturerDescription;
            }

            set
            {
                manufacturerDescription = value;
            }
        }

        public double PricePreTax
        {
            get
            {
                return pricePreTax;
            }

            set
            {
                pricePreTax = value;
            }
        }

        public double PricePostTax
        {
            get
            {
                return pricePostTax;
            }

            set
            {
                pricePostTax = value;
            }
        }

        public double TaxRate
        {
            get
            {
                return taxRate;
            }

            set
            {
                taxRate = value;
            }
        }

        public string DeliveryDate
        {
            get
            {
                return deliveryDate;
            }

            set
            {
                deliveryDate = value;
            }
        }

        public string ForeignNumber
        {
            get
            {
                return foreignNumber;
            }

            set
            {
                foreignNumber = value;
            }
        }
        public static void FormShow(Form fm, string text, System.Windows.Forms.TabControl tabControl1)
        {
            fm.Text = text;
            Type type = fm.GetType();
            string FullName = type.FullName;
            bool bl = false;
            foreach (TabPage TP in tabControl1.TabPages)
            {
                if (TP.Name == FullName)
                {
                    bl = true;
                    break;
                }
            }
            if (bl)
            {
                fm.Dispose();
                tabControl1.SelectedTab = tabControl1.TabPages[FullName];//打开tabPage
            }
            else
            {
                //设置窗体没有边框 加入到选项卡中
                fm.FormBorderStyle = FormBorderStyle.None;

                fm.TopLevel = false;
                TabPage tabPage = new TabPage();
                tabPage.Name = FullName;
                tabPage.Text = text + "  ";
                tabControl1.TabPages.Add(tabPage);
                fm.Parent = tabControl1.TabPages[FullName];

                //fm.BackColor = Color.FromArgb(185, 215, 255);
                fm.ControlBox = false;

                fm.Dock = DockStyle.Fill;

                fm.Show();
                tabControl1.SelectedTab = tabControl1.TabPages[FullName];//打开tabPage
            }
        }
    }
}
