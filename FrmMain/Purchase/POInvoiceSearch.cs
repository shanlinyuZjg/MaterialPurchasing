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
    public partial class POInvoiceSearch : Office2007Form
    {
        public POInvoiceSearch()
        {
            InitializeComponent();
            this.EnableGlass = false;
            MessageBoxEx.EnableGlass = false;
        }

        private void POInvoiceSearch_Load(object sender, EventArgs e)
        {

        }

       

        private DataTable GetDetail(string number,string type,string lineNumber="")
        {
            string sqlSelect = string.Empty;        
            if(type =="ItemNumber")
            {
                sqlSelect = @"SELECT    top 500
                                                        ForeignNumber AS [联系单号],
	                                                    PONumber AS [采购单号],
	                                                    LineNumber AS [行号],
	                                                    ItemDescription AS [描述],
	                                                    UM AS [单位],
	                                                    ReceiveQuantity AS [入库量],
	                                                    OrderQuantity AS [订单量],	                                                   
	                                                    ReceiveDate AS [入库日期],
	                                                    UnitPrice AS [单价],ReceiveQuantity*UnitPrice AS [合计],Id,InvoiceNumber AS 发票号码
                                                    FROM
	                                                    dbo.PurchaseOrderInvoiceRecordByCMF
                                                    WHERE ItemNumber='"+ number + "' Order By Id Desc";
            }
            else if(type == "ItemDescription")
            {
                sqlSelect = @"SELECT    top 500
                                                        ForeignNumber AS [联系单号],
	                                                    PONumber AS [采购单号],
	                                                    LineNumber AS [行号],
	                                                    ItemDescription AS [描述],
	                                                    UM AS [单位],
	                                                    ReceiveQuantity AS [入库量],
	                                                    OrderQuantity AS [订单量],	                                                   
	                                                    ReceiveDate AS [入库日期],
	                                                    UnitPrice AS [单价],ReceiveQuantity*UnitPrice AS [合计],Id,InvoiceNumber AS 发票号码
                                                    FROM
	                                                    dbo.PurchaseOrderInvoiceRecordByCMF
                                                    WHERE ItemDescription like'%" + number + "%' Order By Id Desc";
            }
            else if(type =="PONumber")
            {
                if(string.IsNullOrWhiteSpace(lineNumber))
                {
                    sqlSelect = @"SELECT    top 500
                                                        ForeignNumber AS [联系单号],
	                                                    PONumber AS [采购单号],
	                                                    LineNumber AS [行号],
	                                                    ItemDescription AS [描述],
	                                                    UM AS [单位],
	                                                    ReceiveQuantity AS [入库量],
	                                                    OrderQuantity AS [订单量],	                                                   
	                                                    ReceiveDate AS [入库日期],
	                                                    UnitPrice AS [单价],ReceiveQuantity*UnitPrice AS [合计],Id,InvoiceNumber AS 发票号码
                                                    FROM
	                                                    dbo.PurchaseOrderInvoiceRecordByCMF
                                                    WHERE PONumber ='" + number+"' Order By Id Desc";
                }
                else
                {
                    sqlSelect = @"SELECT    top 500
                                                        ForeignNumber AS [联系单号],
	                                                    PONumber AS [采购单号],
	                                                    LineNumber AS [行号],
	                                                    ItemDescription AS [描述],
	                                                    UM AS [单位],
	                                                    ReceiveQuantity AS [入库量],
	                                                    OrderQuantity AS [订单量],	                                                   
	                                                    ReceiveDate AS [入库日期],
	                                                    UnitPrice AS [单价],ReceiveQuantity*UnitPrice AS [合计],Id,InvoiceNumber AS 发票号码
                                                    FROM
	                                                    dbo.PurchaseOrderInvoiceRecordByCMF
                                                    WHERE PONumber ='" + number + "' And LineNumber='"+lineNumber+"' Order By Id Desc";
                }
            }

            MessageBox.Show(sqlSelect);
            return SQLHelper.GetDataTable(GlobalSpace.FSDBConnstr, sqlSelect);
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            if(rbtnItemNumber.Checked)
            {
                dt = GetDetail(tbItem.Text, "ItemNumber");
            }
            else if(rbtnItemDescription.Checked)
            {
                dt = GetDetail(tbItem.Text, "ItemDescription");
            }
            else if(rbtnPONumber.Checked)
            {
                if(string.IsNullOrWhiteSpace(tbLineNumber.Text))
                {
                    dt = GetDetail(tbPONumber.Text, "PONumber");
                }
                else
                {
                    dt = GetDetail(tbPONumber.Text, "PONumber",tbLineNumber.Text);
                }                
            }
            else
            {
                MessageBoxEx.Show("请先选择查询类型！", "提示");
            }
            dgvPODetail.DataSource = dt;
            dgvPODetail.Columns["Id"].Visible = false;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            List<string> sqlList = new List<string>();
            foreach (DataGridViewRow dgvr in dgvPODetail.Rows)
            {
                if (Convert.ToBoolean(dgvr.Cells["Check"].Value))
                {
                    string sqlDelete = @"delete from  PurchaseOrderInvoiceRecordByCMF  where [Id] = '" + dgvr.Cells["Id"].Value.ToString() + "'";
                    sqlList.Add(sqlDelete);
                }
            }

            if (SQLHelper.BatchExecuteNonQuery(GlobalSpace.FSDBConnstr, sqlList))
            {
                MessageBoxEx.Show("删除成功！", "提示");
            }
            else
            {
                MessageBoxEx.Show("删除失败！", "提示");
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
