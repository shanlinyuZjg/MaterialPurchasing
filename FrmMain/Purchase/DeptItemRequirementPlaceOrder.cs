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
using Global;
using Global.Helper;

namespace Global.Purchase
{
    public partial class DeptItemRequirementPlaceOrder : Office2007Form
    {
        int PONumberStartNumber = 0;
        int PONumberEndNumber = 0;
        string fsuserid = string.Empty;
        bool BBeyondRange = false;
        public DeptItemRequirementPlaceOrder(int poStartNumber,int poEndNumber,string userid)
        {
            this.EnableGlass = false;
            MessageBoxEx.EnableGlass = false;
            InitializeComponent();
            PONumberStartNumber = poStartNumber;
            PONumberEndNumber = poEndNumber;
            fsuserid = userid;
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            dgvDetail.DataSource = GetItemInfo();
            dgvDetail.Columns["Id"].Visible = false;
            dgvDetail.Columns["UniqueID"].Visible = false;

        }

        private DataTable GetItemInfo()
        {
            string sqlSelect = @"SELECT
	Id,
	ItemNumber AS 物料代码,
	ItemDescription AS 描述,
	UM AS 单位,
	RequireQuantity AS 需求数量,PricePreTax AS 含税价格,
	InspectStandard AS 检验标准,
	RequireDate AS 需求日期,
	RemarkOriginal AS 备注,
	AppointedVendor AS 指定供应商,
	VendorNumber AS 供应商码,
	VendorName AS 供应商名,
	UniqueID,
	Remark AS 采购备注
FROM
	dbo.PurchaseDepartmentDeptRequirement Where Status = 0";
            return SQLHelper.GetDataTable(GlobalSpace.FSDBConnstr, sqlSelect);
        }

        private void DeptItemRequirementPlaceOrder_Load(object sender, EventArgs e)
        {
            tbPOMiddle.Text = DateTime.Now.ToString("MMddyy");
            tbPOPostfix.GotFocus += new EventHandler(POPostFixGotFocus);
        }
        private void POPostFixGotFocus(object sender, EventArgs e)
        {
            if (rbtnAutomatic.Checked)
            {
                if (tbPOHeader.Text == "PJ" || tbPOHeader.Text == "PF")
                {
                    tbPOPostfix.Text = "";
                    return;
                }
                string seqNumber = GeneratePONumberSequenceNumber2(tbPOHeader.Text, fsuserid);

                if (seqNumber == "BeyondRange")
                {
                    //                Custom.MsgEx("订单序号超出范围！");
                    BBeyondRange = true;
                    return;
                }
                else
                {
                    tbPOPostfix.Text = seqNumber;
                    BBeyondRange = false;
                }
            }
        }
        private void tbPOHeader_TextChanged(object sender, EventArgs e)
        {
            tbPOHeader.Text = tbPOHeader.Text.ToUpper();
            tbPOHeader.SelectionStart = tbPOHeader.Text.Length;
        }

        private void tbPOHeader_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(!string.IsNullOrWhiteSpace(tbPOHeader.Text))
            {
                if(e.KeyChar == (char)13)
                {
                    tbPOPostfix.Focus();
                }
            }
        }

        private void tbPOPostfix_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void tbPOPostfix_Click(object sender, EventArgs e)
        {

        }

        private void btnPlaceOrder_Click(object sender, EventArgs e)
        {
            string latestSequenceNumber = string.Empty;
            if (tbPOHeader.Text == "PJ" ||  tbPOHeader.Text == "PF" || tbPOHeader.Text == "PP")
            {
                latestSequenceNumber = tbPOPostfix.Text;
            }
            else
            {
                if (rbtnAutomatic.Checked)
                {
                    latestSequenceNumber = GeneratePONumberSequenceNumber2(tbPOHeader.Text, fsuserid);
                }
                else
                {
                    latestSequenceNumber = tbPOPostfix.Text;
                }
            }
            if (latestSequenceNumber == "BeyondRange")
            {
                if (!BBeyondRange)
                {
                    Custom.MsgEx("订单号已超出范围！");
                    return;
                }
            }
            PlacePurchaseOrderNew(tbPOHeader.Text + "-" + tbPOMiddle.Text.Trim() + "-" + latestSequenceNumber);
        }

        //获取最新订单编号
        private string GeneratePONumberSequenceNumber2(string poType, string userID)
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
            if (poType == "PP")
            {
                sqlSelect = @"Select TOP 1 PONumber From PurchaseOrderRecordByCMF Where Buyer = '" + userID + "' And Left(PONumber,2) = '" + poType + "' And POItemPlacedDate = '" + DateTime.Now.ToString("yyyy-MM-dd") + "'  AND IsPurePO = 1 ORDER BY Id DESC";
                string sqlSelectFS = @" SELECT
	                            T1.PONumber
                            FROM
	                            _NoLock_FS_POHeader T1
                            WHERE                               
                                T1.Buyer ='" + fsuserid + "' AND T1.PONumber LIKE '%" + dateNow + "%'  ORDER BY T1.PONumber DESC";
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
            }
            else if (poType == "PM")
            {
                if (PurchaseUser.PONumberSequenceNumberRange == "1-50")
                {
                    sqlSelect = @"Select TOP 1 PONumber From PurchaseOrderRecordByCMF Where  Left(PONumber,2) = '" + poType + "' And POItemPlacedDate = '" + DateTime.Now.ToString("yyyy-MM-dd") + "'  AND IsPurePO = 1 And POTogether = 1 ORDER BY Id DESC";
                }
                else
                {
                    sqlSelect = @"Select TOP 1 PONumber From PurchaseOrderRecordByCMF Where Buyer = '" + userID + "' And Left(PONumber,2) = '" + poType + "' And POItemPlacedDate = '" + DateTime.Now.ToString("yyyy-MM-dd") + "'  AND IsPurePO = 1 ORDER BY Id DESC";
                }

                string sqlSelectFS = @" SELECT
	                            T1.PONumber
                            FROM
	                            _NoLock_FS_POHeader T1
                            WHERE                               
                                T1.Buyer ='" + fsuserid + "' And Left(PONumber,2) = '" + poType + "' AND T1.PONumber LIKE '%" + dateNow + "%'  ORDER BY T1.PONumber DESC";
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
            }
            else if (poType == "PA")
            {
                sqlSelect = @"Select TOP 1 PONumber From PurchaseOrderRecordByCMF Where  Left(PONumber,2) = '" + poType + "' And POItemPlacedDate = '" + DateTime.Now.ToString("yyyy-MM-dd") + "'  AND IsPurePO = 1 And POTogether = 1 ORDER BY Id DESC";


                string sqlSelectFS = @" SELECT
	                            T1.PONumber
                            FROM
	                            _NoLock_FS_POHeader T1
                            WHERE                               
                                T1.Buyer ='" + fsuserid + "' And Left(PONumber,2) = '" + poType + "' AND T1.PONumber LIKE '%" + dateNow + "%'  ORDER BY T1.PONumber DESC";
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
                    strReturn = PONumberStartNumber.ToString();
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
            }
            else
            {
                sqlSelect = @"Select TOP 1 PONumber From PurchaseOrderRecordByCMF Where Left(PONumber,2) = '" + poType + "' And POItemPlacedDate = '" + DateTime.Now.ToString("yyyy-MM-dd") + "'  AND IsPurePO = 1 ORDER BY Id DESC";
                DataTable dt = SQLHelper.GetDataTable(GlobalSpace.FSDBConnstr, sqlSelect);
                if (dt.Rows.Count == 0)
                {
                    strReturn = "001";
                }
                else
                {
                    string str = dt.Rows[0]["PONumber"].ToString().Substring(10, 3);
                    sequenceNumber = Convert.ToInt32(str) + 1;
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
            }

            return strReturn;
        }

        //下达订单
        private void PlacePurchaseOrderNew(string poNumber)
        {
            int iCount = 0;
            string vendorNumber = string.Empty;
            string vendorName = string.Empty;
            int uniqueId = 0;
            int id = 0;
            foreach(DataGridViewRow dgvr in dgvDetail.Rows)
            {
                if(Convert.ToBoolean(dgvr.Cells["Check"].Value))
                {
                    iCount++;
                    vendorNumber = dgvr.Cells["供应商码"].Value.ToString();
                    vendorName = dgvr.Cells["供应商名"].Value.ToString();
                    uniqueId = Convert.ToInt32(dgvr.Cells["UniqueID"].Value);
                    id = Convert.ToInt32(dgvr.Cells["Id"].Value);
                }
            }

            if (iCount > 1)
            {
                MessageBoxEx.Show("请选择一条记录进行处理！", "提示");
                return;
            }
            else if (iCount > 1)
            {
                MessageBoxEx.Show("一次只能处理一条记录！", "提示");
                return;
            }

            string strSqlExist = @"Select Id From PurchaseOrderRecordByCMF Where PONumber='" + poNumber + "'";
            string strSqlExistFromFS = @"Select Count(PONumber) From FSDBMR.dbo._NoLock_FS_POHeader Where PONumber='" + poNumber + "'";
            string sqlUpdateStatus = @"Update PurchaseDepartmentDeptRequirement Set Status = 1 Where Id="+id+"";
            if (SQLHelper.Exist(GlobalSpace.FSDBConnstr, strSqlExist) || SQLHelper.Exist(GlobalSpace.FSDBMRConnstr, strSqlExistFromFS))
            {
                MessageBoxEx.Show("订单号已存在！", "提示");
                return;
            }
            else
            {             
                try
                {
                    string strInsert = @"Insert Into PurchaseOrderRecordByCMF (PONumber,VendorNumber,VendorName,Buyer,Superior,IsPurePO,Guid,POTogether,GSID) Values (@PONumber,@VendorNumber,@VendorName,@Buyer,@Supervisor,@IsPurePO,@Guid,@POTogether,@GSID)";
                    int poTogether = 0;
                    if (PurchaseUser.POTogether == 1)
                    {
                        poTogether = 1;
                    }
                    SqlParameter[] sqlparams =
                    {
                        new SqlParameter("@PONumber",poNumber),
                        new SqlParameter("@VendorNumber",vendorNumber),
                        new SqlParameter("@VendorName",vendorName),
                        new SqlParameter("@Buyer",fsuserid),
                       new SqlParameter("@Supervisor",PurchaseUser.SupervisorID),
                       new SqlParameter("@IsPurePO",1),
                       new SqlParameter("@Guid",Guid.NewGuid().ToString("N")),
                       new SqlParameter("@POTogether",poTogether),new SqlParameter("@GSID",uniqueId)
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

        private void btnRequirementFinish_Click(object sender, EventArgs e)
        {
            List<string> sqlList = new List<string>();
            foreach(DataGridViewRow dgvr in dgvDetail.Rows)
            {
                if(Convert.ToBoolean(dgvr.Cells["Check"].Value))
                {
                    string sqlUpdate = @"Update PurchaseDepartmentDeptRequirement Set Status = 1,ConfirmedDateTime='"+DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")+"',Confirmer='"+fsuserid+"' Where Id=" + Convert.ToInt32(dgvr.Cells["Id"].Value)+"";
                    sqlList.Add(sqlUpdate);
                }
            }

            if(SQLHelper.BatchExecuteNonQuery(GlobalSpace.FSDBConnstr,sqlList))
            {
                MessageBoxEx.Show("确认成功！", "提示");
            }
            else
            {
                MessageBoxEx.Show("操作失败！", "提示");
            }

        }
    }
}
