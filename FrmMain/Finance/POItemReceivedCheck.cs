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


namespace Global.Finance
{
    public partial class POItemReceivedCheck : Office2007Form
    {
        string UserID = string.Empty;
        public POItemReceivedCheck(string userID)
        {
            InitializeComponent();
            UserID = userID;
            this.EnableGlass = false;
        }

        private void POItemReceivedCheck_Load(object sender, EventArgs e)
        {
            dgvPORV.DataSource = GetPORVRecord(DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd"));
            dgvPORecord.DataSource = GetPORecord(DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd"));
        }

        private DataTable GetPORVRecord(string strDate)
        {
            string sqlSelect = @"SELECT
	                                    TransactionDate AS 入库日期,
	                                    UserID AS 用户代码,
	                                    PONumber AS 采购单号,
	                                    POLineNumber AS 行号,
	                                    ItemNumber AS 物料代码,
	                                    POLineUM AS 单位,
	                                    POReceiptActionType AS 入库类型,
	                                    ItemOrderedQuantity AS 订单数量,
	                                    ReceiptQuantity AS 入库数量,
	                                    TotalReceiptQuantity AS 累计入库数量
                                    FROM
	                                    PORV
                                    WHERE
	                                    TransactionDate = '" + strDate+"'";
            return SQLHelper.GetDataTable(GlobalSpace.FSDBConnstr, sqlSelect);
        }

        private DataTable GetPORecord(string strDate)
        {
            string sqlSelect = @"SELECT
	                                    POInventoryKeeperOperatedIntoFSDate AS 入库日期,
	                                    StockKeeper AS 用户代码,
	                                    PONumber AS 采购单号,
	                                    LineNumber AS 行号,
	                                    ItemNumber AS 物料代码,
	                                    LineUM AS 单位,
	                                    POItemQuantity AS 订单数量,
	                                    PORVQuantity  AS 入库数量
                                    FROM
	                                    PurchaseOrderRecordByCMF
                                    WHERE
	                                    POInventoryKeeperOperatedIntoFSDate = '" + strDate + "'";
            return SQLHelper.GetDataTable(GlobalSpace.FSDBConnstr, sqlSelect);
        }

        private void btnSearchPORV_Click(object sender, EventArgs e)
        {
            dgvPORV.DataSource = GetPORVRecord(dtpSearchDatePORV.Value.ToString("yyyy-MM-dd"));
        }

        private void btnSearchPORecord_Click(object sender, EventArgs e)
        {
            dgvPORecord.DataSource = GetPORecord(dtpSearchDatePORecord.Value.ToString("yyyy-MM-dd"));
        }
    }
}
