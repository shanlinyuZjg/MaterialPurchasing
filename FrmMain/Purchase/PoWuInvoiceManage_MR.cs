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
    public partial class PoWuInvoiceManage_MR : Form
    {
        private string UserID = string.Empty;
        private string UserName = string.Empty;
        public PoWuInvoiceManage_MR(string userID,string userName)
        {
            InitializeComponent();
            UserID = userID;
            UserName = userName;
        }

        private void BtnAll_Click(object sender, EventArgs e)
        {
            string sqlSelect = $@"SELECT
                                                    distinct VendorNumber 供应商码,  VendorName 供应商名, Sequence 顺序
                                                FROM
	                                                PurchaseOrderInvoiceRecordMRByCMF where Status =0 and Operator ='{UserID}' and (InvoiceNumberS is null or InvoiceNumberS ='')";
            DGV1.DataSource = SQLHelper.GetDataTable(GlobalSpace.FSDBConnstr, sqlSelect);
            for (int i = 0; i < DGV1.Columns.Count; i++)
            {
                DGV1.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
            }
        }

        private void TbVendorID_KeyDown(object sender, KeyEventArgs e)
        {
           
            string sqlSelect = $@"SELECT
                                                    distinct VendorNumber 供应商码,  VendorName 供应商名, Sequence 顺序
                                                FROM
	                                                PurchaseOrderInvoiceRecordMRByCMF where Status =0 and Operator ='{UserID}' and (InvoiceNumberS is null or InvoiceNumberS ='') and VendorNumber ='{TbVendorID.Text.Trim()}'";
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
	                                                PurchaseOrderInvoiceRecordMRByCMF where VendorNumber ='{DGV1["供应商码", RowIndex].Value.ToString()}' and Sequence='{DGV1["顺序", RowIndex].Value.ToString()}' and Status=0 and (InvoiceNumberS is null or InvoiceNumberS ='')";
            DGV2.DataSource = SQLHelper.GetDataTable(GlobalSpace.FSDBConnstr, sqlSelect);

            for (int i = 0; i < DGV2.Columns.Count; i++)
            {
                DGV2.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
            }
        }

        private void BtnDel_Click(object sender, EventArgs e)
        {
            if (DGV2.Rows.Count == 0) { MessageBox.Show("无信息"); return; }
            List<string> Lists = new List<string>();
            for (int i = 0; i < DGV2.Rows.Count; i++)
            {
                if (Convert.ToBoolean(DGV2["Check", i].Value))
                {
                    Lists.Add(DGV2["Id", i].Value.ToString());
                }
            }
            if (Lists.Count == 0) { MessageBox.Show("未选择"); return; }
            DialogResult dr = MessageBox.Show("是否确认删除？","提示",MessageBoxButtons.OKCancel,MessageBoxIcon.Question);
            if (dr == DialogResult.Cancel) return;
            string sqlDel = $@"delete from PurchaseOrderInvoiceRecordMRByCMF WHERE  Id in ({string.Join(",",Lists)}) and Status = 0";


            if (SQLHelper.ExecuteNonQuery(GlobalSpace.FSDBConnstr, sqlDel))
            {
                MessageBox.Show("删除成功");
                DGV1.DataSource = null;
                DGV2.DataSource = null;
                TbAmount.Text = string.Empty;
                TbInvoiceNumberS.Text = string.Empty;
            }
            else
            {
                MessageBox.Show("删除失败");
            }
        }

        private void BtnSubmit_Click(object sender, EventArgs e)
        {
            if (DGV2.Rows.Count == 0) { MessageBox.Show("无信息"); return; }

            if (string.IsNullOrWhiteSpace(TbInvoiceNumberS.Text)) { MessageBox.Show("无发票号"); return; }

            string VendorID = string.Empty;
            List<string> Lists = new List<string>();
            for (int i = 0; i < DGV2.Rows.Count; i++)
            {
                if (Convert.ToBoolean(DGV2["Check", i].Value))
                {
                    Lists.Add(DGV2["Id", i].Value.ToString());
                    VendorID = DGV2["供应商码", i].Value.ToString();
                }
            }
            if (Lists.Count == 0) { MessageBox.Show("未选择"); return; }

            DataTable dtInvoiceNumberS = SQLHelper.GetDataTable(GlobalSpace.FSDBConnstr, $@"select * from PurchaseOrderInvoiceRecordMRByCMF where VendorNumber='{VendorID}' and InvoiceNumberS='{TbInvoiceNumberS.Text.Trim()}' and Status >0");
            if (dtInvoiceNumberS.Rows.Count > 0)
            {
                MessageBox.Show("此供应商、发票号已存在");
                return;
            }
            //DialogResult dr = MessageBox.Show("是否确认删除？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            //if (dr == DialogResult.Cancel) return;
            string sqlUpdate = $@"update PurchaseOrderInvoiceRecordMRByCMF set InvoiceNumberS='{TbInvoiceNumberS.Text.Trim()}' WHERE  Id in ({string.Join(",", Lists)}) and Status = 0";


            if (SQLHelper.ExecuteNonQuery(GlobalSpace.FSDBConnstr, sqlUpdate))
            {
                MessageBox.Show("有票确认成功");
                DGV1.DataSource = null;
                DGV2.DataSource = null;
                TbAmount.Text = string.Empty;
                TbInvoiceNumberS.Text = string.Empty;
            }
            else
            {
                MessageBox.Show("有票确认失败");
            }
        }

        private void PoWuInvoiceManage_MR_Load(object sender, EventArgs e)
        {

        }

        private void DGV1_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            Rectangle rectangle = new Rectangle(e.RowBounds.Location.X, e.RowBounds.Location.Y, ((DataGridView)sender).RowHeadersWidth - 4, e.RowBounds.Height);
            TextRenderer.DrawText(e.Graphics, (e.RowIndex + 1).ToString(), ((DataGridView)sender).RowHeadersDefaultCellStyle.Font, rectangle, ((DataGridView)sender).RowHeadersDefaultCellStyle.ForeColor, TextFormatFlags.VerticalCenter | TextFormatFlags.Right);
        }

        private void DGV2_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            Rectangle rectangle = new Rectangle(e.RowBounds.Location.X, e.RowBounds.Location.Y, ((DataGridView)sender).RowHeadersWidth - 4, e.RowBounds.Height);
            TextRenderer.DrawText(e.Graphics, (e.RowIndex + 1).ToString(), ((DataGridView)sender).RowHeadersDefaultCellStyle.Font, rectangle, ((DataGridView)sender).RowHeadersDefaultCellStyle.ForeColor, TextFormatFlags.VerticalCenter | TextFormatFlags.Right);
        }

        private void DGV2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int RowIndex = e.RowIndex;
            if (RowIndex < 0) return;
            if (DGV2.Columns[e.ColumnIndex].Name == "Check")
            {
                DGV2["Check", RowIndex].Value = !Convert.ToBoolean(DGV2["Check", RowIndex].Value);
            }
            GetAmount();
        }
        private void GetAmount()
        {
            decimal Amount = 0;
            for (int i = 0; i < DGV2.Rows.Count; i++)
            {
                if (Convert.ToBoolean(DGV2["Check", i].Value))
                {
                    Amount += Convert.ToDecimal(DGV2["总价", i].Value);
                }
            }
            TbAmount.Text = Amount.ToString();
        }

        private void BtnAllSelect_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < DGV2.Rows.Count; i++)
            {
                DGV2.Rows[i].Cells["Check"].Value = true;
            }
            GetAmount();
        }

        private void RowEnd_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter) return;

            int rowSart, rowEnd;
            rowSart = Convert.ToInt32(RowStart.Text.Trim()) - 1;
            rowEnd = Convert.ToInt32(RowEnd.Text.Trim()) - 1;
            for (int i = rowSart; i <= rowEnd; i++)
            {
                DGV2["Check", i].Value = true;
            }
            GetAmount();
        }

        private void TbVendorName_KeyDown(object sender, KeyEventArgs e)
        {
            string sqlSelect = $@"SELECT
                                                    distinct VendorNumber 供应商码,  VendorName 供应商名, Sequence 顺序
                                                FROM
	                                                PurchaseOrderInvoiceRecordMRByCMF where Status =0 and Operator ='{UserID}' and (InvoiceNumberS is null or InvoiceNumberS ='') and VendorName like '%{TbVendorName.Text.Trim()}%'";
            DGV1.DataSource = SQLHelper.GetDataTable(GlobalSpace.FSDBConnstr, sqlSelect);
            for (int i = 0; i < DGV1.Columns.Count; i++)
            {
                DGV1.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
            }
        }
    }
}
