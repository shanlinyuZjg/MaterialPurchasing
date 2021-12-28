using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using Global;
using Global.Helper;

namespace Global.Audit
{
    public partial class InvoiceAudit : Office2007Form
    {
        public InvoiceAudit()
        {
            InitializeComponent();
            this.EnableGlass = false;
            MessageBoxEx.EnableGlass = false;
        }

        private void dgvDetail_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            /*
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
            */
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            dgvDetail.DataSource = GetInvoice("All", "");
            dgvDetail.Columns["Id"].Visible = false;
        }

        //获取当前待审核发票的信息
        private DataTable GetInvoice(string type,string value)
        {
            string sqlSelect = @"SELECT
	                                            T1.Id,
	                                            T1.InvoiceNumber AS 发票号,
	                                            T1.ReceiveDate AS 入库日期,	                                            
	                                            T1.PONumber AS 采购单号,
	                                            T1.VendorName AS 供应商名,
	                                            T1.MfgName AS 生产商名,
	                                            T1.LineNumber AS 行号,
	                                            T1.ItemNumber AS 代码,
	                                            T1.ItemDescription AS 物料描述,
	                                            T1.UM AS 单位,
	                                            T1.OrderQuantity AS 订货量,
	                                            T1.ReceiveQuantity AS 入库量,
	                                            T1.UnitPrice AS 单价,
	                                            (
		                                            T1.ReceiveQuantity * T1.UnitPrice
	                                            ) AS 合计,
                                                T1.ForeignNumber AS 联系单号,
	                                            T1.LotNumber AS 厂家批号,
	                                            T1.InternalLotNumber AS 公司批号,
	                                            T1.StockKeeper AS 库管员,
	                                            T1.BuyerID AS 采购员
                                            FROM
	                                            dbo.PurchaseOrderInvoiceRecordByCMF T1
                                            WHERE
	                                            T1.Status = 1 ";
            string  sqlOrder = @" ORDER BY  T1.Id ASC";
            string sqlCriteria = string.Empty;
            if(type == "All")
            {
                sqlCriteria = @" And 1=1 ";
            }
            else
            {
                sqlCriteria = @" And T1.InvoiceNumber='"+value+"' ";
            }

            return SQLHelper.GetDataTable(GlobalSpace.FSDBConnstr, sqlSelect + sqlCriteria + sqlOrder);
        }

        private void tbInvoiceNumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar == (char)13)
            {
                if(!string.IsNullOrWhiteSpace(tbInvoiceNumber.Text))
                {
                    dgvDetail.DataSource = GetInvoice("invoice", tbInvoiceNumber.Text.Trim());
                    dgvDetail.Columns["Id"].Visible = false;
                }
            }
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            List<string> idList = new List<string>();
            foreach(DataGridViewRow dgvr in dgvDetail.Rows)
            {
                if(Convert.ToBoolean(dgvr.Cells["Check"].Value))
                {
                    idList.Add(dgvr.Cells["Id"].Value.ToString());
                }
            }

            string sqlUpdate = @"Update PurchaseOrderInvoiceRecordByCMF Set Status = 2,AuditUpdateDateTime='"+DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss")+"' Where Id In ('{0}')";
            sqlUpdate = string.Format(sqlUpdate, string.Join("','", idList.ToArray()));
            if (SQLHelper.ExecuteNonQuery(GlobalSpace.FSDBConnstr, sqlUpdate))
            {
                MessageBoxEx.Show("确认成功！", "提示");
            }
            else
            {
                MessageBoxEx.Show("确认失败！", "提示");
            }

        }

        private void InvoiceAudit_Load(object sender, EventArgs e)
        {

        }

        private void btnReturn_Click(object sender, EventArgs e)
        {
            List<string> idList = new List<string>();
            foreach (DataGridViewRow dgvr in dgvDetail.Rows)
            {
                if (Convert.ToBoolean(dgvr.Cells["Check"].Value))
                {
                    idList.Add(dgvr.Cells["Id"].Value.ToString());
                }
            }

            string sqlUpdate = @"Update EBR_ReceiveRecordForInspect Set Status = -1 Where Id In ('{0}')";
            sqlUpdate = string.Format(sqlUpdate, string.Join("','", idList.ToArray()));
            if (SQLHelper.ExecuteNonQuery(GlobalSpace.FSDBConnstr, sqlUpdate))
            {
                MessageBoxEx.Show("退回成功！", "提示");
            }
            else
            {
                MessageBoxEx.Show("退回失败！", "提示");
            }
        }

        private void btnViewRecord_Click(object sender, EventArgs e)
        {
            string sqlSelect = @"SELECT
                                                    InvoiceNumber AS 发票号码,
	                                                PONumber AS 采购单号,
	                                                LineNumber AS 行号,
	                                                VendorNumber AS 供应商码,
	                                                VendorName AS 供应商名,
	                                                ItemNumber AS 物料代码,
	                                                ItemDescription AS 物料描述,
	                                                UM AS 单位,
	                                                ReceiveQuantity AS 入库数量,
	                                                UnitPrice AS 采购单价
                                                FROM
	                                                PurchaseOrderInvoiceRecordByCMF_copy
                                                WHERE
	                                                Status = 2 And  (FinanceUpdateDateTime >'" + dtpStart.Value.AddDays(-1).ToString("yyyy-mm-dd") + "' And FinanceUpdateDateTime <'" + dtpStart.Value.AddDays(1).ToString("yyyy-mm-dd") + "' ) order by PONumber,LineNumber  ASC";
            DataTable dt = SQLHelper.GetDataTable(GlobalSpace.FSDBConnstr, sqlSelect);
            dgvRecord.DataSource = dt;
        }
    }
}
