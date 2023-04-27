using Global.Helper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Global.Purchase
{
    public partial class PoInvoiceSelect_MR : Form
    {
        private string UserID = string.Empty;
        private string UserName = string.Empty;
        private string Department = string.Empty;
        public PoInvoiceSelect_MR(string userID, string userName,string department)
        {
            InitializeComponent();
            UserID = userID;
            UserName = userName;
            Department = department;
        }

        private void PoInvoiceSelect_MR_Load(object sender, EventArgs e)
        {

        }

        private void BtnSelect_Click(object sender, EventArgs e)
        {
            string sqlSelect = string.Empty;
            if (Department == "供应")
            {
                sqlSelect = $@"SELECT
                                                    distinct VendorNumber 供应商码,  VendorName 供应商名, InvoiceNumberS 发票号,Status
                                                FROM
	                                                PurchaseOrderInvoiceRecordMRByCMF where Status >0 and Operator ='{UserID}' and UpdateDateTime >='{dateTimePicker1.Value.ToString("yyyy-MM-dd")}' and UpdateDateTime <'{dateTimePicker2.Value.AddDays(1).ToString("yyyy-MM-dd")}' and VendorNumber like '%{TbVendorNumber.Text.Trim()}%' and VendorName like '%{TbVendorName.Text.Trim()}%' and InvoiceNumberS like '%{TbInvoiceS.Text.Trim()}%'";
            }
            else if (Department == "审计")
            {
                sqlSelect = $@"SELECT
                                                    distinct VendorNumber 供应商码,  VendorName 供应商名, InvoiceNumberS 发票号,Status
                                                FROM
	                                                PurchaseOrderInvoiceRecordMRByCMF where Status >1 and OperateAudit ='{UserID}' and AuditUpdateDateTime >='{dateTimePicker1.Value.ToString("yyyy-MM-dd")}' and AuditUpdateDateTime <'{dateTimePicker2.Value.AddDays(1).ToString("yyyy-MM-dd")}' and VendorNumber like '%{TbVendorNumber.Text.Trim()}%' and VendorName like '%{TbVendorName.Text.Trim()}%' and InvoiceNumberS like '%{TbInvoiceS.Text.Trim()}%'";
            }
            else
            {
                sqlSelect = $@"SELECT
                                                    distinct VendorNumber 供应商码,  VendorName 供应商名, InvoiceNumberS 发票号,Status
                                                FROM
	                                                PurchaseOrderInvoiceRecordMRByCMF where Status >2 and OperateFinance ='{UserID}' and FinanceUpdateDateTime >='{dateTimePicker1.Value.ToString("yyyy-MM-dd")}' and FinanceUpdateDateTime <'{dateTimePicker2.Value.AddDays(1).ToString("yyyy-MM-dd")}' and VendorNumber like '%{TbVendorNumber.Text.Trim()}%' and VendorName like '%{TbVendorName.Text.Trim()}%' and InvoiceNumberS like '%{TbInvoiceS.Text.Trim()}%'";
            }
            DGV1.DataSource = SQLHelper.GetDataTable(GlobalSpace.FSDBConnstr, sqlSelect);
            for (int i = 0; i < DGV1.Columns.Count; i++)
            {
                DGV1.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
            }
        }

        private void DGV1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int RowIndex = e.RowIndex;
            if (RowIndex < 0) return;

            string sqlSelect = $@"SELECT VendorNumber 供应商码, 
	VendorName 供应商名, 
	PONumber 采购单号, 
	LineNumber 行号, 
	SequenceNumber 序号, 
	ItemNumber 物料编码, 
	ItemDescription 物料描述, 
	UM 单位, 
	ReceiveQuantity 入库量, 
	UnitPrice 单价, 
	Amount 总价, 
	InvoiceNumberS 发票号, 
    AllAmount 入库总金额,
    InvoiceNumber 四班票号,
    InvoiceTaxedAmount 总税额,
    InvoiceAmount 不含税发票总额,
	InvoiceMatchedQuantity 已匹配数量, 
	ReceiveDate 入库日期, 
	ForeignNumber 联系单号,
	APReceiptLineKey 
                                                FROM
	                                                PurchaseOrderInvoiceRecordMRByCMF where VendorNumber ='{DGV1["供应商码", RowIndex].Value.ToString()}' and InvoiceNumberS='{DGV1["发票号", RowIndex].Value.ToString()}'";
            DGV2.DataSource = SQLHelper.GetDataTable(GlobalSpace.FSDBConnstr, sqlSelect);

            for (int i = 0; i < DGV2.Columns.Count; i++)
            {
                DGV2.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
            }
        }
    }
}
