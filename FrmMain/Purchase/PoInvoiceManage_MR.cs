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
    public partial class PoInvoiceManage_MR : Form
    {
        private string UserID = string.Empty;
        private string UserName = string.Empty;
        public PoInvoiceManage_MR(string userID,string userName)
        {
            InitializeComponent();
            UserID = userID;
            UserName = userName;
        }

        private void BtnAll_Click(object sender, EventArgs e)
        {
            string sqlSelect = $@"SELECT
                                                    distinct VendorNumber 供应商码,  VendorName 供应商名, InvoiceNumberS 发票号
                                                FROM
	                                                PurchaseOrderInvoiceRecordMRByCMF where Status =0 and Operator ='{UserID}' and  InvoiceNumberS !='' and  InvoiceNumberS  is not null";
            DGV1.DataSource = SQLHelper.GetDataTable(GlobalSpace.FSDBConnstr, sqlSelect);
            for (int i = 0; i < DGV1.Columns.Count; i++)
            {
                DGV1.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
            }
        }

        private void TbInvoiceSelect_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter) return;
            string sqlSelect = $@"SELECT
                                                    distinct VendorNumber 供应商码,  VendorName 供应商名, InvoiceNumberS 发票号
                                                FROM
	                                                PurchaseOrderInvoiceRecordMRByCMF where Status =0 and Operator ='{UserID}' and InvoiceNumberS like '%{TbInvoiceSelect.Text.Trim()}%'";
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
            TbVendor.Text= DGV1["供应商码", RowIndex].Value.ToString()+" "+ DGV1["供应商名", RowIndex].Value.ToString();
            TbInvoiceNumberS.Text = DGV1["发票号", RowIndex].Value.ToString();

            string sqlSelect = $@"SELECT Id,
    Remarks 备注,VendorNumber 供应商码, 
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

            for (int i = 0; i < DGV2.Columns.Count; i++)
            {
                DGV2.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
            }
            decimal RukuAmount = 0;
            for (int i = 0; i < DGV2.Rows.Count; i++)
            {
                RukuAmount += Convert.ToDecimal(DGV2["总价", i].Value.ToString());
            }
            TbRukuAmount.Text = RukuAmount.ToString();
        }

        private void BtnDel_Click(object sender, EventArgs e)
        {
            if (DGV2.SelectedRows.Count == 0) { MessageBox.Show("无信息或未选中"); return; }

            List<string> Lists = new List<string>();
            for (int i = 0; i < DGV2.SelectedRows.Count; i++)
            {
                Lists.Add(DGV2["Id", i].Value.ToString());
            }
            if (Lists.Count == 0) { MessageBox.Show("未选中"); return; }
            string sqlUpdate = $@"update PurchaseOrderInvoiceRecordMRByCMF set InvoiceNumberS='' WHERE    Id in({string.Join(",",Lists)}) and Status = 0";


            if (SQLHelper.ExecuteNonQuery(GlobalSpace.FSDBConnstr, sqlUpdate))
            {
                MessageBox.Show("退回无票成功");
                DGV1.DataSource = null;
                DGV2.DataSource = null;
                TbVendor.Text = string.Empty;
                TbInvoiceNumberS.Text = string.Empty;
                TbRukuAmount.Text = string.Empty;
            }
            else
            {
                MessageBox.Show("退回无票失败");
            }
        }

        private void BtnSubmit_Click(object sender, EventArgs e)
        {
            if (DGV2.Rows.Count == 0) { MessageBox.Show("无信息"); return; }
            string sqlUpdate = $@"update PurchaseOrderInvoiceRecordMRByCMF set Status=1,UpdateDateTime=getdate(),AllAmount={TbRukuAmount.Text.Trim()}{(string.IsNullOrWhiteSpace(TbTax.Text)?"": $@",InvoiceTaxedAmount={TbTax.Text.Trim()}")}{(string.IsNullOrWhiteSpace(TbAmount.Text) ? "" : $@",InvoiceAmount={TbAmount.Text.Trim()}")} WHERE VendorNumber ='{TbVendor.Text.Split(' ')[0]}' and  InvoiceNumberS='{TbInvoiceNumberS.Text}' and Status=0";


            if (SQLHelper.ExecuteNonQuery(GlobalSpace.FSDBConnstr, sqlUpdate))
            {
                MessageBox.Show("提交成功");
                DGV1.DataSource = null;
                DGV2.DataSource = null;
                TbVendor.Text = string.Empty;
                TbInvoiceNumberS.Text = string.Empty;
                TbRukuAmount.Text = string.Empty;
            }
            else
            {
                MessageBox.Show("提交失败");
            }
        }

        private void PoInvoiceManage_MR_Load(object sender, EventArgs e)
        {

        }
    }
}
