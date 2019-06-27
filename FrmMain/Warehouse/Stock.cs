using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using SoftBrands.FourthShift.Transaction;
using Global.Helper;
using System.Data.SqlClient;

namespace Global.Warehouse
{
    public partial class Stock : Office2007Form
    {
        string fsuserid = string.Empty;
        string fspassword = string.Empty;
        string username = string.Empty;
        string itemType = string.Empty;
        string PONumber = string.Empty;
        string PONumberForFS = string.Empty;
        string PONumberForPrint = string.Empty;
        DataTable dtPORV = null;
        DataTable dtPORVX = null;
        public Stock()
        {
            InitializeComponent();
        }

        public Stock(string uid, string password,string name)
        {
            fsuserid = uid;
            fspassword = password;
            username = name;
            MessageBoxEx.EnableGlass = false;
            this.EnableGlass = false;
            InitializeComponent();
        }

        private void Stock_Load(object sender, EventArgs e)
        {
            dgvPODetailFS.AutoGenerateColumns = true;
            dgvPODetail.AutoGenerateColumns = true;

            if(IsFSPerson(fsuserid))
            {
                string sqlSelect = @"Select Email From PurchaseDepartmentRBACByCMF Where UserID='" + fsuserid + "'";
                itemType = SQLHelper.GetDataTable(GlobalSpace.FSDBConnstr, sqlSelect).Rows[0]["Email"].ToString();
                this.superTabItemPORV.Visible = false;
                this.superTabItemFS.Visible = true;
                this.superTabItemPrintFSRecord.Visible = true;
                GetStockHandledPOForFS(itemType);
            }
            else
            {
                this.superTabItemPORV.Visible = true;
                this.superTabItemFS.Visible = false;
                GetStockUnHandledPO(fsuserid);
            }

            
      //      
     //       GetPONumberListForPrint(fsuserid);
     //       GetUnHandledReturnedItem(fsuserid);
            //弹出的对话框采用Office2007样式
                      
        }

        private bool IsFSPerson(string uid)
        {
            string sqlSelect = @"Select Count(Id) From PurchaseDepartmentRBACByCMF Where UserID='"+uid+"'";
            if(SQLHelper.Exist(GlobalSpace.FSDBConnstr,sqlSelect))
            {
                return true;
            }
            return false;
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
	                                                LEFT (StockKeeper, 3) = '"+fsuserid+"' OR LEFT (StockKeeper, 2) = 'CX' ) ORDER BY OperateDateTime DESC";
            dtPORVX = SQLHelper.GetDataTable(GlobalSpace.FSDBConnstr, sqlSelect);
            dgvItemReturnedUnHandledRecod.DataSource = SQLHelper.GetDataTable(GlobalSpace.FSDBConnstr, sqlSelect);
            //出选择列外，其余所有列为只读，避免人员误改动信息
            for (int i = 1; i <= dgvItemReturnedUnHandledRecod.Columns.Count - 1; i++)
            {
                dgvItemReturnedUnHandledRecod.Columns[i].ReadOnly = true;
            }
            dgvItemReturnedUnHandledRecod.Columns["Id"].Visible = false;
        }

        //加载可以打印的物料订单详情
        private void GetPONumberListForPrint(string keeperName)
        {
            string sqlSelect = @"SELECT
	                                DISTINCT PONumber AS 采购单号
                                FROM
	                                PurchaseOrderRecordByCMF
                                WHERE
	                                POStatus = 6
                                AND LEFT (StockKeeper, 3) = '" + keeperName + "'";

            dgvPOPrint.DataSource = SQLHelper.GetDataTable(GlobalSpace.FSDBConnstr, sqlSelect);
        }

        //DGV控件中加载仓库未处理订单
        private void GetStockUnHandledPO(string keeperName)
        {
            string sqlSelect = @"SELECT
	                                DISTINCT PONumber AS 采购单号
                                FROM
	                                PurchaseOrderRecordByCMF
                                WHERE
	                                POStatus = '4'
                                AND (LEFT (StockKeeper, 3) = '" + keeperName + "' OR LEFT (StockKeeper, 3) ='CX')";
            
            dgvPO.DataSource = SQLHelper.GetDataTable(GlobalSpace.FSDBConnstr, sqlSelect);

        }

        //DGV控件加载仓库已经处理的准备写入四班的订单
        private void GetStockHandledPOForFS(string type)
        {
            string sqlSelect = @"SELECT
	                                DISTINCT PONumber AS 采购单号
                                FROM
	                                PurchaseOrderRecordByCMF
                                WHERE
	                                POStatus = '5'
                                AND LEFT (ItemNumber, 1) = '" + type + "'";
           
            dgvPOForFS.DataSource = SQLHelper.GetDataTable(GlobalSpace.FSDBConnstr, sqlSelect);
        }

        private void dgvPO_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            string poNumber = string.Empty;
            if (e.RowIndex < 0)
            {
                MessageBoxEx.Show("请选择有效的行！", "提示");
            }
            else
            {
                poNumber = dgvPO["采购单号", e.RowIndex].Value.ToString();
                PONumber = poNumber;

                dgvPODetail.DataSource = GetVendorPOItemsDetail(poNumber,4,fsuserid);
                dgvPODetail.Columns["Id"].Visible = false;
                dgvPODetail.Columns["Guid"].Visible = false;
                dgvPODetail.Columns["NewPORVRecord"].Visible = false;
                dgvPODetail.Columns["ParentGuid"].Visible = false;
                dgvPODetail.Columns["库管员"].Width = 80;
            }
        }



        private void dgvPO_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            dgvPO_CellContentDoubleClick(sender, e);
        }

        //通过订单号获取订单中物料详情
        private DataTable GetVendorPOItemsDetail(string ponumber,int status,string keepercode)
        {
            DataTable dtTemp = null;
            string date = DateTime.Now.ToString("yyMMdd");
            
            string strSqlCheck = @"Select Id From  PurchaseOrderRecordByCMF Where PONumber='" + ponumber + "'";
            string strSql = @"SELECT
                                                T1.Id,
                                                T1.Guid,
                                                '' AS ParentGuid,
                                                0 AS NewPorvRecord,
                                                T1.LineNumber AS 行号,
	                                        	T1.ItemNumber AS 物料代码,
	                                            T1.ItemDescription AS 描述,
	                                            T1.LineUM AS 单位,
                                                T1.ManufacturerNumber AS 生产商码,
	                                            T1.ManufacturerName AS 生产商名称,
	                                            T1.UnitPrice AS 单价,
	                                            T1.POItemQuantity AS 订购数量,
	                                            T1.DemandDeliveryDate AS 需求日期,
                                                T1.StockKeeper AS 库管员,
                                                 (case T1.POStatus when  '1' then '已提交'
                                                         when  '2' then '已审核'
                                                         when  '3' then '已下达' 
                                                        when  '4' then '已到货' 
                                                        when  '5' then '已收货' 
                                                        when  '6' then '已入库' 
                                                        when  '7' then '已开票' 
                                                 end     
                                                )  AS 订单状态,
	                                            T1.ForeignNumber AS 外贸单号, T1.Stock AS 库,T1.Bin AS 位,T1.InspectionPeriod AS 检验,
                                                T1.POItemQuantity AS 送货数量, 
                                                T1.POItemQuantity AS 入库数量,
                                                T1.InternalLotNumber AS 公司批号,
                                               T1.LotNumber AS 批号, T1.ExpiredDate AS 到期日期      FROM  PurchaseOrderRecordByCMF T1 WHERE T1.PONumber = '" + ponumber + "' AND T1.POStatus = "+status+ " And (Left(T1.StockKeeper,3)='"+keepercode+ "'  OR  Left(T1.StockKeeper,2) = 'CX' )  Order By T1.LineNumber ASC";           

            if (SQLHelper.Exist(GlobalSpace.FSDBConnstr, strSqlCheck))
            {
                dtTemp = SQLHelper.GetDataTable(GlobalSpace.FSDBConnstr, strSql);
            }
            else
            {
                MessageBoxEx.Show("该订单不存在！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            return dtTemp;
        }

        private void btnMakeAllChecked_Click(object sender, EventArgs e)
        {
            if (dgvPODetail.Rows.Count > 0)
            {
                for (int i = 0; i < dgvPODetail.Rows.Count; i++)
                {
                    dgvPODetail.Rows[i].Cells["Check"].Value = true;
                }
            }
        }

        private void btnMakeAllUnChecked_Click(object sender, EventArgs e)
        {
            if (dgvPODetail.Rows.Count > 0)
            {
                for (int i = 0; i < dgvPODetail.Rows.Count; i++)
                {
                    dgvPODetail.Rows[i].Cells["Check"].Value = false;
                }
            }
        }

        private void btnPORV_Click(object sender, EventArgs e)
        {
            List<string> guidList =  new List<string>();
            List<string> sqlInsertList = new List<string>();
            List<string> sqlUpdateList = new List<string>();
            string parentGuid = string.Empty;
            
            foreach(DataGridViewRow dgvr in dgvPODetail.Rows)
            {
                if(Convert.ToInt32(dgvr.Cells["入库数量"].Value) != 0)
                {
                    string sqlUpdate = @"Update PurchaseOrderRecordByCMF Set ExpiredDate='" + dgvr.Cells["到期日期"].Value.ToString() + "',LotNumber='" + dgvr.Cells["批号"].Value.ToString() + "',InternalLotNumber='" + dgvr.Cells["公司批号"].Value.ToString() + "',POStatus = 5,ActualDeliveryQuantity=" + Convert.ToDouble(dgvr.Cells["送货数量"].Value) + ",PORVQuantity=" + Convert.ToDouble(dgvr.Cells["入库数量"].Value) + ",POInventoryKeeperOperatedIntoFSDateTime = '" + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss") + "'   Where Guid='" + dgvr.Cells["Guid"].Value.ToString() + "'";
                    sqlUpdateList.Add(sqlUpdate);
                    if (Convert.ToInt32(dgvr.Cells["NewPORVRecord"].Value) == 1)
                    {
                        parentGuid = dgvr.Cells["ParentGuid"].Value.ToString();
                        guidList.Add(dgvr.Cells["Guid"].Value.ToString());
                    }
                }

            }
            for(int i = 0;i < guidList.Count; i++)
            {
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
	                                                        UnitPrice,
	                                                        LineType,
	                                                        LineStatus,
	                                                        NeededDate,
	                                                        PromisedDate,
	                                                        POItemQuantity,
	                                                        PricePreTax,
	                                                        Stock,
	                                                        Bin,
	                                                        InspectionPeriod,
	                                                        BuyerName,
	                                                        POItemFSPlacedDateTime,
	                                                        TaxRate,
	                                                        Guid,
	                                                        NewPORVRecord
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
	                                                        POStatus,
	                                                        UnitPrice,
	                                                        LineType,
	                                                        LineStatus,
	                                                        NeededDate,
	                                                        PromisedDate,
	                                                        POItemQuantity,
	                                                        PricePreTax,
	                                                        Stock,
	                                                        Bin,
	                                                        InspectionPeriod,
	                                                        BuyerName,
	                                                        POItemFSPlacedDateTime,
	                                                        TaxRate,
	                                                        '"+guidList[i]+"', 1  FROM   PurchaseOrderRecordByCMF  WHERE Guid = '"+parentGuid+"'";
                sqlInsertList.Add(sqlInsert);
            }
            
            if(!SQLHelper.BatchExecuteNonQuery(GlobalSpace.FSDBConnstr,sqlInsertList))
            {
                Custom.MsgEx("新增的入库记录失败，请联系管理员！");
                return;
            }

            if (SQLHelper.BatchExecuteNonQuery(GlobalSpace.FSDBConnstr, sqlUpdateList))
            {
                Custom.MsgEx("入库信息更新成功！");
            }
            else
            {
                Custom.MsgEx("入库信息更新失败！");
            }

        }
        //更新订单状态
        private bool UpdatePOStatus(string poNumber, int status)
        {
            string sqlUpdate = @"Update PurchaseOrdersByCMF Set POStatus = " + status + " Where PONumber='" + poNumber + "'";
            if (SQLHelper.ExecuteNonQuery(GlobalSpace.FSDBConnstr, sqlUpdate) )
            {
                return true;
            }
            return false;
        }
        //仓库更新订单中物料的状态和信息  
        private bool UpdatePOItemStatus(DataGridView dgv, string poNumber, int status)
        {
            bool bSucceed = false;
            bool bCellNull = false;
            string lineNumber = string.Empty;
            string itemNumber = string.Empty;
            string lotNumber = string.Empty;
            string internalLotNumber = string.Empty;
            string manufacturedDate = string.Empty;
            string expiredDate = string.Empty;
            double ActualReceivedQuantity = 0;
            List<string> sqlList = new List<string>();
            try
            {
                for (int i = 0; i <= dgv.Rows.Count - 1; i++)
                {
                    //此处更新状态的物料为物料行已选中同时实际入库量不能为0,物料的状态必须为已下达
                    if (Convert.ToBoolean(dgv.Rows[i].Cells["Check"].Value) == true && Convert.ToDouble(dgv.Rows[i].Cells["实际入库量"].Value) != 0 && dgv.Rows[i].Cells["订单状态"].Value.ToString() == "已到货")
                    {
                        lineNumber = dgv.Rows[i].Cells["行号"].Value.ToString();
                        itemNumber = dgv.Rows[i].Cells["物料代码"].Value.ToString();
                        ActualReceivedQuantity = Convert.ToDouble(dgv.Rows[i].Cells["实际入库量"].Value);                                                    
                        lotNumber = dgv.Rows[i].Cells["批号"].Value.ToString();
                        internalLotNumber = dgv.Rows[i].Cells["公司批号"].Value.ToString();
                        manufacturedDate = dgv.Rows[i].Cells["生产日期"].Value.ToString();
                        expiredDate = dgv.Rows[i].Cells["到期日期"].Value.ToString();
                        string strUpdatePOItem = @"Update PurchaseOrderRecordByCMF Set POStatus = " + status + ",PORVQuantity= '" + ActualReceivedQuantity + "',LotNumber='"+lotNumber+"',InternalLotNumber='"+internalLotNumber+"',ManufacturedDate='"+manufacturedDate+"',ExpiredDate='"+expiredDate+ "',ReveicedByStockKeeper='"+fsuserid+"' Where PONumber='" + poNumber + "' And ItemNumber='" + itemNumber + "' And LineNumber='" + lineNumber + "' And POStatus = 4";
                        sqlList.Add(strUpdatePOItem);
                    }
                }
           //     MessageBox.Show("共含有条" + sqlList.Count.ToString() + "语句");
                if(SQLHelper.BatchExecuteNonQuery(GlobalSpace.FSDBConnstr, sqlList))
                {
                    bSucceed = true;
                }              
            }
            catch (Exception ex)
            {
                MessageBoxEx.Show("异常：" + ex.Message, "提示");            
            }

            return bSucceed;
        }

        private bool UpdatePOItemStatusFS(DataGridViewRow dgvr, string poNumber)
        {
            string lineNumber = string.Empty;
            string itemNumber = string.Empty;
            double porvQuantity = 0;

            using (SqlConnection conn = new SqlConnection(GlobalSpace.FSDBConnstr))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    if (conn.State == ConnectionState.Closed)
                    {
                        conn.Open();
                    }

                    SqlTransaction transaction = null;
                    transaction = conn.BeginTransaction();
                    cmd.Transaction = transaction;
                    cmd.Connection = conn;

                    try
                    {
                        //此处更新状态的物料为物料行已选中,实际入库量不能为0,物料状态必须为4
                        if (Convert.ToBoolean(dgvr.Cells["Check"].Value) == true && Convert.ToDouble(dgvr.Cells["实际入库量"].Value) != 0 && Convert.ToInt32(dgvr.Cells["订单状态"].Value) == 5)
                        {
                            lineNumber = dgvr.Cells["行号"].Value.ToString();
                            itemNumber = dgvr.Cells["物料代码"].Value.ToString();
                            porvQuantity = Convert.ToDouble(dgvr.Cells["实际入库量"].Value);

                            string strUpdatePOItem = @"Update PurchaseOrderRecordByCMF Set POStatus = 6  Where PONumber='" + poNumber + "' And ItemNumber='" + itemNumber + "' And LineNumber='" + lineNumber + "'";

                            cmd.CommandType = CommandType.Text;
                            cmd.CommandText = strUpdatePOItem;
                            if (cmd.ExecuteNonQuery() < 0)
                            {
                                throw new Exception();
                            }
                        }

                        transaction.Commit();
                        return true;
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        MessageBoxEx.Show("异常：" + ex.Message, "提示");
                    }
                }
            }
            return false;
        }

        /*
        //将订单中物料写入四班
        private void WritePOItemInfoIntoFS(DataGridViewRow dgvr, string poNumber)
        {
            bool bExist = false;
            bool bQuantityZero = false;
            bool bSuccees = true;


            if (Convert.ToBoolean(dgvr.Cells["Check"].Value) == true)
            {
                bExist = true;
            }

            if (bExist)
            {

                if (Convert.ToInt32(dgvr.Cells["实际入库量"].Value) == 0)
                {
                    bQuantityZero = true;
                }

                if (bQuantityZero)
                {
                    if ((MessageBoxEx.Show("有物料的入库数量为0，确定继续么？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.OK))
                    {
                        for (int i = 0; i < dgvPODetail.Rows.Count; i++)
                        {
                            if (!UpdatePOItemStatus(dgvPODetail.Rows[i], PONumber))
                            {
                                bSuccees = false;
                            }
                        }
                    }
                    else
                    {
                        return;
                    }

                }
                else
                {
                    for (int i = 0; i < dgvPODetail.Rows.Count; i++)
                    {
                        if (!UpdatePOItemStatus(dgvPODetail.Rows[i], PONumber))
                        {
                            bSuccees = false;
                        }
                    }
                }
                if (!bSuccees)
                {
                    MessageBoxEx.Show("更新不完全！", "提示");
                    dgvPODetail.DataSource = GetVendorPOItemsDetail(PONumber, 4,fsuserid);
                }
                else
                {
                    MessageBoxEx.Show("提交成功！", "提示");
                }
            }
            else
            {
                MessageBoxEx.Show("没有选中的行！", "提示");
            }


        }
        */

        private void dgvPOForFS_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            string poNumber = string.Empty;
            List<string> list = new List<string>();
            string itemNumber = string.Empty;
            if (e.RowIndex < 0)
            {
                MessageBoxEx.Show("请选择有效的行！", "提示");
            }
            else
            {
                poNumber = dgvPOForFS["采购单号", e.RowIndex].Value.ToString();
                PONumberForFS = poNumber;
                dgvPODetailFS.DataSource = GetVendorPOItemsDetailFS(poNumber,5,itemType);
                dgvPODetailFS.Columns["Guid"].Visible = false;
            }
           
            /*
            if(dgvPODetailFS.Rows.Count > 0)
            {
                for (int i = 0; i < dgvPODetailFS.Rows.Count; i++)
                {
                    itemNumber = dgvPODetailFS.Rows[i].Cells["物料代码FS"].Value.ToString();
                    list = GetItemInfo(itemNumber);
                    if (list[0] == "Y")
                    {
                        dgvPODetailFS.Rows[i].Cells["检验状态FS"].Value = "I";
                    }
                    else
                    {
                        dgvPODetailFS.Rows[i].Cells["检验状态FS"].Value = "O";
                    }
                    dgvPODetailFS.Rows[i].Cells["库FS"].Value = list[1];
                    dgvPODetailFS.Rows[i].Cells["位FS"].Value = list[2];
                }
            }
            */
        }
        //通过订单号获取订单中物料详情
        private DataTable GetVendorPOItemsDetailFS(string ponumber, int status, string type)
        {
            DataTable dtTemp = null;
            string date = DateTime.Now.ToString("yyMMdd");

            string strSqlCheck = @"Select Id From  PurchaseOrderRecordByCMF Where PONumber='" + ponumber + "'";
            string strSql = @"SELECT
                                                T1.Guid,                                 
                                                T1.LineNumber AS 行号,
	                                        	T1.ItemNumber AS 物料代码,
	                                            T1.ItemDescription AS 描述,
	                                            T1.LineUM AS 单位,
                                                T1.VendorNumber AS 供应商码,
	                                            T1.VendorName AS 供应商名,
                                                T1.ManufacturerNumber AS 生产商码,
	                                            T1.ManufacturerName AS 生产商名,
	                                            T1.UnitPrice AS 单价,
	                                            T1.POItemQuantity AS 订购数量,
	                                            T1.DemandDeliveryDate AS 需求日期,
                                                T1.StockKeeper AS 库管员,
                                                 (case T1.POStatus when  '1' then '已提交'
                                                         when  '2' then '已审核'
                                                         when  '3' then '已下达' 
                                                        when  '4' then '已到货' 
                                                        when  '5' then '已收货' 
                                                        when  '6' then '已入库' 
                                                        when  '7' then '已开票' 
                                                 end     
                                                )  AS 订单状态,
	                                            T1.ForeignNumber AS 外贸单号, T1.Stock AS 库,T1.Bin AS 位,T1.InspectionPeriod AS 检验,
                                                T1.ActualDeliveryQuantity AS 送货数量, 
                                                T1.PORVQuantity AS 入库数量,
                                                T1.LotNumber AS 批号,
                                                T1.InternalLotNumber AS 公司批号,
                                                T1.ExpiredDate AS 到期日期      
                                                FROM  PurchaseOrderRecordByCMF T1 
                                                WHERE T1.PONumber = '" + ponumber + "' AND T1.POStatus = " + status + " And Left(T1.ItemNumber,1)='" + type + "'  Order By T1.LineNumber ASC";

            if (SQLHelper.Exist(GlobalSpace.FSDBConnstr, strSqlCheck))
            {
                dtTemp = SQLHelper.GetDataTable(GlobalSpace.FSDBConnstr, strSql);
            }
            else
            {
                MessageBoxEx.Show("该订单不存在！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            return dtTemp;
        }
        private void dgvPOForFS_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            dgvPOForFS_CellContentDoubleClick(sender, e);
        }

        private void btnPORVFS_Click(object sender, EventArgs e)
        {
            FSFunctionLib.FSConfigFileInitialize(GlobalSpace.fsconfigfilepath, fsuserid, fspassword);
            int m = 0, n = 0;
            string poNumber = string.Empty;
            string lineNumber = string.Empty;
            string itemNumber = string.Empty;
            string guid = string.Empty;
            double porvQuantity = 0.00000;

            foreach (DataGridViewRow dgvr in dgvPODetailFS.Rows)
            {
                m++;
                try
                {
                    lineNumber = dgvr.Cells["行号"].Value.ToString();
                    itemNumber = dgvr.Cells["物料代码"].Value.ToString();
                    porvQuantity = Convert.ToDouble(dgvr.Cells["入库数量"].Value);
                    guid = dgvr.Cells["Guid"].Value.ToString();
                    poNumber = PONumberForFS;
                    string strUpdatePOItem = @"Update PurchaseOrderRecordByCMF Set POStatus = 6,POInventoryKeeperOperatedIntoFSDate= '"+ DateTime.Now.ToString("yyyy-MM-dd")+"'   Where Guid = '"+ guid + "'";
                        
                    if(PORV(poNumber,dgvr))
                    {
                        //MessageBoxEx.Show("四班入库成功", "提示");
                        n++;
                        //更新订单中物料状态为已入库
                        if(!SQLHelper.ExecuteNonQuery(GlobalSpace.FSDBConnstr, strUpdatePOItem) )
                        {
                            MessageBoxEx.Show("四班入库成功，订单中更新"+lineNumber+"行物料"+itemNumber+"状态时失败,请联系管理员！", "提示");
                        }
                    }                    
                    else
                    {
                        MessageBoxEx.Show("四班入库失败", "提示");
                    }


                }
                catch (Exception ex)
                {                      
                    MessageBoxEx.Show("异常：" + ex.Message, "提示");
                }
            
            }
            if(m == n)
            {
                MessageBoxEx.Show("所有物料四班入库成功", "提示");
            }
            else
            {
                MessageBoxEx.Show("部分物料四班入库成功，请联系管理员查找原因！", "提示");
            }
            CommonOperate.EmptyDataGridView(dgvPODetailFS);
            dgvPODetailFS.DataSource = GetVendorPOItemsDetailFS(poNumber, 5, itemType);
            FSFunctionLib.FSExit();
        }
        //库管员操作入库
        private bool PORV(string poNumber,DataGridViewRow dgvr)
        {         
            PORV01 porv01 = new PORV01();
            porv01.PONumber.Value = poNumber;
            porv01.POLineNumber.Value = dgvr.Cells["行号"].Value.ToString();
            porv01.POLineUM.Value = dgvr.Cells["单位"].Value.ToString();
            porv01.POReceiptActionType.Value = "R";
            porv01.Stockroom1.Value =  dgvr.Cells["库"].Value.ToString();
            porv01.Bin1.Value = dgvr.Cells["位"].Value.ToString(); 
            porv01.InventoryCategory1.Value = "I";// dgvr.Cells["检验状态"].Value.ToString();
            porv01.InspectionCode1.Value = "N";
            porv01.ReceiptQuantityMove1.Value = dgvr.Cells["入库数量"].Value.ToString();
            porv01.POLineType.Value ="P";
            porv01.ItemNumber.Value = dgvr.Cells["物料代码"].Value.ToString();
            porv01.NewLot.Value = "Y";
            porv01.LotNumberAssignmentPolicy.Value = "C";
            porv01.LotNumberDefault.Value = dgvr.Cells["公司批号"].Value.ToString();
            porv01.LotNumber.Value = dgvr.Cells["公司批号"].Value.ToString();
            porv01.VendorLotNumber.Value = dgvr.Cells["批号"].Value.ToString();
            porv01.LotDescription.Value = dgvr.Cells["生产商名"].Value.ToString();
            porv01.LotUserDefined5.Value = dgvr.Cells["生产商码"].Value.ToString();
            porv01.POReceiptDate.Value = DateTime.Now.ToString("MMddyy");//此处不确定
            porv01.PromisedDate.Value = dgvr.Cells["需求日期"].Value.ToString();
            porv01.LotExpirationDate.Value = dgvr.Cells["到期日期"].Value.ToString();    

            string transactionString;
            transactionString = porv01.GetString(TransactionStringFormat.fsCDF);

            if (FSFunctionLib.fstiClient.ProcessId(porv01, null))
            {
                return true;
            }
            else
            {
                MessageBoxEx.Show("订单" + poNumber + "失败！", "提示");
                FSTIError error = FSFunctionLib.fstiClient.TransactionError;
                FSFunctionLib.FSErrorMsg("四班异常信息");
                if(!CommonOperate.WriteFSErrorLog("PORV",porv01,error,fsuserid))
                {
                    Custom.MsgEx("保存四班错误日志时出错，请联系管理员！");
                }

            }
            return false;
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
            if(dgvr.Cells["物料代码"].Value.ToString().Substring(0,1) == "A")
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
        private void btnMakeAllCheckedFS_Click(object sender, EventArgs e)
        {
            
        }

        private void btnMakeAllUnCheckedFS_Click(object sender, EventArgs e)
        {
            if (dgvPODetailFS.Rows.Count > 0)
            {
                for (int i = 0; i < dgvPODetailFS.Rows.Count; i++)
                {
                    dgvPODetailFS.Rows[i].Cells["CheckFS"].Value = false;
                }
            }
        }

        private void tbPONumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar == (char) 13)
            {

            }
        }

        private void tbPONumberFS_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {

            }
        }

        private void btnPrintPODetail_Click(object sender, EventArgs e)
        {

        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            
            DataTable dtTemp = (DataTable)dgvPO.DataSource;
            dtTemp.Rows.Clear();
            dgvPO.DataSource = dtTemp;
            GetStockUnHandledPO(fsuserid);
        }

        private void btnRefreshFS_Click(object sender, EventArgs e)
        {
            CommonOperate.EmptyDataGridView(dgvPOPrint);
            GetStockHandledPOForFS(itemType);
        }

        private void btnPrintMakeAllChecked_Click(object sender, EventArgs e)
        {
            foreach(DataGridViewRow dgvr in dgvPOPrint.Rows)
            {
                if(dgvPOPrint.Rows.Count > 0)
                {
                    dgvr.Cells["CheckPrint"].Value = true;
                }
            }
        }

        private void btnPrintMakeAllUnchecked_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow dgvr in dgvPOPrint.Rows)
            {
                if (dgvPOPrint.Rows.Count > 0)
                {
                    dgvr.Cells["CheckPrint"].Value = false;
                }
            }
        }

        private void btnPOPrintRefresh_Click(object sender, EventArgs e)
        {
            string sqlSelect = @"SELECT
	                                DISTINCT PONumber AS 采购单号
                                FROM
	                                PurchaseOrderRecordByCMF
                                WHERE
	                                POInventoryKeeperOperatedIntoFSDate='" + DateTime.Now.ToString("yyyy-MM-dd") + "'";
            dgvPOPrint.DataSource = SQLHelper.GetDataTable(GlobalSpace.FSDBConnstr, sqlSelect);
        }

        private void tbPONumberPrint_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar==(char)13)
            {
                if(!string.IsNullOrEmpty(tbPONumberPrint.Text.Trim()))
                {
                    string sqlSelect = @"SELECT
	                                DISTINCT PONumber AS 采购单号
                                FROM
	                                PurchaseOrderRecordByCMF
                                WHERE
	                                PONumber='"+tbPONumberPrint.Text.Trim()+"'";
                    dgvPOPrint.DataSource = SQLHelper.GetDataTable(GlobalSpace.FSDBConnstr, sqlSelect);
                }
            }
        }

        private void dgvPOPrint_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            dgvPOPrint_CellDoubleClick(sender, e);
        }

        private void dgvPOPrint_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            PONumberForPrint  = dgvPOPrint.CurrentRow.Cells["采购单号"].Value.ToString();
            dgvPOItemDetailPrint.DataSource = GetPOItemDetailForPrint(PONumberForPrint);
        }

        //查询出仓库用于打印物料请验单的信息
        private DataTable GetPOItemDetailForPrint(string ponumber)
        {
            DataTable dtTemp = null;
            //此处设定为只要是入了四班的物料（状态≥6），都可以查询和打印游览、打印
            string sqlSelect = @"SELECT
	                                        LineNumber AS 行号,
	                                        ItemNumber AS  物料代码,
	                                        ItemDescription AS 物料描述,
	                                        LineUM AS 单位,
	                                        UnitPrice AS 单价,
	                                        POItemQuantity AS 订货数量,
	                                        PORVQuantity AS 实收数量,
	                                        PORVQuantity AS 合格数量,
	                                        LotNumber AS 生产商批号,
	                                        InternalLotNumber AS 公司批号,
	                                        ManufacturerNumber AS 生产商代码,
	                                        ManufacturerName AS 生产商名称
                                        FROM
	                                        PurchaseOrderRecordByCMF
                                        WHERE
	                                        PONumber = '"+ponumber+"' AND POStatus >=6 And POStatus <>99";
            dtTemp = SQLHelper.GetDataTable(GlobalSpace.FSDBConnstr, sqlSelect);
            return dtTemp;
        }
        private void btnPrintSearch_Click(object sender, EventArgs e)
        {

            if (!string.IsNullOrEmpty(tbPONumberPrint.Text.Trim()))
            {
                string sqlSelect = @"SELECT
	                                DISTINCT PONumber AS 采购单号
                                FROM
	                                PurchaseOrderRecordByCMF
                                WHERE
	                                PONumber='" + tbPONumberPrint.Text.Trim() + "'";
                dgvPOPrint.DataSource = SQLHelper.GetDataTable(GlobalSpace.FSDBConnstr, sqlSelect);
            }
        }


        private void btnPrintBatchPO_Click(object sender, EventArgs e)
        {

            if (dgvPOPrint.Rows.Count > 0)
            {
                //构造订单的供应商信息表
                DataTable dtVendorInfo = new DataTable();
                dtVendorInfo.Columns.Add(new DataColumn("采购单号", typeof(string)));               

                //构造订单中物料的明细表
                DataTable dtPOItemDetail = new DataTable();
                dtPOItemDetail.Columns.Add(new DataColumn("采购单号", typeof(string)));
                dtPOItemDetail.Columns.Add(new DataColumn("行号", typeof(string)));
                dtPOItemDetail.Columns.Add(new DataColumn("物料代码", typeof(string)));
                dtPOItemDetail.Columns.Add(new DataColumn("物料描述", typeof(string)));
                dtPOItemDetail.Columns.Add(new DataColumn("单位", typeof(string)));
                dtPOItemDetail.Columns.Add(new DataColumn("单价", typeof(string)));
                dtPOItemDetail.Columns.Add(new DataColumn("订货数量", typeof(string)));
                dtPOItemDetail.Columns.Add(new DataColumn("入库数量", typeof(string)));
                dtPOItemDetail.Columns.Add(new DataColumn("生产商批号", typeof(string)));
                dtPOItemDetail.Columns.Add(new DataColumn("公司批号", typeof(string)));
                dtPOItemDetail.Columns.Add(new DataColumn("生产商代码", typeof(string)));
                dtPOItemDetail.Columns.Add(new DataColumn("生产商名称", typeof(string)));
                dtPOItemDetail.Columns.Add(new DataColumn("外贸单号", typeof(string)));
                dtPOItemDetail.Columns.Add(new DataColumn("供应商代码", typeof(string)));
                dtPOItemDetail.Columns.Add(new DataColumn("供应商名称", typeof(string)));
                dtPOItemDetail.Columns.Add(new DataColumn("采购员", typeof(string)));
                foreach (DataGridViewRow dgvr in dgvPOPrint.Rows)
                {
                    if(Convert.ToBoolean( dgvr.Cells["CheckPrint"].Value) == true)
                    {
                        string ponumber = dgvr.Cells["采购单号"].Value.ToString();
                        string sqlSelect = @"SELECT
	                                        DISTINCT
	                                        PONumber AS 采购单号	                                   
                                        FROM
	                                        PurchaseOrderRecordByCMF T1
                                        WHERE
	                                        PONumber = '" + ponumber + "'AND POStatus >=6";
                        string sqlSelectPOItemDetail = @"SELECT
                                            PONumber AS 采购单号,
	                                        LineNumber AS 行号,
	                                        ItemNumber AS  物料代码,
	                                        ItemDescription AS 物料描述,
	                                        LineUM AS 单位,
	                                        UnitPrice AS 单价,
	                                        POItemQuantity AS 订货数量,
	                                        PORVQuantity AS 入库数量,
	                                        LotNumber AS 生产商批号,
	                                        InternalLotNumber AS 公司批号,
	                                        ManufacturerNumber AS 生产商代码,
	                                        ManufacturerName AS 生产商名称,
                                            ForeignNumber AS 外贸单号,
                                            VendorNumber AS 供应商代码,
	                                        VendorName AS 供应商名称,
                                            (Buyer+'|'+BuyerName) AS 采购员
                                        FROM
	                                        PurchaseOrderRecordByCMF
                                        WHERE
	                                        PONumber = '" + ponumber + "' AND POStatus >=6 And POStatus <> 99";
                        DataTable dtTemp = SQLHelper.GetDataTable(GlobalSpace.FSDBConnstr, sqlSelect);
                        DataTable dtTempPOItemDetail = SQLHelper.GetDataTable(GlobalSpace.FSDBConnstr, sqlSelectPOItemDetail);
                        if(dtTemp.Rows.Count <=0)
                        {
                            MessageBoxEx.Show("Purchase order "+ponumber+"no information!");
                        }
                        else
                        {
                            DataRow dr = dtVendorInfo.NewRow();
                            dr["采购单号"] = dtTemp.Rows[0]["采购单号"].ToString();
                            dtVendorInfo.Rows.Add(dr);
                        }

                        if (dtTempPOItemDetail.Rows.Count <= 0)
                        {
                            MessageBoxEx.Show("Purchase order " + ponumber + "no information!");
                        }
                        else
                        {
                            foreach(DataRow dr2 in dtTempPOItemDetail.Rows)
                            {
                                DataRow dr3 = dtTempPOItemDetail.NewRow();
                                dr3["采购单号"] = dr2["采购单号"];
                                dr3["行号"] = dr2["行号"];
                                dr3["物料代码"] = dr2["物料代码"];
                                dr3["物料描述"] = dr2["物料描述"];
                                dr3["单位"] = dr2["单位"];
                                dr3["订货数量"] = dr2["订货数量"];
                                dr3["入库数量"] = dr2["入库数量"];
                                dr3["生产商批号"] = dr2["生产商批号"];
                                dr3["公司批号"] = dr2["公司批号"];
                                dr3["生产商代码"] = dr2["生产商代码"];
                                dr3["生产商名称"] = dr2["生产商名称"];
                                dr3["外贸单号"] = dr2["外贸单号"];
                                dr3["供应商代码"] = dr2["供应商代码"];
                                dr3["供应商名称"] = dr2["供应商名称"];
                                dr3["采购员"] = dr2["采购员"];
                                dtPOItemDetail.Rows.Add(dr3.ItemArray);                         
                            }                                                       
                        }
                    }            
                }

                WarehousePOItemDetailPrint wpoidp = new WarehousePOItemDetailPrint(dtVendorInfo, dtPOItemDetail,username);
                wpoidp.ShowDialog();

            }
        }

        private void btnItemReturnedRefresh_Click(object sender, EventArgs e)
        {
            GetUnHandledReturnedItem(fsuserid);
        }

        private void btnItemReturnedFSOperate_Click(object sender, EventArgs e)
        {
            FSFunctionLib.FSConfigFileInitialize(GlobalSpace.fsconfigfilepath, fsuserid, fspassword);

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
                        string strUpdatePOItem = @"Update PurchaseOrderItemReturnedByCMF Set Status = 1,OperateFSDateTime = '"+DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")+"' Where Id = '"+lineId+"'";

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

        private void btnRIMakeAllChecked_Click(object sender, EventArgs e)
        {
            foreach(DataGridViewRow dgvr in dgvItemReturnedUnHandledRecod.Rows)
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

        private void btnTest_Click(object sender, EventArgs e)
        {
            PORV02 porv02 = new PORV02();
            porv02.PONumber.Value = "PM-110918-001";
            porv02.POLineNumber.Value = "002";
            porv02.POReceiptActionType.Value = "X";
            porv02.ItemNumber.Value = "M05170";
            porv02.LotNumber.Value = "181111S6601";
            porv02.POLineType.Value = "P";
            porv02.LocationReverseQuantity1.Value = "100";
            porv02.Stockroom1.Value = "M3";
            porv02.Bin1.Value = "01";
            porv02.InventoryCategory1.Value = "I";
            porv02.PromisedDate.Value = "111518";
            porv02.NewLot.Value = "Y";
            
            FSFunctionLib.FSConfigFileInitialize(GlobalSpace.fsconfigfilepath, fsuserid, fspassword);

            try
            {
                if (FSFunctionLib.fstiClient.ProcessId(porv02, null))
                {
                    MessageBoxEx.Show("四班中退库操作成功！", "提示");
                }
                else
                {
                    MessageBoxEx.Show("四班中退库操作下达失败！", "提示");
                    FSFunctionLib.FSErrorMsg("四班异常信息");
                }
            }
            catch (Exception ex)
            {
                MessageBoxEx.Show("出现异常：" + ex.Message);
            }

            FSFunctionLib.FSExit();
        }

        private void btnAddPORVBatches_Click(object sender, EventArgs e)
        {
            /*
            int icount = 0;
            int iindex = 0;
            foreach(DataGridViewRow dgvr in dgvPODetail.Rows)
            {
                if(Convert.ToBoolean(dgvr.Cells["选择"].Value) == true)
                {
                    icount++;
                }
            }

            if(icount == 1)
            {

            }
            else
            {
                Custom.MsgEx("选中了多行，请选择一行进行添加！");
            }
            */
            if(dgvPODetail.SelectedRows.Count == 1)
            {
                int iIndex = dgvPODetail.SelectedCells[0].RowIndex;
                int iCount = Convert.ToInt32(tbPORVBatches.Text.Trim());
                string strGuid = dgvPODetail.SelectedRows[0].Cells["Guid"].Value.ToString();
                
                DataTable dtSource = (DataTable)dgvPODetail.DataSource;
                DataTable dtNew = dtSource.Copy();

                if (iCount == 1)
                {                 
                    DataRow[] drs = dtSource.Select("Guid = '" + strGuid + "'");
                    DataRow dr = dtSource.NewRow();
                    dr.ItemArray = drs[0].ItemArray;
                    dr["Guid"] = Guid.NewGuid().ToString("N");
                    dr["NewPorvRecord"] = "1";
                    dr["ParentGuid"] = strGuid;
                    dtSource.Rows.InsertAt(dr, iIndex + 1);
                    dgvPODetail.Columns["NewPORVRecord"].Visible = false;
                }
                else if( iCount > 1)
                {
                    for(int i = 0; i < iCount; i++)
                    {
                        DataRow[] drs = dtSource.Select("Guid = '" + strGuid + "'");
                        DataRow dr = dtSource.NewRow();
                        dr.ItemArray = drs[0].ItemArray;
                        dr["Guid"] = Guid.NewGuid().ToString("N");
                        dr["NewPorvRecord"] = "1";
                        dr["ParentGuid"] = strGuid;
                        dtSource.Rows.InsertAt(dr, iIndex + 1);
                    }
                    dgvPODetail.Columns["NewPORVRecord"].Visible = false;
                }
            }
            else
            {
                Custom.MsgEx("没有选中的行或选中了多行！");
            }
        }

        private void superTabControlPanel4_Click(object sender, EventArgs e)
        {

        }

        private void dgvPO_CellContextMenuStripChanged(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void tbPONumber_TextChanged(object sender, EventArgs e)
        {

        }

        private void labelX1_Click(object sender, EventArgs e)
        {

        }

        private void btnViewToday_Click(object sender, EventArgs e)
        {
            ShowPODetail(DateTime.Now.ToString("yyyy-MM-dd"),itemType);
        }

        private void ShowPODetail(string date,string type)
        {
            string sqlSelectToday = @"SELECT                              
                                                T1.PONumber AS 采购单号,
	                                        	T1.ItemNumber AS 物料代码,
	                                            T1.ItemDescription AS 描述,
	                                            T1.LineUM AS 单位,
                                                T1.VendorNumber AS 供应商码,
	                                            T1.VendorName AS 供应商名,
                                                T1.ManufacturerNumber AS 生产商码,
	                                            T1.ManufacturerName AS 生产商名,
	                                            T1.POItemQuantity AS 订购数量,
                                                T1.StockKeeper AS 库管员,
	                                            T1.ForeignNumber AS 外贸单号, 
                                                T1.ActualDeliveryQuantity AS 送货数量, 
                                                T1.PORVQuantity AS 入库数量,
                                                T1.LotNumber AS 批号,
                                                T1.InternalLotNumber AS 公司批号,
                                                T1.ExpiredDate AS 到期日期      
                                                FROM  PurchaseOrderRecordByCMF T1 
                                                WHERE   POInventoryKeeperOperatedIntoFSDate='" + date + "' AND LEFT (ItemNumber, 1) = '" + type + "'";
            CommonOperate.EmptyDataGridView(dgvPODetailFS);
            dgvPODetailFS.DataSource = SQLHelper.GetDataTable(GlobalSpace.FSDBConnstr, sqlSelectToday);
        }

        private void btnViewAppointedDate_Click(object sender, EventArgs e)
        {
            ShowPODetail(this.dateTimePicker1.Value.ToString("yyyy-MM-dd"), itemType);
        }

        private void btnPrintForQC_Click(object sender, EventArgs e)
        {

        }

        private void btnViewAppointedPORVPO_Click(object sender, EventArgs e)
        {
            string sqlSelect = @"SELECT
	                                DISTINCT PONumber AS 采购单号
                                FROM
	                                PurchaseOrderRecordByCMF
                                WHERE
	                                POInventoryKeeperOperatedIntoFSDate='" + this.dateTimePicker2.Value.ToString("yyyy-MM-dd") + "'";
            dgvPOPrint.DataSource = SQLHelper.GetDataTable(GlobalSpace.FSDBConnstr, sqlSelect);
        }
    }
}
