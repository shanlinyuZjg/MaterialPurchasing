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

namespace Global.Purchase
{
    public partial class ManageInvoice : Office2007Form
    {
        public static string SequenceNumber = string.Empty;
        public ManageInvoice()
        {
            InitializeComponent();
            this.EnableGlass = false;
            MessageBoxEx.EnableGlass = false;
        }

        private void tbVendorName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar == (char)13)
            {
                if (!string.IsNullOrWhiteSpace(tbVendorName.Text.Trim()))
                {
                    dgvNumber.DataSource = GetSequenceNumber(tbVendorName.Text.Trim());
                }
            }
        }


        //获取序号
        private DataTable GetSequenceNumber(string vendorName)
        {
            string sqlSelect = @"SELECT DISTINCT
	                                                                SequenceNumber as 序号
                                                                FROM
	                                                                PurchaseOrderInvoiceRecordByCMF
                                                                WHERE
	                                                                VendorName LIKE '%" + vendorName + "%' AND Status = 0   ORDER BY   SequenceNumber ASC";
            return SQLHelper.GetDataTable(GlobalSpace.FSDBConnstr, sqlSelect);
        }

        private void dgvNumber_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            dgvNumber_CellClick(sender, e);
        }

        private void dgvNumber_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            SequenceNumber = dgvNumber.Rows[dgvNumber.CurrentCell.RowIndex].Cells["序号"].Value.ToString();
            DataTable dt = GetDetail(0,SequenceNumber);
            dgvDetail.DataSource = dt;
            dgvDetail.Columns["Guid"].Visible = false;
            dgvDetail.Columns["Id"].Visible = false;
            double total = 0;
            foreach (DataRow dr in dt.Rows)
            {total += Convert.ToDouble(dr["合计"]);
            }
            tbTotalAmount.Text = total.ToString();
        }

        
        private DataTable GetDetail(int iIndex,string number)
        {
            string sqlSelect = string.Empty;
            if(iIndex == 0)
            {
                sqlSelect = @"SELECT
                                                        ForeignNumber AS [联系单号],
	                                                    PONumber AS [采购单号],
	                                                    LineNumber AS [行号],
	                                                    ItemDescription AS [描述],
	                                                    UM AS [单位],
	                                                    ReceiveQuantity AS [入库量],
	                                                    OrderQuantity AS [订单量],	                                                   
	                                                    ReceiveDate AS [入库日期],
	                                                    [Key] AS Guid,
	                                                    UnitPrice AS [单价],ReceiveQuantity*UnitPrice AS [合计],Id
                                                    FROM
	                                                    dbo.PurchaseOrderInvoiceRecordByCMF
                                                    WHERE
	                                                    SequenceNumber = '" + number + "'   AND   VendorName  LIKE '%" + tbVendorName.Text.Trim() + "%' and status = 0 Order By ReceiveDate ASC";
            }
            else
            {
                sqlSelect = @"SELECT
                                            
                                                        ForeignNumber AS [联系单号],
	                                                    PONumber AS [采购单号],
	                                                    LineNumber AS [行号],
	                                                    ItemDescription AS [描述],
	                                                    UM AS [单位],
	                                                    ReceiveQuantity AS [入库量],
	                                                    OrderQuantity AS [订单量],	                                                   
	                                                    ReceiveDate AS [入库日期],
	                                                    [Key] AS Guid,
	                                                    UnitPrice AS [单价],ReceiveQuantity*UnitPrice AS [合计],Id
                                                    FROM
	                                                    dbo.PurchaseOrderInvoiceRecordByCMF
                                                    WHERE
	                                                    InvoiceNumber = '" + number + "'  Order By ReceiveDate ASC";
            }
                
            return SQLHelper.GetDataTable(GlobalSpace.FSDBConnstr, sqlSelect);          
        }

        private void dataGridViewX1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            List<string> sqlList = new List<string>();
            foreach(DataGridViewRow dgvr in dgvDetail.Rows)
            {
                if(Convert.ToBoolean(dgvr.Cells["Check"].Value))
                {
                    string sqlDelete = @"delete from  PurchaseOrderInvoiceRecordByCMF  where [key] = '"+dgvr.Cells["Guid"].Value.ToString()+"'";
                    sqlList.Add(sqlDelete);
                }
            }
            
            if(SQLHelper.BatchExecuteNonQuery(GlobalSpace.FSDBConnstr,sqlList))
            {
                MessageBoxEx.Show("删除成功！","提示");
            }
            else
            {
                MessageBoxEx.Show("删除失败！", "提示");
            }

        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrWhiteSpace(tbInvoiceNumber.Text))
            {
                MessageBoxEx.Show("发票号不能为空！", "提示");
                return;
            }

            List<string> sqlList = new List<string>();
            foreach (DataGridViewRow dgvr in dgvDetail.Rows)
            {
                if (Convert.ToBoolean(dgvr.Cells["Check"].Value))
                {
                    string sqlDelete = @"Update PurchaseOrderInvoiceRecordByCMF Set InvoiceNumber='"+tbInvoiceNumber.Text.Trim()+ "',status=1,UpdateDateTime='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'  where Id = '" + dgvr.Cells["Id"].Value.ToString() + "'";
                    sqlList.Add(sqlDelete);
                }
            }
            if (SQLHelper.BatchExecuteNonQuery(GlobalSpace.FSDBConnstr, sqlList))
            {
                MessageBoxEx.Show("确认成功！", "提示");
                
                dgvNumber.DataSource = GetSequenceNumber(tbVendorName.Text.Trim());

                DataTable dt = GetDetail(0,SequenceNumber);
                dgvDetail.DataSource = dt;
                dgvDetail.Columns["Guid"].Visible = false;
                double total = 0;
                foreach (DataRow dr in dt.Rows)
                {
                    total += Convert.ToDouble(dr["合计"]);
                }
                tbTotalAmount.Text = total.ToString();

            }
            else
            {
                MessageBoxEx.Show("确认失败！", "提示");
            }
        }

        private void btnMakeAllChecked_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow dgvr in dgvDetail.Rows)
            {
                dgvr.Cells["Check"].Value = true;
            }
        }

        private void btnSeveral_Click(object sender, EventArgs e)
        {
            List<string> sqlList = new List<string>();
            foreach(DataGridViewRow dgvr in dgvDetail.Rows)
            {
                if(Convert.ToBoolean(dgvr.Cells["Check"].Value))
                {
                    string sqlInsert = @"INSERT INTO [FSDB].[dbo].[PurchaseOrderInvoiceRecordByCMF] (
	                                                                                            [PONumber],
	                                                                                            [VendorNumber],
	                                                                                            [VendorName],
	                                                                                            [MfgNumber],
	                                                                                            [MfgName],
	                                                                                            [LineNumber],
	                                                                                            [ItemNumber],
	                                                                                            [ItemDescription],
	                                                                                            [UM],
	                                                                                            [ReceiveQuantity],
	                                                                                            [LotNumber],
	                                                                                            [InternalLotNumber],
	                                                                                            [StockKeeper],
	                                                                                            [CreateDate],
	                                                                                            [Status],
	                                                                                            [OrderQuantity],
	                                                                                            [ForeignNumber],
	                                                                                            [BuyerID],
	                                                                                            [InvoiceNumber],
	                                                                                            [SequenceNumber],
	                                                                                            [ReceiveDate],
	                                                                                            [Key],
	                                                                                            [UnitPrice]
                                                                                            ) SELECT
	                                                                                            [PONumber],
	                                                                                            [VendorNumber],
	                                                                                            [VendorName],
	                                                                                            [MfgNumber],
	                                                                                            [MfgName],
	                                                                                            [LineNumber],
	                                                                                            [ItemNumber],
	                                                                                            [ItemDescription],
	                                                                                            [UM],
	                                                                                            [ReceiveQuantity],
	                                                                                            [LotNumber],
	                                                                                            [InternalLotNumber],
	                                                                                            [StockKeeper],
	                                                                                            [CreateDate],
	                                                                                            1,
	                                                                                            [OrderQuantity],
	                                                                                            [ForeignNumber],
	                                                                                            [BuyerID],
	                                                                                            " + tbInvoiceNumber.Text.Trim() + ",";

                    string sqlInsert2 = @" [SequenceNumber],
                                               [ReceiveDate],
	                                            [Key],
	                                            [UnitPrice]
                                                        FROM
                                                PurchaseOrderInvoiceRecordByCMF
                                            WHERE
                                                [Id] = '" + dgvr.Cells["Id"].Value.ToString() + "'";
                    sqlList.Add(sqlInsert + sqlInsert2);
                }
            }
                     
            if(SQLHelper.BatchExecuteNonQuery(GlobalSpace.FSDBConnstr,sqlList))
            {
                MessageBoxEx.Show("确认成功！", "提示");
                dgvNumber.DataSource = GetSequenceNumber(tbVendorName.Text.Trim());

                DataTable dt = GetDetail(0,SequenceNumber);
                dgvDetail.DataSource = dt;
                dgvDetail.Columns["Guid"].Visible = false;
                double total = 0;
                foreach (DataRow dr in dt.Rows)
                {
                    total += Convert.ToDouble(dr["合计"]);
                }
                tbTotalAmount.Text = total.ToString();
            }
            else
            {
                MessageBoxEx.Show("确认失败！", "提示");
            }
        }

        private void dgvNumber_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dgvDetail_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
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

        private void ManageInvoice_Load(object sender, EventArgs e)
        {

        }

        private void tbStartLineNumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                if (!string.IsNullOrWhiteSpace(tbStartLineNumber.Text))
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
                    foreach(DataGridViewRow dgvr in dgvDetail.Rows)
                    {
                        dgvr.Cells["Check"].Value = false;
                    }

                    double total = 0;
                    int count = 0;
                    if (Convert.ToInt32(tbEndLineNumber.Text) > dgvDetail.Rows.Count)
                    {
                        count = dgvDetail.Rows.Count;
                    }
                    else
                    {
                        count = Convert.ToInt32(tbEndLineNumber.Text); 
                    }

                    for (int i = Convert.ToInt32(tbStartLineNumber.Text); i < count+1; i++)
                    {
                        dgvDetail.Rows[i-1].Cells["Check"].Value = true;
                        total += Convert.ToDouble(dgvDetail.Rows[i-1].Cells["合计"].Value);
                    }
                    tbTotalAmount.Text = total.ToString();
                }
            }
        }

        private void dgvDetail_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            
            if(e.ColumnIndex == 0 && e.RowIndex != -1 )//&& Convert.ToBoolean(dgvDetail.Rows[e.RowIndex].Cells[0].Value))//dgvDetail.Columns[e.ColumnIndex].ValueType == typeof(bool))
            {
                //MessageBoxEx.Show(e.RowIndex.ToString());
                double total = 0;
                foreach(DataGridViewRow dgvr in dgvDetail.Rows)
                {
                    if(Convert.ToBoolean(dgvr.Cells["Check"].Value))
                    {
                        total += Convert.ToDouble(dgvr.Cells["合计"].Value);
                    }
                }
                tbTotalAmount.Text = total.ToString();
            }

        }

        private void btnInvoiceFinisedManage_Click(object sender, EventArgs e)
        {
            InvoiceFinisdedcs ifc = new InvoiceFinisdedcs();
            ifc.ShowDialog();
        }

        private void tbInvoiceNumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar ==(char)13)
            {
                dgvDetail.DataSource = GetDetail(1, tbInvoiceNumber.Text);
                dgvDetail.Columns["Guid"].Visible = false;
            }
        
        }

        private void btnModify_Click(object sender, EventArgs e)
        {
            List<string> sqlList = new List<string>();
            foreach (DataGridViewRow dgvr in dgvDetail.Rows)
            {
                if (Convert.ToBoolean(dgvr.Cells["Check"].Value))
                {
                    string sqlDelete = @"Update PurchaseOrderInvoiceRecordByCMF Set InvoiceNumber='"+tbInvoiceNumber2.Text.Trim()+"'  where [key] = '" + dgvr.Cells["Guid"].Value.ToString() + "'";
                    sqlList.Add(sqlDelete);
                }
            }

            if (SQLHelper.BatchExecuteNonQuery(GlobalSpace.FSDBConnstr, sqlList))
            {
                MessageBoxEx.Show("修改成功！", "提示");
            }
            else
            {
                MessageBoxEx.Show("修改失败！", "提示");
            }
        }
    }
}
