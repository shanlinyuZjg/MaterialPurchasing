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

namespace Global.Purchase
{
    public partial class ForeignOrderItemAutomaticPlaceOrder : Office2007Form
    {
        string UserID = string.Empty;
        string UserName = string.Empty;
        List<string> VendorList = new List<string>();
        List<string> ItemInfoList = new List<string>();
        public ForeignOrderItemAutomaticPlaceOrder(string id, string name)
        {
            InitializeComponent();
            UserID = id;
            UserName = name;
            this.EnableGlass = false;
            MessageBoxEx.EnableGlass = false;
        }

        private void ForeignOrderItemAutomaticPlaceOrder_Load(object sender, EventArgs e)
        {
            LoadFOItemDetail(UserID, 1, 1);
            BtnSpecialRefresh_Click(null, null);
        }

        private void LoadFOItemDetail(string id, int status, int valid)
        {
            string sqlSelect = @"Select Id, ForeignOrderNumber AS 外贸单号, ItemNumber AS 物料代码,ItemDescription AS 物料描述,ItemUM AS 单位,VendorNumber AS 供应商码,VendorName AS 供应商名,PurchasePrice AS 价格,Quantity AS 采购数量,TotalAmount AS 总金额,SpecificationDescription AS 规格,Requirements AS 要求 From PurchaseDepartmentForeignOrderItemByCMF Where BuyerID='" + id + "'  And IsValid = " + valid + " And Status = " + status + "    Order by OperateDateTime DESC";
            dgvFOConfirmItemsDetail.DataSource = SQLHelper.GetDataTable(GlobalSpace.FSDBConnstr, sqlSelect);
            dgvFOConfirmItemsDetail.Columns["Id"].Visible = false;
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            string deliveryDate = string.Empty;
            double TaxRate = Convert.ToDouble(TbTaxRate.Text);
            if (string.IsNullOrEmpty(tbArrivedDate.Text) && string.IsNullOrEmpty(tbDelayDays.Text))
            {
                Custom.MsgEx("到货日期和当前日期延后天数不能同时为空！");
                return;
            }

            if (string.IsNullOrEmpty(tbArrivedDate.Text) && !string.IsNullOrEmpty(tbDelayDays.Text))
            {
                deliveryDate = tbDelayDays.Text;
            }
            if (!string.IsNullOrEmpty(tbArrivedDate.Text) && string.IsNullOrEmpty(tbDelayDays.Text))
            {
                deliveryDate = tbArrivedDate.Text;
            }
            if (!string.IsNullOrEmpty(tbArrivedDate.Text) && !string.IsNullOrEmpty(tbDelayDays.Text))
            {
                deliveryDate = tbArrivedDate.Text;
            }

            string sqlSelectVendor = @"Select Distinct  VendorNumber,VendorName,SupervisorID,BuyerID From  PurchaseDepartmentForeignOrderItemByCMF  Where IsValid = 1 And Status = 1 And BuyerID = '"+UserID+"'";
            string sqlSelectItemDetail = @"SELECT
                                                                Id,
	                                                            ForeignOrderNumber,
	                                                            ItemNumber,
	                                                            ItemDescription,
	                                                            VendorNumber,
                                                                VendorName,
	                                                            PurchasePrice,
	                                                            CheckedWay,
	                                                            SupervisorID,
	                                                            ItemUM,
	                                                            Quantity,
	                                                            BuyerID,
	                                                            SpecificationDescription,
	                                                            TotalAmount,
	                                                            Requirements
                                                            FROM
	                                                            PurchaseDepartmentForeignOrderItemByCMF  Where IsValid = 1 And Status = 1  And BuyerID = '" + UserID + "' And ItemNumber Not IN (Select ItemNumber From PurchaseDepartmentForeignOrderItemNotInByCMF)";
            DataTable dtVendor = SQLHelper.GetDataTable(GlobalSpace.FSDBConnstr, sqlSelectVendor);
            DataTable dtItemDetail = SQLHelper.GetDataTable(GlobalSpace.FSDBConnstr, sqlSelectItemDetail);
            if (VendorList.Count > 0)
            {
                VendorList.Clear();
            }
            //dt->list
            if (dtVendor.Rows.Count > 0)
            {
                DataTable dtPO = null;
                if (PlacePurchaseOrder(dtItemDetail, out dtPO))
                {
                    //将各个物料添加到订单中
                    List<string> sqlList = new List<string>();
                    for (int i = 0; i < dtPO.Rows.Count; i++)
                    {
                        DataRow[] drs = dtItemDetail.Select("VendorNumber = '" + dtPO.Rows[i]["VendorNumber"].ToString() + "'");

                        foreach (DataRow dr in drs)
                        {
                            string itemKeeper = GetItemStockKeeper(dr["ItemNumber"].ToString());
                            ItemInfoList = GetItemInfo(dr["ItemNumber"].ToString());
                            string sqlInsert = string.Empty;
                            if (deliveryDate.Length == 8)
                            {
                                sqlInsert = @"INSERT INTO PurchaseOrderRecordByCMF (
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
	                                            ForeignNumber,
	                                            POStatus,
	                                            ItemUsedPoint,
	                                            QualityCheckStandard,
	                                            UnitPrice,
                                                PricePreTax,
	                                            LineType,
	                                            LineStatus,
                                                NeededDate,
                                                PromisedDate,
                                                POItemQuantity,                                              
                                                StockKeeper,
Stock,Bin,InspectionPeriod,Guid,TaxRate,Specification,Comment1,ParentGuid,POItemConfirmer,ItemReceiveType,IsFOItem,LotNumberAssign)
                                            VALUES
	                                            (
	                                            '" + dtPO.Rows[i]["PONumber"].ToString() + "','" + dr["VendorNumber"].ToString() + "', '" + dr["VendorName"].ToString() + "', '" + dr["VendorNumber"].ToString() + "', '" + dr["VendorName"].ToString() + "', '" + dr["ItemNumber"].ToString() + "', '" + dr["ItemDescription"].ToString() + "','" + dr["ItemUM"].ToString() + "', '" + dr["BuyerID"].ToString() + "', '" + UserName + "','" + dr["SupervisorID"].ToString() + "','" + DateTime.Now.AddDays(12).ToString("MMddyy") + "','" + dr["ForeignOrderNumber"].ToString() + "',2,'', ''," + Math.Round(Convert.ToDouble(dr["PurchasePrice"]) / (1+TaxRate), 9) + "," + Convert.ToDouble(dr["PurchasePrice"]) + ",'P',4, '" + deliveryDate + "','" + DateTime.Now.AddDays(12).ToString("MMddyy") + "'," + Convert.ToDouble(dr["Quantity"]) + ",'" + itemKeeper.Trim() + "','" + ItemInfoList[1] + "','" + ItemInfoList[2] + "','" + ItemInfoList[0] + "','" + Guid.NewGuid().ToString() + "','"+TaxRate.ToString()+"','" + dr["SpecificationDescription"].ToString() + "','" + dr["Requirements"].ToString() + "','" + dr["ParentGuid"].ToString() + "','"+"P08"+"','"+PurchaseUser.ItemReceiveType+"',1,'C')";
                            }
                            else
                            {
                                sqlInsert = @"INSERT INTO PurchaseOrderRecordByCMF (
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
	                                            ForeignNumber,
	                                            POStatus,
	                                            ItemUsedPoint,
	                                            QualityCheckStandard,
	                                            UnitPrice,
                                                PricePreTax,
	                                            LineType,
	                                            LineStatus,
                                                NeededDate,
                                                PromisedDate,
                                                POItemQuantity,                                               
                                                StockKeeper,                                               Stock,Bin,InspectionPeriod,Guid,TaxRate,Specification,Comment1,ParentGuid,POItemConfirmer,ItemReceiveType,IsFOItem,LotNumberAssign)
                                            VALUES (
	                                            '" + dtPO.Rows[i]["PONumber"].ToString() + "','" + dr["VendorNumber"].ToString() + "', '" + dr["VendorName"].ToString() + "', '" + dr["VendorNumber"].ToString() + "', '" + dr["VendorName"].ToString() + "', '" + dr["ItemNumber"].ToString() + "', '" + dr["ItemDescription"].ToString() + "','" + dr["ItemUM"].ToString() + "', '" + dr["BuyerID"].ToString() + "', '" + UserName + "','" + dr["SupervisorID"].ToString() + "','" + DateTime.Now.AddDays(12).ToString("MMddyy") + "','" + dr["ForeignOrderNumber"].ToString() + "',2,'', ''," + Math.Round(Convert.ToDouble(dr["PurchasePrice"]) / (1+TaxRate), 9) + "," + Convert.ToDouble(dr["PurchasePrice"]) + ",'P',4, '" + DateTime.Now.AddDays(Convert.ToInt32(deliveryDate)).ToString("MMddyy") + "','" + DateTime.Now.AddDays(12).ToString("MMddyy") + "'," + Convert.ToDouble(dr["Quantity"]) + ",'" + itemKeeper.Trim() + "','" + ItemInfoList[1] + "','" + ItemInfoList[2] + "','" + ItemInfoList[0] + "','" + Guid.NewGuid().ToString() + "','"+TaxRate.ToString()+"','" + dr["SpecificationDescription"].ToString() + "','" + dr["Requirements"].ToString() + "','" + dr["ParentGuid"].ToString() + "','"+ "P08" + "','" + PurchaseUser.ItemReceiveType + "',1,'C')";
                            }


                            string sqlFOUpdate = @"Update PurchaseDepartmentForeignOrderItemByCMF Set Status = 2 Where Id = " + Convert.ToInt32(dr["Id"]) + "";
                            sqlList.Add(sqlInsert);
                            sqlList.Add(sqlFOUpdate);
                        }
                    }

                    if (SQLHelper.BatchExecuteNonQuery(GlobalSpace.FSDBConnstr, sqlList))
                    {
                        MessageBoxEx.Show("物料添加成功", "提示");
                    }
                    else
                    {
                        MessageBoxEx.Show("物料添加失败", "提示");
                    }
                }
                else
                {
                    Custom.MsgEx("没有可用的数据！");
                }
            }
            LoadFOItemDetail(UserID, 1, 1);
        }

        //下达订单
        private bool PlacePurchaseOrder(DataTable dt, out DataTable dtPO)
        {
            string PONumber = string.Empty;
            int sequqnceNumber = 0;
            string vendorId = string.Empty;
            string vendorName = string.Empty;
            string sqlSelectLatest = @"Select Distinct Id,PONumber From PurchaseOrderRecordByCMF Where POItemPlacedDate='" + DateTime.Now.ToString("yyyy-MM-dd") + "'  And Buyer = '" + UserID + "' And IsPurePO = 1  Order By Id DESC";

            //查询当天最新的订单记录
            DataTable dtLatest = SQLHelper.GetDataTable(GlobalSpace.FSDBConnstr, sqlSelectLatest);
            if (dtLatest.Rows.Count > 0)
            {
                string ponumber = dtLatest.Rows[0]["PONumber"].ToString();
                sequqnceNumber = Convert.ToInt32(ponumber.Substring(10));
                sequqnceNumber = sequqnceNumber + 1;
            }
            else
            {
                string[] range = PurchaseUser.PONumberSequenceNumberRange.Split('-');
                int poNumberStartNumber = Convert.ToInt32(range[0]);
                sequqnceNumber = poNumberStartNumber;
            }

            DataTable dtTemp = null;
            try
            {
                List<string> sqlList = new List<string>();
                dt.Columns.Add("PONumber");
                dt.Columns.Add("ParentGuid");
                dtTemp = dt.Clone();
                string[] names = CommonOperate.GetDistinctNamesFromDataTable(dt, "VendorNumber");
                for (int i = 0; i < names.Length; i++)
                {
                    DataRow[] drs = dt.Select("VendorNumber = '" + names[i] + "'");
                    if (drs.Length > 0)
                    {
                        DataRow dr = dtTemp.NewRow();
                        dr = drs[0];
                        dtTemp.ImportRow(dr);
                    }
                }


                for (int i = 0; i < dtTemp.Rows.Count; i++)
                {
                    string tempPONumber = string.Empty;
                    /*****
                    string sqlVendorExist = @"Select Count(Id) From PurchaseOrderRecordByCMF Where VendorNumber = '" + dtTemp.Rows[i]["VendorNumber"].ToString() + "' And POItemPlacedDate='" + DateTime.Now.ToString("yyyy-MM-dd") + "' And Buyer = '" + UserID + "' And IsPurePO = 1";
                    if (!SQLHelper.Exist(GlobalSpace.FSDBConnstr, sqlVendorExist))
                    {*/
                        tempPONumber = "PP-" + DateTime.Now.ToString("MMddyy") + "-" + (sequqnceNumber + i).ToString(); ;
               /*     }
                    else
                    {
                        string sqlVendorSelect = @"Select PONumber From PurchaseOrderRecordByCMF Where VendorNumber = '" + dtTemp.Rows[i]["VendorNumber"].ToString() + "' And POItemPlacedDate='" + DateTime.Now.ToString("yyyy-MM-dd") + "' And Buyer = '" + UserID + "' And IsPurePO = 1";
                        tempPONumber = SQLHelper.ExecuteScalar(GlobalSpace.FSDBConnstr, sqlVendorSelect).ToString();
                    }*/
                    string guid = Guid.NewGuid().ToString("N");
                    string sqlInsert = @"Insert Into PurchaseOrderRecordByCMF (PONumber,VendorNumber,VendorName,Buyer,Superior,Guid,IsPurePO) Values ('" + tempPONumber + "','" + dtTemp.Rows[i]["VendorNumber"].ToString() + "','" + dtTemp.Rows[i]["VendorName"].ToString() + "','" + dtTemp.Rows[i]["BuyerID"].ToString() + "','" + dtTemp.Rows[i]["SupervisorID"].ToString() + "','" + guid + "',1)";
                    sqlList.Add(sqlInsert);
                    dtTemp.Rows[i]["PONumber"] = tempPONumber;
                    dtTemp.Rows[i]["ParentGuid"] = guid;
                }
                    if (!SQLHelper.BatchExecuteNonQuery(GlobalSpace.FSDBConnstr, sqlList))
                    {
                        Custom.MsgEx("订单下达失败！");
                        dtPO = null;
                        return false;
                    }
                
            }
            catch (Exception ex)
            {
                MessageBoxEx.Show("异常：" + ex.Message);
                dtPO = null;
                return false;
            }
            dtPO = dtTemp.Copy();
            return true;
        }

        //获取供应商信息
        private string GetVendorInfo(string strponumber, out string vendorName)
        {
            string strSql = @"SELECT
	                        T1.VendorNumber,
	                        T1.VendorName
                        FROM
	                       PurchaseOrdersByCMF T1
                        WHERE
	                        T1.PONumber ='" + strponumber + "'";
            string vendornumber = string.Empty;
            DataTable dtTemp = SQLHelper.GetDataTable(GlobalSpace.FSDBConnstr, strSql);

            if (dtTemp.Rows.Count > 0)
            {
                vendornumber = dtTemp.Rows[0]["VendorNumber"].ToString();
                vendorName = dtTemp.Rows[0]["VendorName"].ToString();
            }
            else
            {
                vendornumber = "";
                vendorName = "";
            }
            return vendornumber;
        }

        //查询物料是否需要检验
        private List<string> GetItemInfo(string itemNumber)
        {
            List<string> list = new List<string>();
            DataTable dtTemp = null;
            string sqlSelect = @"Select  ItemDescription,ItemUM,IsInspectionRequired,PreferredStockroom,PreferredBin From _NoLock_FS_Item Where ItemNumber='" + itemNumber + "'";
            dtTemp = SQLHelper.GetDataTableOleDb(GlobalSpace.oledbconnstrFSDBMR, sqlSelect);
            if (dtTemp.Rows.Count > 0)
            {
                list.Add(dtTemp.Rows[0]["IsInspectionRequired"].ToString());
                list.Add(dtTemp.Rows[0]["PreferredStockroom"].ToString());
                list.Add(dtTemp.Rows[0]["PreferredBin"].ToString());
                list.Add(dtTemp.Rows[0]["ItemDescription"].ToString());
                list.Add(dtTemp.Rows[0]["ItemUM"].ToString());
            }
            return list;
        }

        //获得当前每一个物料的库管员
        private string GetItemStockKeeper(string itemNumber)
        {
            string itemKeeper = string.Empty;
            string sqlSelect = @"SELECT
	                                        (T1.UserID + '|' + T1.UserName) AS ItemKeeper
                                        FROM
	                                        _NoLock_FS_UserAccess T1,
	                                        _NoLock_FS_Item T2
                                        WHERE
	                                        T1.UserID = T2.ItemReference3
                                        AND T2.ItemNumber = '" + itemNumber + "'";
            DataTable dtTemp = SQLHelper.GetDataTableOleDb(GlobalSpace.oledbconnstrFSDBMR, sqlSelect);
            if (dtTemp.Rows.Count > 0)
            {
                itemKeeper = dtTemp.Rows[0]["ItemKeeper"].ToString();
            }
            return itemKeeper;
        }

        private void btnManageSpecialItem_Click(object sender, EventArgs e)
        {
            Purchase.FOSpecialItem si = new FOSpecialItem();
            si.ShowDialog();
        }

        private void btnUnPlaceOrderItemMaintain_Click(object sender, EventArgs e)
        {
            string sqlUpdate = @"Update PurchaseDepartmentForeignOrderItemByCMF Set Status = 3 where IsValid = 1 And Status = 1  And ItemNumber  IN (Select ItemNumber From PurchaseDepartmentForeignOrderItemNotInByCMF)";
            if(SQLHelper.ExecuteNonQuery(GlobalSpace.FSDBConnstr,sqlUpdate))
            {
                Custom.MsgEx("更新成功！");
            }
            else
            {
                Custom.MsgEx("更新失败！");
            }
            LoadFOItemDetail(UserID, 1, 1);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show(PurchaseUser.ItemReceiveType);
        }

        private void BtnSpecialRefresh_Click(object sender, EventArgs e)
        {
            string sqlSelect = @"Select ForeignOrderNumber AS 外贸单号, ItemNumber AS 物料代码,ItemDescription AS 物料描述,ItemUM AS 单位,VendorNumber AS 供应商码,VendorName AS 供应商名,PurchasePrice AS 价格,Quantity AS 采购数量,TotalAmount AS 总金额,SpecificationDescription AS 规格,Requirements AS 要求,Id,OperateDateTime AS 申请日期 From PurchaseDepartmentForeignOrderItemByCMF Where IsValid = 1 And Status = 3 And ItemNumber  IN (Select ItemNumber From PurchaseDepartmentForeignOrderItemNotInByCMF)   Order by Id DESC";
            dgvFOSpeItemsDetail.DataSource = SQLHelper.GetDataTable(GlobalSpace.FSDBConnstr, sqlSelect);
            //dgvFOSpeItemsDetail.Columns["Id"].Visible = false;
        }

        private void dgvFOSpeItemsDetail_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                dgvFOSpeItemsDetail["Check", e.RowIndex].Value = !Convert.ToBoolean(dgvFOSpeItemsDetail["Check", e.RowIndex].Value);
            }
        }

        private void BtnAllSelect_dgvSpe_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow DgvRow in dgvFOSpeItemsDetail.Rows)
            {
                DgvRow.Cells["Check"].Value = true;
            }
        }

        private void BtnAllnotSelect_dgvSpe_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow DgvRow in dgvFOSpeItemsDetail.Rows)
            {
                DgvRow.Cells["Check"].Value = false;
            }
            
        }

        private void BtnSpecialYixiada_Click(object sender, EventArgs e)
        {
            List<int> Lint = new List<int>();
            foreach (DataGridViewRow DgvRow in dgvFOSpeItemsDetail.Rows)
            {
                if (Convert.ToBoolean(DgvRow.Cells["Check"].Value) == true)
                {
                    Lint.Add(Convert.ToInt32(DgvRow.Cells["Id"].Value));
                }
            }
            if (Lint.Count == 0) { MessageBox.Show("未选择任何行"); return; }
            string sqlUpdate = @"Update PurchaseDepartmentForeignOrderItemByCMF Set Status = 2 where Id  IN (" + string.Join( ",",Lint)+")";
            if (SQLHelper.ExecuteNonQuery(GlobalSpace.FSDBConnstr, sqlUpdate))
            {
                Custom.MsgEx("更新成功！");
            }
            else
            {
                Custom.MsgEx("更新失败！");
            }
            BtnSpecialRefresh_Click(null,null);
        }

        private void BtnSpecialDelete_Click(object sender, EventArgs e)
        {
            List<int> Lint = new List<int>();
            foreach (DataGridViewRow DgvRow in dgvFOSpeItemsDetail.Rows)
            {
                if (Convert.ToBoolean(DgvRow.Cells["Check"].Value) == true)
                {
                    Lint.Add(Convert.ToInt32(DgvRow.Cells["Id"].Value));
                }
            }
            if (Lint.Count == 0) { MessageBox.Show("未选择任何行"); return; }
            string sqlUpdate = @"Update PurchaseDepartmentForeignOrderItemByCMF Set IsValid = 0 where Id  IN (" + string.Join(",", Lint) + ")";
            if (SQLHelper.ExecuteNonQuery(GlobalSpace.FSDBConnstr, sqlUpdate))
            {
                Custom.MsgEx("删除成功！");
            }
            else
            {
                Custom.MsgEx("删除失败！");
            }
            BtnSpecialRefresh_Click(null, null);
        }

        private void TbForeignOrderNumber_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                TbForeignOrderNumber.Text = TbForeignOrderNumber.Text.Trim().ToUpper();
                string sqlSelect = @"Select ForeignOrderNumber AS 外贸单号, ItemNumber AS 物料代码,ItemDescription AS 物料描述,ItemUM AS 单位,VendorNumber AS 供应商码,VendorName AS 供应商名,PurchasePrice AS 价格,Quantity AS 采购数量,TotalAmount AS 总金额,SpecificationDescription AS 规格,Requirements AS 要求,Id,OperateDateTime AS 申请日期 From PurchaseDepartmentForeignOrderItemByCMF Where IsValid = 1 And Status = 3 And  ForeignOrderNumber like '%"+ TbForeignOrderNumber.Text + "%' And ItemNumber  IN (Select ItemNumber From PurchaseDepartmentForeignOrderItemNotInByCMF)   Order by Id DESC";
                dgvFOSpeItemsDetail.DataSource = SQLHelper.GetDataTable(GlobalSpace.FSDBConnstr, sqlSelect);
            }
        }
    }

}

