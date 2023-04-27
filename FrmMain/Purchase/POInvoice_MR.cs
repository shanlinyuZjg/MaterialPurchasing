using Global.Helper;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Global.Purchase
{
    public partial class POInvoice_MR : Form
    {
        string UserID = string.Empty;
        string UserName = string.Empty;
        
        //public POInvoice_MR()
        //{
        //    InitializeComponent();
        //}
        public POInvoice_MR(string userID,string userName)
        {
            InitializeComponent();
            UserID = userID;
            UserName = userName;
        }
        private void TbVendorNameSelect_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != (char)13)
            {
                return;
            }
            if (string.IsNullOrWhiteSpace(TbVendorNameSelect.Text))
            {
                MessageBox.Show("请输入供应商名");
                return;
            }
            Encoding EncodingLD = Encoding.GetEncoding("ISO-8859-1");
            Encoding EncodingCH = Encoding.GetEncoding("GB2312");
            string strSql = @"SELECT
	                                    VendorID,
	                                    VendorName,
                                        VendorKey
                                    FROM
	                                    _NoLock_FS_Vendor	                                    
                                    WHERE
	                                    VendorName like '%" + EncodingLD.GetString(EncodingCH.GetBytes(TbVendorNameSelect.Text.Trim())) + "%'";
            DataTable dtTemp = SQLHelper.GetDataTableOleDb(GlobalSpace.oledbconnstrFSDBMR, strSql);
            if (dtTemp.Rows.Count == 1)
            {
                TbVendorNameRO.Text = dtTemp.Rows[0]["VendorID"].ToString() + " " + dtTemp.Rows[0]["VendorName"].ToString();
                GetVendorPODetail(dtTemp.Rows[0]["VendorKey"].ToString().Trim());
            }
            else
            {
                MessageBox.Show("未查询到，或者有多个！");
                return;
            }
            
        }

        private void TbVendorNumberSelect_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != (char)13)
            {
                return;
            }
            if (string.IsNullOrWhiteSpace(TbVendorNumberSelect.Text))
            {
                MessageBox.Show("请输入供应商码");
                return;
            }
            string strSql = @"SELECT
	                                    VendorID,
	                                    VendorName,
                                        VendorKey
                                    FROM
	                                    _NoLock_FS_Vendor	                                    
                                    WHERE
	                                    VendorID = '" + TbVendorNumberSelect.Text.Trim() + "'";
            DataTable dtTemp = SQLHelper.GetDataTableOleDb(GlobalSpace.oledbconnstrFSDBMR, strSql);
            if (dtTemp.Rows.Count == 1)
            {
                TbVendorNameRO.Text = dtTemp.Rows[0]["VendorID"].ToString() + " " + dtTemp.Rows[0]["VendorName"].ToString();
                GetVendorPODetail(dtTemp.Rows[0]["VendorKey"].ToString().Trim());
            }
            else
            {
                return;
            }
        }

        private void GetVendorPODetail(string VendorKEY)
        {
            string APReceiptLineKeys = "SELECT APReceiptLineKey FROM FSDB.[dbo].[PurchaseOrderInvoiceRecordMRByCMF] where status != 3";
            DataTable dtItems = SQLHelper.GetDataTable(GlobalSpace.FSDBConnstr, APReceiptLineKeys);
            List<string> Lists = (from r in dtItems.AsEnumerable() select r.Field<string>("APReceiptLineKey")).ToList<string>();
            string sqlstr = $"SELECT '' AS 联系单号,t1.POReceiptDate 入库日期, t1.PONumber as 采购单号, t1.POReceiptSequenceNumber 序号,  t1.POLineNumber 行号,   t1.InvoiceMatchedQuantity 已匹配数量,  t1.LineItemNumber 物料编码,t2.ItemDescription as 物料描述, t1.LineItemUM 单位, t1.POReceiptQuantity as 入库量,t1.POReceiptLocalUnitCost 单价,t1.POReceiptLocalExtendedCost 合计, t1.APReceiptLineKey as [KEY] FROM  _NoLock_FS_APReceiptLine as t1 inner join _NoLock_FS_Item as t2 on t1.ItemKey=t2.ItemKey where t1.VendorKey={VendorKEY}  and t1.InvoiceMatchedQuantity !=t1.POReceiptQuantity and t1.APReceiptLineKey not in ('{string.Join("','",Lists)}') order by t1.POReceiptDate";
            DataTable dt = SQLHelper.GetDataTableOleDb(GlobalSpace.oledbconnstrFSDBMR, sqlstr);
            Dgv1.DataSource = dt;
            for (int i = 0; i < this.Dgv1.Columns.Count; i++)
            {
                this.Dgv1.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
            }
            TbAmount.Text = string.Empty;

          
            
        }

        private void BtnInvoiceConfrim_Click(object sender, EventArgs e)
        {

            if (Dgv2.Rows.Count == 0) return;
            List<string> sqlList = new List<string>();
            string VendorID = BtnGet.Tag.ToString().Split(' ')[0];
            string  VendorName = BtnGet.Tag.ToString().Split(' ')[1];
            //string AllAmount= BtnGet.Tag.ToString().Split(' ')[2];
            string Sequence = DateTime.Now.ToString("yyMMddHHmmss");
            DataTable dtInvoiceNumberS = SQLHelper.GetDataTable(GlobalSpace.FSDBConnstr, $@"select * from PurchaseOrderInvoiceRecordMRByCMF where VendorNumber='{VendorID}' and Sequence='{Sequence}'");
            if (dtInvoiceNumberS.Rows.Count > 0)
            {
                MessageBox.Show("序号已存在，请稍后再试");
                return;
            }
            if (Dgv2.Rows.Count==0)
            {
                MessageBox.Show("无信息");
                return;
            }
            for (int i=0;i<Dgv2.Rows.Count;i++)
            {
                string sqlInsert = $@"INSERT INTO [FSDB].[dbo].[PurchaseOrderInvoiceRecordMRByCMF] (
	VendorNumber, 
	VendorName, 
	PONumber, 
	LineNumber, 
	SequenceNumber, 
	ItemNumber, 
	ItemDescription, 
	UM, 
	ReceiveQuantity, 
	InvoiceMatchedQuantity, 
	ReceiveDate, 
	ForeignNumber, 
	UnitPrice, 
	Amount, 
	APReceiptLineKey, 
	Sequence, 
    Operator,OrderQuantity,LotNumber,InnerLotNumber,Buyer,Stockkeeper,ManufacturerID,ManufacturerName
                                                                            )
                                                                            VALUES
	                                                                            ('{VendorID}','{VendorName}','{Dgv2["采购单号",i].Value}','{Dgv2["行号", i].Value}','{Dgv2["序号", i].Value}','{Dgv2["物料编码", i].Value}','{Dgv2["物料描述", i].Value}','{Dgv2["单位", i].Value}','{Dgv2["入库量", i].Value}','{Dgv2["已匹配数量", i].Value}','{Dgv2["入库日期", i].Value}','{Dgv2["联系单号", i].Value}','{Dgv2["单价", i].Value}','{Dgv2["合计", i].Value}','{Dgv2["KEY", i].Value}','{Sequence}','{UserID}','{Dgv2["订单量", i].Value}','{Dgv2["厂家批号", i].Value}','{Dgv2["公司批号", i].Value}','{Dgv2["采购员", i].Value}','{Dgv2["库管员", i].Value}','{Dgv2["生产商码", i].Value}','{Dgv2["生产商名", i].Value}')";
                sqlList.Add(sqlInsert);
            }

            if (SQLHelper.BatchExecuteNonQuery(GlobalSpace.FSDBConnstr, sqlList))
            {
                MessageBox.Show("无票确认成功！", "提示");
                Dgv2.DataSource = null;
            }
            else
            {
                MessageBox.Show("无票确认失败！", "提示");
            }
        }
        private void BtnYOUInvoiceConfrim_Click(object sender, EventArgs e)
        {

            if (Dgv2.Rows.Count == 0) return;
            List<string> sqlList = new List<string>();
            string VendorID = BtnGet.Tag.ToString().Split(' ')[0];
            string VendorName = BtnGet.Tag.ToString().Split(' ')[1];
            string AllAmount = BtnGet.Tag.ToString().Split(' ')[2];
            if (string.IsNullOrWhiteSpace(TbInvoiceNumber.Text))
            {
                MessageBox.Show("发票号未填写");
                return;
            }
            DataTable dtInvoiceNumberS = SQLHelper.GetDataTable(GlobalSpace.FSDBConnstr, $@"select * from PurchaseOrderInvoiceRecordMRByCMF where VendorNumber='{VendorID}' and InvoiceNumberS='{TbInvoiceNumber.Text.Trim()}'");
            if (dtInvoiceNumberS.Rows.Count > 0)
            {
                MessageBox.Show("此供应商、发票号已存在");
                return;
            }
            if (Dgv2.Rows.Count == 0)
            {
                MessageBox.Show("无信息");
                return;
            }
            for (int i = 0; i < Dgv2.Rows.Count; i++)
            {
                string sqlInsert = $@"INSERT INTO [FSDB].[dbo].[PurchaseOrderInvoiceRecordMRByCMF] (
	VendorNumber, 
	VendorName, 
	PONumber, 
	LineNumber, 
	SequenceNumber, 
	ItemNumber, 
	ItemDescription, 
	UM, 
	ReceiveQuantity, 
	InvoiceMatchedQuantity, 
	ReceiveDate, 
	ForeignNumber, 
	UnitPrice, 
	Amount, 
	APReceiptLineKey, 
	InvoiceNumberS, 
    AllAmount,
    InvoiceTaxedAmount,
    InvoiceAmount,
    Operator
                                                                            )
                                                                            VALUES
	                                                                            ('{VendorID}','{VendorName}','{Dgv2["采购单号", i].Value}','{Dgv2["行号", i].Value}','{Dgv2["序号", i].Value}','{Dgv2["物料编码", i].Value}','{Dgv2["物料描述", i].Value}','{Dgv2["单位", i].Value}','{Dgv2["入库量", i].Value}','{Dgv2["已匹配数量", i].Value}','{Dgv2["入库日期", i].Value}','{Dgv2["联系单号", i].Value}','{Dgv2["单价", i].Value}','{Dgv2["合计", i].Value}','{Dgv2["KEY", i].Value}','{TbInvoiceNumber.Text.Trim()}','{AllAmount}','{TbTax.Text.Trim()}','{TbInvoiceAmount.Text.Trim()}','{UserID}')";
                sqlList.Add(sqlInsert);
            }

            if (SQLHelper.BatchExecuteNonQuery(GlobalSpace.FSDBConnstr, sqlList))
            {
                MessageBox.Show("有票确认成功！", "提示");
                Dgv2.DataSource = null;
            }
            else
            {
                MessageBox.Show("有票确认失败！", "提示");
            }
        }
        private void Dgv1_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            Rectangle rectangle = new Rectangle(e.RowBounds.Location.X, e.RowBounds.Location.Y, ((DataGridView)sender).RowHeadersWidth - 4, e.RowBounds.Height);
            TextRenderer.DrawText(e.Graphics, (e.RowIndex + 1).ToString(), ((DataGridView)sender).RowHeadersDefaultCellStyle.Font, rectangle, ((DataGridView)sender).RowHeadersDefaultCellStyle.ForeColor, TextFormatFlags.VerticalCenter | TextFormatFlags.Right);
        }

        private void BtnSelect_Click(object sender, EventArgs e)
        {
            
            for (int i = 0; i < Dgv1.Rows.Count; i++)
            {
                Dgv1.Rows[i].Cells["Check"].Value = true;
            }
            GetAmount();
        }


        private void GetAmount()
        {
            decimal Amount = 0;
            for (int i = 0; i < Dgv1.Rows.Count; i++)
            {
                if (Convert.ToBoolean(Dgv1["Check", i].Value))
                {
                    Amount += Convert.ToDecimal(Dgv1["合计", i].Value);
                }
            }
            TbAmount.Text = Amount.ToString();
        }

        private void BtnGet_Click(object sender, EventArgs e)
        {
            if (Dgv1.Rows.Count == 0)
            {
                MessageBox.Show("无数据");
                return;
            }
            DataTable dt = (DataTable)Dgv1.DataSource;
            DataTable dt1 = dt.Clone();
            for (int i = 0; i < Dgv1.Rows.Count; i++)
            {
                if (Convert.ToBoolean(Dgv1["Check", i].Value))
                {
                    dt1.Rows.Add((Dgv1.Rows[i].DataBoundItem as DataRowView).Row.ItemArray);
                }
            }
            dt1.Columns.Add("订单量");
            dt1.Columns.Add("公司批号");
            dt1.Columns.Add("厂家批号");
            dt1.Columns.Add("采购员");
            dt1.Columns.Add("库管员");
            dt1.Columns.Add("生产商码");
            dt1.Columns.Add("生产商名");
            foreach (DataRow dr in dt1.Rows)
            {
                string StrPO = $@"SELECT C.UserName,B.LineItemOrderedQuantity FROM [dbo].[_NoLock_FS_POHeader] A INNER JOIN [dbo].[_NoLock_FS_POLine] B on A.POHeaderKey =B.POHeaderKey INNER JOIN dbo._NoLock_FS_UserAccess C on C.UserID=A.Buyer where A.PONumber='{dr["采购单号"].ToString()}' and B.POLineNumber='{dr["行号"].ToString().TrimStart('0')}'";
                DataTable dtPO = SQLHelper.GetDataTableOleDb(GlobalSpace.oledbconnstrFSDBMR, StrPO);
                dr["订单量"] = dtPO.Rows[0]["LineItemOrderedQuantity"].ToString().Trim();
                dr["采购员"] = dtPO.Rows[0]["UserName"].ToString().Trim();


                string StrPORV = $@"SELECT APReceiptLineKey FROM _NoLock_FS_APReceiptLine where PONumber='{dr["采购单号"]}' and POLineNumber='{dr["行号"]}' and POReceiptQuantity = {dr["入库量"]} and POReceiptDate ='{dr["入库日期"]}' order by APReceiptLineKey";
                DataTable dtPORV = SQLHelper.GetDataTableOleDb(GlobalSpace.oledbconnstrFSDBMR, StrPORV);
                int Sequence=-1;
                for (int i = 0; i < dtPORV.Rows.Count; i++)
                {
                    if (dtPORV.Rows[i]["APReceiptLineKey"].ToString() == dr["KEY"].ToString())
                    {
                        Sequence = i;
                        break;
                    }
                }
                string StrPORV1 = $@"SELECT A.LotNumber,A.VendorLotNumber,A.LotUserDefined5,A.LotDescription,C.UserName FROM [dbo].[_NoLock_FS_HistoryPOReceipt] A INNER JOIN dbo._NoLock_FS_UserAccess C on C.UserID=A.UserID where  A.PONumber='{dr["采购单号"]}' and A.POLineNumber='{dr["行号"].ToString().TrimStart('0')}' and A.ReceiptQuantity={dr["入库量"]} and A.TransactionDate='{dr["入库日期"]}' order by A.HistoryPOReceiptKey";
                if (Convert.ToDecimal(dr["入库量"].ToString()) < 0)
                {
                    StrPORV1 = $@"SELECT A.LotNumber,A.VendorLotNumber,A.LotUserDefined5,A.LotDescription,C.UserName FROM [dbo].[_NoLock_FS_HistoryPOReceipt] A INNER JOIN dbo._NoLock_FS_UserAccess C on C.UserID=A.UserID where  A.PONumber='{dr["采购单号"]}' and A.POLineNumber='{dr["行号"].ToString().TrimStart('0')}' and A.ReversedQuantity={dr["入库量"].ToString().TrimStart('-')} and A.TransactionDate='{dr["入库日期"]}' order by A.HistoryPOReceiptKey";
                }
                DataTable dtPORV1 = SQLHelper.GetDataTableOleDb(GlobalSpace.oledbconnstrFSDBMR, StrPORV1);
                dr["公司批号"] = dtPORV1.Rows[Sequence]["VendorLotNumber"].ToString().Trim();
                dr["厂家批号"] = dtPORV1.Rows[Sequence]["LotNumber"].ToString().Trim();
                dr["库管员"] = dtPORV1.Rows[Sequence]["UserName"].ToString().Trim();
                dr["生产商码"] = dtPORV1.Rows[Sequence]["LotUserDefined5"].ToString().Trim();
                dr["生产商名"] = dtPORV1.Rows[Sequence]["LotDescription"].ToString().Trim();
            }
            Dgv2.DataSource = dt1;
            for (int i = 0; i < this.Dgv2.Columns.Count; i++)
            {
                this.Dgv2.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
            }
            BtnGet.Tag = TbVendorNameRO.Text+" "+TbAmount.Text;

            for (int i = 0; i < this.Dgv2.Rows.Count; i++)
            {
                string selectPO = $@"Select ForeignNumber,PONumber,LineNumber From PurchaseOrderRecordByCMF Where VendorNumber='{TbVendorNameRO.Text.Split(' ')[0]} '   And PONumber ='{Dgv2["采购单号",i].Value.ToString()}' and LineNumber ='{Dgv2["行号", i].Value.ToString()}'";
                DataTable dtPO = SQLHelper.GetDataTable(GlobalSpace.FSDBConnstr, selectPO);
                if (dtPO.Rows.Count == 1)
                {
                    Dgv2["联系单号", i].Value = dtPO.Rows[0]["ForeignNumber"].ToString();
                }
            }
        }

        private void BtnExcelExport_Click(object sender, EventArgs e)
        {
            if (Dgv2.Rows.Count == 0) return;
            SaveFileDialog saveDialog = new SaveFileDialog();
            saveDialog.DefaultExt = "xlsx";
            saveDialog.Filter = "Excel文件|*.xlsx";
            saveDialog.FileName = BtnGet.Tag.ToString().Split(' ')[1]+ DateTime.Now.ToString("yyMMddHHmmss");
            saveDialog.ShowDialog();
            string filename = saveDialog.FileName;
            if (filename.IndexOf(":") < 0) return; //被点了取消


            DataTable dt = ((DataTable)Dgv2.DataSource).Copy();
            
            dt.Columns.Remove("序号");
            string filePath = filename;
            string sheetname = BtnGet.Tag.ToString().Split(' ')[2];

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


                    //if (j == 7 || j == 8 || j == 9 || j == 10 || j == 3)
                    //{
                    //    cell = row.CreateCell(j);
                    //    cell.SetCellValue(Convert.ToDouble(dt.Rows[i][j]));
                    //    //        cell.CellStyle = cellStyle2;
                    //}

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

        private void POInvoice_MR_Load(object sender, EventArgs e)
        {
            
        }

        private void BtnInvoiceManage_Click(object sender, EventArgs e)
        {
            PoInvoiceManage_MR PoIm = new PoInvoiceManage_MR(UserID,UserName);
            PoIm.ShowDialog();
        }

        private void BtnInvoiceSelect_Click(object sender, EventArgs e)
        {
            PoInvoiceSelect_MR PoIm = new PoInvoiceSelect_MR(UserID, UserName,"供应");
            PoIm.ShowDialog();
        }

        private void Dgv1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int RowIndex = e.RowIndex;
            if (RowIndex < 0) return;
            if (e.ColumnIndex < 0) return;
            if (Dgv1.Columns[e.ColumnIndex].Name == "Check")
            {
                Dgv1["Check", RowIndex].Value = !Convert.ToBoolean(Dgv1["Check", RowIndex].Value);
            }
            GetAmount();
        }

        private void RowEnd_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter) return;

            int rowSart, rowEnd;
            rowSart = Convert.ToInt32(RowStart.Text.Trim())-1;
            rowEnd = Convert.ToInt32(RowEnd.Text.Trim())-1;
            for (int i = rowSart; i <= rowEnd; i++)
            {
                Dgv1["Check", i].Value = true;
            }
            GetAmount();
        }

        private void POInvoice_MR_Activated(object sender, EventArgs e)
        {
            TbVendorNameSelect.Focus();
        }

        private void BtnWuInvoiceManage_Click(object sender, EventArgs e)
        {
            PoWuInvoiceManage_MR PWIM = new PoWuInvoiceManage_MR(UserID,UserName);
            PWIM.ShowDialog();
        }
    }
}
