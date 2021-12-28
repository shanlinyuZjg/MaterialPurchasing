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
using System.Drawing.Printing;
using ICSharpCode.SharpZipLib.Zip;
using System.Text.RegularExpressions;
using NPOI.OpenXmlFormats.Dml;
using System.Runtime.Remoting.Channels;
using System.IO;
using gregn6Lib;
using System.Net.Mail;
using System.Net;

namespace Global.Warehouse
{
    public partial class Stock : Office2007Form
    {
        //定义简体中文和西欧文编码字符集
        public static Encoding GB2312 = Encoding.GetEncoding("gb2312");
        public static Encoding ISO88591 = Encoding.GetEncoding("iso-8859-1");
        string FSUserID = string.Empty;
        string FSUserPassword = string.Empty;
        string UserName = string.Empty;
        bool IsNeedToConfirm = false;
        bool IsNeedToSubmit = false;
        bool IsRetrive = false;
        Dictionary<string, string> SpecialUserDict;
        Regex regEnglish = new Regex("^[a-zA-Z]");
        private List<string> FOItemKeeperList;
        private bool IsFSView = false;
        public Stock()
        {
            InitializeComponent();
        }

        public Stock(string uid, string password, string name, string privilege)
        {
            FSUserID = uid;
            FSUserPassword = password;
            UserName = name;
            MessageBoxEx.EnableGlass = false;
            this.EnableGlass = false;
            InitializeComponent();
    //        StockUser.Privilege = "SO";
            if(StockUser.Stock == "WHO")
            {
                GlobalSpace.EBRConnStr = GlobalSpace.GeneralEBRConnStr;
            }
            else
            {
                GlobalSpace.EBRConnStr = GlobalSpace.GeneralEBRConnStr;
            }
        }

        private void Stock_Load(object sender, EventArgs e)
        {
            tabTest.Visible = Program.tabTest;

        string sqlSelectFOItemKeeper = @"Select UserID From PurchaseDepartmentStockFOItemKeeper";
            FOItemKeeperList = SQLHelper.GetDataTable(GlobalSpace.FSDBConnstr, sqlSelectFOItemKeeper).AsEnumerable().Select(r => r.Field<string>("UserID")).ToList();
            string sqlCheckGMP = @"Select IsGMP From PurchaseOrderGMP";
            int gmpStatus =Convert.ToInt32(SQLHelper.ExecuteScalar(GlobalSpace.FSDBConnstr, sqlCheckGMP));

            if (FOItemKeeperList.Contains(StockUser.UserID))
            {
                dtpFP.Visible = true;
                btnFP.Visible = true;
                btnPrintFOForBatchRecord.Visible = true;
                btnPrintForLabel.Visible = false;
            }

            if (StockUser.IsVial == "1")
            {
                dgvVials.DataSource = GetVialDetail(StockUser.UserID);
            }
            if (StockUser.IsERP == "1")
            {
                tabFSOperate.Visible = true;
            }
            if(gmpStatus == 1)
            {
                tabIMTR.Visible = false;
   //             tabFSOperate.Visible = false;
            }
            else
            {
                tabIMTR.Visible = true;
     //           tabFSOperate.Visible = true;
            }
            if(StockUser.Privilege.Contains("TEST"))
            {
                tabIMTR.Visible = false ;
            }


            //获取管理药玻直接拉瓶子的库管员
            string sqlSelectSpecialKeeper = @"Select UserID,Type From PurchaseDepartmentStockSpecialKeeper";
            SpecialUserDict = SQLHelper.GetDataTable(GlobalSpace.FSDBConnstr, sqlSelectSpecialKeeper).Rows.Cast<DataRow>().ToDictionary(r => r["UserID"].ToString(), r => r["Type"].ToString());           

            if (StockUser.Privilege.Contains("SO") && !StockUser.Privilege.Contains("FS"))
            {
                tabPublic.Visible = true;
                tabUnhandledRecord.Visible = true;
                tabViewRecord.Visible = true;
                //显示公共物料
                dgvPublic.DataSource = GetPublicVendorPOItemsDetail(9, dtpPublic.Value.AddDays(-30).ToString("yyyy-MM-dd"));
                dgvPublic.Columns["Guid"].Visible = false;
                dgvVialsDetail.DataSource = GetSubmittedVialRecord();
            }
            else if (StockUser.Privilege.Contains("SFS") && !StockUser.Privilege.Contains("O"))
            {
                tabFSOperate.Visible = true;
                //dgvPODetailFS.DataSource = GetReceivedRecordByStatus(1);
                //dgvPODetailFS.Columns["Guid"].Visible = false;
                //dgvPODetailFS.Columns["Status"].Visible = false;
                //dgvPODetailFS.Columns["LotNumberAssign"].Visible = false;
                dgvFSRefresh();

            }
            else if (StockUser.Privilege.Contains("SOFS"))
            {
                tabPublic.Visible = true;
                tabUnhandledRecord.Visible = true;
                tabViewRecord.Visible = true;
                tabFSOperate.Visible = true;
            }
            else if (StockUser.Privilege.Contains("SAOFS"))
            {
                tabAssistant.Visible = true;
                tabFSOperate.Visible = true;
                tabViewRecord.Visible = true;
            }
            if (SpecialUserDict.ContainsKey(StockUser.UserID) && SpecialUserDict[StockUser.UserID] == "YaoBo")
            {
                tabOnlyVials.Visible = true;
            }

            if (gmpStatus == 1)
            {
                tabIMTR.Visible = false;
                tabFSOperate.Visible = false;
            }
        }

        private DataTable GetVialDetail(string userID)
        {
            string sqlSelect = @"Select ItemNumber AS 代码,ItemDescription AS 描述 From _NoLock_FS_Item Where ItemReference3='" + userID + "'";
            return SQLHelper.GetDataTableOleDb(GlobalSpace.oledbconnstrFSDBMR, sqlSelect);
        }

        private bool IsFSPerson(string uid)
        {
            string sqlSelect = @"Select Count(Id) From PurchaseDepartmentRBACByCMF Where UserID='" + uid + "'";
            if (SQLHelper.Exist(GlobalSpace.FSDBConnstr, sqlSelect))
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
	                                                LEFT (StockKeeper, 3) = '" + FSUserID + "' OR LEFT (StockKeeper, 2) = 'CX' ) ORDER BY OperateDateTime DESC";
            //dtPORVX = SQLHelper.GetDataTable(GlobalSpace.FSDBConnstr, sqlSelect);
            dgvItemReturnedUnHandledRecod.DataSource = SQLHelper.GetDataTable(GlobalSpace.FSDBConnstr, sqlSelect);
            //出选择列外，其余所有列为只读，避免人员误改动信息
            for (int i = 1; i <= dgvItemReturnedUnHandledRecod.Columns.Count - 1; i++)
            {
                dgvItemReturnedUnHandledRecod.Columns[i].ReadOnly = true;
            }
            dgvItemReturnedUnHandledRecod.Columns["Id"].Visible = false;
        }

        private void dgvPO_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            string poNumber = string.Empty;
            if (e.RowIndex >= 0)
            {
                /*   poNumber = dgvPO["采购单号", e.RowIndex].ToString();
                   PONumber = poNumber;
                   tbVendorNumber.Text = dgvPO["VendorNumber", e.RowIndex].ToString();
                   tbVendorName.Text = dgvPO["VendorName", e.RowIndex].ToString();
                   */
                //      dgvPODetail.DataSource = GetVendorPOItemsDetail(4,FSUserID);
                //       dgvPODetail.Columns["Id"].Visible = false;
                //       dgvPODetail.Columns["Guid"].Visible = false;

                //    dgvPODetail.Columns["库管员"].Width = 80;
                /*
                dgvPODetail.Columns["到货数量"].DefaultCellStyle.BackColor = Color.LightBlue;
                dgvPODetail.Columns["公司批号"].DefaultCellStyle.BackColor = Color.LightBlue;
                dgvPODetail.Columns["批号"].DefaultCellStyle.BackColor = Color.LightBlue;
                dgvPODetail.Columns["到期日期"].DefaultCellStyle.BackColor = Color.LightBlue;
                */
            }
        }

        private void dgvPO_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            dgvPO_CellContentDoubleClick(sender, e);
        }

        //获取未处理到货记录NumberOfPackages AS 整件数,PackageUM AS 件单位,PackageOdd AS 零头,PackageSpecification AS 包装规格, T1.LotNumber AS 厂家批号,
        private DataTable GetVendorPOItemsDetail(int status)
        {
            string strSql = string.Empty;
            if (StockUser.Type == "ALL")
            {
                strSql = @"SELECT
	                                        T1.Guid,T1.ParentGuid,T1.ForeignNumber AS 外贸单号,
			    
	                                        T1.PONumber AS 采购单号,T1.VendorNumber AS 供应商码,T1.VendorName AS 供应商名,
	                                        T1.ManufacturerNumber AS 生产商码,
	                                        T1.ManufacturerName AS 生产商名,
	                                        T1.LineNumber AS 行号,
	                                        T1.ItemNumber AS 物料代码,
	                                        T1.ItemDescription AS 描述,
	                                        T1.LineUM AS 单位,UnitPrice AS 单价,
	                                        T1.OrderQuantity AS 订货量,T1.NumberOfPackages AS 整件数,T1.PackageUM AS 件单位,T1.PackageOdd AS 零头,T1.PackageSpecification AS 包装规格,
	                                        T1.ReceiveQuantity AS 入库量,T1.LotNumber AS 厂家批号,
	                                        T1.InternalLotNumber AS 公司批号,T1.ManufacturedDate AS 生产日期,        
	                                        T1.ExpiredDate AS 到期日期,
	                                        T1.Stock AS 库,
	                                        T1.Bin AS 位,
	                                        T1.RetestDate AS 重测日期,	                                        
	                                        T1.InspectionPeriod,
	                                        T1.StockKeeper,
	                                        ItemReceiveType,VendorNumber,VendorName,UnitPrice
                                        FROM
	                                        PurchaseOrderRecordHistoryByCMF T1
                                        WHERE
	                                        T1.Status = " + status + "     AND    SUBSTRING(StockKeeper,0,charindex('|', StockKeeper))= '" + StockUser.UserID + "'     ORDER BY     T1.CreatedDateTime DESC";
            }
            else
            {
                strSql = @"SELECT
	                                        T1.Guid,T1.ParentGuid,T1.ForeignNumber AS 外贸单号,
	                                        T1.PONumber AS 采购单号,T1.VendorNumber AS 供应商码,T1.VendorName AS 供应商名,
	                                        T1.ManufacturerNumber AS 生产商码,
	                                        T1.ManufacturerName AS 生产商名,
	                                        T1.LineNumber AS 行号,
	                                        T1.ItemNumber AS 物料代码,
	                                        T1.ItemDescription AS 描述,
	                                        T1.LineUM AS 单位,UnitPrice AS 单价,
	                                        T1.OrderQuantity AS 订货量,T1.NumberOfPackages AS 整件数,T1.PackageUM AS 件单位,T1.PackageOdd AS 零头,T1.PackageSpecification AS 包装规格,
	                                        T1.ReceiveQuantity AS 入库量,T1.LotNumber AS 厂家批号,
	                                        T1.InternalLotNumber AS 公司批号,T1.ManufacturedDate AS 生产日期,        
	                                        T1.ExpiredDate AS 到期日期,
	                                        T1.Stock AS 库,
	                                        T1.Bin AS 位,
	                                        T1.RetestDate AS 重测日期,	                                        
	                                        T1.InspectionPeriod,
	                                        T1.StockKeeper,
	                                        ItemReceiveType,VendorNumber,VendorName
                                        FROM
	                                        PurchaseOrderRecordHistoryByCMF T1
                                        WHERE
	                                        T1.Status = " + status + "     AND    SUBSTRING(StockKeeper,0,charindex('|', StockKeeper)) = '" + StockUser.UserID + "'   ORDER BY       T1.CreatedDateTime DESC";
            }
            return SQLHelper.GetDataTable(GlobalSpace.FSDBConnstr, strSql);
        }
        //获取未处理到货记录
        private DataTable GetVendorPOForeignItemsDetail(int status)
        {
            string strSql = @"SELECT
	                                        T1.Guid,
	                                        T1.ParentGuid,
	                                        T1.ForeignNumber AS 外贸单号,
	                                        T1.ItemNumber AS 物料代码,
	                                        T1.VendorNumber AS 供应商码,T1.OrderQuantity AS 订货量,
	                                        '' AS 入库量,
	                                        '' AS 整件数,'件' AS 件单位,T1.PackageOdd AS 零头,T1.PackageSpecification AS 包装规格, T1.LotNumber AS 厂家批号,
	                                        T1.InternalLotNumber AS 公司批号,
	                                        T1.ManufacturedDate AS 生产日期,
	                                        T1.ExpiredDate AS 到期日期,
	                                        T1.ItemDescription AS 描述,
	                                        T1.VendorName AS 供应商名,
	                                        T1.PONumber AS 采购单号,
	                                        T1.ManufacturerNumber AS 生产商码,
	                                        T1.ManufacturerName AS 生产商名,
	                                        T1.LineNumber AS 行号,
	                                        T1.LineUM AS 单位,	                                        
	                                        T1.Stock AS 库,
	                                        T1.Bin AS 位,
	                                        T1.RetestDate AS 重测日期,
	                                        T1.InspectionPeriod,
	                                        T1.StockKeeper,
	                                        ItemReceiveType,VendorNumber,VendorName,UnitPrice
                                        FROM
	                                        PurchaseOrderRecordHistoryByCMF T1
                                        WHERE
	                                        T1.Status = " + status + "     AND    LEFT (T1.StockKeeper, 3) = '" + StockUser.UserID + "'      ORDER BY       T1.CreatedDateTime DESC";
            //       MessageBox.Show(strSql);
            return SQLHelper.GetDataTable(GlobalSpace.FSDBConnstr, strSql);
        }
        //获取未处理到货记录
        private DataTable GetVendorPOForeignItemsDetail(string recvDate)
        {
            string strSql = @"SELECT
	                                        T1.Guid,
	                                        T1.ParentGuid,
	                                        T1.ForeignNumber AS 外贸单号,
	                                        T1.ItemNumber AS 物料代码,
	                                        T1.VendorNumber AS 供应商码,
	                                        '' AS 入库量,'' AS 整件数,'件' AS 件单位,'0' AS 零头,'' AS 包装规格,
	                                        T1.LotNumber AS 厂家批号,
	                                        T1.InternalLotNumber AS 公司批号,
	                                        T1.ManufacturedDate AS 生产日期,
	                                        T1.ExpiredDate AS 到期日期,
	                                        T1.ItemDescription AS 描述,
	                                        T1.VendorName AS 供应商名,
	                                        T1.PONumber AS 采购单号,
	                                        T1.ManufacturerNumber AS 生产商码,
	                                        T1.ManufacturerName AS 生产商名,
	                                        T1.LineNumber AS 行号,
	                                        T1.LineUM AS 单位,
	                                        T1.OrderQuantity AS 订货量,
	                                        T1.Stock AS 库,
	                                        T1.Bin AS 位,
	                                        T1.RetestDate AS 重测日期,
	                                        T1.InspectionPeriod,
	                                        T1.StockKeeper,
	                                        ItemReceiveType,VendorNumber,VendorName
                                        FROM
	                                        PurchaseOrderRecordHistoryByCMF T1
                                        WHERE
	                                        T1.ReceiveDate = '" + recvDate + "'     AND    LEFT (T1.StockKeeper, 3) = '" + StockUser.UserID + "' And Status = 9    ORDER BY       T1.CreatedDateTime DESC ";
            //       MessageBox.Show(strSql);
            return SQLHelper.GetDataTable(GlobalSpace.FSDBConnstr, strSql);
        }
        //通过订单号获取订单中物料详情
        private DataTable GetVendorPOAItemsDetail(int status)
        {
            string strSql = @"SELECT
	                                        T1.Guid,T1.ParentGuid,
	                                        T1.PONumber AS 采购单号,T1.VendorNumber AS 供应商码,T1.VendorName AS 供应商名,
	                                        T1.ManufacturerNumber AS 生产商码,
	                                        T1.ManufacturerName AS 生产商名,
	                                        T1.LineNumber AS 行号,
	                                        T1.ItemNumber AS 物料代码,
	                                        T1.ItemDescription AS 描述,
	                                        T1.LineUM AS 单位,
	                                        T1.OrderQuantity AS 订货量,
	                                        T1.OrderQuantity AS 入库量,
	                                        T1.Stock AS 库,
	                                        T1.Bin AS 位,	                                                              
	                                        T1.InspectionPeriod,
	                                        T1.StockKeeper,
	                                        ItemReceiveType
                                        FROM
	                                        PurchaseOrderRecordHistoryByCMF T1
                                        WHERE
	                                        T1.Status = " + status + "     AND    LEFT (T1.StockKeeper, 3) = '" + StockUser.UserID + "'   And ItemReceiveType = '" + StockUser.Type + "'    ORDER BY       T1.CreatedDateTime DESC";

            return SQLHelper.GetDataTable(GlobalSpace.FSDBConnstr, strSql);
        }
        //通过订单号获取订单中物料详情
        private DataTable GetVendorPOItemsDetailReserve(string ponumber, int status, string keepercode)
        {
            string poType = ponumber.Substring(0, 2);
            string strSql = @"SELECT
                                                T1.Id,
                                                T1.Guid,
                                                T1.LineNumber AS 行号,
	                                        	T1.ItemNumber AS 物料代码,
	                                            T1.ItemDescription AS 描述,
	                                            T1.LineUM AS 单位,                                      
	                                            T1.UnitPrice AS 单价,
	                                            T1.POItemQuantity AS 订货量,
                                                T1.POItemQuantity AS 到货量,
                                                 T1.POItemQuantity AS 入库量,T1.InternalLotNumber AS 公司批号,T1.LotNumber AS 厂家批号, '0' AS 有效期年数,  
                                                T1.AccumulatedActualReceiveQuantity AS 累计入库量,
                                                Left(T1.StockKeeper,3) AS 库管员,
                                                 (case T1.POStatus when  '1' then '已提交'
                                                         when  '2' then '已审核'
                                                         when  '3' then '已下达' 
                                                        when  '4' then '已到货' 
                                                        when  '5' then '已收货' 
                                                        when  '6' then '已入库' 
                                                        when  '7' then '已开票' 
                                                        when  '66' then '部分到货' 
                                                 end     
                                                )  AS 订单状态,
	                                            T1.ForeignNumber AS 外贸单号, T1.Stock AS 库,T1.Bin AS 位,T1.InspectionPeriod AS 检验   FROM  PurchaseOrderRecordByCMF T1 WHERE T1.PONumber = '" + ponumber + "' AND T1.POStatus = " + status + " And (Left(T1.StockKeeper,3)='" + keepercode + "'  OR  Left(T1.StockKeeper,2) = 'CX' )  Order By T1.LineNumber ASC ";
            //          T1.ManufacturerNumber AS 生产商码,
            //T1.ManufacturerName AS 生产商名称,
            return SQLHelper.GetDataTable(GlobalSpace.FSDBConnstr, strSql);
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

        private bool CreatePOItemReceiveHistory(List<string> guidList, string guid, double deliveryQuantity, double receiveQuantity, double accumulatedQuantity)
        {
            List<string> sqlList = new List<string>();
            for (int i = 0; i < guidList.Count; i++)
            {
                string sqlInsertSingle = @"INSERT INTO PurchaseOrderRecordHistoryByCMF (
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
	                                                        TaxRate,
	                                                        Guid,	                                                        NewPORVRecord,ParentGuid,BatchParentGuid,ActualDeliveryQuantity,ActualReceiveQuantity,AccumulatedActualReceiveQuantity,POItemInventoryKeeperOperateDateTime
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
	                                                        5,
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
	                                                        TaxRate,
	                                                        '" + Guid.NewGuid().ToString("N") + "', 1,ParentGuid,'" + guid + "'," + deliveryQuantity + "," + receiveQuantity + "," + accumulatedQuantity + ",'" + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss") + "'  FROM   PurchaseOrderRecordByCMF  WHERE Guid = '" + guid + "'";
                sqlList.Add(sqlInsertSingle);
            }

            if (SQLHelper.BatchExecuteNonQuery(GlobalSpace.FSDBConnstr, sqlList))
            {
                return true;
            }
            return false;
        }

        private bool UpdatePOItemOriginalStatus(string guid, int status, double totalQuantity)
        {
            string sqlUpdate = @"Update PurchaseOrderRecordHistoryByCMF Set POStatus = " + status + ",AccumulatedActualReceiveQuantity=AccumulatedActualReceiveQuantity+" + totalQuantity + "  Where Guid = '" + guid + "'";
            if (SQLHelper.ExecuteNonQuery(GlobalSpace.FSDBConnstr, sqlUpdate))
            {
                return true;
            }
            return false;
        }

        private void CreateReceiveHistory(string guid)
        {
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
	                                                            [BuyerID],Stock,Bin,ReceiveDate,Guid
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
	                                                            [StockKeeper],
	                                                            [LotNumberAssign],
	                                                            [OrderQuantity],
	                                                            [ItemReceiveType],
	                                                            [Supervisor],
	                                                            [ForeignNumber],
	                                                            [BuyerID],Stock,Bin,ReceiveDate,Replace(NEWID(),'-','')  FROM   PurchaseOrderRecordByCMF  WHERE Guid = '" + guid + "'";
        }

        private void btnPORV_Click(object sender, EventArgs e)
        {
            if (!IsNeedToConfirm)
            {
                Custom.MsgEx("当前记录状态不允许重复提交！");
                return;
            }




            List<string> sqlList = new List<string>();
            List<string> specialItemList = SQLHelper.GetList(GlobalSpace.FSDBConnstr, "Select ItemNumber  From PurchaseDepartmentStockSpecialItem Where ItemDescription Not  Like '%粗盐%'", "ItemNumber");
            List<string> specialItemDesList = SQLHelper.GetList(GlobalSpace.FSDBConnstr, "Select ItemNumber  From PurchaseDepartmentStockSpecialItem Where ItemDescription Like '%粗盐%'", "ItemNumber");

            string vendorLotNumber = string.Empty;
            string internalLotNumber = string.Empty;
            string packageSpecification = string.Empty;
            string mfgDate = string.Empty;
            string expDate = string.Empty;
            string guid = string.Empty;
            string packageUm = string.Empty;
            double receiveQuantity = 0;
            double orderQuantity = 0;
            int packageQuantity = 0;
            double packageOdd = 0;
            List<string> foItemNumberList = new List<string>();

            DataTable dtOriginal = (DataTable)dgvPODetail.DataSource;
            DataTable dtTemp = dtOriginal.Clone();
            foreach (DataGridViewRow dgvr in dgvPODetail.Rows)
            {
                if (Convert.ToBoolean(dgvr.Cells["Checked"].Value) == true)
                {
                    DataRow dr = dtTemp.NewRow();
                    dr = (dgvr.DataBoundItem as DataRowView).Row;
                    dtTemp.Rows.Add(dr.ItemArray);
                    if(Convert.ToDouble(dgvr.Cells["入库量"].Value) > Convert.ToDouble(dgvr.Cells["订货量"].Value))
                    {
                        foItemNumberList.Add(dgvr.Cells["物料代码"].Value.ToString());
                    }
                }
            }
            if (FOItemKeeperList.Contains(StockUser.UserID))
            {
                if(foItemNumberList.Count > 0)
                {
                    string msg = "当前物料{0}入库数量超过订货数量，是否要继续？";
                    msg = string.Format(msg, string.Join(" ", foItemNumberList.ToArray()));
                    if(MessageBoxEx.Show(msg,"提示",MessageBoxButtons.YesNo) == DialogResult.No)
                    {
                        return;
                    }
                }
            }
            if (dtTemp.Rows.Count == 0)
            {
                Custom.MsgEx("当前无可选中行！");
                return;
            }

            //只针对外贸包材库的物料进行数量的判断，提醒。


            for (int i = 0; i < dtTemp.Rows.Count; i++)
            {
                if (dtTemp.Rows[i]["厂家批号"] == DBNull.Value || dtTemp.Rows[i]["厂家批号"].ToString() == "")
                {
                    vendorLotNumber = "";
                }
                else
                {
                    vendorLotNumber = dtTemp.Rows[i]["厂家批号"].ToString().Trim();
                }
                if (dtTemp.Rows[i]["公司批号"] == DBNull.Value || dtTemp.Rows[i]["公司批号"].ToString().Trim() == "")
                {
                    internalLotNumber = "";
                }
                else
                {
                    internalLotNumber = dtTemp.Rows[i]["公司批号"].ToString().Trim();
                }
                if (dtTemp.Rows[i]["包装规格"] == DBNull.Value || dtTemp.Rows[i]["包装规格"].ToString().Trim() == "")
                {
                    packageSpecification = "";
                }
                else
                {
                    packageSpecification = dtTemp.Rows[i]["包装规格"].ToString();
                }
                if (dtTemp.Rows[i]["件单位"] == DBNull.Value || dtTemp.Rows[i]["件单位"].ToString().Trim() == "")
                {
                    packageUm = "";
                }
                else
                {
                    packageUm = dtTemp.Rows[i]["件单位"].ToString();
                }
                if (dtTemp.Rows[i]["整件数"] == DBNull.Value || string.IsNullOrWhiteSpace(dtTemp.Rows[i]["整件数"].ToString()))
                {
                    packageQuantity = 0;
                }
                else
                {
                    packageQuantity = Convert.ToInt32(dtTemp.Rows[i]["整件数"]);
                }
                if (dtTemp.Rows[i]["零头"] == DBNull.Value || string.IsNullOrWhiteSpace(dtTemp.Rows[i]["零头"].ToString()))
                {
                    packageOdd = 0;
                }
                else
                {
                    packageOdd = Convert.ToDouble(dtTemp.Rows[i]["零头"]);
                }
                if (dtTemp.Rows[i]["入库量"] == DBNull.Value || string.IsNullOrWhiteSpace(dtTemp.Rows[i]["入库量"].ToString()))
                {
                    receiveQuantity = 0;
                }
                else
                {
                    receiveQuantity = Convert.ToDouble(dtTemp.Rows[i]["入库量"]);
                }
                if (dtTemp.Rows[i]["生产日期"] == DBNull.Value || dtTemp.Rows[i]["生产日期"].ToString().Trim() == "")
                {
                    mfgDate = "";
                }
                else
                {
                    if (!dtTemp.Rows[i]["生产日期"].ToString().Contains("."))
                    {
                        Custom.MsgEx("请按照2020.01.01格式填写！");
                        return;
                    }
                    else
                    {
                        mfgDate = dtTemp.Rows[i]["生产日期"].ToString();
                    }

                }
                if (dtTemp.Rows[i]["到期日期"] == DBNull.Value || dtTemp.Rows[i]["到期日期"].ToString().Trim() == "")
                {
                    expDate = "";
                }
                else
                {
                    if (!dtTemp.Rows[i]["到期日期"].ToString().Contains("."))
                    {
                        Custom.MsgEx("请按照2020.01.01格式填写！");
                        return;
                    }
                    else
                    {
                        expDate = dtTemp.Rows[i]["到期日期"].ToString();
                    }

                }


                if (dtTemp.Columns.Contains("新行"))
                {
                    if (dtTemp.Rows[i]["新行"] != DBNull.Value && !string.IsNullOrWhiteSpace(dtTemp.Rows[i]["新行"].ToString()))
                    {
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
	                                                            [ForeignNumber],	                                                            [BuyerID],ReceiveDate,Guid,Stock,Bin,InternalLotNumber,LotNumber,ManufacturedDate,ExpiredDate,RetestDate,Status,Operator,ReceiveQuantity,IsDirectERP,NumberOfPackages,PackageOdd,PackageSpecification,PackageUM,UnitPrice
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
	                                                            [StockKeeper],
	                                                            [LotNumberAssign],
	                                                            [OrderQuantity],
	                                                            [ItemReceiveType],
	                                                            [Supervisor],
	                                                            [ForeignNumber],
	                                                            [BuyerID],ReceiveDate,Replace(NEWID(),'-',''),'" + dtTemp.Rows[i]["库"].ToString() + "','" + dtTemp.Rows[i]["位"].ToString() + "','" + internalLotNumber + "','" + vendorLotNumber + "','" + mfgDate + "','" + expDate + "','" + dtTemp.Rows[i]["重测日期"].ToString() + "',0,'" + StockUser.UserID + "'," + receiveQuantity + "," + Convert.ToInt32(StockUser.IsDirectERP) + "," + packageQuantity + "," + packageOdd + ",'" + packageSpecification + "','" + packageUm + "',UnitPrice  FROM   PurchaseOrderRecordHistoryByCMF  WHERE Guid = '" + dtTemp.Rows[i]["ParentGuid"].ToString() + "'";
                        sqlList.Add(sqlInsert);
                    }
                    else
                    {
                        string sqlUpdate = "Update PurchaseOrderRecordHistoryByCMF Set InternalLotNumber='" + internalLotNumber + "',LotNumber='" + vendorLotNumber + "',ManufacturedDate='" + mfgDate + "',ExpiredDate='" + expDate + "',Stock='" + dtTemp.Rows[i]["库"].ToString() + "',Bin='" + dtTemp.Rows[i]["位"].ToString() + "',Status=0,Operator='" + StockUser.UserID + "',ReceiveQuantity=" + receiveQuantity + ",NumberOfPackages=" + packageQuantity + ",IsDirectERP=" + Convert.ToInt32(StockUser.IsDirectERP) + ",PackageSpecification='" + packageSpecification + "',PackageOdd=" + packageOdd + ",PackageUM='" + packageUm + "'  Where Guid = '" + dtTemp.Rows[i]["Guid"].ToString() + "'";
                        sqlList.Add(sqlUpdate);
                    }
                }
                else
                {
                    string sqlUpdate = "Update PurchaseOrderRecordHistoryByCMF Set InternalLotNumber='" + internalLotNumber + "',LotNumber='" + vendorLotNumber + "',ManufacturedDate='" + mfgDate + "',ExpiredDate='" + expDate + "',Stock='" + dtTemp.Rows[i]["库"].ToString() + "',Bin='" + dtTemp.Rows[i]["位"].ToString() + "',Status=0,Operator='" + StockUser.UserID + "',ReceiveQuantity=" + receiveQuantity + ",NumberOfPackages=" + packageQuantity + ",IsDirectERP=" + Convert.ToInt32(StockUser.IsDirectERP) + ",PackageSpecification='" + packageSpecification + "',PackageOdd=" + packageOdd + ",PackageUM='" + packageUm + "'  Where Guid = '" + dtTemp.Rows[i]["Guid"].ToString() + "'";
                    sqlList.Add(sqlUpdate);
                }

                string itemNumber = dtTemp.Rows[i]["物料代码"].ToString();
                //如果不是特殊物料，则对字段进行判断
                if (!specialItemList.Contains(itemNumber) && !specialItemDesList.Contains(itemNumber))
                {
                    if (string.IsNullOrEmpty(vendorLotNumber) || string.IsNullOrWhiteSpace(internalLotNumber))
                    {
                        if (MessageBoxEx.Show("当前物料有批号为空，确定要提交么？", "提示", MessageBoxButtons.YesNo) == DialogResult.No)
                        {
                            return;
                        }
                    }
                    if (string.IsNullOrEmpty(expDate))
                    {
                        if (MessageBoxEx.Show("当前物料有到期日期为空，确定要提交么？", "提示", MessageBoxButtons.YesNo) == DialogResult.No)
                        {
                            return;
                        }
                    }
                    if (string.IsNullOrEmpty(mfgDate))
                    {
                        if (MessageBoxEx.Show("当前物料有生产日期为空，确定要提交么？", "提示", MessageBoxButtons.YesNo) == DialogResult.No)
                        {
                            return;
                        }
                    }
                    if (string.IsNullOrEmpty(packageUm))
                    {
                        if (MessageBoxEx.Show("当前物料有件数单位为空，确定要提交么？", "提示", MessageBoxButtons.YesNo) == DialogResult.No)
                        {
                            return;
                        }
                    }
                    if (packageQuantity == 0)
                    {
                        if (MessageBoxEx.Show("当前物料有件数为0，确定要提交么？", "提示", MessageBoxButtons.YesNo) == DialogResult.No)
                        {
                            return;
                        }
                    }
                    if (string.IsNullOrWhiteSpace(packageSpecification))
                    {
                        if (MessageBoxEx.Show("当前物料有包装规则为空，确定要提交么？", "提示", MessageBoxButtons.YesNo) == DialogResult.No)
                        {
                            return;
                        }
                    }
                }
            }

            if (SQLHelper.BatchExecuteNonQuery(GlobalSpace.FSDBConnstr, sqlList))
            {
                Custom.MsgEx("提交成功！");
                if (FOItemKeeperList.Contains(StockUser.UserID))
                {
                    dgvPODetail.DataSource = GetVendorPOForeignItemsDetail(9);
                }
                else
                {
                    dgvPODetail.DataSource = GetVendorPOItemsDetail(9);
                }
            }
            else
            {
                Custom.MsgEx("提交失败！");
            }
        }
        //更新订单状态
        private bool UpdatePOStatus(string guid, int status)
        {
            string sqlUpdate = @"Update PurchaseOrdersByCMF Set POStatus = " + status + " Where Guid='" + guid + "'";
            if (SQLHelper.ExecuteNonQuery(GlobalSpace.FSDBConnstr, sqlUpdate))
            {
                return true;
            }
            return false;
        }
        //仓库更新订单中物料的状态和信息  

        //通过订单号获取订单中物料详情
        private DataTable GetVendorPOItemsDetailFS(string ponumber, int status, string type)
        {
            DataTable dtTemp = null;
            string date = DateTime.Now.ToString("yyMMdd");

            string strSqlCheck = @"Select Id From  PurchaseOrderRecordHistoryByCMF Where PONumber='" + ponumber + "'";
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
                                                T1.ActualReceiveQuantity AS 入库数量,
                                                T1.LotNumber AS 批号,
                                                T1.InternalLotNumber AS 公司批号,
                                                T1.ExpiredDate AS 到期日期      
                                                FROM  PurchaseOrderRecordHistoryByCMF T1 
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


        private void btnPORVFS_Click(object sender, EventArgs e)
        {
            if (dgvPODetailFS.Rows.Count == 0)
            {
                Custom.MsgEx("当前无可用数据！");
                return;
            }

            if(IsFSView)
            {
                Custom.MsgEx("当前查询显示的数据不允许写入四班！");
                return;
            }

            DataTable dt = (DataTable)dgvPODetailFS.DataSource;

            DataTable dtTemp = new DataTable();

            for (int i = 0; i < dt.Columns.Count; i++)
            {
                dtTemp.Columns.Add(dt.Columns[i].ColumnName);
            }


            foreach (DataGridViewRow dgvr in dgvPODetailFS.Rows)
            {
                if (Convert.ToBoolean(dgvr.Cells["Check2"].Value) && dgvr.Cells["Status"].Value.ToString() == "1")
                {
                    dtTemp.Rows.Add((dgvr.DataBoundItem as DataRowView).Row.ItemArray);
                }
            }

            if (dtTemp.Rows.Count == 0)
            {
                MessageBoxEx.Show("当前未选择写入行！", "提示");
                return;
            }

            FSFunctionLib.FSConfigFileInitialize(GlobalSpace.fsconfigfilepath, FSUserID, FSUserPassword);
            List<string> errorList = new List<string>();
            List<string> specialItemList = SQLHelper.GetList(GlobalSpace.FSDBConnstr, "Select ItemNumber  From PurchaseDepartmentStockSpecialItem", "ItemNumber");
            foreach (DataRow dr in dtTemp.Rows)
            {
                string strReturn = string.Empty;
                string guid = dr["Guid"].ToString();
                int gsID = Convert.ToInt32(dr["GSID"]);

                //GUID STATUS=1 确认
                if (SQLHelper.GetDataTable(GlobalSpace.FSDBConnstr, "Select *  From PurchaseOrderRecordHistoryByCMF Where Guid='" + guid + "' and Status = 1").Rows.Count != 1)
                    continue;
                //如果是五金辅助材料的库管员
                if (StockUser.Type == "A")
                {
                    if (PORVA(dr, out strReturn))
                    {
                        //更新订单中物料状态为已入库
                        

                        string sqlUpdate = @"Update PurchaseOrderRecordHistoryByCMF Set Status = 2,FSOperateDateTime = '" + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss") + "',FSOperateDate='" + DateTime.Now.ToString("yyyy-MM-dd") + "',FSOperator='"+StockUser.UserID+"' Where Guid='" + guid + "'";
                        try
                        {
                            if (!SQLHelper.ExecuteNonQuery(GlobalSpace.FSDBConnstr, sqlUpdate))
                            {
                                dgvPODetailFS.DataSource = GetReceivedRecordByStatus(1);
                                dgvPODetailFS.Columns["Guid"].Visible = false;
                                dgvPODetailFS.Columns["Status"].Visible = false;
                            }
                            else
                            {
                                dgvPODetailFS.DataSource = GetReceivedRecordByStatus(1);
                                dgvPODetailFS.Columns["Guid"].Visible = false;
                                dgvPODetailFS.Columns["Status"].Visible = false;
                            }
                        }
                        catch (Exception ex)
                        {
                            Custom.MsgEx(ex.Message);
                        }
                        if(gsID != 0)
                        {
                            try
                            {
                                SendGSEmail(gsID);
                            }
                            catch (Exception ex2)
                            {
                                Custom.MsgEx(ex2.Message);
                            }
                        }
                      
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(strReturn))
                        {
                            errorList.Add(strReturn);
                        }
                    }
                }
                else if (specialItemList.Contains(dr["物料代码"].ToString()) || (dr["ItemReceiveType"].ToString() == "M" && dr["物料代码"].ToString().Substring(0, 1) == "A"))
                {
                    if (PORVM(dr, out strReturn))
                    {
                        //更新订单中物料状态为已入库
                        string sqlUpdate = @"Update PurchaseOrderRecordHistoryByCMF Set Status = 2,FSOperateDateTime = '" + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss") + "',FSOperateDate='" + DateTime.Now.ToString("yyyy-MM-dd") + "',FSOperator='" + StockUser.UserID + "' Where Guid='" + guid + "'";
                        try
                        {
                            if (!SQLHelper.ExecuteNonQuery(GlobalSpace.FSDBConnstr, sqlUpdate))
                            {
                                dgvPODetailFS.DataSource = GetReceivedRecordByStatus(1);
                                dgvPODetailFS.Columns["Guid"].Visible = false;
                                dgvPODetailFS.Columns["Status"].Visible = false;
                            }
                            else
                            {
                                dgvPODetailFS.DataSource = GetReceivedRecordByStatus(1);
                                dgvPODetailFS.Columns["Guid"].Visible = false;
                                dgvPODetailFS.Columns["Status"].Visible = false;
                            }
                        }
                        catch (Exception ex)
                        {
                            Custom.MsgEx(ex.Message);
                        }
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(strReturn))
                        {
                            errorList.Add(strReturn);
                        }
                    }
                }
                else
                {
                    if (PORV(dr, out strReturn))
                    {
                        //更新订单中物料状态为已入库
                        string sqlUpdate = @"Update PurchaseOrderRecordHistoryByCMF Set Status = 2,FSOperateDateTime = '" + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss") + "',FSOperateDate='" + DateTime.Now.ToString("yyyy-MM-dd") + "',FSOperator='" + StockUser.UserID + "' Where Guid='" + guid + "'";
                        try
                        {
                            if (!SQLHelper.ExecuteNonQuery(GlobalSpace.FSDBConnstr, sqlUpdate))
                            {
                                dgvPODetailFS.DataSource = GetReceivedRecordByStatus(1);
                                dgvPODetailFS.Columns["Guid"].Visible = false;
                                dgvPODetailFS.Columns["Status"].Visible = false;
                            }
                            else
                            {
                                dgvPODetailFS.DataSource = GetReceivedRecordByStatus(1);
                                dgvPODetailFS.Columns["Guid"].Visible = false;
                                dgvPODetailFS.Columns["Status"].Visible = false;
                            }
                        }
                        catch (Exception ex)
                        {
                            Custom.MsgEx(ex.Message);
                        }
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(strReturn))
                        {
                            errorList.Add(strReturn);
                        }
                    }
                }

            }

            FSFunctionLib.FSExit();
            if (errorList.Count > 0)
            {
                Custom.MsgEx("四班写入出现报错，请查看报错内容！");
            }
            else
            {
                Custom.MsgEx("四班写入成功！");
            }
        }

        //发送邮件测试
        private void SendGSEmail(int id)
        {
            string str = "SELECT WorkCenter,ItemDescription,ItemNumber FROM SolidBuyList WHERE ID="+id+"";
            DataTable dt = SQLHelper.GetDataTable(GlobalSpace.RYData, str);
            if (dt.Rows.Count > 0)
            {
                SmtpClient client = new SmtpClient("192.168.8.3", 25);
                client.Credentials = CredentialCache.DefaultNetworkCredentials;
                client.Credentials = new NetworkCredential("Erp@reyoung.com", "ERP1075+-*/");
                MailMessage mmsg = new MailMessage();
                mmsg.From = new MailAddress("Erp@reyoung.com");
                string[] sp = dt.Rows[0][0].ToString().Split('|');
                for (int i = 0; i < sp.Length; i++)
                {
                    string strMail = "SELECT FoxMail FROM WorkMails WHERE WorkCenter='" + sp[i] + "'";
                    DataTable dtmail = SQLHelper.GetDataTable(GlobalSpace.RYData, strMail);
                    if (dtmail.Rows.Count > 0)
                    {
                        mmsg.To.Add(dtmail.Rows[0][0].ToString().Trim());
                    }
                }
                mmsg.To.Add("chenkai@reyoung.com");
                mmsg.To.Add("caohongling@reyoung.com");
                mmsg.Subject = "" + dt.Rows[0][1] + "采购物料到货通知";
                mmsg.Body = "各位领导您好" + "" + "\n" +
                    "编码：" + dt.Rows[0][2] + "" + "\n" +
                    "固水事业部采购" + dt.Rows[0][1] + "物料已确认到货，请注意查收!" + "" + "\n" +
                    "此邮件为系统邮件，请勿回复!";
                client.Send(mmsg);
                mmsg.Dispose();
            }
        }

        //原辅料特殊物料：A类试剂、鑫泉生产的原料，库管员操作入库，只走账
        private bool PORVM(DataRow dr, out string strError)
        {
            strError = "";
            PORV01 porv01 = new PORV01();
            porv01.PONumber.Value = dr["采购单号"].ToString();
            porv01.POLineNumber.Value = dr["行号"].ToString();
            porv01.POLineUM.Value = dr["单位"].ToString();
            porv01.POReceiptActionType.Value = "R";
            porv01.Stockroom1.Value = dr["库"].ToString();
            porv01.Bin1.Value = dr["位"].ToString();
            porv01.InventoryCategory1.Value = "O";
            porv01.InspectionCode1.Value = "G";//;
            porv01.ReceiptQuantityMove1.Value = dr["入库数量"].ToString();
            porv01.POLineType.Value = "P";
            porv01.ItemNumber.Value = dr["物料代码"].ToString();
            porv01.PromisedDate.Value = dr["承诺交货日"].ToString();
            porv01.NewLot.Value = "Y";
            if (dr["LotNumberAssign"].ToString() == "C" || dr["物料代码"].ToString().Contains("M"))
            {
                porv01.LotNumberAssignmentPolicy.Value = "C";
                if (dr["厂家批号"] == DBNull.Value || string.IsNullOrEmpty(dr["厂家批号"].ToString()))
                {
                    porv01.LotNumberDefault.Value = "";
                    porv01.LotNumber.Value = "";
                }
                else
                {
                    porv01.LotNumberDefault.Value = dr["厂家批号"].ToString().ToUpper();
                    porv01.LotNumber.Value = dr["厂家批号"].ToString().ToUpper();
                }

                string mName = dr["生产商名"].ToString().Trim();
                if (regEnglish.IsMatch(mName))
                {
                    if (mName.Length > 34)
                    {
                        porv01.LotDescription.Value = mName.Substring(0, 34).Replace(" ", "");
                    }
                    else
                    {
                        porv01.LotDescription.Value = mName.Replace(" ", "");
                    }
                }
                else
                {
                    if (mName.Length > 17)
                    {
                        porv01.LotDescription.Value = mName.Substring(0, 17);
                    }
                    else
                    {
                        porv01.LotDescription.Value = mName;
                    }
                }

                porv01.LotUserDefined5.Value = dr["生产商码"].ToString();


                if (dr["到期日期"] != DBNull.Value && !string.IsNullOrWhiteSpace(dr["到期日期"].ToString()))
                {
                    string expiredDate = dr["到期日期"].ToString();
                    if (expiredDate.Length == 8)//20200527格式
                    {
                        porv01.LotExpirationDate.Value = expiredDate.Substring(4, 4) + expiredDate.Substring(2, 2);
                    }
                    else if (expiredDate.Length == 10)//2020.05.27格式
                    {
                        porv01.LotExpirationDate.Value = expiredDate.Substring(5, 2) + expiredDate.Substring(8, 2) + expiredDate.Substring(2, 2);
                    }
                    else
                    {
                        //此处的日期格式不符合要求，故意赋值，在写入四班时会报错，用以提示具体报错问题
                        porv01.LotExpirationDate.Value = expiredDate;
                    }
                }
                else
                {
                    porv01.LotExpirationDate.Value = DateTime.Now.AddYears(2).ToString("MMddyy");
                }
            }
            else
            {
                porv01.LotNumberAssignmentPolicy.Value = "N";
            }
            porv01.POReceiptDate.Value = DateTime.Now.ToString("MMddyy");

            //GB2312.GetString(ISO88591.GetBytes(myDR["CustomerName"].ToString()));
            string transactionString = porv01.GetString(TransactionStringFormat.fsCDF);
            string str = GB2312.GetString(ISO88591.GetBytes(transactionString));
            //        MessageBox.Show(str);

            tbTest.Text = str;


            strError = "111";
            //         return false;
            if (FSFunctionLib.fstiClient.ProcessId(porv01, null))
            {
                return true;
            }

            FSTIError error = FSFunctionLib.fstiClient.TransactionError;
            DumpErrorObject(porv01, error, listResult);
            CommonOperate.WriteFSErrorLog("PORV", porv01, error, FSUserID, dr["采购单号"].ToString() + " " + dr["行号"].ToString());
            strError = dr["采购单号"].ToString() + " " + dr["行号"].ToString() + " ";
            return false;
        }
        private bool PORVMM(DataRow dr, out string strError)
        {
            strError = "";
            PORV01 porv01 = new PORV01();
            porv01.PONumber.Value = dr["采购单号"].ToString();
            porv01.POLineNumber.Value = dr["行号"].ToString();
            porv01.POLineUM.Value = dr["单位"].ToString();
            porv01.POReceiptActionType.Value = "R";
            porv01.Stockroom1.Value = dr["库"].ToString();
            porv01.Bin1.Value = dr["位"].ToString();
            if (dr["检验"].ToString().ToUpper() == "Y")
            {
                porv01.InventoryCategory1.Value = "I";
                //          porv01.InspectionCode1.Value = "N";//;
            }
            else
            {
                porv01.InventoryCategory1.Value = "O";
                //      porv01.InspectionCode1.Value = "G";//;
            }
            porv01.ReceiptQuantityMove1.Value = dr["入库数量"].ToString();
            porv01.POLineType.Value = "P";
            porv01.ItemNumber.Value = dr["物料代码"].ToString();
            porv01.NewLot.Value = "Y";
            if (dr["LotNumberAssign"].ToString() == "C" || dr["物料代码"].ToString().Contains("P") || dr["物料代码"].ToString().Contains("M"))
            {
                porv01.LotNumberAssignmentPolicy.Value = "C";
                if (dr["公司批号"] == DBNull.Value || string.IsNullOrEmpty(dr["公司批号"].ToString()))
                {
                    porv01.LotNumberDefault.Value = dr["厂家批号"].ToString().ToUpper();
                    porv01.LotNumber.Value = dr["厂家批号"].ToString().ToUpper();
                }
                else if (dr["厂家批号"] == DBNull.Value || string.IsNullOrEmpty(dr["厂家批号"].ToString()))
                {
                    porv01.LotNumberDefault.Value = dr["公司批号"].ToString().ToUpper();
                    porv01.LotNumber.Value = dr["公司批号"].ToString().ToUpper();
                }
                else
                {
                    porv01.LotNumberDefault.Value = dr["厂家批号"].ToString().ToUpper();
                    porv01.LotNumber.Value = dr["厂家批号"].ToString().ToUpper();
                }
                porv01.VendorLotNumber.Value = dr["公司批号"].ToString().ToUpper();
                string mName = dr["生产商名"].ToString();
                if (regEnglish.IsMatch(mName))
                {
                    if (mName.Length > 34)
                    {
                        porv01.LotDescription.Value = mName.Substring(0, 34).Replace(" ", "");
                    }
                    else
                    {
                        porv01.LotDescription.Value = mName.Replace(" ", "");
                    }
                }
                else
                {
                    if (mName.Length > 17)
                    {
                        porv01.LotDescription.Value = mName.Substring(0, 17);
                    }
                    else
                    {
                        porv01.LotDescription.Value = mName;
                    }
                }

                porv01.LotUserDefined5.Value = dr["生产商码"].ToString();
                //       porv01.POReceiptDate.Value = DateTime.Now.ToString("MMddyy");//此处不确定
                if (dr["重测日期"] != DBNull.Value && !string.IsNullOrEmpty(dr["重测日期"].ToString()))
                {
                    porv01.RetestDate.Value = dr["重测日期"].ToString();
                }
                if (dr["到期日期"] != DBNull.Value && !string.IsNullOrWhiteSpace(dr["到期日期"].ToString()))
                {
                    string expiredDate = dr["到期日期"].ToString();
                    if (expiredDate.Length == 8)//20200527格式
                    {
                        porv01.LotExpirationDate.Value = expiredDate.Substring(4, 4) + expiredDate.Substring(2, 2);
                    }
                    else if (expiredDate.Length == 10)//2020.05.27格式
                    {
                        porv01.LotExpirationDate.Value = expiredDate.Substring(5, 2) + expiredDate.Substring(8, 2) + expiredDate.Substring(2, 2);
                    }
                    else
                    {
                        //此处的日期格式不符合要求，故意赋值，在写入四班时会报错，用以提示具体报错问题
                        porv01.LotExpirationDate.Value = expiredDate;
                    }
                }
                else
                {
                    porv01.LotExpirationDate.Value = DateTime.Now.AddYears(2).ToString("MMddyy");
                }
            }
            else
            {
                porv01.LotNumberAssignmentPolicy.Value = "N";
            }
            porv01.PromisedDate.Value = dr["承诺交货日"].ToString();
            porv01.POReceiptDate.Value = DateTime.Now.ToString("MMddyy");

            string transactionString = porv01.GetString(TransactionStringFormat.fsCDF);
            //        MessageBox.Show(transactionString);

            tbTest.Text = transactionString;
            strError = "111";
            //  return false;
            if (FSFunctionLib.fstiClient.ProcessId(porv01, null))
            {
                /*
                listResult.Items.Add("Success:");
                listResult.Items.Add("");
                listResult.Items.Add(FSFunctionLib.fstiClient.CDFResponse);
                */
                return true;
            }
            FSTIError error = FSFunctionLib.fstiClient.TransactionError;
            DumpErrorObject(porv01, error, listResult);
            CommonOperate.WriteFSErrorLog("PORV", porv01, error, FSUserID, dr["采购单号"].ToString() + " " + dr["行号"].ToString());
            strError = dr["采购单号"].ToString() + " " + dr["行号"].ToString() + " ";
            return false;
        }
        //五金库管员操作入库
        private bool PORVA(DataRow dr, out string strError)
        {
            strError = "";
            PORV01 porv01 = new PORV01();
            porv01.PONumber.Value = dr["采购单号"].ToString();
            porv01.POLineNumber.Value = dr["行号"].ToString();
            porv01.POLineUM.Value = dr["单位"].ToString();
            porv01.POReceiptActionType.Value = "R";
            porv01.Stockroom1.Value = dr["库"].ToString();
            porv01.Bin1.Value = dr["位"].ToString();
            porv01.InventoryCategory1.Value = "O";
            porv01.ReceiptQuantityMove1.Value = dr["入库数量"].ToString();
            porv01.POLineType.Value = "P";
            porv01.ItemNumber.Value = dr["物料代码"].ToString();
            porv01.PromisedDate.Value = dr["承诺交货日"].ToString();

            //  MessageBox.Show(porv01.ReceiptQuantityMove1.Value.ToString());
            //    MessageBox.Show(porv01.LotDescription.Value.ToString()+"-Length:"+ porv01.LotDescription.Value.Length);

            //GB2312.GetString(ISO88591.GetBytes(myDR["CustomerName"].ToString()));
            /*   string transactionString = porv01.GetString(TransactionStringFormat.fsCDF);
               string str = GB2312.GetString(ISO88591.GetBytes(transactionString));
               MessageBox.Show(str);

               tbTest.Text = str;*/


            /* strError = "111";
             return false;*/
            if (FSFunctionLib.fstiClient.ProcessId(porv01, null))
            {
                /*
                listResult.Items.Add("Success:");
                listResult.Items.Add("");
                listResult.Items.Add(FSFunctionLib.fstiClient.CDFResponse);
                */
                return true;
            }

            FSTIError error = FSFunctionLib.fstiClient.TransactionError;
            DumpErrorObject(porv01, error, listResult);
            CommonOperate.WriteFSErrorLog("PORV", porv01, error, FSUserID, dr["采购单号"].ToString() + " " + dr["行号"].ToString());
            strError = dr["采购单号"].ToString() + " " + dr["行号"].ToString() + " ";
            return false;
        }
        //库管员操作入库
        private bool PORV(DataRow dr, out string strError)
        {
            //            MessageBox.Show("AAA");
            strError = "";
            PORV01 porv01 = new PORV01();
            porv01.PONumber.Value = dr["采购单号"].ToString();
            porv01.POLineNumber.Value = dr["行号"].ToString();
            porv01.POLineUM.Value = dr["单位"].ToString();
            porv01.POReceiptActionType.Value = "R";
            porv01.Stockroom1.Value = dr["库"].ToString();
            porv01.Bin1.Value = dr["位"].ToString();
            if (dr["检验"].ToString().ToUpper() == "Y")
            {
                porv01.InventoryCategory1.Value = "I";
                //          porv01.InspectionCode1.Value = "N";//;
            }
            else
            {
                porv01.InventoryCategory1.Value = "O";

            }
            porv01.ReceiptQuantityMove1.Value = dr["入库数量"].ToString();
            porv01.POLineType.Value = "P";
            porv01.ItemNumber.Value = dr["物料代码"].ToString();
            porv01.NewLot.Value = "Y";
            if (dr["LotNumberAssign"].ToString() == "C" || dr["物料代码"].ToString().Contains("P") || dr["物料代码"].ToString().Contains("M"))
            {
                porv01.LotNumberAssignmentPolicy.Value = "C";
                if (dr["公司批号"] == DBNull.Value || string.IsNullOrEmpty(dr["公司批号"].ToString()))
                {
                    porv01.LotNumberDefault.Value = dr["厂家批号"].ToString().ToUpper();
                    porv01.LotNumber.Value = dr["厂家批号"].ToString().ToUpper();
                }
                else if (dr["厂家批号"] == DBNull.Value || string.IsNullOrEmpty(dr["厂家批号"].ToString()))
                {
                    porv01.LotNumberDefault.Value = dr["公司批号"].ToString().ToUpper();
                    porv01.LotNumber.Value = dr["公司批号"].ToString().ToUpper();
                }
                else
                {
                    porv01.LotNumberDefault.Value = dr["厂家批号"].ToString().ToUpper();
                    porv01.LotNumber.Value = dr["厂家批号"].ToString().ToUpper();
                }
                porv01.VendorLotNumber.Value = dr["公司批号"].ToString().ToUpper();
                string mName = dr["生产商名"].ToString().Trim();
                if (regEnglish.IsMatch(mName))
                {
                    if (mName.Length > 34)
                    {
                        porv01.LotDescription.Value = mName.Substring(0, 34).Replace(" ", "");
                    }
                    else
                    {
                        porv01.LotDescription.Value = mName.Replace(" ", "");
                    }
                }
                else
                {
                    if (mName.Length > 17)
                    {
                        porv01.LotDescription.Value = mName.Substring(0, 17);
                    }
                    else
                    {
                        porv01.LotDescription.Value = mName;
                    }
                }

                porv01.LotUserDefined5.Value = dr["生产商码"].ToString();
                //       porv01.POReceiptDate.Value = DateTime.Now.ToString("MMddyy");//此处不确定
                if (dr["重测日期"] != DBNull.Value && !string.IsNullOrWhiteSpace(dr["重测日期"].ToString()))
                {
                    porv01.RetestDate.Value = dr["重测日期"].ToString();
                }
                string expiredDate = dr["到期日期"].ToString();
                if (expiredDate.Length == 8)//20200527格式
                {
                    porv01.LotExpirationDate.Value = expiredDate.Substring(4, 4) + expiredDate.Substring(2, 2);
                }
                else if (expiredDate.Length == 10)//2020.05.27格式
                {
                    porv01.LotExpirationDate.Value = expiredDate.Substring(5, 2) + expiredDate.Substring(8, 2) + expiredDate.Substring(2, 2);
                }
                else
                {
                    //此处的日期格式不符合要求，故意赋值，在写入四班时会报错，用以提示具体报错问题
                    porv01.LotExpirationDate.Value = expiredDate;
                }
            }
            else
            {
                porv01.LotNumberAssignmentPolicy.Value = "N";
            }
            porv01.PromisedDate.Value = dr["承诺交货日"].ToString();
            porv01.POReceiptDate.Value = DateTime.Now.ToString("MMddyy");
            //  MessageBox.Show(porv01.ReceiptQuantityMove1.Value.ToString());
            //    MessageBox.Show(porv01.LotDescription.Value.ToString()+"-Length:"+ porv01.LotDescription.Value.Length);


            string transactionString = porv01.GetString(TransactionStringFormat.fsCDF);
            //        MessageBox.Show(transactionString);

            tbTest.Text = tbTest.Text + " | " + transactionString;
            strError = "111";
            //  return false;
            if (FSFunctionLib.fstiClient.ProcessId(porv01, null))
            {
                /*
                listResult.Items.Add("Success:");
                listResult.Items.Add("");
                listResult.Items.Add(FSFunctionLib.fstiClient.CDFResponse);
                */
                return true;
            }
            FSTIError error = FSFunctionLib.fstiClient.TransactionError;
            DumpErrorObject(porv01, error, listResult);
            CommonOperate.WriteFSErrorLog("PORV", porv01, error, FSUserID, dr["采购单号"].ToString() + " " + dr["行号"].ToString());
            strError = dr["采购单号"].ToString() + " " + dr["行号"].ToString() + " ";
            return false;
        }
        //库管员操作入库
        private bool PORVTest(DataRow dr, out string strError)
        {
            //            MessageBox.Show("AAA");
            strError = "";
            PORV01 porv01 = new PORV01();
            porv01.PONumber.Value = dr["采购单号"].ToString();
            porv01.POLineNumber.Value = dr["行号"].ToString();
            porv01.POLineUM.Value = dr["单位"].ToString();
            porv01.POReceiptActionType.Value = "R";
            porv01.Stockroom1.Value = dr["库"].ToString();
            porv01.Bin1.Value = dr["位"].ToString();
            if (dr["检验"].ToString().ToUpper() == "Y")
            {
                porv01.InventoryCategory1.Value = "I";
                //          porv01.InspectionCode1.Value = "N";//;
            }
            else
            {
                porv01.InventoryCategory1.Value = "O";

            }
            porv01.ReceiptQuantityMove1.Value = dr["入库数量"].ToString();
            porv01.POLineType.Value = "P";
            porv01.ItemNumber.Value = dr["物料代码"].ToString();
            porv01.NewLot.Value = "Y";
            if (dr["LotNumberAssign"].ToString() == "C" || dr["物料代码"].ToString().Contains("P") || dr["物料代码"].ToString().Contains("M"))
            {
                porv01.LotNumberAssignmentPolicy.Value = "C";
                if (dr["公司批号"] == DBNull.Value || string.IsNullOrEmpty(dr["公司批号"].ToString()))
                {
                    porv01.LotNumberDefault.Value = dr["厂家批号"].ToString().ToUpper();
                    porv01.LotNumber.Value = dr["厂家批号"].ToString().ToUpper();
                }
                else if (dr["厂家批号"] == DBNull.Value || string.IsNullOrEmpty(dr["厂家批号"].ToString()))
                {
                    porv01.LotNumberDefault.Value = dr["公司批号"].ToString().ToUpper();
                    porv01.LotNumber.Value = dr["公司批号"].ToString().ToUpper();
                }
                else
                {
                    porv01.LotNumberDefault.Value = dr["厂家批号"].ToString().ToUpper();
                    porv01.LotNumber.Value = dr["厂家批号"].ToString().ToUpper();
                }
                porv01.VendorLotNumber.Value = dr["公司批号"].ToString().ToUpper();
                string mName = dr["生产商名"].ToString();
                if (regEnglish.IsMatch(mName))
                {
                    if (mName.Length > 34)
                    {
                        porv01.LotDescription.Value = mName.Substring(0, 34).Replace(" ", "");
                    }
                    else
                    {
                        porv01.LotDescription.Value = mName.Replace(" ", "");
                    }
                }
                else
                {
                    if (mName.Length > 17)
                    {
                        porv01.LotDescription.Value = mName.Substring(0, 17);
                    }
                    else
                    {
                        porv01.LotDescription.Value = mName;
                    }
                }

                porv01.LotUserDefined5.Value = dr["生产商码"].ToString();
                //       porv01.POReceiptDate.Value = DateTime.Now.ToString("MMddyy");//此处不确定
                if (dr["重测日期"] != DBNull.Value && !string.IsNullOrEmpty(dr["重测日期"].ToString()))
                {
                    porv01.RetestDate.Value = dr["重测日期"].ToString();
                }
                string expiredDate = dr["到期日期"].ToString();
                if (expiredDate.Length == 8)//20200527格式
                {
                    porv01.LotExpirationDate.Value = expiredDate.Substring(4, 4) + expiredDate.Substring(2, 2);
                }
                else if (expiredDate.Length == 10)//2020.05.27格式
                {
                    porv01.LotExpirationDate.Value = expiredDate.Substring(5, 2) + expiredDate.Substring(8, 2) + expiredDate.Substring(2, 2);
                }
                else
                {
                    //此处的日期格式不符合要求，故意赋值，在写入四班时会报错，用以提示具体报错问题
                    porv01.LotExpirationDate.Value = expiredDate;
                }
            }
            else
            {
                porv01.LotNumberAssignmentPolicy.Value = "N";
            }
            porv01.PromisedDate.Value = dr["承诺交货日"].ToString();
            porv01.POReceiptDate.Value = DateTime.Now.ToString("MMddyy");
            //  MessageBox.Show(porv01.ReceiptQuantityMove1.Value.ToString());
            //    MessageBox.Show(porv01.LotDescription.Value.ToString()+"-Length:"+ porv01.LotDescription.Value.Length);


            string transactionString = porv01.GetString(TransactionStringFormat.fsCDF);
            MessageBox.Show(transactionString);

            tbTest.Text = tbTest.Text + " | " + transactionString;
            strError = "111";
            return false;

        }
        //测试FS错误输出
        private void DumpErrorObject(ITransaction transaction, FSTIError fstiErrorObject, ListBox listResult)
        {
            listResult.Items.Add("Transaction Error:");
            listResult.Items.Add("");
            listResult.Items.Add(String.Format("Transaction: {0}", transaction.Name));
            if (string.IsNullOrEmpty(GlobalSpace.fsTestconfigfilepath))
            {
                string str = GB2312.GetString(ISO88591.GetBytes(fstiErrorObject.Description));
                listResult.Items.Add(String.Format("Description: {0}", str));
            }
            else
            {
                string str = GB2312.GetString(ISO88591.GetBytes(fstiErrorObject.Description));
                listResult.Items.Add(String.Format("Description: {0}", str));
            }

            //         listResult.Items.Add(String.Format("Description: {0}", fstiErrorObject.Description));

            listResult.Items.Add(String.Format("MessageFound: {0} ", fstiErrorObject.MessageFound));
            listResult.Items.Add(String.Format("MessageID: {0} ", fstiErrorObject.MessageID));
            listResult.Items.Add(String.Format("MessageSource: {0} ", fstiErrorObject.MessageSource));
            listResult.Items.Add(String.Format("Number: {0} ", fstiErrorObject.Number));
            listResult.Items.Add(String.Format("Fields in Error: {0} ", fstiErrorObject.NumberOfFieldsInError));
            for (int i = 0; i < fstiErrorObject.NumberOfFieldsInError; i++)
            {
                int field = fstiErrorObject.GetFieldNumber(i);
                listResult.Items.Add(String.Format("Field[{0}]: {1}", i, field));
                ITransactionField myField = transaction.get_Field(field);
                listResult.Items.Add(String.Format("Field name: {0}", myField.Name));
            }
        }

        //库管员操作退库
        private bool PORVX(DataRow dr)
        {
            bool bSucceed = false;

            PORV02 porv02 = new PORV02();
            porv02.PONumber.Value = dr["采购单号"].ToString();
            porv02.POLineNumber.Value = dr["行号"].ToString();
            porv02.POReceiptActionType.Value = "X";
            porv02.ItemNumber.Value = dr["物料代码"].ToString();
            porv02.LotNumber.Value = dr["公司内部批号"].ToString();
            porv02.POLineType.Value = "P";
            porv02.LocationReverseQuantity1.Value = dr["退回数量"].ToString();
            porv02.Stockroom1.Value = dr["库"].ToString();
            porv02.Bin1.Value = dr["位"].ToString();
            porv02.InventoryCategory1.Value = dr["IC"].ToString();
            porv02.PromisedDate.Value = dr["承诺交货日期"].ToString();
            if (dr["物料代码"].ToString().Substring(0, 1) == "A")
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
            if (e.KeyChar == (char)13)
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
            IsNeedToConfirm = true;
            if (FOItemKeeperList.Contains(StockUser.UserID))
            {
                dgvPODetail.DataSource = GetVendorPOForeignItemsDetail(9);
            }
            else
            {
                dgvPODetail.DataSource = GetVendorPOItemsDetail(9);
            }

            dgvPODetail.Columns["Guid"].Visible = false;
            dgvPODetail.Columns["ParentGuid"].Visible = false;
            dgvPODetail.Columns["InspectionPeriod"].Visible = false;
            dgvPODetail.Columns["StockKeeper"].Visible = false;
            dgvPODetail.Columns["ItemReceiveType"].Visible = false;
            dgvPODetail.Columns["VendorName"].Visible = false;
            dgvPODetail.Columns["VendorNumber"].Visible = false;
       //     dgvPODetail.Columns["库"].Visible = false;
      //      dgvPODetail.Columns["位"].Visible = false;
            //         dgvPODetail.Columns["UnitPrice"].Visible = false;
            dgvPODetail.Columns["入库量"].DefaultCellStyle.BackColor = Color.LightYellow;
            dgvPODetail.Columns["公司批号"].DefaultCellStyle.BackColor = Color.LightYellow;
            dgvPODetail.Columns["厂家批号"].DefaultCellStyle.BackColor = Color.LightYellow;
            dgvPODetail.Columns["到期日期"].DefaultCellStyle.BackColor = Color.LightYellow;
            dgvPODetail.Columns["生产日期"].DefaultCellStyle.BackColor = Color.LightYellow;
            dgvPODetail.Columns["整件数"].DefaultCellStyle.BackColor = Color.LightYellow;
            dgvPODetail.Columns["件单位"].DefaultCellStyle.BackColor = Color.LightYellow;
            dgvPODetail.Columns["零头"].DefaultCellStyle.BackColor = Color.LightYellow;
            dgvPODetail.Columns["包装规格"].DefaultCellStyle.BackColor = Color.LightYellow;

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
	                                        PONumber = '" + ponumber + "' AND POStatus >=6 And POStatus <>99";
            dtTemp = SQLHelper.GetDataTable(GlobalSpace.FSDBConnstr, sqlSelect);
            return dtTemp;
        }


        private void btnItemReturnedRefresh_Click(object sender, EventArgs e)
        {
            GetUnHandledReturnedItem(FSUserID);
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

            FSFunctionLib.FSConfigFileInitialize(GlobalSpace.fsconfigfilepath, FSUserID, FSUserPassword);

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



        private void ShowPODetail(string date, string type)
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



        /*     private void tbPORVBatches_KeyPress(object sender, KeyPressEventArgs e)
             {
                 if(e.KeyChar == (char)13)
                 {
                     if (tbPORVBatches.Text != "" || CommonOperate.IsNumber(tbPORVBatches.Text.Trim()))
                     {
                         btnAddPORVBatches_Click(sender, e);
                     }
                 }
             }*/

        private string GetItemShelfAndRetestDays(string itemNumber)
        {
            string stringReturn = "";
            string sqlSelect = @"SELECT
	                                                        T1.ShelfLifeDays,
	                                                        T1.RetestDays
                                                        FROM
	                                                        _NoLock_FS_ItemLotTraceData T1,
	                                                        _NoLock_FS_Item T2
                                                        WHERE
	                                                        T1.ItemKey = T2.ItemKey
                                                        AND T2.ItemNumber = '" + itemNumber + "'";
            DataTable dt = SQLHelper.GetDataTable(GlobalSpace.FSDBConnstr, sqlSelect);
            if (dt.Rows.Count > 0)
            {
                stringReturn = dt.Rows[0]["ShelfLifeDays"].ToString() + "|" + dt.Rows[0]["RetestDays"].ToString();
            }
            return stringReturn;
        }


        private void btnReceiveRefresh_Click(object sender, EventArgs e)
        {
            IsNeedToSubmit = true;
            dgvPOItemDetailView.DataSource = GetReceivedRecordByStatusAndUserID(0, StockUser.UserID);
            dgvPOItemDetailView.Columns["Guid"].Visible = false;
            dgvPOItemDetailView.Columns["Status"].Visible = false;
            dgvPOItemDetailView.Columns["BuyerID"].Visible = false;
            dgvPOItemDetailView.Columns["FDAFlag"].Visible = false;
            dgvPOItemDetailView.Columns["ItemReceiveType"].Visible = false;
            dgvPOItemDetailView.Columns["StockKeeper"].Visible = false;
            dgvPOItemDetailView.Columns["LotNumberAssign"].Visible = false;
        //    dgvPOItemDetailView.Columns["库"].Visible = false;
        //    dgvPOItemDetailView.Columns["位"].Visible = false;

        }
        //获取当前未提交的入库物料信息
        /*   private DataTable GetReceivedRecordByStatusAndUserID(int status, string userID)
           {
               string sqlSelect = @"SELECT
                                                   Guid,ForeignNumber AS 外贸单号,
                                                   PONumber AS 采购单号,
                                                   VendorNumber AS 供应商码,
                                                   VendorName AS 供应商名,
                                                   ManufacturerNumber AS 生产商码,
                                                   ManufacturerName AS 生产商名,
                                                   LineNumber AS 行号,
                                                   ItemNumber AS 物料代码,
                                                   ItemDescription AS 描述,
                                                   LineUM AS 单位,UnitPrice AS 单价,
                                                   DemandDeliveryDate AS 承诺交货日,
                                                   OrderQuantity AS 采购数量,
                                                   ReceiveQuantity AS 入库数量,
                                                   Stock AS 库,
                                                   Bin AS 位,
                                                   InspectionPeriod AS 检验,
                                                   LotNumber AS 厂家批号,
                                                   InternalLotNumber AS 公司批号,ManufacturedDate AS 生产日期,
                                                   ExpiredDate AS 到期日期,
                                                   Operator AS 库管员,
                                                   ReceiveDate AS 入库日期,NumberOfPackages AS 整件数,PackageOdd AS 零头,PackageSpecification AS 包装规格,PackageUM AS 件数单位,QualityCheckStandard AS 检验标准,Status,LotNumberAssign,BuyerID,FDAFlag,ItemReceiveType,StockKeeper
                                               FROM
                                                   dbo.PurchaseOrderRecordHistoryByCMF  Where Status=" + status + " And Operator = '" + StockUser.UserID + "'";
               // MessageBox.Show(StockUser.Type);
               return SQLHelper.GetDataTable(GlobalSpace.FSDBConnstr, sqlSelect);
           }*/
        private DataTable GetReceivedRecordByStatusAndUserID(int status, string userID)
        {
            string sqlSelect = @"SELECT
	                                            Guid,ForeignNumber AS 外贸单号,
	                                            PONumber AS 采购单号,
	                                            VendorNumber AS 供应商码,
	                                            VendorName AS 供应商名,
	                                            ManufacturerNumber AS 生产商码,
	                                            ManufacturerName AS 生产商名,
	                                            LineNumber AS 行号,
	                                            ItemNumber AS 物料代码,
	                                            ItemDescription AS 描述,
	                                            LineUM AS 单位,UnitPrice AS 单价,
	                                            DemandDeliveryDate AS 承诺交货日,
                                                OrderQuantity AS 采购数量,
	                                            ReceiveQuantity AS 入库数量,
	                                            Stock AS 库,
	                                            Bin AS 位,
	                                            InspectionPeriod AS 检验,
	                                            LotNumber AS 厂家批号,
	                                            InternalLotNumber AS 公司批号,ManufacturedDate AS 生产日期,
	                                            ExpiredDate AS 到期日期,
	                                            Operator AS 库管员,
	                                            ReceiveDate AS 入库日期,NumberOfPackages AS 整件数,PackageOdd AS 零头,PackageSpecification AS 包装规格,PackageUM AS 件数单位,QualityCheckStandard AS 检验标准,Status,LotNumberAssign,BuyerID,FDAFlag,ItemReceiveType,StockKeeper,
                                            Checker as 复核人,Receiver as 接收请验人,Conclusion as 结论,ConclusionText as 结论其他内容,IsAnyDeviation as 物料验收过程是否出现偏差,DeviationNumber as 偏差编号,deviationIsClosed as 偏差是否已处理关闭,IsReport as 问题是否已报告,QualityManageIdea as 质量管理部门意见,Sign as 签名,SignDate as 签名日期,IsRequireClean as 是否需要清洁,PollutionSituation as 污染情况,CleanMethod as 清洁方式,IsComplete as 外包装是否完整,
                              DamageSituation as 损坏情况,CauseInvestigation1 as 原因调查1,IsSealed as 外包装是否密封,UnsealedCondition as 不密封情况,CauseInvestigation2 as 原因调查2,IsAnyMaterialWithPollutionRisk as 运输工具内是否存在造成污染交叉污染的物料,IsAnyProblemAffectedMaterialQuality as 是否有其他可能影响物料质量的问题,Question as 问题,CauseInvestigation3 as 原因调查3,LotNumberType as 批号类型,IsApprovedVendor as 是否为质量管理部门批准的供应商,StorageCondition as 规定贮存条件,TransportTemperature as 运输条件检查结果,TransportCondition as 运输条件是否符合,TransportationControlRecord as 是否有运输条件控制记录,Shape as 形状是否一致,Colour as 颜色是否一致,Font as 字体是否一致,RoughWeight as 有无毛重,NetWeight as 有无净重,ApprovalNumber as 有无批准文号,ReportType as 报告类型,Report as 有无报告
                                            FROM
	                                            dbo.PurchaseOrderRecordHistoryByCMF  Where Status=" + status + " And Operator = '" + StockUser.UserID + "'";
            // MessageBox.Show(StockUser.Type);//测试 _copy1
            return SQLHelper.GetDataTable(GlobalSpace.FSDBConnstr, sqlSelect);
        }
        //获取当前未提交的入库物料信息
        private DataTable GetReceivedRecordByStatus(int status)
        {
            string sqlSelect = @"SELECT
	                                            Guid,
	                                            PONumber AS 采购单号,
	                                            VendorNumber AS 供应商码,
	                                            VendorName AS 供应商名,
	                                            ManufacturerNumber AS 生产商码,
	                                            ManufacturerName AS 生产商名,
	                                            LineNumber AS 行号,
	                                            ItemNumber AS 物料代码,
	                                            ItemDescription AS 描述,
	                                            LineUM AS 单位,
	                                            DemandDeliveryDate AS 承诺交货日,
	                                            ReceiveQuantity AS 入库数量,
	                                            Stock AS 库,
	                                            Bin AS 位,
	                                            InspectionPeriod AS 检验,
	                                            LotNumber AS 厂家批号,
	                                            InternalLotNumber AS 公司批号,
	                                            ExpiredDate AS 到期日期,
	                                            RetestDate AS 重测日期,
	                                            Operator AS 库管员,
	                                            ReceiveDate AS 入库日期,Status,LotNumberAssign,ItemReceiveType,GSID
                                            FROM
	                                            dbo.PurchaseOrderRecordHistoryByCMF  Where Status=" + status + "  ";
            string sqlCriteria = string.Empty;
            if (StockUser.IsDirectERP == "0")
            {
                sqlCriteria = @" And ItemReceiveType = '" + StockUser.Type + "' And IsDirectERP = 0 ";
            }
            else if (StockUser.IsDirectERP == "1" && StockUser.Type == "A")
            {
                sqlCriteria = @" And LEFT(PONumber,2) = 'PA' And IsDirectERP = 1 ";
            }
            else if (StockUser.IsDirectERP == "1" && StockUser.Type != "A")
            {
                sqlCriteria = @" And Operator = '" + StockUser.UserID + "' And IsDirectERP = 1 ";
            }

            return SQLHelper.GetDataTable(GlobalSpace.FSDBConnstr, sqlSelect + sqlCriteria);
        }
        //获取当前未提交的入库物料信息
        private DataTable GetReceivedRecordByPONumber(string itemDesc)
        {
            string sqlSelect = @"SELECT
	                                            Guid,StockKeeper AS 库管员,
	                                            PONumber AS 采购单号,
	                                            VendorNumber AS 供应商码,
	                                            VendorName AS 供应商名,
	                                            ManufacturerNumber AS 生产商码,
	                                            ManufacturerName AS 生产商名,
	                                            LineNumber AS 行号,
	                                            ItemNumber AS 物料代码,
	                                            ItemDescription AS 描述,
	                                            LineUM AS 单位,
	                                            DemandDeliveryDate AS 承诺交货日,
	                                            ReceiveQuantity AS 入库数量,
	                                            Stock AS 库,
	                                            Bin AS 位,
	                                            InspectionPeriod AS 检验,
	                                            LotNumber AS 厂家批号,
	                                            InternalLotNumber AS 公司批号,
	                                            ExpiredDate AS 到期日期,
	                                            RetestDate AS 重测日期,
	                                            Operator AS 操作者,
	                                            ReceiveDate AS 入库日期,Status
                                            FROM
	                                            dbo.PurchaseOrderRecordHistoryByCMF  Where ItemDescription like '%" + itemDesc + "%' And Status = 9";
            return SQLHelper.GetDataTable(GlobalSpace.FSDBConnstr, sqlSelect);
        }
        //获取当前未提交的入库物料信息
        private DataTable GetReceivedRecordByReceivedDate(string date)
        {
            string sqlSelect = @"SELECT
	                                            Guid,
	                                            PONumber AS 采购单号,
	                                            VendorNumber AS 供应商码,
	                                            VendorName AS 供应商名,
	                                            ManufacturerNumber AS 生产商码,
	                                            ManufacturerName AS 生产商名,
	                                            LineNumber AS 行号,
	                                            ItemNumber AS 物料代码,
	                                            ItemDescription AS 描述,
	                                            LineUM AS 单位,
	                                            DemandDeliveryDate AS 承诺交货日,OrderQuantity AS 采购数量,
	                                            ReceiveQuantity AS 入库数量,
	                                            Stock AS 库,
	                                            Bin AS 位,
	                                            InspectionPeriod AS 检验,
	                                            LotNumber AS 厂家批号,
	                                            InternalLotNumber AS 公司批号,
	                                            ExpiredDate AS 到期日期,
	                                            RetestDate AS 重测日期,
	                                            Operator AS 库管员,
	                                            ReceiveDate AS 入库日期,Status,BuyerID,FDAFlag
                                            FROM
	                                            dbo.PurchaseOrderRecordHistoryByCMF  Where ReceiveDate='" + date + "' And Status = 1 And Operator='" + StockUser.UserID + "'";
            return SQLHelper.GetDataTable(GlobalSpace.FSDBConnstr, sqlSelect);
        }
        //获取当前未提交的入库物料信息
        private DataTable GetFSReceivedRecordByUserID(string userID)
        {
            string sqlSelect = @"SELECT
	                                            Guid,ForeignNumber AS 外贸单号,
	                                            PONumber AS 采购单号,
	                                            VendorNumber AS 供应商码,
	                                            VendorName AS 供应商名,
	                                            ManufacturerNumber AS 生产商码,
	                                            ManufacturerName AS 生产商名,
	                                            LineNumber AS 行号,
	                                            ItemNumber AS 物料代码,
	                                            ItemDescription AS 描述,
	                                            LineUM AS 单位,UnitPrice AS 单价,
	                                            DemandDeliveryDate AS 承诺交货日,
                                                OrderQuantity AS 采购数量,
	                                            ReceiveQuantity AS 入库数量,
	                                            Stock AS 库,
	                                            Bin AS 位,
	                                            InspectionPeriod AS 检验,
	                                            LotNumber AS 厂家批号,
	                                            InternalLotNumber AS 公司批号,ManufacturedDate AS 生产日期,
	                                            ExpiredDate AS 到期日期,
	                                            Operator AS 库管员,
	                                            ReceiveDate AS 入库日期,NumberOfPackages AS 整件数,PackageOdd AS 零头,PackageSpecification AS 包装规格,PackageUM AS 件数单位,QualityCheckStandard AS 检验标准,Status,LotNumberAssign,BuyerID,FDAFlag,ItemReceiveType,StockKeeper,
                                            Checker as 复核人,Receiver as 接收请验人,Conclusion as 结论,ConclusionText as 结论其他内容,IsAnyDeviation as 物料验收过程是否出现偏差,DeviationNumber as 偏差编号,deviationIsClosed as 偏差是否已处理关闭,IsReport as 问题是否已报告,QualityManageIdea as 质量管理部门意见,Sign as 签名,SignDate as 签名日期,IsRequireClean as 是否需要清洁,PollutionSituation as 污染情况,CleanMethod as 清洁方式,IsComplete as 外包装是否完整,
                              DamageSituation as 损坏情况,CauseInvestigation1 as 原因调查1,IsSealed as 外包装是否密封,UnsealedCondition as 不密封情况,CauseInvestigation2 as 原因调查2,IsAnyMaterialWithPollutionRisk as 运输工具内是否存在造成污染交叉污染的物料,IsAnyProblemAffectedMaterialQuality as 是否有其他可能影响物料质量的问题,Question as 问题,CauseInvestigation3 as 原因调查3,LotNumberType as 批号类型,IsApprovedVendor as 是否为质量管理部门批准的供应商,StorageCondition as 规定贮存条件,TransportTemperature as 运输条件检查结果,TransportCondition as 运输条件是否符合,TransportationControlRecord as 是否有运输条件控制记录,Shape as 形状是否一致,Colour as 颜色是否一致,Font as 字体是否一致,RoughWeight as 有无毛重,NetWeight as 有无净重,ApprovalNumber as 有无批准文号,ReportType as 报告类型,Report as 有无报告
                                            FROM
	                                            dbo.PurchaseOrderRecordHistoryByCMF  Where Operator='" + userID + "'  And Left(SubmitOperateDateTime,10) >='" + dtpFSHistory.Value.ToString("yyyy-MM-dd") + "' and Status in (1,2)";
            return SQLHelper.GetDataTable(GlobalSpace.FSDBConnstr, sqlSelect);
        }
        //获取当前未提交的入库物料信息
        private DataTable GetReceivedRecordByOperateFSDate(string date)
        {
            string sqlSelect = @"SELECT
	                                            Guid,
	                                            PONumber AS 采购单号,
	                                            VendorNumber AS 供应商码,
	                                            VendorName AS 供应商名,
	                                            ManufacturerNumber AS 生产商码,
	                                            ManufacturerName AS 生产商名,
	                                            LineNumber AS 行号,
	                                            ItemNumber AS 物料代码,
	                                            ItemDescription AS 描述,
	                                            LineUM AS 单位,
	                                            DemandDeliveryDate AS 承诺交货日,
	                                            ReceiveQuantity AS 入库数量,
	                                            Stock AS 库,
	                                            Bin AS 位,
	                                            InspectionPeriod AS 检验,
	                                            LotNumber AS 厂家批号,
	                                            InternalLotNumber AS 公司批号,
	                                            ExpiredDate AS 到期日期,
	                                            RetestDate AS 重测日期,
	                                            Operator AS 库管员,
	                                            ReceiveDate AS 入库日期,Status,FSOperator AS 四班操作
                                            FROM
	                                            dbo.PurchaseOrderRecordHistoryByCMF  Where FSOperateDate='" + date + "' And (FSOperator='" + StockUser.UserID + "' or  FSOperator='" + StockUser.UserID + "|手工')  and Status =2";
            return SQLHelper.GetDataTable(GlobalSpace.FSDBConnstr, sqlSelect);
        }
        private void btnBatchesReceive_Click(object sender, EventArgs e)
        {
            int iIndex = dgvPODetail.SelectedCells[0].RowIndex;
            int iCount = Convert.ToInt32(tbReceiveRecordQuantity.Text.Trim());
            string strGuid = dgvPODetail.Rows[iIndex].Cells["Guid"].Value.ToString();

            DataTable dtSource = (DataTable)dgvPODetail.DataSource;
            if (!dtSource.Columns.Contains("新行"))
            {
                dtSource.Columns.Add("新行");
            }


            if (iCount == 1)
            {
                DataRow[] drs = dtSource.Select("Guid = '" + strGuid + "'");
                DataRow dr = dtSource.NewRow();
                dr.ItemArray = drs[0].ItemArray;
                dr["新行"] = "1";
                dr["Guid"] = Guid.NewGuid().ToString("N");
                dr["ParentGuid"] = strGuid;
                dtSource.Rows.InsertAt(dr, iIndex + 1);
            }
            else if (iCount > 1)
            {
                for (int i = 0; i < iCount; i++)
                {
                    DataRow[] drs = dtSource.Select("Guid = '" + strGuid + "'");
                    DataRow dr = dtSource.NewRow();
                    dr.ItemArray = drs[0].ItemArray;
                    dr["新行"] = "1";
                    dr["Guid"] = Guid.NewGuid().ToString("N");
                    dr["ParentGuid"] = strGuid;
                    dtSource.Rows.InsertAt(dr, iIndex + 1);
                }

            }

        }

        private void tbReceiveRecordQuantity_Click(object sender, EventArgs e)
        {
            tbReceiveRecordQuantity.Text = "";
        }

        private void btnReceiveFind_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(tbItemDesc.Text))
            {
                dgvPOItemDetailView.DataSource = GetReceivedRecordByPONumber(tbItemDesc.Text);
                dgvPOItemDetailView.Columns["Guid"].Visible = false;
                dgvPOItemDetailView.Columns["Status"].Visible = false;
            }
        }



        //获取当前物料的优选库位
        private string GetPreferedStockroom(string itemNumber)
        {
            string sqlSelect = @"Select  PreferredStockroom From _NoLock_FS_Item Where ItemNumber='" + itemNumber + "'";
            return SQLHelper.ExecuteScalar(GlobalSpace.FSDBMRConnstr, sqlSelect).ToString();
        }
        //检查物料是否已有同样批号入库记录
        private int IsExistSameLotNumber(string itemNumber,string lotNumber)
        {
            int i = 0;

            //string sqlSelectCheckPOLineNumber = @"Select Count(HistoryPOReceiptKey) From PORV Where PONumber='" + poNumber+"' And POLineNumber='"+lineNumber+"'";
            string sqlSelectCheckitemNumber = @"Select Count(HistoryPOReceiptKey) From PORV Where itemNumber='" + itemNumber+"' And lotNumber='"+lotNumber+"'";
/*
            if(SQLHelper.Exist(GlobalSpace.FSDBConnstr,sqlSelectCheckPOLineNumber))
            {
                i = 1;
            }*/
            if (SQLHelper.Exist(GlobalSpace.FSDBConnstr, sqlSelectCheckitemNumber))
            {
                i = 1;
            }
            return i;
        }

        //批号存在中文括号
        private bool IsExistChineseBracket(string lotNumber)
        {
            if(lotNumber.Contains("（") || lotNumber.Contains("）"))
            {
                return true;
            }
            return false;
        }

        //判断含有中文汉字
        public bool HasChinese(string str)
        {
            return Regex.IsMatch(str, @"[\u4e00-\u9fa5]");
        }
        //批号中存在中文字符
        private bool IsChineseLetter(string CString)
        {
            bool BoolValue = false;
            for (int i = 0; i < CString.Length; i++)
            {
                if (Convert.ToInt32(Convert.ToChar(CString.Substring(i, 1))) < Convert.ToInt32(Convert.ToChar(128)))
                {
                    BoolValue = false;
                }
                else
                {
                    return BoolValue = true;
                }
            }
            return BoolValue;
        }

        private void btnReceiveSubmit_Click(object sender, EventArgs e)
        {
            if (!IsNeedToSubmit)
            {
                Custom.MsgEx("当前记录状态不允许提交！");
                return;
            }

            DataTable dtOriginal = (DataTable)dgvPOItemDetailView.DataSource;
            DataTable dtTemp = dtOriginal.Clone();
            int existLotNumber = 0;
            int chineseBracketCount = 0;
            int containChineseLetter = 0;
                 
            for ( int m = 0;m < dgvPOItemDetailView.Rows.Count;m++)
            {
                if (Convert.ToBoolean(dgvPOItemDetailView.Rows[m].Cells["Check"].Value) == true)
                {
                    DataRow dr = dtTemp.NewRow();
                    dr = (dgvPOItemDetailView.Rows[m].DataBoundItem as DataRowView).Row;
                    dtTemp.Rows.Add(dr.ItemArray);
                    //包含中文括号
                    if(IsExistChineseBracket(dgvPOItemDetailView.Rows[m].Cells["厂家批号"].Value.ToString()))
                    {
                        chineseBracketCount++;
                        dgvPOItemDetailView.Rows[m].DefaultCellStyle.BackColor = Color.LightBlue;
                    }
                    //包含中文字符
                    if (IsChineseLetter(dgvPOItemDetailView.Rows[m].Cells["厂家批号"].Value.ToString()))
                    {
                        containChineseLetter++;
                        dgvPOItemDetailView.Rows[m].DefaultCellStyle.BackColor = Color.LightBlue;
                    }
                    //批号已存在
                    if (IsExistSameLotNumber(dgvPOItemDetailView.Rows[m].Cells["物料代码"].Value.ToString(), dgvPOItemDetailView.Rows[m].Cells["厂家批号"].Value.ToString()) > 0)
                    {
                        existLotNumber++;
                        dgvPOItemDetailView.Rows[m].DefaultCellStyle.BackColor = Color.LightYellow;                           
                    }
                }
            }
         

            if (existLotNumber > 0)
            {
                MessageBox.Show("当前物料存在厂家批号已使用情况，黄色标出，请修改后再次提交。", "提示");
                return;
            }
            if (chineseBracketCount > 0)
            {
                MessageBox.Show("厂家批号中有中文括号，蓝色标出，请修改为英文括号后再次提交。", "提示");
                return;
            }
            if (containChineseLetter > 0)
            {
                MessageBox.Show("厂家批号中有中文字符，蓝色标出，请修改为拼音后再次提交。", "提示");
                return;
            }

            List<string> sqlList = new List<string>();
            List<int> stockList = new List<int>();
            List<int> dateList = new List<int>();
            List<string> specialItemList = SQLHelper.GetList(GlobalSpace.FSDBConnstr, "Select ItemNumber  From PurchaseDepartmentStockSpecialItem", "ItemNumber");

            if (dtTemp.Rows.Count > 0)
            {
                for (int i = 0; i < dtTemp.Rows.Count; i++)
                {
                    if (dtTemp.Rows[i]["Status"].ToString() == "0")
                    {
                        //                sqlList.Add(dtTemp.Rows[i]["Guid"].ToString());
                    }
                    else
                    {
                        MessageBoxEx.Show(string.Format("选中的第{0}行状态不为0",i+1),"提示");
                        return;
                    }
                    if (specialItemList.Contains(dtTemp.Rows[i]["物料代码"].ToString()) || ((dtTemp.Rows[i]["物料代码"].ToString().Substring(0, 1) == "A" && dtTemp.Rows[i]["ItemReceiveType"].ToString() == "M")))
                    {

                    }
                    else
                    {
                        if (dtTemp.Rows[i]["到期日期"].ToString().Length != 8 && dtTemp.Rows[i]["到期日期"].ToString().Length != 10)
                        {
                            dateList.Add(i);
                        }
                    }

             /*       if (dtTemp.Rows[i]["库"].ToString() != GetPreferedStockroom(dtTemp.Rows[i]["物料代码"].ToString()))
                    {
                        stockList.Add(i);
                    }*/
                }

                if (dateList.Count > 0)
                {
                    Custom.MsgEx("当前到期日期存在不正确的格式，已用红色背景标出！");
                    for (int j = 0; j < dateList.Count; j++)
                    {
                        dgvPOItemDetailView.Rows[dateList[j]].DefaultCellStyle.BackColor = Color.HotPink;
                    }
                    return;
                }
                
                if (stockList.Count > 0)
                {
                    Custom.MsgEx("当前有部分物料优选库位与当前四班不一致，已用黄色背景标出！");
                    for (int j = 0; j < stockList.Count; j++)
                    {
                        dgvPOItemDetailView.Rows[stockList[j]].DefaultCellStyle.BackColor = Color.LightYellow;
                    }
                }
                
                for (int i = 0; i < dtTemp.Rows.Count; i++)
                {
                    string itemType = string.Empty; ;
                    if (StockUser.Type == "ALL")
                    {
                        if (dtTemp.Rows[i]["物料代码"].ToString().Substring(0, 1) == "M" || dtTemp.Rows[i]["物料代码"].ToString().Substring(0, 1) == "A")
                        {
                            itemType = "M";
                        }
                        else
                        {
                            itemType = "P";
                        }
                    }
                    else
                    {
                        itemType = StockUser.Type;
                    }
                    string sqlUpdate = @"Update PurchaseOrderRecordHistoryByCMF Set Status = 1,SubmitOperateDateTime='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "',ItemReceiveType ='" + itemType + "'     Where Guid = '" + dtTemp.Rows[i]["Guid"].ToString() + "' and Status = 0";

                    sqlList.Add(sqlUpdate);
                }

                try
                {
                    if (SQLHelper.BatchExecuteNonQuery(GlobalSpace.FSDBConnstr, sqlList))
                    {
                        Custom.MsgEx("提交成功！");
                        dgvPOItemDetailView.DataSource = GetReceivedRecordByStatusAndUserID(0, StockUser.UserID);
                        dgvPOItemDetailView.Columns["Guid"].Visible = false;
                        dgvPOItemDetailView.Columns["Status"].Visible = false;
                        dgvPOItemDetailView.Columns["BuyerID"].Visible = false;
                        dgvPOItemDetailView.Columns["FDAFlag"].Visible = false;

                        /*                dgvPOItemDetailView.DataSource = GetReceivedRecordByStatus(1);
                                        dgvPOItemDetailView.Columns["Guid"].Visible = false;
                                        dgvPOItemDetailView.Columns["Status"].Visible = false;*/
                    }
                    else
                    {
                        Custom.MsgEx("提交失败！");
                    }
                }
                catch (Exception ex)
                {
                    Custom.MsgEx(ex.Message);
                }

            }
            else
            {
                Custom.MsgEx("当前没有选中行记录！");
            }
        }


        private void btnFSRefresh_Click(object sender, EventArgs e)
        {
            dgvFSRefresh();
        }

        private void dgvFSRefresh()
        {
            IsFSView = false;
            //先检查ItemReceiveType字段是否有空值，如果有空值则进行处理
            string sqlSelectExist = @"SELECT
	                                                COUNT (Id)
                                                FROM
	                                                PurchaseOrderRecordHistoryByCMF
                                                WHERE
	                                                (
		                                                ItemReceiveType = ''
		                                                OR ItemReceiveType IS NULL
	                                                )
                                                AND Status = 1";


            if (SQLHelper.Exist(GlobalSpace.FSDBConnstr, sqlSelectExist))
            {
                string sqlUpdate = @"UPDATE PurchaseOrderRecordHistoryByCMF SET ItemReceiveType=LEFT(ItemNumber,1) WHERE ( ItemReceiveType = ''  OR ItemReceiveType IS NULL )  AND Status = 1";
                SQLHelper.ExecuteNonQuery(GlobalSpace.FSDBConnstr, sqlUpdate);
            }

            dgvPODetailFS.DataSource = GetReceivedRecordByStatus(1);
            dgvPODetailFS.Columns["Guid"].Visible = false;
            dgvPODetailFS.Columns["Status"].Visible = false;
            dgvPODetailFS.Columns["LotNumberAssign"].Visible = false;
            dgvPODetailFS.Columns["ItemReceiveType"].Visible = false;
            dgvPODetailFS.Columns["ItemReceiveType"].Visible = false;
            dgvPODetailFS.Columns["ItemReceiveType"].Visible = false;
            //dgvPODetailFS.Columns["Check2"].ReadOnly = false; 
        }

        private void btnViewAppointedDate_Click(object sender, EventArgs e)
        {
            IsFSView = true;
            dgvPODetailFS.DataSource = GetReceivedRecordByOperateFSDate(dtpFS.Value.ToString("yyyy-MM-dd"));
            dgvPODetailFS.Columns["Guid"].Visible = false;
            dgvPODetailFS.Columns["Status"].Visible = false;
        }

        private void btnViewFSErrorMSG_Click(object sender, EventArgs e)
        {
            string sqlSelect = @"Select Type AS 类型,ErrorContent AS 内容,OperateDateTime AS 日期 From FSErrorLogByCMF Where Operator='" + FSUserID + "' And Left(OperateDateTime,10)='" + DateTime.Now.ToString("yyyy-MM-dd") + "'  Order By OperateDateTime Desc";
            dgvPODetailFS.DataSource = SQLHelper.GetDataTable(GlobalSpace.FSDBConnstr, sqlSelect);
        }

        private void btnGenerateBatchNumber_Click(object sender, EventArgs e)
        {
            if (StockUser.Number == "NotSet")
            {
                Custom.MsgEx("没有设置公司批号用库管员代码，请联系管理员！");
                return;
            }
            int countExist = 0;
            string sequenceNumber = string.Empty;
            DataTable dtTemp = (DataTable)dgvPODetail.DataSource;
            if (rbtnMannual.Checked)
            {
                countExist = Convert.ToInt32(tbExistBatches.Text);
            }
            else if (rbtnAutomatic.Checked)
            {
                //获取今日已确认的批号数量
                string sqlSelect = @"Select Count(Id) From PurchaseOrderRecordHistoryByCMF Where Operator='" + FSUserID + "' And ReceiveDate='" + DateTime.Now.ToString("yyyy-MM-dd") + "'";
                countExist = Convert.ToInt32(SQLHelper.ExecuteScalar(GlobalSpace.FSDBConnstr, sqlSelect));
            }
            /*
            for(int j = 0;j <dgvPODetail.Rows.Count;j++)
            {
                if(Convert.ToBoolean(dgvPODetail.Rows[j].Cells["Checked"].Value))
                {
                    countExist = countExist + 1;
                    if (countExist.ToString().Length == 1)
                    {
                        sequenceNumber = "0" + countExist.ToString();
                    }
                    else
                    {
                        sequenceNumber = countExist.ToString();
                    }
                    if (CommonOperate.IsNumber(dtTemp.Rows[i]["物料代码"].ToString().Substring(1, 1)))
                    {
                        if (FOItemKeeperList.Contains(StockUser.UserID))
                        {
                            dtTemp.Rows[i]["公司批号"] = dtTemp.Rows[i]["物料代码"].ToString().Substring(0, 1) + "F" + dtpLotNumber.Value.ToString("yyMMdd") + sequenceNumber + StockUser.Number;
                        }
                        else
                        {
                            dtTemp.Rows[i]["公司批号"] = dtTemp.Rows[i]["物料代码"].ToString().Substring(0, 1) + dtpLotNumber.Value.ToString("yyMMdd") + sequenceNumber + StockUser.Number;
                        }

                    }
                    else if (dtTemp.Rows[i]["物料代码"].ToString().ToUpper().Substring(1, 1) == "X" || dtTemp.Rows[i]["物料代码"].ToString().ToUpper().Substring(1, 1) == "Y")
                    {
                        dtTemp.Rows[i]["公司批号"] = dtTemp.Rows[i]["物料代码"].ToString().Substring(0, 1) + dtpLotNumber.Value.ToString("yyMMdd") + sequenceNumber + StockUser.Number;
                    }
                    else
                    {
                        dtTemp.Rows[i]["公司批号"] = dtTemp.Rows[i]["物料代码"].ToString().Substring(0, 2) + dtpLotNumber.Value.ToString("yyMMdd") + sequenceNumber + StockUser.Number;
                    }
                }
            }
            */
            //根据已确认批号数量和选中的行，生成公司批
            List<int> list = new List<int>();
            for (int j = dtTemp.Rows.Count - 1; j >= 0; j--)
            {
                if (dtTemp.Rows[j]["厂家批号"] == DBNull.Value || string.IsNullOrEmpty(dtTemp.Rows[j]["厂家批号"].ToString().Trim()))
                {
                    dtTemp.Rows.RemoveAt(j);
                }
            }

            
            for (int i = 0; i < dtTemp.Rows.Count; i++)
            {
                countExist = countExist + 1;
                if (countExist.ToString().Length == 1)
                {
                    sequenceNumber = "0" + countExist.ToString();
                }
                else
                {
                    sequenceNumber = countExist.ToString();
                }
                if (CommonOperate.IsNumber(dtTemp.Rows[i]["物料代码"].ToString().Substring(1, 1)))
                {
                    if (FOItemKeeperList.Contains(StockUser.UserID))
                    {
                        dtTemp.Rows[i]["公司批号"] = dtTemp.Rows[i]["物料代码"].ToString().Substring(0, 1) + "F" + dtpLotNumber.Value.ToString("yyMMdd") + sequenceNumber + StockUser.Number;
                    }
                    else
                    {
                        dtTemp.Rows[i]["公司批号"] = dtTemp.Rows[i]["物料代码"].ToString().Substring(0, 1) + dtpLotNumber.Value.ToString("yyMMdd") + sequenceNumber + StockUser.Number;
                    }
                }
                else if (dtTemp.Rows[i]["物料代码"].ToString().ToUpper().Substring(1, 1) == "X" || dtTemp.Rows[i]["物料代码"].ToString().ToUpper().Substring(1, 1) == "Y")
                {
                    dtTemp.Rows[i]["公司批号"] = dtTemp.Rows[i]["物料代码"].ToString().Substring(0, 1) + dtpLotNumber.Value.ToString("yyMMdd") + sequenceNumber + StockUser.Number;
                }
                else
                {
                    dtTemp.Rows[i]["公司批号"] = dtTemp.Rows[i]["物料代码"].ToString().Substring(0, 2) + dtpLotNumber.Value.ToString("yyMMdd") + sequenceNumber + StockUser.Number;
                }
            }
            dgvPODetail.DataSource = dtTemp;
        }

        private void btnModify_Click(object sender, EventArgs e)
        {
            #region 原有代码
            /*
            if (!IsNeedToSubmit)
            {
                Custom.MsgEx("当前状态不允许记录修改！");
            }
            List<string> sqlList = new List<string>();
            foreach (DataGridViewRow dgvr in dgvPOItemDetailView.Rows)
            {
                if (dgvr.Cells["Status"].Value.ToString() == "0" && Convert.ToBoolean(dgvr.Cells["Check"].Value) == true)
                {
                    string guid = dgvr.Cells["Guid"].Value.ToString();
                    string internalLotNumber = dgvr.Cells["公司批号"].Value.ToString().ToUpper();
                    string vendorLotNumber = dgvr.Cells["厂家批号"].Value.ToString().ToUpper();
                    double receiptQuantity = Convert.ToDouble(dgvr.Cells["入库数量"].Value);
                    string stock = dgvr.Cells["库"].Value.ToString().ToUpper();
                    string bin = dgvr.Cells["位"].Value.ToString().ToUpper();
                    string expiredDate = dgvr.Cells["到期日期"].Value.ToString();
                    string mfgDate = dgvr.Cells["生产日期"].Value.ToString();
                    int numberOfPackages = Convert.ToInt32(dgvr.Cells["整件数"].Value);
                    double packageOdd = Convert.ToDouble(dgvr.Cells["零头"].Value);
                    string packageSpecification = dgvr.Cells["包装规格"].Value.ToString();
                    string packageUM = dgvr.Cells["件数单位"].Value.ToString();
                    string itemDesc = dgvr.Cells["描述"].Value.ToString();
                  
                    if (expiredDate.Length < 8 || expiredDate.Length > 10)
                    {
                        MessageBoxEx.Show("日期格式不符合要求，请重新输入！", "提示");
                        return;
                    }
                    string sqlUpdate = @"Update PurchaseOrderRecordHistoryByCMF Set ReceiveQuantity=" + receiptQuantity + ",LotNumber='" + vendorLotNumber + "',InternalLotNumber='" + internalLotNumber + "',Stock='" + stock + "',Bin='" + bin + "',ExpiredDate='" + expiredDate + "',ManufacturedDate='" + mfgDate + "',NumberOfPackages=" + numberOfPackages + ",PackageOdd=" + packageOdd + ",PackageSpecification='" + packageSpecification + "',PackageUM='" + packageUM + "',ItemDescription='" + itemDesc + "' Where Guid='" + guid + "'";
                    sqlList.Add(sqlUpdate);
                }
            }

            if (sqlList.Count == 0)
            {
                Custom.MsgEx("当前无选中的行！");
                return;
            }

            if (SQLHelper.BatchExecuteNonQuery(GlobalSpace.FSDBConnstr, sqlList))
            {
                Custom.MsgEx("更新成功！");
                dgvPOItemDetailView.DataSource = GetReceivedRecordByStatusAndUserID(0, StockUser.UserID);
                dgvPOItemDetailView.Columns["Guid"].Visible = false;
                dgvPOItemDetailView.Columns["Status"].Visible = false;
                dgvPOItemDetailView.Columns["BuyerID"].Visible = false;
                dgvPOItemDetailView.Columns["FDAFlag"].Visible = false;
                dgvPOItemDetailView.Columns["ItemReceiveType"].Visible = false;
                dgvPOItemDetailView.Columns["StockKeeper"].Visible = false;
            }
            else
            {
                Custom.MsgEx("更新失败！");
            }
            */
            #endregion

            if (!IsNeedToSubmit)
            {
                Custom.MsgEx("当前状态不允许记录修改！");
            }
            List<string> sqlList = new List<string>();
            foreach (DataGridViewRow dgvr in dgvPOItemDetailView.Rows)
            {
                if (dgvr.Cells["Status"].Value.ToString() == "0" && Convert.ToBoolean(dgvr.Cells["Check"].Value) == true)
                {
                    string guid = dgvr.Cells["Guid"].Value.ToString();
                    string internalLotNumber = dgvr.Cells["公司批号"].Value.ToString().ToUpper();
                    string vendorLotNumber = dgvr.Cells["厂家批号"].Value.ToString().ToUpper();
                    double receiptQuantity = Convert.ToDouble(dgvr.Cells["入库数量"].Value);
                    string stock = dgvr.Cells["库"].Value.ToString().ToUpper();
                    string bin = dgvr.Cells["位"].Value.ToString().ToUpper();
                    string expiredDate = dgvr.Cells["到期日期"].Value.ToString();
                    string mfgDate = dgvr.Cells["生产日期"].Value.ToString();
                    int numberOfPackages = Convert.ToInt32(dgvr.Cells["整件数"].Value);
                    double packageOdd = Convert.ToDouble(dgvr.Cells["零头"].Value);
                    string packageSpecification = dgvr.Cells["包装规格"].Value.ToString();
                    string packageUM = dgvr.Cells["件数单位"].Value.ToString();
                    string itemDesc = dgvr.Cells["描述"].Value.ToString();
                    //新增字段App
                    string Checker = dgvr.Cells["复核人"].Value.ToString().Trim();
                    string Receiver = dgvr.Cells["接收请验人"].Value.ToString().Trim();
                    string Conclusion = dgvr.Cells["结论"].Value.ToString().Trim();
                    string ConclusionText = dgvr.Cells["结论其他内容"].Value.ToString().Trim();
                    string IsAnyDeviation = dgvr.Cells["物料验收过程是否出现偏差"].Value.ToString().Trim();
                    string DeviationNumber = dgvr.Cells["偏差编号"].Value.ToString().Trim();
                    string deviationIsClosed = dgvr.Cells["偏差是否已处理关闭"].Value.ToString().Trim();
                    string IsReport = dgvr.Cells["问题是否已报告"].Value.ToString().Trim();
                    string QualityManageIdea = dgvr.Cells["质量管理部门意见"].Value.ToString().Trim();
                    string Sign = dgvr.Cells["签名"].Value.ToString().Trim();
                    string SignDate = dgvr.Cells["签名日期"].Value.ToString().Trim();
                    string IsRequireClean = dgvr.Cells["是否需要清洁"].Value.ToString().Trim();
                    string PollutionSituation = dgvr.Cells["污染情况"].Value.ToString().Trim();
                    string CleanMethod = dgvr.Cells["清洁方式"].Value.ToString().Trim();
                    string IsComplete = dgvr.Cells["外包装是否完整"].Value.ToString().Trim();

                    string DamageSituation = dgvr.Cells["损坏情况"].Value.ToString().Trim();
                    string CauseInvestigation1 = dgvr.Cells["原因调查1"].Value.ToString().Trim();
                    string IsSealed = dgvr.Cells["外包装是否密封"].Value.ToString().Trim();
                    string UnsealedCondition = dgvr.Cells["不密封情况"].Value.ToString().Trim();
                    string CauseInvestigation2 = dgvr.Cells["原因调查2"].Value.ToString().Trim();
                    string IsAnyMaterialWithPollutionRisk = dgvr.Cells["运输工具内是否存在造成污染交叉污染的物料"].Value.ToString().Trim();
                    string IsAnyProblemAffectedMaterialQuality = dgvr.Cells["是否有其他可能影响物料质量的问题"].Value.ToString().Trim();
                    string Question = dgvr.Cells["问题"].Value.ToString().Trim();
                    string CauseInvestigation3 = dgvr.Cells["原因调查3"].Value.ToString().Trim();
                    string LotNumberType = dgvr.Cells["批号类型"].Value.ToString().Trim();
                    string IsApprovedVendor = dgvr.Cells["是否为质量管理部门批准的供应商"].Value.ToString().Trim();
                    string StorageCondition = dgvr.Cells["规定贮存条件"].Value.ToString().Trim();
                    string TransportTemperature = dgvr.Cells["运输条件检查结果"].Value.ToString().Trim();
                    string TransportCondition = dgvr.Cells["运输条件是否符合"].Value.ToString().Trim();
                    string TransportationControlRecord = dgvr.Cells["是否有运输条件控制记录"].Value.ToString().Trim();

                    string Shape = dgvr.Cells["形状是否一致"].Value.ToString().Trim();
                    string Colour = dgvr.Cells["颜色是否一致"].Value.ToString().Trim();
                    string Font = dgvr.Cells["字体是否一致"].Value.ToString().Trim();
                    string RoughWeight = dgvr.Cells["有无毛重"].Value.ToString().Trim();
                    string NetWeight = dgvr.Cells["有无净重"].Value.ToString().Trim();
                    string ApprovalNumber = dgvr.Cells["有无批准文号"].Value.ToString().Trim();
                    string ReportType = dgvr.Cells["报告类型"].Value.ToString().Trim();
                    string Report = dgvr.Cells["有无报告"].Value.ToString().Trim();
                    /*
                    if (expiredDate.Contains("."))
                    {
                        expiredDate = expiredDate.Replace(".", "").Trim();
                    }
                    */
                    if (expiredDate.Length < 8 || expiredDate.Length > 10)
                    {
                        MessageBoxEx.Show("日期格式不符合要求，请重新输入！", "提示");
                        return;
                    }
                    //string sqlUpdate = @"Update PurchaseOrderRecordHistoryByCMF Set ReceiveQuantity=" + receiptQuantity + ",LotNumber='" + vendorLotNumber + "',InternalLotNumber='" + internalLotNumber + "',Stock='" + stock + "',Bin='" + bin + "',ExpiredDate='" + expiredDate + "',ManufacturedDate='" + mfgDate + "',NumberOfPackages=" + numberOfPackages + ",PackageOdd=" + packageOdd + ",PackageSpecification='" + packageSpecification + "',PackageUM='" + packageUM + "',ItemDescription='"+itemDesc+"' Where Guid='" + guid + "'";
                    string sqlUpdate = @"Update PurchaseOrderRecordHistoryByCMF Set ReceiveQuantity=" + receiptQuantity + ",LotNumber='" + vendorLotNumber + "',InternalLotNumber='" + internalLotNumber + "',Stock='" + stock + "',Bin='" + bin + "',ExpiredDate='" + expiredDate + "',ManufacturedDate='" + mfgDate + "',NumberOfPackages=" + numberOfPackages + ",PackageOdd=" + packageOdd + ",PackageSpecification='" + packageSpecification + "',PackageUM='" + packageUM + "',ItemDescription='" + itemDesc + "',Checker='" + Checker + "',Receiver='" + Receiver + "',Conclusion='" + Conclusion + "',ConclusionText='" + ConclusionText + "',IsAnyDeviation='" + IsAnyDeviation + "',DeviationNumber='" + DeviationNumber + "',deviationIsClosed='" + deviationIsClosed + "',IsReport='" + IsReport + "',QualityManageIdea='" + QualityManageIdea + "',Sign='" + Sign + "',SignDate='" + SignDate + "',IsRequireClean='" + IsRequireClean + "',PollutionSituation='" + PollutionSituation + "',CleanMethod='" + CleanMethod + "',IsComplete='" + IsComplete + "',DamageSituation='" + DamageSituation + "',CauseInvestigation1='" + CauseInvestigation1 + "',IsSealed='" + IsSealed + "',UnsealedCondition='" + UnsealedCondition + "',CauseInvestigation2='" + CauseInvestigation2 + "',IsAnyMaterialWithPollutionRisk='" + IsAnyMaterialWithPollutionRisk + "',IsAnyProblemAffectedMaterialQuality='" + IsAnyProblemAffectedMaterialQuality + "',Question='" + Question + "',CauseInvestigation3='" + CauseInvestigation3 + "',LotNumberType='" + LotNumberType + "',IsApprovedVendor='" + IsApprovedVendor + "',StorageCondition='" + StorageCondition + "',TransportTemperature='" + TransportTemperature + "',TransportCondition='" + TransportCondition + "',TransportationControlRecord='" + TransportationControlRecord + "',Shape='" + Shape + "',Colour='" + Colour + "',Font='" + Font + "',RoughWeight='" + RoughWeight + "',NetWeight='" + NetWeight + "',ApprovalNumber='" + ApprovalNumber + "',ReportType='" + ReportType + "',Report='" + Report + "' Where Guid='" + guid + "' and Status= 0";
                    sqlList.Add(sqlUpdate);
                }
            }

            if (sqlList.Count == 0)
            {
                Custom.MsgEx("当前无选中的行！");
                return;
            }

            if (SQLHelper.BatchExecuteNonQuery(GlobalSpace.FSDBConnstr, sqlList))
            {
                Custom.MsgEx("修改成功！");
                dgvPOItemDetailView.DataSource = GetReceivedRecordByStatusAndUserID(0, StockUser.UserID);
                dgvPOItemDetailView.Columns["Guid"].Visible = false;
                dgvPOItemDetailView.Columns["Status"].Visible = false;
                dgvPOItemDetailView.Columns["BuyerID"].Visible = false;
                dgvPOItemDetailView.Columns["FDAFlag"].Visible = false;
                dgvPOItemDetailView.Columns["ItemReceiveType"].Visible = false;
                dgvPOItemDetailView.Columns["StockKeeper"].Visible = false;
                dgvPOItemDetailView.Columns["LotNumberAssign"].Visible = false;
            }
            else
            {
                Custom.MsgEx("修改失败！");
            }
        }

        private void btnRetriveRecord_Click(object sender, EventArgs e)
        {
            List<string> sqlList = new List<string>();

            foreach (DataGridViewRow dgvr in dgvPODetailFS.Rows)
            {
                if (Convert.ToBoolean(dgvr.Cells["Check2"].Value))
                {
                    if (dgvr.Cells["Status"].Value.ToString() == "1")
                    {
                        string sqlUpdate = string.Empty;
                        //A类表示五金类物料
                        if(dgvr.Cells["ItemReceiveType"].Value.ToString() == "A")
                        {
                            sqlUpdate = @"Update PurchaseOrderRecordHistoryByCMF Set Status =9  Where Guid='" + dgvr.Cells["Guid"].Value.ToString() + "' and Status =1";
                        }
                        else
                        {
                            sqlUpdate =@"Update PurchaseOrderRecordHistoryByCMF Set Status =0  Where Guid='" + dgvr.Cells["Guid"].Value.ToString() + "' and Status =1";
                        }
                            
                        sqlList.Add(sqlUpdate);
                    }
                }
            }

            if (sqlList.Count == 0)
            {
                Custom.MsgEx("当前无选中行！");
                return;
            }

            if (SQLHelper.BatchExecuteNonQuery(GlobalSpace.FSDBConnstr, sqlList))
            {
                Custom.MsgEx("更新成功！");
                dgvPODetailFS.DataSource = GetReceivedRecordByStatus(1);
                dgvPODetailFS.Columns["Guid"].Visible = false;
                dgvPODetailFS.Columns["Status"].Visible = false;
            }
            else
            {
                Custom.MsgEx("更新失败！");
            }
        }


        private void btnFDAWrite_Click(object sender, EventArgs e)
        {
            btnFDAWrite.Enabled = false;
            string sqlSelect = @"SELECT
	                                            ItemNumber,
	                                            Specification
                                            FROM
	                                            dbo.PurchaseDepartmentStockFDAItemSpecification";
            List<string> fdaList = SQLHelper.GetDataTable(GlobalSpace.FSDBConnstr, sqlSelect).AsEnumerable().Select(r => r.Field<string>("ItemNumber")).ToList();
            DataTable dtOriginal = (DataTable)dgvPOItemDetailView.DataSource;
            DataTable dtTemp = dtOriginal.Clone();
            dtTemp.Columns.Add("FDAPackage");

            foreach (DataGridViewRow dgvr in dgvPOItemDetailView.Rows)
            {
                if (Convert.ToBoolean(dgvr.Cells["Check"].Value) && Convert.ToInt32(dgvr.Cells["FDAFlag"].Value) == 0)
                {
                    DataRow dr = dtTemp.NewRow();
                    dr = (dgvr.DataBoundItem as DataRowView).Row;
                    dtTemp.Rows.Add(dr.ItemArray);
                }
            }

            if (dtTemp.Rows.Count == 0)
            {
                MessageBoxEx.Show("当前无可用的信息！", "提示");
            }
            else
            {
                #region 功能取消
                /*
                if (GlobalSpace.dictFDAItem.Count > 0)
                {
                    GlobalSpace.dictFDAItem.Clear();
                }
                else
                {
                    DataTable dtFDA = new DataTable();
                    dtFDA.Columns.Add("Guid");
                    dtFDA.Columns.Add("包装规格");
                    dtFDA.Columns.Add("物料代码");
                    dtFDA.Columns.Add("物料描述");
                    dtFDA.Columns.Add("入库数量");
                    dtFDA.Columns.Add("厂家批号");

                    int m = 0;

                    foreach(DataRow dr in dtTemp.Rows)
                    {
                        if(fdaList.Contains(dr["物料代码"].ToString()))
                        {
                            DataRow dr2 = dtFDA.NewRow();
                            dr2["Guid"] = dr["Guid"];
                            dr2["物料代码"] = dr["物料代码"];
                            dr2["物料描述"] = dr["描述"];
                            dr2["入库数量"] = dr["入库数量"];
                            dr2["厂家批号"] = dr["厂家批号"];
                            dtFDA.Rows.Add(dr2.ItemArray);
                            m++;
                        }
                    }
                    if(m > 0)
                    {
                        FDAPackage fda = new FDAPackage(dtFDA);
                        fda.ShowDialog();
                        if(GlobalSpace.dictFDAItem.Count == 0)
                        {
                            Custom.Msg("没有填写包装规格！");
                            btnFDAWrite.Enabled = true;
                            return;
                        }
                        else
                        {
                            foreach(var v in GlobalSpace.dictFDAItem)
                            {
                                foreach(DataRow dr3 in dtTemp.Rows)
                                {
                                    if(v.Key == dr3["Guid"].ToString())
                                    {
                                        dr3.SetField<string>("FDAPackage", v.Value);
                                    }
                                }
                            }
                        }
                    }

                }
                */
                #endregion
                List<string> sqlList = new List<string>();
                List<string> guidList = new List<string>();

                foreach (DataRow dr4 in dtTemp.Rows)
                {
                    if (dr4["生产日期"] == DBNull.Value || string.IsNullOrWhiteSpace(dr4["生产日期"].ToString()))
                    {
                        MessageBoxEx.Show("生产日期不能有空项！", "提示");
                        return;
                    }
                    string sqlInsert = @"INSERT INTO [RYZY_YJM].[dbo].[FL_plan_IN] (
	                                                                [VendorId],
	                                                                [Vendor],
	                                                                [ManufactureID],
	                                                                [Manufacture],
	                                                                [PurchaseOrder],
	                                                                [LineNumber],
	                                                                [ItemNumber],
	                                                                [ItemDescription],
	                                                                [UM],
	                                                                [OrderQuantity],
	                                                                [ReceiveQuantity],
	                                                                [ManufactureLotNumbcer],
	                                                                [CompanyLotNumber],
	                                                                [Buyer],
	                                                                [ExpireDate],
	                                                                [StockKeeper],[ManufacturedDate],PackModel)
                                                                VALUES  ( '" + dr4["供应商码"].ToString() + "',  '" + dr4["供应商名"].ToString() + "', '" + dr4["生产商码"].ToString() + "','" + dr4["生产商名"].ToString() + "','" + dr4["采购单号"].ToString() + "', '" + dr4["行号"].ToString() + "', '" + dr4["物料代码"].ToString() + "',  '" + dr4["描述"].ToString() + "','" + dr4["单位"].ToString() + "', " + Convert.ToDouble(dr4["采购数量"]) + "," + Convert.ToDouble(dr4["入库数量"]) + ",'" + dr4["厂家批号"].ToString().ToUpper().Replace(".", "") + "','" + dr4["公司批号"].ToString().ToUpper() + "','" + dr4["BuyerID"].ToString() + "','" + dr4["到期日期"].ToString() + "', '" + StockUser.UserID + "','" + dr4["生产日期"].ToString().Replace(".", "") + "','" + dr4["包装规格"].ToString() + "')";
                    sqlList.Add(sqlInsert);
                    guidList.Add(dr4["Guid"].ToString());
                }

                if (SQLHelper.BatchExecuteNonQuery(GlobalSpace.FDAConnstr, sqlList))
                {
                    List<string> sqlUpdateList = new List<string>();
                    foreach (var v in guidList)
                    {
                        string sqlUpdate = @"Update PurchaseOrderRecordHistoryByCMF Set FDAFlag = 1 Where Guid ='" + v.ToString() + "'";
                        sqlUpdateList.Add(sqlUpdate);
                    }
                    if (SQLHelper.BatchExecuteNonQuery(GlobalSpace.FSDBConnstr, sqlUpdateList))
                    {
                        MessageBoxEx.Show("写入成功！", "提示");
                    }
                    else
                    {
                        MessageBoxEx.Show("写入成功，更新状态失败，请联系管理员！", "提示");
                    }
                }
                else
                {
                    MessageBoxEx.Show("写入失败，请联系管理员！", "提示");
                }
            }
            btnFDAWrite.Enabled = true;
        }

        private void btnReceivePrint_Click(object sender, EventArgs e)
        {

            DataTable dt = new DataTable();
            string[] du = new string[2] { "高", "毕" };
            string[] gao = new string[2] { "杜", "毕" };
            string[] bi = new string[2] { "高", "杜" };
            string name = StockUser.UserName.Substring(0, 1);
            Random rd = new Random();
            dt.Columns.Add("联系单号");
            dt.Columns.Add("代码");
            dt.Columns.Add("描述");
            dt.Columns.Add("供应商码");
            dt.Columns.Add("供应商名");
            dt.Columns.Add("生产商码");
            dt.Columns.Add("生产商名");
            dt.Columns.Add("入库量");
            dt.Columns.Add("厂家批号");
            dt.Columns.Add("公司批号");
            dt.Columns.Add("接收日期");
            dt.Columns.Add("接收");
            dt.Columns.Add("复核");
            dt.Columns.Add("件数");
            bool isChecked = false;
            foreach (DataGridViewRow dgvr in dgvPOItemDetailView.Rows)
            {
                if (Convert.ToBoolean(dgvr.Cells["Check"].Value))
                {
                    isChecked = true;
                    DataRow dr = dt.NewRow();
                    dr["联系单号"] = dgvr.Cells["外贸单号"].Value.ToString();
                    dr["代码"] = dgvr.Cells["物料代码"].Value.ToString();
                    dr["描述"] = dgvr.Cells["描述"].Value.ToString();
                    dr["供应商码"] = dgvr.Cells["供应商码"].Value.ToString();
                    dr["供应商名"] = dgvr.Cells["供应商名"].Value.ToString();
                    dr["生产商码"] = dgvr.Cells["生产商码"].Value.ToString();
                    dr["生产商名"] = dgvr.Cells["生产商名"].Value.ToString();
                    dr["入库量"] = dgvr.Cells["入库数量"].Value.ToString();
                    dr["厂家批号"] = dgvr.Cells["厂家批号"].Value.ToString();
                    dr["公司批号"] = dgvr.Cells["公司批号"].Value.ToString();
                    dr["接收日期"] = dgvr.Cells["到期日期"].Value.ToString();
                    dr["接收"] = name;
                    if (name == "高")
                    {
                        dr["复核"] = gao[rd.Next(gao.Length)];
                    }
                    else if (name == "毕")
                    {
                        dr["复核"] = bi[rd.Next(gao.Length)];
                    }
                    else
                    {
                        dr["复核"] = du[rd.Next(gao.Length)];
                    }

                    if (Convert.ToInt32(dgvr.Cells["零头"].Value) == 0)
                    {
                        dr["件数"] = dgvr.Cells["整件数"].Value.ToString() + "*" + dgvr.Cells["包装规格"].Value.ToString();
                    }
                    else
                    {
                        dr["件数"] = dgvr.Cells["整件数"].Value.ToString() + "*" + dgvr.Cells["包装规格"].Value.ToString() + "+" + dgvr.Cells["零头"].Value.ToString();
                    }

                    dt.Rows.Add(dr.ItemArray);
                }
            }
            if (!isChecked)
            {
                MessageBoxEx.Show("没有选中的行！", "提示");
                return;
            }

            PrintPO pp = new PrintPO(dt, "\\PrintPORecord.grf");
            pp.Show();


        }

        private void tbReceiveRecordQuantity_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!string.IsNullOrEmpty(tbReceiveRecordQuantity.Text.Trim()))
            {
                if (e.KeyChar == (char)13)
                {
                    btnBatchesReceive_Click(sender, e);
                }
            }
        }

        private void btnMakeAllChecked_Click_1(object sender, EventArgs e)
        {

            foreach (DataGridViewRow dgvr in dgvPOItemDetailView.Rows)
            {
                dgvr.Cells["Check"].Value = true;
            }

        }

        private void btnMakeAllUnchecked_Click_1(object sender, EventArgs e)
        {
            foreach (DataGridViewRow dgvr in dgvPOItemDetailView.Rows)
            {
                dgvr.Cells["Check"].Value = false;
            }
        }

        private void btnFSHistory_Click(object sender, EventArgs e)
        {
            IsNeedToSubmit = false;
            dgvPOItemDetailView.DataSource = GetFSReceivedRecordByUserID(StockUser.UserID);
            dgvPOItemDetailView.Columns["Guid"].Visible = false;
            dgvPOItemDetailView.Columns["Status"].Visible = false;
            dgvPOItemDetailView.Columns["BuyerID"].Visible = false;
            dgvPOItemDetailView.Columns["FDAFlag"].Visible = false;
            dgvPOItemDetailView.Columns["ItemReceiveType"].Visible = false;
            dgvPOItemDetailView.Columns["StockKeeper"].Visible = false;
            dgvPOItemDetailView.Columns["LotNumberAssign"].Visible = false;
    //        dgvPOItemDetailView.Columns["库"].Visible = false;
    //        dgvPOItemDetailView.Columns["位"].Visible = false;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DataTable dtTemp = (DataTable)dgvPODetail.DataSource;
            int iIndex = dgvPODetail.SelectedCells[0].RowIndex;
            string strGuid = dgvPODetail.Rows[iIndex].Cells["Guid"].Value.ToString();
            string sqlCheck = @"Select Count(Id) From PurchaseOrderRecordHistoryByCMF Where Guid='" + strGuid + "'";
            if (SQLHelper.Exist(GlobalSpace.FSDBConnstr, sqlCheck))
            {
                string sqlDelete = @"Update PurchaseOrderRecordHistoryByCMF Set Status = 77,DeleteOperateDateTime='" + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss") + "'+'|'+'" + StockUser.UserID + "'  Where Guid='" + strGuid + "'";
                if (MessageBoxEx.Show("本条记录为原始记录，确认在数据库中删除么？", "提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    if (SQLHelper.ExecuteNonQuery(GlobalSpace.FSDBConnstr, sqlDelete))
                    {
                        Custom.MsgEx("删除成功！");
                        IsNeedToConfirm = true;
                        dgvPODetail.DataSource = GetVendorPOItemsDetail(9);
                        dgvPODetail.Columns["Guid"].Visible = false;
                        dgvPODetail.Columns["InspectionPeriod"].Visible = false;
                        dgvPODetail.Columns["StockKeeper"].Visible = false;
                        dgvPODetail.Columns["ItemReceiveType"].Visible = false;
                        dgvPODetail.Columns["VendorName"].Visible = false;
                        dgvPODetail.Columns["VendorNumber"].Visible = false;
                        dgvPODetail.Columns["入库量"].DefaultCellStyle.BackColor = Color.LightYellow;
                        dgvPODetail.Columns["公司批号"].DefaultCellStyle.BackColor = Color.LightYellow;
                        dgvPODetail.Columns["厂家批号"].DefaultCellStyle.BackColor = Color.LightYellow;
                        dgvPODetail.Columns["到期日期"].DefaultCellStyle.BackColor = Color.LightYellow;
                    }
                    else
                    {
                        Custom.MsgEx("删除失败！");
                    }
                }
            }
            else
            {
                Custom.MsgEx("本条记录未提交到数据库里，将在本界面中移除！");
                dtTemp.Rows.RemoveAt(iIndex);
            }
        }

        private void btnViewPONumber_Click(object sender, EventArgs e)
        {
            /*
            string strSql = @"SELECT
	                                                        (
		                                                        CASE
		                                                        WHEN LEFT (T1.StockKeeper, 2) = 'CX' THEN
			                                                        '公共'
		                                                        ELSE
			                                                        LEFT (T1.StockKeeper, 3)
		                                                        END
	                                                        ) AS 库管员,
	                                                        (
		                                                        CASE
		                                                        WHEN T1.Status = '9' THEN
			                                                        '确认到货'
		                                                        WHEN T1.Status = '0' THEN
			                                                        '已收货'
		                                                        WHEN T1.Status = '1' THEN
			                                                        '已提交'
		                                                        WHEN T1.Status = '2' THEN
			                                                        '操作四班'
		                                                        END
	                                                        ) AS 状态,
	                                                        T1.PONumber AS 采购单号,
	                                                        T1.ManufacturerNumber AS 生产商码,
	                                                        T1.ManufacturerName AS 生产商名,
	                                                        T1.LineNumber AS 行号,
	                                                        T1.ItemNumber AS 物料代码,
	                                                        T1.ItemDescription AS 描述,
	                                                        T1.LineUM AS 单位,
	                                                        T1.OrderQuantity AS 订货量,
	                                                        T1.OrderQuantity AS 入库量,
	                                                        T1.LotNumber AS 厂家批号,
	                                                        T1.InternalLotNumber AS 公司批号,
	                                                        T1.ExpiredDate AS 到期日期,
	                                                        T1.Stock AS 库,
	                                                        T1.Bin AS 位,	T1.ForeignNumber AS 外贸单号	                                       
                                        FROM
	                                        PurchaseOrderRecordHistoryByCMF T1
                                        WHERE  T1.PONumber  = '" + tbPONumberView.Text.Trim() + "'   ORDER BY     T1.LineNumber ASC";
            DataTable dt = SQLHelper.GetDataTable(GlobalSpace.FSDBConnstr, strSql);
            if (dt.Rows.Count > 0)
            {
                dgvPODetailFS.DataSource = dt;
            }
            else
            {
                Custom.MsgEx("未查到该订单" + tbPONumberView.Text + "信息！");
            }*/
        }

        private void tbPONumberView_TextChanged(object sender, EventArgs e)
        {
       /*     tbPONumberView.Text = tbPONumberView.Text.ToUpper();
            tbPONumberView.SelectionStart = tbPONumberView.Text.Length;*/
        }

        private void btnMultiplyStockReceive_Click(object sender, EventArgs e)
        {
            List<string> sqlList = new List<string>();
            foreach (DataGridViewRow dgvr in dgvPODetail.Rows)
            {
                if (Convert.ToBoolean(dgvr.Cells["Checked"].Value) == true)
                {
                    string guid = dgvr.Cells["Guid"].Value.ToString();
                    string sqlInsert = @"Insert Into PurchaseOrderRecordHistoryByCMF (PONumber,
	                                                                                        VendorNumber,
	                                                                                        VendorName,
	                                                                                        ManufacturerNumber,
	                                                                                        ManufacturerName,
	                                                                                        LineNumber,
	                                                                                        ItemNumber,
	                                                                                        ItemDescription,
	                                                                                        LineUM,
	                                                                                        DemandDeliveryDate,
	                                                                                        ReceiveQuantity,
	                                                                                        Stock,
	                                                                                        Bin,
	                                                                                        InspectionPeriod,
	                                                                                        LotNumber,
	                                                                                        InternalLotNumber,
	                                                                                        ExpiredDate,
	                                                                                        Operator,
	                                                                                        StockKeeper,
	                                                                                        RetestDate,
	                                                                                        LotNumberAssign,
	                                                                                        OrderQuantity,
	                                                                                        ItemReceiveType,
	                                                                                        Supervisor,
	                                                                                        ForeignNumber,
	                                                                                        BuyerID,
	                                                                                        FDAFlag,
	                                                                                        Guid,
	                                                                                        ParentGuid)  Select 	PONumber,
	                                                                                        VendorNumber,
	                                                                                        VendorName,
	                                                                                        ManufacturerNumber,
	                                                                                        ManufacturerName,
	                                                                                        LineNumber,
	                                                                                        ItemNumber,
	                                                                                        ItemDescription,
	                                                                                        LineUM,
	                                                                                        DemandDeliveryDate,
	                                                                                        ReceiveQuantity,
	                                                                                        '" + dgvr.Cells["库"].ToString() + "','" + dgvr.Cells["位"].ToString() + "',";
                    string sqlInsert2 = @"InspectionPeriod,
                                                    LotNumber,
                                                    InternalLotNumber,
                                                    ExpiredDate,
                                                    Operator,
                                                    StockKeeper,
                                                    RetestDate,
                                                    LotNumberAssign,
                                                    OrderQuantity,
                                                    ItemReceiveType,
                                                    Supervisor,
                                                    ForeignNumber,
                                                    BuyerID,
                                                    FDAFlag,
                                                    '" + Guid.NewGuid().ToString("N") + "',Guid FROM PurchaseOrderRecordHistoryByCMF WHERE Guid = '" + guid + "' ";
                    sqlList.Add(sqlInsert + sqlInsert2);

                    double orderQuantity = Convert.ToDouble(dgvr.Cells["订货量"].Value);
                    double receiveQuantity = Convert.ToDouble(dgvr.Cells["入库量"].Value);
                    string sqlUpdateStatus = string.Empty;
                    string sqlUpdate = "Update PurchaseOrderRecordHistoryByCMF Set InternalLotNumber='" + dgvr.Cells["公司批号"].Value.ToString().ToUpper() + "',LotNumber='" + dgvr.Cells["厂家批号"].Value.ToString().ToUpper() + "',ExpiredDate='" + dgvr.Cells["到期日期"].Value.ToString() + "',Stock='" + dgvr.Cells["库"].Value.ToString().ToUpper() + "',Bin='" + dgvr.Cells["位"].Value.ToString().ToUpper() + "',RetestDate='" + dgvr.Cells["重测日期"].Value.ToString() + "',Status=0,Operator='" + StockUser.UserID + "',ReceiveQuantity=" + Convert.ToDouble(dgvr.Cells["入库量"].Value) + "  Where Guid = '" + dgvr.Cells["Guid"].Value.ToString() + "'";

                    if (receiveQuantity / orderQuantity >= 1)
                    {
                        sqlUpdateStatus = @"Update [FSDB].[dbo].[PurchaseOrderRecordByCMF] Set POStatus = 5,AccumulatedActualReceiveQuantity=AccumulatedActualReceiveQuantity+" + receiveQuantity + ",Remark='" + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss") + "," + dgvr.Cells["采购单号"].Value.ToString() + "," + dgvr.Cells["行号"].Value.ToString() + "," + dgvr.Cells["物料代码"].Value.ToString() + "," + receiveQuantity.ToString() + "|" + "' Where Guid='" + guid + "'";
                    }
                    else
                    {
                        sqlUpdateStatus = @"Update [FSDB].[dbo].[PurchaseOrderRecordByCMF] Set POStatus = 66,AccumulatedActualReceiveQuantity=AccumulatedActualReceiveQuantity+" + receiveQuantity + ",Remark='" + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss") + "," + dgvr.Cells["采购单号"].Value.ToString() + "," + dgvr.Cells["行号"].Value.ToString() + "," + dgvr.Cells["物料代码"].Value.ToString() + "," + receiveQuantity.ToString() + "|" + "'   Where Guid='" + guid + "'";
                    }
                    sqlList.Add(sqlUpdate);
                    sqlList.Add(sqlUpdateStatus);
                }

            }

            if (sqlList.Count == 0)
            {
                MessageBoxEx.Show("只有选中的行才能执行多厂区入库操作！", "提示");
            }
            else
            {
                if (SQLHelper.BatchExecuteNonQuery(GlobalSpace.FSDBConnstr, sqlList))
                {
                    MessageBoxEx.Show("提交成功！", "提示");
                    if (FOItemKeeperList.Contains(StockUser.UserID))
                    {
                        dgvPODetail.DataSource = GetVendorPOForeignItemsDetail(9);
                    }
                    else
                    {
                        dgvPODetail.DataSource = GetVendorPOItemsDetail(9);
                    }
                    dgvPODetail.Columns["Guid"].Visible = false;
                    dgvPODetail.Columns["ParentGuid"].Visible = false;
                    dgvPODetail.Columns["InspectionPeriod"].Visible = false;
                    dgvPODetail.Columns["StockKeeper"].Visible = false;
                    dgvPODetail.Columns["ItemReceiveType"].Visible = false;
                    dgvPODetail.Columns["VendorName"].Visible = false;
                    dgvPODetail.Columns["VendorNumber"].Visible = false;
                    dgvPODetail.Columns["入库量"].DefaultCellStyle.BackColor = Color.LightYellow;
                    dgvPODetail.Columns["公司批号"].DefaultCellStyle.BackColor = Color.LightYellow;
                    dgvPODetail.Columns["厂家批号"].DefaultCellStyle.BackColor = Color.LightYellow;
                    dgvPODetail.Columns["到期日期"].DefaultCellStyle.BackColor = Color.LightYellow;
                }
                else
                {
                    MessageBoxEx.Show("提交失败，请联系管理员！", "提示");
                }
            }
        }

        private void superTabControlPanel1_Click(object sender, EventArgs e)
        {

        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            FSFunctionLib.FSConfigFileInitialize(GlobalSpace.fsTestconfigfilepath, FSUserID, FSUserPassword);

            IMTR01 imtr = new IMTR01();
            imtr.ItemNumber.Value = "MF40050";
            imtr.InventoryQuantity.Value = "5";
            imtr.StockroomFrom.Value = "M9";
            imtr.BinFrom.Value = "07";
            imtr.InventoryCategoryFrom.Value = "I";
            imtr.LotNumberFrom.Value = "CPS11000320N";


            imtr.StockroomTo.Value = "M9";
            imtr.BinTo.Value = "07";
            imtr.InventoryCategoryTo.Value = "O";
            imtr.LotNumberTo.Value = "CPS11000320N";
            imtr.LotIdentifier.Value = "E";


            if (FSFunctionLib.fstiClient.ProcessId(imtr, null))
            {

                listResult.Items.Add("Success:");
                listResult.Items.Add("");
                listResult.Items.Add(FSFunctionLib.fstiClient.CDFResponse);
            }
            else
            {
                FSTIError error = FSFunctionLib.fstiClient.TransactionError;
                DumpErrorObject(imtr, error, listResult);
            }

            FSFunctionLib.FSExit();
        }

        private void btnItemReturnedFSOperate_Click(object sender, EventArgs e)
        {

        }

        private void btnImtr_Click(object sender, EventArgs e)
        {
            List<string> poList = new List<string>();
            int iCount = 0;
            if(dgvPODetailFS.Rows.Count == 0)
            {
                MessageBoxEx.Show("当前无可用记录！", "提示");
            }
            foreach (DataGridViewRow dgvr in dgvPODetailFS.Rows)
            {
                if (Convert.ToBoolean(dgvr.Cells["Check2"].Value) == true)
                {
                    iCount++;
                }
            }

            if(iCount == 0)
            {
                MessageBoxEx.Show("当前无选中的可用记录！", "提示");
            }

            FSFunctionLib.FSConfigFileInitialize(GlobalSpace.fsconfigfilepath, FSUserID, FSUserPassword);

            foreach (DataGridViewRow dgvr in dgvPODetailFS.Rows)
            {
                if (Convert.ToBoolean(dgvr.Cells["Check2"].Value) == true)
                {
                    IMTR01 imtr = new IMTR01();
                    imtr.ItemNumber.Value = dgvr.Cells["物料代码"].Value.ToString();
                    imtr.InventoryQuantity.Value = dgvr.Cells["入库数量"].Value.ToString();
                    imtr.StockroomFrom.Value = dgvr.Cells["库"].Value.ToString();
                    imtr.BinFrom.Value = dgvr.Cells["位"].Value.ToString();
                    imtr.InventoryCategoryFrom.Value = "I";
                    imtr.LotNumberFrom.Value = dgvr.Cells["厂家批号"].Value.ToString();

                    imtr.StockroomTo.Value = dgvr.Cells["库"].Value.ToString();
                    imtr.BinTo.Value = dgvr.Cells["位"].Value.ToString();
                    imtr.InventoryCategoryTo.Value = "O";
                    imtr.LotNumberTo.Value = dgvr.Cells["厂家批号"].Value.ToString();
                    imtr.LotIdentifier.Value = "E";


                    if (!FSFunctionLib.fstiClient.ProcessId(imtr, null))
                    {
                        FSTIError error = FSFunctionLib.fstiClient.TransactionError;
                        DumpErrorObject(imtr, error, listResult);
                        CommonOperate.WriteFSErrorLog("IMTR", imtr, error, FSUserID);
                        poList.Add(dgvr.Cells["采购单号"].Value.ToString() + " " + dgvr.Cells["行号"].Value.ToString() + " " + dgvr.Cells["物料代码"].Value.ToString() + " " + dgvr.Cells["描述"].Value.ToString() + " " + dgvr.Cells["公司批号"].Value.ToString());
                    }
                }
            }
            FSFunctionLib.FSExit();
            if (poList.Count > 0)
            {
                MessageBoxEx.Show("以下订单：" + string.Join(",", poList.ToArray()) + " 移库失败！", "提示");
            }
            else
            {
                MessageBoxEx.Show("全部移库成功！", "提示");
            }
        }

        private void btnAdjustStock_Click(object sender, EventArgs e)
        {
            BatchPORVX b = new BatchPORVX(FSUserID, FSUserPassword);
            b.ShowDialog();
        }

        private void btnPublicRefresh_Click(object sender, EventArgs e)
        {
            dgvPublic.DataSource = GetPublicVendorPOItemsDetail(9, dtpPublic.Value.AddDays(-30).ToString("yyyy-MM-dd"));
            dgvPublic.Columns["Guid"].Visible = false;
        }


        //
        private DataTable GetPublicVendorPOItemsDetail(int status, string date)
        {
            string sqlSelect = @"SELECT
	                                        T1.Guid,T1.ReceiveDate AS 确认日期,T1.ForeignNumber AS 外贸单号,
	                                        T1.PONumber AS 采购单号,T1.VendorNumber AS 供应商码,T1.VendorName AS 供应商名,
	                                        T1.ManufacturerNumber AS 生产商码,
	                                        T1.ManufacturerName AS 生产商名,
	                                        T1.LineNumber AS 行号,
	                                        T1.ItemNumber AS 物料代码,
	                                        T1.ItemDescription AS 描述,
	                                        T1.LineUM AS 单位,
	                                        T1.OrderQuantity AS 订货量,  T1.Stock AS 库,
	                                        T1.Bin AS 位	                                    
                                        FROM
	                                        PurchaseOrderRecordHistoryByCMF T1
                                        WHERE
	                                        T1.Status = " + status + "    AND  LEFT (T1.StockKeeper, 2) = 'CX'  And  T1.ReceiveDate>='" + date + "' ORDER BY  T1.ReceiveDate, T1.LineNumber ASC";
            return SQLHelper.GetDataTable(GlobalSpace.FSDBConnstr, sqlSelect);
        }
        internal class PrintService
        {
            public PrintService()
            {

            }

            #region Members //成员
            public String printName = String.Empty;
            public Font prtTextFont = new Font("Verdana", 10);
            public Font prtTitleFont = new Font("宋体", 10);
            private String[] titles = new String[0];
            public String[] Titles
            {
                set
                {
                    titles = value as String[];
                    if (null == titles)
                    {
                        titles = new String[0];
                    }
                }
                get
                {
                    return titles;
                }
            }
            private Int32 left = 20;
            private Int32 top = 20;
            public Int32 Top { set { top = value; } get { return top; } }
            public Int32 Left { set { left = value; } get { return left; } }
            private DataTable printedTable;
            private Int32 pheight;
            private Int32 pWidth;
            private Int32 pindex;
            private Int32 curdgi;
            private Int32[] width;
            private Int32 rowOfDownDistance = 3;
            private Int32 rowOfUpDistance = 2;

            Int32 startColumnControls = 0;
            Int32 endColumnControls = 0;

            #endregion

            #region Method for PrintDataTable //打印数据集
            /// <summary>
            /// 打印数据集
            /// </summary>
            /// <param name="table">数据集</param>
            /// <returns></returns>
            public Boolean PrintDataTable(DataTable table)
            {
                PrintDocument prtDocument = new PrintDocument();
                try
                {
                    if (printName != String.Empty)
                    {
                        prtDocument.PrinterSettings.PrinterName = printName;
                    }
                    prtDocument.DefaultPageSettings.Landscape = true;
                    prtDocument.OriginAtMargins = false;
                    PrintDialog prtDialog = new PrintDialog();
                    prtDialog.AllowSomePages = true;
                    prtDialog.ShowHelp = false;
                    prtDialog.Document = prtDocument;
                    if (DialogResult.OK != prtDialog.ShowDialog())
                    {
                        return false;
                    }
                    printedTable = table;
                    pindex = 0;
                    curdgi = 0;
                    width = new Int32[printedTable.Columns.Count];
                    pheight = prtDocument.PrinterSettings.DefaultPageSettings.Bounds.Bottom + 400;
                    //pheight = prtDocument.PrinterSettings.DefaultPageSettings.Bounds.Bottom;
                    pWidth = prtDocument.PrinterSettings.DefaultPageSettings.Bounds.Right;
                    prtDocument.PrintPage += new PrintPageEventHandler(docToPrint_PrintPage);
                    prtDocument.Print();
                }
                catch (InvalidPrinterException ex)
                {
                    MessageBox.Show("没有安装打印机");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                prtDocument.Dispose();
                return true;
            }
            #endregion

            #region Handler for docToPrint_PrintPage //设置打印机开始打印的事件处理函数
            /// <summary>
            /// 设置打印机开始打印的事件处理函数
            /// </summary>
            /// <param name="sender"></param>
            /// <param name="ev"></param>
            private void docToPrint_PrintPage(object sender,
             System.Drawing.Printing.PrintPageEventArgs ev)//设置打印机开始打印的事件处理函数
            {
                Int32 x = left;
                Int32 y = top;
                Int32 rowOfTop = top;
                Int32 i;
                Pen pen = new Pen(Brushes.Black, 1);
                if (0 == pindex)
                {
                    for (i = 0; i < titles.Length; i++)
                    {
                        ev.Graphics.DrawString(titles[i], prtTitleFont, Brushes.Black, left, y + rowOfUpDistance);
                        y += prtTextFont.Height + rowOfDownDistance;
                    }
                    rowOfTop = y;
                    foreach (DataRow dr in printedTable.Rows)
                    {
                        for (i = 0; i < printedTable.Columns.Count; i++)
                        {
                            Int32 colwidth = Convert.ToInt16(ev.Graphics.MeasureString(dr[i].ToString().Trim(), prtTextFont).Width);
                            if (colwidth > width[i])
                            {
                                width[i] = colwidth;
                            }
                        }
                    }
                }
                for (i = endColumnControls; i < printedTable.Columns.Count; i++)
                {
                    String headtext = printedTable.Columns[i].ColumnName.Trim();
                    if (pindex == 0)
                    {
                        int colwidth = Convert.ToInt16(ev.Graphics.MeasureString(headtext, prtTextFont).Width);
                        if (colwidth > width[i])
                        {
                            width[i] = colwidth;
                        }
                    }
                    //判断宽是否越界
                    if (x + width[i] > pheight)
                    {
                        break;
                    }
                    ev.Graphics.DrawString(headtext, prtTextFont, Brushes.Black, x, y + rowOfUpDistance);
                    x += width[i];
                }
                startColumnControls = endColumnControls;
                if (i < printedTable.Columns.Count)
                {
                    endColumnControls = i;
                    ev.HasMorePages = true;
                }
                else
                {
                    endColumnControls = printedTable.Columns.Count;
                }
                ev.Graphics.DrawLine(pen, left, rowOfTop, x, rowOfTop);
                y += rowOfUpDistance + prtTextFont.Height + rowOfDownDistance;
                ev.Graphics.DrawLine(pen, left, y, x, y);
                //打印数据
                for (i = curdgi; i < printedTable.Rows.Count; i++)
                {
                    x = left;
                    for (Int32 j = startColumnControls; j < endColumnControls; j++)
                    {
                        ev.Graphics.DrawString(printedTable.Rows[i][j].ToString().Trim(), prtTextFont, Brushes.Black, x, y + rowOfUpDistance);
                        x += width[j];
                    }
                    y += rowOfUpDistance + prtTextFont.Height + rowOfDownDistance;
                    ev.Graphics.DrawLine(pen, left, y, x, y);
                    //判断高是否越界
                    if (y > pWidth - prtTextFont.Height - 400) //if (y > pWidth - prtTextFont.Height - 30)
                    {
                        break;
                    }
                }
                ev.Graphics.DrawLine(pen, left, rowOfTop, left, y);
                x = left;
                for (Int32 k = startColumnControls; k < endColumnControls; k++)
                {
                    x += width[k];
                    ev.Graphics.DrawLine(pen, x, rowOfTop, x, y);
                }
                //判断高是否越界
                if (y > pWidth - prtTextFont.Height - 400) //if (y > pWidth - prtTextFont.Height - 30) 
                {
                    pindex++; if (0 == startColumnControls)
                    {
                        curdgi = i + 1;
                    }
                    if (!ev.HasMorePages)
                    {
                        endColumnControls = 0;
                    }
                    ev.HasMorePages = true;
                }
            }
            #endregion
        }

        private void btnPublicConfirm_Click(object sender, EventArgs e)
        {
            List<string> sqlList = new List<string>();
            Boolean isChecked = false;
            foreach (DataGridViewRow dgvr in dgvPublic.Rows)
            {
                if (Convert.ToBoolean(dgvr.Cells["PublicCheck"].Value))
                {
                    string sqlUpdate = @"Update PurchaseOrderRecordHistoryByCMF Set StockKeeper='" + StockUser.UserID + "|" + StockUser.UserName + "' Where Guid='" + dgvr.Cells["Guid"].Value.ToString() + "'";
                    sqlList.Add(sqlUpdate);
                    isChecked = true;
                }
            }
            if (!isChecked)
            {
                MessageBoxEx.Show("当前无选中行！", "提示");
                return;
            }
            if (SQLHelper.BatchExecuteNonQuery(GlobalSpace.FSDBConnstr, sqlList))
            {
                MessageBoxEx.Show("确认成功，请到待处理订单中填写信息！", "提示");
                dgvPublic.DataSource = GetPublicVendorPOItemsDetail(9, dtpPublic.Value.AddDays(-3).ToString("yyyy-MM-dd"));
                dgvPublic.Columns["Guid"].Visible = false;
                IsNeedToConfirm = true;
                dgvPODetail.DataSource = GetVendorPOItemsDetail(9);
                dgvPODetail.Columns["Guid"].Visible = false;
                dgvPODetail.Columns["ParentGuid"].Visible = false;
                dgvPODetail.Columns["InspectionPeriod"].Visible = false;
                dgvPODetail.Columns["StockKeeper"].Visible = false;
                dgvPODetail.Columns["ItemReceiveType"].Visible = false;
                dgvPODetail.Columns["VendorName"].Visible = false;
                dgvPODetail.Columns["VendorNumber"].Visible = false;
                dgvPODetail.Columns["入库量"].DefaultCellStyle.BackColor = Color.LightYellow;
                dgvPODetail.Columns["公司批号"].DefaultCellStyle.BackColor = Color.LightYellow;
                dgvPODetail.Columns["厂家批号"].DefaultCellStyle.BackColor = Color.LightYellow;
                dgvPODetail.Columns["到期日期"].DefaultCellStyle.BackColor = Color.LightYellow;
            }
            else
            {
                MessageBoxEx.Show("确认失败！", "提示");
            }
        }

        private void btnMultiplyStockRecv_Click(object sender, EventArgs e)
        {

            List<string> sqlList = new List<string>();

            foreach (DataGridViewRow dgvr in dgvPublic.Rows)
            {
                if (Convert.ToBoolean(dgvr.Cells["PublicCheck"].Value))
                {
                    string guid = dgvr.Cells["Guid"].Value.ToString();
                    string sqlInsert = @"Insert Into PurchaseOrderRecordHistoryByCMF (PONumber,
	                                                                                        VendorNumber,
	                                                                                        VendorName,
	                                                                                        ManufacturerNumber,
	                                                                                        ManufacturerName,
	                                                                                        LineNumber,
	                                                                                        ItemNumber,
	                                                                                        ItemDescription,
	                                                                                        LineUM,
	                                                                                        DemandDeliveryDate,
	                                                                                        ReceiveQuantity,
	                                                                                        Stock,
	                                                                                        Bin,
	                                                                                        InspectionPeriod,
	                                                                                        LotNumber,
	                                                                                        InternalLotNumber,
	                                                                                        ExpiredDate,
	                                                                                        Operator,
	                                                                                        StockKeeper,
	                                                                                        RetestDate,
	                                                                                        LotNumberAssign,
	                                                                                        OrderQuantity,
	                                                                                        ItemReceiveType,
	                                                                                        Supervisor,
	                                                                                        ForeignNumber,
	                                                                                        BuyerID,
	                                                                                        FDAFlag,
	                                                                                        Guid,
	                                                                                        ParentGuid,UnitPrice)  Select 	PONumber,
	                                                                                        VendorNumber,
	                                                                                        VendorName,
	                                                                                        ManufacturerNumber,
	                                                                                        ManufacturerName,
	                                                                                        LineNumber,
	                                                                                        ItemNumber,
	                                                                                        ItemDescription,
	                                                                                        LineUM,
	                                                                                        DemandDeliveryDate,
	                                                                                        ReceiveQuantity,
	                                                                                        '" + dgvr.Cells["库"].Value.ToString() + "','" + dgvr.Cells["位"].Value.ToString() + "',";
                    string sqlInsert2 = @"InspectionPeriod,
                                                    LotNumber,
                                                    InternalLotNumber,
                                                    ExpiredDate,
                                                    Operator,  '" + StockUser.UserID + "|" + StockUser.UserName + "'";
                    string sqlInsert3 = @",RetestDate,
                                                    LotNumberAssign,
                                                    OrderQuantity,
                                                    ItemReceiveType,
                                                    Supervisor,
                                                    ForeignNumber,
                                                    BuyerID,
                                                    FDAFlag,
                                                    '" + Guid.NewGuid().ToString("N") + "',Guid,UnitPrice FROM PurchaseOrderRecordHistoryByCMF WHERE Guid = '" + guid + "' ";
                    sqlList.Add(sqlInsert + sqlInsert2 + sqlInsert3);
                }

            }
            if (MessageBoxEx.Show("多厂区入库，需要修改库位，请确认已经修改完毕！", "提示", MessageBoxButtons.YesNo) == DialogResult.No)
            {
                return;
            }
            if (sqlList.Count == 0)
            {
                MessageBoxEx.Show("只有选中的行才能执行多厂区入库操作！", "提示");
            }
            else
            {
              /*  MessageBox.Show(sqlList[0]);
                MessageBox.Show(sqlList[1]);
              */
                
                if (SQLHelper.BatchExecuteNonQuery(GlobalSpace.FSDBConnstr, sqlList))
                {
                    MessageBoxEx.Show("提交成功！", "提示");
                }
                else
                {
                    MessageBoxEx.Show("提交失败，请联系管理员！", "提示");
                }
            
            }
        }
            private void dgvVials_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
            {
                dgvVials_CellDoubleClick(sender, e);
            }

            private void dgvVials_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
            {
                tbItemNumber.Text = dgvVials.Rows[dgvVials.CurrentCell.RowIndex].Cells["代码"].Value.ToString();
                tbItemDescription.Text = dgvVials.Rows[dgvVials.CurrentCell.RowIndex].Cells["描述"].Value.ToString();
            }

            private void btnVialSubmit_Click(object sender, EventArgs e)
            {
                if (string.IsNullOrWhiteSpace(tbItemNumber.Text) || string.IsNullOrWhiteSpace(tbItemDescription.Text) || string.IsNullOrWhiteSpace(tbExpiredDate.Text) || string.IsNullOrWhiteSpace(tbLotNumber.Text) || string.IsNullOrWhiteSpace(tbInternalLotNumber.Text) || string.IsNullOrWhiteSpace(tbReceiveQuantity.Text))
                {
                    MessageBoxEx.Show("不能有空项！", "提示");
                    return;
                }
                string sqlInsert = @"INSERT INTO [FSDB].[dbo].[PurchaseDepartmentVial] (	
	                                [ItemNumber],
	                                [ItemDescription],
	                                [Quantity],
	                                [LotNumber],
	                                [InternalLotNumber],
	                                [ExpirationDate],VendorNumber,VendorName
                                )
                                VALUES ('" + tbItemNumber.Text + "', '" + tbItemDescription.Text + "'," + Convert.ToDouble(tbReceiveQuantity.Text) + ",'" + tbLotNumber.Text + "','" + tbInternalLotNumber.Text + "','" + tbExpiredDate.Text.Substring(5, 2) + tbExpiredDate.Text.Substring(8, 2) + tbExpiredDate.Text.Substring(2, 2) + "','370021','山东省药用玻璃股份有限公司')";
                if (SQLHelper.ExecuteNonQuery(GlobalSpace.FSDBConnstr, sqlInsert))
                {
                    MessageBoxEx.Show("提交成功！", "提示");
                    dgvVialsDetail.DataSource = GetSubmittedVialRecord();
                }
                else
                {
                    MessageBoxEx.Show("提交失败！", "提示");
                }
            }

            private DataTable GetSubmittedVialRecord()
            {
                string sqlSelect = @"SELECT
	                            [ItemNumber] AS 代码,
	                            [ItemDescription] AS 描述,
	                            [Quantity] AS 数量,
	                            [LotNumber] AS 厂家批号,
	                            [InternalLotNumber] AS 公司批号,
	                            [ExpirationDate] AS 过期日期
                            FROM
	                            PurchaseDepartmentVial
                            WHERE
	                            Status = 0";
                return SQLHelper.GetDataTable(GlobalSpace.FSDBConnstr, sqlSelect);
            }

            private void btnManualFS_Click(object sender, EventArgs e)
            {
                List<string> sqlList = new List<string>();

                foreach (DataGridViewRow dgvr in dgvPODetailFS.Rows)
                {
                    if (Convert.ToBoolean(dgvr.Cells["Check2"].Value))
                    {
                        if (dgvr.Cells["Status"].Value.ToString() == "1")
                        {
                        string sqlUpdate = @"Update PurchaseOrderRecordHistoryByCMF Set Status = 2,FSOperateDateTime = '" + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss") + "',FSOperateDate='" + DateTime.Now.ToString("yyyy-MM-dd") + "',FSOperator='" + StockUser.UserID + "|手工' Where Guid='" + dgvr.Cells["Guid"].Value.ToString() + "' and Status = 1";
                        //string sqlUpdate = @"Update PurchaseOrderRecordHistoryByCMF Set Status =2,FSOperateDateTime='"+DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")+"'  Where Guid='" + dgvr.Cells["Guid"].Value.ToString() + "'";
                            sqlList.Add(sqlUpdate);
                        }
                    }
                }

                if (sqlList.Count == 0)
                {
                    Custom.MsgEx("当前无选中行！");
                    return;
                }

                if (SQLHelper.BatchExecuteNonQuery(GlobalSpace.FSDBConnstr, sqlList))
                {
                    Custom.MsgEx("更新成功！");
                    dgvPODetailFS.DataSource = GetReceivedRecordByStatus(1);
                    dgvPODetailFS.Columns["Guid"].Visible = false;
                    dgvPODetailFS.Columns["Status"].Visible = false;
                }
                else
                {
                    Custom.MsgEx("更新失败！");
                }
            }

            private void dgvVialsDetail_CellContentClick(object sender, DataGridViewCellEventArgs e)
            {

            }

            private void dgvVials_CellEndEdit(object sender, DataGridViewCellEventArgs e)
            {

            }

            private void tbItemNumber_TextChanged(object sender, EventArgs e)
            {
                tbItemNumber.Text = tbItemNumber.Text.ToUpper();
                tbItemNumber.SelectionStart = tbItemNumber.Text.Length;
            }

            private void tbLotNumber_TextChanged(object sender, EventArgs e)
            {

            }

            private void tbInternalLotNumber_TextChanged(object sender, EventArgs e)
            {

            }

            private void tbReceiveQuantity_KeyPress(object sender, KeyPressEventArgs e)
            {
                if (!string.IsNullOrWhiteSpace(tbReceiveQuantity.Text))
                {
                    if (e.KeyChar == (char)13)
                    {
                        tbLotNumber.Focus();
                    }
                }
            }

            private void tbLotNumber_KeyPress(object sender, KeyPressEventArgs e)
            {
                if (!string.IsNullOrWhiteSpace(tbLotNumber.Text))
                {
                    if (e.KeyChar == (char)13)
                    {
                        tbInternalLotNumber.Focus();
                    }
                }
            }

            private void tbInternalLotNumber_KeyPress(object sender, KeyPressEventArgs e)
            {
                if (!string.IsNullOrWhiteSpace(tbInternalLotNumber.Text))
                {
                    if (e.KeyChar == (char)13)
                    {
                        tbExpiredDate.Focus();
                    }
                }
            }

            private void tbExpiredDate_KeyPress(object sender, KeyPressEventArgs e)
            {
                if (!string.IsNullOrWhiteSpace(tbExpiredDate.Text))
                {
                    if (e.KeyChar == (char)13)
                    {
                        btnVialSubmit_Click(sender, e);
                    }
                }

            }

            private void btnConvertKeeper_Click(object sender, EventArgs e)
            {
                List<string> sqlList = new List<string>();
                foreach (DataGridViewRow dgvr in dgvPOItemDetailView.Rows)
                {
                    if (Convert.ToBoolean(dgvr.Cells["Check"].Value))
                    {
                        string sqlUpdate = @"Update PurchaseOrderRecordHistoryByCMF Set StockKeeper='" + StockUser.UserID + "|" + StockUser.UserName + "' Where Guid='" + dgvr.Cells["Guid"].Value.ToString() + "' and Status =9";
                        sqlList.Add(sqlUpdate);
                    }
                }

                if (sqlList.Count == 0)
                {
                    MessageBoxEx.Show("当前无选中的行！", "提示");
                    return;
                }
                else
                {
                    if (SQLHelper.BatchExecuteNonQuery(GlobalSpace.FSDBConnstr, sqlList))
                    {
                        MessageBoxEx.Show("转换成功！", "提示");
                        dgvPOItemDetailView.DataSource = GetReceivedRecordByPONumber(tbItemDesc.Text);
                        dgvPOItemDetailView.Columns["Guid"].Visible = false;
                        dgvPOItemDetailView.Columns["Status"].Visible = false;
                    }
                    else
                    {
                        MessageBoxEx.Show("转换失败！", "提示");
                    }
                }
            }

            private void btnPrintPO_Click(object sender, EventArgs e)
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("联系单号");
                dt.Columns.Add("代码");
                dt.Columns.Add("描述");
                dt.Columns.Add("供应商码");
                dt.Columns.Add("供应商名");
                dt.Columns.Add("生产商码");
                dt.Columns.Add("生产商名");
                dt.Columns.Add("入库量");
                dt.Columns.Add("厂家批号");
                dt.Columns.Add("公司批号");
                dt.Columns.Add("到期日期");
                dt.Columns.Add("件数");
                bool isChecked = false;
                foreach (DataGridViewRow dgvr in dgvPODetail.Rows)
                {
                    if (Convert.ToBoolean(dgvr.Cells["Checked"].Value))
                    {
                        isChecked = true;
                        DataRow dr = dt.NewRow();
                        dr["联系单号"] = dgvr.Cells["外贸单号"].Value.ToString();
                        dr["代码"] = dgvr.Cells["物料代码"].Value.ToString();
                        dr["描述"] = dgvr.Cells["描述"].Value.ToString();
                        dr["供应商码"] = dgvr.Cells["供应商码"].Value.ToString();
                        dr["供应商名"] = dgvr.Cells["供应商名"].Value.ToString();
                        dr["生产商码"] = dgvr.Cells["生产商码"].Value.ToString();
                        dr["生产商名"] = dgvr.Cells["生产商名"].Value.ToString();
                        dr["入库量"] = dgvr.Cells["入库量"].Value.ToString();
                        dr["厂家批号"] = dgvr.Cells["厂家批号"].Value.ToString();
                        dr["公司批号"] = dgvr.Cells["公司批号"].Value.ToString();
                        dr["到期日期"] = dgvr.Cells["到期日期"].Value.ToString();
                        dr["件数"] = dgvr.Cells["整件数"].Value.ToString();
                        dt.Rows.Add(dr.ItemArray);
                    }
                }
                if (!isChecked)
                {
                    MessageBoxEx.Show("没有选中的行！", "提示");
                    return;
                }

                PrintPO pp = new PrintPO(dt, "\\PrintPO.grf");
                pp.Show();

            }

            private void btnAllCheck_Click(object sender, EventArgs e)
            {
                foreach (DataGridViewRow dgvr in dgvPODetail.Rows)
                {
                    dgvr.Cells["Checked"].Value = true;
                }

            }

            private void btnARefresh_Click(object sender, EventArgs e)
            {
                dgvAssistant.DataSource = GetAssistantVendorPOItemsDetail("", "");
                dgvAssistant.Columns["Guid"].Visible = false;
                dgvAssistant.Columns["件数"].Visible = false;
                dgvAssistant.Columns["件数详情"].Visible = false;
            }
            private DataTable GetAssistantVendorPOItemsDetail(string type, string str)
            {
                string sqlSelect = @"SELECT
	                                        T1.Guid,
	                                        T1.PONumber AS 采购单号,T1.VendorNumber AS 供应商码,T1.VendorName AS 供应商名,
	                                        T1.LineNumber AS 行号,
	                                        T1.ItemNumber AS 物料代码,
	                                        T1.ItemDescription AS 描述,
	                                        T1.LineUM AS 单位,
	                                        T1.OrderQuantity AS 订货量,T1.OrderQuantity AS 入库量,  T1.Stock AS 库, T1.Bin AS 位,'' AS 件数,FONumberOfPackages AS 件数详情                                    
                                        FROM
	                                        PurchaseOrderRecordHistoryByCMF T1
                                        WHERE
	                                        T1.Status = 9    AND  (T1.ItemReceiveType='A' OR T1.ItemReceiveType='M' )  ";
                string sqlCriteria = string.Empty;
                if (type == "Vendor")
                {
                    sqlCriteria = " And T1.VendorName Like '%" + str + "%'";
                }
                else if (type == "Item")
                {
                    sqlCriteria = " And T1.ItemDescription Like '%" + str + "%'";
                }
                else
                {
                    sqlCriteria = " And 1 =1";
                }
                return SQLHelper.GetDataTable(GlobalSpace.FSDBConnstr, sqlSelect + sqlCriteria);
            }
            private void btnFSMakeAllCheck_Click(object sender, EventArgs e)
            {
                foreach (DataGridViewRow dgvr in dgvPODetailFS.Rows)
                {
                    dgvr.Cells["Check2"].Value = true;
                }
            }

            private void tbVendorName_KeyPress(object sender, KeyPressEventArgs e)
            {
                if (!string.IsNullOrWhiteSpace(tbVendorName.Text))
                {
                    if (e.KeyChar == (char)13)
                    {
                        dgvAssistant.DataSource = GetAssistantVendorPOItemsDetail("Vendor", tbVendorName.Text);
                        dgvAssistant.Columns["Guid"].Visible = false;
                    }
                }
            }

            private void tbAssistantItem_KeyPress(object sender, KeyPressEventArgs e)
            {
                if (!string.IsNullOrWhiteSpace(tbAssistantItem.Text))
                {
                    if (e.KeyChar == (char)13)
                    {
                        dgvAssistant.DataSource = GetAssistantVendorPOItemsDetail("Item", tbAssistantItem.Text);
                        dgvAssistant.Columns["Guid"].Visible = false;
                    }
                }
            }

            private void btnAssistantAllChecked_Click(object sender, EventArgs e)
            {
                foreach (DataGridViewRow dgvr in dgvAssistant.Rows)
                {
                    dgvr.Cells["ACheck"].Value = true;
                }
            }

            private void btnAssistantConfirm_Click(object sender, EventArgs e)
            {
                List<string> sqlList = new List<string>();
                foreach (DataGridViewRow dgvr in dgvAssistant.Rows)
                {
                    if (Convert.ToBoolean(dgvr.Cells["ACheck"].Value))
                    {
                        string sqlUpdate = @"Update PurchaseOrderRecordHistoryByCMF Set ReceiveQuantity=" + Convert.ToDouble(dgvr.Cells["入库量"].Value) + ",ReceiveDate='" + DateTime.Now.ToString("yyyy-MM-dd") + "',SubmitOperateDateTime='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "',Operator='" + StockUser.UserID + "',IsDirectERP=1,Status = 1 Where Guid = '" + dgvr.Cells["Guid"].Value.ToString() + "'";
                        sqlList.Add(sqlUpdate);
                    }
                }

                if (sqlList.Count == 0)
                {
                    Custom.MsgEx("无可用记录！");
                }
                else
                {
                    if (SQLHelper.BatchExecuteNonQuery(GlobalSpace.FSDBConnstr, sqlList))
                    {
                        Custom.MsgEx("确认成功！");
                        dgvAssistant.DataSource = GetAssistantVendorPOItemsDetail("", "");
                        dgvAssistant.Columns["Guid"].Visible = false;
                    }
                    else
                    {
                        Custom.MsgEx("确认失败！");
                    }
                }

            }

            private void btnAAllUnChecked_Click(object sender, EventArgs e)
            {
                foreach (DataGridViewRow dgvr in dgvAssistant.Rows)
                {
                    dgvr.Cells["ACheck"].Value = false;

                }
            }

            private void btnViewReturn_Click(object sender, EventArgs e)
            {
                List<string> sqlList = new List<string>();
                foreach (DataGridViewRow dgvr in dgvPOItemDetailView.Rows)
                {
                    if (Convert.ToBoolean(dgvr.Cells["Check"].Value))
                    {
                        string sqlUpdate = @"Update PurchaseOrderRecordHistoryByCMF Set Status= 9 Where Guid='" + dgvr.Cells["Guid"].Value.ToString() + "' and Status= 0";
                        sqlList.Add(sqlUpdate);
                    }
                }

                if (sqlList.Count == 0)
                {
                    MessageBoxEx.Show("当前无选中的行！", "提示");
                    return;
                }
                else
                {
                    if (SQLHelper.BatchExecuteNonQuery(GlobalSpace.FSDBConnstr, sqlList))
                    {
                        MessageBoxEx.Show("退回成功！", "提示");
                    dgvPOItemDetailView.DataSource = GetReceivedRecordByStatusAndUserID(0, StockUser.UserID);
                    dgvPOItemDetailView.Columns["Guid"].Visible = false;
                    dgvPOItemDetailView.Columns["Status"].Visible = false;
                    dgvPOItemDetailView.Columns["BuyerID"].Visible = false;
                    dgvPOItemDetailView.Columns["FDAFlag"].Visible = false;
                    dgvPOItemDetailView.Columns["ItemReceiveType"].Visible = false;
                    dgvPOItemDetailView.Columns["StockKeeper"].Visible = false;
                    dgvPOItemDetailView.Columns["LotNumberAssign"].Visible = false;
                }
                    else
                    {
                        MessageBoxEx.Show("退回失败！", "提示");
                    }
                }
            }

            private void btnViewDelete_Click(object sender, EventArgs e)
            {
                List<string> sqlList = new List<string>();
                foreach (DataGridViewRow dgvr in dgvPOItemDetailView.Rows)
                {
                    if (Convert.ToBoolean(dgvr.Cells["Check"].Value))
                    {
                        string sqlUpdate = @"Update  PurchaseOrderRecordHistoryByCMF Set Status = 77,DeleteOperateDateTime='" + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss") + "'+'|'+'" + StockUser.UserID + "'    Where Guid='" + dgvr.Cells["Guid"].Value.ToString() + "' and Status= 0";
                        sqlList.Add(sqlUpdate);
                    }
                }

                if (sqlList.Count == 0)
                {
                    MessageBoxEx.Show("当前无选中的行！", "提示");
                    return;
                }
                else
                {
                    if (SQLHelper.BatchExecuteNonQuery(GlobalSpace.FSDBConnstr, sqlList))
                    {
                        MessageBoxEx.Show("删除成功！", "提示");
                    dgvPOItemDetailView.DataSource = GetReceivedRecordByStatusAndUserID(0, StockUser.UserID);
                    dgvPOItemDetailView.Columns["Guid"].Visible = false;
                    dgvPOItemDetailView.Columns["Status"].Visible = false;
                    dgvPOItemDetailView.Columns["BuyerID"].Visible = false;
                    dgvPOItemDetailView.Columns["FDAFlag"].Visible = false;
                    dgvPOItemDetailView.Columns["ItemReceiveType"].Visible = false;
                    dgvPOItemDetailView.Columns["StockKeeper"].Visible = false;
                    dgvPOItemDetailView.Columns["LotNumberAssign"].Visible = false;
                }
                    else
                    {
                        MessageBoxEx.Show("删除失败！", "提示");
                    }
                }
            }

            private void btnReturnToMyself_Click(object sender, EventArgs e)
            {
                List<string> sqlList = new List<string>();

                foreach (DataGridViewRow dgvr in dgvPODetailFS.Rows)
                {
                    if (Convert.ToBoolean(dgvr.Cells["Check2"].Value))
                    {
                        if (dgvr.Cells["Status"].Value.ToString() == "1")
                        {
                            string sqlUpdate = @"Update PurchaseOrderRecordHistoryByCMF Set Status =1  Where Guid='" + dgvr.Cells["Guid"].Value.ToString() + "'";
                            sqlList.Add(sqlUpdate);
                        }
                    }
                }

                if (sqlList.Count == 0)
                {
                    Custom.MsgEx("当前无选中行！");
                    return;
                }

                if (SQLHelper.BatchExecuteNonQuery(GlobalSpace.FSDBConnstr, sqlList))
                {
                    Custom.MsgEx("更新成功！");
                    dgvPODetailFS.DataSource = GetReceivedRecordByStatus(1);
                    dgvPODetailFS.Columns["Guid"].Visible = false;
                    dgvPODetailFS.Columns["Status"].Visible = false;
                }
                else
                {
                    Custom.MsgEx("更新失败！");
                }
            }

            private void btnFP_Click(object sender, EventArgs e)
            {
                IsNeedToConfirm = true;
                dgvPODetail.DataSource = GetVendorPOForeignItemsDetail(dtpFP.Value.ToString("yyyy-MM-dd"));
                dgvPODetail.Columns["Guid"].Visible = false;
                dgvPODetail.Columns["ParentGuid"].Visible = false;
                dgvPODetail.Columns["InspectionPeriod"].Visible = false;
                dgvPODetail.Columns["StockKeeper"].Visible = false;
                dgvPODetail.Columns["ItemReceiveType"].Visible = false;
                dgvPODetail.Columns["VendorName"].Visible = false;
                dgvPODetail.Columns["VendorNumber"].Visible = false;
                dgvPODetail.Columns["入库量"].DefaultCellStyle.BackColor = Color.LightYellow;
                dgvPODetail.Columns["公司批号"].DefaultCellStyle.BackColor = Color.LightYellow;
                dgvPODetail.Columns["厂家批号"].DefaultCellStyle.BackColor = Color.LightYellow;
                dgvPODetail.Columns["到期日期"].DefaultCellStyle.BackColor = Color.LightYellow;
                dgvPODetail.Columns["整件数"].DefaultCellStyle.BackColor = Color.LightYellow;
            }

        /*   private void btnPrintBatchRecord_Click(object sender, EventArgs e)
           {

               if(dgvPOItemDetailView.Rows.Count == 0)
               {
                   MessageBoxEx.Show("当前无可用的记录！", "提示");
                   return;
               }

               if(string.IsNullOrWhiteSpace(StockUser.FileTracedNumber))
               {
                   MessageBoxEx.Show("当前库管员未查到请验记录版本信息，无法进行提交！", "提示");
                   return;
               }

               List<string> guidList = new List<string>();
               Dictionary<string, string> dict = new Dictionary<string, string>();
               string sqlSelectDesc = @"Select ItemNumber,ProductName From PurchaseDepartmentStockProductName ";

               bool isExistRecord = false;

               dict = SQLHelper.GetDataTable(GlobalSpace.FSDBConnstr, sqlSelectDesc).Rows.Cast<DataRow>().ToDictionary(r => r["ItemNumber"].ToString(), r => r["ProductName"].ToString());

               DataTable dtTemp0 = (DataTable)dgvPOItemDetailView.DataSource;
               DataTable dtTemp = dtTemp0.Clone();

               for (int m = 0; m < dgvPOItemDetailView.Rows.Count; m++)
               {
                   if (Convert.ToBoolean(dgvPOItemDetailView.Rows[m].Cells["Check"].Value) == true)
                   {
                       DataRow dr = dtTemp.NewRow();
                       guidList.Add(dgvPOItemDetailView.Rows[m].Cells["Guid"].Value.ToString());
                       dr = (dgvPOItemDetailView.Rows[m].DataBoundItem as DataRowView).Row;
                       dtTemp.Rows.Add(dr.ItemArray);
                   }
               }
               string itemNumber = string.Empty;
               bool isExistMultiplyPOitem = false;
               int multiplyPOItemCount = 0;
               bool isExistEmptyValue = false;
               bool isExistDictEmpty = false;
               foreach (DataRow  dgvr in dtTemp.Rows)
               {             
                       if (!dict.ContainsKey(dgvr["物料代码"].ToString()))
                       {
                           itemNumber += dgvr["物料代码"].ToString() + "  ";
                       }
                       else
                       {
                           if(!string.IsNullOrWhiteSpace(dict[dgvr["物料代码"].ToString()]))
                           {
                               dgvr["描述"] = dict[dgvr["物料代码"].ToString()];
                           }
                           else
                           {
                               isExistDictEmpty = true;
                           }
                       }

                       DataRow[] drs = dtTemp.Select("物料代码='" + dgvr["物料代码"].ToString() + "' And 厂家批号='" + dgvr["厂家批号"].ToString() + "'");
                       multiplyPOItemCount = drs.Length;
                       if (multiplyPOItemCount > 1)
                       {
                           isExistMultiplyPOitem = true;
                       }

                       if(dgvr["入库数量"] == DBNull.Value || dgvr["入库数量"].ToString()=="0" || dgvr["厂家批号"] == DBNull.Value || dgvr["厂家批号"].ToString() == "" || dgvr["公司批号"] == DBNull.Value || dgvr["公司批号"].ToString() == "")
                       {
                           isExistEmptyValue = true;
                       }
               }
               if(isExistEmptyValue)
               {
                   MessageBoxEx.Show("入库数量、厂家批号和公司批号不能为0或有空值！", "提示");
                   return;
               }

               if (isExistMultiplyPOitem && !cbMultiplyPOItem.Checked)
               {
                   MessageBoxEx.Show("当前存在同一物料同一批号在多个订单中情况，请对此类记录单独选中，并选中界面的复选框后进行提交！", "提示");
                   return;
               }
               string fileNumber = string.Empty;
               if (cbMultiplyPOItem.Checked)
               {
                   if (multiplyPOItemCount != dtTemp.Rows.Count)
                   {
                       MessageBoxEx.Show("当前跨订单物料的记录数量不准确！", "提示");
                       return;
                   }                   
               }

               if (guidList.Count > 0)
                   {
                       for (int i = 0; i < guidList.Count; i++)
                       {
                           string selectExist = @"Select Count(Id) From EBR_ReceiveRecordForInspect Where Guid='" + guidList[i] + "'";
                           if (SQLHelper.Exist(GlobalSpace.EBRConnStr, selectExist))
                           {
                               isExistRecord = true;
                           }
                       }
                   }


               if (!string.IsNullOrEmpty(itemNumber) || isExistDictEmpty)
               {
                   MessageBoxEx.Show("以下物料" + itemNumber + "没有进行品名维护或品名为空，请先进行维护！", "提示");
                   return;
               }

               if(isExistRecord)
               {
                   MessageBoxEx.Show("当前记录已保存，请勿重复提交，从记录管理中打印！", "提示");
                   return;
               }

               DataTable dt = new DataTable();
               dt.Columns.Add("Guid");
               dt.Columns.Add("ApplyDate");
               dt.Columns.Add("ItemNumber");
               dt.Columns.Add("ItemDescription");
               dt.Columns.Add("VendorLotNumber");
               dt.Columns.Add("InternalLotNumber");
               dt.Columns.Add("MfgName");
               dt.Columns.Add("VendorName");
               dt.Columns.Add("PackageQuantity");
               dt.Columns.Add("Receiver");
               dt.Columns.Add("UM");
               dt.Columns.Add("Quantity");
               dt.Columns.Add("PackageOdd");
               dt.Columns.Add("PackageSpecification");
               dt.Columns.Add("MfgDate");
               dt.Columns.Add("ExpiredDate");
               dt.Columns.Add("PackageUM");
               dt.Columns.Add("FONumber");
               dt.Columns.Add("PONumber");
               dt.Columns.Add("LineNumber");

               foreach (DataRow drTemp in dtTemp.Rows)
               {
                   DataRow dr = dt.NewRow();
                   dr["Guid"] = drTemp["Guid"].ToString();
                   dr["ApplyDate"] = DateTime.Now.ToString("yyyy-MM-dd"); 
                   dr["ItemNumber"] = drTemp["物料代码"].ToString() + drTemp["生产商码"].ToString();
                   dr["ItemDescription"] = drTemp["描述"].ToString();
                   dr["PONumber"] = drTemp["采购单号"];
                   dr["LineNumber"] = drTemp["行号"];
                   if (FOItemKeeperList.Contains(StockUser.UserID))
                   {
                       string lotnumber = drTemp["厂家批号"].ToString().Trim();
                       string fonumber = string.Empty;


                       if (drTemp["外贸单号"] == DBNull.Value || drTemp["外贸单号"].ToString() == "")
                       {
                           // MessageBoxEx.Show("联系单号不能为空！", "提示");
                           // return;
                           dr["VendorLotNumber"] = lotnumber;
                           dr["FONumber"] = "";
                       }
                       else
                       {
                           fonumber = drTemp["外贸单号"].ToString().Trim();
                           if (fonumber.Length != 3 && fonumber.Length != 5 && fonumber.Length != 7 && fonumber.Length != 8)
                           {
                               MessageBoxEx.Show("联系单号长度不正确！", "提示");
                               return;
                           }

                           if (fonumber.Length == 3 || fonumber.Length == 5)
                           {
                               dr["VendorLotNumber"] = lotnumber.Replace(fonumber, "");
                               dr["FONumber"] = fonumber;
                           }
                           else if (fonumber.Length == 7)
                           {
                               dr["VendorLotNumber"] = lotnumber.Substring(0, lotnumber.Length - 4);
                               dr["FONumber"] = fonumber;
                           }
                           else if (fonumber.Length == 8)
                           {
                               dr["VendorLotNumber"] = lotnumber.Substring(0, lotnumber.Length - 5); 
                               dr["FONumber"] = fonumber;
                           }
                       }

                       dr["PackageUM"] = "件";
                   }
                   else
                   {
                       dr["VendorLotNumber"] = drTemp["厂家批号"];
                       dr["PackageUM"] = drTemp["件数单位"];
                   }
                   dr["InternalLotNumber"] = drTemp["公司批号"].ToString().ToUpper(); 
                   dr["MfgName"] = drTemp["生产商名"];
                   dr["VendorName"] = drTemp["供应商名"];
                   dr["PackageQuantity"] = drTemp["整件数"];
                   dr["Receiver"] = drTemp["StockKeeper"].ToString().Split('|')[1];
                   dr["UM"] = drTemp["单位"]; 

                   dr["Quantity"] = drTemp["入库数量"];
                   dr["PackageOdd"] = drTemp["零头"];
                   dr["PackageSpecification"] = drTemp["包装规格"];
                   dr["MfgDate"] = drTemp["生产日期"];
                   dr["ExpiredDate"] = drTemp["到期日期"];
                   dt.Rows.Add(dr.ItemArray);
               }

       //    string fileNumber = GetFileNumber(StockUser.Stock);
  //         GridppReport Report = new GridppReport();


           int icount = 0;
           if(cbMultiplyPOItem.Checked)
           {
               fileNumber = GetFileNumber(StockUser.Stock);
               List<string> sqlList = new List<string>();
               foreach (DataRow dr in dt.Rows)
               {                         
                   //记录写入
                   string sqlInsert = @"INSERT INTO EBR_ReceiveRecordForInspect  (
                                                                   [PONumber],
                                                                   [VendorName],
                                                                   [ManufacturerName],
                                                                   [LineNumber],
                                                                   [ItemNumber],
                                                                   [ItemDescription],
                                                                   [LineUM],
                                                                   [ReceiveQuantity],
                                                                   [LotNumber],
                                                                   [InternalLotNumber],
                                                                   [ExpiredDate],
                                                                   [Operator],
                                                                   [ForeignNumber],
                                                                   [Guid],
                                                                   [ManufacturedDate],
                                                                   [FileEdition],
                                                                   [FileNumber],
                                                                   [FileTracedNumber],[PackageQuantity],PackageUM,PackageOdd,PackageSpecification
                                                               )
                                                               VALUES
                                                                   ( '" + dr["PONumber"].ToString() + "','" + dr["VendorName"].ToString() + "','" + dr["MfgName"].ToString() + "','" + dr["LineNumber"].ToString() + "','" + dr["ItemNumber"].ToString() + "','" + dr["ItemDescription"].ToString().Replace("'", "''") + "','" + dr["UM"].ToString() + "','" + Convert.ToDouble(dr["Quantity"]) + "','" + dr["VendorLotNumber"].ToString() + "','" + dr["InternalLotNumber"].ToString() + "','" + dr["ExpiredDate"].ToString() + "','" + StockUser.UserName + "','" + dr["FONumber"].ToString() + "','" + dr["Guid"].ToString() + "','" + dr["MfgDate"].ToString() + "','" + StockUser.FileEdition + "','" + fileNumber + "','" + StockUser.FileTracedNumber + "','" + dr["PackageQuantity"].ToString() + "','" + dr["PackageUM"].ToString() + "'," + Convert.ToDouble(dr["PackageOdd"]) + ",'" + dr["PackageSpecification"].ToString() + "')";
                   sqlList.Add(sqlInsert);
               }                  

               if(SQLHelper.BatchExecuteNonQuery(GlobalSpace.EBRConnStr,sqlList))
               {
                   for(int x = 0;x < sqlList.Count; x++)
                   {
                       icount++;
                   }
               }

           }
           else
           {
               foreach (DataRow dr in dt.Rows)
               {
                   //记录写入
                   string sqlInsert = @"INSERT INTO EBR_ReceiveRecordForInspect  (
                                                                       [PONumber],
                                                                       [VendorName],
                                                                       [ManufacturerName],
                                                                       [LineNumber],
                                                                       [ItemNumber],
                                                                       [ItemDescription],
                                                                       [LineUM],
                                                                       [ReceiveQuantity],
                                                                       [LotNumber],
                                                                       [InternalLotNumber],
                                                                       [ExpiredDate],
                                                                       [Operator],
                                                                       [ForeignNumber],
                                                                       [Guid],
                                                                       [ManufacturedDate],
                                                                       [FileEdition],
                                                                       [FileNumber],
                                                                       [FileTracedNumber],[PackageQuantity],PackageUM,PackageOdd,PackageSpecification
                                                                   )
                                                                   VALUES
                                                                       ( '" + dr["PONumber"].ToString() + "','" + dr["VendorName"].ToString() + "','" + dr["MfgName"].ToString() + "','" + dr["LineNumber"].ToString() + "','" + dr["ItemNumber"].ToString() + "','" + dr["ItemDescription"].ToString().Replace("'", "''") + "','" + dr["UM"].ToString() + "','" + Convert.ToDouble(dr["Quantity"]) + "','" + dr["VendorLotNumber"].ToString() + "','" + dr["InternalLotNumber"].ToString() + "','" + dr["ExpiredDate"].ToString() + "','" + StockUser.UserName + "','" + dr["FONumber"].ToString() + "','" + dr["Guid"].ToString() + "','" + dr["MfgDate"].ToString() + "','" + StockUser.FileEdition + "','" + GetFileNumber(StockUser.Stock) + "','" + StockUser.FileTracedNumber + "','" + dr["PackageQuantity"].ToString() + "','" + dr["PackageUM"].ToString() + "'," + Convert.ToDouble(dr["PackageOdd"]) + ",'" + dr["PackageSpecification"].ToString() + "')";
                   if (SQLHelper.ExecuteNonQuery(GlobalSpace.EBRConnStr, sqlInsert))
                   {
                       icount++;
                   }
               }
           }

         if(dt.Rows.Count != icount)
           {
               MessageBoxEx.Show("保存记录条数与选择条数不一致，请从记录管理中查看和打印！", "提示");
           }
           else
           {
               MessageBoxEx.Show("保存记录成功，请从记录管理中进行打印！", "提示");
           }
           if(cbMultiplyPOItem.Checked)
           {
               cbMultiplyPOItem.Checked = false;
           }
       }*/

        private void btnPrintBatchRecord_Click(object sender, EventArgs e)
        {

            if (dgvPOItemDetailView.Rows.Count == 0)
            {
                MessageBoxEx.Show("当前无可用的记录！", "提示");
                return;
            }

            if (string.IsNullOrWhiteSpace(StockUser.FileTracedNumber))
            {
                MessageBoxEx.Show("当前库管员未查到请验记录版本信息，无法进行提交！", "提示");
                return;
            }

            List<string> guidList = new List<string>();
            Dictionary<string, string> dict = new Dictionary<string, string>();
            string sqlSelectDesc = @"Select ItemNumber,ProductName From PurchaseDepartmentStockProductName ";

            bool isExistRecord = false;

            dict = SQLHelper.GetDataTable(GlobalSpace.FSDBConnstr, sqlSelectDesc).Rows.Cast<DataRow>().ToDictionary(r => r["ItemNumber"].ToString(), r => r["ProductName"].ToString());

            DataTable dtTemp0 = (DataTable)dgvPOItemDetailView.DataSource;
            DataTable dtTemp = dtTemp0.Clone();

            for (int m = 0; m < dgvPOItemDetailView.Rows.Count; m++)
            {
                if (Convert.ToBoolean(dgvPOItemDetailView.Rows[m].Cells["Check"].Value) == true)
                {
                    DataRow dr = dtTemp.NewRow();
                    guidList.Add(dgvPOItemDetailView.Rows[m].Cells["Guid"].Value.ToString());
                    dr = (dgvPOItemDetailView.Rows[m].DataBoundItem as DataRowView).Row;
                    dtTemp.Rows.Add(dr.ItemArray);
                }
            }
            string itemNumber = string.Empty;
            bool isExistMultiplyPOitem = false;
            int multiplyPOItemCount = 0;
            bool isExistEmptyValue = false;
            foreach (DataRow dgvr in dtTemp.Rows)
            {
                if (!dict.ContainsKey(dgvr["物料代码"].ToString()))
                {
                    itemNumber += dgvr["物料代码"].ToString() + "  ";
                }
                else
                {
                    if (!string.IsNullOrWhiteSpace(dict[dgvr["物料代码"].ToString()]))
                    {
                        dgvr["描述"] = dict[dgvr["物料代码"].ToString()];
                    }
                    else
                    {
                        itemNumber += dgvr["物料代码"].ToString() + "  ";
                    }
                }

                DataRow[] drs = dtTemp.Select("物料代码='" + dgvr["物料代码"].ToString() + "' And 厂家批号='" + dgvr["厂家批号"].ToString() + "'");
                multiplyPOItemCount = drs.Length;
                if (multiplyPOItemCount > 1)
                {
                    isExistMultiplyPOitem = true;
                }

                if (dgvr["入库数量"] == DBNull.Value || dgvr["入库数量"].ToString() == "0" || dgvr["厂家批号"] == DBNull.Value || dgvr["厂家批号"].ToString() == "" || dgvr["公司批号"] == DBNull.Value || dgvr["公司批号"].ToString() == "")
                {
                    isExistEmptyValue = true;
                }
            }
            if (isExistEmptyValue)
            {
                MessageBoxEx.Show("入库数量、厂家批号和公司批号不能为0或有空值！", "提示");
                return;
            }

            if (isExistMultiplyPOitem && !cbMultiplyPOItem.Checked)
            {
                MessageBoxEx.Show("当前存在同一物料同一批号在多个订单中情况，请对此类记录单独选中，并选中界面的复选框后进行提交！", "提示");
                return;
            }
            string fileNumber = string.Empty;
            if (cbMultiplyPOItem.Checked)
            {
                if (multiplyPOItemCount != dtTemp.Rows.Count)
                {
                    MessageBoxEx.Show("当前跨订单物料的记录数量不准确！", "提示");
                    return;
                }
            }

            if (guidList.Count > 0)
            {
                for (int i = 0; i < guidList.Count; i++)
                {
                    string selectExist = @"Select Count(Id) From EBR_ReceiveRecordForInspect Where Guid='" + guidList[i] + "'";
                    if (SQLHelper.Exist(GlobalSpace.EBRConnStr, selectExist))
                    {
                        isExistRecord = true;
                    }
                }
            }


            if (!string.IsNullOrEmpty(itemNumber))
            {
                MessageBoxEx.Show("以下物料" + itemNumber + "没有进行品名维护或品名为空，请先进行维护！", "提示");
                return;
            }

            if (isExistRecord)
            {
                MessageBoxEx.Show("当前记录已保存，请勿重复提交，从记录管理中打印！", "提示");
                return;
            }

            DataTable dt = new DataTable();
            dt.Columns.Add("Guid");
            dt.Columns.Add("ApplyDate");
            dt.Columns.Add("ItemNumber");
            dt.Columns.Add("ItemDescription");
            dt.Columns.Add("VendorLotNumber");
            dt.Columns.Add("InternalLotNumber");
            dt.Columns.Add("MfgName");
            dt.Columns.Add("VendorName");
            dt.Columns.Add("PackageQuantity");
            dt.Columns.Add("Receiver");
            dt.Columns.Add("UM");
            dt.Columns.Add("Quantity");
            dt.Columns.Add("PackageOdd");
            dt.Columns.Add("PackageSpecification");
            dt.Columns.Add("MfgDate");
            dt.Columns.Add("ExpiredDate");
            dt.Columns.Add("PackageUM");
            dt.Columns.Add("FONumber");
            dt.Columns.Add("PONumber");
            dt.Columns.Add("LineNumber");

            dt.Columns.Add("QualityCheckStandard");
            //Checker as 复核人,Receiver as 接收请验人,Conclusion as 结论,ConclusionText as 结论其他内容,IsAnyDeviation as 物料验收过程是否出现偏差,DeviationNumber as 偏差编号,deviationIsClosed as 偏差是否已处理关闭,IsReport as 问题是否已报告,QualityManageIdea as 质量管理部门意见,Sign as 签名,SignDate as 签名日期,IsRequireClean as 是否需要清洁,PollutionSituation as 污染情况,CleanMethod as 清洁方式,IsComplete as 外包装是否完整,
            //DamageSituation as 损坏情况,CauseInvestigation1 as 原因调查1,IsSealed as 外包装是否密封,UnsealedCondition as 不密封情况,CauseInvestigation2 as 原因调查2,IsAnyMaterialWithPollutionRisk as 运输工具内是否存在造成污染交叉污染的物料,IsAnyProblemAffectedMaterialQuality as 是否有其他可能影响物料质量的问题,Question as 问题,CauseInvestigation3 as 原因调查3,LotNumberType as 批号类型,IsApprovedVendor as 是否为质量管理部门批准的供应商,StorageCondition as 规定贮存条件,TransportTemperature as 运输条件检查结果,TransportCondition as 运输条件是否符合,TransportationControlRecord as 是否有运输条件控制记录,Shape as 形状是否一致,Colour as 颜色是否一致,Font as 字体是否一致,RoughWeight as 有无毛重,NetWeight as 有无净重,ApprovalNumber as 有无批准文号,ReportType as 报告类型,Report as 有无报告
            dt.Columns.Add("Checker");
            dt.Columns.Add("ReceiverAPP");
            dt.Columns.Add("Conclusion");
            dt.Columns.Add("ConclusionText");
            dt.Columns.Add("IsAnyDeviation");
            dt.Columns.Add("DeviationNumber");
            dt.Columns.Add("deviationIsClosed");
            dt.Columns.Add("IsReport");
            dt.Columns.Add("QualityManageIdea");
            dt.Columns.Add("Sign");
            dt.Columns.Add("SignDate");
            dt.Columns.Add("IsRequireClean");
            dt.Columns.Add("PollutionSituation");
            dt.Columns.Add("CleanMethod");
            dt.Columns.Add("IsComplete");
            dt.Columns.Add("DamageSituation");
            dt.Columns.Add("CauseInvestigation1");
            dt.Columns.Add("IsSealed");
            dt.Columns.Add("UnsealedCondition");
            dt.Columns.Add("CauseInvestigation2");
            dt.Columns.Add("IsAnyMaterialWithPollutionRisk");
            dt.Columns.Add("IsAnyProblemAffectedMaterialQuality");
            dt.Columns.Add("Question");
            dt.Columns.Add("CauseInvestigation3");
            dt.Columns.Add("LotNumberType");
            dt.Columns.Add("IsApprovedVendor");
            dt.Columns.Add("StorageCondition");
            dt.Columns.Add("TransportTemperature");
            dt.Columns.Add("TransportCondition");
            dt.Columns.Add("TransportationControlRecord");
            dt.Columns.Add("Shape");
            dt.Columns.Add("Colour");
            dt.Columns.Add("Font");
            dt.Columns.Add("RoughWeight");
            dt.Columns.Add("NetWeight");
            dt.Columns.Add("ApprovalNumber");
            dt.Columns.Add("ReportType");
            dt.Columns.Add("Report");
            foreach (DataRow drTemp in dtTemp.Rows)
            {
                DataRow dr = dt.NewRow();
                dr["Guid"] = drTemp["Guid"].ToString();
                dr["ApplyDate"] = DateTime.Now.ToString("yyyy-MM-dd");
                dr["ItemNumber"] = drTemp["物料代码"].ToString() + drTemp["生产商码"].ToString();
                dr["ItemDescription"] = drTemp["描述"].ToString();
                dr["PONumber"] = drTemp["采购单号"];
                dr["LineNumber"] = drTemp["行号"];
                if (FOItemKeeperList.Contains(StockUser.UserID))
                {
                    string lotnumber = drTemp["厂家批号"].ToString().Trim();
                    string fonumber = string.Empty;


                    if (drTemp["外贸单号"] == DBNull.Value || drTemp["外贸单号"].ToString() == "")
                    {
                        // MessageBoxEx.Show("联系单号不能为空！", "提示");
                        // return;
                        dr["VendorLotNumber"] = lotnumber;
                        dr["FONumber"] = "";
                    }
                    else
                    {
                        fonumber = drTemp["外贸单号"].ToString().Trim();
                        if (fonumber.Length != 3 && fonumber.Length != 5 && fonumber.Length != 7 && fonumber.Length != 8)
                        {
                            MessageBoxEx.Show("联系单号长度不正确！", "提示");
                            return;
                        }

                        if (fonumber.Length == 3 || fonumber.Length == 5)
                        {
                            dr["VendorLotNumber"] = lotnumber.Replace(fonumber, "");
                            dr["FONumber"] = fonumber;
                        }
                        else if (fonumber.Length == 7)
                        {
                            dr["VendorLotNumber"] = lotnumber.Substring(0, lotnumber.Length - 4);
                            dr["FONumber"] = fonumber;
                        }
                        else if (fonumber.Length == 8)
                        {
                            dr["VendorLotNumber"] = lotnumber.Substring(0, lotnumber.Length - 5);
                            dr["FONumber"] = fonumber;
                        }
                    }

                    dr["PackageUM"] = "件";
                }
                else
                {
                    dr["VendorLotNumber"] = drTemp["厂家批号"];
                    dr["PackageUM"] = drTemp["件数单位"];
                }
                dr["InternalLotNumber"] = drTemp["公司批号"].ToString().ToUpper();
                dr["MfgName"] = drTemp["生产商名"];
                dr["VendorName"] = drTemp["供应商名"];
                dr["PackageQuantity"] = drTemp["整件数"];
                dr["Receiver"] = drTemp["StockKeeper"].ToString().Split('|')[1];
                dr["UM"] = drTemp["单位"];

                dr["Quantity"] = drTemp["入库数量"];
                dr["PackageOdd"] = drTemp["零头"];
                dr["PackageSpecification"] = drTemp["包装规格"];
                dr["MfgDate"] = drTemp["生产日期"];
                dr["ExpiredDate"] = drTemp["到期日期"];
                //新增字段

                dr["QualityCheckStandard"] = drTemp["检验标准"];
                dr["Checker"] = drTemp["复核人"];
                dr["ReceiverAPP"] = drTemp["接收请验人"];
                dr["Conclusion"] = drTemp["结论"];
                dr["ConclusionText"] = drTemp["结论其他内容"];
                dr["IsAnyDeviation"] = drTemp["物料验收过程是否出现偏差"];
                dr["DeviationNumber"] = drTemp["偏差编号"];
                dr["deviationIsClosed"] = drTemp["偏差是否已处理关闭"];
                dr["IsReport"] = drTemp["问题是否已报告"];
                dr["QualityManageIdea"] = drTemp["质量管理部门意见"];
                dr["Sign"] = drTemp["签名"];
                dr["SignDate"] = drTemp["签名日期"];
                dr["IsRequireClean"] = drTemp["是否需要清洁"];
                dr["PollutionSituation"] = drTemp["污染情况"];
                dr["CleanMethod"] = drTemp["清洁方式"];
                dr["IsComplete"] = drTemp["外包装是否完整"];
                dr["DamageSituation"] = drTemp["损坏情况"];
                dr["CauseInvestigation1"] = drTemp["原因调查1"];
                dr["IsSealed"] = drTemp["外包装是否密封"];
                dr["UnsealedCondition"] = drTemp["不密封情况"];
                dr["CauseInvestigation2"] = drTemp["原因调查2"];
                dr["IsAnyMaterialWithPollutionRisk"] = drTemp["运输工具内是否存在造成污染交叉污染的物料"];
                dr["IsAnyProblemAffectedMaterialQuality"] = drTemp["是否有其他可能影响物料质量的问题"];
                dr["Question"] = drTemp["问题"];
                dr["CauseInvestigation3"] = drTemp["原因调查3"];
                dr["LotNumberType"] = drTemp["批号类型"];
                dr["IsApprovedVendor"] = drTemp["是否为质量管理部门批准的供应商"];
                dr["StorageCondition"] = drTemp["规定贮存条件"];
                dr["TransportTemperature"] = drTemp["运输条件检查结果"];
                dr["TransportCondition"] = drTemp["运输条件是否符合"];
                dr["TransportationControlRecord"] = drTemp["是否有运输条件控制记录"];
                dr["Shape"] = drTemp["形状是否一致"];
                dr["Colour"] = drTemp["颜色是否一致"];
                dr["Font"] = drTemp["字体是否一致"];
                dr["RoughWeight"] = drTemp["有无毛重"];
                dr["NetWeight"] = drTemp["有无净重"];
                dr["ApprovalNumber"] = drTemp["有无批准文号"];
                dr["ReportType"] = drTemp["报告类型"];
                dr["Report"] = drTemp["有无报告"];
                dt.Rows.Add(dr.ItemArray);
            }

            //    string fileNumber = GetFileNumber(StockUser.Stock);
            //         GridppReport Report = new GridppReport();


            int icount = 0;
            if (cbMultiplyPOItem.Checked)
            {
                fileNumber = GetFileNumber(StockUser.Stock);
                List<string> sqlList = new List<string>();
                foreach (DataRow dr in dt.Rows)
                {
                    //记录写入
                    string sqlInsert = @"INSERT INTO EBR_ReceiveRecordForInspect  (
	                                                                [PONumber],
	                                                                [VendorName],
	                                                                [ManufacturerName],
	                                                                [LineNumber],
	                                                                [ItemNumber],
	                                                                [ItemDescription],
	                                                                [LineUM],
	                                                                [ReceiveQuantity],
	                                                                [LotNumber],
	                                                                [InternalLotNumber],
	                                                                [ExpiredDate],
	                                                                [Operator],
	                                                                [ForeignNumber],
	                                                                [Guid],
	                                                                [ManufacturedDate],
	                                                                [FileEdition],
	                                                                [FileNumber],
	                                                                [FileTracedNumber],EffectiveDate,[PackageQuantity],PackageUM,PackageOdd,PackageSpecification,QualityCheckStandard,                  Checker,Conclusion,ConclusionText,IsAnyDeviation,DeviationNumber,deviationIsClosed,IsReport,QualityManageIdea,Sign,SignDate,IsRequireClean,PollutionSituation,CleanMethod,IsComplete,DamageSituation,CauseInvestigation1,IsSealed,UnsealedCondition,CauseInvestigation2,IsAnyMaterialWithPollutionRisk,IsAnyProblemAffectedMaterialQuality,Question,CauseInvestigation3,LotNumberType,IsApprovedVendor,StorageCondition,TransportTemperature,TransportCondition,TransportationControlRecord,Shape,Colour,Font,RoughWeight,NetWeight,ApprovalNumber,ReportType,Report
                                                                )
                                                                VALUES
	                                                                ( '" + dr["PONumber"].ToString() + "','" + dr["VendorName"].ToString() + "','" + dr["MfgName"].ToString() + "','" + dr["LineNumber"].ToString() + "','" + dr["ItemNumber"].ToString() + "','" + dr["ItemDescription"].ToString().Replace("'", "''") + "','" + dr["UM"].ToString() + "','" + Convert.ToDouble(dr["Quantity"]) + "','" + dr["VendorLotNumber"].ToString() + "','" + dr["InternalLotNumber"].ToString() + "','" + dr["ExpiredDate"].ToString() + "','" + StockUser.UserID+"|"+StockUser.UserName + "','" + dr["FONumber"].ToString() + "','" + dr["Guid"].ToString() + "','" + dr["MfgDate"].ToString() + "','" + StockUser.FileEdition + "','" + fileNumber + "','" + StockUser.FileTracedNumber + "','" + StockUser.EffectiveDate + "','" + dr["PackageQuantity"].ToString() + "','" + dr["PackageUM"].ToString() + "'," + Convert.ToDouble(dr["PackageOdd"]) + ",'" + dr["PackageSpecification"].ToString() + "','" + (dr["QualityCheckStandard"] == DBNull.Value ? "" : dr["QualityCheckStandard"].ToString().Trim()) + "','" + (dr["Checker"] == DBNull.Value ? "" : dr["Checker"].ToString().Trim()) + "','" + (dr["Conclusion"] == DBNull.Value ? "" : dr["Conclusion"].ToString().Trim()) + "','" + (dr["ConclusionText"] == DBNull.Value ? "" : dr["ConclusionText"].ToString().Trim()) + "','" + (dr["IsAnyDeviation"] == DBNull.Value ? "" : dr["IsAnyDeviation"].ToString().Trim()) + "','" + (dr["DeviationNumber"] == DBNull.Value ? "" : dr["DeviationNumber"].ToString().Trim()) + "','" + (dr["deviationIsClosed"] == DBNull.Value ? "" : dr["deviationIsClosed"].ToString().Trim()) + "','" + (dr["IsReport"] == DBNull.Value ? "" : dr["IsReport"].ToString().Trim()) + "','" + (dr["QualityManageIdea"] == DBNull.Value ? "" : dr["QualityManageIdea"].ToString().Trim()) + "','" + (dr["Sign"] == DBNull.Value ? "" : dr["Sign"].ToString().Trim()) + "','" + (dr["SignDate"] == DBNull.Value ? "" : dr["SignDate"].ToString().Trim()) + "','" + (dr["IsRequireClean"] == DBNull.Value ? "" : dr["IsRequireClean"].ToString().Trim()) + "','" + (dr["PollutionSituation"] == DBNull.Value ? "" : dr["PollutionSituation"].ToString().Trim()) + "','" + (dr["CleanMethod"] == DBNull.Value ? "" : dr["CleanMethod"].ToString().Trim()) + "','" + (dr["IsComplete"] == DBNull.Value ? "" : dr["IsComplete"].ToString().Trim()) + "','" + (dr["DamageSituation"] == DBNull.Value ? "" : dr["DamageSituation"].ToString().Trim()) + "','" + (dr["CauseInvestigation1"] == DBNull.Value ? "" : dr["CauseInvestigation1"].ToString().Trim()) + "','" + (dr["IsSealed"] == DBNull.Value ? "" : dr["IsSealed"].ToString().Trim()) + "','" + (dr["UnsealedCondition"] == DBNull.Value ? "" : dr["UnsealedCondition"].ToString().Trim()) + "','" + (dr["CauseInvestigation2"] == DBNull.Value ? "" : dr["CauseInvestigation2"].ToString().Trim()) + "','" + (dr["IsAnyMaterialWithPollutionRisk"] == DBNull.Value ? "" : dr["IsAnyMaterialWithPollutionRisk"].ToString().Trim()) + "','" + (dr["IsAnyProblemAffectedMaterialQuality"] == DBNull.Value ? "" : dr["IsAnyProblemAffectedMaterialQuality"].ToString().Trim()) + "','" + (dr["Question"] == DBNull.Value ? "" : dr["Question"].ToString().Trim()) + "','" + (dr["CauseInvestigation3"] == DBNull.Value ? "" : dr["CauseInvestigation3"].ToString().Trim()) + "','" + (dr["LotNumberType"] == DBNull.Value ? "" : dr["LotNumberType"].ToString().Trim()) + "','" + (dr["IsApprovedVendor"] == DBNull.Value ? "" : dr["IsApprovedVendor"].ToString().Trim()) + "','" + (dr["StorageCondition"] == DBNull.Value ? "" : dr["StorageCondition"].ToString().Trim()) + "','" + (dr["TransportTemperature"] == DBNull.Value ? "" : dr["TransportTemperature"].ToString().Trim()) + "','" + (dr["TransportCondition"] == DBNull.Value ? "" : dr["TransportCondition"].ToString().Trim()) + "','" + (dr["TransportationControlRecord"] == DBNull.Value ? "" : dr["TransportationControlRecord"].ToString().Trim()) + "','" + (dr["Shape"] == DBNull.Value ? "" : dr["Shape"].ToString().Trim()) + "','" + (dr["Colour"] == DBNull.Value ? "" : dr["Colour"].ToString().Trim()) + "','" + (dr["Font"] == DBNull.Value ? "" : dr["Font"].ToString().Trim()) + "','" + (dr["RoughWeight"] == DBNull.Value ? "" : dr["RoughWeight"].ToString().Trim()) + "','" + (dr["NetWeight"] == DBNull.Value ? "" : dr["NetWeight"].ToString().Trim()) + "','" + (dr["ApprovalNumber"] == DBNull.Value ? "" : dr["ApprovalNumber"].ToString().Trim()) + "','" + (dr["ReportType"] == DBNull.Value ? "" : dr["ReportType"].ToString().Trim()) + "','" + (dr["Report"] == DBNull.Value ? "" : dr["Report"].ToString().Trim()) + "')";
                    sqlList.Add(sqlInsert);
                }

                if (SQLHelper.BatchExecuteNonQuery(GlobalSpace.EBRConnStr, sqlList))
                {
                    for (int x = 0; x < sqlList.Count; x++)
                    {
                        icount++;
                    }
                }

            }
            else
            {
                foreach (DataRow dr in dt.Rows)
                {
                    //记录写入
                    string sqlInsert = @"INSERT INTO EBR_ReceiveRecordForInspect  (
	                                                                    [PONumber],
	                                                                    [VendorName],
	                                                                    [ManufacturerName],
	                                                                    [LineNumber],
	                                                                    [ItemNumber],
	                                                                    [ItemDescription],
	                                                                    [LineUM],
	                                                                    [ReceiveQuantity],
	                                                                    [LotNumber],
	                                                                    [InternalLotNumber],
	                                                                    [ExpiredDate],
	                                                                    [Operator],
	                                                                    [ForeignNumber],
	                                                                    [Guid],
	                                                                    [ManufacturedDate],
	                                                                    [FileEdition],
	                                                                    [FileNumber],
	                                                                    [FileTracedNumber],EffectiveDate,[PackageQuantity],PackageUM,PackageOdd,PackageSpecification,QualityCheckStandard,                  Checker,Conclusion,ConclusionText,IsAnyDeviation,DeviationNumber,deviationIsClosed,IsReport,QualityManageIdea,Sign,SignDate,IsRequireClean,PollutionSituation,CleanMethod,IsComplete,DamageSituation,CauseInvestigation1,IsSealed,UnsealedCondition,CauseInvestigation2,IsAnyMaterialWithPollutionRisk,IsAnyProblemAffectedMaterialQuality,Question,CauseInvestigation3,LotNumberType,IsApprovedVendor,StorageCondition,TransportTemperature,TransportCondition,TransportationControlRecord,Shape,Colour,Font,RoughWeight,NetWeight,ApprovalNumber,ReportType,Report
                                                                    )
                                                                    VALUES
	                                                                    ( '" + dr["PONumber"].ToString() + "','" + dr["VendorName"].ToString() + "','" + dr["MfgName"].ToString() + "','" + dr["LineNumber"].ToString() + "','" + dr["ItemNumber"].ToString() + "','" + dr["ItemDescription"].ToString().Replace("'", "''") + "','" + dr["UM"].ToString() + "','" + Convert.ToDouble(dr["Quantity"]) + "','" + dr["VendorLotNumber"].ToString() + "','" + dr["InternalLotNumber"].ToString() + "','" + dr["ExpiredDate"].ToString() + "','" + StockUser.UserID + "|" + StockUser.UserName + "','" + dr["FONumber"].ToString() + "','" + dr["Guid"].ToString() + "','" + dr["MfgDate"].ToString() + "','" + StockUser.FileEdition + "','" + GetFileNumber(StockUser.Stock) + "','" + StockUser.FileTracedNumber + "','" + StockUser.EffectiveDate + "','" + dr["PackageQuantity"].ToString() + "','" + dr["PackageUM"].ToString() + "'," + Convert.ToDouble(dr["PackageOdd"]) + ",'" + dr["PackageSpecification"].ToString() + "','" + (dr["QualityCheckStandard"] == DBNull.Value ? "" : dr["QualityCheckStandard"].ToString().Trim()) + "','" + (dr["Checker"] == DBNull.Value ? "" : dr["Checker"].ToString().Trim()) + "','" + (dr["Conclusion"] == DBNull.Value ? "" : dr["Conclusion"].ToString().Trim()) + "','" + (dr["ConclusionText"] == DBNull.Value ? "" : dr["ConclusionText"].ToString().Trim()) + "','" + (dr["IsAnyDeviation"] == DBNull.Value ? "" : dr["IsAnyDeviation"].ToString().Trim()) + "','" + (dr["DeviationNumber"] == DBNull.Value ? "" : dr["DeviationNumber"].ToString().Trim()) + "','" + (dr["deviationIsClosed"] == DBNull.Value ? "" : dr["deviationIsClosed"].ToString().Trim()) + "','" + (dr["IsReport"] == DBNull.Value ? "" : dr["IsReport"].ToString().Trim()) + "','" + (dr["QualityManageIdea"] == DBNull.Value ? "" : dr["QualityManageIdea"].ToString().Trim()) + "','" + (dr["Sign"] == DBNull.Value ? "" : dr["Sign"].ToString().Trim()) + "','" + (dr["SignDate"] == DBNull.Value ? "" : dr["SignDate"].ToString().Trim()) + "','" + (dr["IsRequireClean"] == DBNull.Value ? "" : dr["IsRequireClean"].ToString().Trim()) + "','" + (dr["PollutionSituation"] == DBNull.Value ? "" : dr["PollutionSituation"].ToString().Trim()) + "','" + (dr["CleanMethod"] == DBNull.Value ? "" : dr["CleanMethod"].ToString().Trim()) + "','" + (dr["IsComplete"] == DBNull.Value ? "" : dr["IsComplete"].ToString().Trim()) + "','" + (dr["DamageSituation"] == DBNull.Value ? "" : dr["DamageSituation"].ToString().Trim()) + "','" + (dr["CauseInvestigation1"] == DBNull.Value ? "" : dr["CauseInvestigation1"].ToString().Trim()) + "','" + (dr["IsSealed"] == DBNull.Value ? "" : dr["IsSealed"].ToString().Trim()) + "','" + (dr["UnsealedCondition"] == DBNull.Value ? "" : dr["UnsealedCondition"].ToString().Trim()) + "','" + (dr["CauseInvestigation2"] == DBNull.Value ? "" : dr["CauseInvestigation2"].ToString().Trim()) + "','" + (dr["IsAnyMaterialWithPollutionRisk"] == DBNull.Value ? "" : dr["IsAnyMaterialWithPollutionRisk"].ToString().Trim()) + "','" + (dr["IsAnyProblemAffectedMaterialQuality"] == DBNull.Value ? "" : dr["IsAnyProblemAffectedMaterialQuality"].ToString().Trim()) + "','" + (dr["Question"] == DBNull.Value ? "" : dr["Question"].ToString().Trim()) + "','" + (dr["CauseInvestigation3"] == DBNull.Value ? "" : dr["CauseInvestigation3"].ToString().Trim()) + "','" + (dr["LotNumberType"] == DBNull.Value ? "" : dr["LotNumberType"].ToString().Trim()) + "','" + (dr["IsApprovedVendor"] == DBNull.Value ? "" : dr["IsApprovedVendor"].ToString().Trim()) + "','" + (dr["StorageCondition"] == DBNull.Value ? "" : dr["StorageCondition"].ToString().Trim()) + "','" + (dr["TransportTemperature"] == DBNull.Value ? "" : dr["TransportTemperature"].ToString().Trim()) + "','" + (dr["TransportCondition"] == DBNull.Value ? "" : dr["TransportCondition"].ToString().Trim()) + "','" + (dr["TransportationControlRecord"] == DBNull.Value ? "" : dr["TransportationControlRecord"].ToString().Trim()) + "','" + (dr["Shape"] == DBNull.Value ? "" : dr["Shape"].ToString().Trim()) + "','" + (dr["Colour"] == DBNull.Value ? "" : dr["Colour"].ToString().Trim()) + "','" + (dr["Font"] == DBNull.Value ? "" : dr["Font"].ToString().Trim()) + "','" + (dr["RoughWeight"] == DBNull.Value ? "" : dr["RoughWeight"].ToString().Trim()) + "','" + (dr["NetWeight"] == DBNull.Value ? "" : dr["NetWeight"].ToString().Trim()) + "','" + (dr["ApprovalNumber"] == DBNull.Value ? "" : dr["ApprovalNumber"].ToString().Trim()) + "','" + (dr["ReportType"] == DBNull.Value ? "" : dr["ReportType"].ToString().Trim()) + "','" + (dr["Report"] == DBNull.Value ? "" : dr["Report"].ToString().Trim()) + "')";
                    if (SQLHelper.ExecuteNonQuery(GlobalSpace.EBRConnStr, sqlInsert))
                    {
                        icount++;
                    }
                }
            }

            if (dt.Rows.Count != icount)
            {
                MessageBoxEx.Show("保存记录条数与选择条数不一致，请从记录管理中查看和打印！", "提示");
            }
            else
            {
                MessageBoxEx.Show("保存记录成功，请从记录管理中进行打印！", "提示");
            }
            if (cbMultiplyPOItem.Checked)
            {
                cbMultiplyPOItem.Checked = false;
            }
        }
        private void superTabControlPanel2_Click(object sender, EventArgs e)
            {

            }

            private void btnPrintForLabel_Click(object sender, EventArgs e)
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("代码");
                dt.Columns.Add("描述");
                dt.Columns.Add("厂家批号");
                dt.Columns.Add("公司批号");
                dt.Columns.Add("件数");
                
                bool isChecked = false;

                bool isExistEmptyValue = false;
                Dictionary<string, string> dict = new Dictionary<string, string>();
                string sqlSelectDesc = @"Select ItemNumber,ProductName From PurchaseDepartmentStockProductName ";


                dict = SQLHelper.GetDataTable(GlobalSpace.FSDBConnstr, sqlSelectDesc).Rows.Cast<DataRow>().ToDictionary(r => r["ItemNumber"].ToString(), r => r["ProductName"].ToString());

                foreach (DataGridViewRow dgvr in dgvPOItemDetailView.Rows)
                {
                    if (Convert.ToBoolean(dgvr.Cells["Check"].Value))
                    {
                        isChecked = true;
                        DataRow dr = dt.NewRow();
                        dr["代码"] = dgvr.Cells["物料代码"].Value.ToString() + dgvr.Cells["生产商码"].Value.ToString();                      
                        dr["厂家批号"] = dgvr.Cells["厂家批号"].Value.ToString();
                        dr["公司批号"] = dgvr.Cells["公司批号"].Value.ToString();
                        if (Convert.ToInt32(dgvr.Cells["零头"].Value) == 0)
                        {
                            dr["件数"] = dgvr.Cells["整件数"].Value.ToString();
                        }
                        else
                        {
                            dr["件数"] = (Convert.ToInt32(dgvr.Cells["整件数"].Value) + 1).ToString();
                        }
                        
                        if(!dict.ContainsKey(dgvr.Cells["物料代码"].Value.ToString()))
                        {
                            isExistEmptyValue = true;
                        }
                        else
                        {
                            if(string.IsNullOrWhiteSpace(dict[dgvr.Cells["物料代码"].Value.ToString()]))
                            {
                                 isExistEmptyValue = true;
                            }
                            else
                            {
                                dr["描述"] = dict[dgvr.Cells["物料代码"].Value.ToString()];
                            }
                        }
                    dt.Rows.Add(dr.ItemArray);
                }
                }


            if (isExistEmptyValue)
            {
                MessageBoxEx.Show("物料没有进行品名维护或品名为空，请先进行维护！", "提示");
                return;
            }


            if (!isChecked)
                {
                    MessageBoxEx.Show("没有选中的行！", "提示");
                    return;
                }
                //         MessageBox.Show("111");
                PrintPO pp = new PrintPO(dt, "\\PrintPORecordForLabel.grf");
                pp.Show();
            }

            private void buttonX4_Click(object sender, EventArgs e)
            {
                string sqlSelect = @"SELECT
	                                            Guid,
	                                            PONumber AS 采购单号,
	                                            VendorNumber AS 供应商码,
	                                            VendorName AS 供应商名,
	                                            ManufacturerNumber AS 生产商码,
	                                            ManufacturerName AS 生产商名,
	                                            LineNumber AS 行号,
	                                            ItemNumber AS 物料代码,
	                                            ItemDescription AS 描述,
	                                            LineUM AS 单位,
	                                            DemandDeliveryDate AS 承诺交货日,
	                                            ReceiveQuantity AS 入库数量,
	                                            Stock AS 库,
	                                            Bin AS 位,
	                                            InspectionPeriod AS 检验,
	                                            LotNumber AS 厂家批号,
	                                            InternalLotNumber AS 公司批号,
	                                            ExpiredDate AS 到期日期,
	                                            RetestDate AS 重测日期,
	                                            Operator AS 库管员,
	                                            ReceiveDate AS 入库日期,Status,LotNumberAssign,ItemReceiveType
                                            FROM
	                                            dbo.PurchaseOrderRecordHistoryByCMF  Where Status=2 and  FSOperateDate = '2020-12-02'  AND ItemReceiveType = 'P'";
                dgvPOItemDetailView.DataSource = SQLHelper.GetDataTable(GlobalSpace.FSDBConnstr, sqlSelect);
            }

            private void buttonX6_Click(object sender, EventArgs e)
            {

                DataTable dt = (DataTable)dgvPOItemDetailView.DataSource;

                DataTable dtTemp = new DataTable();

                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    dtTemp.Columns.Add(dt.Columns[i].ColumnName);
                }


                foreach (DataGridViewRow dgvr in dgvPOItemDetailView.Rows)
                {
                    if (Convert.ToBoolean(dgvr.Cells["Check"].Value))
                    {
                        dtTemp.Rows.Add((dgvr.DataBoundItem as DataRowView).Row.ItemArray);
                    }
                }

                if (dtTemp.Rows.Count == 0)
                {
                    MessageBoxEx.Show("当前未选择写入行！", "提示");
                    return;
                }

                FSFunctionLib.FSConfigFileInitialize(GlobalSpace.fsconfigfilepath, FSUserID, FSUserPassword);
                List<string> errorList = new List<string>();

                foreach (DataRow dr in dtTemp.Rows)
                {
                    string strReturn = string.Empty;
                    if (PORV(dr, out strReturn))
                    {
                        //更新订单中物料状态为已入库
                        string guid = dr["Guid"].ToString();
                        string sqlUpdate = @"Update PurchaseOrderRecordHistoryByCMF Set Status = 2,FSOperateDateTime = '" + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss") + "',FSOperateDate='" + DateTime.Now.ToString("yyyy-MM-dd") + "' Where Guid='" + guid + "'";
                        try
                        {
                            if (!SQLHelper.ExecuteNonQuery(GlobalSpace.FSDBConnstr, sqlUpdate))
                            {
                                dgvPODetailFS.DataSource = GetReceivedRecordByStatus(1);
                                dgvPODetailFS.Columns["Guid"].Visible = false;
                                dgvPODetailFS.Columns["Status"].Visible = false;
                            }
                            else
                            {
                                dgvPODetailFS.DataSource = GetReceivedRecordByStatus(1);
                                dgvPODetailFS.Columns["Guid"].Visible = false;
                                dgvPODetailFS.Columns["Status"].Visible = false;
                            }
                        }
                        catch (Exception ex)
                        {
                            Custom.MsgEx(ex.Message);
                        }
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(strReturn))
                        {
                            errorList.Add(strReturn);
                        }
                    }
                }

                FSFunctionLib.FSExit();
                if (errorList.Count > 0)
                {
                    Custom.MsgEx("四班写入出现报错，请查看报错内容！");
                }
                else
                {
                    Custom.MsgEx("四班写入成功！");
                }
            }

            private void buttonX7_Click(object sender, EventArgs e)
            {
                string sqlSelect = @"SELECT
                                                Guid,
	                                            PONumber AS 采购单号,
	                                            VendorNumber AS 供应商码,
	                                            VendorName AS 供应商名,
	                                            ManufacturerNumber AS 生产商码,
	                                            ManufacturerName AS 生产商名,
	                                            LineNumber AS 行号,
	                                            ItemNumber AS 物料代码,
	                                            ItemDescription AS 描述,
	                                            LineUM AS 单位,
	                                            DemandDeliveryDate AS 承诺交货日,
	                                            ReceiveQuantity AS 入库数量,
	                                            Stock AS 库,
	                                            Bin AS 位,
	                                            InspectionPeriod AS 检验,
	                                            LotNumber AS 厂家批号,
	                                            InternalLotNumber AS 公司批号,
	                                            ExpiredDate AS 到期日期,
	                                            RetestDate AS 重测日期,
	                                            Operator AS 库管员,
	                                            ReceiveDate AS 入库日期,Status
                                            FROM
                                                dbo.PurchaseOrderRecordHistoryByCMF Where FSOperateDate = '2020-12-02' And ItemReceiveType = 'P'";
                dgvPOItemDetailView.DataSource = SQLHelper.GetDataTable(GlobalSpace.FSDBConnstr, sqlSelect);
            }

            private void buttonX5_Click(object sender, EventArgs e)
            {
                FSFunctionLib.FSConfigFileInitialize(GlobalSpace.fsconfigfilepath, FSUserID, FSUserPassword);

                foreach (DataGridViewRow dgvr in dgvPOItemDetailView.Rows)
                {
                    if (Convert.ToBoolean(dgvr.Cells["Check"].Value) == true)
                    {
                        IMTR01 imtr = new IMTR01();
                        imtr.ItemNumber.Value = dgvr.Cells["物料代码"].Value.ToString();
                        imtr.InventoryQuantity.Value = dgvr.Cells["入库数量"].Value.ToString();
                        imtr.StockroomFrom.Value = dgvr.Cells["库"].Value.ToString();
                        imtr.BinFrom.Value = dgvr.Cells["位"].Value.ToString();
                        imtr.InventoryCategoryFrom.Value = "I";
                        imtr.LotNumberFrom.Value = dgvr.Cells["厂家批号"].Value.ToString();

                        imtr.StockroomTo.Value = dgvr.Cells["库"].Value.ToString();
                        imtr.BinTo.Value = dgvr.Cells["位"].Value.ToString();
                        imtr.InventoryCategoryTo.Value = "O";
                        imtr.LotNumberTo.Value = dgvr.Cells["厂家批号"].Value.ToString();
                        imtr.LotIdentifier.Value = "E";


                        if (!FSFunctionLib.fstiClient.ProcessId(imtr, null))
                        {
                            FSTIError error = FSFunctionLib.fstiClient.TransactionError;
                            DumpErrorObject(imtr, error, listResult);
                            CommonOperate.WriteFSErrorLog("PORV", imtr, error, FSUserID);
                        }
                    }
                }

                FSFunctionLib.FSExit();
            }

        private void btnImtrSelf_Click(object sender, EventArgs e)
        {
            ItemIMTR ii = new ItemIMTR();
            ii.ShowDialog();
        }

        private void btnIMTRByOrder_Click(object sender, EventArgs e)
        {
            ItemIMTRByOrder ii = new ItemIMTRByOrder(StockUser.UserID, StockUser.Password);
            ii.ShowDialog();
        }

        private void btnManageRecord_Click(object sender, EventArgs e)
        {
            ManageBatchRecord mbr = new ManageBatchRecord(StockUser.UserID,StockUser.UserName);
            mbr.ShowDialog();
        }

        private void btnManageProductName_Click(object sender, EventArgs e)
        {
            ManageProductName mpn = new ManageProductName();
            mpn.ShowDialog();
        }

        private void superTabControlPanel8_Click(object sender, EventArgs e)
        {

        }

        private void tbItemDesc_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar == (char)13)
            {
                if (!string.IsNullOrEmpty(tbItemDesc.Text))
                {
                    IsNeedToSubmit = false;
                    dgvPOItemDetailView.DataSource = GetReceivedRecordByPONumber(tbItemDesc.Text);
                    dgvPOItemDetailView.Columns["Guid"].Visible = false;
                    dgvPOItemDetailView.Columns["Status"].Visible = false;
                }
            }
        }

        private void btnBatchPrint_Click(object sender, EventArgs e)
        {

            List<string> guidList = new List<string>();
            Dictionary<string, string> dict = new Dictionary<string, string>();
            string sqlSelectDesc = @"Select ItemNumber,ProductName From PurchaseDepartmentStockProductName ";

            bool isExist = true;

            dict = SQLHelper.GetDataTable(GlobalSpace.FSDBConnstr, sqlSelectDesc).Rows.Cast<DataRow>().ToDictionary(r => r["ItemNumber"].ToString(), r => r["ProductName"].ToString());

            DataTable dtTemp = (DataTable)dgvPOItemDetailView.DataSource;
            DataRow dr0 = dtTemp.NewRow();
            DataTable dtNew = dtTemp.Clone();
            string itemNumber = string.Empty;
            bool isInMultiplyPO = false;
            foreach (DataGridViewRow dgvr in dgvPOItemDetailView.Rows)
            {
                if (Convert.ToBoolean(dgvr.Cells["Check"].Value))
                {
                    guidList.Add(dgvr.Cells["Guid"].Value.ToString());
                    dr0 = (dgvr.DataBoundItem as DataRowView).Row;
                    if (!dict.ContainsKey(dr0["物料代码"].ToString()))
                    {
                        isExist = false;
                        itemNumber += dr0["物料代码"].ToString() + " ";
                    }
                    dtNew.Rows.Add(dr0.ItemArray);
                }
            }

            if(!isExist)
            {
                MessageBoxEx.Show("物料"+itemNumber+"未维护品名，请先维护品名！", "提示");
                return;
            }

            bool isExistRecord = false;

            if (guidList.Count > 0)
            {
              //  MessageBoxEx.Show("当前选择多条记录，默认按照同一批次物料在不同订单中进行汇总处理！", "提示");
                for (int i = 0; i < guidList.Count; i++)
                {
                    string selectExist = @"Select Count(Id) From EBR_ReceiveRecordForInspect Where Guid='" + guidList[i] + "'";
                    if (SQLHelper.Exist(GlobalSpace.EBRConnStr, selectExist))
                    {
                        isExistRecord = true;
                    }
                }
            }
            else
            {
                MessageBoxEx.Show("当前可用记录！", "提示");
                return;
            }

            if (isExistRecord)
            {
                MessageBoxEx.Show("当前记录已有保存，请从记录管理中打印！", "提示");
                return;
            }

            DataTable dt = new DataTable();
            dt.Columns.Add("Guid");
            dt.Columns.Add("ApplyDate");
            dt.Columns.Add("ItemNumber");
            dt.Columns.Add("ItemDescription");
            dt.Columns.Add("VendorLotNumber");
            dt.Columns.Add("InternalLotNumber");
            dt.Columns.Add("MfgName");
            dt.Columns.Add("VendorName");
            dt.Columns.Add("PackageQuantity");
            dt.Columns.Add("Receiver");
            dt.Columns.Add("UM");
            dt.Columns.Add("Quantity");
            dt.Columns.Add("PackageOdd");
            dt.Columns.Add("PackageSpecification");
            dt.Columns.Add("MfgDate");
            dt.Columns.Add("ExpiredDate");
            dt.Columns.Add("PackageUM");
            dt.Columns.Add("FONumber");
            dt.Columns.Add("PONumber");
            dt.Columns.Add("LineNumber");

            foreach (DataRow drTemp in dtNew.Rows)
            {
                DataRow dr = dt.NewRow();
                dr["Guid"] = drTemp["Guid"].ToString();
                dr["ApplyDate"] = DateTime.Now.ToString("yyyy-MM-dd");
                dr["ItemNumber"] = drTemp["物料代码"].ToString() + drTemp["生产商码"].ToString();
                dr["ItemDescription"] = drTemp["描述"].ToString();
                dr["PONumber"] = drTemp["采购单号"];
                dr["LineNumber"] = drTemp["行号"];
                if (FOItemKeeperList.Contains(StockUser.UserID))
                {
                    string lotnumber = drTemp["厂家批号"].ToString().Trim();
                    string fonumber = string.Empty;


                    if (drTemp["外贸单号"] == DBNull.Value || drTemp["外贸单号"].ToString() == "")
                    {
                        // MessageBoxEx.Show("联系单号不能为空！", "提示");
                        // return;
                        dr["VendorLotNumber"] = lotnumber;
                        dr["FONumber"] = "";
                    }
                    else
                    {
                        fonumber = drTemp["外贸单号"].ToString().Trim();
                        if (fonumber.Length != 3 && fonumber.Length != 5 && fonumber.Length != 7 && fonumber.Length != 8)
                        {
                            MessageBoxEx.Show("联系单号长度不正确！", "提示");
                            return;
                        }

                        if (fonumber.Length == 3 || fonumber.Length == 5)
                        {
                            dr["VendorLotNumber"] = lotnumber.Replace(fonumber, "");
                            dr["FONumber"] = fonumber;
                        }
                        else if (fonumber.Length == 7)
                        {
                            dr["VendorLotNumber"] = lotnumber.Substring(0, lotnumber.Length - 4);
                            dr["FONumber"] = fonumber;
                        }
                        else if (fonumber.Length == 8)
                        {
                            dr["VendorLotNumber"] = lotnumber.Substring(0, lotnumber.Length - 5); ;
                            dr["FONumber"] = fonumber;
                        }
                    }

                    dr["PackageUM"] = "件";
                }
                else
                {
                    dr["VendorLotNumber"] = drTemp["厂家批号"];
                    dr["PackageUM"] = drTemp["件数单位"];
                }
                dr["InternalLotNumber"] = drTemp["公司批号"];
                dr["MfgName"] = drTemp["生产商名"];
                dr["VendorName"] = drTemp["供应商名"];
                dr["PackageQuantity"] = drTemp["整件数"];
                dr["Receiver"] = drTemp["StockKeeper"].ToString().Split('|')[1];
                dr["UM"] = drTemp["单位"];
                dr["Quantity"] = drTemp["入库数量"];
                dr["PackageOdd"] = drTemp["零头"];
                dr["PackageSpecification"] = drTemp["包装规格"];
                dr["MfgDate"] = drTemp["生产日期"];
                dr["ExpiredDate"] = drTemp["到期日期"];
                dt.Rows.Add(dr.ItemArray);
            }


            GridppReport Report = new GridppReport();
            //加载报表文件
            Report.LoadFromFile(Application.StartupPath + "\\入库请验单.grf");
            DataRow DR = dt.NewRow();
            double totalQuantity = 0;
            double totalOdd = 0;
            foreach (DataRow dr in dt.Rows)
            {
                DR = dr;
                
                DataRow[] drs = dt.Select("FileNumber='" + DR["FileNumber"].ToString() + "'");
                if (drs.Length > 1)
                {
                    for (int i = 0; i < drs.Length; i++)
                    {
                        totalOdd += Convert.ToDouble(drs[i]["PackageOdd"]);
                        totalQuantity += Convert.ToDouble(drs[i]["Quantity"]);
                    }
                }
                else
                {
                    totalOdd = Convert.ToDouble(DR["PackageOdd"]);
                    totalQuantity = Convert.ToDouble(DR["Quantity"]);
                }
                
                totalOdd = Convert.ToDouble(DR["PackageOdd"]);
                totalQuantity = Convert.ToDouble(DR["Quantity"]);

                Report.ParameterByName("ApplyDate").AsString = DR["ApplyDate"].ToString().Replace("-", ".");
                Report.ParameterByName("FileNumber").AsString = DR["FileNumber"].ToString();
                Report.ParameterByName("ItemNumber").AsString = DR["ItemNumber"].ToString();
                Report.ParameterByName("ItemDescription").AsString = DR["ItemDescription"].ToString();
                Report.ParameterByName("VendorLotNumber").AsString = DR["VendorLotNumber"].ToString();
                Report.ParameterByName("InternalLotNumber").AsString = DR["InternalLotNumber"].ToString();
                Report.ParameterByName("MfgName").AsString = DR["MfgName"].ToString();
                Report.ParameterByName("VendorName").AsString = DR["VendorName"].ToString();
                Report.ParameterByName("Packages").AsString = DR["PackageQuantity"].ToString();
                Report.ParameterByName("UM").AsString = "总标示值（" + DR["UM"].ToString() + "）";
                Report.ParameterByName("PackageOddUM").AsString = "零头标示值（" + DR["UM"].ToString() + "）";
                Report.ParameterByName("PackageUM").AsString = "整包件数（" + DR["PackageUM"].ToString() + "）";
                Report.ParameterByName("Quantity").AsString = totalQuantity.ToString();// DR["Quantity"].ToString();
                Report.ParameterByName("PackageSpecification").AsString = DR["PackageSpecification"].ToString();
                Report.ParameterByName("PackageOdd").AsString = totalOdd.ToString(); //DR["PackageOdd"].ToString();
                Report.ParameterByName("MfgDate").AsString = DR["MfgDate"].ToString();
                Report.ParameterByName("ExpiredDate").AsString = DR["ExpiredDate"].ToString();
                Report.ParameterByName("FileTracedNumber").AsString = StockUser.FileTracedNumber;
                Report.ParameterByName("FileEdition").AsString = StockUser.FileEdition;
                Report.ParameterByName("EffectiveDate").AsString = StockUser.EffectiveDate;
                Report.ParameterByName("FONumber").AsString = DR["FONumber"].ToString();
                //默认直接打印
                Report.Print(false);
            }

        }


   /*     private string GetFileNumber(string area)
        {
            string filenumber = string.Empty;
          
            // 文件编号固定格式：年度后2位 + 月份（2位）+003（仓库代码）+四位流水号，每个月1号开始从0001开。   

            if (DateTime.Now.ToString("dd") == "01")
            {
                string sqlSelectExist = @"Select Count(Id) From EBR_ReceiveRecordForInspect Where ApplyDate='" + DateTime.Now.ToString("yyyy-MM-dd") + "' And FileTracedNumber='" + StockUser.FileTracedNumber + "' And FileEdition='" + StockUser.FileEdition + "'";
                if (!SQLHelper.Exist(GlobalSpace.EBRConnStr, sqlSelectExist))
                {
                    filenumber = DateTime.Now.ToString("yyMM") + "0030001";
                }
                else
                {
                    string sqlSelectLatest = @"Select FileNumber  From EBR_ReceiveRecordForInspect Where  FileTracedNumber='" + StockUser.FileTracedNumber + "' And FileEdition='" + StockUser.FileEdition + "'  Order By FileNumber Desc";
                    string lastFileNumber = SQLHelper.ExecuteScalar(GlobalSpace.EBRConnStr, sqlSelectLatest).ToString();
                    int number = Convert.ToInt32(lastFileNumber.Substring(lastFileNumber.Length - 4, 4));
                    string newNumber = string.Empty;
                    if (number + 1 < 10)
                    {
                        newNumber = "000" + (number + 1).ToString();
                    }
                    else if (number + 1 < 100)
                    {
                        newNumber = "00" + (number + 1).ToString();
                    }
                    else if (number + 1 < 1000)
                    {
                        newNumber = "0" + (number + 1).ToString();
                    }
                    else
                    {
                        newNumber = (number + 1).ToString();
                    }
                    filenumber = DateTime.Now.ToString("yyMM") + "003" + newNumber;
                }
            }
            else
            {
                string sqlExistDay = @"Select Count(Id) From EBR_ReceiveRecordForInspect Where  FileTracedNumber='" + StockUser.FileTracedNumber + "' And FileEdition='" + StockUser.FileEdition + "'";
                //2.判断当天记录是否存在，如果存在，则获取最新一条记录的文件编号进行处理；不存在则产生当天的第一条记录。
                if (SQLHelper.Exist(GlobalSpace.EBRConnStr, sqlExistDay))
                {
                    string sqlSelectLatest = @"Select FileNumber  From EBR_ReceiveRecordForInspect Where  FileTracedNumber='" + StockUser.FileTracedNumber + "' And FileEdition='" + StockUser.FileEdition + "'  Order By FileNumber Desc";
                    string lastFileNumber = SQLHelper.ExecuteScalar(GlobalSpace.EBRConnStr, sqlSelectLatest).ToString();
                    int number = Convert.ToInt32(lastFileNumber.Substring(lastFileNumber.Length - 4, 4));
                    string newNumber = string.Empty;
                    if (number + 1 < 10)
                    {
                        newNumber = "000" + (number + 1).ToString();
                    }
                    else if (number + 1 < 100)
                    {
                        newNumber = "00" + (number + 1).ToString();
                    }
                    else if (number + 1 < 1000)
                    {
                        newNumber = "0" + (number + 1).ToString();
                    }
                    else
                    {
                        newNumber = (number + 1).ToString();
                    }
                    filenumber = DateTime.Now.ToString("yyMM") + "003" + newNumber;
                }
                else
                {
                    filenumber = DateTime.Now.ToString("yyMM") + "0030001";
                }
            }
            //   }
            return filenumber;
        }
   */
        private string GetFileNumber(string area)
        {
                       #region 2021-10-22
            string sqlSelectLatest = @"Select FileNumber  From EBR_ReceiveRecordForInspect Where  FileTracedNumber='" + StockUser.FileTracedNumber + "' And FileEdition='" + StockUser.FileEdition + "' and FileNumber like '"+ DateTime.Now.ToString("yyMM")+ StockUser.RecordArea + "____'  Order By FileNumber Desc";
            object lastFileNumber = SQLHelper.ExecuteScalar(GlobalSpace.EBRConnStr, sqlSelectLatest);
            String NewFileNumber = string.Empty;
            if (lastFileNumber == null || lastFileNumber==DBNull.Value)
            {
                NewFileNumber= DateTime.Now.ToString("yyMM") + StockUser.RecordArea + "0001";
            }
            else
            {
                string Num = lastFileNumber.ToString();
                NewFileNumber= DateTime.Now.ToString("yyMM") + StockUser.RecordArea + (Convert.ToInt32(Num.Substring(Num.Length-4,4))+1).ToString().PadLeft(4,'0'); 
            }
            return NewFileNumber;
            #endregion
        }

        private void buttonX4_Click_1(object sender, EventArgs e)
        {
            MessageBox.Show(GlobalSpace.GeneralEBRConnStr);
        }

        /*   private void btnPreview_Click(object sender, EventArgs e)
           {
               if (dgvPOItemDetailView.Rows.Count == 0)
               {
                   MessageBoxEx.Show("当前无可用的记录！", "提示");
                   return;
               }

               List<string> guidList = new List<string>();
               Dictionary<string, string> dict = new Dictionary<string, string>();
               string sqlSelectDesc = @"Select ItemNumber,ProductName From PurchaseDepartmentStockProductName ";


               dict = SQLHelper.GetDataTable(GlobalSpace.FSDBConnstr, sqlSelectDesc).Rows.Cast<DataRow>().ToDictionary(r => r["ItemNumber"].ToString(), r => r["ProductName"].ToString());

               DataTable dtTemp0 = (DataTable)dgvPOItemDetailView.DataSource;
               DataTable dtTemp = dtTemp0.Clone();

               for (int m = 0; m < dgvPOItemDetailView.Rows.Count; m++)
               {
                   if (Convert.ToBoolean(dgvPOItemDetailView.Rows[m].Cells["Check"].Value) == true)
                   {
                       DataRow dr = dtTemp.NewRow();
                       guidList.Add(dgvPOItemDetailView.Rows[m].Cells["Guid"].Value.ToString());
                       dr = (dgvPOItemDetailView.Rows[m].DataBoundItem as DataRowView).Row;
                       dtTemp.Rows.Add(dr.ItemArray);
                   }
               }
               string itemNumber = string.Empty;
               bool isExistMultiplyPOitem = false;
               int multiplyPOItemCount = 0;
               bool isExistEmptyValue = false;
               bool isExistDictEmpty = false;
               bool isExistRecord = false;
               foreach (DataRow dgvr in dtTemp.Rows)
               {
                   if (!dict.ContainsKey(dgvr["物料代码"].ToString()))
                   {
                       itemNumber += dgvr["物料代码"].ToString() + "  ";
                   }
                   else
                   {
                       if (!string.IsNullOrWhiteSpace(dict[dgvr["物料代码"].ToString()]))
                       {
                           dgvr["描述"] = dict[dgvr["物料代码"].ToString()];
                       }
                       else
                       {
                           isExistDictEmpty = true;
                       }
                   }

                   DataRow[] drs = dtTemp.Select("物料代码='" + dgvr["物料代码"].ToString() + "' And 公司批号='" + dgvr["公司批号"].ToString() + "'");
                   multiplyPOItemCount = drs.Length;
                   if (multiplyPOItemCount > 1)
                   {
                       isExistMultiplyPOitem = true;
                   }

                   if (dgvr["入库数量"] == DBNull.Value || dgvr["入库数量"].ToString() == "0" || dgvr["厂家批号"] == DBNull.Value || dgvr["厂家批号"].ToString() == "" || dgvr["公司批号"] == DBNull.Value || dgvr["公司批号"].ToString() == "")
                   {
                       isExistEmptyValue = true;
                   }
               }
               if (isExistEmptyValue)
               {
                   MessageBoxEx.Show("入库数量、厂家批号和公司批号不能为0或有空值！", "提示");
                   return;
               }

               if (isExistMultiplyPOitem && !cbMultiplyPOItem.Checked)
               {
                   MessageBoxEx.Show("当前存在同一物料同一批号在多个订单中情况，请对此类记录单独选中，并选中界面的复选框后进行提交！", "提示");
                   return;
               }
               string fileNumber = string.Empty;
               if (cbMultiplyPOItem.Checked)
               {
                   if (multiplyPOItemCount != dtTemp.Rows.Count)
                   {
                       MessageBoxEx.Show("当前跨订单物料的记录数量不准确！", "提示");
                       return;
                   }
               }

               if (guidList.Count > 0)
               {
                   for (int i = 0; i < guidList.Count; i++)
                   {
                       string selectExist = @"Select Count(Id) From EBR_ReceiveRecordForInspect Where Guid='" + guidList[i] + "'";
                       if (SQLHelper.Exist(GlobalSpace.EBRConnStr, selectExist))
                       {
                           isExistRecord = true;
                       }
                   }
               }


               if (!string.IsNullOrEmpty(itemNumber) || isExistDictEmpty)
               {
                   MessageBoxEx.Show("以下物料" + itemNumber + "没有进行品名维护或品名为空，请先进行维护！", "提示");
                   return;
               }


               DataTable dt = new DataTable();
               dt.Columns.Add("Guid");
               dt.Columns.Add("ApplyDate");
               dt.Columns.Add("ItemNumber");
               dt.Columns.Add("ItemDescription");
               dt.Columns.Add("VendorLotNumber");
               dt.Columns.Add("InternalLotNumber");
               dt.Columns.Add("MfgName");
               dt.Columns.Add("VendorName");
               dt.Columns.Add("PackageQuantity");
               dt.Columns.Add("Receiver");
               dt.Columns.Add("UM");
               dt.Columns.Add("Quantity");
               dt.Columns.Add("PackageOdd");
               dt.Columns.Add("PackageSpecification");
               dt.Columns.Add("MfgDate");
               dt.Columns.Add("ExpiredDate");
               dt.Columns.Add("PackageUM");
               dt.Columns.Add("FONumber");
               dt.Columns.Add("PONumber");
               dt.Columns.Add("LineNumber");
               dt.Columns.Add("FileNumber");

               foreach (DataRow drTemp in dtTemp.Rows)
               {
                   DataRow dr = dt.NewRow();
                   dr["Guid"] = drTemp["Guid"].ToString();
                   dr["ApplyDate"] = DateTime.Now.ToString("yyyy-MM-dd");
                   dr["ItemNumber"] = drTemp["物料代码"].ToString() + drTemp["生产商码"].ToString();
                   dr["ItemDescription"] = drTemp["描述"].ToString();
                   dr["PONumber"] = drTemp["采购单号"];
                   dr["LineNumber"] = drTemp["行号"];
                   if (FOItemKeeperList.Contains(StockUser.UserID))
                   {
                       string lotnumber = drTemp["厂家批号"].ToString().Trim();
                       string fonumber = string.Empty;


                       if (drTemp["外贸单号"] == DBNull.Value || drTemp["外贸单号"].ToString() == "")
                       {
                           // MessageBoxEx.Show("联系单号不能为空！", "提示");
                           // return;
                           dr["VendorLotNumber"] = lotnumber;
                           dr["FONumber"] = "";
                       }
                       else
                       {
                           fonumber = drTemp["外贸单号"].ToString().Trim();
                           if (fonumber.Length != 3 && fonumber.Length != 5 && fonumber.Length != 7 && fonumber.Length != 8)
                           {
                               MessageBoxEx.Show("联系单号长度不正确！", "提示");
                               return;
                           }

                           if (fonumber.Length == 3 || fonumber.Length == 5)
                           {
                               dr["VendorLotNumber"] = lotnumber.Replace(fonumber, "");
                               dr["FONumber"] = fonumber;
                           }
                           else if (fonumber.Length == 7)
                           {
                               dr["VendorLotNumber"] = lotnumber.Substring(0, lotnumber.Length - 4);
                               dr["FONumber"] = fonumber;
                           }
                           else if (fonumber.Length == 8)
                           {
                               dr["VendorLotNumber"] = lotnumber.Substring(0, lotnumber.Length - 5); ;
                               dr["FONumber"] = fonumber;
                           }
                       }

                       dr["PackageUM"] = "件";
                   }
                   else
                   {
                       dr["VendorLotNumber"] = drTemp["厂家批号"];
                       dr["PackageUM"] = drTemp["件数单位"];
                   }
                   dr["InternalLotNumber"] = drTemp["公司批号"];
                   dr["MfgName"] = drTemp["生产商名"];
                   dr["VendorName"] = drTemp["供应商名"];
                   dr["PackageQuantity"] = drTemp["整件数"];
                   dr["Receiver"] = drTemp["StockKeeper"].ToString().Split('|')[1];
                   dr["UM"] = drTemp["单位"];

                   dr["Quantity"] = drTemp["入库数量"];
                   dr["PackageOdd"] = drTemp["零头"];
                   dr["PackageSpecification"] = drTemp["包装规格"];
                   dr["MfgDate"] = drTemp["生产日期"];
                   dr["ExpiredDate"] = drTemp["到期日期"];
                   dt.Rows.Add(dr.ItemArray);
               }





               if (cbMultiplyPOItem.Checked)
               {
                   DataTable dtNew = dt.Clone();
                   List<string> fileNumberList = new List<string>();
                   double totalQuantity = 0;
                   double totalPackage = 0;
                   double totalOdd = 0;
                   DataRow drNew = dtNew.NewRow();
                   foreach (DataRow dr in dt.Rows)
                   {
                       drNew = dr;
                       totalQuantity += Convert.ToDouble(dr["Quantity"]);
                       totalPackage += Convert.ToDouble(dr["PackageQuantity"]);
                       totalOdd += Convert.ToDouble(dr["PackageOdd"]);
                   }                
                   drNew["Quantity"] = totalQuantity;
                   drNew["PackageQuantity"] = totalPackage;
                   drNew["PackageOdd"] = totalOdd;
                   dtNew.Rows.Add(drNew.ItemArray);
                   PrintBatchRecord pp = new PrintBatchRecord(dtNew, "\\入库请验单.grf", 0, 0);
                   pp.ShowDialog();
               }
               else
               {
                   PrintBatchRecord pp = new PrintBatchRecord(dt, "\\入库请验单.grf", 0, 0);
                   pp.ShowDialog();
               }


           }**/
        private void btnPreview_Click(object sender, EventArgs e)
        {
            if (dgvPOItemDetailView.Rows.Count == 0)
            {
                MessageBoxEx.Show("当前无可用的记录！", "提示");
                return;
            }

            List<string> guidList = new List<string>();
            Dictionary<string, string> dict = new Dictionary<string, string>();
            string sqlSelectDesc = @"Select ItemNumber,ProductName From PurchaseDepartmentStockProductName ";


            dict = SQLHelper.GetDataTable(GlobalSpace.FSDBConnstr, sqlSelectDesc).Rows.Cast<DataRow>().ToDictionary(r => r["ItemNumber"].ToString(), r => r["ProductName"].ToString());

            DataTable dtTemp0 = (DataTable)dgvPOItemDetailView.DataSource;
            DataTable dtTemp = dtTemp0.Clone();

            for (int m = 0; m < dgvPOItemDetailView.Rows.Count; m++)
            {
                if (Convert.ToBoolean(dgvPOItemDetailView.Rows[m].Cells["Check"].Value) == true)
                {
                    DataRow dr = dtTemp.NewRow();
                    guidList.Add(dgvPOItemDetailView.Rows[m].Cells["Guid"].Value.ToString());
                    dr = (dgvPOItemDetailView.Rows[m].DataBoundItem as DataRowView).Row;
                    dtTemp.Rows.Add(dr.ItemArray);
                }
            }
            string itemNumber = string.Empty;
            bool isExistMultiplyPOitem = false;
            int multiplyPOItemCount = 0;
            bool isExistEmptyValue = false;
            bool isExistDictEmpty = false;
            bool isExistRecord = false;
            foreach (DataRow dgvr in dtTemp.Rows)
            {
                if (!dict.ContainsKey(dgvr["物料代码"].ToString()))
                {
                    itemNumber += dgvr["物料代码"].ToString() + "  ";
                }
                else
                {
                    if (!string.IsNullOrWhiteSpace(dict[dgvr["物料代码"].ToString()]))
                    {
                        dgvr["描述"] = dict[dgvr["物料代码"].ToString()];
                    }
                    else
                    {
                        isExistDictEmpty = true;
                    }
                }

                DataRow[] drs = dtTemp.Select("物料代码='" + dgvr["物料代码"].ToString() + "' And 公司批号='" + dgvr["公司批号"].ToString() + "'");
                multiplyPOItemCount = drs.Length;
                if (multiplyPOItemCount > 1)
                {
                    isExistMultiplyPOitem = true;
                }

                if (dgvr["入库数量"] == DBNull.Value || dgvr["入库数量"].ToString() == "0" || dgvr["厂家批号"] == DBNull.Value || dgvr["厂家批号"].ToString() == "" || dgvr["公司批号"] == DBNull.Value || dgvr["公司批号"].ToString() == "")
                {
                    isExistEmptyValue = true;
                }
            }
            if (isExistEmptyValue)
            {
                MessageBoxEx.Show("入库数量、厂家批号和公司批号不能为0或有空值！", "提示");
                return;
            }

            if (isExistMultiplyPOitem && !cbMultiplyPOItem.Checked)
            {
                MessageBoxEx.Show("当前存在同一物料同一批号在多个订单中情况，请对此类记录单独选中，并选中界面的复选框后进行提交！", "提示");
                return;
            }
            string fileNumber = string.Empty;
            if (cbMultiplyPOItem.Checked)
            {
                if (multiplyPOItemCount != dtTemp.Rows.Count)
                {
                    MessageBoxEx.Show("当前跨订单物料的记录数量不准确！", "提示");
                    return;
                }
            }

            if (guidList.Count > 0)
            {
                for (int i = 0; i < guidList.Count; i++)
                {
                    string selectExist = @"Select Count(Id) From EBR_ReceiveRecordForInspect Where Guid='" + guidList[i] + "'";
                    if (SQLHelper.Exist(GlobalSpace.EBRConnStr, selectExist))
                    {
                        isExistRecord = true;
                    }
                }
            }


            if (!string.IsNullOrEmpty(itemNumber) || isExistDictEmpty)
            {
                MessageBoxEx.Show("以下物料" + itemNumber + "没有进行品名维护或品名为空，请先进行维护！", "提示");
                return;
            }


            DataTable dt = new DataTable();
            dt.Columns.Add("Guid");
            dt.Columns.Add("ApplyDate");
            dt.Columns.Add("ItemNumber");
            dt.Columns.Add("ItemDescription");
            dt.Columns.Add("VendorLotNumber");
            dt.Columns.Add("InternalLotNumber");
            dt.Columns.Add("MfgName");
            dt.Columns.Add("VendorName");
            dt.Columns.Add("PackageQuantity");
            dt.Columns.Add("Receiver");
            dt.Columns.Add("UM");
            dt.Columns.Add("Quantity");
            dt.Columns.Add("PackageOdd");
            dt.Columns.Add("PackageSpecification");
            dt.Columns.Add("MfgDate");
            dt.Columns.Add("ExpiredDate");
            dt.Columns.Add("PackageUM");
            dt.Columns.Add("FONumber");
            dt.Columns.Add("PONumber");
            dt.Columns.Add("LineNumber");
            dt.Columns.Add("FileNumber");

            dt.Columns.Add("QualityCheckStandard");
            //Checker as 复核人,Receiver as 接收请验人,Conclusion as 结论,ConclusionText as 结论其他内容,IsAnyDeviation as 物料验收过程是否出现偏差,DeviationNumber as 偏差编号,deviationIsClosed as 偏差是否已处理关闭,IsReport as 问题是否已报告,QualityManageIdea as 质量管理部门意见,Sign as 签名,SignDate as 签名日期,IsRequireClean as 是否需要清洁,PollutionSituation as 污染情况,CleanMethod as 清洁方式,IsComplete as 外包装是否完整,
            //DamageSituation as 损坏情况,CauseInvestigation1 as 原因调查1,IsSealed as 外包装是否密封,UnsealedCondition as 不密封情况,CauseInvestigation2 as 原因调查2,IsAnyMaterialWithPollutionRisk as 运输工具内是否存在造成污染交叉污染的物料,IsAnyProblemAffectedMaterialQuality as 是否有其他可能影响物料质量的问题,Question as 问题,CauseInvestigation3 as 原因调查3,LotNumberType as 批号类型,IsApprovedVendor as 是否为质量管理部门批准的供应商,StorageCondition as 规定贮存条件,TransportTemperature as 运输条件检查结果,TransportCondition as 运输条件是否符合,TransportationControlRecord as 是否有运输条件控制记录,Shape as 形状是否一致,Colour as 颜色是否一致,Font as 字体是否一致,RoughWeight as 有无毛重,NetWeight as 有无净重,ApprovalNumber as 有无批准文号,ReportType as 报告类型,Report as 有无报告
            dt.Columns.Add("Checker");
            dt.Columns.Add("ReceiverAPP");
            dt.Columns.Add("Conclusion");
            dt.Columns.Add("ConclusionText");
            dt.Columns.Add("IsAnyDeviation");
            dt.Columns.Add("DeviationNumber");
            dt.Columns.Add("deviationIsClosed");
            dt.Columns.Add("IsReport");
            dt.Columns.Add("QualityManageIdea");
            dt.Columns.Add("Sign");
            dt.Columns.Add("SignDate");
            dt.Columns.Add("IsRequireClean");
            dt.Columns.Add("PollutionSituation");
            dt.Columns.Add("CleanMethod");
            dt.Columns.Add("IsComplete");
            dt.Columns.Add("DamageSituation");
            dt.Columns.Add("CauseInvestigation1");
            dt.Columns.Add("IsSealed");
            dt.Columns.Add("UnsealedCondition");
            dt.Columns.Add("CauseInvestigation2");
            dt.Columns.Add("IsAnyMaterialWithPollutionRisk");
            dt.Columns.Add("IsAnyProblemAffectedMaterialQuality");
            dt.Columns.Add("Question");
            dt.Columns.Add("CauseInvestigation3");
            dt.Columns.Add("LotNumberType");
            dt.Columns.Add("IsApprovedVendor");
            dt.Columns.Add("StorageCondition");
            dt.Columns.Add("TransportTemperature");
            dt.Columns.Add("TransportCondition");
            dt.Columns.Add("TransportationControlRecord");
            dt.Columns.Add("Shape");
            dt.Columns.Add("Colour");
            dt.Columns.Add("Font");
            dt.Columns.Add("RoughWeight");
            dt.Columns.Add("NetWeight");
            dt.Columns.Add("ApprovalNumber");
            dt.Columns.Add("ReportType");
            dt.Columns.Add("Report");
            foreach (DataRow drTemp in dtTemp.Rows)
            {
                DataRow dr = dt.NewRow();
                dr["Guid"] = drTemp["Guid"].ToString();
                dr["ApplyDate"] = DateTime.Now.ToString("yyyy-MM-dd");
                dr["ItemNumber"] = drTemp["物料代码"].ToString() + drTemp["生产商码"].ToString();
                dr["ItemDescription"] = drTemp["描述"].ToString();
                dr["PONumber"] = drTemp["采购单号"];
                dr["LineNumber"] = drTemp["行号"];
                if (FOItemKeeperList.Contains(StockUser.UserID))
                {
                    string lotnumber = drTemp["厂家批号"].ToString().Trim();
                    string fonumber = string.Empty;


                    if (drTemp["外贸单号"] == DBNull.Value || drTemp["外贸单号"].ToString() == "")
                    {
                        // MessageBoxEx.Show("联系单号不能为空！", "提示");
                        // return;
                        dr["VendorLotNumber"] = lotnumber;
                        dr["FONumber"] = "";
                    }
                    else
                    {
                        fonumber = drTemp["外贸单号"].ToString().Trim();
                        if (fonumber.Length != 3 && fonumber.Length != 5 && fonumber.Length != 7 && fonumber.Length != 8)
                        {
                            MessageBoxEx.Show("联系单号长度不正确！", "提示");
                            return;
                        }

                        if (fonumber.Length == 3 || fonumber.Length == 5)
                        {
                            dr["VendorLotNumber"] = lotnumber.Replace(fonumber, "");
                            dr["FONumber"] = fonumber;
                        }
                        else if (fonumber.Length == 7)
                        {
                            dr["VendorLotNumber"] = lotnumber.Substring(0, lotnumber.Length - 4);
                            dr["FONumber"] = fonumber;
                        }
                        else if (fonumber.Length == 8)
                        {
                            dr["VendorLotNumber"] = lotnumber.Substring(0, lotnumber.Length - 5); ;
                            dr["FONumber"] = fonumber;
                        }
                    }

                    dr["PackageUM"] = "件";
                }
                else
                {
                    dr["VendorLotNumber"] = drTemp["厂家批号"];
                    dr["PackageUM"] = drTemp["件数单位"];
                }
                dr["InternalLotNumber"] = drTemp["公司批号"];
                dr["MfgName"] = drTemp["生产商名"];
                dr["VendorName"] = drTemp["供应商名"];
                dr["PackageQuantity"] = drTemp["整件数"];
                dr["Receiver"] = drTemp["StockKeeper"].ToString().Split('|')[1];
                dr["UM"] = drTemp["单位"];

                dr["Quantity"] = drTemp["入库数量"];
                dr["PackageOdd"] = drTemp["零头"];
                dr["PackageSpecification"] = drTemp["包装规格"];
                dr["MfgDate"] = drTemp["生产日期"];
                dr["ExpiredDate"] = drTemp["到期日期"];
                //新增字段

                dr["QualityCheckStandard"] = drTemp["检验标准"];
                dr["Checker"] = drTemp["复核人"];
                dr["ReceiverAPP"] = drTemp["接收请验人"];
                dr["Conclusion"] = drTemp["结论"];
                dr["ConclusionText"] = drTemp["结论其他内容"];
                dr["IsAnyDeviation"] = drTemp["物料验收过程是否出现偏差"];
                dr["DeviationNumber"] = drTemp["偏差编号"];
                dr["deviationIsClosed"] = drTemp["偏差是否已处理关闭"];
                dr["IsReport"] = drTemp["问题是否已报告"];
                dr["QualityManageIdea"] = drTemp["质量管理部门意见"];
                dr["Sign"] = drTemp["签名"];
                dr["SignDate"] = drTemp["签名日期"];
                dr["IsRequireClean"] = drTemp["是否需要清洁"];
                dr["PollutionSituation"] = drTemp["污染情况"];
                dr["CleanMethod"] = drTemp["清洁方式"];
                dr["IsComplete"] = drTemp["外包装是否完整"];
                dr["DamageSituation"] = drTemp["损坏情况"];
                dr["CauseInvestigation1"] = drTemp["原因调查1"];
                dr["IsSealed"] = drTemp["外包装是否密封"];
                dr["UnsealedCondition"] = drTemp["不密封情况"];
                dr["CauseInvestigation2"] = drTemp["原因调查2"];
                dr["IsAnyMaterialWithPollutionRisk"] = drTemp["运输工具内是否存在造成污染交叉污染的物料"];
                dr["IsAnyProblemAffectedMaterialQuality"] = drTemp["是否有其他可能影响物料质量的问题"];
                dr["Question"] = drTemp["问题"];
                dr["CauseInvestigation3"] = drTemp["原因调查3"];
                dr["LotNumberType"] = drTemp["批号类型"];
                dr["IsApprovedVendor"] = drTemp["是否为质量管理部门批准的供应商"];
                dr["StorageCondition"] = drTemp["规定贮存条件"];
                dr["TransportTemperature"] = drTemp["运输条件检查结果"];
                dr["TransportCondition"] = drTemp["运输条件是否符合"];
                dr["TransportationControlRecord"] = drTemp["是否有运输条件控制记录"];
                dr["Shape"] = drTemp["形状是否一致"];
                dr["Colour"] = drTemp["颜色是否一致"];
                dr["Font"] = drTemp["字体是否一致"];
                dr["RoughWeight"] = drTemp["有无毛重"];
                dr["NetWeight"] = drTemp["有无净重"];
                dr["ApprovalNumber"] = drTemp["有无批准文号"];
                dr["ReportType"] = drTemp["报告类型"];
                dr["Report"] = drTemp["有无报告"];
                dt.Rows.Add(dr.ItemArray);
            }





            if (cbMultiplyPOItem.Checked)
            {
                DataTable dtNew = dt.Clone();
                List<string> fileNumberList = new List<string>();
                double totalQuantity = 0;
                double totalPackage = 0;
                double totalOdd = 0;
                DataRow drNew = dtNew.NewRow();
                foreach (DataRow dr in dt.Rows)
                {
                    drNew = dr;
                    totalQuantity += Convert.ToDouble(dr["Quantity"]);
                    totalPackage += Convert.ToDouble(dr["PackageQuantity"]);
                    totalOdd += Convert.ToDouble(dr["PackageOdd"]);
                }
                drNew["Quantity"] = totalQuantity;
                drNew["PackageQuantity"] = totalPackage;
                drNew["PackageOdd"] = totalOdd;
                dtNew.Rows.Add(drNew.ItemArray);
                PrintBatchRecord pp = new PrintBatchRecord(dtNew, "\\入库请验单.grf", 0, 0,false);
                pp.ShowDialog();
            }
            else
            {
                PrintBatchRecord pp = new PrintBatchRecord(dt, "\\入库请验单.grf", 0, 0,false);
                pp.ShowDialog();
            }

        }
        private void tbItemDesc_Click(object sender, EventArgs e)
        {
            tbItemDesc.Text = "";
        }

        private void tbVendorName_Click(object sender, EventArgs e)
        {
            tbVendorName.Text = "";
            tbVendorName.ForeColor = Color.Black;
        }

        private void tbAssistantItem_Click(object sender, EventArgs e)
        {
            tbAssistantItem.Text = "";
            tbAssistantItem.ForeColor = Color.Black;
        }

        private void btnInspectionConfirm_Click(object sender, EventArgs e)
        {
            List<string> sqlList = new List<string>();
            foreach (DataGridViewRow dgvr in dgvAssistant.Rows)
            {
                if (Convert.ToBoolean(dgvr.Cells["ACheck"].Value))
                {
                    string sqlUpdate = @"Update PurchaseOrderRecordHistoryByCMF Set ReceiveQuantity=" + Convert.ToDouble(dgvr.Cells["入库量"].Value) + ",ReceiveDate='" + DateTime.Now.ToString("yyyy-MM-dd") + "',SubmitOperateDateTime='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "',Operator='" + StockUser.UserID + "',IsDirectERP=1,Status = 0 Where Guid = '" + dgvr.Cells["Guid"].Value.ToString() + "'";
                    sqlList.Add(sqlUpdate);
                }
            }

            if (sqlList.Count == 0)
            {
                Custom.MsgEx("无可用记录！");
            }
            else
            {
                if (SQLHelper.BatchExecuteNonQuery(GlobalSpace.FSDBConnstr, sqlList))
                {
                    Custom.MsgEx("确认成功！");
                    dgvAssistant.DataSource = GetAssistantVendorPOItemsDetail("", "");
                    dgvAssistant.Columns["Guid"].Visible = false;
                }
                else
                {
                    Custom.MsgEx("确认失败！");
                }
            }
        }

        private void dgvPODetailFS_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int RowIndex = e.RowIndex;
            if (RowIndex < 0) return;
            dgvPODetailFS["Check2", RowIndex].Value = !Convert.ToBoolean(dgvPODetailFS["Check2", RowIndex].Value);
        }
    }
    }
