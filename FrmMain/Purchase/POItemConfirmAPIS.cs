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
using Global;

namespace Global.Purchase
{
    public partial class POItemConfirmAPIS : Office2007Form
    {
        string UserID = string.Empty;
        string UserName = string.Empty;
        List<string> ParentGuidList = new List<string>();
        private DataSet UpdateDs = new DataSet();
        private SqlDataAdapter UpdateSDA = new SqlDataAdapter();
        string PONumber = string.Empty;
        string PONumberTimes = string.Empty;
      
        public POItemConfirmAPIS(string id,string name)
        {
            this.EnableGlass = false;
            MessageBoxEx.EnableGlass = false;
            InitializeComponent();
            UserID = id;
            UserName = name;
        }

        private void POItemConfirm_Load(object sender, EventArgs e)
        {
          //  dgvPO.DataSource = GetUnConfirmedPO(PurchaseUser.UserID);           
        }

   
     

        //获取Unconfirmed订单中物料详情
        private DataTable GetVendorPOItemsUnConfirmedDetail(string type,string strValue,string userID)
        {
            string strSql = @"SELECT  T1.POItemConfirmer AS 确认人,T1.PONumber AS  采购单号, T1.VendorNumber AS 供应商码,T1.VendorName AS 供应商名,                                 
                                                T1.LineNumber AS 行号,
	                                        	T1.ItemNumber AS 物料代码,
	                                            T1.ItemDescription AS 物料描述,
	                                            T1.LineUM AS 单位,	                                         
	                                            T1.UnitPrice AS 单价,
	                                            T1.POItemQuantity AS 订购数量,
                                                T1.QualityCheckStandard AS 请验标准,
	                                            T1.DemandDeliveryDate AS 要求到货日,
	                                            T1.ForeignNumber AS 外贸单号,
                                                T1.ActualDeliveryQuantity AS 实际到货数量,
                                                T1.POItemRemainedQuantity AS 剩余到货数量,
                                                T1.StockKeeper AS 库管员,
                                                (case T1.POStatus 
                                                          when  '3' then '已下达'  
                                                          when  '4' then '已到货'                                                       
                                                        when '66' then '部分入库' 
                                                 end     
                                                ) as 状态 ,
                                               T1.Guid
                                        FROM
	                                        PurchaseOrderRecordByCMF T1
                                        WHERE   (T1.POStatus = 3  OR  T1.POStatus = 66 )   ";
            string sqlCriteria = string.Empty;
            switch(type)
            {
                case "ItemNumber":
                    sqlCriteria = " And T1.ItemNumber='" + strValue + "' Order By POItemPlacedDate Desc";
                    break;
                case "ItemDescription":
                    sqlCriteria = " And T1.ItemDescription Like '%"+ strValue + "%'  Order By POItemPlacedDate Desc ";
                    break;
                case "VendorNumber":
                    sqlCriteria = " And T1.VendorNumber='" + strValue + "' Order By POItemPlacedDate Desc";
                    break;
                case "VendorName":
                    sqlCriteria = " And T1.VendorName Like '%" + strValue + "%'  Order By POItemPlacedDate Desc ";
                    break;
                case "PONumber":
                    sqlCriteria = " And T1.PONumber='" + strValue + "' Order By POItemPlacedDate Desc";
                    break;
                case "FONumber":
                    sqlCriteria = " And T1.ForeignNumber Like '%" + strValue + "%'  Order By POItemPlacedDate Desc ";
                    break;
                default:
                    sqlCriteria = "  And 1 =1 ";
                    break;
            }
         //   Custom.MsgEx(sqlCriteria);
            return SQLHelper.GetDataTable(GlobalSpace.FSDBConnstr, strSql + sqlCriteria);
        }
        //获取Confirmed订单中物料详情
        private DataTable GetVendorPOItemsConfirmedDetail(string type, string strValue, string userID)
        {
               string strSql = @"SELECT
                                                   T1.PONumber AS  采购单号, T1.VendorNumber AS 供应商码,T1.VendorName AS 供应商名,
                                                   T1.LineNumber AS 行号,
                                                   T1.ItemNumber AS 物料代码,
                                                   T1.ItemDescription AS 物料描述,
                                                   T1.LineUM AS 单位,	                                         
                                                   T1.UnitPrice AS 单价,
                                                   T1.POItemQuantity AS 订购数量,
                                                   T1.DemandDeliveryDate AS 要求到货日,
                                                   T1.ForeignNumber AS 外贸单号,
                                                   T1.ActualDeliveryQuantity AS 实际到货数量,
                                                   T1.StockKeeper AS 库管员,
                                                   (case T1.POStatus 
                                                             when  '3' then '已下达'  
                                                             when  '4' then '已到货'                                                       
                                                             when  '5' then '已入库'                                                       
                                                           when '66' then '部分入库' 
                                                    end     
                                                   ) as 状态 ,
                                                   T1.AccumulatedActualReceiveQuantity AS 累计数量,T1.Guid
                                           FROM
                                               PurchaseOrderRecordByCMF T1
                                           WHERE   (T1.POStatus = 4  OR  T1.POStatus = 66 )    ";
          
            string sqlCriteria = string.Empty;
            switch (type)
            {
                case "ItemNumber":
                    sqlCriteria = " And T1.ItemNumber='" + strValue + "' Order By POItemPlacedDate Desc";
                    break;
                case "ItemDescription":
                    sqlCriteria = " And T1.ItemDescription Like '%" + strValue + "%'  Order By POItemPlacedDate Desc ";
                    break;
                case "VendorNumber":
                    sqlCriteria = " And T1.VendorNumber='" + strValue + "' Order By POItemPlacedDate Desc";
                    break;
                case "VendorName":
                    sqlCriteria = " And T1.VendorName Like '%" + strValue + "%'  Order By POItemPlacedDate Desc ";
                    break;
                case "PONumber":
                    sqlCriteria = " And T1.PONumber='" + strValue + "' Order By POItemPlacedDate Desc";
                    break;
                case "FONumber":
                    sqlCriteria = " And T1.ForeignNumber='" + strValue + "' Order By POItemPlacedDate Desc";
                    break;
                default:
                    sqlCriteria = "  And 1 =1 ";
                    break;
            }
//            MessageBox.Show(sqlCriteria);
            return SQLHelper.GetDataTable(GlobalSpace.FSDBConnstr, strSql + sqlCriteria);
        }

        private void btnSubmitPOToStockKeeper_Click(object sender, EventArgs e)
        {
            DataTable dtTemp = (DataTable)dgvPODetail.DataSource;
            List<string> sqlList = new List<string>();
            List<string> sqlUpdateList = new List<string>();
            List<string> parentGuidList = new List<string>();


            for(int i = 0;i < dgvPODetail.Rows.Count;i++)
            {
                if (dgvPODetail.Rows[i].Cells["实际到货数量"].Value.ToString() != "0")
                {
                    int status = 0;
                    if (Convert.ToBoolean(dgvPODetail.Rows[i].Cells[0].Value))
                    {
                        status = 66;
                        string sqlInsert = @"INSERT INTO PurchaseOrderRecordByCMF (
	                                                        PONumber,
	                                                        VendorNumber,
	                                                        VendorName,
	                                                        ManufacturerNumber,
	                                                        ManufacturerName,
	                                                        LineNumber,
	                                                        ItemNumber,
	                                                        ItemDescription,
	                                                        LineUM,
	                                                        Buyer,
	                                                        Superior,
	                                                        DemandDeliveryDate,
	                                                        ForeignNumber,
	                                                        StockKeeper,
	                                                        POItemQualityCheckedStatus,
	                                                        POStatus,
	                                                        POSubmittedToSuperiorDateTime,
	                                                        POAudittedBySuperiorDateTime,
	                                                        POPurchaseSentToVendorDateTime,
	                                                        POSentToWarehouseKeeperDateTime,
	                                                        POPurchaseOperatedIntoFSDateTime,
	                                                        PODetailForInvoiceSentToVendorDateTime,
	                                                        POGetInvoiceFromVendorDateTime,
	                                                        POInvoiceRecordedInFSByFinanceDateTime,
	                                                        InvoiceNumber,
	                                                        ItemUsedPoint,
	                                                        ActualDeliveryDate,
	                                                        VendorBatchNumber,
	                                                        QualityCheckStandard,
	                                                        UnitPrice,
	                                                        InvoiceIssuedDateTime,
	                                                        Comment1,
	                                                        Comment2,
	                                                        Comment3,
	                                                        LineType,
	                                                        LineStatus,
	                                                        NeededDate,
	                                                        PromisedDate,
	                                                        POItemQuantity,
	                                                        ActualDeliveryQuantity,
	                                                        PricePreTax,
	                                                        PORVQuantity,
	                                                        Stock,
	                                                        Bin,
	                                                        InspectionPeriod,
	                                                        LotNumber,
	                                                        InternalLotNumber,
	                                                        ManufacturedDate,
	                                                        ExpiredDate,
	                                                        CheckedWay,
	                                                        InstanceID,
	                                                        ContractID,
	                                                        Specification,
	                                                        BuyerName,
	                                                        InventoryPrintTimes,
	                                                        PurchasePrintTimes,
	                                                        POItemFSPlacedDateTime,
	                                                        ReveicedByStockKeeper,
	                                                        ReceivedByFinanceChecker,
	                                                        InvoiceMatchByFinanceChecker,
	                                                        ReceivedByFinanceCheckDateTime,
	                                                        InvoiceMatchByFinanceDateTime,
	                                                        ReceivedByFinanceCheckStatus,
	                                                        InvoiceMatchByFinanceCheckStatus,
	                                                        POInventoryKeeperOperatedIntoFSDate,
	                                                        Guid,
	                                                        TaxRate,
	                                                        IsPORVSentToVendor,
	                                                        IsGetInvoice,
	                                                        InvoiceStatusUpdateDateTime,
	                                                        ParentGuid
                                                        ) SELECT
	                                                        PONumber,
	                                                        VendorNumber,
	                                                        VendorName,
	                                                        ManufacturerNumber,
	                                                        ManufacturerName,
	                                                        LineNumber,
	                                                        ItemNumber,
	                                                        ItemDescription,
	                                                        LineUM,
	                                                        Buyer,
	                                                        Superior,
	                                                        DemandDeliveryDate,
	                                                        ForeignNumber,
	                                                        StockKeeper,
	                                                        POItemQualityCheckedStatus,
	                                                        '4',
	                                                        POSubmittedToSuperiorDateTime,
	                                                        POAudittedBySuperiorDateTime,
	                                                        POPurchaseSentToVendorDateTime,
	                                                        POSentToWarehouseKeeperDateTime,
	                                                        POPurchaseOperatedIntoFSDateTime,
	                                                        PODetailForInvoiceSentToVendorDateTime,
	                                                        POGetInvoiceFromVendorDateTime,
	                                                        POInvoiceRecordedInFSByFinanceDateTime,
	                                                        InvoiceNumber,
	                                                        ItemUsedPoint,
	                                                        ActualDeliveryDate,
	                                                        VendorBatchNumber,
	                                                        QualityCheckStandard,
	                                                        UnitPrice,
	                                                        InvoiceIssuedDateTime,
	                                                        Comment1,
	                                                        Comment2,
	                                                        Comment3,
	                                                        LineType,
	                                                        LineStatus,
	                                                        NeededDate,
	                                                        PromisedDate,
	                                                        POItemQuantity,
	                                                        "+Convert.ToDouble(dgvPODetail.Rows[i].Cells["实际到货数量"].Value) +", PricePreTax, PORVQuantity,Stock,Bin, InspectionPeriod, LotNumber,InternalLotNumber,ManufacturedDate,ExpiredDate,CheckedWay,InstanceID,ContractID,Specification,BuyerName,InventoryPrintTimes,PurchasePrintTimes,POItemFSPlacedDateTime,ReveicedByStockKeeper,ReceivedByFinanceChecker,InvoiceMatchByFinanceChecker,ReceivedByFinanceCheckDateTime,InvoiceMatchByFinanceDateTime,ReceivedByFinanceCheckStatus,InvoiceMatchByFinanceCheckStatus,POInventoryKeeperOperatedIntoFSDate,'" + Guid.NewGuid().ToString("N") + "', TaxRate,  IsPORVSentToVendor,    IsGetInvoice,  InvoiceStatusUpdateDateTime, '" + dgvPODetail.Rows[i].Cells["GUID"].Value.ToString() + "'  FROM  PurchaseOrderRecordByCMF   WHERE    Guid = '" + dgvPODetail.Rows[i].Cells["GUID"].Value.ToString()+ "'";
                        string sql = @"Update PurchaseOrderRecordByCMF Set AccumulatedActualDeliveryQuantity = AccumulatedActualDeliveryQuantity+ " + Convert.ToDouble(dgvPODetail.Rows[i].Cells["实际到货数量"].Value) + ",POStatus = " + status + " Where Guid='" + dgvPODetail.Rows[i].Cells["Guid"].Value.ToString() + "'";
                        if(Convert.ToDouble(dgvPODetail.Rows[i].Cells["累计数量"].Value) + Convert.ToDouble(dgvPODetail.Rows[i].Cells["实际到货数量"].Value) >= Convert.ToDouble(dgvPODetail.Rows[i].Cells["订购数量"].Value))
                        {
                            string sqlUpdate = @"Udpate PurchaseOrderRecordByCMF Set POStatus = 67 Where Guid='" + dgvPODetail.Rows[i].Cells["Guid"].Value.ToString() + "'";
                            sqlUpdateList.Add(sqlUpdate);
                        }
                        sqlList.Add(sqlInsert);
                        sqlList.Add(sql);
                    }
                    else
                    {
                        status = 4;
                        string sql = @"Update PurchaseOrderRecordByCMF Set ActualDeliveryQuantity = " + Convert.ToDouble(dgvPODetail.Rows[i].Cells["实际到货数量"].Value) + ",AccumulatedActualDeliveryQuantity = AccumulatedActualDeliveryQuantity+ " + Convert.ToDouble(dgvPODetail.Rows[i].Cells["实际到货数量"].Value) + ",POStatus = " + status + " Where Guid='" + dgvPODetail.Rows[i].Cells["Guid"].Value.ToString() + "'";
                        sqlList.Add(sql);
                    }
                                   
                }
            }



            if(SQLHelper.BatchExecuteNonQuery(GlobalSpace.FSDBConnstr,sqlList))
            {
                Custom.MsgEx("提交成功！");
            }
            else
            {
                Custom.MsgEx("提交失败！");
            }
        }

        private void tbItemNumberOrDescription_TextChanged(object sender, EventArgs e)
        {
            tbItemNumber.Text = tbItemNumber.Text.ToUpper();
            tbItemNumber.SelectionStart = tbItemNumber.TextLength;
        }

        private void tbFONumber_TextChanged(object sender, EventArgs e)
        {
            tbItemDescription.Text = tbItemDescription.Text.ToUpper();
            tbItemDescription.SelectionStart = tbItemDescription.TextLength;
        }

        private void btnCancelAllChecked2_Click(object sender, EventArgs e)
        {

        }

        private void UpdatePOItemStatus(object sender, EventArgs e)
        {

            if(string.IsNullOrWhiteSpace(tbQualityStandard.Text.Trim()))
            {
                Custom.MsgEx("检验标准不能为空！");
                return;
            }

            List<string> guidList = new List<string>();
            int i = 0;
            foreach (DataGridViewRow dgvr in dgvPODetail.Rows)
            {
                if (Convert.ToBoolean(dgvr.Cells["Check"].Value))
                {
                    guidList.Add(dgvr.Cells["Guid"].Value.ToString()); 
                }
                if(dgvr.Cells["物料代码"].Value.ToString().Substring(0,2)=="MF" || dgvr.Cells["物料代码"].Value.ToString().Substring(0, 2) == "MJ"|| dgvr.Cells["物料代码"].Value.ToString().Substring(0, 2) == "MY")
                {
                    i++;
                }
            }
            if(i > 1)
            {
                if(MessageBoxEx.Show("当前需要填写请验质量标准的物料有多个，是否确认使用同一个质量标准？","提示",MessageBoxButtons.YesNo) == DialogResult.No)
                {
                    return;
                }
            }
            

            if (Convert.ToInt32(tbReceiveRecordQuantity.Text) != guidList.Count)
            {
                Custom.MsgEx("到货批次数量与选中的记录条数不一致，请重新选择！");
                return;
            }

            if (CommonOperate.BatchUpdatePOItemStatusByGuid(guidList, 4))
            {

                if (CreatePOItemReceiveHistory(guidList))
                {
                    Custom.MsgEx("更新状态和确认到货成功！");
                    DataTable dt = (DataTable)dgvPODetail.DataSource;
                    dt.Clear();
                    dgvPODetail.DataSource = dt;
                }
                else
                {
                    Custom.MsgEx("确认到货失败失败！");
                }
            }
            else
            {
                Custom.MsgEx("更新状态失败！");
            }

        }


        private bool CreatePOItemReceiveHistory(List<string> guidList)
        {
            List<string> sqlList = new List<string>();
            for(int i=0;i<guidList.Count;i++)
            {
                string guid = Guid.NewGuid().ToString("N");
                string sqlInsert = @"INSERT INTO PurchaseOrderRecordHistoryByCMF (
	                                                        	[PONumber],
	                                                            [VendorNumber],
	                                                            [VendorName],
	                                                            [ManufacturerNumber],
	                                                            [ManufacturerName],
	                                                            [LineNumber],
	                                                            [ItemNumber],
	                                                            [ItemDescription],
	                                                            [LineUM],
	                                                            [DemandDeliveryDate],
	                                                            [InspectionPeriod],
	                                                            [ParentGuid],
	                                                            [StockKeeper],
	                                                            [LotNumberAssign],
	                                                            [OrderQuantity],
	                                                            [ItemReceiveType],
	                                                            [Supervisor],
	                                                            [ForeignNumber],
	                                                            [BuyerID],Stock,Bin,ReceiveDate,Guid,UnitPrice,QualityCheckStandard,GSID
                                                        ) SELECT
	                                                       	[PONumber],
	                                                        [VendorNumber],
	                                                        [VendorName],
	                                                        [ManufacturerNumber],
	                                                        [ManufacturerName],
	                                                        [LineNumber],
	                                                        [ItemNumber],
	                                                        [ItemDescription],
	                                                        [LineUM],
	                                                        [DemandDeliveryDate],
	                                                        [InspectionPeriod],                                                        
	                                                        [Guid],
	                                                        [StockKeeper],LotNumberAssign,
	                                                        [POItemQuantity],
	                                                        [ItemReceiveType],
	                                                        [Superior],
	                                                        [ForeignNumber],Buyer,Stock,Bin,Left(ActualDeliveryDate,10),Replace(NEWID(),'-',''),[UnitPrice],'"+tbQualityStandard.Text+"',GSID  FROM   PurchaseOrderRecordByCMF  WHERE Guid = '" + guidList[i] + "'";
         //       MessageBox.Show(sqlInsert);
                sqlList.Add(sqlInsert);
            }
                    
            if (SQLHelper.BatchExecuteNonQuery(GlobalSpace.FSDBConnstr, sqlList))
            {
                return true;
            }
            return false;
        }


        private void btnMakeAllChecked2_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dgvPODetail.Rows.Count; i++)
            {
                dgvPODetail.Rows[i].Cells["Check"].Value = true;
            }

        }

      

        private void btnMakeAllCheckedCanceled_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dgvPODetail.Rows.Count; i++)
            {
                dgvPODetail.Rows[i].Cells["Check"].Value = false;
            }
        }


        private void tbItemNumberConfirmed_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (CommonOperate.TextBoxCheck(tbItemNumberConfirmed, e))
            {
                if (e.KeyChar == (char)13)
                {
                    MessageBox.Show(PurchaseUser.UserID);
                    dgvConfirmed.DataSource = GetVendorPOItemsConfirmedDetail("ItemNumber", tbItemNumberConfirmed.Text, PurchaseUser.UserID);
                    tbItemNumberConfirmed.Text = "";

                }
            }
        }

   

        private void tbItemNumberConfirmed_TextChanged(object sender, EventArgs e)
        {
            tbItemNumberConfirmed.Text = tbItemNumberConfirmed.Text.ToUpper();
            tbItemNumberConfirmed.SelectionStart = tbItemNumberConfirmed.Text.Length;
        }

        private void tbItemNumber_TextChanged(object sender, EventArgs e)
        {
            tbItemNumber.Text = tbItemNumber.Text.ToUpper();
            tbItemNumber.SelectionStart = tbItemNumber.Text.Length;
        }

        private void tbItemNumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(!string.IsNullOrEmpty(tbItemNumber.Text))
            {
                if(e.KeyChar == (char)13)
                {
    
                    dgvPODetail.DataSource = GetVendorPOItemsUnConfirmedDetail("ItemNumber", tbItemNumber.Text, PurchaseUser.UserID);
                    dgvPODetail.Columns["Guid"].Visible = false;
              
                }
            }
        }

        private void tbItemDescription_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!string.IsNullOrEmpty(tbItemDescription.Text))
            {
                if (e.KeyChar == (char)13)
                {
                   dgvPODetail.DataSource = GetVendorPOItemsUnConfirmedDetail("ItemDescription", tbItemDescription.Text, "HDH");//PurchaseUser.UserID
                    dgvPODetail.Columns["Guid"].Visible = false;
                    tbItemDescription.Text = "";
                }
            }
        }

        private void tbVendorNumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!string.IsNullOrEmpty(tbVendorNumber.Text))
            {
                if (e.KeyChar == (char)13)
                {
                   dgvPODetail.DataSource =  GetVendorPOItemsUnConfirmedDetail("VendorNumber", tbVendorNumber.Text, PurchaseUser.UserID);
                    dgvPODetail.Columns["Guid"].Visible = false;
                    tbVendorNumber.Text = "";
                }
            }
        }

        private void tbPONumber_TextChanged(object sender, EventArgs e)
        {
            tbPONumber.Text = tbPONumber.Text.ToUpper();
            tbPONumber.SelectionStart = tbPONumber.Text.Length;
        }

        private void tbPONumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!string.IsNullOrEmpty(tbPONumber.Text))
            {
                if (e.KeyChar == (char)13)
                {
                  dgvPODetail.DataSource =  GetVendorPOItemsUnConfirmedDetail("PONumber", tbPONumber.Text, PurchaseUser.UserID);
                    dgvPODetail.Columns["Guid"].Visible = false;
                    tbPONumber.Text = "";

                }
            }
        }

        private void tbVendorName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!string.IsNullOrEmpty(tbVendorName.Text))
            {
                if (e.KeyChar == (char)13)
                {
                 dgvPODetail.DataSource =   GetVendorPOItemsUnConfirmedDetail("VendorName", tbVendorName.Text, PurchaseUser.UserID);
                    dgvPODetail.Columns["Guid"].Visible = false;
                    tbVendorName.Text = "";
                }
            }
        }

        private void tbItemNumberConfirmed_TextChanged_1(object sender, EventArgs e)
        {

        }


        private void tbItemDescriptionConfirmed_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (CommonOperate.TextBoxCheck(tbItemDescriptionConfirmed, e))
            {
                if (e.KeyChar == (char)13)
                {
                    dgvConfirmed.DataSource = GetVendorPOItemsConfirmedDetail("ItemDescription", tbItemDescriptionConfirmed.Text, PurchaseUser.UserID);
                    tbItemDescriptionConfirmed.Text = "";
                }
            }
        }

        private void tbItemDescriptionConfirmed_TextChanged(object sender, EventArgs e)
        {

        }

        private void tbPONumberConfirmed_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (CommonOperate.TextBoxCheck(tbPONumberConfirmed, e))
            {
                if (e.KeyChar == (char)13)
                {
                    dgvConfirmed.DataSource = GetVendorPOItemsConfirmedDetail("PONumber", tbPONumberConfirmed.Text, PurchaseUser.UserID);
                    tbPONumberConfirmed.Text = "";
                }
            }
        }

        private void tbPONumberConfirmed_TextChanged(object sender, EventArgs e)
        {
            tbPONumberConfirmed.Text = tbPONumberConfirmed.Text.ToUpper();
            tbPONumberConfirmed.SelectionStart = tbPONumberConfirmed.Text.Length;
        }

        private void tbVendorNumberConfirmed_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (CommonOperate.TextBoxCheck(tbVendorNumberConfirmed, e))
            {
                if (e.KeyChar == (char)13)
                {
                    dgvConfirmed.DataSource = GetVendorPOItemsConfirmedDetail("VendorNumber", tbVendorNumberConfirmed.Text, PurchaseUser.UserID);
                    tbVendorNumberConfirmed.Text = "";
                }
            }
        }

        private void tbVendorNumberConfirmed_TextChanged(object sender, EventArgs e)
        {

        }

        private void tbVendorNameConfirmed_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (CommonOperate.TextBoxCheck(tbVendorNameConfirmed, e))
            {
                if (e.KeyChar == (char)13)
                {
                    dgvConfirmed.DataSource = GetVendorPOItemsConfirmedDetail("VendorName", tbVendorNameConfirmed.Text, PurchaseUser.UserID);
                    tbVendorNameConfirmed.Text = "";
                }
            }
        }

        private void btnConfirmDeliverySeveralTimes_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(tbQualityStandard.Text.Trim()))
            {
                Custom.MsgEx("检验标准不能为空！");
                return;
            }
            List<string> guidList = new List<string>();
            int i = 0;
            foreach (DataGridViewRow dgvr in dgvPODetail.Rows)
            {
                if (Convert.ToBoolean(dgvr.Cells["Check"].Value))
                {
                    guidList.Add(dgvr.Cells["Guid"].Value.ToString());
                }
                if (dgvr.Cells["物料代码"].Value.ToString().Substring(0, 2) == "MF" || dgvr.Cells["物料代码"].Value.ToString().Substring(0, 2) == "MJ" || dgvr.Cells["物料代码"].Value.ToString().Substring(0, 2) == "MY")
                {
                    i++;
                }
            }

            if (i > 1)
            {
                if (MessageBoxEx.Show("当前需要填写请验质量标准的物料有多个，是否确认使用同一个质量标准？", "提示", MessageBoxButtons.YesNo) == DialogResult.No)
                {
                    return;
                }
            }

            if (Convert.ToInt32(tbReceiveRecordQuantity.Text) != guidList.Count)
            {
                Custom.MsgEx("到货批次数量与选中的记录条数不一致，请重新选择！");
                return;
            }
            if (CommonOperate.BatchUpdatePOItemStatusByGuid(guidList, 66))
            {
                if (CreatePOItemReceiveHistory(guidList))
                {
                    Custom.MsgEx("更新状态和确认到货成功！");
                }
                else
                {
                    Custom.MsgEx("确认到货失败！");
                }
            }
            else
            {
                Custom.MsgEx("更新状态失败！");
            }
            
        }


        private void tbReceiveRecordQuantity_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(!string.IsNullOrEmpty(tbReceiveRecordQuantity.Text))
            {
                if(e.KeyChar == (char)13)
                {
                    int iIndex = dgvPODetail.SelectedCells[0].RowIndex;
                    int iCount = Convert.ToInt32(tbReceiveRecordQuantity.Text.Trim());
                    string strGuid = dgvPODetail.Rows[iIndex].Cells["Guid"].Value.ToString();

                    DataTable dtSource = (DataTable)dgvPODetail.DataSource;
                    DataTable dtNew = dtSource.Copy();

                    if (iCount == 1)
                    {
                        DataRow[] drs = dtSource.Select("Guid = '" + strGuid + "'");
                        DataRow dr = dtSource.NewRow();
                        dr.ItemArray = drs[0].ItemArray;
                        //             dr["Guid"] = Guid.NewGuid().ToString("N");
                        dtSource.Rows.InsertAt(dr, iIndex + 1);
                    }
                    else if (iCount > 1)
                    {
                        for (int i = 0; i < iCount-1; i++)
                        {
                            DataRow[] drs = dtSource.Select("Guid = '" + strGuid + "'");
                            DataRow dr = dtSource.NewRow();
                            dr.ItemArray = drs[0].ItemArray;
                            //            dr["Guid"] = Guid.NewGuid().ToString("N");
                            dtSource.Rows.InsertAt(dr, iIndex + 1);
                        }
                    }
                }
            }
        }

        private void tbReceiveRecordQuantity_MouseClick(object sender, MouseEventArgs e)
        {
            tbReceiveRecordQuantity.Text = "";
        }

        private void btnReverse_Click(object sender, EventArgs e)
        {
            string guid = dgvConfirmed.Rows[dgvConfirmed.CurrentCell.RowIndex].Cells["Guid"].Value.ToString();
            string status = dgvConfirmed.Rows[dgvConfirmed.CurrentCell.RowIndex].Cells["状态"].Value.ToString();
            List<string> sqlList = new List<string>();
            if (status == "已到货" || status == "部分入库")
            {
                string sqlUpdatePOStatus = @"Update PurchaseOrderRecordByCMF Set POStatus = 3 Where Guid='" + guid + "'";
                string sqlDeleteHistoryRecord = @"Delete From PurchaseOrderRecordHistoryByCMF Where ParentGuid='" + guid + "' And Status = 9";
                sqlList.Add(sqlUpdatePOStatus);
                sqlList.Add(sqlDeleteHistoryRecord);

                if (SQLHelper.BatchExecuteNonQuery(GlobalSpace.FSDBConnstr, sqlList))
                {
                    Custom.MsgEx("退回操作成功！");
                }
                else
                {
                    Custom.MsgEx("退回操作失败！");
                }
            }
            else
            {
                Custom.MsgEx("当前状态不允许进行此操作！");
            }
        }

        private void tbFONumber_Validated(object sender, EventArgs e)
        {

        }

        private void tbFONumber_TextChanged_1(object sender, EventArgs e)
        {
            tbFONumber.Text = tbFONumber.Text.ToUpper();
            tbFONumber.SelectionStart = tbFONumber.Text.Length;
        }

        private void tbFONumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!string.IsNullOrEmpty(tbFONumber.Text))
            {
                if (e.KeyChar == (char)13)
                {
                    dgvPODetail.DataSource = GetVendorPOItemsUnConfirmedDetail("FONumber", tbFONumber.Text, PurchaseUser.UserID);
                    dgvPODetail.Columns["Guid"].Visible = false;
                    tbFONumber.Text = "";
                }
            }
        }

        private void tbFONumberConfirmed_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (CommonOperate.TextBoxCheck(tbFONumberConfirmed, e))
            {
                if (e.KeyChar == (char)13)
                {
                    dgvConfirmed.DataSource = GetVendorPOItemsConfirmedDetail("FONumber", tbFONumberConfirmed.Text, PurchaseUser.UserID);
                    tbFONumberConfirmed.Text = "";
                }
            }
        }

        private void tbQualityStandard_Click(object sender, EventArgs e)
        {
            //tbQualityStandard.Text = "";
            //tbQualityStandard.ForeColor = Color.Black;
        }

        private void btnClosePOItem_Click(object sender, EventArgs e)
        {
            List<string> guidList = new List<string>();
            foreach (DataGridViewRow dgvr in dgvPODetail.Rows)
            {
                if (Convert.ToBoolean(dgvr.Cells["Check"].Value))
                {
                    guidList.Add(dgvr.Cells["Guid"].Value.ToString());
                }
            }

            if (CommonOperate.BatchUpdatePOItemStatusByGuid(guidList, 4))
            {
                Custom.MsgEx("关闭订单成功！");
            }
            else
            {
                Custom.MsgEx("关闭订单失败！");
            }
        }

        private void btnUpdateReceivedTotalQuantity_Click(object sender, EventArgs e)
        {
            List<string> guidList = new List<string>();
            List<string> sqlList = new List<string>();
            string lineNumber = string.Empty;
            string guid = string.Empty;
            double orderQuantity = 0;
            double receivedQuantity = 0;
            foreach (DataGridViewRow dgvr in dgvPODetail.Rows)
            {
                if (Convert.ToBoolean(dgvr.Cells["Check"].Value))
                {
                    PONumber= dgvr.Cells["采购单号"].Value.ToString().Trim().ToUpper();
                    lineNumber = Convert.ToInt32(dgvr.Cells["行号"].Value).ToString();
                    guid = dgvr.Cells["GUID"].Value.ToString();
                    orderQuantity = Convert.ToDouble(dgvr.Cells["订购数量"].Value);
                    #region 更新主体
                    receivedQuantity = GetReceivedTotalQuantity(PONumber, lineNumber, GlobalSpace.FSDBMRConnstr);
                    string sqlUpdate = @"Update PurchaseOrderRecordByCMF Set ActualReceiveQuantity = "+receivedQuantity+", POItemRemainedQuantity=" + (orderQuantity - receivedQuantity) + " Where Guid='" + guid + "'";
                    if (SQLHelper.ExecuteNonQuery(GlobalSpace.FSDBConnstr, sqlUpdate))
                    {
                        MessageBoxEx.Show("更新成功,只更新选中的第一条", "提示");
                        dgvr.Cells["实际到货数量"].Value = receivedQuantity;
                        dgvr.Cells["剩余到货数量"].Value = orderQuantity - receivedQuantity; 
                    }
                    else
                    {
                        MessageBoxEx.Show("更新失败！", "提示");
                    }
                    #endregion
                    
                    break;
                }
            }
            
        }
        //获取累计入库数量
        private double GetReceivedTotalQuantity(string poNumber, string lineNumber, string connStr)
        {
            double quantity = 0;
            string sqlSelect = @"SELECT
                                     TOP 1	TotalReceiptQuantity
                                    FROM
	                                    _NoLock_FS_HistoryPOReceipt
                                    WHERE
	                                    PONumber = '" + poNumber + "'  AND POLineNumber = '" + lineNumber + "' and TransactionDate >='"+DateTime.Now.AddYears(-2).ToString("yyyy-MM-dd")+"' ORDER BY HistoryPOReceiptKey DESC";
            DataTable dt = SQLHelper.GetDataTable(connStr, sqlSelect);
            if (dt.Rows.Count == 1)
            {
                quantity = Convert.ToDouble(dt.Rows[0]["TotalReceiptQuantity"]);
            }
            else
            {
                quantity = 0;
            }
            return quantity;
        }

        //获取订单中物料详情
        private DataTable GetVendorPOItemsDetail(string guid)
        {
            string strSql = @"SELECT
                                                T1.Id,
                                                T1.Guid AS GUID,
                                                T1.ParentGuid AS ParentGUID,
                                                T1.LineNumber AS 行号,
                                                T1.ForeignNumber AS 外贸单号,
	                                        	T1.ItemNumber AS 物料代码,
	                                            T1.ItemDescription AS 物料描述,
	                                            T1.LineUM AS 单位,	                                         
	                                            T1.UnitPrice AS 单价,
	                                            T1.POItemQuantity AS 订购数量,
                                                T1.POItemRemainedQuantity AS 剩余数量,
	                                            T1.DemandDeliveryDate AS 要求到货日期,	                     
                                                T1.StockKeeper AS 库管员,
                                                (case T1.POStatus 
                                                          when  '3' then '已下达'  
                                                          when  '4' then '已到货'                                                       
                                                        when '66' then '部分入库' 
                                                 end     
                                                ) as 状态
                                        FROM
	                                        PurchaseOrderRecordByCMF T1
                                        WHERE  T1.Guid = '"+guid+"' AND (T1.POStatus = 3 OR T1.POStatus = 66 )";
            return SQLHelper.GetDataTable(GlobalSpace.FSDBConnstr, strSql);
        }
    }
}
