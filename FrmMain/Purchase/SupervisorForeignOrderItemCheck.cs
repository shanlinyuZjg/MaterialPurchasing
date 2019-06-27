using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using Global.Helper;

namespace Global.Purchase
{
    public partial class SupervisorForeignOrderItemCheck  : Office2007Form
    {
        public string userID = string.Empty;
        public string itemNumber = string.Empty;
        public string poNumber = string.Empty;
        public SupervisorForeignOrderItemCheck()
        {
            InitializeComponent();
        }
        public SupervisorForeignOrderItemCheck(string strID)
        {
            userID = strID;
            MessageBoxEx.EnableGlass = false;
            InitializeComponent();
        }
        private void SupervisorForeignOrderItemCheck_Load(object sender, EventArgs e)
        {
            LoadUnHandledForeignOrderItem(userID);
        }

        private void LoadUnHandledForeignOrderItem(string id)
        {
            string sqlSelect = @"Select distinct BuyerID AS 采购员,ForeignOrderNumber AS 外贸单号,ItemNumber AS 物料代码 From PurchaseDepartmentForeignOrderItemByCMF Where SupervisorID='"+id+"' And IsValid = 0 And Status = 0";
            dgvForeginOrderAndItem.DataSource = SQLHelper.GetDataTable(GlobalSpace.FSDBConnstr, sqlSelect);
        }
        private void LoadHandledForeignOrderItem(string id)
        {
            string sqlSelect = @"Select distinct ForeignOrderNumber AS 外贸单号,ItemNumber AS 物料代码 From PurchaseDepartmentForeignOrderItemByCMF Where SupervisorID='" + id + "' And IsValid = 1 And Status = 1";
        //    dgvForeginOrderAndItem.DataSource = SQLHelper.GetDataTable(GlobalSpace.connstr, sqlSelect);
        }
        private void LoadForeignOrderItemDetail(string id,string ponumber,string itemnumber)
        {
            string sqlSelect = @"Select ItemNumber AS 物料代码,ItemDescription AS 物料描述,ItemUM AS 单位,VendorNumber AS 供应商码,VendorName AS 名称,PurchasePrice AS 价格,Quantity AS 采购数量,SpecificationDescription As 说明 From PurchaseDepartmentForeignOrderItemByCMF Where SupervisorID='" + id + "' And ForeignOrderNumber='"+ponumber+"' And ItemNumber ='"+itemnumber+"'  And IsValid = 0 And Status = 0";
            dgvForeignOrderDetail.DataSource = SQLHelper.GetDataTable(GlobalSpace.FSDBConnstr, sqlSelect);
        }

        private void dgvForeginOrderAndItem_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.RowIndex < 0 )
            {
                MessageBoxEx.Show("请点击有效区域！", "提示");
            }
            else
            {
                poNumber = dgvForeginOrderAndItem.Rows[e.RowIndex].Cells["外贸单号"].Value.ToString();
                itemNumber = dgvForeginOrderAndItem.Rows[e.RowIndex].Cells["物料代码"].Value.ToString();
                LoadForeignOrderItemDetail(userID, poNumber, itemNumber);
            }
        }

        private void dgvForeginOrderAndItem_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            dgvForeginOrderAndItem_CellContentDoubleClick(sender, e);
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            string sqlStart = @"Update PurchaseDepartmentForeignOrderItemByCMF";
            string sqlSetAdopted = "Set Status = 1,IsValid = 1";
            string sqlSetNotAdopted = "Set Status = 1,IsValid = 0 ";
            string sqlFinish = @"Where ForeignOrderNumber = '"+poNumber+"' And ItemNumber='"+itemNumber+"'";
            string sqlUpdate = string.Empty;
            int i = 0, j = 0,k= 0;
            string sqlExtra = string.Empty;
            string dt = DateTime.Now.ToString();
            if (dgvForeignOrderDetail.Rows.Count > 0)
            {
                foreach (DataGridViewRow dgvr in dgvForeignOrderDetail.Rows)
                {
                    if (Convert.ToBoolean(dgvr.Cells["Choose"].Value) == true)
                    {
                        k += 1;
                    }
                }
                if(k > 1)
                {
                    MessageBoxEx.Show("选中的记录数量超过1条！", "提示");
                    return;
                }
                else if( k == 0)
                {
                    MessageBoxEx.Show("没有选中的记录！", "提示");
                    return;
                }
                else
                {
                    foreach (DataGridViewRow dgvr in dgvForeignOrderDetail.Rows)
                    {
                        sqlUpdate = "";
                        sqlExtra = dgvr.Cells["供应商码"].Value.ToString();
                        if (Convert.ToBoolean(dgvr.Cells["Choose"].Value) == true)
                        {
                            sqlUpdate = sqlStart + " " + sqlSetAdopted + ",OperateDateTime2 = '" + dt + "'" + sqlFinish + " And VendorNumber='" + sqlExtra + "'";
                            if (!SQLHelper.ExecuteNonQuery(GlobalSpace.FSDBConnstr, sqlUpdate))
                            {
                                i++;
                            }
                        }
                        else
                        {
                            sqlUpdate = sqlStart + " " + sqlSetNotAdopted + ",OperateDateTime2 = '" + dt + "'" + sqlFinish + " And VendorNumber='" + sqlExtra + "'";
                            if (!SQLHelper.ExecuteNonQuery(GlobalSpace.FSDBConnstr, sqlUpdate) )
                            {
                                j++;
                            }
                        }
                    }
                }
                
            }
            if( i > 0 || j > 0)
            {
                MessageBoxEx.Show("选择的记录保存失败，请联系管理员！", "提示");
                LoadUnHandledForeignOrderItem(userID);
                CommonOperate.EmptyDataGridView(dgvForeignOrderDetail);
            }
            else
            {
                MessageBoxEx.Show("提交成功！", "提示");
                LoadUnHandledForeignOrderItem(userID);
                CommonOperate.EmptyDataGridView(dgvForeignOrderDetail);
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadUnHandledForeignOrderItem(userID);
        }
    }
}
