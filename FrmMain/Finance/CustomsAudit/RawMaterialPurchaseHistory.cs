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
using Global.Finance.CustomsAudit;

namespace Global.Finance
{
    public partial class RawMaterialPurchaseHistory : Office2007Form
    {
 //       string lotNumber = string.Empty;
        string itemNumber = string.Empty;
        public RawMaterialPurchaseHistory(string strid)
        {
            this.EnableGlass = false;
            itemNumber = strid;
            InitializeComponent();
        }

        private void RawMaterialPurchaseHistory_Load(object sender, EventArgs e)
        {

        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if(endDate.Value >= startDate.Value)
            {
                string sqlSelect = @"SELECT
	                                        T1.ItemNumber AS 物料代码,
	                                        T3.ItemDescription AS 描述,
	                                        T1.ItemUM AS 单位,
	                                        T2.VendorName AS 供应商,
	                                        T1.VendorLotNumber AS 供应商批号,
	                                        T1.LotNumber AS 内部批号,
	                                        T1.PONumber AS 采购单号,
	                                        T1.ItemOrderedQuantity AS 订单数量,
	                                        T1.ReceiptQuantity AS 入库量,
	                                        T1.TransactionDate AS 入库日期
                                        FROM
	                                        FSDB.dbo.PORV T1,
	                                        FSDBMR.dbo._NoLock_FS_Vendor T2,
	                                        FSDBMR.dbo._NoLock_FS_Item T3
                                        WHERE
                                        T1.ItemNumber = T3.ItemNumber
                                        AND T2.VendorID = T1.VendorID
                                        AND T1.TransactionDate >= '"+startDate.Value.ToString("yyyy-MM-dd")+"' AND T1.TransactionDate <= '"+endDate.Value.ToString("yyyy-MM-dd")+"'  AND T1.ItemNumber = '"+itemNumber+"' ORDER BY T1.TransactionDate DESC";
                dgvPurchaseHistory.DataSource = SQLHelper.GetDataTableOleDb(GlobalSpace.oledbconnstrFSDBMR, sqlSelect);
            }
            else
            {
                MessageBoxEx.Show("截止日期必须大于开始日期！", "提示");
            }
        }

        private void dgvPurchaseHistory_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.RowIndex >= 0)
            {
                 string lotNumber = dgvPurchaseHistory.Rows[e.RowIndex].Cells["内部批号"].Value.ToString();
                RawMaterialForProductManufacturedHistory rfpmfh = new RawMaterialForProductManufacturedHistory(itemNumber, lotNumber);
                rfpmfh.Show();
            }
            else
            {
                MessageBoxEx.Show("请点击有效区域", "提示");
            }
        }

        private void dgvPurchaseHistory_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            dgvPurchaseHistory_CellContentDoubleClick(sender, e);
        }

        private void btnProductHistory_Click(object sender, EventArgs e)
        {
            if(itemNumber !="")
            {
                string lotnumber = dgvPurchaseHistory.CurrentRow.Cells["内部批号"].Value.ToString();
                RawMaterialForProductManufacturedHistory rfpmfh = new RawMaterialForProductManufacturedHistory(itemNumber, lotnumber);
                rfpmfh.Show();
            }
        }
    }
}
