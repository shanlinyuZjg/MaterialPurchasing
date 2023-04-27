using Global.Helper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Global.Audit
{
    public partial class InvoiceAuditMR : Form
    {
        private string UserID = string.Empty;
        private string UserName = string.Empty;
        public InvoiceAuditMR(string userID,string userName)
        {
            InitializeComponent();
            UserID = userID;
            UserName = userName;
        }

        private void InvoiceAuditMR_Load(object sender, EventArgs e)
        {

        }

        private void BtnAll_Click(object sender, EventArgs e)
        {
            string sqlSelect = @"SELECT
                                                    distinct VendorNumber 供应商码,  VendorName 供应商名, InvoiceNumberS 发票号
                                                FROM
	                                                PurchaseOrderInvoiceRecordMRByCMF where Status=1";
            DGV1.DataSource = SQLHelper.GetDataTable(GlobalSpace.FSDBConnstr, sqlSelect);
            for (int i = 0; i < DGV1.Columns.Count; i++)
            {
                DGV1.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
            }
        }

        private void BtnAuditHistory_Click(object sender, EventArgs e)
        {
            Purchase.PoInvoiceSelect_MR PS = new Purchase.PoInvoiceSelect_MR(UserID, UserName,"审计");
            PS.ShowDialog();
        }

        private void TbInvoiceSelect_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter) return;
            string sqlSelect = $@"SELECT
                                                    distinct VendorNumber 供应商码,  VendorName 供应商名, InvoiceNumberS 发票号
                                                FROM
	                                                PurchaseOrderInvoiceRecordMRByCMF where Status =1  and InvoiceNumberS like '%{TbInvoiceSelect.Text.Trim()}%'";
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
            TbVendorID.Text = DGV1["供应商码", RowIndex].Value.ToString() ;
            TbVendorName.Text =   DGV1["供应商名", RowIndex].Value.ToString();
            TbInvoiceNumberS.Text = DGV1["发票号", RowIndex].Value.ToString();

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
	                                                PurchaseOrderInvoiceRecordMRByCMF where VendorNumber ='{DGV1["供应商码", RowIndex].Value.ToString()}' and InvoiceNumberS='{TbInvoiceNumberS.Text}'";
            DGV2.DataSource = SQLHelper.GetDataTable(GlobalSpace.FSDBConnstr, sqlSelect);

            if (DGV2.Rows.Count > 0) TBstorageAmount.Text = DGV2["入库总金额", 0].Value.ToString();
            for (int i = 0; i < DGV2.Columns.Count; i++)
            {
                DGV2.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
            }
        }

        private void BtnConfrim_Click(object sender, EventArgs e)
        {
            if (DGV2.Rows.Count == 0) { MessageBox.Show("无信息");return; }
            string sqlUpdate = $@"update PurchaseOrderInvoiceRecordMRByCMF set Status=2,AuditUpdateDateTime=getdate(),OperateAudit='{UserID}' WHERE VendorNumber ='{TbVendorID.Text.Trim()}' and  InvoiceNumberS='{TbInvoiceNumberS.Text}'";


            if (SQLHelper.ExecuteNonQuery(GlobalSpace.FSDBConnstr, sqlUpdate))
            {
                MessageBox.Show("提交成功");
                DGV1.DataSource = null;
                DGV2.DataSource = null;
                TbVendorID.Text = string.Empty;
                TbVendorName.Text = string.Empty;
                TbInvoiceNumberS.Text = string.Empty;
                TBstorageAmount.Text = string.Empty;
            }
            else
            {
                MessageBox.Show("提交失败");
            }
        }

        private void BtnReturn_Click(object sender, EventArgs e)
        {
            if (DGV2.Rows.Count == 0) { MessageBox.Show("无信息"); return; }
            string sqlUpdate = $@"update PurchaseOrderInvoiceRecordMRByCMF set Status=0,AuditUpdateDateTime=getdate(),OperateAudit='{UserID}',Remarks='审计退回' WHERE VendorNumber ='{TbVendorID.Text.Trim()}' and  InvoiceNumberS='{TbInvoiceNumberS.Text}'";


            if (SQLHelper.ExecuteNonQuery(GlobalSpace.FSDBConnstr, sqlUpdate))
            {
                MessageBox.Show("退回成功");
                DGV1.DataSource = null;
                DGV2.DataSource = null;
                TbVendorID.Text = string.Empty;
                TbVendorName.Text = string.Empty;
                TbInvoiceNumberS.Text = string.Empty;
                TBstorageAmount.Text = string.Empty;
            }
            else
            {
                MessageBox.Show("退回失败");
            }
        }
    }
}
