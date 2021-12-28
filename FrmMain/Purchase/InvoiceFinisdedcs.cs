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
    public partial class InvoiceFinisdedcs : Office2007Form
    {
        public InvoiceFinisdedcs()
        {
            InitializeComponent();
            this.EnableGlass = false;
            MessageBoxEx.EnableGlass = false;
        }

        private void tbPONumber_Click(object sender, EventArgs e)
        {
            tbPONumber.Text = "";
            tbPONumber.ForeColor = Color.Black;
        }

        private void tbPONumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar == (char)13)
            {
                if (!string.IsNullOrEmpty(tbPONumber.Text))
                {
                    DataTable dt = GetDT(tbPONumber.Text);
                    if(dt.Rows.Count > 0)
                    {
                        dgvDetail.DataSource = dt;
                        dgvDetail.Columns["Id"].Visible = false;
                    }
                    else
                    {
                        MessageBoxEx.Show("未查到该订单记录！", "提示");
                    }
                    
                }
            }
        }

        private DataTable GetDT(string str)
        {
            string sqlSelect = @"SELECT
	                                    Id,
	                                    Left(CreatedDate,10) AS 日期,
	                                    PONumber AS 采购单号
                                    FROM
	                                    PurchaseOrderInvoicedPO
                                    WHERE  PONumber='" + str.Trim() + "' order by Id Desc";
            
            return SQLHelper.GetDataTable(GlobalSpace.FSDBConnstr, sqlSelect);
        }

        private void btnRecover_Click(object sender, EventArgs e)
        {
            List<string> sqlList = new List<string>();
            foreach (DataGridViewRow dgvr in dgvDetail.Rows)
            {
                if (Convert.ToBoolean(dgvr.Cells["Check"].Value))
                {
                    string sqlUpdate = @"Delete From PurchaseOrderInvoicedPO Where Id="+Convert.ToInt32(dgvr.Cells["Id"].Value)+"";
                    sqlList.Add(sqlUpdate);
                }
            }
            if(sqlList.Count == 0)
            {
                MessageBoxEx.Show("当前无选中项！", "提示");
            }
            else
            {
                if(SQLHelper.BatchExecuteNonQuery(GlobalSpace.FSDBConnstr,sqlList))
                {
                    MessageBoxEx.Show("还原成功！", "提示");
                    dgvDetail.DataSource = GetDT("");
                    dgvDetail.Columns["Id"].Visible = false;
                }
                else
                {
                    MessageBoxEx.Show("还原失败！", "提示");
                }
            }
        }
    }
}
