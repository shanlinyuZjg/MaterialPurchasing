using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using Global.Helper;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;

namespace Global.Purchase
{
    public partial class POInvoice : Office2007Form
    {
        public string PONumber = string.Empty;
        public string VendorNumber = string.Empty;
        public string MfgNumber = string.Empty;
        public string VendorName = string.Empty;
        public string MfgName = string.Empty;
        public List<string> VendorList = new List<string>();
        public bool isInvoiced = false;
        //字符集，此处用于供应商名字模糊查询供应商时使用
        Encoding GB2312 = Encoding.GetEncoding("gb2312");
        Encoding ISO88591 = Encoding.GetEncoding("iso-8859-1");

        public static string Entry = "000";
        public bool isInvoiceView = false;


        public POInvoice()
        {
            InitializeComponent();
            this.EnableGlass = false;
            MessageBoxEx.EnableGlass = false;
        }

        public POInvoice(string entry)
        {
            InitializeComponent();
            this.EnableGlass = false;
            MessageBoxEx.EnableGlass = false;
            Entry = entry;
        }

        private void tbVendorFuzzyName_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void POInvoice_Load(object sender, EventArgs e)
        {

        }

        //获取订单
        private DataTable GetVendorPO()
        {
            string str = ISO88591.GetString(GB2312.GetBytes(tbVendorName.Text.Trim()));
            List<string> polist = new List<string>();
            string sqlSelectPO = @"Select PONumber From PurchaseOrderInvoicedPO";
            polist = SQLHelper.GetList(GlobalSpace.FSDBConnstr, sqlSelectPO, "PONumber");
            string sqlSelect = @"SELECT
	                                                        PONumber AS 采购单号
                                                        FROM
	                                                        FSDB.dbo.PORV
                                                        WHERE
	                                                        HistoryPOReceiptKey IN (
			                                                        SELECT
			                                                        MAX (T1.HistoryPOReceiptKey)
		                                                        FROM
			                                                        FSDB.dbo.PORV T1,
			                                                        FSDBMR.dbo._NoLock_FS_Vendor T2
		                                                        WHERE
			                                                        T1.VendorID = T2.VendorID
                                                        AND T2.VendorName LIKE '%{0}%'
		                                                        AND T1.POReceiptDate >= '{1}'
                                                                AND T1.PONumber NOT IN ('{2}')
		                                                        GROUP BY
			                                                        T1.PONumber
	                                                        )
                                                        ORDER BY
	                                                        POReceiptDate ASC";     
 
            if(rbtnNoDate.Checked)
            {
                sqlSelect = string.Format(sqlSelect, str, "2020-01-01",string.Join("','",polist.ToArray()));
            }
            else
            {
                sqlSelect = string.Format(sqlSelect, str, "2020-12-01", string.Join("','", polist.ToArray()));
            }

            return SQLHelper.GetDataTable(GlobalSpace.FSDBMRConnstr, sqlSelect);

        }

        private void tbVendorName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar == (char)13)
            {
                if (!string.IsNullOrWhiteSpace(tbVendorName.Text))
                {
                    string str = ISO88591.GetString(GB2312.GetBytes(tbVendorName.Text.Trim()));
                    string vendorNumber = string.Empty;
                    string sqlSelect = @"SELECT
	                                                        DISTINCT VendorNumber         
		                                                        FROM
			                                                        PurchaseOrderRecordByCMF 
		                                                        WHERE  VendorName LIKE '%" + tbVendorName.Text.Trim()+ "%'";
                    DataTable dt = SQLHelper.GetDataTable(GlobalSpace.FSDBConnstr, sqlSelect);
                    if(dt.Rows.Count  == 0 )
                    {
                        MessageBoxEx.Show("未查到该供应商信息！", "提示");
                        return;
                    }
                    else if(dt.Rows.Count > 1)
                    {
                        MessageBoxEx.Show("当前查到供应商信息超过1条，请输入更精确供应商信息！", "提示");
                        return;
                    }
                    else
                    {
                        vendorNumber = dt.Rows[0]["VendorNumber"].ToString();

                        GetVendorPODetail(vendorNumber);
                    }
                }
            }

        }

        private void GetVendorPODetail(string vendorNumber)
        {
            string sqlSelectExist = @"Select [Key] From PurchaseOrderInvoiceRecordByCMF where VendorNumber ='"+ vendorNumber + "'";
            List<string> keyList = SQLHelper.GetList(GlobalSpace.FSDBConnstr, sqlSelectExist, "Key");
            string sqlSelect = string.Empty;
            string   selectPO = string.Empty;
            if (keyList.Count > 0)
            {
                                                        sqlSelect = @"SELECT
                                                        '' AS 联系单号,
	                                                        T1.POReceiptDate AS 入库日期,
	                                                        T1.PONumber AS 采购单号,
	                                                        T1.POLineNumber AS 行号,
	                                                        T1.ItemNumber AS 代码,
	                                                        T2.ItemDescription AS 描述,
                                                          T1.POLineUM AS 单位,
	                                                        T1.ItemOrderedQuantity AS 订单量,
	                                                        (
		                                                        CASE T1.POReceiptActionType
		                                                        WHEN 'R' THEN
			                                                        T1.ItemReceiptQuantity
		                                                        ELSE
			                                                        (0 - T1.ReversedQuantity)
		                                                        END
	                                                        ) AS 入库量,T1.TotalReceiptQuantity AS 累计入库量,
	                                                        T1.ItemStandardLocalUnitPrice AS 单价,
                                                        (
		                                                        CASE T1.POReceiptActionType
		                                                        WHEN 'R' THEN
			                                                        T1.ItemReceiptQuantity
		                                                        ELSE
			                                                        (0 - T1.ReversedQuantity)
		                                                        END
	                                                        )* T1.ItemStandardLocalUnitPrice AS 合计,
	                                                        T1.LotNumber AS 厂家批号,
	                                                        T1.VendorLotNumber AS 公司批号,(
		                                                        SELECT
			                                                        Rtrim(UserName)
		                                                        FROM
			                                                        FSDBMR.dbo._NoLock_FS_UserAccess
		                                                        WHERE
			                                                        UserID = T1.Buyer
	                                                        ) AS 采购员,
	                                                        (	SELECT
			                                                        Rtrim(UserName)
		                                                        FROM
			                                                        FSDBMR.dbo._NoLock_FS_UserAccess 
		                                                        WHERE
			                                                        UserID = T2.ItemReference3) AS 库管员,T1.HistoryPOReceiptKey AS [Key]
                                                                                                                    FROM
	                                                                                                                    FSDB.dbo.PORV T1,
	                                                                                                                    FSDBMR.dbo._NoLock_FS_Item T2
                                                                                                                    WHERE
	                                                                                                                    T1.ItemNumber = T2.ItemNumber And  T1.POReceiptDate >= '{0}'  And T1.VendorID='"+vendorNumber+ "' And T1.HistoryPOReceiptKey not in ({1})  ORDER BY T1.TransactionDate,T1.TransactionTime ASC";
                selectPO = @"Select ForeignNumber,PONumber,LineNumber From PurchaseOrderRecordByCMF Where VendorNumber='" + vendorNumber + "' And IsPurePo = 0 And POItemPlacedDate >='{0}'";

                if (rbtnNoDate.Checked)
                {
                    sqlSelect = string.Format(sqlSelect, "2020-01-01", string.Join(",", keyList.ToArray()));
                    selectPO = string.Format(selectPO, "2020-01-01");
                }
                else
                {
                    sqlSelect = string.Format(sqlSelect, "2020-12-01", string.Join(",", keyList.ToArray()));
                    selectPO = string.Format(selectPO, "2020-12-01");
                }
            }  

            DataTable dt = SQLHelper.GetDataTableOleDb(GlobalSpace.oledbconnstrFSDB, sqlSelect);          
            DataTable dtPO = SQLHelper.GetDataTable(GlobalSpace.FSDBConnstr, selectPO);
       


            for(int i = dt.Rows.Count; i > 0;i--)
            {

                string number = dt.Rows[i - 1]["行号"].ToString();

                if (dt.Rows[i - 1]["采购单号"].ToString().Substring(0, 2) == "PP")
                {
                    if (number.Length == 1)
                    {
                        number = "00" + number;
                    }
                    else if (number.Length == 2)
                    {
                        number = "0" + number;
                    }

                    DataRow[] drs = dtPO.Select("PONumber = '" + dt.Rows[i - 1]["采购单号"].ToString() + "' And LineNumber='" + number + "' ");
                    if (drs.Length > 0)
                    {
                        dt.Rows[i - 1]["联系单号"] = drs[0]["ForeignNumber"].ToString();
                    }
                }

                //if (keyList.Contains(dt.Rows[i - 1]["Key"].ToString()))
                //{
                //    dt.Rows.RemoveAt(i - 1);
                //}
            }

            dgvPODetail.DataSource = dt;
       

        }

        private void btnPrintDetail_Click(object sender, EventArgs e)
        {

                if (string.IsNullOrWhiteSpace(tbInvoiceNumber.Text))
                {
                    MessageBoxEx.Show("发票号不能为空！", "提示");
                    return;
                }

            //    this.axGRPrintViewer1.Print(true);
            if(rbtnSinglePO.Checked)
            {
                DataTable dt = (DataTable)dgvPODetail.DataSource;
                DataTable dtNew = dt.Clone();
                foreach (DataGridViewRow dgvr in dgvPODetail.Rows)
                {
                    if (Convert.ToBoolean(dgvr.Cells["Check"].Value))
                    {
                        DataRow dr = dtNew.NewRow();
                        dr = (dgvr.DataBoundItem as DataRowView).Row;
                        dtNew.Rows.Add(dr.ItemArray);
                    }
                }
                if (dtNew.Columns.Contains("Id"))
                {
                    dtNew.Columns.Remove("Id");
                }
                string str = tbInvoiceNumber.Text.Trim();
                PrintInvoiceItemDetail print = new PrintInvoiceItemDetail(VendorList, dtNew,str, "\\POInvoiceAudit.grf");
                print.ShowDialog();
            }
            else
            {
                DataTable dtTotal = (DataTable)dgvTotal.DataSource;
                PrintInvoiceItemDetail print = new PrintInvoiceItemDetail(VendorList, dtTotal, tbInvoiceNumber.Text.Trim(), "\\POInvoiceAudit.grf");
                print.ShowDialog();
            }        
        }

        private void btnJoinInvoice_Click(object sender, EventArgs e)
        {
            List<string> lineNumberList = new List<string>();
            if (dgvTotal.DataSource == null)
            {
                string sqlColumnHeader = @"SELECT
                                        '' AS 联系单号,
	                                        T1.POReceiptDate AS 入库日期,
	                                        T1.PONumber AS 采购单号,
	                                        T1.POLineNumber AS 行号,
	                                        T1.ItemNumber AS 代码,
	                                        T2.ItemDescription AS 描述,T1.POLineUM AS 单位,
	                                        T1.ItemOrderedQuantity AS 订单量,
	                                        (
		                                        CASE T1.POReceiptActionType
		                                        WHEN 'R' THEN
			                                        T1.ItemReceiptQuantity
		                                        ELSE
			                                        (0 - T1.ReversedQuantity)
		                                        END
	                                        ) AS 入库量,T1.TotalReceiptQuantity AS 累计入库量,
	                                        T1.ItemStandardLocalUnitPrice AS 单价,
                                        (
		                                        CASE T1.POReceiptActionType
		                                        WHEN 'R' THEN
			                                        T1.ItemReceiptQuantity
		                                        ELSE
			                                        (0 - T1.ReversedQuantity)
		                                        END
	                                        )* T1.ItemStandardLocalUnitPrice AS 合计,
	                                        T1.LotNumber AS 厂家批号,
	                                        T1.VendorLotNumber AS 公司批号,(
		                                        SELECT
			                                        Rtrim(UserName)
		                                        FROM
			                                        FSDBMR.dbo._NoLock_FS_UserAccess
		                                        WHERE
			                                        UserID = T1.Buyer
	                                        ) AS 采购员,
	                                        (	SELECT
			                                        Rtrim(UserName)
		                                        FROM
			                                        FSDBMR.dbo._NoLock_FS_UserAccess 
		                                        WHERE
			                                        UserID = T2.ItemReference3) AS 库管员,T1.HistoryPOReceiptKey AS [Key]
                                        FROM
	                                        FSDB.dbo.PORV T1,
	                                        FSDBMR.dbo._NoLock_FS_Item T2
                                                                                WHERE 1=2  ";
                DataTable dt = SQLHelper.GetDataTable(GlobalSpace.FSDBConnstr, sqlColumnHeader);              
                foreach (DataGridViewRow dgvr in dgvPODetail.Rows)
                {
                    if (Convert.ToBoolean(dgvr.Cells["Check"].Value))
                    {
                        DataRow dr = dt.NewRow();
                        dr = (dgvr.DataBoundItem as DataRowView).Row;
                        dt.Rows.Add(dr.ItemArray);
                        if(isExistInvoiceRecord(dgvr.Cells["Key"].Value.ToString()))
                        {
                            lineNumberList.Add(dgvr.Cells["行号"].Value.ToString());
                        }
                    }
                }
                dgvTotal.DataSource = dt;
            }
            else
            {
                DataTable dt = (DataTable)dgvTotal.DataSource;
                List<int> keyList = dt.AsEnumerable().Select(r => r.Field<int>("Key")).ToList();
               
                foreach (DataGridViewRow dgvr in dgvPODetail.Rows)
                {
                    if(Convert.ToBoolean(dgvr.Cells["Check"].Value))
                    {
                        if(!keyList.Contains(Convert.ToInt32(dgvr.Cells["Key"].Value)))
                        {
                            DataRow dr = dt.NewRow();
                            dr = (dgvr.DataBoundItem as DataRowView).Row;
                            dt.Rows.Add(dr.ItemArray);
                            if (isExistInvoiceRecord(dgvr.Cells["Key"].Value.ToString()))
                            {
                                lineNumberList.Add(dgvr.Cells["行号"].Value.ToString());
                            }
                        }                       
                    }
                }
                dgvTotal.DataSource = dt;
            }
            dgvPODetail.DataSource = null;

            if(lineNumberList.Count > 0)
            {
                string msg = string.Join(" ", lineNumberList.ToArray());
                MessageBoxEx.Show("以下行的加入的物料在已增加的发票记录中存在，请进行核对！"+msg, "提示");
            }

            DataTable dt1 = (DataTable)dgvTotal.DataSource;
            string sqlSelectPO = @"Select VendorNumber,VendorName,ManufacturerNumber,ManufacturerName From PurchaseOrderRecordByCMF Where PONumber='" + dt1.Rows[0]["采购单号"].ToString() + "' and IsPurePO = 0";
            DataTable dtPOHeader = SQLHelper.GetDataTable(GlobalSpace.FSDBConnstr, sqlSelectPO);


            if (dtPOHeader.Rows.Count > 0)
            {
                if(dtPOHeader.Rows.Count > 1)
                {
                    VendorNumber = dtPOHeader.Rows[0]["VendorNumber"].ToString();
                    VendorName = dtPOHeader.Rows[0]["VendorName"].ToString();
                    MfgNumber = dtPOHeader.Rows[0]["ManufacturerNumber"].ToString();
                    MfgName = dtPOHeader.Rows[0]["ManufacturerName"].ToString();
                    if (VendorList.Count > 0)
                    {
                        VendorList.Clear();
                    }
                    VendorList.Add(VendorNumber);
                    VendorList.Add(VendorName);
                    VendorList.Add(MfgNumber);
                    VendorList.Add(MfgName);
                }
                else
                {
                    VendorNumber = dtPOHeader.Rows[0]["VendorNumber"].ToString();
                    VendorName = dtPOHeader.Rows[0]["VendorName"].ToString();
                    MfgNumber = dtPOHeader.Rows[0]["ManufacturerNumber"].ToString();
                    MfgName = dtPOHeader.Rows[0]["ManufacturerName"].ToString();
                    if (VendorList.Count > 0)
                    {
                        VendorList.Clear();
                    }
                    VendorList.Add(VendorNumber);
                    VendorList.Add(VendorName);
                    VendorList.Add(MfgNumber);
                    VendorList.Add(MfgName);
                }
             
            }
        }

        private bool isExistInvoiceRecord(string key)
        {
            string sqlCheck = @"Select Count(Id) From PurchaseOrderInvoiceRecordByCMF Where [Key]='"+key+"'";
            if(SQLHelper.Exist(GlobalSpace.FSDBConnstr,sqlCheck))
            {
                return true;
            }
            return false;
        }

        private void btnMakInvoiced_Click(object sender, EventArgs e)
        {

        }

        private void tbPONumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar == (char)13)
            {
                if(!string.IsNullOrEmpty(tbPONumber.Text))
                {
                    string sqlSelectExist = @"Select [Key] From PurchaseOrderInvoiceRecordByCMF where PONumber = '" + PONumber + "'";
                    List<string> keyList = SQLHelper.GetList(GlobalSpace.FSDBConnstr, sqlSelectExist, "Key");

                    PONumber = tbPONumber.Text.Trim();
                    string sqlSelectPO = @"SELECT
	                                                                                    T1.POReceiptDate AS 入库日期,
	                                                                                    T1.PONumber AS 采购单号,
	                                                                                    T1.POLineNumber AS 行号,
	                                                                                    T1.ItemNumber AS 代码,
	                                                                                    T2.ItemDescription AS 描述,T1.POLineUM AS 单位,
	                                                                                    T1.ItemOrderedQuantity AS 订单量,
	                                                                                    (
		                                                                                    CASE T1.POReceiptActionType
		                                                                                    WHEN 'R' THEN
			                                                                                    T1.ItemReceiptQuantity
		                                                                                    ELSE
			                                                                                    (0 - T1.ReversedQuantity)
		                                                                                    END
	                                                                                    ) AS 入库量,T1.TotalReceiptQuantity AS 累计入库量,
	                                                                                    T1.ItemStandardLocalUnitPrice AS 单价,
                                                                                    (
		                                                                                    CASE T1.POReceiptActionType
		                                                                                    WHEN 'R' THEN
			                                                                                    T1.ItemReceiptQuantity
		                                                                                    ELSE
			                                                                                    (0 - T1.ReversedQuantity)
		                                                                                    END
	                                                                                    )* T1.ItemStandardLocalUnitPrice AS 合计,
	                                                                                    T1.LotNumber AS 厂家批号,
	                                                                                    T1.VendorLotNumber AS 公司批号,(
		                                                                                    SELECT
			                                                                                    Rtrim(UserName)
		                                                                                    FROM
			                                                                                    FSDBMR.dbo._NoLock_FS_UserAccess
		                                                                                    WHERE
			                                                                                    UserID = T1.Buyer
	                                                                                    ) AS 采购员,
	                                                                                    (	SELECT
			                                                                                    Rtrim(UserName)
		                                                                                    FROM
			                                                                                    FSDBMR.dbo._NoLock_FS_UserAccess 
		                                                                                    WHERE
			                                                                                    UserID = T2.ItemReference3) AS 库管员,T1.HistoryPOReceiptKey AS [Key]
                                                                                    FROM
	                                                                                    FSDB.dbo.PORV T1,
	                                                                                    FSDBMR.dbo._NoLock_FS_Item T2
                                                                                    WHERE
	                                                                                    T1.PONumber = '" + tbPONumber.Text.Trim()+ "' AND T1.ItemNumber = T2.ItemNumber And T1.HistoryPOReceiptKey NOT IN ('{0}') ORDER BY T1.TransactionDate,T1.PONumber,T1.POLineNumber ASC";
                    sqlSelectPO = string.Format(sqlSelectPO, string.Join("','", keyList.ToArray()));

                    DataTable dt = SQLHelper.GetDataTableOleDb(GlobalSpace.oledbconnstrFSDBMR, sqlSelectPO);

                    string selectPO = @"Select ForeignNumber,PONumber,LineNumber From PurchaseOrderRecordByCMF Where PONumber='" + PONumber + "' And IsPurePo = 0";
                    DataTable dtPO = SQLHelper.GetDataTable(GlobalSpace.FSDBConnstr, selectPO);

                    dt.Columns.Add("联系单号");
                    dt.Columns["联系单号"].SetOrdinal(0);

                    if (PONumber.Substring(0, 2) == "PP")
                    {
                        foreach (DataRow dr in dt.Rows)
                        {
                            string number = dr["行号"].ToString();
                            if (number.Length == 1)
                            {
                                number = "00" + number;
                            }
                            else if (number.Length == 2)
                            {
                                number = "0" + number;
                            }

                            DataRow[] drs = dtPO.Select("PONumber = '" + PONumber + "' And LineNumber='" + number + "' ");

                            dr["联系单号"] = drs[0]["ForeignNumber"].ToString();
                        }
                    }
                    dgvPODetail.DataSource = dt;

                    DataTable dtPOSingle = new DataTable();
                    dtPOSingle.Columns.Add("采购单号");
                    DataRow drSingle = dtPOSingle.NewRow();
                    drSingle["采购单号"] = tbPONumber.Text.Trim();
                    dtPOSingle.Rows.Add(drSingle);

                    string sqlSelect = @"Select VendorNumber,VendorName,ManufacturerNumber,ManufacturerName From PurchaseOrderRecordByCMF Where PONumber='" + PONumber + "' and IsPurePO = 0";
                    DataTable dtPOHeader = SQLHelper.GetDataTable(GlobalSpace.FSDBConnstr, sqlSelect);

                    if (dtPOHeader.Rows.Count > 0)
                    {
                        VendorNumber = dtPOHeader.Rows[0]["VendorNumber"].ToString();
                        VendorName = dtPOHeader.Rows[0]["VendorName"].ToString();
                        MfgNumber = dtPOHeader.Rows[0]["ManufacturerNumber"].ToString();
                        MfgName = dtPOHeader.Rows[0]["ManufacturerName"].ToString();
                        if (VendorList.Count > 0)
                        {
                            VendorList.Clear();
                        }
                        VendorList.Add(VendorNumber);
                        VendorList.Add(VendorName);
                        VendorList.Add(MfgNumber);
                        VendorList.Add(MfgName);
                    }

                }
            }
        }

        private void btnChooseFilePath_Click(object sender, EventArgs e)
        {
            string filename = tbExportFilePath.Text;
            SaveFileDialog saveDialog = new SaveFileDialog();
            saveDialog.DefaultExt = "xls";
            saveDialog.Filter = "Excel文件|*.xls";
            saveDialog.FileName = filename;
            saveDialog.ShowDialog();
            filename = saveDialog.FileName;
            if (filename.IndexOf(":") < 0) return; //被点了取消
            tbExportFilePath.Text = filename;
        }

        private void btnExportToExcel_Click(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)dgvTotal.DataSource;
            if(dt.Columns.Contains("厂家批号"))
            {
                dt.Columns.Remove("厂家批号");
            }
            dt.Columns.Remove("公司批号");
            dt.Columns.Remove("采购员");
            dt.Columns.Remove("库管员");
            dt.Columns.Remove("Key");
            dt.Columns.Remove("累计入库量");
            string timeStr = DateTime.Now.ToString("yy-MM-dd HH:mm:ss");
            //string filePath = @"D:\\"+VendorName+DateTime.Now.ToString("yy-MM-dd ")+".xlsx";
            string filePath = @"D:\\"+VendorName+timeStr.Replace(":","-")+".xlsx";
            string sheetname = "Sheet1";

            XSSFWorkbook workbook = new XSSFWorkbook();
            ISheet sheet = workbook.CreateSheet(sheetname);
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
                    //       cell.CellStyle = cellstyle2;

                    cell.SetCellValue(dt.Rows[i][j].ToString());

                    
                    if (j == 7 || j == 8 || j == 9 || j == 10 || j == 3)
                    {
                        cell = row.CreateCell(j);
                        cell.SetCellValue(Convert.ToDouble(dt.Rows[i][j]));
                //        cell.CellStyle = cellStyle2;
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
                if (MessageBoxEx.Show("当前同名文件已存在，是否覆盖该文件？", "提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
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

        private void btnNoInvoice_Click(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)dgvTotal.DataSource;
            List<string> sqlList = new List<string>();
            string sequenceNumber = string.Empty;

            string sqlExist = @"Select distinct SequenceNumber from  PurchaseOrderInvoiceRecordByCMF where VendorNumber ='"+VendorNumber+"' order by SequenceNumber desc";
            DataTable dtSequenceNumber = SQLHelper.GetDataTable(GlobalSpace.FSDBConnstr, sqlExist);

            if(dtSequenceNumber.Rows.Count > 0)
            {
                sequenceNumber = (Convert.ToInt32(dtSequenceNumber.Rows[0]["SequenceNumber"]) + 1).ToString();
            }
            else
            {
                sequenceNumber = "100001";
            }

            foreach(DataRow dr in dt.Rows)
            {
                string sqlInsert = @"INSERT INTO [FSDB].[dbo].[PurchaseOrderInvoiceRecordByCMF] (
	                                                                            ReceiveDate,
	                                                                            [PONumber],
	                                                                            [VendorNumber],
	                                                                            [VendorName],[MfgNumber],
	                                                                            [mfgName],
	                                                                            [LineNumber],
	                                                                            [ItemNumber],
	                                                                            [ItemDescription],                                                                         
	                                                                            [LotNumber],
	                                                                            [InternalLotNumber],
	                                                                            [StockKeeper],
	                                                                            [ForeignNumber],
	                                                                            [BuyerID],
	                                                                            [SequenceNumber],
	                                                                            [OrderQuantity],
	                                                                            [ReceiveQuantity],[Key],[UM],[UnitPrice]
                                                                            )
                                                                            VALUES
	                                                                            (
		                                                                            '" +Convert.ToDateTime(dr["入库日期"]).ToString("yyyy-MM-dd")+ "', '" + dr["采购单号"].ToString() + "', '" + VendorNumber + "', '"+VendorName+"','"+MfgNumber+"','"+MfgName+"','" + dr["行号"].ToString() + "', '" + dr["代码"].ToString() + "', '" + dr["描述"].ToString() + "', '" + dr["厂家批号"].ToString() + "', '" + dr["公司批号"].ToString() + "', '" + dr["库管员"].ToString() + "', '" + dr["联系单号"].ToString() + "', '" + dr["采购员"].ToString() + "', '" + sequenceNumber + "', '" +Convert.ToDouble(dr["订单量"]) + "', '" +Convert.ToDouble(dr["入库量"])+ "','"+ dr["Key"].ToString() + "','"+ dr["单位"].ToString() + "',"+ Convert.ToDouble(dr["单价"]) + ")";
                sqlList.Add(sqlInsert);
            }

            if(SQLHelper.BatchExecuteNonQuery(GlobalSpace.FSDBConnstr,sqlList))
            {
                MessageBoxEx.Show("确认成功！", "提示");
            }
            else
            {
                MessageBoxEx.Show("确认失败！", "提示");
            }

        }

        private void btnInvoced_Click(object sender, EventArgs e)
        {
            /*
            if (string.IsNullOrEmpty(tbInvoiced.Text)|| string.IsNullOrEmpty(tbInvoiceAmount.Text)|| string.IsNullOrEmpty(tbInvoiceTaxedAmount.Text))
            {
                MessageBoxEx.Show("发票号、面额或税额均不能为空！", "提示");
                return;
            }
            */

            if (string.IsNullOrEmpty(tbInvoiced.Text))
            {
                MessageBoxEx.Show("发票号不能为空！", "提示");
                return;
            }

            DataTable dt = (DataTable)dgvTotal.DataSource;
            List<string> sqlList = new List<string>();
            string sequenceNumber = string.Empty;

            string sqlExist = @"Select distinct SequenceNumber from  PurchaseOrderInvoiceRecordByCMF where VendorNumber ='" + VendorNumber + "' order by SequenceNumber desc";
            DataTable dtSequenceNumber = SQLHelper.GetDataTable(GlobalSpace.FSDBConnstr, sqlExist);

            if (dtSequenceNumber.Rows.Count > 0)
            {
                sequenceNumber = (Convert.ToInt32(dtSequenceNumber.Rows[0]["SequenceNumber"]) + 1).ToString();
            }
            else
            {
                sequenceNumber = "100001";
            }

            foreach (DataRow dr in dt.Rows)
            {
                string sqlInsert = @"INSERT INTO [FSDB].[dbo].[PurchaseOrderInvoiceRecordByCMF] (
	                                                                            ReceiveDate,
	                                                                            [PONumber],
	                                                                            [VendorNumber],
	                                                                            [VendorName],[MfgNumber],
	                                                                            [mfgName],
	                                                                            [LineNumber],
	                                                                            [ItemNumber],
	                                                                            [ItemDescription],                                                                         
	                                                                            [LotNumber],
	                                                                            [InternalLotNumber],
	                                                                            [StockKeeper],
	                                                                            [ForeignNumber],
	                                                                            [BuyerID],
	                                                                            [SequenceNumber],
	                                                                            [OrderQuantity],
	                                                                            [ReceiveQuantity],InvoiceNumber,[UM],[Key],[UnitPrice],InvoiceAmount,InvoiceTaxedAmount
                                                                            )
                                                                            VALUES
	                                                                            (
		                                                                            '" + dr["入库日期"].ToString() + "', '" + dr["采购单号"].ToString() + "', '" + VendorNumber + "', '" + VendorName + "','" + MfgNumber + "','" + MfgName + "','" + dr["行号"].ToString() + "', '" + dr["代码"].ToString() + "', '" + dr["描述"].ToString() + "', '" + dr["厂家批号"].ToString() + "', '" + dr["公司批号"].ToString() + "', '" + dr["库管员"].ToString() + "', '" + dr["联系单号"].ToString() + "', '" + dr["采购员"].ToString() + "', '" + sequenceNumber + "', '" + Convert.ToDouble(dr["订单量"]) + "', '" + Convert.ToDouble(dr["入库量"]) + "','"+tbInvoiced.Text.Trim()+ "', '" + dr["单位"].ToString() + "','" + dr["Key"].ToString() + "','"+Convert.ToDouble(dr["单价"]) +"',"+Convert.ToDouble(tbInvoiceAmount.Text)+","+Convert.ToDouble(tbInvoiceTaxedAmount.Text)+")";
                sqlList.Add(sqlInsert);
            }

            if (SQLHelper.BatchExecuteNonQuery(GlobalSpace.FSDBConnstr, sqlList))
            {
                MessageBoxEx.Show("确认成功！", "提示");
             //   dgvTotal.DataSource = null;
            }
            else
            {
                MessageBoxEx.Show("确认失败！", "提示");
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            dgvTotal.DataSource = null;
        }

        private void btnMakeAllChecked_Click(object sender, EventArgs e)
        {
            foreach(DataGridViewRow dgvr in dgvPODetail.Rows)
            {
                dgvr.Cells["Check"].Value = true;
            }
        }

        private void tbInvoicedView_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar == (char)13)
            {
                if(!string.IsNullOrEmpty(tbInvoicedView.Text))
                {
                    tbInvoiceNumber.Text=tbInvoicedView.Text.Trim();
                    string sqlSelect = @"SELECT
                                ForeignNumber AS 联系单号,
	                            ReceiveDate AS 入库日期,
	                            PONumber AS 采购单号,
	                            LineNumber AS 行号,
	                            ItemNumber AS 代码,
	                            ItemDescription AS 描述,
	                            UM AS 单位,
	                            OrderQuantity AS 订单量,
	                            ReceiveQuantity AS 入库量,
	                            UnitPrice AS 单价,
	                            LotNumber AS 厂家批号,
	                            InternalLotNumber AS 公司批号,
	                            BuyerID AS 采购员,
	                            StockKeeper AS 库管员,Id,(ReceiveQuantity*UnitPrice) AS Amount
                            FROM
	                            PurchaseOrderInvoiceRecordByCMF
                            WHERE InvoiceNumber = '" + tbInvoicedView.Text.Trim() + "'";
                    DataTable dt = SQLHelper.GetDataTable(GlobalSpace.FSDBConnstr, sqlSelect);
                    dgvPODetail.DataSource = dt;
                    dgvPODetail.Columns["Id"].Visible = false;
                    dgvPODetail.Columns["Amount"].Visible = false;
                    isInvoiceView = true;

                    double totalAmount = 0;
                    foreach(DataRow dr in dt.Rows)
                    {
                        totalAmount += Convert.ToDouble(dr["Amount"]);
                    }

                    lblTotalAmount.Text = totalAmount.ToString();

                    string sqlSelectPOHeader = @"Select VendorNumber,VendorName,MfgNumber,MfgName From PurchaseOrderInvoiceRecordByCMF Where InvoiceNumber='" + tbInvoicedView.Text.Trim() + "' ";
                    DataTable dtPOHeader = SQLHelper.GetDataTable(GlobalSpace.FSDBConnstr, sqlSelectPOHeader);

                    if (dtPOHeader.Rows.Count > 0)
                    {
                        VendorNumber = dtPOHeader.Rows[0]["VendorNumber"].ToString();
                        VendorName = dtPOHeader.Rows[0]["VendorName"].ToString();
                        MfgNumber = dtPOHeader.Rows[0]["MfgNumber"].ToString();
                        MfgName = dtPOHeader.Rows[0]["MfgName"].ToString();
                        if (VendorList.Count > 0)
                        {
                            VendorList.Clear();
                        }
                        VendorList.Add(VendorNumber);
                        VendorList.Add(VendorName);
                        VendorList.Add(MfgNumber);
                        VendorList.Add(MfgName);
                    }
                }
            }
            
        }

        private void btnManageInvoice_Click(object sender, EventArgs e)
        {
            ManageInvoice mi = new ManageInvoice();
            mi.ShowDialog();
        }

        private void tbStartLineNumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar == (char)13)
            {
                if(!string.IsNullOrWhiteSpace(tbStartLineNumber.Text))
                {
                    tbEndLineNumber.Focus();
                }
            }
        }

        private void tbEndLineNumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                if (!string.IsNullOrWhiteSpace(tbEndLineNumber.Text))
                {

                    for(int i =Convert.ToInt32(tbStartLineNumber.Text) ;i < dgvPODetail.Rows.Count;i++)
                    {
                        dgvPODetail.Rows[i].Cells["Check"].Value = true;
                    }
                }
            }
        }

        private void dgvPODetail_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            var grid = sender as DataGridView;
            var rowIdx = (e.RowIndex + 1).ToString();
            var centerFormat = new StringFormat()
            {
                // right alignment might actually make more sense for numbers
                Alignment = StringAlignment.Center,
                LineAlignment = StringAlignment.Center
            };
            var headerBounds = new Rectangle(e.RowBounds.Left, e.RowBounds.Top, grid.RowHeadersWidth, e.RowBounds.Height);
            e.Graphics.DrawString(rowIdx, this.Font, SystemBrushes.ControlText, headerBounds, centerFormat);
        }

        private void btnRecover_Click(object sender, EventArgs e)
        {
            List<string> sqlList = new List<string>();
            foreach(DataGridViewRow dgvr in dgvPODetail .Rows)
            {
                if(Convert.ToBoolean(dgvr.Cells["Check"].Value))
                {
                    string sqlUpdate = @"Update PurchaseOrderInvoiceRecordByCMF Set Status = 0,InvoiceNumber='' Where Id='"+dgvr.Cells["Id"].Value.ToString()+"'";
                    sqlList.Add(sqlUpdate);
                }
            }

            if(SQLHelper.BatchExecuteNonQuery(GlobalSpace.FSDBConnstr,sqlList))
            {
                MessageBoxEx.Show("更新成功！", "提示");
                string sqlSelect = @"SELECT
                                ForeignNumber AS 联系单号,
	                            ReceiveDate AS 入库日期,
	                            PONumber AS 采购单号,
	                            LineNumber AS 行号,
	                            ItemNumber AS 代码,
	                            ItemDescription AS 描述,
	                            UM AS 单位,
	                            OrderQuantity AS 订单量,
	                            ReceiveQuantity AS 入库量,
	                            UnitPrice AS 单价,
	                            LotNumber AS 厂家批号,
	                            InternalLotNumber AS 公司批号,
	                            BuyerID AS 采购员,
	                            StockKeeper AS 库管员,Id
                            FROM
	                            PurchaseOrderInvoiceRecordByCMF
                            WHERE InvoiceNumber = '" + tbInvoicedView.Text.Trim() + "'";
                DataTable dt = SQLHelper.GetDataTable(GlobalSpace.FSDBConnstr, sqlSelect);
                dgvPODetail.DataSource = dt;
                dgvPODetail.Columns["Id"].Visible = false;
            }
            else
            {
                MessageBoxEx.Show("更新失败！", "提示");
            }
        }

     

        private void tbPONumber_TextChanged(object sender, EventArgs e)
        {
            tbPONumber.Text = tbPONumber.Text.ToUpper();
            tbPONumber.SelectionStart = tbPONumber.Text.Length;
        }

        private void btnInvoiceSearch_Click(object sender, EventArgs e)
        {
            Purchase.POInvoiceSearch pois = new POInvoiceSearch();
            pois.Show();
        }
    }
}
