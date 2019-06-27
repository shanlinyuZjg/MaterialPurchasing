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
    public partial class ItemInventory : Office2007Form
    {
        public ItemInventory()
        {
            this.EnableGlass = false;
            MessageBoxEx.EnableGlass = false;
            InitializeComponent();
        }

        private void ItemInventory_Load(object sender, EventArgs e)
        {

        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if(tbItemNumber.Text !="")
            {
                string strSql = @"
                        SELECT
                        T1.ItemNumber as 物料代码,
	                    T1.ItemDescription as 描述,
	                    T1.ItemUM as 单位,
	                    T2.Stockroom as 仓库,
	                    T2.Bin as 库位,
	                    T2.InventoryQuantity as 库存量,
                        T2.InventoryCategory as 状态,
	                    T2.LotNumber as 批号,
	                    T2.LotReceiptDate as 入库日期	                    
                        FROM
                        _NoLock_FS_Item T1,
                        _NoLock_FS_ItemInventory T2
                         WHERE
                        T1.ItemKey = T2.ItemKey
                        AND 
                        T1.ItemNumber ='"+tbItemNumber.Text.Trim()+"'";
                if (dgvItems.Rows.Count > 0)
                {
                    CommonOperate.EmptyDataGridView(dgvItems);
                }
                dgvItems.DataSource = SQLHelper.GetDataTableOleDb(GlobalSpace.oledbconnstrFSDBMR, strSql);
            }
            else
            {
                MessageBoxEx.Show("物料代码不能为空！", "提示");
            }
        }

        private void btnGetPORV_Click(object sender, EventArgs e)
        {
            if (tbItemNumber.Text != "")
            {
                string strSql = @"SELECT
	                            T1.ItemNumber AS 物料代码,
	                            T3.ItemDescription AS 描述,
	                            T1.ItemUM AS 单位,
	                            T2.VendorName AS 供应商,
	                            T1.VendorLotNumber AS 内部批号,
	                            T1.LotNumber AS 供应商批号,
	                            T1.PONumber AS 采购单号,
	                            T1.ItemOrderedQuantity AS 订单数量,
	                            T1.TotalReceiptQuantity AS 最终入库量,
	                            T1.TransactionDate AS 入库日期,
	                            T4.UserName  AS 采购员
                            FROM
	                            FSDB.dbo.PORV T1,
	                            FSDBMR.dbo._NoLock_FS_Vendor T2,
	                            FSDBMR.dbo._NoLock_FS_Item T3,
	                            FSDBMR.dbo.FS_UserAccess T4
                            WHERE
	                            T4.UserID = T1.Buyer
                            AND
	                            T1.ItemNumber = T3.ItemNumber
                            AND
	                            T2.VendorID = T1.VendorID
                            AND
	                            T1.TransactionDate > '" + startDate.Value.AddDays(-1).ToString("yyyy-MM-dd") + "'AND T1.TransactionDate < '" + endDate.Value.AddDays(-1).ToString("yyyy-MM-dd") + "' AND T1.ItemNumber = '" + tbItemNumber.Text.Trim() + "' Order By T1.TransactionDate Desc";
                if (dgvItems.Rows.Count > 0)
                {
                    CommonOperate.EmptyDataGridView(dgvItems);
                }
                dgvItems.DataSource = SQLHelper.GetDataTableOleDb(GlobalSpace.oledbconnstrFSDBMR, strSql);
            }
            else
            {
                MessageBoxEx.Show("物料代码不能为空！", "提示");
            }
        }

        private void tbItemNumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar ==(char) 13)
            {
                btnSearch_Click(sender, e);
            }
        }

        private void tbItemNumber_TextChanged(object sender, EventArgs e)
        {
            tbItemNumber.Text = tbItemNumber.Text.ToUpper();
            tbItemNumber.SelectionStart = tbItemNumber.Text.Length;
        }
    }
}
