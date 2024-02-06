using Global.Helper;
using NPOI.HSSF.UserModel;
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
    public partial class PurchaseOrderInfo : Form
    {
        public PurchaseOrderInfo()
        {
            InitializeComponent();
        }

        private void TbVendorNumber_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter) return;
            GetPurchaseOrderInfo();
        }

        private void GetPurchaseOrderInfo()
        {
            string VendorNumber = TbVendorNumber.Text.Trim().ToUpper();
            string sqlSelect = $@"SELECT D.VendorID 供应商码, D.VendorName 供应商名, A.PONumber 采购单号,B.POLineNumberString 行号,C.ItemNumber 物料编码,C.ItemDescription 物料描述,C.ItemUM 物料单位, B.ReceiptQuantity 入库数量,B.LineItemOrderedQuantity 订单数量,B.POLineStatus 四班状态,A.POCreatedDate 订单下达日期 FROM [dbo].[_NoLock_FS_POLine] as B INNER JOIN [dbo].[_NoLock_FS_POHeader] as A on A.POHeaderKey=B.POHeaderKey INNER JOIN [dbo].[_NoLock_FS_Item] AS C on C.ItemKey = B.ItemKey INNER JOIN [dbo].[_NoLock_FS_Vendor] AS D on D.VendorID=A.VendorID  where A.VendorID ='{VendorNumber}' and  A.POCreatedDate >= '{DtpStart.Value.ToString("yyyy-MM-dd")}' and A.POCreatedDate < '{DtpEnd.Value.AddDays(1).ToString("yyyy-MM-dd")}' ";
            //
            DGV1.DataSource = SQLHelper.GetDataTableOleDb(GlobalSpace.oledbconnstrFSDBMR, sqlSelect);
            MessageBox.Show("查询完成");
        }

        private void BtExportExcel_Click(object sender, EventArgs e)
        {
            if (DGV1.Rows.Count == 0)
            { MessageBox.Show("无数据！"); return; }

            string filePath = getExcelpath();
            if (filePath.IndexOf(":") < 0)
            { return; }
            TableToExcel(DGV1, filePath);
            MessageBox.Show("导出完成");
        }
        private static string getExcelpath()
        {
            SaveFileDialog saveDialog = new SaveFileDialog();
            saveDialog.DefaultExt = "xlsx";
            saveDialog.Filter = "EXCEL表格|*.xlsx";
            //saveDialog.FileName = "条形码";
            saveDialog.ShowDialog();
            return saveDialog.FileName;
        }
        public static void TableToExcel(DataGridView dt, string file)
        {
            //AppDomain.CurrentDomain.AssemblyResolve += CurrentDomain_AssemblyResolve;
            IWorkbook workbook;
            string fileExt = Path.GetExtension(file).ToLower();
            if (fileExt == ".xlsx")
            { workbook = new XSSFWorkbook(); }
            else if (fileExt == ".xls")
            { workbook = new HSSFWorkbook(); }
            else { workbook = null; }
            if (workbook == null) { return; }
            ISheet sheet = string.IsNullOrEmpty(dt.Name) ? workbook.CreateSheet("Sheet1") : workbook.CreateSheet(dt.Name);

            //表头  
            IRow row = sheet.CreateRow(0);
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                ICell cell = row.CreateCell(i);
                cell.SetCellValue(dt.Columns[i].HeaderText);
            }

            //数据  
            for (int i = 0, x = 0; i < dt.Rows.Count; i++, x++)
            {

                IRow row1 = sheet.CreateRow(x + 1);
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    ICell cell = row1.CreateCell(j);
                    //if (j == 9)
                    //{
                    //    cell.SetCellValue(dt.Rows[i].Cells[j].Value == null ? "" : Convert.ToDateTime(dt.Rows[i].Cells[j].Value.ToString()).ToString("MMddyy"));
                    //}
                    //else
                    //{
                        cell.SetCellValue(dt.Rows[i].Cells[j].Value == null ? "" : dt.Rows[i].Cells[j].Value.ToString());
                    //}
                }

            }

            //转为字节数组  
            MemoryStream stream = new MemoryStream();//读写内存的对象
            workbook.Write(stream);
            var buf = stream.ToArray();//字节数组
            //保存为Excel文件  
            using (FileStream fs = new FileStream(file, FileMode.Create, FileAccess.Write))
            {
                fs.Write(buf, 0, buf.Length);
                fs.Flush();//缓冲区在内存中有个临时区域  盆 两个水缸 //缓冲区装满才会自动提交
            }
        }

        private void TbVendorName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != (char)13)
            {
                return;
            }
            if (string.IsNullOrWhiteSpace(TbVendorName.Text))
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
	                                    VendorName like '%" + EncodingLD.GetString(EncodingCH.GetBytes(TbVendorName.Text.Trim())) + "%'";
            DataTable dtTemp = SQLHelper.GetDataTableOleDb(GlobalSpace.oledbconnstrFSDBMR, strSql);
            if (dtTemp.Rows.Count == 1)
            {
                TbVendorNumber.Text = dtTemp.Rows[0]["VendorID"].ToString();
                GetPurchaseOrderInfo();
            }
            else
            {
                MessageBox.Show("未查询到，或者有多个！");
                return;
            }
        }
    }
}
