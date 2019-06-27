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
using SoftBrands.FourthShift.Transaction;

namespace Global.Warehouse
{
    public partial class StockItemReturned : Office2007Form
    {
        string userID = string.Empty;
        string userPwd = string.Empty;
        string userName = string.Empty;
        DataTable dtPORVX = null;
        public StockItemReturned(string id,string password,string name)
        {
            userID = id;
            userPwd = password;
            userName = name;
            this.EnableGlass = false;
            MessageBoxEx.EnableGlass = false;
            InitializeComponent();
        }

        private void btnRIMakeAllChecked_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow dgvr in dgvItemReturnedUnHandledRecod.Rows)
            {
                dgvr.Cells[0].Value = true;
            }
        }

        private void btnRIMakeAllUnchecked_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow dgvr in dgvItemReturnedUnHandledRecod.Rows)
            {
                dgvr.Cells[0].Value = false;
            }
        }

        private void btnItemReturnedRefresh_Click(object sender, EventArgs e)
        {
            GetUnHandledReturnedItem(userID);
        }
                //获得未处理的退库物料
        private void GetUnHandledReturnedItem(string userid)
        {
            string sqlSelect = @"SELECT
                                                Id,
                                                LineNumber AS 行号,
	                                                 (
		                                                CASE Status
		                                                WHEN 0 THEN
			                                                '已提交'
		                                                WHEN 1 THEN
			                                                '已完成'
		                                                END
	                                                ) AS 状态,
	                                                (
		                                                CASE StockKeeper
		                                                WHEN 'CX' THEN
			                                                '公共物料'
		                                                ELSE
			                                                StockKeeper
		                                                END
	                                                ) AS 物料归属,
	                                                PONumber AS 采购单号,
	                                                VendorNumber AS 供应商代码,
	                                                VendorName AS 供应商名称,
	                                                ItemNumber AS 物料代码,
	                                                ItemDescription AS 物料描述,
	                                                ItemOrderedQuantity AS 采购数量,
	                                                ItemReceivedQuantity AS 接收数量,
                                                    Stock as 库,
                                                    Bin as 位,
                                                    IC,
                                                    PromisedDate as 承诺交货日期,
	                                                VendorLotNumber AS 供应商批号,
	                                                InternalLotNumber AS 公司内部批号,
	                                                ItemReturnedQuantity AS 退回数量,
	                                                Comment AS 退回原因,
	                                                PointOfUse AS 使用单位,
	                                                OperateDateTime AS 提交时间,
	                                                OperateFSDateTime AS 写入四班时间
                                                FROM
	                                                PurchaseOrderItemReturnedByCMF
                                                WHERE
	                                                Status = 0
                                                AND (
	                                                LEFT (StockKeeper, 3) = '" + userID + "' OR LEFT (StockKeeper, 2) = 'CX' ) ORDER BY OperateDateTime DESC";
            dtPORVX = SQLHelper.GetDataTable(GlobalSpace.FSDBConnstr, sqlSelect);
            dgvItemReturnedUnHandledRecod.DataSource = SQLHelper.GetDataTable(GlobalSpace.FSDBConnstr, sqlSelect);
            //出选择列外，其余所有列为只读，避免人员误改动信息
            for (int i = 1; i <= dgvItemReturnedUnHandledRecod.Columns.Count - 1; i++)
            {
                dgvItemReturnedUnHandledRecod.Columns[i].ReadOnly = true;
            }
            dgvItemReturnedUnHandledRecod.Columns["Id"].Visible = false;
        }
    

        private void btnItemReturnedFSOperate_Click(object sender, EventArgs e)
        {
            FSFunctionLib.FSConfigFileInitialize(GlobalSpace.fsconfigfilepath, userID, userPwd);

            bool bExist = false;
            string lineId = string.Empty;
            string poNumber = string.Empty;
            string lineNumber = string.Empty;
            string itemNumber = string.Empty;
            double porvQuantity = 0.00000;

            foreach (DataGridViewRow dgvr in dgvItemReturnedUnHandledRecod.Rows)
            {
                if (Convert.ToBoolean(dgvr.Cells[0].Value) == true)
                {
                    bExist = true;
                }
            }
            if (!bExist)
            {
                MessageBoxEx.Show("没有选中行！", "提示");
                return;
            }

            foreach (DataGridViewRow dgvr in dgvItemReturnedUnHandledRecod.Rows)
            {
                if (Convert.ToBoolean(dgvr.Cells[0].Value) == true && Convert.ToInt32(dgvr.Cells["退回数量"].Value) != 0)
                {
                    try
                    {/*
                        lineNumber = dgvr.Cells["行号"].Value.ToString();
                        itemNumber = dgvr.Cells["物料代码"].Value.ToString();
                        porvQuantity = Convert.ToDouble(dgvr.Cells["退回数量"].Value);
                        poNumber = dgvr.Cells["采购单号"].Value.ToString(); */
                        lineId = dgvr.Cells["Id"].Value.ToString();
                        string strUpdatePOItem = @"Update PurchaseOrderItemReturnedByCMF Set Status = 1,OperateFSDateTime = '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "',Operator='"+userID+"' Where Id = '" + lineId + "'";

                        if (PORVX(dgvr))
                        {
                            MessageBoxEx.Show("四班物料退库成功", "提示");
                            //更新订单中物料状态为已入库
                            if (!SQLHelper.ExecuteNonQuery(GlobalSpace.FSDBConnstr, strUpdatePOItem))
                            {
                                MessageBoxEx.Show("退库记录中中更新" + lineNumber + "行物料" + itemNumber + "状态时失败,请联系管理员！", "提示");
                            }

                        }
                        else
                        {
                            MessageBoxEx.Show("四班物料退库失败", "提示");
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBoxEx.Show("异常：" + ex.Message, "提示");
                    }
                }
            }
            FSFunctionLib.FSExit();
        }

        //库管员操作退库
        private bool PORVX(DataGridViewRow dgvr)
        {
            bool bSucceed = false;

            PORV02 porv02 = new PORV02();
            porv02.PONumber.Value = dgvr.Cells["采购单号"].Value.ToString();
            porv02.POLineNumber.Value = dgvr.Cells["行号"].Value.ToString();
            porv02.POReceiptActionType.Value = "X";
            porv02.ItemNumber.Value = dgvr.Cells["物料代码"].Value.ToString();
            porv02.LotNumber.Value = dgvr.Cells["公司内部批号"].Value.ToString();
            porv02.POLineType.Value = "P";
            porv02.LocationReverseQuantity1.Value = dgvr.Cells["退回数量"].Value.ToString();
            porv02.Stockroom1.Value = dgvr.Cells["库"].Value.ToString();
            porv02.Bin1.Value = dgvr.Cells["位"].Value.ToString();
            porv02.InventoryCategory1.Value = dgvr.Cells["IC"].Value.ToString();
            porv02.PromisedDate.Value = dgvr.Cells["承诺交货日期"].Value.ToString();
            if (dgvr.Cells["物料代码"].Value.ToString().Substring(0, 1) == "A")
            {
                porv02.NewLot.Value = "N";
            }
            else
            {
                porv02.NewLot.Value = "Y";
            }

            try
            {
                if (FSFunctionLib.fstiClient.ProcessId(porv02, null))
                {
                    bSucceed = true;
                }
                else
                {
                    FSFunctionLib.FSErrorMsg("四班异常信息");
                }
            }
            catch (Exception ex)
            {
                MessageBoxEx.Show("出现异常：" + ex.Message);
            }
            return bSucceed;
        }
        private void btnSearchRecord_Click(object sender, EventArgs e)
        {
            if(tbItemReturnedItemNumber.Text !="")
            {
                string sqlSelect = @"SELECT
                                                Operator AS 操作者,
                                                LineNumber AS 行号,
	                                                 (
		                                                CASE Status
		                                                WHEN 0 THEN
			                                                '已提交'
		                                                WHEN 1 THEN
			                                                '已完成'
		                                                END
	                                                ) AS 状态,
	                                                (
		                                                CASE StockKeeper
		                                                WHEN 'CX' THEN
			                                                '公共物料'
		                                                ELSE
			                                                StockKeeper
		                                                END
	                                                ) AS 物料归属,
	                                                PONumber AS 采购单号,
	                                                VendorNumber AS 供应商代码,
	                                                VendorName AS 供应商名称,
	                                                ItemNumber AS 物料代码,
	                                                ItemDescription AS 物料描述,
	                                                ItemOrderedQuantity AS 采购数量,
	                                                ItemReceivedQuantity AS 接收数量,
                                                    Stock as 库,
                                                    Bin as 位,
                                                    IC,
                                                    PromisedDate as 承诺交货日期,
	                                                VendorLotNumber AS 供应商批号,
	                                                InternalLotNumber AS 公司内部批号,
	                                                ItemReturnedQuantity AS 退回数量,
	                                                Comment AS 退回原因,
	                                                PointOfUse AS 使用单位,
	                                                OperateDateTime AS 提交时间,
	                                                OperateFSDateTime AS 写入四班时间                                                  
                                                FROM
	                                                PurchaseOrderItemReturnedByCMF
                                                WHERE
	                                                Status = 1
                                                AND (
	                                                Operator = '" + userID + "' And ItemNumber='"+tbItemReturnedItemNumber.Text.Trim()+"'  ORDER BY OperateDateTime DESC";
                dgvItemReturnedRecord.DataSource = SQLHelper.GetDataTable(GlobalSpace.FSDBConnstr, sqlSelect);
            }
            else
            {
                MessageBoxEx.Show("物料代码不能为空！", "提示");
            }
        }

        private void btnDateSearchRecord_Click(object sender, EventArgs e)
        {
                string sqlSelect = @"SELECT
                                                Operator AS 操作者,
                                                LineNumber AS 行号,
	                                                 (
		                                                CASE Status
		                                                WHEN 0 THEN
			                                                '已提交'
		                                                WHEN 1 THEN
			                                                '已完成'
		                                                END
	                                                ) AS 状态,
	                                                (
		                                                CASE StockKeeper
		                                                WHEN 'CX' THEN
			                                                '公共物料'
		                                                ELSE
			                                                StockKeeper
		                                                END
	                                                ) AS 物料归属,
	                                                PONumber AS 采购单号,
	                                                VendorNumber AS 供应商代码,
	                                                VendorName AS 供应商名称,
	                                                ItemNumber AS 物料代码,
	                                                ItemDescription AS 物料描述,
	                                                ItemOrderedQuantity AS 采购数量,
	                                                ItemReceivedQuantity AS 接收数量,
                                                    Stock as 库,
                                                    Bin as 位,
                                                    IC,
                                                    PromisedDate as 承诺交货日期,
	                                                VendorLotNumber AS 供应商批号,
	                                                InternalLotNumber AS 公司内部批号,
	                                                ItemReturnedQuantity AS 退回数量,
	                                                Comment AS 退回原因,
	                                                PointOfUse AS 使用单位,
	                                                OperateDateTime AS 提交时间,
	                                                OperateFSDateTime AS 写入四班时间                                                  
                                                FROM
	                                                PurchaseOrderItemReturnedByCMF
                                                WHERE
	                                                Status = 1
                                                AND (
	                                                Operator = '" + userID + "' And   (OperateFSDateTime >='"+dtpStart.Value.AddDays(-1).ToString("yyyy-MM-dd")+"' and OperateFSDateTime <='"+dtpFinish.Value.AddDays(-1).ToString("yyyy-MM-dd")+"' ) ORDER BY OperateDateTime DESC";
                dgvItemReturnedRecord.DataSource = SQLHelper.GetDataTable(GlobalSpace.FSDBConnstr, sqlSelect);
            
        }
    }
}
