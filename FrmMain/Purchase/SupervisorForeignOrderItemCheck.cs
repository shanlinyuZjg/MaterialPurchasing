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
        public string ItemNumber = string.Empty;
        public string FONumber = string.Empty;
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
            string sqlSelect = @"SELECT DISTINCT
	                                                ForeignOrderNumber AS 外贸单号,
	                                                T2.Name AS 采购员
                                                FROM
	                                                PurchaseDepartmentForeignOrderItemByCMF T1
                                                LEFT JOIN PurchaseDepartmentRBACByCMF T2 ON T1.BuyerID = T2.UserID
                                                WHERE
	                                                T1.SupervisorID = '"+id+"'  AND T1.IsValid = 0   AND T1.Status = 0";
            dgvForeginOrderAndItem.DataSource = SQLHelper.GetDataTable(GlobalSpace.FSDBConnstr, sqlSelect);
        }
        private void LoadHandledForeignOrderItem(string id)
        {
            string sqlSelect = @"Select distinct ForeignOrderNumber AS 外贸单号,ItemNumber AS 物料代码 From PurchaseDepartmentForeignOrderItemByCMF Where SupervisorID='" + id + "' And IsValid = 1 And Status = 1";
        //    dgvForeginOrderAndItem.DataSource = SQLHelper.GetDataTable(GlobalSpace.connstr, sqlSelect);
        }
        private void LoadForeignOrderItemDetail(string id,string  foNumber)
        {
            string sqlSelect = @"Select ItemNumber AS 物料代码,ItemDescription AS 物料描述,ItemUM AS 单位,VendorNumber AS 供应商码,VendorName AS 名称,PurchasePrice AS 价格,Quantity AS 采购数量,SpecificationDescription As 说明,Id From PurchaseDepartmentForeignOrderItemByCMF Where SupervisorID='" + id + "' And ForeignOrderNumber  Like '%"+ foNumber + "%'   And IsValid = 0 And Status = 0";
            dgvForeignOrderDetail.DataSource = SQLHelper.GetDataTable(GlobalSpace.FSDBConnstr, sqlSelect);
            dgvForeignOrderDetail.Columns["Id"].Visible = false;
        }

        private void dgvForeginOrderAndItem_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.RowIndex < 0 )
            {
                MessageBoxEx.Show("请点击有效区域！", "提示");
            }
            else
            {
                FONumber = dgvForeginOrderAndItem.Rows[e.RowIndex].Cells["外贸单号"].Value.ToString();
                LoadForeignOrderItemDetail(userID, FONumber);
            }
        }

        private void dgvForeginOrderAndItem_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            dgvForeginOrderAndItem_CellContentDoubleClick(sender, e);
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            List<string> sqlList = new List<string>();
            string sqlUpdate = string.Empty;

            if (dgvForeignOrderDetail.Rows.Count > 0)
            {
                foreach (DataGridViewRow dgvr in dgvForeignOrderDetail.Rows)
                {
                    if (Convert.ToBoolean(dgvr.Cells["Choose"].Value) == true)
                    {
                        sqlUpdate = @"Update PurchaseDepartmentForeignOrderItemByCMF Set Status = 1,IsValid = 1,OperateDateTime2 = '"+DateTime.Now.ToString()+"'  Where Id = " + dgvr.Cells["Id"].Value.ToString();
                        sqlList.Add(sqlUpdate);
                    }
                    else
                    {
                        sqlUpdate = @"Update PurchaseDepartmentForeignOrderItemByCMF Set Status = 1,IsValid = 0,OperateDateTime2 = '" + DateTime.Now.ToString() + "' Where Id = " + dgvr.Cells["Id"].Value.ToString();
                        //2021-11-19  修复领导提交BUG
                        //sqlUpdate = @"Update PurchaseDepartmentForeignOrderItemByCMF Set Status = 1,IsValid = 0,OperateDateTime2 = '" + DateTime.Now.ToString() + "'  Where ForeignOrderNumber = '" + FONumber + "' And ItemNumber='" + dgvr.Cells["物料代码"].Value.ToString() + "' And  VendorNumber = '" + dgvr.Cells["供应商码"].Value.ToString() + "'";  
                        sqlList.Add(sqlUpdate);
                    }
                }               
            }
            if(!SQLHelper.BatchExecuteNonQuery(GlobalSpace.FSDBConnstr,sqlList))
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

        private void btnMakeAllChecked_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow dgvr in dgvForeignOrderDetail.Rows)
            {
                dgvr.Cells["Choose"].Value = true;
            }
        }
    }
}
