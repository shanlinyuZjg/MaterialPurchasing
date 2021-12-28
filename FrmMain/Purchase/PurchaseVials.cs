using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using Global.Helper;
using ICSharpCode.SharpZipLib.Zip;
using Global;
namespace Global.Purchase
{
    public partial class PurchaseVials : Office2007Form
    {
        public int PONumberStartNumber, PONumberEndNumber;

        public PurchaseVials(int startNumber,int endNumber)
        {
            this.EnableGlass= false;
            MessageBoxEx.EnableGlass = false;
            InitializeComponent();
            PONumberStartNumber = startNumber;
            PONumberEndNumber = endNumber;
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            dgvDetail.DataSource = GetVialsDataTable();
            dgvDetail.Columns["Id"].Visible = false;
        }


        private DataTable GetVialsDataTable()
        {
            string sqlSelect = @"SELECT
	                                            VendorNumber AS 供应商码,
	                                            VendorName AS 供应商名,
	                                            ItemNumber AS 物料代码,
	                                            ItemDescription AS 描述,
                                                '0' AS 税前价格,
	                                            Quantity AS 数量,
	                                            LotNumber AS 厂家批号,
	                                            InternalLotNumber AS 公司批号,
	                                            ExpirationDate AS 过期日期,Id
                                            FROM
	                                            dbo.PurchaseDepartmentVial
                                            WHERE
	                                            Status = 0";
            return SQLHelper.GetDataTable(GlobalSpace.FSDBConnstr, sqlSelect);
        }

        //下达订单
        private void PlacePurchaseOrderNew(string poNumber,out string parentGuid)
        {
            parentGuid = string.Empty;
            string vendorId = string.Empty;
            string vendorName = string.Empty;
            string strSqlExist = @"Select Id From PurchaseOrderRecordByCMF Where PONumber='" + poNumber + "'";
            string strSqlExistFromFS = @"Select Count(PONumber) From FSDBMR.dbo._NoLock_FS_POHeader Where PONumber='" + poNumber + "'";
            if (SQLHelper.Exist(GlobalSpace.FSDBConnstr, strSqlExist) || SQLHelper.Exist(GlobalSpace.FSDBMRConnstr, strSqlExistFromFS))
            {
                MessageBoxEx.Show("订单号已存在！", "提示");
                return;
            }
            else
            {
                    vendorId = "370021";
                    vendorName = "山东省药用玻璃股份有限公司";            
                try
                {
                    string strInsert = @"Insert Into PurchaseOrderRecordByCMF (PONumber,VendorNumber,VendorName,Buyer,Superior,IsPurePO,Guid,POTogether) Values (@PONumber,@VendorNumber,@VendorName,@Buyer,@Supervisor,@IsPurePO,@Guid,@POTogether)";
                    int poTogether = 0;
                    if (PurchaseUser.POTogether == 1)
                    {
                        poTogether = 1;
                    }
                    parentGuid = Guid.NewGuid().ToString("N");
                    SqlParameter[] sqlparams =
                    {
                        new SqlParameter("@PONumber",poNumber),
                        new SqlParameter("@VendorNumber",vendorId),
                        new SqlParameter("@VendorName",vendorName),
                        new SqlParameter("@Buyer",PurchaseUser.UserID),
                       new SqlParameter("@Supervisor",PurchaseUser.SupervisorID),
                       new SqlParameter("@IsPurePO",1),
                       new SqlParameter("@Guid",parentGuid),
                       new SqlParameter("@POTogether",poTogether),
                    };
                    if (SQLHelper.ExecuteNonQuery(GlobalSpace.FSDBConnstr, strInsert, sqlparams))
                    {
                        MessageBoxEx.Show("订单下达成功！");
                        //            tbPONumber.Text = "";
                    }
                    else
                    {
                        MessageBoxEx.Show("订单下达失败！");
                    }
                }
                catch (Exception ex)
                {
                    MessageBoxEx.Show("异常：" + ex.Message);
                }
            }
        }

        private void btnPlacePO_Click(object sender, EventArgs e)
        {
            if (dgvDetail.Rows.Count == 0)
            {
                MessageBoxEx.Show("当前无可用数据！", "提示");
                return;
            }
            string parentGuid = string.Empty;
            string poNumber = string.Empty;
            string date = DateTime.Now.ToString("MMddyy");
            string sequenceNumber = GeneratePONumberSequenceNumber("PP", PurchaseUser.UserID);
            poNumber = "PP" + "-" + date + "-" + sequenceNumber;
            //下达订单首部信息
            PlacePurchaseOrderNew(poNumber, out parentGuid);
            DataTable dtPO = (DataTable)dgvDetail.DataSource;
            List<string> sqlList = new List<string>();
            DataTable dtItemInfo = CommonOperate.GetBatchItemInfo(dtPO.AsEnumerable().Select(r => r.Field<string>("物料代码")).ToList());

            foreach (DataRow dr in dtPO.Rows)
            {
                if(dr["税前价格"] != DBNull.Value  || !string.IsNullOrWhiteSpace(dr["税前价格"].ToString()))
                {
                    DataRow[] drs = dtItemInfo.Select("ItemNumber='" + dr["物料代码"].ToString() + "'");
                    string sqlInsert = @"INSERT INTO PurchaseOrderRecordByCMF (
	                                            PONumber,
	                                            VendorNumber,
	                                            VendorName,
	                                            ManufacturerNumber,
	                                            ManufacturerName,
	                                            ItemNumber,
	                                            ItemDescription,
	                                            LineUM,
	                                            Buyer,
                                                BuyerName,
	                                            Superior,
	                                            DemandDeliveryDate,	                                      
	                                            POStatus,PricePreTax,	                                         
	                                            UnitPrice,
	                                            LineType,
	                                            LineStatus,
                                                NeededDate,
                                                PromisedDate,
                                                POItemQuantity,                                              
                                                StockKeeper,
Stock,Bin,InspectionPeriod,Guid,ParentGuid,POItemConfirmer,ItemReceiveType,IsDirectPurchaseVial  ) VALUES(  '" + poNumber + "','" + dr["供应商码"].ToString() + "', '" + dr["供应商名"].ToString() + "', '" + dr["供应商码"].ToString() + "', '" + dr["供应商名"].ToString() + "', '" + dr["物料代码"].ToString() + "', '" + dr["描述"].ToString() + "','" + drs[0]["UM"].ToString() + "', '" + PurchaseUser.UserID + "', '" + PurchaseUser.UserName + "','DJB','" + DateTime.Now.AddDays(12).ToString("MMddyy") + "',2," + Convert.ToDouble(dr["税前价格"]) + " ," + Math.Round(Convert.ToDouble(dr["税前价格"]) / (1 + Convert.ToDouble(tbTaxRate.Text)), 9) + ",'P',4, '" + DateTime.Now.AddDays(12).ToString("MMddyy") + "','" + DateTime.Now.AddDays(12).ToString("MMddyy") + "'," + Convert.ToDouble(dr["数量"]) + ",'S42|田小翠','" + drs[0]["Stock"].ToString() + "','" + drs[0]["Bin"].ToString() + "','" + drs[0]["IsInspectionRequired"].ToString() + "','" + Guid.NewGuid().ToString() + "','"+parentGuid+"','P08','" + PurchaseUser.ItemReceiveType + "',1)";
                    string sqlUpdate = @"Update PurchaseDepartmentVial Set Status = 1 where Id='"+dr["Id"].ToString()+"'";
                  //  textBoxX1.Text = sqlInsert;
                    sqlList.Add(sqlInsert);
                    sqlList.Add(sqlUpdate);
                }               
            }
            
            if(SQLHelper.BatchExecuteNonQuery(GlobalSpace.FSDBConnstr,sqlList))
            {
                MessageBoxEx.Show("下达订单成功！", "提示");
                dgvDetail.DataSource = GetVialsDataTable();
                dgvDetail.Columns["Id"].Visible = false;
            }
            else
            {
                MessageBoxEx.Show("下达订单失败！", "提示");
            }

        }

        private void btnUpdatePrice_Click(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)dgvDetail.DataSource;
            foreach(DataRow dr in dt.Rows)
            {
                dr["税前价格"] = GetVialPrice(dr["物料代码"].ToString());
            }
        }

        //获取价格
        private string GetVialPrice(string itemNumber)
        {
            string price = string.Empty;
            string sqlSelectItemPrice = @"SELECT
	                                                    ItemNumber AS 物料代码,
	                                                    ItemDescription AS 物料描述,
	                                                    VendorNumber AS 供应商代码,
	                                                    VendorName AS 供应商名称,
	                                                    PricePreTax AS 含税价格
                                                    FROM
	                                                    PurchaseDepartmentDomesticProductItemPrice
                                                    WHERE
	                                                    ItemNumber = '" + itemNumber + "' And VendorNumber = '370021'";

            DataTable dtItemPrice = SQLHelper.GetDataTable(GlobalSpace.FSDBConnstr, sqlSelectItemPrice);
            if (dtItemPrice.Rows.Count > 0)
            {
                if (dtItemPrice.Rows.Count == 1)
                {
                    price = dtItemPrice.Rows[0]["含税价格"].ToString();
                }
                else
                {
                    price = "手动选择";
                }
            }
            return price;
        }

        private void dgvDetail_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            dgvDetail_CellContentDoubleClick(sender, e);
        }

        private void dgvDetail_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            Purchase.DomesticItemPrice dip = new DomesticItemPrice(dgvDetail.Rows[dgvDetail.CurrentCell.RowIndex].Cells["物料代码"].Value.ToString(), "370021","");
            dip.ShowDialog();
            dgvDetail.Rows[dgvDetail.CurrentCell.RowIndex].Cells["税前价格"].Value = GlobalSpace.VialPrice.ToString();
        }

        private void buttonX2_Click(object sender, EventArgs e)
        {

        }

        //获取最新订单编号
        private string GeneratePONumberSequenceNumber(string poType, string userID)
        {
            string strReturn = string.Empty;
            string sqlSelect = string.Empty;
            int sequenceNumber = 0;
            int sequenceNumberFS = 0;
            string latestPONumber = string.Empty;
            string strSequenceNumber = string.Empty;
            string strPOSequenceNumber = string.Empty;
            string latestPONumberFS = string.Empty;
            string strSequenceNumberFS = string.Empty;
            string strPOSequenceNumberFS = string.Empty;
            string dateNow = DateTime.Now.ToString("MMddyy");


            //检查该供应商订单是否存在，存在则提示
                sqlSelect = @"Select TOP 1 PONumber From PurchaseOrderRecordByCMF Where Buyer = '" + userID + "' And Left(PONumber,2) = '" + poType + "' And POItemPlacedDate = '" + DateTime.Now.ToString("yyyy-MM-dd") + "'  AND IsPurePO = 1 ORDER BY Id DESC";
                string sqlSelectFS = @" SELECT
	                            T1.PONumber
                            FROM
	                            _NoLock_FS_POHeader T1
                            WHERE                               
                                T1.Buyer ='" + userID + "' AND T1.PONumber LIKE '%" + dateNow + "%'  ORDER BY T1.PONumber DESC";
                DataTable dt = SQLHelper.GetDataTable(GlobalSpace.FSDBConnstr, sqlSelect);
                DataTable dtLatestFS = SQLHelper.GetDataTable(GlobalSpace.FSDBMRConnstr, sqlSelectFS);

                if (dt.Rows.Count > 0)
                {
                    latestPONumber = dt.Rows[0]["PONumber"].ToString();
                    strSequenceNumber = latestPONumber.Substring(10);
                    sequenceNumber = Convert.ToInt32(strSequenceNumber);
                }

                if (dtLatestFS.Rows.Count > 0)
                {
                    latestPONumberFS = dtLatestFS.Rows[0]["PONumber"].ToString();
                    strSequenceNumberFS = latestPONumberFS.Substring(10);
                    sequenceNumberFS = Convert.ToInt32(strSequenceNumberFS);
                }

                if (sequenceNumberFS > sequenceNumber)
                {
                    sequenceNumber = sequenceNumberFS;
                }


                if (sequenceNumber == 0)
                {
                    if (PONumberStartNumber == 1)
                    {
                        strReturn = "001";
                    }
                    else if (PONumberStartNumber == 51)
                    {
                        strReturn = "051";
                    }
                    else
                    {
                        strReturn = PONumberStartNumber.ToString();
                    }
                }
                else
                {
                    //     string str = dt.Rows[0]["PONumber"].ToString().Substring(10, 3);
                    sequenceNumber = sequenceNumber + 1;
                    if (sequenceNumber > PONumberEndNumber)
                    {
                        strReturn = "BeyondRange";
                        return strReturn;
                    }
                    if (sequenceNumber.ToString().Length == 1)
                    {
                        strReturn = "00" + sequenceNumber.ToString();
                    }
                    else if (sequenceNumber.ToString().Length == 2)
                    {
                        strReturn = "0" + sequenceNumber.ToString();
                    }
                    else
                    {
                        strReturn = sequenceNumber.ToString();
                    }
                }   
            return strReturn;
        }
    }
}
