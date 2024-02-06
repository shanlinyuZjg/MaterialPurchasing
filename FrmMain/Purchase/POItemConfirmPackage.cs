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
using Global;

namespace Global.Purchase
{
    public partial class POItemConfirmPackage : Office2007Form
    {
        string UserID = string.Empty;
        string UserName = string.Empty;
        string PONumber = string.Empty;
        string ItemNumber = string.Empty;
        string VendorName = string.Empty;
        string ForeignNumber = string.Empty;

        public POItemConfirmPackage(string id,string name)
        {
            this.EnableGlass = false;
            InitializeComponent();
            UserID = id;
            UserName = name;
            MessageBoxEx.EnableGlass = false;
        }

        private void tbItemNumberConfirmed_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (CommonOperate.TextBoxCheck(tbItemNumberConfirmed, e))
            {
                if (e.KeyChar == (char)13)
                {
                    dgvConfirmed.DataSource = GetVendorAllPOConfirmed("ItemNumber", tbItemNumberConfirmed.Text, UserID);
                    dgvConfirmed.Columns["Id"].Visible = false;
                    dgvConfirmed.Columns["Guid"].Visible = false;
                    tbItemNumberConfirmed.Text = "";
                }
            }
        }

        //获取订单，按照日期降序排序
        private DataTable GetVendorAllPOConfirmed(string strCriteria, string strValue, string userID)
        {
            string sqlSelect = @"SELECT
                                                T1.Id,T1.Guid,T1.ForeignNumber AS 联系单号,T1.RequireDept  AS 需求部门,Left(T1.ActualDeliveryDate,10) AS 实际到货日,T1.VendorNumber AS 供应商码,T1.VendorName AS 供应商名,T1.PONumber AS 采购单号,
                                                T1.LineNumber AS 行号,
	                                        	T1.ItemNumber AS 物料代码,
	                                            T1.ItemDescription AS 物料描述,
	                                            T1.LineUM AS 单位,	                                         
	                                            T1.UnitPrice AS 单价,
	                                            T1.POItemQuantity AS 订购数量,
	                                            T1.DemandDeliveryDate AS 要求到货日,
                                                T1.ActualDeliveryQuantity AS 实际到货数量,
                                                T1.StockKeeper AS 库管员,
                                                (case T1.POStatus 
                                                          when  '3' then '已下达'  
                                                          when  '4' then '已到货'                                                       
                                                        when '66' then '部分入库' 
                                                        when '5' then '已入库' 
                                                 end     
                                                ) as 状态 ,
                                                T1.AccumulatedActualReceiveQuantity AS 累计数量  ,
                                                T1.Comment1 AS 备注
                                        FROM
	                                        PurchaseOrderRecordByCMF T1
                                        WHERE  ";

            string sqlCriteria = string.Empty;
            int caseIndex = 0;
            switch (strCriteria)
            {
                case "ForeignNumber":
                    caseIndex = 1;
                    break;
                case "ItemNumber":
                    caseIndex = 2;
                    break;
                case "ItemDescription":
                    caseIndex = 3;
                    break;
                case "VendorNumber":
                    caseIndex = 4;
                    break;
                case "VendorName":
                    caseIndex = 5;
                    break;
            }
            switch (caseIndex)
            {
                case 1:
                    sqlCriteria = "ForeignNumber Like '%" + strValue + "%'   And POStatus > 3  ORDER BY  POItemPlacedDate DESC";
                    break;
                case 2:
                    sqlCriteria = "ItemNumber = '" + strValue + "' And  POStatus > 3     ORDER BY  POItemPlacedDate DESC";
                    break;
                case 3:
                    sqlCriteria = "ItemDescription Like '%" + strValue + "%'   And POStatus > 3  ORDER BY  POItemPlacedDate DESC";
                    break;
                case 4:
                    sqlCriteria = "VendorNumber = '" + strValue + "' And  POStatus > 3     ORDER BY  POItemPlacedDate DESC";
                    break;
                case 5:
                    sqlCriteria = "VendorName Like '%" + strValue + "%'   And POStatus > 3  ORDER BY  POItemPlacedDate DESC";
                    break;
            }
            return SQLHelper.GetDataTable(GlobalSpace.FSDBConnstr, sqlSelect + sqlCriteria);
        }

        private void tbFONumberConfirmed_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (CommonOperate.TextBoxCheck(tbFONumberConfirmed, e))
            {
                if (e.KeyChar == (char)13)
                {
                    dgvConfirmed.DataSource = GetVendorAllPOConfirmed("ForeignNumber", tbFONumberConfirmed.Text, UserID);
                    dgvConfirmed.Columns["Id"].Visible = false;
                    dgvConfirmed.Columns["Guid"].Visible = false;

                    tbFONumber.Text = tbFONumberConfirmed.Text;
                    dgvPO.DataSource = GetVendorAllPO("ForeignNumber", tbFONumber.Text, UserID);
                    dgvPO.Columns["下单日期"].Visible = false;

                    tbiFONumber.Text = tbFONumberConfirmed.Text;
                    dgvFOSpecialItem.DataSource = GetFOSpecialItem(tbiFONumber.Text);
                }
            }
        }

        private DataTable GetUnConfirmedPO(string userID,int months)
        {
            string sqlSelect = @"SELECT  Distinct
                                            PONumber AS 采购单号,
	                                        VendorName AS 供应商名,
	                                        POItemPlacedDate AS 下单日期
                                            FROM
	                                            PurchaseOrderRecordByCMF
                                            WHERE POStatus = 3 And POItemConfirmer = '" + userID + "' And POItemPlacedDate >='"+DateTime.Now.AddMonths(-months).ToString("yyyy-MM-dd")+ "' Order By POItemPlacedDate Desc";
            return SQLHelper.GetDataTable(GlobalSpace.FSDBConnstr, sqlSelect);
        }

        private DataTable GetUnConfirmedPOTimes(string userID)
        {
            string sqlSelect = @"SELECT  Distinct
                                            PONumber AS 采购单号,
	                                        VendorName AS 供应商名,
	                                        POItemPlacedDate AS 下单日期
                                            FROM
	                                            PurchaseOrderRecordByCMF
                                            WHERE POStatus = 3 And POItemConfirmer = '" + userID + "' ";
            return SQLHelper.GetDataTable(GlobalSpace.FSDBConnstr, sqlSelect);
        }

        private void tbItemNumberOrDescription_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (CommonOperate.TextBoxCheck(tbItemNumberOrDescription, e))
            {
                if (e.KeyChar == (char)13)
                {
                    dgvPO.DataSource = GetVendorAllPO("ItemNumber", tbItemNumberOrDescription.Text, UserID);
                    dgvPO.Columns["下单日期"].Visible = false;
                }
                tbItemNumberOrDescription.Text = "";
            }
        }

        private void tbFONumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (CommonOperate.TextBoxCheck(tbFONumber, e))
            {
                if (e.KeyChar == (char)13)
                {
                    dgvPO.DataSource = GetVendorAllPO("ForeignNumber", tbFONumber.Text, UserID);
                    dgvPO.Columns["下单日期"].Visible = false;

                    tbFONumberConfirmed.Text = tbFONumber.Text;
                    dgvConfirmed.DataSource = GetVendorAllPOConfirmed("ForeignNumber", tbFONumberConfirmed.Text, UserID);
                    dgvConfirmed.Columns["Id"].Visible = false;
                    dgvConfirmed.Columns["Guid"].Visible = false;

                    tbiFONumber.Text = tbFONumber.Text;
                    dgvFOSpecialItem.DataSource = GetFOSpecialItem(tbiFONumber.Text);
                }
            }
        }

        //获取订单，按照日期降序排序
        private DataTable GetVendorAllPO(string strCriteria, string strValue, string userID)
        {
            string sqlSelect = @"SELECT DISTINCT
                                            TOP 500 PONumber AS 采购单号,
	                                        VendorName AS 供应商名,
	                                        POItemPlacedDate AS 下单日期
                                            FROM
	                                            PurchaseOrderRecordByCMF
                                            WHERE  ";

            string sqlCriteria = string.Empty;
            int caseIndex = 0;
            switch (strCriteria)
            {
                case "ForeignNumber":
                    caseIndex = 1;
                    break;
                case "ItemNumber":
                    caseIndex = 2;
                    break;
                case "VendorName":
                    caseIndex = 3;
                    break;
                case "ItemDescription":
                    caseIndex = 4;
                    break;
            }
            switch (caseIndex)
            {
                case 1:
                    sqlCriteria = "ForeignNumber Like '%" + strValue + "%'   And (POStatus = 66 OR POStatus = 3)   ORDER BY  POItemPlacedDate DESC";
                    break;
                case 2:
                    sqlCriteria = "ItemNumber = '" + strValue + "' And (POStatus = 66 OR POStatus = 3)     ORDER BY  POItemPlacedDate DESC";
                    break;
                case 3:
                    sqlCriteria = "VendorName Like '%" + strValue + "%'  And (POStatus = 66 OR POStatus = 3)     ORDER BY  POItemPlacedDate DESC";
                    break;
                case 4:
                    sqlCriteria = "ItemDescription Like '%" + strValue + "%'  And (POStatus = 66 OR POStatus = 3)     ORDER BY  POItemPlacedDate DESC";
                    break;
            }
            if (string.IsNullOrWhiteSpace(cbbBuyers.Text))
            {
                return SQLHelper.GetDataTable(GlobalSpace.FSDBConnstr, sqlSelect + sqlCriteria);
            }
            else
            {
                return SQLHelper.GetDataTable(GlobalSpace.FSDBConnstr, sqlSelect +"Buyer ='"+cbbBuyers.Text.Split('|')[0]+"'  and "+ sqlCriteria);
            }

        }
        //获取订单，按照日期降序排序
        private DataTable GetVendorAllPOTimes(string strCriteria, string strValue, string userID)
        {
            string sqlSelect = @"SELECT DISTINCT
                                            PONumber AS 采购单号,
	                                        VendorName AS 供应商名,
	                                        POItemPlacedDate AS 下单日期
                                            FROM
	                                            PurchaseOrderRecordByCMF
                                            WHERE  ";
            string sqlCriteria = string.Empty;
            switch (strCriteria)
            {
                case "ForeignNumber":
                    sqlCriteria = "ForeignNumber = '" + strValue + "  And  POStatus = 66  ORDER BY  POItemPlacedDate DESC";
                    break;
                case "ItemNumber":
                    sqlCriteria = "ItemNumber = '" + strValue + "' And POStatus = 66     ORDER BY  POItemPlacedDate DESC";
                    break;
            }
            return SQLHelper.GetDataTable(GlobalSpace.FSDBConnstr, sqlSelect + sqlCriteria);
        }

        private void btnMakeAllChecked2_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dgvPODetail.Rows.Count; i++)
            {
                dgvPODetail.Rows[i].Cells["PORVSeveralTimes"].Value = true;
            }
        }

        private void btnMakeAllCheckedCanceled_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dgvPODetail.Rows.Count; i++)
            {
                dgvPODetail.Rows[i].Cells["PORVSeveralTimes"].Value = false;
            }
        }

        private void btnConfirmAll_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(tbQualityStandard.Text.Trim()))
            {
                Custom.MsgEx("检验标准不能为空！");
                return;
            }
            List<string> guidList = new List<string>();
            foreach (DataGridViewRow dgvr in dgvPODetail.Rows)
            {
                if (Convert.ToBoolean(dgvr.Cells["PORVSeveralTimes"].Value))
                {
                    guidList.Add(dgvr.Cells["GUID"].Value.ToString());
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

        private bool CreatePOItemReceiveHistory(List<string> guidList)
        {
            List<string> sqlList = new List<string>();
            for (int i = 0; i < guidList.Count; i++)
            {
                string guid = Guid.NewGuid().ToString("N");
                string sqlInsert = $@"INSERT INTO PurchaseOrderRecordHistoryByCMF (
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
	                                                            [BuyerID],Stock,Bin,ReceiveDate,Guid,IsFOItem,UnitPrice,GSID,QualityCheckStandard,IsInvestigation
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
	                                                        [ForeignNumber],Buyer,Stock,Bin,Left(ActualDeliveryDate,10),Replace(NEWID(),'-',''),IsFOItem,UnitPrice,GSID,'{tbQualityStandard.Text.Trim()}',IsInvestigation  FROM   PurchaseOrderRecordByCMF  WHERE Guid = '{guidList[i]}'";
                //       MessageBox.Show(sqlInsert);
                sqlList.Add(sqlInsert);
            }

            if (SQLHelper.BatchExecuteNonQuery(GlobalSpace.FSDBConnstr, sqlList))
            {
                return true;
            }
            return false;
        }

        private void dgvPO_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                if (dgvPODetail.Rows.Count > 0)   //清空dgvPODetail
                {
                    DataTable dt = (DataTable)dgvPODetail.DataSource;
                    dt.Rows.Clear();
                    dgvPODetail.DataSource = dt;
                }

                string poNumber = dgvPO.Rows[e.RowIndex].Cells["采购单号"].Value.ToString();
                PONumber = poNumber;
                dgvPODetail.DataSource = GetVendorPOItemsDetail(PONumber, UserID,tbFONumber.Text);
                dgvPODetail.Columns["GUID"].Visible = false;
                dgvPODetail.Columns["Id"].Visible = false;
                dgvPODetail.Columns["ParentGuid"].Visible = false;
            }
        }

        private void dgvPO_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            dgvPO_CellDoubleClick(sender, e);
        }

        private void superTabControlPanel1_Click(object sender, EventArgs e)
        {

        }

        private void btnRefreshTimes_Click(object sender, EventArgs e)
        {
           // dgvPOTimes.DataSource = GetUnConfirmedPOTimes(UserID);
        }


        //获取订单中物料详情
        private DataTable GetVendorPOItemsDetailTimes(string strValue, string userID)
        {
            string strSql = @"SELECT
                                                T1.Id,
                                                T1.Guid AS GUID,
                                                T1.ParentGuid AS ParentGUID,
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
                                                        when '66' then '部分入库' 
                                                        when '67' then '已完成' 
                                                 end     
                                                ) as 状态 ,
                                                T1.AccumulatedActualReceiveQuantity AS 累计数量 
                                        FROM
	                                        PurchaseOrderRecordByCMF T1
                                        WHERE  ";
            string sqlCriteria = "T1.PONumber = '" + strValue + "' And T1.POStatus = 66";

            return SQLHelper.GetDataTable(GlobalSpace.FSDBConnstr, strSql + sqlCriteria);
        }
        //获取订单中物料详情
        private DataTable GetVendorPOItemsDetail(string strValue, string userID,string foNumber)
        {
            string sqlCriteria = string.Empty;
            string strSql = @"SELECT
                                                T1.Id,
                                                T1.Guid AS GUID,
                                                T1.ParentGuid AS ParentGUID,
                                                T1.LineNumber AS 行号,
                                                T1.ForeignNumber AS 外贸单号,
                                                T1.RequireDept  AS 需求部门,
	                                        	T1.ItemNumber AS 物料代码,
	                                            T1.ItemDescription AS 物料描述,
	                                            T1.LineUM AS 单位,	                                         
	                                            T1.UnitPrice AS 单价,
	                                            T1.POItemQuantity AS 订购数量,
                                                T1.QualityCheckStandard AS 请验标准,
	                                            T1.DemandDeliveryDate AS 要求到货日,
	                                            
                                                T1.ActualDeliveryQuantity AS 实际到货数量,
                                                T1.StockKeeper AS 库管员,
                                                (case T1.POStatus 
                                                          when  '3' then '已下达'  
                                                          when  '4' then '已到货'                                                       
                                                        when '66' then '部分入库' 
                                                 end     
                                                ) as 状态 ,
                                                T1.AccumulatedActualReceiveQuantity AS 累计接收数量 ,
                                                T1.POItemRemainedQuantity AS 剩余数量,
                                                T1.Comment1 AS 备注
                                        FROM
	                                        PurchaseOrderRecordByCMF T1
                                        WHERE  T1.PONumber = '" + strValue + "' And (T1.POStatus = 3 OR T1.POStatus = 66) ";
            if(string.IsNullOrEmpty(tbFONumber.Text))
            {
                sqlCriteria = "  And 1 =1 ";
            }
            else
            {
                sqlCriteria = " And ForeignNumber like '%" + foNumber + "%'";
            }
            return SQLHelper.GetDataTable(GlobalSpace.FSDBConnstr, strSql + sqlCriteria);
        }
        private void dgvPOTimes_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            //dgvPOTimes_CellDoubleClick(sender, e);
        }
        /*
        private void dgvPOTimes_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                if (dgvPODetailTimes.Rows.Count > 0)
                {
                    DataTable dt = (DataTable)dgvPODetailTimes.DataSource;
                    dt.Rows.Clear();
                    dgvPODetailTimes.DataSource = dt;
                }

                string poNumber = dgvPOTimes.Rows[e.RowIndex].Cells["采购单号"].Value.ToString();
                PONumber = poNumber;
                dgvPODetailTimes.DataSource = GetVendorPOItemsDetailTimes(PONumber, UserID);
                dgvPODetailTimes.Columns["GUID"].Visible = false;
                dgvPODetailTimes.Columns["ParentGUID"].Visible = false;
                dgvPODetailTimes.Columns["Id"].Visible = false;

            }
        }
        */
        private void btnFinishPO_Click(object sender, EventArgs e)
        {
            List<string> guidList = new List<string>();
            foreach (DataGridViewRow dgvr in dgvPODetail.Rows)
            {
                if (Convert.ToBoolean(dgvr.Cells["PORVSeveralTimes"].Value))
                {
                    guidList.Add(dgvr.Cells["GUID"].Value.ToString());
                }
            }

            if (CommonOperate.BatchUpdatePOItemStatusByGuid(guidList, 67))
            {
                Custom.MsgEx("更新成功！");
            }
            else
            {
                Custom.MsgEx("更新失败！");
            }
        }

        private void POItemConfirm2_Load(object sender, EventArgs e)
        {
            dgvPO.DataSource = GetUnConfirmedPO(UserID,3);
        //    dgvPOTimes.DataSource = GetUnConfirmedPOTimes(UserID);
            dgvFOSpecialItem.DataSource = GetFOSpecialItem("");
            GetcbbBuyers();
        }

        private void GetcbbBuyers()
        {
            cbbBuyers.Items.Clear();
            DataTable dt= SQLHelper.GetDataTable(GlobalSpace.FSDBConnstr, "SELECT UserID+'|'+Name  FROM [dbo].[PurchaseDepartmentRBACByCMF] where PurchaseType='P' and (POType ='D' or POType ='F')");
            foreach (DataRow dr in dt.Rows)
            {
                cbbBuyers.Items.Add(dr[0]);
            }
        }

        private DataTable GetFOSpecialItem(string foNumber)
        {
            string sqlCriteria = string.Empty;
            string sqlSelectItemDetail = @"SELECT               OperateDateTime 提交日期,
	                                                            ForeignOrderNumber AS 联系单号,
	                                                            ItemNumber AS 物料代码,
	                                                            ItemDescription AS 物料描述,
	                                                            VendorNumber AS 供应商码,
                                                                VendorName AS 供应商名,
	                                                            PurchasePrice AS 采购价格,
	                                                            ItemUM AS 单位,
	                                                            Quantity AS 数量,
	                                                            SpecificationDescription AS 说明,
	                                                            TotalAmount AS 总金额,
	                                                            Requirements AS 要求
                                                            FROM
	                                                            PurchaseDepartmentForeignOrderItemByCMF  Where IsValid = 1 And Status = 3  And ItemNumber  IN (Select ItemNumber From PurchaseDepartmentForeignOrderItemNotInByCMF)   ";
            if(string.IsNullOrEmpty(foNumber))
            {
                sqlCriteria = " And 1 =1 ";
            }
            else
            {
                sqlCriteria = " And ForeignOrderNumber like '%"+foNumber+"%'  ";
            }
            return SQLHelper.GetDataTable(GlobalSpace.FSDBConnstr, sqlSelectItemDetail+sqlCriteria+ " Order By Id Desc");
        }
        private DataTable GetFOSpecialItemByVN(string VendorName)
        {
            string sqlCriteria = string.Empty;
            string sqlSelectItemDetail = @"SELECT               OperateDateTime 提交日期,
	                                                            ForeignOrderNumber AS 联系单号,
	                                                            ItemNumber AS 物料代码,
	                                                            ItemDescription AS 物料描述,
	                                                            VendorNumber AS 供应商码,
                                                                VendorName AS 供应商名,
	                                                            PurchasePrice AS 采购价格,
	                                                            ItemUM AS 单位,
	                                                            Quantity AS 数量,
	                                                            SpecificationDescription AS 说明,
	                                                            TotalAmount AS 总金额,
	                                                            Requirements AS 要求 
                                                            FROM
	                                                            PurchaseDepartmentForeignOrderItemByCMF  Where IsValid = 1 And Status = 3  And ItemNumber  IN (Select ItemNumber From PurchaseDepartmentForeignOrderItemNotInByCMF)   ";
                sqlCriteria = " And VendorName like '%" + VendorName + "%'  ";
            
            return SQLHelper.GetDataTable(GlobalSpace.FSDBConnstr, sqlSelectItemDetail + sqlCriteria + " Order By Id Desc");
        }
        private DataTable GetFOSpecialItemForSearch()
        {
            string sqlSelectItemDetail = @"SELECT
	                                                            ForeignOrderNumber AS 联系单号,
	                                                            ItemNumber AS 物料代码,
	                                                            ItemDescription AS 物料描述,
	                                                            VendorNumber AS 供应商码,
                                                                VendorName AS 供应商名,
	                                                            PurchasePrice AS 采购价格,
	                                                            ItemUM AS 单位,
	                                                            Quantity AS 数量,
	                                                            SpecificationDescription AS 说明,
	                                                            TotalAmount AS 总金额,
	                                                            Requirements AS 要求
                                                            FROM
	                                                            PurchaseDepartmentForeignOrderItemByCMF  Where IsValid = 1 And Status = 3  And ItemNumber  IN (Select ItemNumber From PurchaseDepartmentForeignOrderItemNotInByCMF)  Order By Id Desc";
            return SQLHelper.GetDataTable(GlobalSpace.FSDBConnstr, sqlSelectItemDetail);
        }
     

        private void tbItemNumberOrDescription_TextChanged(object sender, EventArgs e)
        {
            tbItemNumberOrDescription.Text = tbItemNumberOrDescription.Text.ToUpper();
            tbItemNumberOrDescription.SelectionStart = tbItemNumberOrDescription.Text.Length;
        }

        private void tbFONumber_TextChanged(object sender, EventArgs e)
        {
            tbFONumber.Text = tbFONumber.Text.ToUpper();
            tbFONumber.SelectionStart = tbFONumber.Text.Length;
        }

        private void tbItemNumberConfirmed_TextChanged(object sender, EventArgs e)
        {
            tbItemNumberConfirmed.Text = tbItemNumberConfirmed.Text.ToUpper();
            tbItemNumberConfirmed.SelectionStart = tbItemNumberConfirmed.Text.Length;
        }

        private void tbFONumberConfirmed_TextChanged(object sender, EventArgs e)
        {
            tbFONumberConfirmed.Text = tbFONumberConfirmed.Text.ToUpper();
            tbFONumberConfirmed.SelectionStart = tbFONumberConfirmed.Text.Length;
        }

        private void btniRefresh_Click(object sender, EventArgs e)
        {
            dgvFOSpecialItem.DataSource = GetFOSpecialItem("");
        }

        private void tbVendorName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (CommonOperate.TextBoxCheck(tbVendorName, e))
            {
                if (e.KeyChar == (char)13)
                {
                    dgvPO.DataSource = GetVendorAllPO("VendorName", tbVendorName.Text, UserID);
                    dgvPO.Columns["下单日期"].Visible = false;
                    tbVendorName.Text = "";
                }
            }
        }

     

        private void rbtn3Months_CheckedChanged(object sender, EventArgs e)
        {
            if(rbtn3Months.Checked)
            {
                dgvPO.DataSource = GetUnConfirmedPO(UserID, 3);
            }
        }

        private void rbtn6Months_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtn6Months.Checked)
            {
                dgvPO.DataSource = GetUnConfirmedPO(UserID, 6);
            }
        }

        private void rbtn12Months_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtn12Months.Checked)
            {
                dgvPO.DataSource = GetUnConfirmedPO(UserID,12);
            }
        }
        private void tbiFONumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!String.IsNullOrWhiteSpace(tbiFONumber.Text))
            {
                if (e.KeyChar == (char)13)
                {
                    tbiFONumber.Text = tbiFONumber.Text.Trim().ToUpper();
                    tbiFONumber.SelectionStart = tbiFONumber.Text.Length;
                    dgvFOSpecialItem.DataSource = GetFOSpecialItem(tbiFONumber.Text);

                    tbFONumberConfirmed.Text = tbiFONumber.Text;
                    dgvConfirmed.DataSource = GetVendorAllPOConfirmed("ForeignNumber", tbFONumberConfirmed.Text, UserID);
                    dgvConfirmed.Columns["Id"].Visible = false;
                    dgvConfirmed.Columns["Guid"].Visible = false;

                    tbFONumber.Text = tbiFONumber.Text;
                    dgvPO.DataSource = GetVendorAllPO("ForeignNumber", tbFONumber.Text, UserID);
                    dgvPO.Columns["下单日期"].Visible = false;
                }
            }
        }

        private void btnConfirmSeveralTimes_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(tbQualityStandard.Text.Trim()))
            {
                Custom.MsgEx("检验标准不能为空！");
                return;
            }
            List<string> guidList = new List<string>();
            foreach (DataGridViewRow dgvr in dgvPODetail.Rows)
            {
                if (Convert.ToBoolean(dgvr.Cells["PORVSeveralTimes"].Value))
                {
                    guidList.Add(dgvr.Cells["GUID"].Value.ToString());
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

        private void tbReceiveRecordQuantity_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!string.IsNullOrEmpty(tbReceiveRecordQuantity.Text))
            {
                if (e.KeyChar == (char)13)
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
                        for (int i = 0; i < iCount - 1; i++)
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

        private void tbItemDescription_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (CommonOperate.TextBoxCheck(tbItemDescription, e))
            {
                if (e.KeyChar == (char)13)
                {
                    dgvPO.DataSource = GetVendorAllPO("ItemDescription", tbItemDescription.Text, UserID);
                    dgvPO.Columns["下单日期"].Visible = false;
                    tbItemDescriptionConfirmed.Text = tbFONumber.Text;
                    dgvConfirmed.DataSource = GetVendorAllPOConfirmed("ItemDescription", tbItemDescriptionConfirmed.Text, UserID);
                    dgvConfirmed.Columns["Id"].Visible = false;     
                }
            }
        }

        private void tbVendorNumberConfirmed_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (CommonOperate.TextBoxCheck(tbVendorNumberConfirmed, e))
            {
                if (e.KeyChar == (char)13)
                {
                    dgvConfirmed.DataSource = GetVendorAllPOConfirmed("VendorNumber", tbVendorNumberConfirmed.Text, UserID);
                    dgvConfirmed.Columns["Id"].Visible = false;
                    dgvConfirmed.Columns["Guid"].Visible = false;
                    tbVendorNumberConfirmed.Text = "";

                }
            }
        }

        private void tbVendorNameConfirmed_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (CommonOperate.TextBoxCheck(tbVendorNameConfirmed, e))
            {
                if (e.KeyChar == (char)13)
                {
                    dgvConfirmed.DataSource = GetVendorAllPOConfirmed("VendorName", tbVendorNameConfirmed.Text, UserID);
                    dgvConfirmed.Columns["Id"].Visible = false;
                    dgvConfirmed.Columns["Guid"].Visible = false;
                    tbVendorNameConfirmed.Text = "";
                }
            }
        }

        private void btnClosePOItem_Click(object sender, EventArgs e)
        {
            List<string> guidList = new List<string>();
            foreach (DataGridViewRow dgvr in dgvPODetail.Rows)
            {
                if (Convert.ToBoolean(dgvr.Cells["PORVSeveralTimes"].Value))
                {
                    guidList.Add(dgvr.Cells["GUID"].Value.ToString());
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

        private void tbQualityStandard_Click(object sender, EventArgs e)
        {
            //tbQualityStandard.Text = "";
            //tbQualityStandard.ForeColor = Color.Black;
        }

        private void btnUpdateReceivedTotalQuantity_Click(object sender, EventArgs e)
        {
            List<string> guidList = new List<string>();
            List<string> sqlList = new List<string>();
            //int iCount = 0;
            string lineNumber = string.Empty;
            string Id = string.Empty;
            double orderQuantity = 0;
            double receivedQuantity = 0;
            int rowIndex = -1;
            foreach (DataGridViewRow dgvr in dgvPODetail.Rows)
            {
                if (Convert.ToBoolean(dgvr.Cells["PORVSeveralTimes"].Value))
                {
                    //iCount++;
                    lineNumber = Convert.ToInt32(dgvr.Cells["行号"].Value).ToString();
                    Id = dgvr.Cells["Id"].Value.ToString();
                    orderQuantity = Convert.ToDouble(dgvr.Cells["订购数量"].Value);
                    rowIndex = dgvr.Index;
                    //
                    receivedQuantity = GetReceivedTotalQuantity(PONumber, lineNumber, GlobalSpace.FSDBMRConnstr);
                    if (receivedQuantity == -1)
                    {
                        MessageBoxEx.Show($@"行号{lineNumber}未从四班查询到数据！", "提示");
                        continue;
                    }
                    string sqlUpdate = @"Update PurchaseOrderRecordByCMF Set POItemRemainedQuantity=" + (orderQuantity - receivedQuantity) + ", AccumulatedActualReceiveQuantity= " + receivedQuantity + " Where Id='" + Id + "'";
                    if (SQLHelper.ExecuteNonQuery(GlobalSpace.FSDBConnstr, sqlUpdate))
                    {

                        //if (lineNumber.Length == 1)
                        //{
                        //    lineNumber = "00" + lineNumber;
                        //}
                        //else if (lineNumber.Length == 2)
                        //{
                        //    lineNumber = "0" + lineNumber;
                        //}
                        //dgvPODetail.DataSource = GetVendorPOItemsDetail(PONumber, lineNumber);
                        //dgvPODetail.Columns["GUID"].Visible = false;
                        //dgvPODetail.Columns["Id"].Visible = false;
                        //dgvPODetail.Columns["ParentGuid"].Visible = false;
                        //MessageBox.Show(rowIndex.ToString());
                        dgvPODetail["累计接收数量", rowIndex].Value = receivedQuantity;
                        dgvPODetail["剩余数量", rowIndex].Value = orderQuantity - receivedQuantity;
                        //MessageBoxEx.Show("更新成功！", "提示");
                    }
                    else
                    {
                        MessageBoxEx.Show($@"行号{lineNumber}更新失败！", "提示");
                    }
                }
            }
            MessageBoxEx.Show("更新完成！", "提示");

        }
        //获取累计入库数量
        private double GetReceivedTotalQuantity(string poNumber, string lineNumber, string connStr)
        {
            double quantity = 0;
            string sqlSelect = $@"SELECT B.ReceiptQuantity FROM [dbo].[_NoLock_FS_POLine] as B INNER JOIN [dbo].[_NoLock_FS_POHeader] as A on A.POHeaderKey=B.POHeaderKey  where A.PONumber ='{poNumber}' and B.POLineNumber = '{lineNumber}'";
            DataTable dt = SQLHelper.GetDataTable(connStr, sqlSelect);
            if (dt.Rows.Count == 1)
            {
                quantity = Convert.ToDouble(dt.Rows[0]["ReceiptQuantity"]);
            }
            else
            {
                quantity = -1;
                //-1表示未从四班查到数据！
            }
            return quantity;
        }

        //获取订单中物料详情
        private DataTable GetVendorPOItemsDetail(string poNumber, string lineNumber)
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
                                        WHERE  T1.PONumber = '" + poNumber + "' And T1.LineNumber='" + lineNumber + "' AND (T1.POStatus = 3 OR T1.POStatus = 66 )";
            return SQLHelper.GetDataTable(GlobalSpace.FSDBConnstr, strSql);
        }

        private void BtnFoSpecialItem_Click(object sender, EventArgs e)
        {
            dgvFOSpecialItem.DataSource = GetFOSpecialItem("");
        }

        private void tbiVendorName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                tbiVendorName.Text = tbiVendorName.Text.Trim();
                dgvFOSpecialItem.DataSource = GetFOSpecialItemByVN(tbiVendorName.Text);
            }
        }

        private void tbItemDescriptionConfirmed_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(tbItemDescriptionConfirmed.Text))
            {
                if (e.KeyChar == (char)13)
                {
                    dgvConfirmed.DataSource = GetVendorAllPOConfirmed("ItemDescription", tbItemDescriptionConfirmed.Text.Trim(), UserID);
                    dgvConfirmed.Columns["Id"].Visible = false;
                    dgvConfirmed.Columns["Guid"].Visible = false;
                    tbItemDescriptionConfirmed.Text = "";
                }
            }
        }

        private void BtPurchaseOrderInfo_Click(object sender, EventArgs e)
        {
            PurchaseOrderInfo purchaseOrderInfo = new PurchaseOrderInfo();
            purchaseOrderInfo.ShowDialog();
        }
    }
}
