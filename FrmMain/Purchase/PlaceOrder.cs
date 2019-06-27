using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using SoftBrands.FourthShift.Transaction;
using Global.Helper;
using System.Data.SqlClient;
using NPOI.SS.UserModel;
using NPOI.HSSF.UserModel;
using System.IO;
using NPOI.SS.Util;
using NPOI.XSSF.UserModel;

namespace Global.Purchase
{
    public partial class PlaceOrder : Office2007Form
    {
        string fsuserid = string.Empty;
        string fspassword = string.Empty;
        string fsusername = string.Empty;
        //此处UserID为该用户的直属领导代码
        string supervisorID = string.Empty;
        string VendorInfo = string.Empty;
        string strPromisedDateOld = string.Empty;
        string VendorNumberForFO = string.Empty;
        //订单列表
        List<string> poList = new List<string>();
        List<string> poErrorList = new List<string>();
        List<string> poToSuperiorList = new List<string>();
        List<string> iteminfoList = new List<string>();
        //字符集，此处用于供应商名字模糊查询供应商时使用
        Encoding GB2312 = Encoding.GetEncoding("gb2312");
        Encoding ISO88591 = Encoding.GetEncoding("iso-8859-1");

        public PlaceOrder()
        {
            InitializeComponent();
        }

        public PlaceOrder(string userid, string password, string name)
        {
            InitializeComponent();
            fsuserid = userid;
            fspassword = password;
            fsusername = name;
        }

        //获取上级领导代码
        private string GetSuperiorId(string struserid)
        {
            string supervisorID = string.Empty;
            string strSelect = @"Select SupervisorID from PurchaseDepartmentRBACByCMF where UserID='" + struserid + "'";
            DataTable dtTemp = SQLHelper.GetDataTable(GlobalSpace.FSDBConnstr, strSelect);
            if (dtTemp.Rows.Count > 0)
            {
                supervisorID = dtTemp.Rows[0]["SupervisorID"].ToString();
            }
            else
            {
                supervisorID = "NULL";
            }
            return supervisorID;
        }
        private void btnPlaceOrder_Click(object sender, EventArgs e)
        {
            PlacePurchaseOrder(tbPONumber.Text.Trim());
            tbPONumber.Text = "";
            tbVendorNumber.Text = "";
            cbbVendor.Text = "";
            tbVendorFuzzyName.Text = "";
        }

        //下达订单
        private void PlacePurchaseOrder(string poNumber)
        {
            string PONumber = string.Empty;
            string vendorId = string.Empty;
            string vendorName = string.Empty;
            string strSqlExist = @"Select Id From PurchaseOrdersByCMF Where PONumber='" + poNumber + "'";
            string strSqlExistFromFS = @"Select Count(PONumber) From FSDBMR.dbo._NoLock_FS_POHeader Where PONumber='" + poNumber + "'";
            if (SQLHelper.Exist(GlobalSpace.FSDBConnstr, strSqlExist) || SQLHelper.Exist(GlobalSpace.FSDBConnstr, strSqlExistFromFS))
            {
                MessageBoxEx.Show("订单号已存在！", "提示");
                return;
            }
            else
            {
                if (cbbVendor.Text != "")
                {
                    vendorId = cbbVendor.Text.Trim().Substring(0, 6);
                    vendorName = cbbVendor.Text.Trim().Substring(7);
                }
                else
                {
                    vendorId = tbVendorNumber.Text.Trim();
                    vendorName = GetVendorName(vendorId);
                    if (vendorName == "")
                    {
                        MessageBoxEx.Show("供应商不存在!");
                        return;
                    }
                }
                try
                {
                    string strInsert = @"Insert Into PurchaseOrdersByCMF (PONumber,VendorNumber,VendorName,Buyer,Supervisor) Values (@PONumber,@VendorNumber,@VendorName,@Buyer,@Supervisor)";
                    SqlParameter[] sqlparams =
                    {
                        new SqlParameter("@PONumber",poNumber),
                        new SqlParameter("@VendorNumber",vendorId),
                        new SqlParameter("@VendorName",vendorName),
                        new SqlParameter("@Buyer",fsuserid),
                       new SqlParameter("@Supervisor",supervisorID)
                    };
                    if (SQLHelper.ExecuteNonQuery(GlobalSpace.FSDBConnstr, strInsert, sqlparams))
                    {
                        MessageBoxEx.Show("订单下达成功！");
                        tbPONumber.Text = "";
                        tbVendorNumber.Text = "";
                        cbbVendor.Text = "";
                    }
                    else
                    {
                        MessageBoxEx.Show("订单下达失败！");
                        cbbVendor.Text = "";
                    }
                    ShowOrder("0");
                }
                catch (Exception ex)
                {
                    MessageBoxEx.Show("异常：" + ex.Message);
                }
            }

        }

        //获取供应商名字
        private string GetVendorName(string vendorId)
        {
            string strTemp = "";
            string strSelect = @"Select VendorName From _NoLock_FS_Vendor Where VendorID='" + vendorId + "'";
            try
            {
                DataTable dtTemp = SQLHelper.GetDataTableOleDb(GlobalSpace.oledbconnstrFSDBMR, strSelect);
                if (dtTemp.Rows.Count > 0)
                {
                    strTemp = dtTemp.Rows[0]["VendorName"].ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBoxEx.Show("异常：" + ex.Message);
            }

            return strTemp;
        }

        //将PurchaseOrdersByCMF表已审核订单写入四班
        private bool PlaceFSPurchaseOrder(string poNumber, string buyer, string vendorId)
        {
            POMT00 myPomt00 = new POMT00();
            myPomt00.Buyer.Value = buyer;
            myPomt00.PONumber.Value = poNumber;
            myPomt00.VendorID.Value = vendorId;
            try
            {
                if (FSFunctionLib.fstiClient.ProcessId(myPomt00, null))
                {
                    return true;
                }
                else
                {
                    MessageBoxEx.Show("四班中" + poNumber + "订单下达失败！", "提示");
                    FSTIError error = FSFunctionLib.fstiClient.TransactionError;
                    FSFunctionLib.FSErrorMsg("四班异常");
                    string sqlInsert2 = @"Insert Into FSErrorLogByCMF (FSErrorDescription) values ('" + error.Description + "')";
                    if (SQLHelper.ExecuteNonQuery(GlobalSpace.FSDBConnstr, sqlInsert2))
                    {

                    }
                    MessageBoxEx.Show("四班异常：" + error.Description, "提示");
                    return false;
                }
            }
            catch (Exception ex)
            {
                MessageBoxEx.Show("出现异常：" + ex.Message);
            }
            return false;
        }
        //显示订单
        private void ShowOrder(string strperiod)
        {
            string strDateNow = DateTime.Now.ToString("yyyy-MM-dd");
            string strWeekDate = DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd");
            string strMonthDate = DateTime.Now.AddMonths(-1).ToString("yyyy-MM-dd");
            DataTable dtTemp = null;
            try
            {
                switch (strperiod)
                {
                    case "0":
                        string strSelect = @" SELECT
	                                T1.PONumber ,
	                                T1.VendorNumber,
                                    T1.VendorName,
                                     (case T1.POStatus when  '0' then '已准备'
                                             when  '1' then '已提交'
                                             when  '2' then '已审核'
                                             when  '3' then '已下达' 
                                            when  '4' then '已到货' 
                                            when  '5' then '已收货' 
                                            when  '6' then '已入库' 
                                            when  '7' then '已开票' 
                                    end     
                                    ) as POStatus  
                                FROM
	                                PurchaseOrdersByCMF T1
                                WHERE                                   
                                    T1.Buyer ='" + fsuserid + "' AND T1.POCreatedDate ='" + strDateNow + "'";
                        dtTemp = SQLHelper.GetDataTable(GlobalSpace.FSDBConnstr, strSelect);
                        break;
                    case "Week":
                        string strWeekSelect = @"SELECT
	                                T1.PONumber,
	                                T1.VendorNumber,
                                    T1.VendorName,
                                    (case T1.POStatus when  '0' then '已准备'
                                              when  '1' then '已提交'
                                             when  '2' then '已审核'
                                             when  '3' then '已下达' 
                                            when  '4' then '已到货' 
                                            when  '5' then '已收货' 
                                            when  '6' then '已入库' 
                                            when  '7' then '已开票' 
                                    end     
                                    ) as POStatus 
                                FROM
	                                PurchaseOrdersByCMF T1
                                WHERE                                   
                                    T1.Buyer ='" + fsuserid + "' AND T1.POCreatedDate >='" + strWeekDate + "' AND T1.POCreatedDate <='" + strDateNow + "'";
                        dtTemp = SQLHelper.GetDataTable(GlobalSpace.FSDBConnstr, strWeekSelect);
                        break;
                    case "Month":
                        string strMonthSelect = @" SELECT
	                                T1.PONumber,
	                                T1.VendorNumber,
                                    T1.VendorName,
                                     (case T1.POStatus when  '0' then '已准备'
                                              when  '1' then '已提交'
                                             when  '2' then '已审核'
                                             when  '3' then '已下达' 
                                            when  '4' then '已到货' 
                                            when  '5' then '已收货' 
                                            when  '6' then '已入库' 
                                            when  '7' then '已开票' 
                                    end     
                                    ) as POStatus  
                                FROM
	                                PurchaseOrdersByCMF T1
                                WHERE                                   
                                    T1.Buyer ='" + fsuserid + "' AND T1.POCreatedDate >='" + strMonthDate + "' AND T1.POCreatedDate <='" + strDateNow + "'";
                        dtTemp = SQLHelper.GetDataTable(GlobalSpace.FSDBConnstr, strMonthSelect);
                        break;
                    default:
                        string strDateSelect = @" SELECT
	                                T1.PONumber,
	                                T1.VendorNumber,
                                    T1.VendorName
                                FROM
	                                _NoLock_FS_POHeader T1
                                WHERE
                                    T2.VendorID = T1.VendorID And
                                    T1.Buyer ='" + fsuserid + "' AND T1.POCreatedDate ='" + strperiod + "'";
                        dtTemp = SQLHelper.GetDataTable(GlobalSpace.FSDBConnstr, strDateSelect);
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBoxEx.Show("发生异常：" + ex.Message);
            }
            dgvPO.DataSource = dtTemp;
        }

        private void PlaceOrder_Load(object sender, EventArgs e)
        {
            ////dtp控件的显示方式为自定义的yyyy格式
            //this.dtpForInvoice.Format = DateTimePickerFormat.Custom;
            //this.dtpForInvoice.CustomFormat = "yyyy";
            tbYearForInvoice.Text = DateTime.Now.ToString("yyyy");

            supervisorID = GetSuperiorId(fsuserid);
            if (supervisorID == "")
            {
                MessageBoxEx.Show("当前登录账号没有关联上级领导代码，请联系管理员！电话61075");
                this.Enabled = false;
            }
            else
            {
                //      FSFunctionLib.FSConfigFileInitialize(fsTestconfigfilepath, fsuserid, fspassword);
                ShowOrder("0");
                //   }
                //弹出的对话框采用Office2007样式
                MessageBoxEx.EnableGlass = false;
                //订单用DataGridView控件中，选中一列的ReadOnly属性为false，这样可以手动进行选中和取消的操作
            }
        }

        private void PlaceOrder_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBoxEx.Show("确定退出么？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.OK)
            {
                if (this.Enabled)
                {
                    FSFunctionLib.FSExit();
                    e.Cancel = false;
                    this.Dispose();
                }
                else
                {
                    e.Cancel = false;
                    this.Dispose();
                }
            }
            else
            {
                e.Cancel = true;
            }
        }

        private void rbToday_CheckedChanged(object sender, EventArgs e)
        {
            ShowOrder("0");
        }

        private void rbWeek_CheckedChanged(object sender, EventArgs e)
        {
            ShowOrder("Week");
        }

        private void rbMonth_CheckedChanged(object sender, EventArgs e)
        {
            ShowOrder("Month");
        }

        private void tbVendorFuzzyName_KeyPress(object sender, KeyPressEventArgs e)
        {
            string str = string.Empty;
            str = ISO88591.GetString(GB2312.GetBytes(tbVendorFuzzyName.Text.ToString()));
            cbbVendor.Items.Clear();
            if (e.KeyChar == (char)13)
            {
                if (string.IsNullOrEmpty(tbVendorFuzzyName.Text.Trim()))
                {
                    MessageBoxEx.Show("查询内容不得为空！");
                }
                else
                {
                    string strSql = @"SELECT
	                                    (VendorID+'|'+
	                                    VendorName) AS VendorInfo
                                    FROM
	                                    _NoLock_FS_Vendor	                                    
                                    WHERE
	                                    VendorName like '%" + str + "%'";
                    DataTable dtTemp = SQLHelper.GetDataTableOleDb(GlobalSpace.oledbconnstrFSDBMR, strSql);
                    if (dtTemp.Rows.Count > 0)
                    {
                        // MessageBoxEx.Show("共有：" + dtTemp.Rows.Count + "条记录");
                        foreach (DataRow dr in dtTemp.Rows)
                        {
                            cbbVendor.Items.Add(dr["VendorInfo"].ToString());
                        }
                    }
                }
            }
        }

        private void tbPONumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                if (string.IsNullOrEmpty(tbPONumber.Text.ToString()))
                {
                    MessageBoxEx.Show("订单号不得为空！");
                }
                else
                {
                    btnPlaceOrder_Click(sender, e);
                }
            }
        }

        private void dgvPO_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                MessageBoxEx.Show("请选择有效的行！", "提示");
            }
            else
            {

                //MessageBoxEx.Show(e.RowIndex+"|"+e.ColumnIndex);

                if (e.ColumnIndex == 5)
                {
                    dgvPO.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = true;
                }
                else
                {
                    tbPONumberInDetail.Text = dgvPO["PONumber", e.RowIndex].Value.ToString();
                    VendorNumberForFO = dgvPO["VendorNumber", e.RowIndex].Value.ToString();
                    tbForeignNumber.Focus();
                }
            }
        }

        private void dgvPO_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            string strPO = string.Empty;
            if (e.RowIndex < 0)
            {
                MessageBoxEx.Show("请选择有效的行！", "提示");
            }
            else
            {
                strPO = dgvPO["PONumber", e.RowIndex].Value.ToString();
                VendorNumberForFO = dgvPO["VendorNumber", e.RowIndex].Value.ToString();
                tbPONumberInDetail.Text = strPO;
                tbItemNumber.Focus();
                ShowPOItemDetail(strPO);
            }
        }

        private void dgvPO_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            dgvPO_CellContentDoubleClick(sender, e);
        }
        private void ShowPOItemDetail(string ponumber)
        {
            string strSqlCheck = @"Select Id From  PurchaseOrdersByCMF Where PONumber='" + ponumber + "'";
            string strSql = @"SELECT
                                                T1.Id,
                                                T1.Guid,
                                                T1.LineNumber AS 行号,
	                                        	T1.ItemNumber AS 物料代码,
	                                            T1.ItemDescription AS 物料描述,
	                                            T1.LineUM AS 单位,
	                                            T1.LineType AS 类型,
	                                            T1.LineStatus AS 状态,
	                                            T1.UnitPrice AS 单价,
	                                            T1.POItemQuantity AS 订购数量,
	                                            T1.DemandDeliveryDate AS 需求日期,
	                                            T1.ForeignNumber AS 外贸单号,
                                                  (     case T1.POStatus
                                                        when  '0' then '已准备'
                                                         when  '1' then '已提交'
                                                         when  '2' then '已审核'
                                                         when  '3' then '已下达' 
                                                        when  '4' then '已到货' 
                                                        when  '5' then '已收货' 
                                                        when  '6' then '已入库' 
                                                        when  '7' then '已开票' 
                                                end     
                                                ) as 物料状态  
                                        FROM
	                                        PurchaseOrderRecordByCMF T1
                                        WHERE
	                                        T1.PONumber = '" + ponumber + "' And T1.POStatus <> 99";


            if (SQLHelper.Exist(GlobalSpace.FSDBConnstr, strSqlCheck))
            {
                dgvPOItemDetail.DataSource = SQLHelper.GetDataTable(GlobalSpace.FSDBConnstr, strSql);
                dgvPOItemDetail.Columns["Id"].Visible = false;
                dgvPOItemDetail.Columns["Guid"].Visible = false;
                dgvPOItemDetail.Columns["行号"].ReadOnly = true;
                dgvPOItemDetail.Columns["物料代码"].ReadOnly = true;
                dgvPOItemDetail.Columns["物料描述"].ReadOnly = true;
                dgvPOItemDetail.Columns["单位"].ReadOnly = true;
                dgvPOItemDetail.Columns["类型"].ReadOnly = true;
                dgvPOItemDetail.Columns["状态"].ReadOnly = true;
                dgvPOItemDetail.Columns["物料状态"].ReadOnly = true;

                dgvPOItemDetail.Columns["单价"].DefaultCellStyle.BackColor = System.Drawing.Color.Coral;
                dgvPOItemDetail.Columns["订购数量"].DefaultCellStyle.BackColor = System.Drawing.Color.Coral;
                dgvPOItemDetail.Columns["需求日期"].DefaultCellStyle.BackColor = System.Drawing.Color.Coral;
                dgvPOItemDetail.Columns["外贸单号"].DefaultCellStyle.BackColor = System.Drawing.Color.Coral;
            }
            else
            {
                //dgvPOItemDetail.DataSource = SQLHelper.GetDataTableOleDb(GlobalSpace.oledbconnstrFSDBTR, strSqlFS);
                Custom.MsgEx("该订单直接从四班下达，无法进行操作！");
            }
            //DGV控件滚到内容最底部

            if (dgvPOItemDetail.Rows.Count > 0)
            {
                dgvPOItemDetail.FirstDisplayedScrollingRowIndex = dgvPOItemDetail.Rows.Count - 1;
            }

        }

        private void tbItemNumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                if (iteminfoList.Count > 0)
                {
                    iteminfoList.Clear();
                }
                iteminfoList = GetItemInfo(tbItemNumber.Text.Trim());
                //查询该物料代码是否属于外贸物料，是的话自动弹出窗口进行选择
                if (tbForeignNumber.Text.Trim() != "")
                {
                    string sqlCheck = @"Select Count(Id) From PurchaseDepartmentForeignOrderItemByCMF Where ForeignOrderNumber='" + tbForeignNumber.Text.Trim() + "' And ItemNumber = '" + tbItemNumber.Text.Trim() + "' And IsValid = 1 And Status = 1";
                    if (SQLHelper.Exist(GlobalSpace.FSDBConnstr, sqlCheck))
                    {
                        Purchase.ForeignOrderItemChoose foic = new ForeignOrderItemChoose(VendorNumberForFO, tbForeignNumber.Text.Trim(), tbItemNumber.Text.Trim(), fsuserid);
                        foic.ShowDialog();
                        if (GlobalSpace.foItemInfoList != null)
                        {
                            tbItemNumber.Text = GlobalSpace.foItemInfoList[0];
                            tbItemDescription.Text = GlobalSpace.foItemInfoList[1];
                            tbUM.Text = GlobalSpace.foItemInfoList[2];
                            tbPricePreTax.Text = (Convert.ToDouble(GlobalSpace.foItemInfoList[3]) * 1.16).ToString();
                            tbPricePostTax.Text = GlobalSpace.foItemInfoList[3];
                            tbOrderQuantity.Text = GlobalSpace.foItemInfoList[4];
                            tbDeliveryDate.Focus();
                            GetVendorInfo(tbPONumberInDetail.Text.Trim(), this.cbbVendorList);
                            GetManufacturerInfo(tbItemNumber.Text.Trim(), tbPONumberInDetail.Text.Trim(), this.cbbManufacturerList);
                        }
                    }
                    else
                    {
                        if (iteminfoList.Count > 0)
                        {
                            if (e.KeyChar == (char)13)
                            {
                                if (string.IsNullOrEmpty(tbItemNumber.Text.ToString()))
                                {
                                    MessageBoxEx.Show("物料代码不得为空！");
                                }
                                else
                                {
                                    GetVendorInfo(tbPONumberInDetail.Text.Trim(), this.cbbVendorList);
                                    //GetItemInfo();
                                    tbItemDescription.Text = iteminfoList[3].ToString();
                                    tbUM.Text = iteminfoList[4].ToString();
                                    GetManufacturerInfo(tbItemNumber.Text.Trim(), tbPONumberInDetail.Text.Trim(), this.cbbManufacturerList);
                                    tbOrderQuantity.Focus();
                                }
                            }
                            else
                            {
                                MessageBoxEx.Show("未查到该物料代码信息！");
                            }
                        }
                    }

                }
                else
                {
                    if (iteminfoList.Count > 0)
                    {
                        if (string.IsNullOrEmpty(tbItemNumber.Text.ToString()))
                        {
                            MessageBoxEx.Show("物料代码不得为空！");
                        }
                        else
                        {
                            GetVendorInfo(tbPONumberInDetail.Text.Trim(), this.cbbVendorList);
                            //GetItemInfo();
                            tbItemDescription.Text = iteminfoList[3].ToString();
                            tbUM.Text = iteminfoList[4].ToString();
                            GetManufacturerInfo(tbItemNumber.Text.Trim(), tbPONumberInDetail.Text.Trim(), this.cbbManufacturerList);
                            tbOrderQuantity.Focus();
                        }
                    }
                }

            }

        }

        //获取供应商信息
        private void GetVendorInfo(string strponumber, ComboBox cbb)
        {
            string strSql = @"SELECT
	                        (T1.VendorNumber +'|'+
	                        T1.VendorName) AS VendorInfo
                        FROM
	                       PurchaseOrdersByCMF T1
                        WHERE
	                        T1.PONumber ='" + strponumber + "'";
            string vendorinfo = string.Empty;
            DataTable dtTemp = SQLHelper.GetDataTable(GlobalSpace.FSDBConnstr, strSql);

            if (dtTemp.Rows.Count > 0)
            {
                vendorinfo = dtTemp.Rows[0]["VendorInfo"].ToString();
                VendorInfo = vendorinfo;
            }
            cbb.Text = vendorinfo;
        }
        //获取生产商信息
        private void GetManufacturerInfo(string stritemnumber, string strponumber, ComboBox cbb)
        {
            string strSql = @"SELECT
	            (
		            T2.ManufacturerNumber + '|' + T2.ManufacturerName
	            ) AS MInfo
            FROM
	            PurchaseOrdersByCMF T1,
	            ItemManufacturerInfoByCMF T2
            WHERE	            
                    T1.PONumber = '" + strponumber + "'  AND T2.ItemNumber = '" + stritemnumber + "' AND T1.VendorNumber = T2.VendorNumber  COLLATE Chinese_PRC_CI_AI ";

            DataTable dtTemp = SQLHelper.GetDataTable(GlobalSpace.FSDBConnstr, strSql);
            int iMNumber = dtTemp.Rows.Count;

            if (iMNumber == 0)
            {
                cbb.Text = VendorInfo;
            }
            else if (iMNumber == 1)
            {
                cbb.Items.Add(VendorInfo);
                cbb.Items.Add(dtTemp.Rows[0]["Minfo"].ToString());
            }
            else
            {
                cbb.Items.Add(VendorInfo);
                for (int i = 0; i < iMNumber; i++)
                {
                    cbb.Items.Add(dtTemp.Rows[i]["Minfo"].ToString());
                }
            }
        }
        //获取物料信息
        private void GetItemInfo2(string stritemnumber)
        {
            string strSql = @"select
                              ItemDescription,
                              ItemUM
                              from _NoLock_FS_Item where   ItemNumber ='" + stritemnumber + "'";
            DataTable dtTemp = SQLHelper.GetDataTableOleDb(GlobalSpace.oledbconnstrFSDBMR, strSql);
            if (dtTemp.Rows.Count > 0)
            {
                tbItemDescription.Text = dtTemp.Rows[0]["ItemDescription"].ToString();
                tbUM.Text = dtTemp.Rows[0]["ItemUM"].ToString();
            }
            else
            {
                MessageBoxEx.Show("未查到该物料的信息！");
            }
        }

        private void tbOrderQuantity_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                if (string.IsNullOrEmpty(tbOrderQuantity.Text.ToString()))
                {
                    MessageBoxEx.Show("订购数量不得为空！");
                }
                else
                {
                    tbPricePreTax.Focus();
                }
            }
        }

        private void tbPricePreTax_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                if (string.IsNullOrEmpty(tbPricePreTax.Text.ToString()))
                {
                    MessageBoxEx.Show("税前价格不得为空！");
                }
                else
                {
                    double fPrice = Convert.ToDouble(tbPricePreTax.Text.ToString());
                    double fTaxRate = Convert.ToDouble(cbbTaxRate.SelectedItem.ToString());
                    string strResult = (fPrice / (1 + fTaxRate)).ToString();
                    bool b = strResult.Contains(".");
                    string[] sArray = { };
                    int ilength = 0;
                    string strResult2 = string.Empty;
                    if (b)
                    {
                        sArray = strResult.Split('.');
                        ilength = sArray[1].Length;
                        strResult2 = string.Empty;

                        if (ilength > 9)
                        {
                            strResult2 = sArray[1].Substring(0, 9);
                        }
                        else
                        {
                            strResult2 = sArray[1];
                        }
                        tbPricePostTax.Text = sArray[0] + "." + strResult2;
                    }
                    else
                    {
                        tbPricePostTax.Text = strResult;
                    }
                    tbPricePostTax.Focus();
                }
            }
        }

        private void tbPricePostTax_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                if (string.IsNullOrEmpty(tbPricePostTax.Text.ToString()))
                {
                    MessageBoxEx.Show("税后价格不得为空！");
                }
                else
                {
                    tbDeliveryDate.Focus();
                }
            }
        }

        private void tbDeliveryDate_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                if (string.IsNullOrEmpty(tbDeliveryDate.Text.ToString()))
                {
                    MessageBoxEx.Show("交货日期不得为空！");
                }
                else
                {
                    btnAddItemToPO.Focus();
                }
            }
        }

        private void tbForeignNumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                tbItemNumber.Focus();
            }
        }

        private void tbItemNumber_TextChanged(object sender, EventArgs e)
        {
            tbItemNumber.Text = tbItemNumber.Text.ToUpper();
            tbItemNumber.SelectionStart = tbItemNumber.Text.Length;//避免光标在输入字母的前面
        }

        private void btnAddItemToPO_Click(object sender, EventArgs e)
        {
            //查询符合已审核状态的该订单是否存在，如果订单号本身存在，但是为其他状态，也不可以下达订单
            string sqlCheckPOStatus = @"Select Count(Id) From PurchaseOrdersByCMF Where PONumber='" + tbPONumberInDetail.Text.Trim() + "' And POStatus < 2";
            if (SQLHelper.Exist(GlobalSpace.FSDBConnstr, sqlCheckPOStatus))
            {
                string itemKeeper = GetItemStockKeeper(tbItemNumber.Text.Trim());
                string strInsert = @"INSERT INTO PurchaseOrderRecordByCMF (
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
	                                                LineType,
	                                                LineStatus,
                                                    NeededDate,
                                                    PromisedDate,
                                                    POItemQuantity,
                                                    PricePreTax,
                                                    StockKeeper,
                                                    Stock,Bin,InspectionPeriod,Guid,TaxRate
                                                )
                                                VALUES
	                                                (
	                                                '" + tbPONumberInDetail.Text.Trim() + "',@VendorNumber, @VendorName, @ManufacturerNumber, @ManufacturerName, @ItemNumber, @ItemDescription,@LineUM, @Buyer,  @BuyerName,@Superior,@DemandDeliveryDate,@ForeignNumber, @POStatus,@ItemUsedPoint, @QualityCheckStandard,@UnitPrice,@LineType,@LineStatus,                                               @NeededDate,@PromisedDate, @POItemQuantity,@PricePreTax,@StockKeeper,@Stock,@Bin,@InspectionPeriod,@Guid,@TaxRate )";


                if (cbbManufacturerList.Text == "")
                {
                    MessageBoxEx.Show("该物料有多个生产商，请进行选择！");
                }
                else if (tbPONumberInDetail.Text == "" || tbItemNumber.Text == "" || tbOrderQuantity.Text == "" || tbPricePostTax.Text == "" || tbDeliveryDate.Text == "")
                {
                    MessageBoxEx.Show("订单号、物料代码、订购数量、税后价格和交货日期不能为空！", "提示");
                }
                else
                {
                    string strDate = tbDeliveryDate.Text.Trim();
                    string ponumber = tbPONumberInDetail.Text.Trim();
                    SqlParameter[] sqlparams =
                                {
                                    new SqlParameter("@PONumber",ponumber),
                                    new SqlParameter("@VendorNumber",cbbVendorList.Text.Substring(0,6)),
                                    new SqlParameter("@VendorName",cbbVendorList.Text.Substring(7)),
                                    new SqlParameter("@ManufacturerNumber",cbbManufacturerList.Text.Substring(0,6)),
                                    new SqlParameter("@ManufacturerName",cbbManufacturerList.Text.Substring(7)),
                                    new SqlParameter("@ItemNumber",tbItemNumber.Text.Trim()),
                                    new SqlParameter("@ItemDescription",tbItemDescription.Text.Trim()),
                                    new SqlParameter("@LineUM",tbUM.Text.Trim()),
                                    new SqlParameter("@Buyer",fsuserid),
                                    new SqlParameter("@BuyerName",fsusername),
                                    new SqlParameter("@Superior",supervisorID),
                                    new SqlParameter("@DemandDeliveryDate",strDate),
                                    new SqlParameter("@ForeignNumber",tbForeignNumber.Text.Trim()),
                                    new SqlParameter("@POStatus","0"),
                                    new SqlParameter("@ItemUsedPoint",tbItemUsedPoint.Text.Trim()),
                                    new SqlParameter("@QualityCheckStandard",tbQualityCheckStandard.Text.Trim()),
                                    new SqlParameter("@UnitPrice",Convert.ToDouble(tbPricePostTax.Text.Trim())),
                                    new SqlParameter("@LineType",tbLineType.Text.Trim().Substring(0,1)),
                                    new SqlParameter("@LineStatus",tbPOStatus.Text.Trim().Substring(0,1)),
                                    new SqlParameter("@NeededDate",strDate),
                                    new SqlParameter("@PromisedDate",strDate),
                                    new SqlParameter("@POItemQuantity",Convert.ToDouble(tbOrderQuantity.Text.Trim())),
                                    new SqlParameter("@PricePreTax",Convert.ToDouble(tbPricePreTax.Text.Trim())),
                                    new SqlParameter("@StockKeeper",itemKeeper.Trim()),
                                    new SqlParameter("@Stock",iteminfoList[1]),
                                    new SqlParameter("@Bin",iteminfoList[2]),
                                    new SqlParameter("@InspectionPeriod",iteminfoList[0]),
                                    new SqlParameter("@Guid",Guid.NewGuid().ToString("N")),
                                    new SqlParameter("@TaxRate",cbbTaxRate.SelectedItem.ToString())
                             };


                    if (SQLHelper.ExecuteNonQuery(GlobalSpace.FSDBConnstr, strInsert, sqlparams) )
                    {
                        MessageBoxEx.Show("物料添加成功", "提示");

                        //清空外贸物料用的list
                        if (GlobalSpace.foItemInfoList != null)
                        {
                            GlobalSpace.foItemInfoList.Clear();
                        }
                        //已经添加成功的外贸物料，更新其状态
                        if (tbForeignNumber.Text != "")
                        {
                            string sqlCheck = @"Select Count(Id) from PurchaseDepartmentForeignOrderItemByCMF where ForeignOrderNumber='" + tbForeignNumber.Text.Trim() + "' And ItemNumber = '" + tbItemNumber.Text.Trim() + "'";
                            if (SQLHelper.Exist(GlobalSpace.FSDBConnstr, sqlCheck))
                            {
                                string sqlUpdate = @"Update PurchaseDepartmentForeignOrderItemByCMF Set Status = 2 Where ForeignOrderNumber='" + tbForeignNumber.Text.Trim() + "' And ItemNumber = '" + tbItemNumber.Text.Trim() + "' And IsValid = 1";
                                if (SQLHelper.ExecuteNonQuery(GlobalSpace.FSDBConnstr, sqlUpdate) )
                                {

                                }
                                else
                                {
                                    MessageBoxEx.Show("更新外贸订单审核记录中物料状态失败，请联系管理员！", "提示");
                                }
                            }
                        }
                    }
                    else
                    {
                        MessageBoxEx.Show("物料添加失败", "提示");
                    }


                    //additemtopo(tbponumber.text.trim());
                    //showpoitemdetail(tbponumberindetail.text.trim());

                    if (cbbManufacturerList.Items.Count > 0)
                    {
                        cbbManufacturerList.Items.Clear();
                    }
                    if (cbbVendorList.Items.Count > 0)
                    {
                        cbbVendorList.Items.Clear();
                    }
                    ShowPOItemDetail(tbPONumberInDetail.Text.Trim());
                    tbPricePostTax.Text = "";
                    tbItemNumber.Text = "";
                    tbDeliveryDate.Text = "";
                    tbOrderQuantity.Text = "";
                    tbPricePreTax.Text = "";
                    tbItemDescription.Text = "";
                    tbUM.Text = "";
                    tbForeignNumber.Text = "";
                    tbItemUsedPoint.Text = "";
                    cbbManufacturerList.Text = "";
                    cbbVendorList.Text = "";
                    tbQualityCheckStandard.Text = "";
                    tbRemark.Text = "";
                }
            }
            else
            {
                MessageBoxEx.Show("该订单状态（非已准备或已提交）不允许下达物料！", "提示");
            }

        }
        //添加物料到订单
        private void AddItemToPO(string strponumber)
        {
            string strSql = @"SELECT
                                TOP 1
	                                T2.POLineNumberString
                                FROM
	                                _NoLock_FS_POHeader T1,
	                                _NoLock_FS_POLine T2
                                WHERE
	                                T1.POHeaderKey = T2.POHeaderKey
                                AND T1.PONumber ='" + strponumber + "'ORDER BY 	T2.POLineNumberString DESC";


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
        //四班中添加物料到订单里
        private bool FSAddItemToPO(string poNumber, DataRow dr)
        {

            POMT10 myPomt10 = new POMT10();
            myPomt10.PONumber.Value = poNumber;
            myPomt10.POLineUM.Value = dr["单位"].ToString();
            myPomt10.POLineType.Value = dr["类型"].ToString();
            myPomt10.POLineStatus.Value = dr["状态"].ToString();
            myPomt10.ItemNumber.Value = dr["物料代码"].ToString();
            myPomt10.PromisedDate.Value = dr["需求日期"].ToString();
            myPomt10.LineItemOrderedQuantity.Value = dr["订购数量"].ToString();
            myPomt10.NeededDate.Value = dr["需求日期"].ToString();
            myPomt10.ItemUnitCost.Value = dr["单价"].ToString();

            if (FSFunctionLib.fstiClient.ProcessId(myPomt10, null))
            {
                //     string linenumber = SQLHelper.GetItemValue(GlobalSpace.connstr, "POLineNumberString", strSql);
                //此处为写入自己的表的记录，需要好好实现
                return true;
            }
            FSFunctionLib.FSErrorMsg("物料添加到订单失败");
            return false;
        }

        private void btnSearchPO_Click(object sender, EventArgs e)
        {
            string strDate = this.dtpForPO.Value.ToString("yyyy-MM-dd");
            ShowOrder(strDate);
        }

        private void tbPONumber_TextChanged(object sender, EventArgs e)
        {
            tbPONumber.Text = tbPONumber.Text.ToUpper();
            tbPONumber.SelectionStart = tbPONumber.Text.Length;//避免光标在输入字母的前面
        }

        private void tbVendorNumber_TextChanged(object sender, EventArgs e)
        {
            tbVendorNumber.Text = tbVendorNumber.Text.ToUpper();
            tbVendorNumber.SelectionStart = tbVendorNumber.Text.Length;//避免光标在输入字母的前面
        }

        private void dgvPO_RowStateChanged(object sender, DataGridViewRowStateChangedEventArgs e)
        {
            e.Row.HeaderCell.Value = string.Format("{0}", e.Row.Index + 1);
        }

        private void btnMakeAllChecked_Click(object sender, EventArgs e)
        {
            if (dgvPO.Rows.Count > 0)
            {
                foreach (DataGridViewRow dgvr in dgvPO.Rows)
                {
                    dgvr.Cells["Checked"].Value = true;
                }
            }
        }

        private void btnCancelAllChecked_Click(object sender, EventArgs e)
        {
            if (dgvPO.Rows.Count > 0)
            {
                foreach (DataGridViewRow dgvr in dgvPO.Rows)
                {
                    dgvr.Cells["Checked"].Value = false;
                }
            }
        }

        private void btnSubmitPO_Click(object sender, EventArgs e)
        {
            //MessageBoxEx.Show("此处更新订单中物料状态的SQL语句不对，还需要增加行号的条件");
            //return;
            int icount = 0, jcount = 0;

            if (poToSuperiorList.Count > 0)
            {
                poToSuperiorList.Clear();
            }

            if (dgvPO.Rows.Count > 0)
            {
                try
                {
                    foreach (DataGridViewRow dgvr in dgvPO.Rows)
                    {
                        //订单状态已选中同时为已准备
                        if (Convert.ToBoolean(dgvr.Cells["Checked"].Value) == true && dgvr.Cells["POStatus"].Value.ToString() == "已准备")
                        {
                            poToSuperiorList.Add(dgvr.Cells["PONumber"].Value.ToString());
                        }
                    }

                    //MessageBoxEx.Show(poList.Count.ToString() + "条数据！");
                    //MessageBoxEx.Show(poList[0]);
                    if (poToSuperiorList.Count > 0)
                    {
                        for (int i = 0; i < poToSuperiorList.Count; i++)
                        {
                            //判断当前订单是否包含物料，如果没有的话，对订单不进行状态更新
                            string sqlselect = @"Select Count(Id) from PurchaseOrderRecordByCMF Where PONumber='" + poToSuperiorList[i] + "'";
                            if (SQLHelper.Exist(GlobalSpace.FSDBConnstr, sqlselect))
                            {
                                if (!BuyerUpdateStatus(poToSuperiorList[i], 1))
                                {
                                    icount++;
                                }
                                if (!BuyeUpdatePOItemStatus(poToSuperiorList[i], 1))
                                {
                                    jcount++;
                                }
                            }
                        }
                    }
                    else
                    {
                        MessageBoxEx.Show("当前没有可以进行提交的订单！", "提示");
                    }

                    if (icount > 0)
                    {
                        MessageBoxEx.Show("订单状态更新不完全,请确认");
                    }
                    if (jcount > 0)
                    {
                        MessageBoxEx.Show("订单中物料更新状态不完全,请确认");
                    }
                    if (icount == jcount && icount == 0)
                    {
                        MessageBoxEx.Show("订单提交成功！", "提示");
                    }
                }
                catch (Exception ex)
                {
                    MessageBoxEx.Show("异常：" + ex.Message);
                }
            }
            else
            {
                MessageBoxEx.Show("无可用的订单！");
            }
        }

        //采购员更新订单状态
        private bool BuyerUpdateStatus(string ponumber, int status)
        {
            string strUpdatePO = @" Update PurchaseOrdersByCMF Set POStatus = '" + status + "' Where PONumber='" + ponumber + "'";
            try
            {
                if (SQLHelper.ExecuteNonQuery(GlobalSpace.FSDBConnstr, strUpdatePO) )
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                MessageBoxEx.Show("异常：" + ex.Message, "提示");
            }
            return false;
        }
        //采购员更新订单中物料状态
        private bool BuyeUpdatePOItemStatus(string ponumber, int status)
        {
            string strUpdatePO = @" Update PurchaseOrderRecordByCMF Set POStatus = '" + status + "' Where PONumber='" + ponumber + "'";
            try
            {
                if (SQLHelper.ExecuteNonQuery(GlobalSpace.FSDBConnstr, strUpdatePO) )
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                MessageBoxEx.Show("异常：" + ex.Message, "提示");
            }
            return false;
        }
        //采购员直属领导审批后，更新订单状态
        private bool SuperiorUpdatePOStatus(string ponumber, int status)
        {
            string strUpdatePO = @" Update PurchaseOrdersByCMF Set POStatus = '" + status + "' Where PONumber='" + ponumber + "'";
            try
            {
                if (SQLHelper.ExecuteNonQuery(GlobalSpace.FSDBConnstr, strUpdatePO) )
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                MessageBoxEx.Show("异常：" + ex.Message, "提示");
            }
            return false;
        }
        //采购员直属领导审批后，更新订单中物料状态
        private bool SuperiorUpdatePOItemStatus(string ponumber, int status)
        {
            string strUpdatePO = @" Update PurchaseOrderRecordByCMF Set POStatus = '" + status + "' Where PONumber='" + ponumber + "'";
            try
            {
                if (SQLHelper.ExecuteNonQuery(GlobalSpace.FSDBConnstr, strUpdatePO) )
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                MessageBoxEx.Show("异常：" + ex.Message, "提示");
            }
            return false;
        }
        //库管员更新订单状态
        private bool WarehouseKeeperUpdatePOStatus(string ponumber, int status)
        {
            string strUpdatePO = @" Update PurchaseOrdersByCMF Set POStatus = '" + status + "' Where PONumber='" + ponumber + "'";
            try
            {
                if (SQLHelper.ExecuteNonQuery(GlobalSpace.FSDBConnstr, strUpdatePO) )
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                MessageBoxEx.Show("异常：" + ex.Message, "提示");
            }
            return false;
        }
        //库管员更新订单中物料状态
        private bool WarehouseKeeperUpdatePOItemStatus(string ponumber, int status)
        {
            string strUpdatePO = @" Update PurchaseOrderRecordByCMF Set POStatus = '" + status + "' Where PONumber='" + ponumber + "'";
            try
            {
                if (SQLHelper.ExecuteNonQuery(GlobalSpace.FSDBConnstr, strUpdatePO) )
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                MessageBoxEx.Show("异常：" + ex.Message, "提示");
            }
            return false;
        }
        private void btnWritePOToFS_Click(object sender, EventArgs e)
        {
            int icount = 0, jcount = 0;

            FSFunctionLib.FSConfigFileInitialize(GlobalSpace.fsconfigfilepath, fsuserid, fspassword);
            DataTable dtTemp = null;

            //清空poList中元素
            if (poList.Count > 0)
            {
                poList.Clear();
            }
            if (dgvPO.Rows.Count > 0)
            {
                try
                {
                    foreach (DataGridViewRow dgvr in dgvPO.Rows)
                    {
                        //订单状态已选中同时为领导已审核状态
                        if (Convert.ToBoolean(dgvr.Cells["Checked"].Value) == true && dgvr.Cells["POStatus"].Value.ToString() == "已审核")
                        {
                            poList.Add(dgvr.Cells["PONumber"].Value.ToString());
                            if (!IsPOExist(dgvr.Cells["PONumber"].Value.ToString()))
                            {
                                if (!PlaceFSPurchaseOrder(dgvr.Cells["PONumber"].Value.ToString(), fsuserid, dgvr.Cells["VendorNumber"].Value.ToString()))
                                {
                                    poErrorList.Add(dgvr.Cells["PONumber"].Value.ToString());
                                }
                                else
                                {
                                    string sqlUpdatePO = @"Update PurchaseOrdersByCMF Set POStatus = 3 Where PONumber='" + dgvr.Cells["PONumber"].Value.ToString() + "'";
                                    if (!SQLHelper.ExecuteNonQuery(GlobalSpace.FSDBConnstr, sqlUpdatePO))
                                    {
                                        MessageBoxEx.Show("订单状态更新失败，请联系管理员！", "提示");
                                    }
                                    else
                                    {

                                    }
                                    icount++;
                                }
                            }

                        }
                    }

                    if (poList.Count == 0)
                    {
                        MessageBoxEx.Show("无可用订单！", "提示");
                        return;
                    }
                    else
                    {
                        MessageBoxEx.Show("成功下达订单" + icount.ToString() + "个，失败" + poErrorList.Count.ToString() + "个!");
                        //遍历poList中所有订单信息，逐个按照订单信息进行物料添加
                        for (int i = 0; i < poList.Count; i++)
                        {
                            dtTemp = GetPODetail(poList[i]);//获取订单中的物料明细
                            if (dtTemp.Rows.Count > 0)
                            {
                                foreach (DataRow dr in dtTemp.Rows)
                                {
                                    if (FSAddItemToPO(poList[i], dr))
                                    {
                                        UpdatePOItemStatus(poList[i], dr);
                                        jcount++;
                                    }
                                }

                            }
                        }
                    }
                    if (jcount == dtTemp.Rows.Count)
                    {
                        MessageBoxEx.Show("所有物料下达成功！", "提示");
                    }
                }
                catch (Exception ex)
                {
                    MessageBoxEx.Show("异常：" + ex.Message);
                }

            }
            else
            {
                MessageBoxEx.Show("无订单！");
            }
            FSFunctionLib.FSExit();
        }

        //判断订单是否存在
        private bool IsPOExist(string ponumber)
        {
            string strSelect = @"Select VendorID From _NoLock_FS_POHeader Where PONumber = '" + ponumber + "'";
            if (SQLHelper.Exist(GlobalSpace.FSDBMRConnstr, strSelect))
            {
                return true;
            }
            return false;
        }
        //查询订单里的物料详情
        private DataTable GetPODetail(string ponumber)
        {
            string strSelect = @"SELECT
                                                T1.Id,
	                                        	T1.ItemNumber AS 物料代码,
	                                            T1.ItemDescription AS 物料描述,
	                                            T1.LineUM AS 单位,
	                                            T1.LineType AS 类型,
	                                            T1.LineStatus AS 状态,
	                                            T1.UnitPrice AS 单价,
	                                            T1.POItemQuantity AS 订购数量,
	                                            T1.DemandDeliveryDate AS 需求日期
                                        FROM
	                                        PurchaseOrderRecordByCMF T1
                                        WHERE
	                                        T1.PONumber = '" + ponumber + "' And T1.POStatus <> 99";
            return SQLHelper.GetDataTable(GlobalSpace.FSDBConnstr, strSelect);
        }

        //更新订单状态
        private bool UpdatePOStatus(string ponumber)
        {
            string strUpdate = @"Update PurchaseOrdersByCMF Set POStatus='2' Where PONumber='" + ponumber + "' ";
            if (SQLHelper.ExecuteNonQuery(GlobalSpace.FSDBConnstr, strUpdate) )
            {
                return true;
            }
            return false;
        }

        //更新订单中物料明细的状态
        private bool UpdatePOItemStatus(string ponumber, DataRow dr)
        {
            string lineNumberString = GetFSPOItemLineString(ponumber, dr["物料代码"].ToString());//查询物料在四班订单中下达后的行号
            if (string.IsNullOrEmpty(lineNumberString))
            {
                Custom.MsgEx("未查到该物料的行号，请联系管理员！");
            }

            string strUpdate = @"Update PurchaseOrderRecordByCMF Set POStatus= 3,LineNumber = '" + lineNumberString + "'  Where PONumber='" + ponumber + "' And ItemNumber='" + dr["物料代码"].ToString() + "' And  Id='" + dr["Id"].ToString() + "'";
            if (SQLHelper.ExecuteNonQuery(GlobalSpace.FSDBConnstr, strUpdate) )
            {
                return true;
            }
            return false;
        }

        //获取四班中订单中刚刚添加成功的物料的行号
        private string GetFSPOItemLineString(string poNumber, string itemNumber)
        {
            string lineNumberString = string.Empty;
            string sqlSelect = @"SELECT
	                                        T3.POLineNumberString
                                        FROM
	                                        _NoLock_FS_Item T1,
	                                        _NoLock_FS_POHeader T2,
	                                        _NoLock_FS_POLine T3
                                        WHERE
	                                        T1.ItemKey = T3.ItemKey
                                        AND
	                                        T3.POHeaderKey = T2.POHeaderKey
                                        AND
	                                        T1.ItemNumber = '" + itemNumber + "' AND T2.PONumber = '" + poNumber + "'  order by 	T3.POLineNumberString ASC";
            DataTable dtTemp = SQLHelper.GetDataTable(GlobalSpace.FSDBMRConnstr, sqlSelect);
            if (dtTemp.Rows.Count > 0)
            {
                //即使同一个订单中有多个相同的物料代码，最后一次下达的物料的必然在
                lineNumberString = dtTemp.Rows[dtTemp.Rows.Count - 1]["POLineNumberString"].ToString();
            }
            return lineNumberString;
        }
        private void btnCheckedByPassword_Click(object sender, EventArgs e)
        {
            bool isExist = false;
            foreach (DataGridViewRow dgvr in dgvPO.Rows)
            {
                //订单状态已选中同时为领导已审核状态
                if (Convert.ToBoolean(dgvr.Cells["Checked"].Value) == true)
                {
                    isExist = true;
                }
            }
            if (isExist)
            {
                List<string> sqlList = new List<string>();
                string sqlCheck = @"Select Id from PurchaseDepartmentCheckPasswordByCMF Where Supervisor ='" + supervisorID + "' And IsValid = 0";
                string sqlSelect = @"Select Password from PurchaseDepartmentCheckPasswordByCMF Where Supervisor ='" + supervisorID + "' And IsValid = 0";
                foreach (DataGridViewRow dgvr in dgvPO.Rows)
                {
                    //订单状态已选中同时为领导已审核状态
                    if (Convert.ToBoolean(dgvr.Cells["Checked"].Value) == true)
                    {
                        string sqlUpdatePOItem = @"Update PurchaseOrderRecordByCMF Set POStatus = 1, CheckedWay = 'Password'  Where PONumber='" + dgvr.Cells["PONumber"].Value.ToString() + "'";
                        string sqlUpdatePO = @"Update PurchaseOrdersByCMF Set POStatus = 1  Where PONumber='" + dgvr.Cells["PONumber"].Value.ToString() + "'";
                        sqlList.Add(sqlUpdatePOItem);
                        sqlList.Add(sqlUpdatePO);
                    }
                }

                if (SQLHelper.Exist(GlobalSpace.FSDBConnstr, sqlCheck))
                {
                    string password = SQLHelper.GetItemValue(GlobalSpace.FSDBConnstr, "Password", sqlSelect);
                    Purchase.CheckedByPassword pcbp = new CheckedByPassword(password, sqlList);
                    pcbp.Show();

                }
                else
                {
                    MessageBoxEx.Show("当前不存在有效密码！", "提示");
                }
            }
            else
            {
                MessageBoxEx.Show("没有选中的订单！", "提示");
            }


        }



        private void buttonX2_Click(object sender, EventArgs e)
        {
            //      MessageBox.Show(cbbManufacturerList.Text.ToString());
            /*
            POMT10 myPomt10 = new POMT10();
            myPomt10.PONumber.Value = poNumber;
            myPomt10.POLineUM.Value = dr["单位"].ToString();
            myPomt10.POLineType.Value = dr["类型"].ToString();
            myPomt10.POLineStatus.Value = dr["状态"].ToString();
            myPomt10.ItemNumber.Value = dr["物料代码"].ToString();
            myPomt10.PromisedDate.Value = dr["需求日期"].ToString();
            myPomt10.LineItemOrderedQuantity.Value = dr["订购数量"].ToString();
            myPomt10.NeededDate.Value = dr["需求日期"].ToString();
            myPomt10.ItemUnitCost.Value = dr["单价"].ToString();
            

            try
            {
                if (FSFunctionLib.fstiClient.ProcessId(myPomt10, null))
                {
                    //     string linenumber = SQLHelper.GetItemValue(GlobalSpace.connstr, "POLineNumberString", strSql);
                    //此处为写入自己的表的记录，需要好好实现

                }
                else
                {
                    FSFunctionLib.ErrorMsg("物料添加到订单失败");
                }
            }
            catch (Exception ex)
            {
                MessageBoxEx.Show("发生异常：" + ex.Message);
            }
            */
            //MessageBoxEx.Show(Convert.ToInt32("PA-010113-001").ToString());
        }

        private void btnRefreshPO_Click(object sender, EventArgs e)
        {
            ShowOrder("0");
        }

        private void btnMakeAllChecked2_Click(object sender, EventArgs e)
        {
            if (dgvPO2.Rows.Count > 0)
            {
                //foreach (DataGridViewRow dgvr in dgvPO2.Rows)
                //{
                //    dgvr.Cells["Choose"].Value = true;
                //}

                for (int i = 0; i < dgvPO2.Rows.Count - 1; i++)
                {
                    dgvPO2.Rows[i].Cells["Choose"].Value = true;
                }
            }
        }

        private void btnCancelAllChecked2_Click(object sender, EventArgs e)
        {
            if (dgvPO2.Rows.Count > 0)
            {
                foreach (DataGridViewRow dgvr in dgvPO2.Rows)
                {
                    dgvr.Cells["Choose"].Value = false;
                }
            }
        }

        private void btnSendVendorPOMail_Click(object sender, EventArgs e)
        {
            string filePath = string.Empty;

            //判断当前订单导出的Excel文件是否存在
            string sqlSelect = @"Select Name,FilePath From PurchaseDepartmentFilePathByCMF Where BuyerID='" + fsuserid + "' And Status = 0 And Name = '" + "采购订单导出路径" + "'";

            DataTable dtTemp = SQLHelper.GetDataTable(GlobalSpace.FSDBConnstr, sqlSelect);
            if (dtTemp.Rows.Count > 0)
            {
                filePath = dtTemp.Rows[0]["FilePath"].ToString();
            }

            if (cbbPO.Text != "")
            {
                if (File.Exists(filePath + "\\" + cbbPO.Text.Trim() + ".xlsx"))
                {
                    string sqlSelectUserInfo = @"Select Email,Password,Name From PurchaseDepartmentRBACByCMF Where UserID='" + fsuserid + "'";
                    string sqlSelectVendorInfo = @"Select VendorNumber From PurchaseOrdersByCMF Where PONumber='" + cbbPO.Text.Trim() + "'";
                    string vendorNumber = string.Empty;
                    DataTable dtVendorInfo = SQLHelper.GetDataTable(GlobalSpace.FSDBConnstr, sqlSelectVendorInfo);
                    if (dtVendorInfo.Rows.Count > 0)
                    {
                        vendorNumber = dtVendorInfo.Rows[0]["VendorNumber"].ToString();
                    }
                    else
                    {
                        MessageBoxEx.Show("未查到该供应商信息！", "提示");
                        return;
                    }

                    string sqlSelectVendorInfo2 = @"Select VendorName,Email From PurchaseDepartmentVendorEmailByCMF Where VendorNumber='" + vendorNumber + "'";
                    DataTable dtUserInfo = SQLHelper.GetDataTable(GlobalSpace.FSDBConnstr, sqlSelectUserInfo);
                    DataTable dtVendorInfo2 = SQLHelper.GetDataTable(GlobalSpace.FSDBConnstr, sqlSelectVendorInfo2);
                    string vendorEmail = string.Empty;
                    string vendorName = string.Empty;
                    if (dtVendorInfo2.Rows.Count > 0)
                    {
                        vendorEmail = dtVendorInfo2.Rows[0]["Email"].ToString();
                        vendorName = dtVendorInfo2.Rows[0]["VendorName"].ToString();
                    }
                    else
                    {
                        MessageBoxEx.Show("供应商邮箱未设置！", "提示");
                        return;
                    }
                    if (dtUserInfo.Rows.Count > 0)
                    {
                        if (dtUserInfo.Rows[0]["Email"] != DBNull.Value)
                        {
                            if (dtUserInfo.Rows[0]["Email"].ToString() != "")
                            {
                                List<string> smtpList = CommonOperate.GetSMTPServerInfo();
                                if (smtpList.Count > 0)
                                {
                                    Email email = new Email();
                                    email.fromEmail = dtUserInfo.Rows[0]["Email"].ToString();
                                    email.fromPerson = dtUserInfo.Rows[0]["Name"].ToString();
                                    email.toEmail = vendorEmail;
                                    email.toPerson = vendorName;
                                    email.encoding = "UTF-8";
                                    email.smtpServer = smtpList[0];
                                    email.userName = dtUserInfo.Rows[0]["Email"].ToString();
                                    email.passWord = CommonOperate.Base64Decrypt(dtUserInfo.Rows[0]["Password"].ToString());
                                    email.emailTitle = "瑞阳制药有限公司采购订单";
                                    email.emailContent = vendorName + "：" + "<br>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + "您好!<br>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;附件是瑞阳制药有限公司采购订单，请查收并及时配货，有问题及时联系！";

                                    if (MailHelper.SendEmailWithAttachment(email, (filePath + "\\" + cbbPO.Text.Trim() + ".xlsx")))
                                    {
                                        MessageBoxEx.Show("邮件发送成功！", "提示");
                                    }
                                    else
                                    {
                                        MessageBoxEx.Show("邮件发送失败！", "提示");
                                    }
                                }
                                else
                                {
                                    MessageBoxEx.Show("未设置SMTP服务器IP地址和端口，请联系管理员！", "提示");
                                }

                            }
                        }
                        else
                        {
                            MessageBoxEx.Show("自己的邮箱未设置！", "提示");
                        }
                    }
                }
                else
                {
                    MessageBoxEx.Show("没有导出该订单的文件，请先导出！", "提示");
                }
            }
            else
            {
                MessageBoxEx.Show("没有可用的订单号！", "提示");
            }
        }

        //获取用于导出Excel文件的订单详情
        private DataTable GetVendorPOItemDetailToExport()
        {
            return (DataTable)dgvPO2.DataSource;
        }

        private void btnSubmitPOToStockKeeper_Click(object sender, EventArgs e)
        {
            string poNumber = cbbPO.Text.Trim();
            List<string> sqlList = new List<string>();

            if (string.IsNullOrEmpty(poNumber))
            {
                MessageBoxEx.Show("采购单号不能为空！", "提示");
            }
            else
            {
                string strCheck = @" Select Count(Id) from PurchaseOrdersByCMF Where PONumber='" + poNumber + "' And POStatus >= 3";
                if (SQLHelper.Exist(GlobalSpace.FSDBConnstr, strCheck))
                {
                    foreach (DataGridViewRow dgvr in dgvPO2.Rows)
                    {
                        if (Convert.ToBoolean(dgvr.Cells["Choose"].Value) == true && dgvr.Cells["POStatus2"].Value.ToString() == "已下达")
                        {                         
                            string sqlUpdate = @"Update PurchaseOrderRecordByCMF Set POStatus = 4 Where Guid = '" + dgvr.Cells["GUID"].Value.ToString() + "' ";
                            sqlList.Add(sqlUpdate);
                        }             
                    }

                    if (SQLHelper.BatchExecuteNonQuery(GlobalSpace.FSDBConnstr, sqlList))
                    {
                        Custom.MsgEx("更新成功！");
                        dgvPO2.DataSource = GetVendorPOItemsDetail(cbbPO.Text.Trim());
                        dgvPO2.Columns["GUID"].Visible = false;
                    }
                    else
                    {
                        Custom.MsgEx("更新失败，联系管理员！");
                    }
                }
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
        //更新订单中物料的状态
        private bool UpdatePOItemStatus(DataGridView dgv, string poNumber, int status)
        {
            bool bSucceed = false;
            string lineNumber = string.Empty;
            string itemNumber = string.Empty;
            double ActualDeliveryQuantity = 0;
            SqlTransaction transaction = null;
            try
            {
                using (SqlConnection conn = new SqlConnection(GlobalSpace.FSDBConnstr))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        if (conn.State == ConnectionState.Closed)
                        {
                            conn.Open();
                        }
                        transaction = conn.BeginTransaction();
                        cmd.Transaction = transaction;
                        cmd.Connection = conn;

                        for (int i = 0; i <= dgv.Rows.Count - 1; i++)
                        {

                            //此处更新状态的物料为物料行已选中同时实际入库量不能为0,物料的状态必须为已下达
                            if (Convert.ToBoolean(dgv.Rows[i].Cells["Choose"].Value) == true && Convert.ToDouble(dgv.Rows[i].Cells["ActualDeliveryQuantity"].Value) != 0 && dgv.Rows[i].Cells["POStatus2"].Value.ToString() == "已下达")
                            {
                                lineNumber = dgv.Rows[i].Cells["LineNumber"].Value.ToString();
                                itemNumber = dgv.Rows[i].Cells["ItemNumber"].Value.ToString();
                                ActualDeliveryQuantity = Convert.ToDouble(dgv.Rows[i].Cells["ActualDeliveryQuantity"].Value);

                                string strUpdatePOItem = @"Update PurchaseOrderRecordByCMF Set POStatus = " + status + ",ActualDeliveryQuantity= '" + ActualDeliveryQuantity + "' Where PONumber='" + poNumber + "' And ItemNumber='" + itemNumber + "' And LineNumber='" + lineNumber + "' And POStatus = 3";

                                cmd.CommandType = CommandType.Text;
                                cmd.CommandText = strUpdatePOItem;
                                if (cmd.ExecuteNonQuery() < 0)
                                {
                                    throw new Exception();
                                }
                            }
                        }
                        transaction.Commit();
                        bSucceed = true;
                    }
                }
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                MessageBoxEx.Show("异常：" + ex.Message, "提示");
            }

            return bSucceed;
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
        private void btnSendVendorInvoiceMail_Click(object sender, EventArgs e)
        {

        }

        private void btnSearchPO2_Click(object sender, EventArgs e)
        {
            if (tbVendorNumber2.Text == "" && cbbPO.Text == "")
            {
                MessageBoxEx.Show("查询条件不能为空！", "提示");
            }
            else if (tbVendorNumber2.Text != "" && cbbPO.Text == "")
            {
                if (cbbPO.Items.Count > 0)
                {
                    cbbPO.Items.Clear();
                }
                DataTable dtTemp = GetVendorAllPO(tbVendorNumber2.Text.Trim(), fsuserid);
                foreach (DataRow dr in dtTemp.Rows)
                {
                    cbbPO.Items.Add(dr["PONumber"]);
                }
            }
            else if (tbVendorNumber2.Text == "" && cbbPO.Text != "")
            {

                if (GetVendorPOItemsDetail(cbbPO.Text.Trim()) != null)
                {
                    dgvPO2.DataSource = GetVendorPOItemsDetail(cbbPO.Text.Trim());
                    dgvPO2.Columns["GUID"].Visible = false;
                }
                else
                {
                    DataTable dtTemp2 = (DataTable)dgvPO2.DataSource;
                    if (dtTemp2 != null)
                    {
                        dtTemp2.Rows.Clear();
                    }
                    dgvPO2.DataSource = dtTemp2;
                }
            }
            else
            {
                MessageBoxEx.Show("本次查询仅以订单号为查询条件进行查询！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                if (GetVendorPOItemsDetail(cbbPO.Text.Trim()) != null)
                {
                    dgvPO2.DataSource = GetVendorPOItemsDetail(cbbPO.Text.Trim());
                    dgvPO2.Columns["GUID"].Visible = false;
                }
                else
                {
                    DataTable dtTemp2 = (DataTable)dgvPO2.DataSource;
                    dtTemp2.Rows.Clear();
                    dgvPO2.DataSource = dtTemp2;
                }

            }
        }

        //通过订单号获取订单中物料详情
        private DataTable GetVendorPOItemsDetail(string ponumber)
        {
            DataTable dtTemp = null;
            string strSqlCheck = @"Select Id From  PurchaseOrdersByCMF Where PONumber='" + ponumber + "'";
            string strSql = @"SELECT
                                                T1.Guid AS GUID,
                                                T1.LineNumber AS LineNumber,
	                                        	T1.ItemNumber AS ItemNumber,
	                                            T1.ItemDescription AS ItemDescription,
	                                            T1.LineUM AS ItemUM,	                                         
	                                            T1.UnitPrice AS UnitPrice,
	                                            T1.POItemQuantity AS OrderQuantity,
	                                            T1.DemandDeliveryDate AS DemandDate,
	                                            T1.ForeignNumber AS ForeignNumber,
                                                T1.ActualDeliveryQuantity AS ActualDeliveryQuantity,
                                                (T1.UnitPrice*T1.POItemQuantity) As ItemSum,
                                                T1.StockKeeper,
                                                (case T1.POStatus when  '1' then '已提交'
                                                         when  '2' then '已审核'
                                                         when  '3' then '已下达' 
                                                        when  '4' then '已到货' 
                                                        when  '5' then '已收货' 
                                                        when  '6' then '已入库' 
                                                        when  '7' then '已开票' 
                                                 end     
                                                ) as POStatus  
                                        FROM
	                                        PurchaseOrderRecordByCMF T1
                                        WHERE
	                                        T1.PONumber = '" + ponumber + "' And T1.POStatus <> 99";

            if (SQLHelper.Exist(GlobalSpace.FSDBConnstr, strSqlCheck))
            {

                dtTemp = SQLHelper.GetDataTable(GlobalSpace.FSDBConnstr, strSql);
                DataRow drNew = dtTemp.NewRow();
                drNew[0] = "合计";
                /*如果还有其他列需要计算求和，可以进行遍历或者单独计算
                for(int i = 1; i < dtTemp.Columns.Count; i++)
                {

                }*/
                drNew[dtTemp.Columns.Count - 3] = dtTemp.Compute(string.Format("SUM({0})", dtTemp.Columns[dtTemp.Columns.Count - 3]), "true");
                dtTemp.Rows.Add(drNew);
            }
            else
            {
                MessageBoxEx.Show("该订单不存在！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            return dtTemp;
        }

        private bool IsVendorPOExist(string ponumber, string userid)
        {
            string strSqlCheck = @"Select Id From  PurchaseOrdersByCMF Where PONumber='" + ponumber + "' And Buyer='" + userid + "'";
            if (SQLHelper.Exist(GlobalSpace.FSDBConnstr, strSqlCheck))
            {
                return true;
            }
            return false;
        }

        //通过供应商代码获取该供应商所有订单，按照日期降序排序
        private DataTable GetVendorAllPO(string ponumber, string userid)
        {
            string strSelect = @"Select PONumber From PurchaseOrdersByCMF Where VendorNumber='" + ponumber + "' And Buyer='" + userid + "'";
            return SQLHelper.GetDataTable(GlobalSpace.FSDBConnstr, strSelect);
        }
        //通过获取订单编号该供应商所有订单信息，按照日期降序排序
        private DataTable GetVendorAllPOForInvoiceByPONumber(string ponumber, string userid)
        {
            string strSelect = @" SELECT
	                                T1.PONumber ,
	                                T1.VendorNumber,
                                    T1.VendorName,
                                    T1.POStatus 
                                FROM
	                                PurchaseOrdersByCMF T1
                                WHERE                                   
                                    T1.Buyer ='" + userid + "' AND T1.PONumber ='" + ponumber + "'";
            return SQLHelper.GetDataTable(GlobalSpace.FSDBConnstr, strSelect);
        }
        //通过供应商代码获取该供应商所有订单信息，按照日期降序排序
        private DataTable GetVendorAllPOForInvoiceByVendorNumber(string vendornumber, string userid)
        {
            string strSelect = @" SELECT
	                                T1.PONumber ,
	                                T1.VendorNumber,
                                    T1.VendorName,
                                    T1.POStatus 
                                FROM
	                                PurchaseOrdersByCMF T1
                                WHERE                                   
                                    T1.Buyer ='" + userid + "' AND T1.VendorNumber ='" + vendornumber + "'";
            return SQLHelper.GetDataTable(GlobalSpace.FSDBConnstr, strSelect);
        }
        private void tbddPONumber_TextChanged(object sender, EventArgs e)
        {
            cbbPO.Text = cbbPO.Text.ToUpper();
            cbbPO.SelectionStart = cbbPO.Text.Length;//避免光标在输入字母的前面
        }

        private void tbVendorNumber2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                btnSearchPO2_Click(sender, e);
            }
        }


        private void cbbPO_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnSearchPO2_Click(sender, e);
            }
        }

        private void cbbPO_TextChanged(object sender, EventArgs e)
        {
            cbbPO.Text = cbbPO.Text.ToUpper();
            cbbPO.SelectionStart = cbbPO.Text.Length;//避免光标在输入字母的前面
        }

        private void btnExportToExcel_Click(object sender, EventArgs e)
        {
            string sqlSelect = @"Select Name,FilePath From PurchaseDepartmentFilePathByCMF Where BuyerID='" + fsuserid + "' And Status = 0 And Name = '" + "采购订单导出路径" + "'";
            DataTable dtTemp = SQLHelper.GetDataTable(GlobalSpace.FSDBConnstr, sqlSelect);
            if (dtTemp.Rows.Count > 0)
            {
                string poNumber = string.Empty;
                string filePath = dtTemp.Rows[0]["FilePath"].ToString();

                //获取通过订单查询供应商代码和名称
                if (tbVendorNumber2.Text == "" && cbbPO.Text != "")
                {
                    poNumber = cbbPO.Text;
                }
                else if (tbVendorNumber2.Text != "" && cbbPO.Text != "")
                {
                    poNumber = cbbPO.SelectedItem.ToString();
                }

                if (File.Exists(filePath + "\\" + poNumber + ".xlsx"))
                {
                    if (MessageBoxEx.Show("该订单的导出文件已经存在，确定要覆盖么？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.OK)
                    {
                        DataRow dr3 = GetVendorIDAndName(poNumber);
                        string vendorNumber = dr3["VendorNumber"].ToString();
                        string vendorName = dr3["VendorName"].ToString();

                        if (dgvPO2.Rows.Count > 0)
                        {
                            //由于Dgv控件数据源中有多个不需要的列，此处新建表只构造需要的列

                            DataTable dtOrigin = (DataTable)dgvPO2.DataSource;
                            DataTable dtNew = new DataTable();
                            dtNew.Columns.Add(dtOrigin.Columns["ItemNumber"].Caption, typeof(string));
                            dtNew.Columns.Add(dtOrigin.Columns["ItemDescription"].Caption, typeof(string));
                            dtNew.Columns.Add(dtOrigin.Columns["ItemUM"].Caption, typeof(string));
                            dtNew.Columns.Add(dtOrigin.Columns["UnitPrice"].Caption, typeof(double));
                            dtNew.Columns.Add(dtOrigin.Columns["OrderQuantity"].Caption, typeof(double));
                            dtNew.Columns.Add(dtOrigin.Columns["ItemSum"].Caption, typeof(double));
                            dtNew.Columns.Add(dtOrigin.Columns["DemandDate"].Caption, typeof(string));
                            dtNew.Columns.Add(dtOrigin.Columns["ForeignNumber"].Caption, typeof(string));

                            DataRow dr2 = null;
                            foreach (DataRow dr in dtOrigin.Rows)
                            {
                                dr2 = dtNew.NewRow();
                                dr2["ItemNumber"] = dr["ItemNumber"];
                                dr2["ItemDescription"] = dr["ItemDescription"];
                                dr2["ItemUM"] = dr["ItemUM"];
                                dr2["UnitPrice"] = dr["UnitPrice"];
                                dr2["OrderQuantity"] = dr["OrderQuantity"];
                                dr2["ItemSum"] = dr["ItemSum"];
                                dr2["DemandDate"] = dr["DemandDate"];
                                dr2["ForeignNumber"] = dr["ForeignNumber"];

                                dtNew.Rows.Add(dr2);
                            }

                            if (ExportVendorPOItemDetailToExcel(filePath, dtNew, poNumber, vendorNumber, vendorName))
                            {
                                MessageBoxEx.Show("导出成功！", "提示");
                            }
                            else
                            {
                                MessageBoxEx.Show("导出失败，请联系管理员查找原因！", "提示");
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
                    DataRow dr3 = GetVendorIDAndName(poNumber);
                    string vendorNumber = dr3["VendorNumber"].ToString();
                    string vendorName = dr3["VendorName"].ToString();

                    if (dgvPO2.Rows.Count > 0)
                    {
                        //由于Dgv控件数据源中有多个不需要的列，此处新建表只构造需要的列

                        DataTable dtOrigin = (DataTable)dgvPO2.DataSource;
                        DataTable dtNew = new DataTable();
                        dtNew.Columns.Add(dtOrigin.Columns["ItemNumber"].Caption, typeof(string));
                        dtNew.Columns.Add(dtOrigin.Columns["ItemDescription"].Caption, typeof(string));
                        dtNew.Columns.Add(dtOrigin.Columns["ItemUM"].Caption, typeof(string));
                        dtNew.Columns.Add(dtOrigin.Columns["UnitPrice"].Caption, typeof(double));
                        dtNew.Columns.Add(dtOrigin.Columns["OrderQuantity"].Caption, typeof(double));
                        dtNew.Columns.Add(dtOrigin.Columns["ItemSum"].Caption, typeof(double));
                        dtNew.Columns.Add(dtOrigin.Columns["DemandDate"].Caption, typeof(string));
                        dtNew.Columns.Add(dtOrigin.Columns["ForeignNumber"].Caption, typeof(string));

                        DataRow dr2 = null;
                        foreach (DataRow dr in dtOrigin.Rows)
                        {
                            dr2 = dtNew.NewRow();
                            dr2["ItemNumber"] = dr["ItemNumber"];
                            dr2["ItemDescription"] = dr["ItemDescription"];
                            dr2["ItemUM"] = dr["ItemUM"];
                            dr2["UnitPrice"] = dr["UnitPrice"];
                            dr2["OrderQuantity"] = dr["OrderQuantity"];
                            dr2["ItemSum"] = dr["ItemSum"];
                            dr2["DemandDate"] = dr["DemandDate"];
                            dr2["ForeignNumber"] = dr["ForeignNumber"];

                            dtNew.Rows.Add(dr2);
                        }

                        if (ExportVendorPOItemDetailToExcel(filePath, dtNew, poNumber, vendorNumber, vendorName))
                        {
                            MessageBoxEx.Show("导出成功！", "提示");
                        }
                        else
                        {
                            MessageBoxEx.Show("导出失败，请联系管理员查找原因！", "提示");
                        }
                    }
                }
            }
            else
            {
                MessageBoxEx.Show("没有导出文件保存路径，请先设置保存目录！", "提示");
            }

        }

        //导出订单详情Excel文件
        private bool ExportVendorPOItemDetailToExcel(string filePath, DataTable dt, string ponumber, string vendornumber, string vendorname)
        {
            bool bSucceed = false;
            try
            {
                //    IWorkbook workbook = new HSSFWorkbook();//声明工作簿对象，创建xls格式Excel文件
                IWorkbook workbook = new XSSFWorkbook();//声明工作簿对象，创建xlsx格式Excel文件
                ISheet sheet1 = workbook.CreateSheet(ponumber); //创建工作表               
                ICell sheet1Title = sheet1.CreateRow(0).CreateCell(0); //创建第一行第一个单元格
                sheet1Title.SetCellValue("瑞阳制药有限公司采购订单"); //表头
                sheet1Title.CellStyle = ExcelHelper.GetTitleCellStyle(workbook);
                sheet1.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(0, 0, 0, 7));  //合并单元格
                IRow row, row2;
                ICell cell;
                ICellStyle cellStyle1 = ExcelHelper.GetCellStyle(workbook, 0);
                ICellStyle cellStyle2 = ExcelHelper.GetCellStyle(workbook, 0);

                //创建供应商信息单元格（包括供应商代码，名称，和单号）
                row = sheet1.CreateRow(1);
                cell = row.CreateCell(0);
                cell.SetCellValue(ponumber + "    " + vendornumber + "    " + vendorname);
                cell.CellStyle = cellStyle1;


                cell = row.CreateCell(1);
                cell.SetCellValue("");
                cell.CellStyle = cellStyle1;

                cell = row.CreateCell(2);
                cell.SetCellValue("");
                cell.CellStyle = cellStyle1;

                cell = row.CreateCell(3);
                cell.SetCellValue("");
                cell.CellStyle = cellStyle1;

                cell = row.CreateCell(4);
                cell.SetCellValue("");
                cell.CellStyle = cellStyle1;

                cell = row.CreateCell(5);
                cell.SetCellValue("");
                cell.CellStyle = cellStyle1;

                cell = row.CreateCell(6);
                cell.SetCellValue("");
                cell.CellStyle = cellStyle1;

                cell = row.CreateCell(7);
                cell.SetCellValue("");
                cell.CellStyle = cellStyle1;

                CellRangeAddress region = new CellRangeAddress(1, 1, 0, 7);
                sheet1.AddMergedRegion(region);

                //表头数据。此处由于前边设置问题，导致列标题为英文，所以手动设定为中文
                row2 = sheet1.CreateRow(2);
                cell = row2.CreateCell(0);
                cell.SetCellValue("物料代码");
                cell.CellStyle = cellStyle1;


                cell = row2.CreateCell(1);
                cell.SetCellValue("物料描述");
                cell.CellStyle = cellStyle1;

                cell = row2.CreateCell(2);
                cell.SetCellValue("单位");
                cell.CellStyle = cellStyle1;

                cell = row2.CreateCell(3);
                cell.SetCellValue("单价");
                cell.CellStyle = cellStyle1;

                cell = row2.CreateCell(4);
                cell.SetCellValue("订购数量");
                cell.CellStyle = cellStyle1;

                cell = row2.CreateCell(5);
                cell.SetCellValue("合计");
                cell.CellStyle = cellStyle1;

                cell = row2.CreateCell(6);
                cell.SetCellValue("需求日期");
                cell.CellStyle = cellStyle1;

                cell = row2.CreateCell(7);
                cell.SetCellValue("外贸单号");
                cell.CellStyle = cellStyle1;

                // 写入数据
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataRow dataR = dt.Rows[i];
                    row = sheet1.CreateRow(i + 3);
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        sheet1.AutoSizeColumn(j);//从第0列开始，设置第j列的宽度为自适应宽度
                        if (dataR[j] == DBNull.Value)
                        {
                            cell = row.CreateCell(j);
                            cell.SetCellValue("");
                            cell.CellStyle = cellStyle2;
                        }
                        else
                        {
                            cell = row.CreateCell(j);
                            cell.SetCellValue(dataR[j].ToString());
                            cell.CellStyle = cellStyle2;
                        }
                    }

                    /*

                    cell = row.CreateCell(0);
                    cell.SetCellValue(dataR["ItemNumber"].ToString());
                    cell.CellStyle = cellStyle2;

                    cell = row.CreateCell(1);
                    cell.SetCellValue(dataR["ItemDescription"].ToString());
                    cell.CellStyle = cellStyle2;

                    cell = row.CreateCell(2);
                    cell.SetCellValue(dataR["ItemUM"].ToString());
                    cell.CellStyle = cellStyle2;

                    cell = row.CreateCell(3);
                    cell.SetCellValue((Double)dataR["UnitPrice"]);
                    cell.CellStyle = cellStyle2;

                    cell = row.CreateCell(4);
                    cell.SetCellValue((Double)dataR["OrderQuantity"]);
                    cell.CellStyle = cellStyle2;

                    cell = row.CreateCell(5);
                    cell.SetCellValue((Double)dataR["ItemSum"]);
                    cell.CellStyle = cellStyle2;

                    cell = row.CreateCell(6);
                    cell.SetCellValue(dataR["DemandDate"].ToString());
                    cell.CellStyle = cellStyle2;

                    cell = row.CreateCell(7);
                    cell.SetCellValue(dataR["ForeignNumber"].ToString());
                    cell.CellStyle = cellStyle2;
                    */

                }

                if (!Directory.Exists(filePath))  //检查是否存在文件夹，不存在则新建
                {
                    Directory.CreateDirectory(filePath);
                }

                FileStream file = new FileStream(filePath + "\\" + ponumber + ".xlsx", FileMode.Create);
                workbook.Write(file);
                file.Close();
                workbook.Close();
                bSucceed = true;
            }
            catch (Exception ex)
            {
                MessageBoxEx.Show("异常：" + ex.Message);
                throw;
            }
            return bSucceed;
        }
        //通过订单号查询供应商代码和名称
        private DataRow GetVendorIDAndName(string ponumber)
        {
            string sqlSelect = @"Select VendorNumber,VendorName From PurchaseOrdersByCMF Where PONumber='" + ponumber + "'";
            DataTable dtTemp = SQLHelper.GetDataTable(GlobalSpace.FSDBConnstr, sqlSelect);
            if (dtTemp.Rows.Count == 1)
            {
                return dtTemp.Rows[0];
            }
            else if (dtTemp.Rows.Count > 1)
            {
                MessageBoxEx.Show("订单号存在两个以上，出现异常，请联系管理员！", "提示");
            }
            return null;
        }
        private void cbbPO_SelectionChangeCommitted(object sender, EventArgs e)
        {

        }


        private void btnSearchPOForInvoice_Click(object sender, EventArgs e)
        {
            string sqlSelect = @"SELECT
	                                            VendorID AS 供应商代码,
	                                            POReceiptDate AS 日期,
	                                            AccountingYear AS 年度,
	                                            AccountingPeriod AS 月,
	                                            PONumber AS 采购单号,
                                              POReceiptMatchedStatus  AS 状态,
	                                            POReceiptLocalAmount AS 入库物料总金额,
	                                            InvoiceMatchedLocalAmount AS 已匹配金额
                                            FROM
	                                            _NoLock_FS_APReceiptHeader
                                            WHERE
	                                            POReceiptMatchedStatus <> 'F'
                                            AND AccountingYear = '" + tbYearForInvoice.Text.Trim() + "'  AND VendorID = '" + tbVendorNumberForInvoice.Text.Trim() + "' ";
            string sqlSelf = "AND Buyer = '" + fsuserid + "'";
            string sqlOrder = " ORDER BY POReceiptDate ASC ";
            if (tbVendorNumberForInvoice.Text == "" || tbYearForInvoice.Text == "")
            {
                MessageBoxEx.Show("供应商代码或年度不能为空！", "提示");
            }
            else
            {
                if (rbSelf.Checked)
                {
                    sqlSelect = sqlSelect + sqlSelf + sqlOrder;
                    dgvForInvoice.DataSource = SQLHelper.GetDataTable(GlobalSpace.FSDBMRConnstr, sqlSelect);
                }
                else
                {
                    sqlSelect = sqlSelect + sqlOrder;
                    dgvForInvoice.DataSource = SQLHelper.GetDataTable(GlobalSpace.FSDBMRConnstr, sqlSelect);
                }

            }
        }

        private void tbVendorNumberForInvoice_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                btnSearchPOForInvoice_Click(sender, e);
            }
        }

        private void tbPONumberForInvoice_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                btnSearchPOForInvoice_Click(sender, e);
            }
        }

        private void dgvPOForInvoice_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                MessageBoxEx.Show("请选择有效的行！", "提示");
            }
            else
            {

            }
        }

        private void dgvPOForInvoice_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            dgvPOForInvoice_CellContentDoubleClick(sender, e);
        }

        private void btnExportInvoiceToExcel_Click(object sender, EventArgs e)
        {
            string sqlSelect = @"Select Name,FilePath From PurchaseDepartmentFilePathByCMF Where BuyerID='" + fsuserid + "' And Status = 0 And Name = '" + "开具发票明细导出路径" + "'";
            DataTable dtTemp = SQLHelper.GetDataTable(GlobalSpace.FSDBConnstr, sqlSelect);
            if (dtTemp.Rows.Count > 0)
            {
                string poNumber = string.Empty;
                string filePath = dtTemp.Rows[0]["FilePath"].ToString();
                //获取通过订单查询供应商代码和名称
                if (tbVendorNumber2.Text == "" && cbbPO.Text != "")
                {
                    poNumber = cbbPO.Text;
                }
                else if (tbVendorNumber2.Text != "" && cbbPO.Text != "")
                {
                    poNumber = cbbPO.SelectedItem.ToString();
                }

                if (File.Exists(filePath + poNumber + ".xls"))
                {
                    if (MessageBoxEx.Show("该订单的导出文件已经存在，确定要覆盖么？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.OK)
                    {
                        DataRow dr3 = GetVendorIDAndName(poNumber);
                        string vendorNumber = dr3["VendorNumber"].ToString();
                        string vendorName = dr3["VendorName"].ToString();

                        if (dgvPO2.Rows.Count > 0)
                        {
                            //由于Dgv控件数据源中有多个不需要的列，此处新建表只构造需要的列

                            DataTable dtOrigin = (DataTable)dgvPO2.DataSource;
                            DataTable dtNew = new DataTable();
                            dtNew.Columns.Add(dtOrigin.Columns["ItemNumber"].Caption, typeof(string));
                            dtNew.Columns.Add(dtOrigin.Columns["ItemDescription"].Caption, typeof(string));
                            dtNew.Columns.Add(dtOrigin.Columns["ItemUM"].Caption, typeof(string));
                            dtNew.Columns.Add(dtOrigin.Columns["UnitPrice"].Caption, typeof(double));
                            dtNew.Columns.Add(dtOrigin.Columns["OrderQuantity"].Caption, typeof(double));
                            dtNew.Columns.Add(dtOrigin.Columns["ItemSum"].Caption, typeof(double));
                            dtNew.Columns.Add(dtOrigin.Columns["DemandDate"].Caption, typeof(string));
                            dtNew.Columns.Add(dtOrigin.Columns["ForeignNumber"].Caption, typeof(string));

                            DataRow dr2 = null;
                            foreach (DataRow dr in dtOrigin.Rows)
                            {
                                dr2 = dtNew.NewRow();
                                dr2["ItemNumber"] = dr["ItemNumber"];
                                dr2["ItemDescription"] = dr["ItemDescription"];
                                dr2["ItemUM"] = dr["ItemUM"];
                                dr2["UnitPrice"] = dr["UnitPrice"];
                                dr2["OrderQuantity"] = dr["OrderQuantity"];
                                dr2["ItemSum"] = dr["ItemSum"];
                                dr2["DemandDate"] = dr["DemandDate"];
                                dr2["ForeignNumber"] = dr["ForeignNumber"];

                                dtNew.Rows.Add(dr2);
                            }

                            ExportVendorPOItemDetailToExcel(filePath, dtNew, poNumber, vendorNumber, vendorName);
                        }
                    }
                    else
                    {
                        return;
                    }
                }
                else
                {
                    DataRow dr3 = GetVendorIDAndName(poNumber);
                    string vendorNumber = dr3["VendorNumber"].ToString();
                    string vendorName = dr3["VendorName"].ToString();

                    if (dgvPO2.Rows.Count > 0)
                    {
                        //由于Dgv控件数据源中有多个不需要的列，此处新建表只构造需要的列

                        DataTable dtOrigin = (DataTable)dgvPO2.DataSource;
                        DataTable dtNew = new DataTable();
                        dtNew.Columns.Add(dtOrigin.Columns["ItemNumber"].Caption, typeof(string));
                        dtNew.Columns.Add(dtOrigin.Columns["ItemDescription"].Caption, typeof(string));
                        dtNew.Columns.Add(dtOrigin.Columns["ItemUM"].Caption, typeof(string));
                        dtNew.Columns.Add(dtOrigin.Columns["UnitPrice"].Caption, typeof(double));
                        dtNew.Columns.Add(dtOrigin.Columns["OrderQuantity"].Caption, typeof(double));
                        dtNew.Columns.Add(dtOrigin.Columns["ItemSum"].Caption, typeof(double));
                        dtNew.Columns.Add(dtOrigin.Columns["DemandDate"].Caption, typeof(string));
                        dtNew.Columns.Add(dtOrigin.Columns["ForeignNumber"].Caption, typeof(string));

                        DataRow dr2 = null;
                        foreach (DataRow dr in dtOrigin.Rows)
                        {
                            dr2 = dtNew.NewRow();
                            dr2["ItemNumber"] = dr["ItemNumber"];
                            dr2["ItemDescription"] = dr["ItemDescription"];
                            dr2["ItemUM"] = dr["ItemUM"];
                            dr2["UnitPrice"] = dr["UnitPrice"];
                            dr2["OrderQuantity"] = dr["OrderQuantity"];
                            dr2["ItemSum"] = dr["ItemSum"];
                            dr2["DemandDate"] = dr["DemandDate"];
                            dr2["ForeignNumber"] = dr["ForeignNumber"];

                            dtNew.Rows.Add(dr2);
                        }

                        ExportVendorPOItemDetailToExcel(filePath, dtNew, poNumber, vendorNumber, vendorName);
                    }
                }


            }
            else
            {
                MessageBoxEx.Show("当前发票明细导出文件路径没有设置，请进行设置！", "提示");
            }
        }

        //查询构建发票明细DataTable
        private DataTable GetVendorInvoicePOItemDetail(string ponumber)
        {
            DataTable dtInvoice = new DataTable();
            DataTable dtTemp = new DataTable();
            string sqlSelect = @"SELECT
	                                        CONVERT([varchar](100),T1.TransactionDate,(23)) AS 入库日期,
	                                        T1.PONumber AS 订单编号,
	                                        T1.POLineNumber AS 行号,
	                                        T1.ItemNumber AS 物料代码,
	                                        T2.ItemDescription AS 物料描述,
	                                        T1.ItemUM AS 单位,
	                                        T1.ItemReceiptQuantity AS 未开数量,
	                                        T2.PricePreTax AS 采购单价,
	                                        (T1.ItemReceiptQuantity*T2.PricePreTax) AS 金额
                                            T2.ForeignNumber AS 外贸单号,
                                            T2.Comment1 AS 版数
                                        FROM
	                                        PORV T1,
	                                        PurchaseOrderRecordByCMF T2
                                        WHERE
	                                        T1.TransactionFunctionCode = 'PORV'
                                        AND T1.PONumber = 'PM-081818-001'
                                        AND T1.PONumber = T2.PONumber
                                        AND T1.POLineNumber = T2.LineNumber
                                        AND T1.POReceiptActionType = 'R'
                                        AND T1.POLineNumber = '1'
                                        ORDER BY T1.TransactionDate DESC";

            //foreach遍历订单中所有的行号，来查询每一个物料的入库记录

            return dtInvoice;
        }

        //导出发票开具订单详情Excel文件
        private void ExportVendorInvoicePOItemDetailToExcel(DataTable dt, string ponumber, string vendornumber, string vendorname)
        {
            try
            {
                IWorkbook workbook = new HSSFWorkbook();//声明工作簿对象，可以创建xls或xlsx Excel文件
                ISheet sheet1 = workbook.CreateSheet(ponumber); //创建工作表               
                ICell sheet1Title = sheet1.CreateRow(0).CreateCell(0); //创建第一行第一个单元格
                sheet1Title.SetCellValue("瑞阳制药有限公司采购订单"); //表头
                sheet1Title.CellStyle = ExcelHelper.GetTitleCellStyle(workbook);
                sheet1.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(0, 0, 0, 7));  //合并单元格
                IRow row, row2;
                ICell cell;
                ICellStyle cellStyle1 = ExcelHelper.GetCellStyle(workbook, 0);
                ICellStyle cellStyle2 = ExcelHelper.GetCellStyle(workbook, 0);

                //创建供应商信息单元格（包括供应商代码，名称，和单号）
                row = sheet1.CreateRow(1);
                cell = row.CreateCell(0);
                cell.SetCellValue(ponumber + "    " + vendornumber + "    " + vendorname);
                cell.CellStyle = cellStyle1;


                cell = row.CreateCell(1);
                cell.SetCellValue("");
                cell.CellStyle = cellStyle1;

                cell = row.CreateCell(2);
                cell.SetCellValue("");
                cell.CellStyle = cellStyle1;

                cell = row.CreateCell(3);
                cell.SetCellValue("");
                cell.CellStyle = cellStyle1;

                cell = row.CreateCell(4);
                cell.SetCellValue("");
                cell.CellStyle = cellStyle1;

                cell = row.CreateCell(5);
                cell.SetCellValue("");
                cell.CellStyle = cellStyle1;

                cell = row.CreateCell(6);
                cell.SetCellValue("");
                cell.CellStyle = cellStyle1;

                cell = row.CreateCell(7);
                cell.SetCellValue("");
                cell.CellStyle = cellStyle1;

                CellRangeAddress region = new CellRangeAddress(1, 1, 0, 7);
                sheet1.AddMergedRegion(region);

                //表头数据。此处由于前边设置问题，导致列标题为英文，所以手动设定为中文
                row2 = sheet1.CreateRow(2);
                cell = row2.CreateCell(0);
                cell.SetCellValue("物料代码");
                cell.CellStyle = cellStyle1;


                cell = row2.CreateCell(1);
                cell.SetCellValue("物料描述");
                cell.CellStyle = cellStyle1;

                cell = row2.CreateCell(2);
                cell.SetCellValue("单位");
                cell.CellStyle = cellStyle1;

                cell = row2.CreateCell(3);
                cell.SetCellValue("单价");
                cell.CellStyle = cellStyle1;

                cell = row2.CreateCell(4);
                cell.SetCellValue("订购数量");
                cell.CellStyle = cellStyle1;

                cell = row2.CreateCell(5);
                cell.SetCellValue("需求日期");
                cell.CellStyle = cellStyle1;

                cell = row2.CreateCell(6);
                cell.SetCellValue("外贸单号");
                cell.CellStyle = cellStyle1;

                cell = row2.CreateCell(7);
                cell.SetCellValue("小计");
                cell.CellStyle = cellStyle1;

                // 写入数据
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataRow dataR = dt.Rows[i];
                    row = sheet1.CreateRow(i + 3);
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        sheet1.AutoSizeColumn(j);//从第0列开始，设置第j列的宽度为自适应宽度
                        if (dataR[j] == DBNull.Value)
                        {
                            cell = row.CreateCell(j);
                            cell.SetCellValue("");
                            cell.CellStyle = cellStyle2;
                        }
                        else
                        {
                            cell = row.CreateCell(j);
                            cell.SetCellValue(dataR[j].ToString());
                            cell.CellStyle = cellStyle2;
                        }
                    }

                    /*

                    cell = row.CreateCell(0);
                    cell.SetCellValue(dataR["ItemNumber"].ToString());
                    cell.CellStyle = cellStyle2;

                    cell = row.CreateCell(1);
                    cell.SetCellValue(dataR["ItemDescription"].ToString());
                    cell.CellStyle = cellStyle2;

                    cell = row.CreateCell(2);
                    cell.SetCellValue(dataR["ItemUM"].ToString());
                    cell.CellStyle = cellStyle2;

                    cell = row.CreateCell(3);
                    cell.SetCellValue((Double)dataR["UnitPrice"]);
                    cell.CellStyle = cellStyle2;

                    cell = row.CreateCell(4);
                    cell.SetCellValue((Double)dataR["OrderQuantity"]);
                    cell.CellStyle = cellStyle2;

                    cell = row.CreateCell(5);
                    cell.SetCellValue((Double)dataR["ItemSum"]);
                    cell.CellStyle = cellStyle2;

                    cell = row.CreateCell(6);
                    cell.SetCellValue(dataR["DemandDate"].ToString());
                    cell.CellStyle = cellStyle2;

                    cell = row.CreateCell(7);
                    cell.SetCellValue(dataR["ForeignNumber"].ToString());
                    cell.CellStyle = cellStyle2;
                    */

                }

                if (!Directory.Exists(@"E:\Score Excel"))  //检查是否存在文件夹，不存在则新建
                {
                    Directory.CreateDirectory(@"E:\Score Excel");
                }

                FileStream file = new FileStream(@"E:\Score Excel\" + ponumber + ".xls", FileMode.Create);
                workbook.Write(file);
                file.Close();
                workbook.Close();
            }
            catch (Exception ex)
            {
                MessageBoxEx.Show("异常：" + ex.Message);
                throw;
            }

        }

        //导出五金材料发票开具订单详情Excel文件
        private void ExportNonManufacturingItemVendorInvoicePOItemDetailToExcel(DataTable dt, string ponumber, string vendornumber, string vendorname)
        {

        }

        private void btnInvoiceCheckedPOGetSum_Click(object sender, EventArgs e)
        {

        }

        private void btnStockKeeperDetail_Click(object sender, EventArgs e)
        {
            StockKeeper sk = new Purchase.StockKeeper();
            sk.Show();
        }

        private void tbItemNumber_KeyUp(object sender, KeyEventArgs e)
        {

        }

        private void tbItemNumber_KeyDown(object sender, KeyEventArgs e)
        {
            if (tbForeignNumber.Text != "")
            {
                if (e.Modifiers != 0)
                {
                    if (e.Alt && e.KeyValue == 113)
                    {
                        if (tbItemNumber.Text.Trim() != "")
                        {
                            Purchase.ForeignOrderItemChoose foic = new ForeignOrderItemChoose(VendorNumberForFO, tbForeignNumber.Text.Trim(), tbItemNumber.Text.Trim(), fsuserid);
                            foic.ShowDialog();
                            if (GlobalSpace.foItemInfoList != null)
                            {
                                tbItemNumber.Text = GlobalSpace.foItemInfoList[0];
                                tbItemDescription.Text = GlobalSpace.foItemInfoList[1];

                                tbUM.Text = GlobalSpace.foItemInfoList[2];
                                tbPricePreTax.Text = (Convert.ToDouble(GlobalSpace.foItemInfoList[3]) * 1.16).ToString();
                                tbPricePostTax.Text = GlobalSpace.foItemInfoList[3];
                                tbOrderQuantity.Text = GlobalSpace.foItemInfoList[4];
                                tbDeliveryDate.Focus();
                                GetVendorInfo(tbPONumberInDetail.Text.Trim(), this.cbbVendorList);
                                GetManufacturerInfo(tbItemNumber.Text.Trim(), tbPONumberInDetail.Text.Trim(), this.cbbManufacturerList);
                            }
                            if (iteminfoList.Count > 0)
                            {
                                iteminfoList.Clear();
                            }
                            iteminfoList = GetItemInfo(tbItemNumber.Text.Trim());
                        }
                        else
                        {
                            Purchase.ForeignOrderItemChoose foic = new ForeignOrderItemChoose(VendorNumberForFO, tbForeignNumber.Text.Trim(), "", fsuserid);
                            foic.ShowDialog();
                            if (GlobalSpace.foItemInfoList != null)
                            {
                                tbItemNumber.Text = GlobalSpace.foItemInfoList[0];
                                tbItemDescription.Text = GlobalSpace.foItemInfoList[1];

                                tbUM.Text = GlobalSpace.foItemInfoList[2];
                                tbPricePreTax.Text = (Convert.ToDouble(GlobalSpace.foItemInfoList[3]) * 1.16).ToString();
                                tbPricePostTax.Text = GlobalSpace.foItemInfoList[3];
                                tbOrderQuantity.Text = GlobalSpace.foItemInfoList[4];
                                tbDeliveryDate.Focus();
                                GetVendorInfo(tbPONumberInDetail.Text.Trim(), this.cbbVendorList);
                                GetManufacturerInfo(tbItemNumber.Text.Trim(), tbPONumberInDetail.Text.Trim(), this.cbbManufacturerList);
                            }
                            if (iteminfoList.Count > 0)
                            {
                                iteminfoList.Clear();
                            }
                            iteminfoList = GetItemInfo(tbItemNumber.Text.Trim());
                        }
                    }
                }
            }
        }

        private void tbForeignNumber_TextChanged(object sender, EventArgs e)
        {
            tbForeignNumber.Text = tbForeignNumber.Text.ToUpper();
            tbForeignNumber.SelectionStart = tbForeignNumber.Text.Length;
        }

        private void btnVendorEmailSetting_Click(object sender, EventArgs e)
        {
            VendorEmailSetting ves = new VendorEmailSetting(fsuserid);
            ves.Show();
        }

        private void btnFileExportPath_Click(object sender, EventArgs e)
        {
            FilePathSetting fps = new FilePathSetting(fsuserid);
            fps.Show();
        }

        private void btnYearlySearchForInvoice_Click(object sender, EventArgs e)
        {
            string sqlSelect = @"SELECT
                                                    VendorID as 供应商代码,
	                                                POReceiptDate as 日期,
	                                                AccountingYear as 年度,
	                                                AccountingPeriod AS 月,                                                
	                                                PONumber AS 采购单号,
                                                    POReceiptMatchedStatus  AS 状态,
	                                                POReceiptLocalAmount AS 入库物料总金额,
	                                                InvoiceMatchedLocalAmount AS 已匹配金额
                                                FROM
	                                                _NoLock_FS_APReceiptHeader
                                                WHERE
	                                                POReceiptMatchedStatus <>'F' AND AccountingYear = '" + tbYearForInvoice.Text.Trim() + "' ";
            string sqlSelf = "AND Buyer = '" + fsuserid + "'";
            if (tbYearForInvoice.Text == "")
            {
                MessageBoxEx.Show("年度不能为空！", "提示");
            }
            else
            {
                if (rbAll.Checked)
                {
                    dgvForInvoice.DataSource = SQLHelper.GetDataTable(GlobalSpace.FSDBMRConnstr, sqlSelect);
                }
                else
                {
                    sqlSelect = sqlSelect + sqlSelf;
                    dgvForInvoice.DataSource = SQLHelper.GetDataTable(GlobalSpace.FSDBMRConnstr, sqlSelect);
                }
            }
        }

        private void tbVendorNumber_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar == (char)13)
            {
                CommonOperate.TextBoxNext(tbVendorNumber, tbPONumber, e);
                if (GetVendorName(tbVendorNumber.Text.Trim()) == "")
                {
                    Custom.MsgEx("未查到该供应商代码信息！");
                }
                else
                {
                    tbVendorFuzzyName.Text = GetVendorName(tbVendorNumber.Text.Trim());
                }
            }

        }

        private void 修改该行物料ToolStripMenuItem_Click(object sender, EventArgs e)
        {

            string strPONumber = string.Empty;
            string strLineNumber = string.Empty;
            string strItemNumber = string.Empty;
            string strPromisedDate = string.Empty;
            string strId = string.Empty;
            string strFONumber = string.Empty;
            double strItemQuantity = 0;
            double strItemUnitPrice = 0;

            strPONumber = tbPONumberInDetail.Text.Trim();
            strId = dgvPOItemDetail["Id", dgvPOItemDetail.CurrentCell.RowIndex].Value.ToString();
            strLineNumber = dgvPOItemDetail["行号", dgvPOItemDetail.CurrentCell.RowIndex].Value.ToString();
            strPromisedDate = dgvPOItemDetail["需求日期", dgvPOItemDetail.CurrentCell.RowIndex].Value.ToString();
            strItemQuantity = Convert.ToDouble(dgvPOItemDetail["订购数量", dgvPOItemDetail.CurrentCell.RowIndex].Value);
            strItemUnitPrice = Convert.ToDouble(dgvPOItemDetail["单价", dgvPOItemDetail.CurrentCell.RowIndex].Value);
            strFONumber = dgvPOItemDetail["外贸单号", dgvPOItemDetail.CurrentCell.RowIndex].Value.ToString();
            string sqlSelect = @"Select POStatus From PurchaseOrderRecordByCMF Where Id = '" + strId + "'";

            int poStatus = Convert.ToInt32(SQLHelper.GetItemValue(GlobalSpace.FSDBConnstr, "POStatus", sqlSelect));

            if (poStatus < 3)
            {
                string sqlUpdate = @"Update PurchaseOrderRecordByCMF Set DemandDeliveryDate = '" + strPromisedDate + "',NeededDate='" + strPromisedDate + "',PromisedDate='" + strPromisedDate + "',UnitPrice=" + strItemUnitPrice + ",POItemQuantity=" + strItemQuantity + ", ForeignNumber = '" + strFONumber + "' Where Id = '" + strId + "'";

                if (SQLHelper.ExecuteNonQuery(GlobalSpace.FSDBConnstr, sqlUpdate) )
                {
                    Custom.MsgEx("修改成功！");
                    ShowPOItemDetail(tbPONumberInDetail.Text.Trim());
                }
                else
                {
                    Custom.MsgEx("修改失败！");
                }
            }
            else if (poStatus == 3)
            {
                string sqlUpdate = @"Update PurchaseOrderRecordByCMF Set DemandDeliveryDate = '" + strPromisedDate + "',NeededDate='" + strPromisedDate + "',PromisedDate='" + strPromisedDate + "',UnitPrice=" + strItemUnitPrice + ",POItemQuantity=" + strItemQuantity + ", ForeignNumber = '" + strFONumber + "' Where Id = '" + strId + "'";

                strItemNumber = dgvPOItemDetail["物料代码", dgvPOItemDetail.CurrentCell.RowIndex].Value.ToString();

                FSFunctionLib.FSConfigFileInitialize(GlobalSpace.fsconfigfilepath, fsuserid, fspassword);

                POMT12 myPomt12 = new POMT12();
                myPomt12.PONumber.Value = strPONumber;
                myPomt12.POLineNumber.Value = strLineNumber;
                myPomt12.ItemNumber.Value = strItemNumber;
                myPomt12.LineItemOrderedQuantity.Value = strItemQuantity.ToString();
                myPomt12.ItemUnitCost.Value = strItemUnitPrice.ToString();
                myPomt12.PromisedDate.Value = strPromisedDate;
                myPomt12.PromisedDateOld.Value = strPromisedDateOld;
                myPomt12.POLineSubType.Value = "L";

                if (FSFunctionLib.fstiClient.ProcessId(myPomt12, null))
                {
                    if (SQLHelper.ExecuteNonQuery(GlobalSpace.FSDBConnstr, sqlUpdate) )
                    {
                        Custom.MsgEx("修改成功！");
                        ShowPOItemDetail(tbPONumberInDetail.Text.Trim());
                        FSFunctionLib.FSExit();
                    }
                    else
                    {
                        Custom.MsgEx("四班记录修改成功，程序记录修改失败，请联系管理员！");
                    }
                }
                else
                {
                    FSFunctionLib.FSErrorMsg("物料修改失败");
                }
            }
            else
            {
                Custom.MsgEx("物料状态不允许进行修改！");
            }
        }

        private void 删除该行物料ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string strPONumber = string.Empty;
            string strLineNumber = string.Empty;
            string strItemNumber = string.Empty;
            string strPromisedDate = string.Empty;
            string strId = string.Empty;

            strPONumber = tbPONumberInDetail.Text.ToString();
            strLineNumber = dgvPOItemDetail["行号", dgvPOItemDetail.CurrentCell.RowIndex].Value.ToString();
            strId = dgvPOItemDetail["Id", dgvPOItemDetail.CurrentCell.RowIndex].Value.ToString();

            string sqlSelect = @"Select POStatus From PurchaseOrderRecordByCMF Where Id = '" + strId + "'";
            int poStatus = Convert.ToInt32(SQLHelper.GetItemValue(GlobalSpace.FSDBConnstr, "POStatus", sqlSelect));

            if (poStatus < 3)
            {
                string sqlUpdate = @"Update PurchaseOrderRecordByCMF Set POStatus = 99 Where Id = '" + strId + "'";
                if (SQLHelper.ExecuteNonQuery(GlobalSpace.FSDBConnstr, sqlUpdate) )
                {
                    Custom.MsgEx("修改成功！");
                    ShowPOItemDetail(tbPONumberInDetail.Text.Trim());
                }
                else
                {
                    Custom.MsgEx("修改失败！");
                }
            }
            else if (poStatus == 3)
            {
                string sqlUpdate = @"Update PurchaseOrderRecordByCMF Set POStatus = 99 Where Id = '" + strId + "'";
                strItemNumber = dgvPOItemDetail["物料代码", dgvPOItemDetail.CurrentCell.RowIndex].Value.ToString();
                strPromisedDate = dgvPOItemDetail["交货日", dgvPOItemDetail.CurrentCell.RowIndex].Value.ToString();

                FSFunctionLib.FSConfigFileInitialize(GlobalSpace.fsconfigfilepath, fsuserid, fspassword);

                POMT15 myPomt = new POMT15();

                myPomt.PONumber.Value = strPONumber;
                myPomt.POLineNumber.Value = strLineNumber;
                myPomt.ItemNumber.Value = strItemNumber;
                myPomt.PromisedDateOld.Value = strPromisedDate;
                myPomt.POLineSubType.Value = "L";


                if (FSFunctionLib.fstiClient.ProcessId(myPomt, null))
                {
                    if (SQLHelper.ExecuteNonQuery(GlobalSpace.FSDBConnstr, sqlUpdate) )
                    {
                        Custom.MsgEx("删除成功！");
                        ShowPOItemDetail(tbPONumberInDetail.Text.Trim());
                        FSFunctionLib.FSExit();
                    }
                    else
                    {
                        Custom.MsgEx("四班记录删除成功，程序记录删除失败，请联系管理员！");
                    }
                }
                else
                {
                    FSFunctionLib.FSErrorMsg("物料删除失败");
                }
            }
            else
            {
                Custom.MsgEx("物料状态不允许进行修改！");
            }




        }

        private void dgvPOItemDetail_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dgvPOItemDetail_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (e.ColumnIndex >= 0)
                {
                    if (e.RowIndex >= 0)
                    {
                        dgvPOItemDetail.ClearSelection();
                        dgvPOItemDetail.Rows[e.RowIndex].Selected = true;
                        dgvPOItemDetail.CurrentCell = dgvPOItemDetail.Rows[e.RowIndex].Cells[e.ColumnIndex];
                        this.contextMenuStrip1.Show(MousePosition.X, MousePosition.Y);
                    }
                }
            }
        }

        private void dgvPOItemDetail_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                strPromisedDateOld = dgvPOItemDetail.Rows[e.RowIndex].Cells["需求日期"].Value.ToString();
            }
        }

        private void tbForeignNumber_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Modifiers != 0)
            {
                if (e.Alt && e.KeyValue == 113)
                {
                    string sqlCheck = @"Select Count(Id) From PurchaseDepartmentForeignOrderItemByCMF Where VendorNumber='" + VendorNumberForFO + "' And IsValid = 1 And Status = 1";
                    if (!SQLHelper.Exist(GlobalSpace.FSDBConnstr, sqlCheck))
                    {
                        Custom.MsgEx("该供应商无可用的已审核物料记录！");
                        return;
                    }
                    Purchase.ForeignOrderItemChoose foic = new ForeignOrderItemChoose(VendorNumberForFO, fsuserid);
                    foic.ShowDialog();
                    if (GlobalSpace.foItemInfoList != null)
                    {
                        tbItemNumber.Text = GlobalSpace.foItemInfoList[0];
                        tbItemDescription.Text = GlobalSpace.foItemInfoList[1];

                        tbUM.Text = GlobalSpace.foItemInfoList[2];
                        tbPricePreTax.Text = (Convert.ToDouble(GlobalSpace.foItemInfoList[3]) * 1.16).ToString();
                        tbPricePostTax.Text = GlobalSpace.foItemInfoList[3];
                        tbOrderQuantity.Text = GlobalSpace.foItemInfoList[4];
                        tbDeliveryDate.Focus();
                        GetVendorInfo(tbPONumberInDetail.Text.Trim(), this.cbbVendorList);
                        GetManufacturerInfo(tbItemNumber.Text.Trim(), tbPONumberInDetail.Text.Trim(), this.cbbManufacturerList);
                    }
                    if (iteminfoList.Count > 0)
                    {
                        iteminfoList.Clear();
                    }
                    iteminfoList = GetItemInfo(tbItemNumber.Text.Trim());
                }
            }
        }

        private void tbForeignNumber_Enter(object sender, EventArgs e)
        {
            lblFOTipText.Visible = true;
        }

        private void tbForeignNumber_Leave(object sender, EventArgs e)
        {
            lblFOTipText.Visible = false;
        }

        private void tbItemNumber_Enter(object sender, EventArgs e)
        {
            lblFOItemTipText.Visible = true;

        }

        private void tbItemNumber_Leave(object sender, EventArgs e)
        {
            lblFOItemTipText.Visible = false;
        }

        private void 删除该订单ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (DialogResult.OK == MessageBoxEx.Show("删除订单前，确保订单中物料已经完全删除", "提示", MessageBoxButtons.OKCancel))
            {
                string ponumber = string.Empty;
                
                ponumber = dgvPO["PONumber", dgvPO.CurrentCell.RowIndex].Value.ToString();
                string sqlDelete = @"Delete From PurchaseOrdersByCMF Where PONumber='"+ponumber+"'";

                POMT04 pomt04 = new POMT04();
                pomt04.PONumber.Value = ponumber;

                if (FSFunctionLib.fstiClient.ProcessId(pomt04, null))
                {
                    if(SQLHelper.ExecuteNonQuery(GlobalSpace.FSDBConnstr,sqlDelete) )
                    {
                        MessageBox.Show("订单删除成功！");
                    }
                    else
                    {
                        MessageBox.Show("四班中订单删除成功，数据库中删除失败，请联系管理员！");
                    }
                }
                else
                {
                    FSFunctionLib.FSErrorMsg("订单删除失败！");
                }
            }
        }

        private void dgvPO_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (e.RowIndex >= 0)
                {

                    dgvPO.ClearSelection();
                    dgvPO.Rows[e.RowIndex].Selected = true;
                    this.contextMenuStrip2.Show(MousePosition.X, MousePosition.Y);
                }
            }
        }

        private void btnAddItemToPO_KeyPress(object sender, KeyPressEventArgs e)
        {
            btnAddItemToPO_Click(sender, e);
        }
    }

}
