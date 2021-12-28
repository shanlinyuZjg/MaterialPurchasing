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
    public partial class POConfirmByInvoice : Office2007Form
    {
        string PONumber = string.Empty;
        public POConfirmByInvoice(string poNumber)
        {
            InitializeComponent();
            this.EnableGlass = false;
            MessageBoxEx.EnableGlass = false;
            PONumber = poNumber;
        }

        private void POConfirmByInvoice_Load(object sender, EventArgs e)
        {
			string sqlSelect = @"SELECT
	                                                        T1.POReceiptDate AS 入库日期,
	                                                        T1.PONumber AS 采购单号,
	                                                        T1.POLineNumber AS 行号,
	                                                        T1.ItemNumber AS 代码,
	                                                        T2.ItemDescription AS 描述,
                                                          T1.POLineUM AS 单位,
	                                                        T1.ItemOrderedQuantity AS 订单量,
	                                                        (
		                                                        CASE T1.POReceiptActionType
		                                                        WHEN 'R' THEN
			                                                        T1.ItemReceiptQuantity
		                                                        ELSE
			                                                        T1.ReversedQuantity*(-1)
		                                                        END
	                                                        ) AS 入库量,T1.TotalReceiptQuantity AS 累计入库量,
	                                                        T1.ItemStandardLocalUnitPrice AS 单价,
                                                        (
		                                                        CASE T1.POReceiptActionType
		                                                        WHEN 'R' THEN
			                                                        T1.ItemReceiptQuantity
		                                                        ELSE
			                                                        (0 - T1.ReversedQuantity)
		                                                        END
	                                                        )* T1.ItemStandardLocalUnitPrice AS 合计,
	                                                        T1.LotNumber AS 厂家批号,
	                                                        T1.VendorLotNumber AS 公司批号,(
		                                                        SELECT
			                                                        Rtrim(UserName)
		                                                        FROM
			                                                        FSDBMR.dbo._NoLock_FS_UserAccess
		                                                        WHERE
			                                                        UserID = T1.Buyer
	                                                        ) AS 采购员,
	                                                        (	SELECT
			                                                        Rtrim(UserName)
		                                                        FROM
			                                                        FSDBMR.dbo._NoLock_FS_UserAccess 
		                                                        WHERE
			                                                        UserID = T2.ItemReference3) AS 库管员
                                                                                                                    FROM
	                                                                                                                    FSDB.dbo.PORV T1,
	                                                                                                                    FSDBMR.dbo._NoLock_FS_Item T2
                                                                                                                    WHERE
	                                                                                                                    T1.PONumber = '" + PONumber + "' AND T1.ItemNumber = T2.ItemNumber    ORDER BY T1.POLineNumber,T1.TransactionDate,T1.TransactionTime ASC";
			dgvPO.DataSource = SQLHelper.GetDataTableOleDb(GlobalSpace.oledbconnstrFSDBMR, sqlSelect);
		}

        private void btnConfirm_Click(object sender, EventArgs e)
        {
			string sqlInsert = @"Insert Into PurchaseOrderInvoicedPO (PONumber,Operator) Values ('" + PONumber + "','" + PurchaseUser.UserName + "')";
			if (SQLHelper.ExecuteNonQuery(GlobalSpace.FSDBConnstr, sqlInsert))
			{
				MessageBoxEx.Show("更新成功！", "提示");
			}
			else
			{
				MessageBoxEx.Show("更新失败！", "提示");
			}
		}
    }
}
