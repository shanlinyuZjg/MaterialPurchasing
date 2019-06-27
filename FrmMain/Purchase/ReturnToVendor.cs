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
using System.Data.SqlClient;
using System.Globalization;

namespace Global.Purchase
{
    public partial class ReturnToVendor : Office2007Form
    {
        string userID = string.Empty;
        string userName = string.Empty;
        string PONumber = string.Empty;
        List<string> ItemInfoList = new List<string>(3);
        public ReturnToVendor(string userid,string username)
        {
            userID = userid;
            userName = username;
            InitializeComponent();
        }

        private void tbPONumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            
            if(e.KeyChar == (char)13)
            {
                if(tbPONumber.Text.Trim() != "")
                {
                    List<string> list = GetVendorInfo(tbPONumber.Text.Trim());
                    if(list.Count <= 0)
                    {
                        MessageBoxEx.Show("采购订单供应商信息查不到！", "提示");
                        return;
                    }
                    else
                    {
                        tbVendorNumber.Text = list[0];
                        tbVendorName.Text = list[1];
                        PONumber = tbPONumber.Text.Trim();
                        string sqlSelect = @"SELECT DISTINCT
	                                                    T3.POLineNumberString AS 行号,
	                                                    T6.ItemNumber AS 物料代码,
	                                                    T6.ItemDescription AS 物料描述
                                                    FROM
	                                                    _NoLock_FS_POHeader T1,
	                                                    _NoLock_FS_POLine T3,
	                                                    _NoLock_FS_Item T4,
	                                                    _NoLock_FS_POLineData T5,
	                                                    _NoLock_FS_Item T6
                                                    WHERE
	                                                    T1.POHeaderKey = T3.POHeaderKey
                                                    AND T6.ItemKey = T3.ItemKey
                                                    AND T3.ItemKey = T4.ItemKey
                                                    AND T5.POLineKey = T3.POLineKey
                                                    AND T1.PONumber = '" + tbPONumber.Text.Trim() + "' ORDER BY T3.POLineNumberString ASC";
                        dgvPOItem.DataSource = SQLHelper.GetDataTableOleDb(GlobalSpace.oledbconnstrFSDBMR, sqlSelect);
                    }
                  
                }
                else
                {
                    MessageBoxEx.Show("采购订单号不能为空！", "提示");
                }
            }
        }

      

        private void ReturnToVendor_Load(object sender, EventArgs e)
        {
            MessageBoxEx.EnableGlass = false;
            
        }

        private void tbPONumber_TextChanged(object sender, EventArgs e)
        {
            tbPONumber.Text = tbPONumber.Text.ToUpper();
            tbPONumber.SelectionStart = tbPONumber.Text.Length;//避免光标在输入字母的前面
        }

        private void dgvPOItem_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.RowIndex < 0)
            {
                MessageBoxEx.Show("选择行不正确！请重新选择。", "提示");
            }
            else
            {
                CommonOperate.EmptyDataGridView(dgvPOItemInventory);
                int poLineNumber =Convert.ToInt32(dgvPOItem.Rows[e.RowIndex].Cells["行号"].Value.ToString());
               if(ItemInfoList.Count > 0)
                {
                    ItemInfoList.Clear();
                }
                ItemInfoList.Add(dgvPOItem.Rows[e.RowIndex].Cells["行号"].Value.ToString());
                ItemInfoList.Add(dgvPOItem.Rows[e.RowIndex].Cells["物料代码"].Value.ToString());
                ItemInfoList.Add(dgvPOItem.Rows[e.RowIndex].Cells["物料描述"].Value.ToString());
                string stockkeeper = CommonOperate.GetItemStockKeeper(ItemInfoList[1]);
                if (stockkeeper == "")
                {
                    MessageBoxEx.Show("未查到该物料的库管员，无法进行退库操作！", "提示");
                }
                else
                {
                    string sqlSelect = @"SELECT
                                                        TransactionDate AS 入库日期,
	                                                    POLineNumber AS 行号,
	                                                    ItemNumber AS 物料代码,
	                                                    ItemStandardLocalUnitPrice AS 单价,
	                                                    ItemOrderedQuantity AS 订购数量,
	                                                    (
		                                                    CASE POReceiptActionType
		                                                    WHEN 'R' THEN
			                                                    '入库'
		                                                    WHEN 'X' THEN
			                                                    '退库'
		                                                    END
	                                                    ) AS 接收类型,
	                                                    (
		                                                    CASE
		                                                    WHEN ReceiptQuantity IS NULL THEN
			                                                    '0'
		                                                    ELSE
			                                                    ReceiptQuantity
		                                                    END
	                                                    ) AS 接收数量,
                                                        Stockroom1 as 库,
                                                        Bin1 as 位,
                                                        InventoryCategory1 as IC,
                                                        PromisedDeliveryDate As 承诺交货日期,
	                                                    (
		                                                    CASE
		                                                    WHEN ReversedQuantity IS NULL THEN
			                                                    '0'
		                                                    ELSE
			                                                    ReversedQuantity
		                                                    END
	                                                    ) AS 退货数量,
	                                                    TotalReceiptQuantity AS 当前数量,
	                                                    VendorID AS 供应商代码,
	                                                    VendorLotNumber AS 供应商批号,
                                                        LotNumber AS 公司批号                                                       
                                                    FROM
	                                                    PORV
                                                    WHERE
	                                                    PONumber = '" + PONumber + "'  AND POLineNumber = " + poLineNumber + " ORDER BY TransactionDate DESC";
                    dgvPOItemDetail.DataSource = SQLHelper.GetDataTable(GlobalSpace.FSDBConnstr, sqlSelect);
                }
                tbLineNumber.Text = ItemInfoList[0];
                tbItemNumber.Text = ItemInfoList[1];
                tbItemDescription.Text = ItemInfoList[2];
                tbItemStockKeeper.Text = stockkeeper;
                cbbItemIC.SelectedIndex = -1;
            }
        }

        //查询供应商信息
        private List<string> GetVendorInfo( string ponumber)
        {
            List<string> list = new List<string>(2);
            string sqlSelect = @"SELECT
	                                            T2.VendorID,
	                                            T2.VendorName
                                            FROM
	                                            _NoLock_FS_POHeader T1,
	                                            _NoLock_FS_Vendor T2
                                            WHERE
	                                            T1.VendorID = T2.VendorID
                                            AND T1.PONumber ='"+ponumber+"'";
            DataTable dtTemp = SQLHelper.GetDataTableOleDb(GlobalSpace.oledbconnstrFSDBMR, sqlSelect);
            if(dtTemp.Rows.Count > 0)
            {
                list.Add(dtTemp.Rows[0]["VendorID"].ToString());
                list.Add(dtTemp.Rows[0]["VendorName"].ToString());
            }
            return list;
        }

        private void dgvPOItem_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            dgvPOItem_CellDoubleClick(sender, e);
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            PONumber = tbPONumber.Text.Trim();
        }

        private void dgvPOItemDetail_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            dgvPOItemDetail_CellDoubleClick(sender, e);
        }

        private void dgvPOItemDetail_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                MessageBoxEx.Show("选择行不正确！请重新选择！", "提示");
            }
            else
            {
                CommonOperate.EmptyDataGridView(dgvPOItemInventory);
                tbItemReturnedPointOfUse.Text = "";
                tbItemReturnedQuantity.Text = "";
                tbItemReturnedReason.Text = "";
                if(dgvPOItemDetail.Rows[e.RowIndex].Cells["接收类型"].Value.ToString() == "退库")
                {
                    MessageBoxEx.Show("不能选择退库的记录，请选择入库的记录！", "提示");
                }
                else
                {
                    tbItemOrderedQuantity.Text = dgvPOItemDetail.Rows[e.RowIndex].Cells["订购数量"].Value.ToString();
                    tbItemReceivedQuantity.Text = dgvPOItemDetail.Rows[e.RowIndex].Cells["接收数量"].Value.ToString();
                    tbVendorLotNumber.Text = dgvPOItemDetail.Rows[e.RowIndex].Cells["供应商批号"].Value.ToString();
                    tbInternalLotNumber.Text = dgvPOItemDetail.Rows[e.RowIndex].Cells["公司批号"].Value.ToString();
                    tbStockroom.Text = dgvPOItemDetail.Rows[e.RowIndex].Cells["库"].Value.ToString();
                    tbBin.Text = dgvPOItemDetail.Rows[e.RowIndex].Cells["位"].Value.ToString();
                    tbPromisedDeliveryDate.Text = dgvPOItemDetail.Rows[e.RowIndex].Cells["承诺交货日期"].Value.ToString();
                    tbLineNumber.Text = ItemInfoList[0];
                    tbItemNumber.Text = ItemInfoList[1];
                    if(ItemInfoList[1].Substring(0,1) == "A")
                    {
                        cbbItemIC.SelectedIndex = 1;
                    }
                    tbItemDescription.Text = ItemInfoList[2];
                    dgvPOItemInventory.DataSource = GetPOItemInventory(ItemInfoList[1], dgvPOItemDetail.Rows[e.RowIndex].Cells["公司批号"].Value.ToString());
                }
            }
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            string ic = string.Empty;

            if(cbbItemIC.Text =="")
            {
                MessageBoxEx.Show("物料状态必须选择！", "提示");
                return;
            }
            DateTime dt;
            DateTimeFormatInfo dtFormat = new System.Globalization.DateTimeFormatInfo();
         //   dtFormat.ShortDatePattern = "MMddyyyy";
            dt = Convert.ToDateTime(tbPromisedDeliveryDate.Text.Trim(), dtFormat);
            string date = dt.ToString("MMddyy");
            if (String.IsNullOrEmpty(tbItemReturnedQuantity.Text.Trim()) || String.IsNullOrEmpty(tbItemReturnedReason.Text.Trim()))
            {
                MessageBoxEx.Show("退库数量或者退库原因不能为空！", "提示");
                return; 
            }
            else if(String.IsNullOrEmpty(tbVendorNumber.Text.Trim()) || String.IsNullOrEmpty(tbItemNumber.Text.Trim()))
            {
                MessageBoxEx.Show("供应商代码或者物料代码不能为空！", "提示");
                return;
            }
            else
            {
                string sqlInsert = @"INSERT INTO PurchaseOrderItemReturnedByCMF 
		                                        (
			                                        PONumber,
			                                        VendorNumber,
			                                        VendorName,
			                                        ItemNumber,
			                                        ItemDescription,
			                                        ItemOrderedQuantity,
			                                        ItemReceivedQuantity,
			                                        VendorLotNumber,
			                                        InternalLotNumber,
			                                        ItemReturnedQuantity,
			                                        Comment,PointOfUse,StockKeeper,Buyer,LineNumber,Stock,Bin,IC,PromisedDate
		                                        )
                                        VALUES
	                                        (
			                                        @PONumber,
			                                        @VendorNumber,
			                                        @VendorName,
			                                        @ItemNumber,
			                                        @ItemDescription,
			                                        @ItemOrderedQuantity,
			                                        @ItemReceivedQuantity,
			                                        @VendorLotNumber,
			                                        @InternalLotNumber,
			                                        @ItemReturnedQuantity,
			                                        @Comment,@PointOfUse,@StockKeeper,@Buyer,@LineNumber,@Stock,@Bin,@IC,@PromisedDate
	                                        )";
                SqlParameter[] sqlparams =
                {
                    new SqlParameter("@PONumber",PONumber),
                    new SqlParameter("@VendorNumber",tbVendorNumber.Text.Trim()),
                    new SqlParameter("@VendorName",tbVendorName.Text.Trim()),
                    new SqlParameter("@ItemNumber",tbItemNumber.Text.Trim()),
                    new SqlParameter("@ItemDescription",tbItemDescription.Text.Trim()),
                    new SqlParameter("@ItemOrderedQuantity",Convert.ToDouble(tbItemOrderedQuantity.Text.Trim())),
                    new SqlParameter("@ItemReceivedQuantity",Convert.ToDouble(tbItemReceivedQuantity.Text.Trim())),
                    new SqlParameter("@VendorLotNumber",tbVendorLotNumber.Text.Trim()),
                    new SqlParameter("@InternalLotNumber",tbInternalLotNumber.Text.Trim()),
                    new SqlParameter("@ItemReturnedQuantity",tbItemReturnedQuantity.Text.Trim()),
                    new SqlParameter("@Comment",tbItemReturnedReason.Text.Trim()),
                    new SqlParameter("@PointOfUse",tbItemReturnedPointOfUse.Text.Trim()),
                    new SqlParameter("@StockKeeper",tbItemStockKeeper.Text.Trim()),
                    new SqlParameter("@Buyer",userID+"|"+userName),
                    new SqlParameter("@LineNumber",tbLineNumber.Text.Trim()),
                    new SqlParameter("@Stock",tbStockroom.Text.Trim()),
                    new SqlParameter("@Bin",tbBin.Text.Trim()),
                    new SqlParameter("@IC",cbbItemIC.Text.Trim().Substring(0,1)),
                    new SqlParameter("@PromisedDate",date)
                };

                if(SQLHelper.ExecuteNonQuery(GlobalSpace.FSDBConnstr, sqlInsert,sqlparams) )
                {
                    MessageBoxEx.Show("提交成功！", "提示");
                    tbItemReturnedPointOfUse.Text = "";
                    tbItemReturnedQuantity.Text = "";
                    tbItemReturnedReason.Text = "";
                    cbbItemIC.SelectedIndex = -1;
                }
                else
                {
                    MessageBoxEx.Show("提交失败！", "提示");
                }
            }
            CommonOperate.EmptyDataGridView(dgvPOItemDetail);
            CommonOperate.EmptyDataGridView(dgvPOItemInventory);
        }

        //获取订单中当前物料的库存
        private DataTable GetPOItemInventory(string itemnumber,string lotnumber)
        {
            string sqlSelect = "Select T1.Stockroom AS 库,T1.Bin as 位,T1.InventoryCategory AS IC,T1.InventoryQuantity AS 数量,T1.LotNumber as 批号 from _NoLock_FS_ItemInventory T1, _NoLock_FS_Item T2 Where T1.ItemKey = T2.ItemKey And T2.ItemNumber = '"+itemnumber+"' And T1.LotNumber = '"+lotnumber+"' ";
            return SQLHelper.GetDataTable(GlobalSpace.FSDBMRConnstr, sqlSelect);
        }

        private void btnSearch_Click_1(object sender, EventArgs e)
        {
            if (tbPONumber.Text.Trim() != "")
            {
                List<string> list = GetVendorInfo(tbPONumber.Text.Trim());
                if (list.Count < 0)
                {
                    MessageBoxEx.Show("采购订单供应商信息查不到！", "提示");
                    return;
                }
                else
                {
                    tbVendorNumber.Text = list[0];
                    tbVendorName.Text = list[1];
                    PONumber = tbPONumber.Text.Trim();
                    string sqlSelect = @"SELECT DISTINCT
	                                                    T3.POLineNumberString AS 行号,
	                                                    T6.ItemNumber AS 物料代码,
	                                                    T6.ItemDescription AS 物料描述
                                                    FROM
	                                                    _NoLock_FS_POHeader T1,
	                                                    _NoLock_FS_POLine T3,
	                                                    _NoLock_FS_Item T4,
	                                                    _NoLock_FS_POLineData T5,
	                                                    _NoLock_FS_Item T6
                                                    WHERE
	                                                    T1.POHeaderKey = T3.POHeaderKey
                                                    AND T6.ItemKey = T3.ItemKey
                                                    AND T3.ItemKey = T4.ItemKey
                                                    AND T5.POLineKey = T3.POLineKey
                                                    AND T1.PONumber = '" + tbPONumber.Text.Trim() + "' ORDER BY T3.POLineNumberString ASC";
                    dgvPOItem.DataSource = SQLHelper.GetDataTableOleDb(GlobalSpace.oledbconnstrFSDBMR, sqlSelect);
                }
            }
            else
            {
                MessageBoxEx.Show("采购订单号不能为空！", "提示");
            }
        }

        private void btnSearchRecord_Click(object sender, EventArgs e)
        {
            dgvItemReturnedRecord.DataSource = GetSearchResult(tbPONumberRecord, tbPONumberRecord.Text.Trim());
        }

        private void tbPONumberRecord_KeyPress(object sender, KeyPressEventArgs e)
        {
            dgvItemReturnedRecord.DataSource = GetSearchResult(tbPONumberRecord, tbPONumberRecord.Text.Trim());
        }

        private DataTable GetSearchResult(TextBox tb,string ponumber)
        {
            DataTable dtTemp = null;
            if(tb.Text == "")
            {
                MessageBoxEx.Show("但号不能为空！","提示");
            }
            else
            {
                string sqlselect = @"SELECT
	                                                TOP 30 
	                                                (
		                                                CASE Status
		                                                WHEN 0 THEN
			                                                '已提交'
		                                                WHEN 1 THEN
			                                                '已完成'
		                                                END
	                                                ) AS 状态,
	                                                PONumber AS 采购单号,
	                                                VendorNumber AS 供应商代码,
	                                                VendorName AS 供应商名称,
	                                                ItemNumber AS 物料代码,
	                                                ItemDescription AS 物料描述,
	                                                ItemOrderedQuantity AS 采购数量,
                                                    Stock AS 库,
                                                    Bin AS 位,
                                                    IC,
                                                    PromisedDate AS 承诺交货日期,
	                                                ItemReceivedQuantity AS 接收数量,
	                                                VendorLotNumber AS 供应商批号,
	                                                InternalLotNumber AS 公司内部批号,
	                                                ItemReturnedQuantity AS 退回数量,
	                                                Comment AS 退回原因,
	                                                PointOfUse AS 使用单位,
	                                                OperateDateTime AS 提交时间,
	                                                OperateFSDateTime AS 写入四班时间
                                                FROM
	                                                PurchaseOrderItemReturnedByCMF ORDER BY OperateDateTime DESC";
                dtTemp = SQLHelper.GetDataTable(GlobalSpace.FSDBConnstr, sqlselect);
            }
            return dtTemp;
        }

        private DataTable GetSearchResult(string startdate, string finishdate)
        {
            DataTable dtTemp = null;
            string sqlselect = @"SELECT
	                                            (
		                                            CASE Status
		                                            WHEN 0 THEN
			                                            '已提交'
		                                            WHEN 1 THEN
			                                            '已完成'
		                                            END
	                                            ) AS 状态,
	                                            PONumber AS 采购单号,
	                                            VendorNumber AS 供应商代码,
	                                            VendorName AS 供应商名称,
	                                            ItemNumber AS 物料代码,
	                                            ItemDescription AS 物料描述,
	                                            ItemOrderedQuantity AS 采购数量,
	                                            ItemReceivedQuantity AS 接收数量,
                                                Stock AS 库,
                                                Bin AS 位,
                                                IC,
                                                PromisedDate AS 承诺交货日期,
	                                            VendorLotNumber AS 供应商批号,
	                                            InternalLotNumber AS 公司内部批号,
	                                            ItemReturnedQuantity AS 退回数量,
	                                            Comment AS 退回原因,
	                                            PointOfUse AS 使用单位,
	                                            OperateDateTime AS 提交时间,
	                                            OperateFSDateTime AS 写入四班时间
                                            FROM
	                                            PurchaseOrderItemReturnedByCMF 
                                            Where OperateDateTime >= '" + startdate+"'And OperateDateTime <='"+finishdate+ "'                                              ORDER BY OperateDateTime DESC";
            dtTemp = SQLHelper.GetDataTable(GlobalSpace.FSDBConnstr, sqlselect);

            return dtTemp;
        }

        private void btnDateSearchRecord_Click(object sender, EventArgs e)
        {
            dgvItemReturnedRecord.DataSource =  GetSearchResult(dtpStart.Value.AddDays(-1).ToString("yyyy-MM-dd"), dtpFinish.Value.AddDays(1).ToString("yyyy-MM-dd"));
        }
    }
}
