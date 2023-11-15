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
using Spire.Xls;
using Global;
using System.Linq;


namespace Global.Purchase
{
    public partial class PlaceOrder : Office2007Form
    {
        string fsuserid = string.Empty;
        string fspassword = string.Empty;
        string fsusername = string.Empty;
        //此处UserInfo.SupervisorID为该用户的直属领导代码
        
        string VendorInfo = string.Empty;
        string strPromisedDateOld = string.Empty;
        string VendorNumberForFO = string.Empty;
        string Privilege = string.Empty;
        int PONumberStartNumber = 0;
        int PONumberEndNumber = 0;
        bool BBeyondRange = false;
        Dictionary<string, string> ShelfDaysDict;
        //订单列表
        List<string> poList = new List<string>();
        List<string> poErrorList = new List<string>();
        List<string> poToSuperiorList = new List<string>();
        List<string> iteminfoList = new List<string>();
        //List<string> WithoutRestrictItemList = new List<string>();
        Dictionary<string,string> ItemConfirmerDict = new Dictionary<string, string>();
        List<string> StockSpecialItemList = new List<string>();
        string SpecialItemKeeper = string.Empty;

        //字符集，此处用于供应商名字模糊查询供应商时使用
        Encoding GB2312 = Encoding.GetEncoding("gb2312");
        Encoding ISO88591 = Encoding.GetEncoding("iso-8859-1");

        public PlaceOrder()
        {
            InitializeComponent();
        }

        public PlaceOrder(string userid, string password, string name,string privilege,int poStartNumber,int poEndNumber)
        {
            InitializeComponent();
            fsuserid = userid;
            fspassword = password;
            fsusername = name;
            Privilege = privilege;
            PONumberStartNumber = poStartNumber;
            PONumberEndNumber = poEndNumber;
            MessageBoxEx.EnableGlass = false;
            this.EnableGlass = false;
        }

        //获取用户信息
        private DataTable GetUserInfo(string struserid)
        {            
            string strSelect = @"Select SupervisorID,Status,Email,Name,Password,PurchaseType,POType,PriceCompare,POItemOthersConfirm,PONumberSequenceNumberRange,POTypeWithRange,POTogether,ItemReceiveType,Group,ConfirmGroup   from PurchaseDepartmentRBACByCMF where UserID='" + struserid + "'";           
            return SQLHelper.GetDataTable(GlobalSpace.FSDBConnstr, strSelect);
        }

        //获取上级领导代码
      /*  private string GetSuperiorId(string struserid)
        {
            string UserInfo.SupervisorID = string.Empty;
            string strSelect = @"Select UserInfo.SupervisorID from PurchaseDepartmentRBACByCMF where UserID='" + struserid + "'";
            DataTable dtTemp = SQLHelper.GetDataTable(GlobalSpace.FSDBConnstr, strSelect);
            if (dtTemp.Rows.Count > 0)
            {
                UserInfo.SupervisorID = dtTemp.Rows[0]["UserInfo.SupervisorID"].ToString();
            }
            else
            {
                UserInfo.SupervisorID = "NULL";
            }
            return UserInfo.SupervisorID;
        }*/
        private void btnPlaceOrder_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(cbbPOPrefix.Text))
            {
                Custom.MsgEx("采购类型不能为空！");
                return;
            }

            //1.该供应商当天是否存在订单，存在的话提示是否追加
            //2.追加订单的处理

            //3.下达新订单的处理
            //包材类购买
            string latestSequenceNumber = string.Empty;
            if ("P"+cbbPOPrefix.Text == "PJ" || "P" + cbbPOPrefix.Text == "PF"|| "P" + cbbPOPrefix.Text == "PP")
            {
                latestSequenceNumber = tbPOPostfix.Text;
            }
            else
            {
                if(rbtnAutomatic.Checked)
                {
                    latestSequenceNumber = GeneratePONumberSequenceNumber2("P" + cbbPOPrefix.Text, fsuserid);
                }
                else
                {
                    latestSequenceNumber = tbPOPostfix.Text;
                }
            }
            if(latestSequenceNumber == "BeyondRange")
            {
                if (!BBeyondRange)
                {
                    Custom.MsgEx("订单号已超出范围！");
                    return;
                }
            }
            PlacePurchaseOrderNew(lblPOPrefix.Text+cbbPOPrefix.Text+"-"+ tbPONumber.Text.Trim()+"-"+ latestSequenceNumber);
       //     tbPONumber.Text = "";
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
            string strSqlExist = @"Select Count(Id) From PurchaseOrderRecordByCMF Where PONumber='" + poNumber + "'";
            string strSqlExistFromFS = @"Select Count(PONumber) From FSDBMR.dbo._NoLock_FS_POHeader Where PONumber='" + poNumber + "'";
            if (SQLHelper.Exist(GlobalSpace.FSDBConnstr, strSqlExist) || SQLHelper.Exist(GlobalSpace.FSDBMRConnstr, strSqlExistFromFS))
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
                    string strInsert = @"Insert Into PurchaseOrdersByCMF (PONumber,VendorNumber,VendorName,Buyer,Supervisor,ParentGuid) Values (@PONumber,@VendorNumber,@VendorName,@Buyer,@Supervisor,@Guid)";
                    SqlParameter[] sqlparams =
                    {
                        new SqlParameter("@PONumber",poNumber),
                        new SqlParameter("@VendorNumber",vendorId),
                        new SqlParameter("@VendorName",vendorName),
                        new SqlParameter("@Buyer",fsuserid),
                       new SqlParameter("@Supervisor",PurchaseUser.SupervisorID),
                       new SqlParameter("@Guid",Guid.NewGuid().ToString("N"))
                    };
                    if (SQLHelper.ExecuteNonQuery(GlobalSpace.FSDBConnstr, strInsert, sqlparams))
                    {
                        MessageBoxEx.Show("订单下达成功！");
                 //       tbPONumber.Text = "";
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
        //下达订单
        private void PlacePurchaseOrderNew(string poNumber)
        {
            string PONumber = string.Empty;
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
                    string strInsert = @"Insert Into PurchaseOrderRecordByCMF (PONumber,VendorNumber,VendorName,Buyer,Superior,IsPurePO,Guid,POTogether) Values (@PONumber,@VendorNumber,@VendorName,@Buyer,@Supervisor,@IsPurePO,@Guid,@POTogether)";
                    int poTogether = 0;
                    if(PurchaseUser.POTogether == 1)
                    {
                        poTogether = 1;
                    }
                    SqlParameter[] sqlparams =
                    {
                        new SqlParameter("@PONumber",poNumber),
                        new SqlParameter("@VendorNumber",vendorId),
                        new SqlParameter("@VendorName",vendorName),
                        new SqlParameter("@Buyer",fsuserid),
                       new SqlParameter("@Supervisor",PurchaseUser.SupervisorID),
                       new SqlParameter("@IsPurePO",1),
                       new SqlParameter("@Guid",Guid.NewGuid().ToString("N")),
                       new SqlParameter("@POTogether",poTogether),
                    };
                    if (SQLHelper.ExecuteNonQuery(GlobalSpace.FSDBConnstr, strInsert, sqlparams))
                    {
                        MessageBoxEx.Show("订单下达成功！");
            //            tbPONumber.Text = "";
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
                    CommonOperate.WriteFSErrorLog("POMT00", myPomt00, error, fsuserid);
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
            string strSpecialDate = dtpForPO.Value.ToString("yyyy-MM-dd");
            string sqlSelect = @" SELECT
                                    Distinct T1.Guid,
	                                T1.PONumber AS 采购单号,
	                                T1.VendorNumber AS 供应商码,
                                    T1.VendorName AS 供应商名,GSID                                  
                                FROM
	                                PurchaseOrderRecordByCMF T1
                                WHERE                                   
                                    T1.Buyer ='" + fsuserid + "' And IsPurePO = 1  ";
            string sqlCriteria = string.Empty;
            DataTable dtTemp = null;
            try
            {
                switch (strperiod)
                {
                    case "0":
                        sqlCriteria = @"AND T1.POItemPlacedDate ='" + strDateNow + "'  Order By PONumber ASC";
                        dtTemp = SQLHelper.GetDataTable(GlobalSpace.FSDBConnstr, sqlSelect + sqlCriteria);
                        break;
                    case "Week":
                        sqlCriteria = @" AND T1.POItemPlacedDate >='" + strWeekDate + "' AND T1.POItemPlacedDate <='" + strDateNow + "'    Order By PONumber ASC";
                        dtTemp = SQLHelper.GetDataTable(GlobalSpace.FSDBConnstr, sqlSelect + sqlCriteria );
                        break;
                    case "Month":
                       sqlCriteria = @"  AND T1.POItemPlacedDate >='" + strMonthDate + "' AND T1.POItemPlacedDate <='" + strDateNow + "'    Order By PONumber ASC";
                        dtTemp = SQLHelper.GetDataTable(GlobalSpace.FSDBConnstr, sqlSelect + sqlCriteria);
                        break;
                    case "Special":
                        sqlCriteria = @"  AND T1.POItemPlacedDate ='" + strSpecialDate + "'    Order By PONumber ASC";
                        dtTemp = SQLHelper.GetDataTable(GlobalSpace.FSDBConnstr, sqlSelect + sqlCriteria);
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBoxEx.Show("发生异常：" + ex.Message);
            }
            dgvPO.DataSource = dtTemp;
            /*
            dtTemp.Columns.Add("Guid");
            foreach(DataRow dr in dtTemp.Rows)
            {
                string guid = CommonOperate.GetPOGuid(dr["采购单号"].ToString());
                dr["Guid"] = guid;
            }
            */
            dgvPO.Columns["Guid"].Visible = false;
           dgvPO.Columns["GSID"].Visible = false;
        }

        private void GetFSPrice(object sender,EventArgs e)
        {
            lblFSItemPrice.Text = GetItemStandardPrice(tbItemNumber.Text);
            GetVendorInfo(tbPONumberInDetail.Text.Trim(), this.cbbVendorList);
       //     GetManufacturerInfo(tbItemNumber.Text.Trim(), tbPONumberInDetail.Text.Trim(), this.cbbManufacturerList);
            if (iteminfoList.Count > 0)
            {
                iteminfoList.Clear();
            }
            iteminfoList = GetItemInfo(tbItemNumber.Text.Trim());

        }
        private DataTable GetPOItemConfirmer()
        {
            string sqlSelect = @"Select ItemNumber,Confirmer From PurchaseDepartmentPOItemConfirmer";
            return SQLHelper.GetDataTable(GlobalSpace.FSDBConnstr, sqlSelect);
        }
        private void PlaceOrder_Load(object sender, EventArgs e)
        {
            string sqlSelect = @"Select ItemNumber From PurchaseDepartmentWithourResrtict";
            //WithoutRestrictItemList = SQLHelper.GetList(GlobalSpace.FSDBConnstr, sqlSelect, "ItemNumber");

            if (PurchaseUser.UserStatus == 2)
            {
                lblBatches.Visible = true;
                tbBatches.Visible = true;
            }
            //获取当前所有物料的确认人明细
            ItemConfirmerDict = GetPOItemConfirmer().Rows.Cast<DataRow>().ToDictionary(r => r["ItemNumber"].ToString(), r => r["Confirmer"].ToString());
            tbOrderQuantity.GotFocus += new EventHandler(GetFSPrice);
            ////dtp控件的显示方式为自定义的yyyy格式
            //this.dtpForInvoice.Format = DateTimePickerFormat.Custom;
            //this.dtpForInvoice.CustomFormat = "yyyy";
            tbPOPostfix.GotFocus += new EventHandler(POPostFixGotFocus);
            tbPONumber.Text = DateTime.Now.ToString("MMddyy");

            StockSpecialItemList = GetStockSpecialItemList();

            string sqlSelectSpecialItemKeeper = @"Select UserID+'|'+UserName  AS StockKeeper  From PurchaseDepartmentStockSpecialItemKeeper";
            SpecialItemKeeper = SQLHelper.ExecuteScalar(GlobalSpace.FSDBConnstr, sqlSelectSpecialItemKeeper).ToString();

            if (PurchaseUser.POItemOthersConfirm == 1)
            {
                if (PurchaseUser.PurchaseType.Contains("P"))
                {
                    DataTable dtConfirmType = GetPOItemConfirmPersonList(PurchaseUser.PurchaseType);
                    BindingSource bs = new BindingSource();
                    bs.DataSource = dtConfirmType.Rows.Cast<DataRow>().ToDictionary(r => r["UserID"].ToString(), r => r["Name"].ToString());
                    cbbConfirmPerson.DataSource = bs;
                    cbbConfirmPerson.DisplayMember = "Value";
                    cbbConfirmPerson.ValueMember = "Key";
                    cbbConfirmPerson.SelectedIndex = -1;
                }
                else
                {
                    cbbConfirmPerson.Text = fsuserid;
                }

         

                //      FSFunctionLib.FSConfigFileInitialize(fsTestconfigfilepath, fsuserid, fspassword);
                ShowOrder("0");
                //   }
                //弹出的对话框采用Office2007样式

                //订单用DataGridView控件中，选中一列的ReadOnly属性为false，这样可以手动进行选中和取消的操作

                dgvPO.Columns["dgvPOCheck"].Width = 30;
            }
            string[] pType = PurchaseUser.PurchaseType.Split('|');
            cbbPOPrefix.DataSource = pType;
            ShowOrder("0");
        }

        private List<string> GetStockSpecialItemList()
        {
            string sqlSelect = @"Select ItemNumber From PurchaseDepartmentStockSpecialItem2";
            return SQLHelper.GetDataTable(GlobalSpace.FSDBConnstr, sqlSelect).AsEnumerable().Select(r => r.Field<string>("ItemNumber")).ToList();
        }

        private DataTable GetPOItemConfirmPersonList(string purchaseType)
        {
            string type = string.Empty;

            if (purchaseType.Contains("A"))
            {
                type = "A";
            }
            if (purchaseType.Contains("P"))
            {
                type = "P";
            }
            string sqlSelect = @"Select UserID,(UserID+'|'+Name) AS Name From PurchaseDepartmentRBACByCMF Where POItemConfirmType = '" + type+"'";
            return SQLHelper.GetDataTable(GlobalSpace.FSDBConnstr, sqlSelect);
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
                    cbbVendor.Text = "";
                    if(cbbVendor.Items.Count > 0)
                    {
                        cbbVendor.Items.Clear();
                    }
                    string strSql = @"SELECT
	                                    VendorID,
	                                    VendorName
                                    FROM
	                                    _NoLock_FS_Vendor	                                    
                                    WHERE
	                                    VendorName like '%" + str + "%'";
                    DataTable dtTemp = SQLHelper.GetDataTableOleDb(GlobalSpace.oledbconnstrFSDBMR, strSql);
                    if(dtTemp.Rows.Count > 0)
                    {
                        if (dtTemp.Rows.Count == 1)
                        {
                            tbVendorNumber.Text = dtTemp.Rows[0]["VendorID"].ToString();
                            cbbVendor.Text = dtTemp.Rows[0]["VendorID"].ToString() + " " + dtTemp.Rows[0]["VendorName"].ToString();
                        }
                        else
                        {
                            tbVendorNumber.Text = "";
                            cbbVendor.Text = "";
                            foreach (DataRow dr in dtTemp.Rows)
                            {
                                cbbVendor.Items.Add(dr["VendorID"].ToString() + " " + dr["VendorName"].ToString());
                            }
                        }
                    }
                      
                }
            }
        }

        private void tbPONumber_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void dgvPO_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                tbPONumberInDetail.Text = dgvPO["采购单号", e.RowIndex].Value.ToString();
                tbPONumberInDetail.Tag = dgvPO["Guid", e.RowIndex].Value.ToString();
                VendorNumberForFO = dgvPO["供应商码", e.RowIndex].Value.ToString();
                tbItemNumber.Tag = dgvPO["供应商码", e.RowIndex].Value.ToString();
                btnAddItemToPO.Tag = dgvPO["GSID", e.RowIndex].Value.ToString();
                if (PurchaseUser.UserPOType == "F")
                {
                    tbForeignNumber.Focus();
                }
                    else
                {
                    tbItemNumber.Focus();
                }
            }
        }

        private void dgvPO_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if(PurchaseUser.UserPOType == "F")
            {
                tbFONumber.Focus();
            }
            else
            {
                tbItemNumber.Focus();
            }
            string strPO = string.Empty;
            if (e.RowIndex < 0)
            {
                MessageBoxEx.Show("请选择有效的行！", "提示");
            }
            else
            {
                strPO = dgvPO["采购单号", e.RowIndex].Value.ToString();
                VendorNumberForFO = dgvPO["供应商码", e.RowIndex].Value.ToString();
                tbPONumberInDetail.Text = strPO;
                tbPONumberInDetail.Tag = dgvPO["Guid", e.RowIndex].Value.ToString(); 
         //       tbItemNumber.Focus();
                ShowPOItemDetail(strPO);
                tbPOForSearch.Text = strPO;
                btnSearchPO2_Click(sender, e);
            }
        }

        private void dgvPO_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            dgvPO_CellContentDoubleClick(sender, e);
        }
        private void ShowPOItemDetail(string ponumber)
        {
            string strSqlCheck = @"Select  Count(Id) From  PurchaseOrderRecordByCMF Where PONumber='" + ponumber + "'";
            string strSql = string.Empty;
            //if(PurchaseUser.PurchaseType.Contains("M"))
            //{
                strSql = @"SELECT
                                                T1.Id,
                                                T1.Guid,
                                                T1.LineNumber AS 行号,
	                                        	T1.ItemNumber AS 物料代码,
	                                            T1.ItemDescription AS 物料描述,
	                                            T1.LineUM AS 单位,
	                                            T1.LineStatus AS 四班状态,
                                                T1.PricePreTax AS 含税单价,
	                                            T1.UnitPrice AS 单价,
	                                            T1.POItemQuantity AS 订购数量,
	                                            T1.DemandDeliveryDate AS 需求日期,
	                                            T1.ForeignNumber AS 外贸单号,
                                                  (     case T1.POStatus
                                                        when  '-1' then '已取消'
                                                        when  '0' then '已准备'
                                                         when  '1' then '已提交'
                                                         when  '2' then '已审核'
                                                         when  '3' then '已下达' 
                                                        when  '4' then '已到货' 
                                                        when  '5' then '已收货' 
                                                        when  '6' then '已入库' 
                                                        when  '7' then '已开票' 
                                                end     
                                                ) as 物料状态,
                                                T1.ManufacturerNumber AS 生产商码,
                                                T1.ManufacturerName AS  生产商名,
                                                T1.TaxRate           AS  税率,
	                                            T1.LineType AS 类型,
                                                T1.DemandDeliveryDate AS 需求日期旧
                                        FROM
	                                        PurchaseOrderRecordByCMF T1
                                        WHERE
	                                        T1.PONumber = '" + ponumber + "' And T1.POStatus <> 99 And IsPurePO = 0";
            //}
            //else
            //{
            //    strSql = @"SELECT
            //                                    T1.Id,
            //                                    T1.Guid,
            //                                    T1.LineNumber AS 行号,
	           //                             	T1.ItemNumber AS 物料代码,
	           //                                 T1.ItemDescription AS 物料描述,
	           //                                 T1.LineUM AS 单位,
	           //                                 T1.LineType AS 类型,
	           //                                 T1.LineStatus AS 状态,
	           //                                 T1.UnitPrice AS 单价,
	           //                                 T1.POItemQuantity AS 订购数量,
	           //                                 T1.DemandDeliveryDate AS 需求日期,
	           //                                 T1.ForeignNumber AS 外贸单号,
            //                                      (     case T1.POStatus
            //                                            when  '0' then '已准备'
            //                                             when  '1' then '已提交'
            //                                             when  '2' then '已审核'
            //                                             when  '3' then '已下达' 
            //                                            when  '4' then '已到货' 
            //                                            when  '5' then '已收货' 
            //                                            when  '6' then '已入库' 
            //                                            when  '7' then '已开票' 
            //                                    end     
            //                                    ) as 物料状态
            //                            FROM
	           //                             PurchaseOrderRecordByCMF T1
            //                            WHERE
	           //                             T1.PONumber = '" + ponumber + "' And T1.POStatus <> 99 And IsPurePO = 0";
            //}
           


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
                dgvPOItemDetail.Columns["四班状态"].ReadOnly = true;
                dgvPOItemDetail.Columns["含税单价"].ReadOnly = true;
                dgvPOItemDetail.Columns["物料状态"].ReadOnly = true;
                dgvPOItemDetail.Columns["生产商码"].ReadOnly = true;
                dgvPOItemDetail.Columns["生产商名"].ReadOnly = true;
                dgvPOItemDetail.Columns["税率"].ReadOnly = true;
                dgvPOItemDetail.Columns["需求日期旧"].Visible = false; 

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
        //获取物料在四班中的标准价格
        private string GetItemStandardPrice(string itemNumber)
        {
            string sqlSelect = @"SELECT
	                                            T2.TotalRolledCost
                                            FROM
	                                            _NoLock_FS_Item T1,
	                                            _NoLock_FS_ItemCost T2
                                            WHERE
	                                            T1.ItemKey = T2.ItemKey
                                            AND T2.CostType = 0
                                            AND T1.ItemNumber = '"+itemNumber+"'";
            return SQLHelper.ExecuteScalar(GlobalSpace.FSDBMRConnstr, sqlSelect).ToString();
        }
        //获取P类物料的价格
        private DataTable GetPackageItemPrice(string itemNumber,string itemDescription,string vendorNumber)
        {
            string sqlSelect = @"Select PricePreTax From PurchaseDepartmentDomesticProductItemPrice Where ItemNumber = '"+ItemNumber+"' And ItemDescription='"+itemDescription+"' And VendorNumber='"+vendorNumber+"' ";
            return SQLHelper.GetDataTable(GlobalSpace.FSDBConnstr, sqlSelect);
        }

        private void tbItemNumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                if (iteminfoList.Count > 0)
                {
                    iteminfoList.Clear();
                }
                if (string.IsNullOrEmpty(tbItemNumber.Text))
                {
                    MessageBoxEx.Show("物料代码不得为空！");
                }
                else
                {
                    iteminfoList = GetItemInfo(tbItemNumber.Text.Trim());
                    if (iteminfoList.Count > 0)
                    {
                        GetVendorInfo(tbPONumberInDetail.Text.Trim(), this.cbbVendorList);
                        //GetItemInfo();
                        tbItemDescription.Text = iteminfoList[3].ToString();
                        tbUM.Text = iteminfoList[4].ToString();
                        tbUM.Tag = iteminfoList[5].ToString();
                        if(cbbManufacturerList.Items.Count > 0)
                        {
                            cbbManufacturerList.Text = "";
                            cbbManufacturerList.Items.Clear();
                        }
                        //              GetManufacturerInfo(tbItemNumber.Text.Trim(), tbItemNumber.Tag.ToString(), this.cbbManufacturerList);
                        
                        string strSql = @"SELECT
	                    (
		                    ManufacturerNumber + '|' + ManufacturerName
	                    ) AS MInfo
                    FROM
	                    ItemManufacturerInfoByCMF
                    WHERE
	                    ItemNumber = '" + tbItemNumber.Text + "'    AND VendorNumber = '" + tbItemNumber.Tag.ToString() + "' and Status=0";
                   //     MessageBox.Show(strSql);
                        List<string> mList = SQLHelper.GetDataTable(GlobalSpace.FSDBConnstr, strSql).AsEnumerable().Select(r => r.Field<string>("MInfo")).ToList() ;
               //         MessageBox.Show(mList.Count.ToString());
                        if (mList.Count == 0)
                        {
                            cbbManufacturerList.Text = VendorInfo;
                   //         MessageBox.Show(VendorInfo);
                        }
                        else
                        {
                            mList.Add(VendorInfo);
                            for (int x = 0; x < mList.Count; x++)
                            {
                                cbbManufacturerList.Items.Add(mList[x]);
                            }
                        }

                        lblFSItemPrice.Text = GetItemStandardPrice(tbItemNumber.Text);
                    }
                    if(PurchaseUser.UserPOType == "D")
                    {
                        string sqlSelectItemPrice = @"SELECT
	                                                    ItemNumber AS 物料代码,
	                                                    ItemDescription AS 物料描述,
	                                                    VendorNumber AS 供应商代码,
	                                                    VendorName AS 供应商名称,
	                                                    PricePreTax AS 含税价格
                                                    FROM
	                                                    PurchaseDepartmentDomesticProductItemPrice
                                                    WHERE
	                                                    ItemNumber = '" + tbItemNumber.Text.Trim() + "' And VendorNumber = '" + VendorNumberForFO + "'";

                        DataTable dtItemPrice = SQLHelper.GetDataTable(GlobalSpace.FSDBConnstr, sqlSelectItemPrice);
                        if (dtItemPrice.Rows.Count > 0)
                        {
                            if (dtItemPrice.Rows.Count == 1)
                            {
                                tbPricePreTax.Text = dtItemPrice.Rows[0]["含税价格"].ToString();
                            }
                            else
                            {
                                Purchase.DomesticItemPrice dip = new DomesticItemPrice(tbItemNumber.Text.Trim(), VendorNumberForFO,"");
                                dip.ShowDialog();
                                tbPricePreTax.Text = GlobalSpace.VialPrice.ToString();
                            }
                        }
                    }

                    //  }

                }
                tbOrderQuantity.Focus();
            }
          
        }

        //获取供应商信息
        private void GetVendorInfo(string strponumber, ComboBox cbb)
        {
            string strSql = @"SELECT
	                        (T1.VendorNumber +'|'+
	                        T1.VendorName) AS VendorInfo
                        FROM
	                       PurchaseOrderRecordByCMF T1
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
     /*   private void GetManufacturerInfo(string stritemnumber, string vendornumber, ComboBox cbb)
        {
        
            string strSql = @"SELECT
	                    (
		                    ManufacturerNumber + '|' + ManufacturerName
	                    ) AS MInfo
                    FROM
	                    ItemManufacturerInfoByCMF
                    WHERE
	                    ItemNumber = '"+ stritemnumber + "'    AND VendorNumber = '"+vendornumber+"' ";
            
            DataTable dtTemp = SQLHelper.GetDataTable(GlobalSpace.FSDBConnstr, strSql);
            int iMNumber = dtTemp.Rows.Count;

            if(cbb.Items.Count > 0)
            {
                cbb.Items.Clear();
            }

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
        }*/
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
        //获取当前不需要审核物料列表
        private List<string>  GetItemWithoutReviewList()
        {
            string sqlSelect = @"Select ItemNumber From PurchaseDepartmentDomesticProductItemWithoutReviewByCMF";
            return SQLHelper.GetDataTable(GlobalSpace.FSDBConnstr, sqlSelect).AsEnumerable().Select(r => r.Field<string>("ItemNumber")).ToList();
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
                    if(!string.IsNullOrEmpty( tbPricePostTax.Text))
                    {
                        tbDeliveryDate.Focus();
                    }
                    else
                    {
                        tbPricePreTax.Focus();
                    }
                    //
                    Dictionary<string, string> vendorOrItemDict = GetDirectVendorOrItemDict();

                    if(ItemConfirmerDict.ContainsKey(tbItemNumber.Text.Trim()))
                    {
                        cbbConfirmPerson.Text = ItemConfirmerDict[tbItemNumber.Text.Trim()];
                    }
                    else
                    {
                        if(tbItemNumber.Text.Substring(0,1) == "M" || tbItemNumber.Text.Substring(0, 1) == "F")
                        {
                            Custom.MsgEx("当前物料没有增加确认到货人员，请先添加！");
                            return;
                        }
                        else if((tbItemNumber.Text.Substring(0, 1) == "A" && PurchaseUser.ItemReceiveType == "M"))
                        {
                            Custom.MsgEx("当前物料没有增加确认到货人员，请先添加！");
                            return;
                        }
                        else if (vendorOrItemDict.Keys.Contains(VendorNumberForFO))
                        {
                            cbbConfirmPerson.Text = vendorOrItemDict["VendorNumberForFO"];
                        }
                        else if (tbItemDescription.Text.Contains("胶塞") || tbItemDescription.Text.Contains("铝塑盖") || tbItemDescription.Text.Contains("铝盖") || tbItemDescription.Text.Contains("铝塑组合盖"))
                        {
                            //胶塞、铝盖和铝塑盖归桑元庆管理，P04
                            cbbConfirmPerson.Text = "P08|桑元庆";
                        }
                        else if (PurchaseUser.POItemOthersConfirm == 0)
                        {
                            cbbConfirmPerson.Text = fsuserid + "|" + fsusername;
                        }
                        else if (PurchaseUser.UserPOType == "D")
                        {
                            cbbConfirmPerson.Text = "P04|丁积峰";
                        }
                        else if (PurchaseUser.UserPOType == "F")
                        {
                            cbbConfirmPerson.Text = "P08|桑元庆";
                        }
                    }             
                }
            }
        }

        private List<string> GetItemNotComparePriceList()
        {
            string sqlSelect = @"Select ItemNumber  From PurchaseDepartmentNotComparePrice";
            return SQLHelper.GetDataTable(GlobalSpace.FSDBConnstr, sqlSelect).AsEnumerable().Select(r => r.Field<string>("ItemNumber")).ToList();
        }
        private void tbPricePreTax_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                if (string.IsNullOrEmpty(tbPricePreTax.Text.ToString()))
                {
                    tbPricePostTax.Focus();
                }
                else
                {
                    double fPrice = Convert.ToDouble(tbPricePreTax.Text);
                    double fTaxRate = Convert.ToDouble(cbbTaxRate.SelectedItem.ToString());
                    tbPricePostTax.Text = (Math.Round( fPrice / (1 + fTaxRate),9)).ToString();
                           double standardPrice = Convert.ToDouble(lblFSItemPrice.Text);
              //      double standardPrice = 1.2;
                    List<string> itemList = GetItemWithoutReviewList();
                    List<string> itemNotComparePriceList = GetItemNotComparePriceList();
                    
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
            /*
            if (e.KeyChar == (char)13)
            {
                if (string.IsNullOrEmpty(tbPricePostTax.Text.ToString()))
                {
                    MessageBoxEx.Show("税后价格不得为空！");
                }
                else
                {
                    double fPrice = Convert.ToDouble(tbPricePostTax.Text);
                    double standardPrice = Convert.ToDouble(lblFSItemPrice.Text);
                    //>0.1|<-0.1，>0.05 |<-0.05
                    List<string> itemNotComparePriceList = GetItemNotComparePriceList();
                    string rate = CommonOperate.CompareItemPriceToStandardPrice(fPrice, standardPrice);
                    string itemType = tbItemNumber.Text.Trim().Substring(0, 1);
                    if(PurchaseUser.PriceCompare == 1)
                    {
                        if(!itemNotComparePriceList.Contains(tbItemNumber.Text))
                        {
                            if (itemType == "P")
                            {
                                if (rate == "0.15")
                                {
                                    Custom.MsgEx("该物料下达价格与标准价格相差超出15%，无法下达订单！请先修改物料标准成本");
                                    btnAddItemToPO.Enabled = false;
                                    return;
                                }
                                else if (rate == "0.1")
                                {
                                    Custom.MsgEx("该物料下达价格与标准价格相差超出10%，请及时修改物料标准成本！");
                                    btnAddItemToPO.Enabled = true;
                                }
                                else
                                {
                                    btnAddItemToPO.Enabled = true;
                                }
                            }
                            else if (itemType == "M")
                            {
                                if (rate == "0.1")
                                {
                                    Custom.MsgEx("该物料下达价格与标准价格相差超出10%，无法下达订单！请先修改物料标准成本");
                                    btnAddItemToPO.Enabled = false;
                                    return;
                                }
                                else if (rate == "0.05")
                                {
                                    Custom.MsgEx("该物料下达价格与标准价格相差超出5%，请及时修改物料标准成本！");
                                    btnAddItemToPO.Enabled = true;
                                }
                                else
                                {
                                    btnAddItemToPO.Enabled = true;
                                }
                            }
                            else if (itemType == "A")
                            {
                                if (rate == "0.1")
                                {
                                    Custom.MsgEx("该物料下达价格与标准价格相差超出10%，请及时修改物料标准成本");
                                }
                                else if (rate == "0.05")
                                {
                                    Custom.MsgEx("该物料下达价格与标准价格相差超出5%，请及时修改物料标准成本！");
                                }
                                else
                                {

                                }
                            }
                        }

                    }

                    tbDeliveryDate.Focus();
                }
            }
            */
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
                    cbbConfirmPerson.Focus();
                }
            }
        }

        private void tbForeignNumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                tbItemNumber.Focus();
                int abc = 0;
                if (tbForeignNumber.Text.Contains("A"))
                {
                    tbRequireDept.Text = "国际业务一部";
                    abc++;
                }
                if (tbForeignNumber.Text.Contains("B"))
                {
                    tbRequireDept.Text = "国际业务二部";
                    abc++;
                }
                if (tbForeignNumber.Text.Contains("C"))
                {
                    tbRequireDept.Text = "国际业务三部";
                    abc++;
                }
                if (tbForeignNumber.Text == "0000" || abc > 1)
                {
                    tbRequireDept.Text = "国际业务部（公用）";
                }
            }
        }

        private void tbItemNumber_TextChanged(object sender, EventArgs e)
        {
            tbItemNumber.Text = tbItemNumber.Text.Trim().ToUpper();
            tbItemNumber.SelectionStart = tbItemNumber.Text.Length;//避免光标在输入字母的前面
        }

        //获取直接对应确认到货人员的物料代码或者供应商代码
        private Dictionary<string,string> GetDirectVendorOrItemDict()
        {
            string sqlSelect = @"Select VendorOrItemNumber,BuyerID+'|'+BuyerName AS Buyer   From PurchaseDepartmentDirectVendorItem";
            return SQLHelper.GetDataTable(GlobalSpace.FSDBConnstr, sqlSelect).Rows.Cast<DataRow>().ToDictionary(r => r["VendorOrItemNumber"].ToString(), r => r["Buyer"].ToString());
        }

        private void btnAddItemToPO_Click(object sender, EventArgs e)
        {
            int isDirectFOOrder = 0;

            if(tbItemNumber.Text.Substring(0,2)=="MJ")
            {
                GlobalSpace.ShouCeNumber = string.Empty;
                Cusotoms c = new Cusotoms();
                c.ShowDialog();
                if(string.IsNullOrWhiteSpace(GlobalSpace.ShouCeNumber))
                {
                    MessageBoxEx.Show("当前为进料加工物料，请选择手册号！", "提示");
                    return;
                }
            }


            if (string.IsNullOrEmpty(cbbConfirmPerson.Text))
            {
                Custom.MsgEx("请选择确认到货人员！");
                return;
            }

            if(string.IsNullOrWhiteSpace(tbPricePostTax.Text))
            {
                Custom.MsgEx("税后价格不能为空或为0！");
                return;
            }
            if (string.IsNullOrWhiteSpace(tbRequireDept.Text) && ((tbUM.Tag.ToString() == "Y" && tbItemNumber.Text.StartsWith("A"))|| !tbItemNumber.Text.StartsWith("A")))
            {
                Custom.MsgEx("请填写需求部门！");
                return;
            }
            if ((tbPONumberInDetail.Text.Substring(0,2) == "PF" &&(tbPONumberInDetail.Text.Substring(11, 1) == "1" || tbPONumberInDetail.Text.Substring(11,1) == "2" || tbPONumberInDetail.Text.Substring(11, 1) == "3")) || tbPONumberInDetail.Text.Substring(0, 2) == "PJ")
            {
                isDirectFOOrder = 1;
              /*  if (MessageBoxEx.Show("当前采购订单为外贸部门直接采购物料，写入四班后是否直接产生入库记录？","提示",MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    isDirectFOOrder = 1;
                }*/
            }

            if((tbPONumberInDetail.Text.Trim().Substring(0,2) == "PF" || tbPONumberInDetail.Text.Trim().Substring(0, 2) == "PJ")&& string.IsNullOrEmpty(tbForeignNumber.Text))
            {
                    Custom.Msg("外贸用原辅料请填写联系单号！多个联系单号请全部填写，使用横线“A1234-B3421-C1234”的格式区分；事业部未提供时，请计划员提供虚拟联系单号。");
                    return;                
            }
            //价格差异控制---财务要求
            string sqlItemPriceCompareInsert = @"Insert Into PurchaseDepartmentPriceRestrictRecord (ItemNumber,ItemDescription,UserID,Range,UserName,StandardPrice,PlacedUnitPrice) Values ('" + tbItemNumber.Text + "','" + tbItemDescription.Text + "','" + fsuserid + "',@Range,'"+fsusername+"',@StandardPrice,@PlacedUnitPrice) ";
            List<SqlParameter> paraListPriceRecord = new List<SqlParameter>();
            bool isNeedRecord = false;
            List<string> itemNotComparePriceList = GetItemNotComparePriceList();
            if (!(tbItemNumber.Text.StartsWith("FF")))
            {
                if (PurchaseUser.PriceCompare == 0)
                {
                    double fPrice = Convert.ToDouble(tbPricePostTax.Text);
                    double standardPrice = Convert.ToDouble(lblFSItemPrice.Text);
                    //>0.1|<-0.1，>0.05 |<-0.05
                    string rate = CommonOperate.CompareItemPriceToStandardPrice(fPrice, standardPrice);

                    if (Convert.ToDouble(rate) >= 0.1)
                    {
                        Custom.MsgEx("该物料下达价格与标准价格相差超出10%，无法下达订单！请先修改物料标准成本");
                        return;
                    }
                    else if (rate == "0.05")
                    {
                        Custom.MsgEx("该物料下达价格与标准价格相差超出5%，请及时修改物料标准成本！");
                        paraListPriceRecord.Add(new SqlParameter("@Range", rate));
                        isNeedRecord = true;
                    }


                }
                else if (PurchaseUser.PriceCompare == 1 || PurchaseUser.PriceCompare == 2)
                {
                    //if(!WithoutRestrictItemList.Contains(tbItemNumber.Text))
                    //{
                    double fPrice = Convert.ToDouble(tbPricePostTax.Text);
                    double standardPrice = Convert.ToDouble(lblFSItemPrice.Text);
                    //>0.1|<-0.1，>0.05 |<-0.05
                    string rate = CommonOperate.CompareItemPriceToStandardPrice(fPrice, standardPrice);
                    if (Convert.ToDouble(rate) >= 5)
                    {
                        MessageBoxEx.Show("当前采购价格与四班标准成本相差超过5倍！无法下达订单，请先修改成本。", "提示");
                        return;
                    }
                    //}              
                }
                else if (PurchaseUser.PriceCompare == 3)
                {
                    double fPrice = Convert.ToDouble(tbPricePostTax.Text);
                    double standardPrice = Convert.ToDouble(lblFSItemPrice.Text);
                    //>0.1|<-0.1，>0.05 |<-0.05
                    string rate = CommonOperate.CompareItemPriceToStandardPrice(fPrice, standardPrice);


                    if (itemNotComparePriceList.Contains(tbItemNumber.Text))
                    {
                        if (Convert.ToDouble(rate) >= 5)
                        {
                            MessageBoxEx.Show("当前采购价格与四班标准成本相差超过5倍！无法下达订单，请先修改成本。", "提示");
                            return;
                        }
                    }
                    else
                    {
                        if (Convert.ToDouble(rate) >= 0.15)
                        {
                            Custom.MsgEx("该物料下达价格与标准价格相差超出15%，无法下达订单！请先修改物料标准成本");
                            return;
                        }
                        else if (rate == "0.1")
                        {
                            Custom.MsgEx("该物料下达价格与标准价格相差超出10%，请及时修改物料标准成本！");
                            isNeedRecord = true;
                            paraListPriceRecord.Add(new SqlParameter("@Range", rate));
                        }
                    }
                }
            }

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
Stock,Bin,InspectionPeriod,Guid,TaxRate,ParentGuid,POItemConfirmer,ItemReceiveType,Comment1,LotNumberAssign,IsFOItem,IsDirectFOOrder,EditionQuantity,GSID,RequireDept,StandardPrice,ShouCeNumber)
                                                VALUES
	                                                (
	                                                '" + tbPONumberInDetail.Text.Trim() + "',@VendorNumber, @VendorName, @ManufacturerNumber, @ManufacturerName, @ItemNumber, @ItemDescription,@LineUM, @Buyer,  @BuyerName,@Superior,@DemandDeliveryDate,@ForeignNumber, @POStatus,@ItemUsedPoint, @QualityCheckStandard,@UnitPrice,@LineType,@LineStatus,                                               @NeededDate,@PromisedDate, @POItemQuantity,@PricePreTax,@StockKeeper,@Stock,@Bin,@InspectionPeriod,@Guid,@TaxRate,@ParentGuid,@POItemConfirmer,@ItemReceiveType,@Comment1,@LotNumberAssign,@IsFOItem,@IsDirectFOOrder,@EditionQuantity,@GSID,@RequireDept,@StandardPrice,@ShouCeNumber)";

                if (cbbManufacturerList.Text == "")
                {
                    MessageBoxEx.Show("该物料有多个生产商，请进行选择！");
                }
                else if (tbPONumberInDetail.Text == "" || tbItemNumber.Text == "" || tbOrderQuantity.Text == "" || tbPricePostTax.Text == "" || tbDeliveryDate.Text == "")
                {
                    MessageBoxEx.Show("订单号、物料代码、订购数量、税后价格和交货日期不能为空！", "提示");
                }
                else if(PurchaseUser.POItemOthersConfirm == 1 && string.IsNullOrEmpty(cbbConfirmPerson.Text))
                {
                    Custom.MsgEx("需要选择确认到货人员");
                }
                else
                {
                    string strDate = tbDeliveryDate.Text.Trim();
                    string ponumber = tbPONumberInDetail.Text.Trim();
             
                List<SqlParameter> sqlparaList = new List<SqlParameter>();

                List<string> itemList = GetItemWithoutReviewList();
                
                sqlparaList.Add(new SqlParameter("@PONumber", ponumber));
                sqlparaList.Add(new SqlParameter("@VendorNumber", cbbVendorList.Text.Substring(0, 6)));
                sqlparaList.Add(new SqlParameter("@VendorName", cbbVendorList.Text.Substring(7)));
                sqlparaList.Add(new SqlParameter("@ManufacturerNumber", cbbManufacturerList.Text.Substring(0, 6)));
                sqlparaList.Add(new SqlParameter("@ManufacturerName", cbbManufacturerList.Text.Substring(7)));
                sqlparaList.Add(new SqlParameter("@ItemNumber", tbItemNumber.Text.Trim()));
                sqlparaList.Add(new SqlParameter("@ItemDescription", tbItemDescription.Text.Trim()));
                sqlparaList.Add(new SqlParameter("@LineUM", tbUM.Text.Trim()));
                sqlparaList.Add(new SqlParameter("@Buyer", fsuserid));
                sqlparaList.Add(new SqlParameter("@BuyerName", fsusername));
                sqlparaList.Add(new SqlParameter("@Superior", PurchaseUser.SupervisorID));
                sqlparaList.Add(new SqlParameter("@DemandDeliveryDate", strDate));
                sqlparaList.Add(new SqlParameter("@ForeignNumber", tbForeignNumber.Text.Trim()));
                sqlparaList.Add(new SqlParameter("@ItemUsedPoint ",""));
                sqlparaList.Add(new SqlParameter("@QualityCheckStandard", tbQualityCheckStandard.Text.Trim()));
                sqlparaList.Add(new SqlParameter("@UnitPrice", Convert.ToDouble(tbPricePostTax.Text.Trim())));
                sqlparaList.Add(new SqlParameter("@LineType","P"));
                sqlparaList.Add(new SqlParameter("@LineStatus","4"));
                sqlparaList.Add(new SqlParameter("@NeededDate", strDate));
                sqlparaList.Add(new SqlParameter("@PromisedDate", strDate));
                sqlparaList.Add(new SqlParameter("@POItemQuantity", Convert.ToDouble(tbOrderQuantity.Text.Trim())));
                sqlparaList.Add(new SqlParameter("@EditionQuantity",Convert.ToInt32(tbEditionQuantity.Text)));
                sqlparaList.Add(new SqlParameter("@RequireDept", tbRequireDept.Text));
                sqlparaList.Add(new SqlParameter("@StandardPrice", Convert.ToDouble(lblFSItemPrice.Text)));
                sqlparaList.Add(new SqlParameter("@ShouCeNumber", GlobalSpace.ShouCeNumber+ ponumber.Substring(10,3)));

                if (StockSpecialItemList.Contains(tbItemNumber.Text.Trim()))
                {
                    sqlparaList.Add(new SqlParameter("@StockKeeper", SpecialItemKeeper));
                }
                else
                {
                    sqlparaList.Add(new SqlParameter("@StockKeeper", itemKeeper.Trim()));
                }
                sqlparaList.Add(new SqlParameter("@Stock", iteminfoList[1]));
                sqlparaList.Add(new SqlParameter("@Bin", iteminfoList[2]));
                sqlparaList.Add(new SqlParameter("@InspectionPeriod", iteminfoList[0]));
                sqlparaList.Add(new SqlParameter("@Guid", Guid.NewGuid().ToString("N")));
                sqlparaList.Add(new SqlParameter("@TaxRate", cbbTaxRate.SelectedItem.ToString()));
                sqlparaList.Add(new SqlParameter("@ParentGuid", tbPONumberInDetail.Tag.ToString()));
                sqlparaList.Add(new SqlParameter("@POItemConfirmer", cbbConfirmPerson.Text.Split('|')[0].ToUpper()));
                if(PurchaseUser.UserStatus == 2)
                {
                    sqlparaList.Add(new SqlParameter("@ItemReceiveType", tbItemNumber.Text.Substring(0,1)));
                }
                else
                {
                    sqlparaList.Add(new SqlParameter("@ItemReceiveType", PurchaseUser.ItemReceiveType));
                }                
                sqlparaList.Add(new SqlParameter("@Comment1", tbRemark.Text));
                string lotNumberAssign = string.Empty;
                if(tbUM.Tag.ToString() == "Y")
                {
                    lotNumberAssign = "C";
                }
                else
                {
                    lotNumberAssign = "";
                }

                if(PurchaseUser.UserPOType == "F")
                {
                    sqlparaList.Add(new SqlParameter("@IsFOItem", "1"));
                }
                else
                {
                    sqlparaList.Add(new SqlParameter("@IsFOItem", "0"));
                }

                sqlparaList.Add(new SqlParameter("@LotNumberAssign", lotNumberAssign));
                sqlparaList.Add(new SqlParameter("@IsDirectFOOrder", isDirectFOOrder));
                sqlparaList.Add(new SqlParameter("@GSID", Convert.ToInt32(btnAddItemToPO.Tag)));
                //分为两大类：内销和外贸，外贸下单人员逐个下单的物料不需要审核；需要审核的人员里边部分物料可能是无需审核的
                if (PurchaseUser.UserStatus == 1)
                {
                    if(itemList.Contains(tbItemNumber.Text.Trim()))
                    {
                        sqlparaList.Add(new SqlParameter("@POStatus", "2"));
                    }
                    else
                    {
                        sqlparaList.Add(new SqlParameter("@POStatus", "0"));
                    }                  
                }
                else if (PurchaseUser.UserStatus == 0)
                {
                    sqlparaList.Add(new SqlParameter("@POStatus", "2"));
                }
                else if (PurchaseUser.UserStatus == 2|| PurchaseUser.UserStatus == 3)
                {
                    sqlparaList.Add(new SqlParameter("@POStatus", "2"));
                }

                if (string.IsNullOrEmpty(tbPricePreTax.Text))
                {
                    sqlparaList.Add(new SqlParameter("@PricePreTax", "0"));
                }
                else
                {
                    sqlparaList.Add(new SqlParameter("@PricePreTax", Convert.ToDouble(tbPricePreTax.Text.Trim())));
                }

                SqlParameter[] sqlparams = sqlparaList.ToArray();


                if (SQLHelper.ExecuteNonQuery(GlobalSpace.FSDBConnstr, strInsert, sqlparams) )
                {
                    MessageBoxEx.Show("物料添加成功", "提示");

                if(isNeedRecord)
                {
                    paraListPriceRecord.Add(new SqlParameter("@StandardPrice", Convert.ToDouble(lblFSItemPrice.Text.Trim())));
                    paraListPriceRecord.Add(new SqlParameter("@PlacedUnitPrice", Convert.ToDouble(tbPricePostTax.Text.Trim())));
                    SqlParameter[] sqlparaPrice = paraListPriceRecord.ToArray();
                    //需要修改物料标准成本的物料写入四班。
                    if (!string.IsNullOrEmpty(sqlItemPriceCompareInsert))
                    {
                        if (!SQLHelper.ExecuteNonQuery(GlobalSpace.FSDBConnstr, sqlItemPriceCompareInsert, sqlparaPrice))
                        {
                            MessageBoxEx.Show("记录该物料信息时出错！", "提示");
                            return;
                        }
                    }
                }
                   
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

                if(tbUM.Text.Trim() == "WA")
                {
                    MessageBoxEx.Show("该物料单位为WA，请注意采购数量的准确性！", "提示");
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

                if(Convert.ToInt32(btnAddItemToPO.Tag) != 0)
                {
                    string sqlUpdateGS = @"Update SolidBuyList Set PurChaseNumber='" + tbPONumberInDetail.Text + "',Flag=1 Where ID=" + Convert.ToInt32(btnAddItemToPO.Tag) + "";

                    if (!SQLHelper.ExecuteNonQuery(GlobalSpace.RYData, sqlUpdateGS))
                    {
                        MessageBoxEx.Show("该物料为固水事业部需求物料，更新状态失败，请联系管理员！", "提示");
                    }
                }
                


                tbPricePostTax.Text = "";
                tbItemNumber.Text = "";
                tbDeliveryDate.Text = "";
                tbOrderQuantity.Text = "";
                tbPricePreTax.Text = "";
                tbItemDescription.Text = "";
                tbUM.Text = "";
                tbForeignNumber.Text = "";
            
                cbbManufacturerList.Text = "";
                cbbVendorList.Text = "";
                tbQualityCheckStandard.Text = "";
                tbRemark.Text = "";
                tbForeignNumber.Focus();
                      }
          //  }
//             else
//             {
//                 MessageBoxEx.Show("该订单状态（非已准备或已提交）不允许下达物料！", "提示");
//             }

        }
        
        //查询物料是否需要检验
        private List<string> GetItemInfo(string itemNumber)
        {
            List<string> list = new List<string>();
            DataTable dtTemp = null;
            string sqlSelect = @"Select  ItemDescription,ItemUM,IsInspectionRequired,PreferredStockroom,PreferredBin,IsLotTraced From _NoLock_FS_Item Where ItemNumber='" + itemNumber + "'";
            dtTemp = SQLHelper.GetDataTableOleDb(GlobalSpace.oledbconnstrFSDBMR, sqlSelect);
            if (dtTemp.Rows.Count > 0)
            {
                list.Add(dtTemp.Rows[0]["IsInspectionRequired"].ToString());
                list.Add(dtTemp.Rows[0]["PreferredStockroom"].ToString());
                list.Add(dtTemp.Rows[0]["PreferredBin"].ToString());
                list.Add(dtTemp.Rows[0]["ItemDescription"].ToString());
                list.Add(dtTemp.Rows[0]["ItemUM"].ToString());
                list.Add(dtTemp.Rows[0]["IsLotTraced"].ToString());
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
            ShowOrder("Special");
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
                    dgvr.Cells["dgvPOCheck"].Value = true;
                }
            }
        }

        private void btnCancelAllChecked_Click(object sender, EventArgs e)
        {
            if (dgvPO.Rows.Count > 0)
            {
                foreach (DataGridViewRow dgvr in dgvPO.Rows)
                {
                    dgvr.Cells["dgvPOCheck"].Value = false;
                }
            }
        }

        private void btnSubmitPO_Click(object sender, EventArgs e)
        {
            //MessageBoxEx.Show("此处更新订单中物料状态的SQL语句不对，还需要增加行号的条件");
            //return;
            int  jcount = 0;
            List<string> poGuidList = new List<string>();
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
                        string sqlSelectPOStatus = @"Select Count(Id) From PurchaseOrderRecordByCMF Where IsPurePO = 0 And PONumber = '"+dgvr.Cells["采购单号"].Value.ToString()+"' And POStatus = 0";
                        //订单状态已选中同时为已准备
                        if (Convert.ToBoolean(dgvr.Cells["dgvPOCheck"].Value) == true && SQLHelper.Exist(GlobalSpace.FSDBConnstr,sqlSelectPOStatus))
                        {
                            poToSuperiorList.Add(dgvr.Cells["采购单号"].Value.ToString());
                            poGuidList.Add(dgvr.Cells["Guid"].Value.ToString());
                        }
                    }

                    /*
                    if (poToSuperiorList.Count > 0)
                    {
                        for (int i = 0; i < poToSuperiorList.Count; i++)
                        {
                                if (!BuyeUpdatePOItemStatus(poToSuperiorList[i], 1))
                                {
                                    jcount++;
                                }                            
                        }
                    }*/
                    if(PurchaseUser.UserStatus == 0)
                    {
                        if (poGuidList.Count > 0)
                        {
                            for (int i = 0; i < poGuidList.Count; i++)
                            {
                                if (!BuyeUpdatePOItemStatus(poGuidList[i], 2))
                                {
                                    jcount++;
                                }
                            }
                        }
                        else
                        {
                            MessageBoxEx.Show("当前没有可以进行提交的订单！", "提示");
                        }
                    }
                    else if(PurchaseUser.UserStatus == 1)
                    {
                        List<string> itemList = GetItemWithoutReviewList();
                        if (poGuidList.Count > 0)
                        {
                            for (int i = 0; i < poGuidList.Count; i++)
                            {
                                if (!BuyeUpdatePOItemStatus(poGuidList[i], 1))
                                {
                                    jcount++;
                                }
                            }
                        }
                        else
                        {
                            MessageBoxEx.Show("当前没有可以进行提交的订单！", "提示");
                        }
                    }

                    if (jcount > 0)
                    {
                        MessageBoxEx.Show("订单中物料更新状态不完全,请确认！");
                    }
                    else if ( jcount  == 0)
                    {
                        MessageBoxEx.Show("订单提交成功！", "提示");
                        List<string> listSuper = CommonOperate.GetSuperiorNameAndEmail(fsuserid);
                        string sqlSelectUserInfo = @"Select Email,Password,Name From PurchaseDepartmentRBACByCMF Where UserID='" + fsuserid + "'";
                        DataTable dtUserInfo = SQLHelper.GetDataTable(GlobalSpace.FSDBConnstr, sqlSelectUserInfo);
                        string supername = listSuper[0];
                        string supermail = listSuper[1];

                        if (dtUserInfo.Rows.Count > 0)
                        {
                            if (dtUserInfo.Rows[0]["Email"] != DBNull.Value && dtUserInfo.Rows[0]["Email"].ToString() != "")
                            {
                                List<string> smtpList = CommonOperate.GetSMTPServerInfo();
                                if (smtpList.Count > 0)
                                {
                                    Email email = new Email();
                                    email.fromEmail = dtUserInfo.Rows[0]["Email"].ToString();
                                    email.fromPerson = dtUserInfo.Rows[0]["Name"].ToString();
                                    email.toEmail = supermail;
                                    email.toPerson = supername;
                                    email.encoding = "UTF-8";
                                    email.smtpServer = smtpList[0];
                                    email.userName = dtUserInfo.Rows[0]["Email"].ToString();
                                    email.passWord = CommonOperate.Base64Decrypt(dtUserInfo.Rows[0]["Password"].ToString());
                                    email.emailTitle = "采购订单审核提醒";
                                    email.emailContent = supername +"处长"+ "：" + "<br>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + "您好!<br>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;采购员已提交采购订单申请，请及时审批！";

                                    if (MailHelper.SendReminderEmail(email))
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
                            else
                            {
                                MessageBoxEx.Show("邮箱未设置！", "提示");
                            }
                        }

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
        private bool BuyeUpdatePOItemStatus(string guid, int status)
        {
            string strUpdatePO = @" Update PurchaseOrderRecordByCMF Set POStatus = '" + status + "' Where ParentGuid='" + guid + "' And IsPurePO = 0 And POStatus = 0";
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
            int icount = 0;
            List<string> poItemGuidList = new List<string>();
            List<string> sqlList = new List<string>();
            List<string> itemIdList = new List<string>();
            List<string> sqlReceiveList = new List<string>();
            string strUpdate = string.Empty;
            //       MessageBox.Show(fsuserid + "-" + fspassword);
            FSFunctionLib.FSConfigFileInitialize(GlobalSpace.fsconfigfilepath, fsuserid, fspassword);
            DataTable dtTemp = null;

            //清空poList中元素
            
            if (poList.Count > 0)
            {
                poList.Clear();
            }

            if (itemIdList.Count > 0)
            {
                itemIdList.Clear();
            }

            if (dgvPO.Rows.Count > 0)
            {
                try
                {
                    
                    foreach (DataGridViewRow dgvr in dgvPO.Rows)
                    {
                        string sqlSelectPOStatus = @"Select Count(Id) From PurchaseOrderRecordByCMF Where IsPurePO = 0 And PONumber = '" + dgvr.Cells["采购单号"].Value.ToString() + "' And POStatus = 2";
                        if (Convert.ToBoolean(dgvr.Cells["dgvPOCheck"].Value) == true && SQLHelper.Exist(GlobalSpace.FSDBConnstr,sqlSelectPOStatus))
                        {
                            poItemGuidList.Add(dgvr.Cells["Guid"].Value.ToString());
                            poList.Add(dgvr.Cells["采购单号"].Value.ToString());
                            if (!IsPOExist(dgvr.Cells["采购单号"].Value.ToString()))
                            {
                                if (!PlaceFSPurchaseOrder(dgvr.Cells["采购单号"].Value.ToString(), fsuserid, dgvr.Cells["供应商码"].Value.ToString()))
                                {
                                    poErrorList.Add(dgvr.Cells["采购单号"].Value.ToString());
                                }
                                else
                                {
                                    icount++;
                                }                               
                            }
                            else
                            {
                                MessageBoxEx.Show("订单号"+ dgvr.Cells["采购单号"].Value.ToString() + "已存在！","提示");                              
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
                                        string lineNumberString = GetFSPOItemLineString(poList[i], dr["物料代码"].ToString());//查询物料在四班订单中下达后的行号
                                        if (string.IsNullOrEmpty(lineNumberString))
                                        {
                                            Custom.MsgEx("未查到物料"+ dr["物料代码"].ToString() + "的行号，请联系管理员！");
                                        }
                                        if(PurchaseUser.UserStatus == 2 || PurchaseUser.UserStatus == 3 || dr["IsDirectPurchaseVial"].ToString() == "1"|| dr["IsDirectFOOrder"].ToString() == "1")//五金采购员和大客户，直接产生到货记录
                                        {
                                            strUpdate = @"Update PurchaseOrderRecordByCMF Set POStatus= 4,ActualDeliveryDate='"+DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss")+"',LineNumber = '" + lineNumberString + "',FSadddt=GETDATE()  Where  Id='" + dr["Id"].ToString() + "'";
                                            itemIdList.Add(dr["Id"].ToString());
                                        }
                                        else
                                        {
                                            strUpdate = @"Update PurchaseOrderRecordByCMF Set POStatus= 3,LineNumber = '" + lineNumberString + "',FSadddt=GETDATE()  Where  Id='" + dr["Id"].ToString() + "'";
                                        }
                                        sqlList.Add(strUpdate);
                                     //   UpdatePOItemStatus(poList[i], dr);
                                    //    jcount++;
                                    }
                                }
                            }
                        }
                    }
                    if (SQLHelper.BatchExecuteNonQuery(GlobalSpace.FSDBConnstr,sqlList))
                    {
                        MessageBoxEx.Show("所有物料下达成功！", "提示");
                       
                        if(itemIdList.Count > 0)
                        {
                            if (!CreatePOItemReceiveHistory(itemIdList))
                            {
                                MessageBoxEx.Show("产生入库记录失败，请联系管理员！", "提示");
                            }
                        }
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
            ShowPOItemDetail(poList[0]);
        }
        //产生入库记录
        private bool CreatePOItemReceiveHistory(List<string> idList)
        {
            List<string> sqlList = new List<string>();
            for (int i = 0; i < idList.Count; i++)
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
	                                                            [BuyerID],Stock,Bin,ReceiveDate,Guid,UnitPrice,
                                                                QualityCheckStandard
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
                                                    [ForeignNumber],Buyer,Stock,Bin,'" + DateTime.Now.ToString("yyyy-MM-dd")+ "',Replace(NEWID(),'-',''),UnitPrice,QualityCheckStandard  FROM   PurchaseOrderRecordByCMF  WHERE Id = '" + idList[i] + "'";
                  //     MessageBox.Show(sqlInsert);
                sqlList.Add(sqlInsert);
            }

            if (SQLHelper.BatchExecuteNonQuery(GlobalSpace.FSDBConnstr, sqlList))
            {
                return true;
            }
            return false;
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
	                                            T1.DemandDeliveryDate AS 需求日期,IsDirectPurchaseVial,IsDirectFOOrder
                                        FROM
	                                        PurchaseOrderRecordByCMF T1
                                        WHERE
	                                        T1.PONumber = '" + ponumber + "' And T1.POStatus = 2";
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
        private bool UpdatePOItemStatus(string ponumber, DataRow dr,int status)
        {
            string lineNumberString = GetFSPOItemLineString(ponumber, dr["物料代码"].ToString());//查询物料在四班订单中下达后的行号
            if (string.IsNullOrEmpty(lineNumberString))
            {
                Custom.MsgEx("未查到该物料的行号，请联系管理员！");
            }

            string strUpdate = @"Update PurchaseOrderRecordByCMF Set POStatus= " + status + ",LineNumber = '" + lineNumberString + "'  Where PONumber='" + ponumber + "' And ItemNumber='" + dr["物料代码"].ToString() + "' And  Id='" + dr["Id"].ToString() + "' And POStatus = 2";
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
                if (Convert.ToBoolean(dgvr.Cells["dgvPOCheck"].Value) == true)
                {
                    isExist = true;
                }
            }
            if (isExist)
            {
                List<string> sqlList = new List<string>();
                string sqlCheck = @"Select Id from PurchaseDepartmentCheckPasswordByCMF Where Supervisor ='" + PurchaseUser.SupervisorID + "' And IsValid = 0";
                string sqlSelect = @"Select Password from PurchaseDepartmentCheckPasswordByCMF Where Supervisor ='" + PurchaseUser.SupervisorID + "' And IsValid = 0";
                foreach (DataGridViewRow dgvr in dgvPO.Rows)
                {
                    //订单状态已选中同时为领导已审核状态
                    if (Convert.ToBoolean(dgvr.Cells["Checked"].Value) == true)
                    {
                        string sqlUpdatePOItem = @"Update PurchaseOrderRecordByCMF Set POStatus = 1, CheckedWay = 'Password'  Where PONumber='" + dgvr.Cells["采购单号"].Value.ToString() + "' And IsPurePO = 0 And POStatus = 0";               
                        sqlList.Add(sqlUpdatePOItem);
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
            tbPOForSearch.Text = tbPOForSearch.Text.Trim().ToUpper();
            if (GlobalSpace.vendorEmailList != null)
            {
                if (GlobalSpace.vendorEmailList.Count > 0)
                {
                    GlobalSpace.vendorEmailList.Clear();
                }
            }

            string filePath = string.Empty;

            //判断当前订单导出的Excel文件是否存在
            string sqlSelect = @"Select Name,FilePath From PurchaseDepartmentFilePathByCMF Where BuyerID='" + fsuserid + "' And Status = 0 And Name = '采购订单导出路径'";

            DataTable dtTemp = SQLHelper.GetDataTable(GlobalSpace.FSDBConnstr, sqlSelect);
            if (dtTemp.Rows.Count > 0)
            {
                filePath = dtTemp.Rows[0]["FilePath"].ToString();
            }
            if (!string.IsNullOrWhiteSpace(tbPOForSearch.Text))
            {
                DataRow dr3 = GetVendorIDAndName(tbPOForSearch.Text);
                string vendorNumber = dr3["VendorNumber"].ToString();
                string vendorName = dr3["VendorName"].ToString();
                if (File.Exists(filePath + "\\" +  tbPOForSearch.Text + ".xlsx"))
                {

                    VendorEmailSetting Ves = new VendorEmailSetting(fsuserid, tbPOForSearch.Text.Trim().ToUpper(), filePath + "\\" +  tbPOForSearch.Text + ".xlsx", vendorNumber, vendorName);
                    Ves.ShowDialog();
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
            tbPOForSearch.Text= tbPOForSearch.Text.Trim();
            if (tbPOForSearch.Text != "")
            {
                if (GetVendorPOItemsDetail(tbPOForSearch.Text.Trim()) != null)
                {
                    dgvPO2.DataSource = GetVendorPOItemsDetail(tbPOForSearch.Text.Trim());
                    dgvPO2.Columns["GUID"].Visible = false;
                }
            }

        }
        //通过订单号获取订单中物料详情用于发票导出
        private DataTable GetInvoiceVendorPOItemsDetail(string ponumber)
        {
            DataTable dtTemp = null;
            string strSqlCheck = @"Select Id From  PurchaseOrderRecordByCMF Where PONumber='" + ponumber + "'";
            string strSql = @"SELECT
                                                T1.Guid AS GUID,
                                                T1.LineNumber AS 行号,
	                                        	T1.ItemNumber AS 物料代码,
	                                            T1.ItemDescription AS 物料描述,
	                                            T1.LineUM AS 单位,	                                         
	                                            T1.UnitPrice AS 单价,
	                                            T1.ForeignNumber AS 外贸单号,
                                                T1.PORVQuantity AS 入库数量,
                                                (T1.UnitPrice*T1.PORVQuantity) As 合计
                                        FROM
	                                        PurchaseOrderRecordByCMF T1
                                        WHERE
	                                        T1.PONumber = '" + ponumber + "' And T1.POStatus >= 6";

            if (SQLHelper.Exist(GlobalSpace.FSDBConnstr, strSqlCheck))
            {

                dtTemp = SQLHelper.GetDataTable(GlobalSpace.FSDBConnstr, strSql);
                DataRow drNew = dtTemp.NewRow();
                drNew[0] = "合计";
                /*如果还有其他列需要计算求和，可以进行遍历或者单独计算
                for(int i = 1; i < dtTemp.Columns.Count; i++)
                {

                }*/
                drNew[dtTemp.Columns.Count] = dtTemp.Compute(string.Format("SUM({0})", dtTemp.Columns[dtTemp.Columns.Count]), "true");
                dtTemp.Rows.Add(drNew);
            }
            else
            {
                MessageBoxEx.Show("该订单不存在！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            return dtTemp;
        }
        //通过订单号获取订单中物料详情
        private DataTable GetVendorPOItemsDetail(string ponumber)
        {
            DataTable dtTemp = null;
            string strSqlCheck = @"Select Count(Id) From  PurchaseOrderRecordByCMF Where PONumber='" + ponumber + "' And IsPurePO = 1";
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
                                                (case T1.POStatus  when  '-1' then '已取消' when  '0' then '已准备'
                                                           when  '1' then '已提交'
                                                         when  '2' then '已审核'
                                                         when  '3' then '已下达' 
                                                        when  '4' then '已到货' 
                                                        when  '5' then '已收货' 
                                                        when  '6' then '已入库' 
                                                        when  '7' then '已开票' 
                                                        when  '66' then '多次到货' 
                                                 end     
                                                ) as POStatus  ,ManufacturerNumber,ManufacturerName 
                                        FROM
	                                        PurchaseOrderRecordByCMF T1
                                        WHERE
	                                        T1.PONumber = '" + ponumber + "' And T1.POStatus <> 99 And IsPurePO = 0";

            if (SQLHelper.Exist(GlobalSpace.FSDBConnstr, strSqlCheck))
            {

                dtTemp = SQLHelper.GetDataTable(GlobalSpace.FSDBConnstr, strSql);
                DataRow drNew = dtTemp.NewRow();
                drNew[1] = "合计";
                /*如果还有其他列需要计算求和，可以进行遍历或者单独计算
                for(int i = 1; i < dtTemp.Columns.Count; i++)
                {

                }*/
                //drNew[dtTemp.Columns.Count - 5] = dtTemp.Compute(string.Format("SUM({0})", dtTemp.Columns[dtTemp.Columns.Count - 5]), "true");
                drNew["ItemSum"] = dtTemp.Compute(string.Format("SUM({0})", dtTemp.Columns["ItemSum"]), "true");
                dtTemp.Rows.Add(drNew);
            }
            else
            {
                MessageBoxEx.Show("该订单不存在！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            return dtTemp;
        }
        //通过联系单号获取订单中物料详情
        private DataTable GetVendorPOItemsDetailByFONumber(string fonumber)
        {
            DataTable dtTemp = null;
            string strSqlCheck = @"Select Id From  PurchaseOrderRecordByCMF Where ForeignNumber='" + fonumber + "'";
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
	                                        T1.ForeignNumber = '" + fonumber + "' And T1.POStatus <> 99";

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
        //该用户下达的订单是否已经存在
        private bool IsPOExist(string ponumber, string userid)
        {
            string strSqlCheck = @"Select Count(Id) From  PurchaseOrderRecordByCMF Where PONumber='" + ponumber + "' And Buyer='" + userid + "'";
            if (SQLHelper.Exist(GlobalSpace.FSDBConnstr, strSqlCheck))
            {
                return true;
            }
            return false;
        }
        //该用户下达的该供应商是否已存在订单了
        private bool IsVendorPOExist(string vendorNumber,string userid)
        {
            string strSqlCheck = @"Select Count(Id) From  PurchaseOrderRecordByCMF Where VendorNumber='" + vendorNumber + "' And Buyer='" + userid + "' And POItemPlacedDate = '"+DateTime.Now.ToString("yyyy-MM-dd")+"'";
            if (SQLHelper.Exist(GlobalSpace.FSDBConnstr, strSqlCheck))
            {
                return true;
            }
            return false;
        }
        //该用户下达的该供应商是否已存在订单了
        private bool IsVendorPOExist(string vendorNumber)
        {
            string strSqlCheck = @"Select Count(Id) From  PurchaseOrderRecordByCMF Where VendorNumber='" + vendorNumber + "' And POItemPlacedDate = '" + DateTime.Now.ToString("yyyy-MM-dd") + "'";
            if (SQLHelper.Exist(GlobalSpace.FSDBConnstr, strSqlCheck))
            {
                return true;
            }
            return false;
        }
        //通过供应商代码获取该供应商所有订单，按照日期降序排序
        private DataTable GetVendorAllPO(string vendornumber, string userid)
        {
            string strSelect = @"Select distinct PONumber AS 采购单号 From PurchaseOrderRecordByCMF Where VendorNumber='" + vendornumber + "' And Buyer='" + userid + "'";
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
	                                PurchaseOrderRecordByCMF T1
                                WHERE                                   
                                    T1.Buyer ='" + userid + "' AND T1.PONumber ='" + ponumber + "' And IsPurePO = 0";
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
	                                PurchaseOrderRecordByCMF T1
                                WHERE                                   
                                    T1.Buyer ='" + userid + "' AND T1.VendorNumber ='" + vendornumber + "' And IsPurePO = 0";
            return SQLHelper.GetDataTable(GlobalSpace.FSDBConnstr, strSelect);
        }


        private void tbVendorNumber2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                if(tbVendorNumber2.Text != "")
                {
                    dgvPO3.DataSource = GetVendorAllPO(tbVendorNumber2.Text.Trim(), fsuserid);
                }
                else
                {
                    Custom.MsgEx("供应商码不能为空！");
                }
            }
        }


        private void cbbPO_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnSearchPO2_Click(sender, e);
            }
        }



        private void btnExportToExcel_Click(object sender, EventArgs e)
        {
            string sqlSelect = @"Select Name,FilePath From PurchaseDepartmentFilePathByCMF Where BuyerID='" + fsuserid + "' And Status = 0 And Name = '" + "采购订单导出路径" + "'";
            DataTable dtTemp = SQLHelper.GetDataTable(GlobalSpace.FSDBConnstr, sqlSelect);
            if (dtTemp.Rows.Count > 0)
            {
                string poNumber = string.Empty;
                string filePath = dtTemp.Rows[0]["FilePath"].ToString();

                if(!Directory.Exists(filePath))
                {
                    Custom.MsgEx("当前导出路径未设置，请先设置导出路径！");
                    return;
                }

                //获取通过订单查询供应商代码和名称
                if (tbPOForSearch.Text != "")
                {
                    poNumber = tbPOForSearch.Text;
                }
                else
                {
                    Custom.MsgEx("订单号不能为空！");
                    return;
                }
                DataRow dr3 = GetVendorIDAndName(poNumber);
                string vendorNumber = dr3["VendorNumber"].ToString();
                string vendorName = dr3["VendorName"].ToString();
                if (File.Exists(filePath + "\\" +  poNumber + ".xlsx"))
                {
                    if (MessageBoxEx.Show("该订单的导出文件已经存在，确定要覆盖么？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.OK)
                    {
                        

                        if (dgvPO2.Rows.Count > 0)
                        {
                            //由于Dgv控件数据源中有多个不需要的列，此处新建表只构造需要的列

                            DataTable dtOrigin = (DataTable)dgvPO2.DataSource;
                            DataTable dtNew = new DataTable();
                            dtNew.Columns.Add(dtOrigin.Columns["ForeignNumber"].Caption, typeof(string));
                            dtNew.Columns.Add(dtOrigin.Columns["LineNumber"].Caption, typeof(string));
                            dtNew.Columns.Add(dtOrigin.Columns["ItemNumber"].Caption, typeof(string));
                            dtNew.Columns.Add(dtOrigin.Columns["ItemDescription"].Caption, typeof(string));
                            dtNew.Columns.Add(dtOrigin.Columns["ItemUM"].Caption, typeof(string));
                            dtNew.Columns.Add(dtOrigin.Columns["OrderQuantity"].Caption, typeof(double));
                            dtNew.Columns.Add(dtOrigin.Columns["UnitPrice"].Caption, typeof(double));                            
                            dtNew.Columns.Add(dtOrigin.Columns["ItemSum"].Caption, typeof(double));
                            dtNew.Columns.Add(dtOrigin.Columns["DemandDate"].Caption, typeof(string));

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
                                dr2["LineNumber"] = dr["LineNumber"];

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
                    

                    if (dgvPO2.Rows.Count > 0)
                    {
                        //由于Dgv控件数据源中有多个不需要的列，此处新建表只构造需要的列

                        DataTable dtOrigin = (DataTable)dgvPO2.DataSource;
                        DataTable dtNew = new DataTable();
                        dtNew.Columns.Add(dtOrigin.Columns["ForeignNumber"].Caption, typeof(string));
                        dtNew.Columns.Add(dtOrigin.Columns["LineNumber"].Caption, typeof(string));
                        dtNew.Columns.Add(dtOrigin.Columns["ItemNumber"].Caption, typeof(string));
                        dtNew.Columns.Add(dtOrigin.Columns["ItemDescription"].Caption, typeof(string));
                        dtNew.Columns.Add(dtOrigin.Columns["ItemUM"].Caption, typeof(string));
                        dtNew.Columns.Add(dtOrigin.Columns["OrderQuantity"].Caption, typeof(double));
                        dtNew.Columns.Add(dtOrigin.Columns["UnitPrice"].Caption, typeof(double));                     
                        dtNew.Columns.Add(dtOrigin.Columns["ItemSum"].Caption, typeof(double));
                        dtNew.Columns.Add(dtOrigin.Columns["DemandDate"].Caption, typeof(string));
                        

                        DataRow dr2 = null;
                        foreach (DataRow dr in dtOrigin.Rows)
                        {
                            dr2 = dtNew.NewRow();
                            dr2["ForeignNumber"] = dr["ForeignNumber"];
                            dr2["ItemNumber"] = dr["ItemNumber"];
                            dr2["ItemDescription"] = dr["ItemDescription"];
                            dr2["ItemUM"] = dr["ItemUM"];
                            dr2["UnitPrice"] = dr["UnitPrice"];
                            dr2["OrderQuantity"] = dr["OrderQuantity"];
                            dr2["ItemSum"] = dr["ItemSum"];
                            dr2["DemandDate"] = dr["DemandDate"];
                            dr2["LineNumber"] = dr["LineNumber"];

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
                sheet1.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(0, 0, 0, 8));  //合并单元格
                sheet1Title.CellStyle = ExcelHelper.GetTitleCellStyle(workbook);
                IRow currentrow = sheet1.GetRow(0);//获取第一行
                for(int m = 0;m < currentrow.Cells.Count; m++)
                {
                    currentrow.Cells[m].CellStyle = ExcelHelper.GetTitleCellStyle(workbook);
                }

                sheet1.PrintSetup.Landscape = true;
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

                cell = row.CreateCell(8);
                cell.SetCellValue("");
                cell.CellStyle = cellStyle1;

                CellRangeAddress region = new CellRangeAddress(1, 1, 0, 8);
                sheet1.AddMergedRegion(region);

                //表头数据。此处由于前边设置问题，导致列标题为英文，所以手动设定为中文
                row2 = sheet1.CreateRow(2);

                cell = row2.CreateCell(0);
                cell.SetCellValue("外贸单号");
                cell.CellStyle = cellStyle1;

                cell = row2.CreateCell(1);
                cell.SetCellValue("行号");
                cell.CellStyle = cellStyle1;

                cell = row2.CreateCell(2);
                cell.SetCellValue("物料代码");
                cell.CellStyle = cellStyle1;


                cell = row2.CreateCell(3);
                cell.SetCellValue("物料描述");
                cell.CellStyle = cellStyle1;

                cell = row2.CreateCell(4);
                cell.SetCellValue("单位");
                cell.CellStyle = cellStyle1;

                cell = row2.CreateCell(5);
                cell.SetCellValue("订购数量");
                cell.CellStyle = cellStyle1;

                cell = row2.CreateCell(6);
                cell.SetCellValue("单价");
                cell.CellStyle = cellStyle1;

                cell = row2.CreateCell(7);
                cell.SetCellValue("合计");
                cell.CellStyle = cellStyle1;

                cell = row2.CreateCell(8);
                cell.SetCellValue("需求日期");
                cell.CellStyle = cellStyle1;
         

                // 写入数据
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataRow dataR = dt.Rows[i];
                    row = sheet1.CreateRow(i + 3);
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {                      

                        if (dataR[j] == DBNull.Value || dataR[j].ToString() =="")
                        {
                            cell = row.CreateCell(j);
                            cell.SetCellValue("");
                            cell.CellStyle = cellStyle2;
                        }
                        else
                        {
                            if(j == 5 || j==6 || j==7)
                            {
                                cell = row.CreateCell(j);
                                cell.SetCellValue(Convert.ToDouble(dataR[j]));
                                cell.CellStyle = cellStyle2;
                            }
                            else
                            {
                                cell = row.CreateCell(j);
                                cell.SetCellValue(dataR[j].ToString().Trim());
                                cell.CellStyle = cellStyle2;
                            }

                        }
                    }                
                }

                if (!Directory.Exists(filePath))  //检查是否存在文件夹，不存在则新建
                {
                    Directory.CreateDirectory(filePath);
                }
                /*
                for(int k = 0; k < dt.Columns.Count; k++)
                {
                    sheet1.AutoSizeColumn(k);//从第0列开始，设置列的宽度为自适应宽度。注意：这里的自适应宽度其实要比实际宽度多不少的，所以编写了再次调整的方法
                }
                */
                AutosizeColumnWidth(sheet1, dt.Columns.Count-1);
                
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


        //调整NPOI中ISheet中列的宽度
        public void AutosizeColumnWidth(ISheet sheet, int cols)
        {
            for (int col = 0; col <= cols; col++)
            {
                sheet.AutoSizeColumn(col);
                int columnWidth = sheet.GetColumnWidth(col) / 256;//获取当前列宽度
                for (int rowIndex = 2; rowIndex <= sheet.LastRowNum; rowIndex++)
                {                    
                    IRow row = sheet.GetRow(rowIndex);
                    ICell cell = row.GetCell(col);
                    int contextLength = Encoding.UTF8.GetBytes(cell.ToString()).Length;//获取当前单元格的内容宽度
                    columnWidth = columnWidth < contextLength ? contextLength : columnWidth;                   
                    /*
                    IRow currentRow;
                    if(sheet.GetRow(rowIndex) == null)
                    {
                        currentRow = sheet.CreateRow(rowIndex);
                    }
                    else
                    {
                        currentRow = sheet.GetRow(rowIndex);
                    }
                    if(currentRow.GetCell(col) != null)
                    {
                        ICell currentCell = currentRow.GetCell(col);
                        int length = Encoding.Default.GetBytes(currentCell.ToString()).Length;
                        if(columnWidth < length)
                        {
                            columnWidth = length + 3;
                        }
                    }
                    */
                }
                sheet.SetColumnWidth(col, columnWidth * 256);//
            }
            /*
            sheet.AutoSizeColumn(0);//自适应宽度，但是其实还是比实际文本要宽
            sheet.AutoSizeColumn(3);
            sheet.AutoSizeColumn(5);
            */
        }

        //通过订单号查询供应商代码和名称
        private DataRow GetVendorIDAndName(string ponumber)
        {
            string sqlSelect = @"Select VendorNumber,VendorName From PurchaseOrderRecordByCMF Where PONumber='" + ponumber + "' And IsPurePO = 1";
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
            /*
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
                    dgvPONumberForInvoice.DataSource = SQLHelper.GetDataTable(GlobalSpace.FSDBMRConnstr, sqlSelect);
                }
                else
                {
                    sqlSelect = sqlSelect + sqlOrder;
                    dgvPONumberForInvoice.DataSource = SQLHelper.GetDataTable(GlobalSpace.FSDBMRConnstr, sqlSelect);
                }

            }
            */
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
                btnInvoiceViewByPONumber_Click(sender, e);
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
                string ponumber = dgvPONumberForInvoice.Rows[e.RowIndex].Cells["采购单号"].Value.ToString();
                dgvPOItemsForInvoiceDetail.DataSource = GetVendorInvoicePOItemDetail(ponumber);
                
            }
        }

        private void dgvPOForInvoice_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            dgvPOForInvoice_CellContentDoubleClick(sender, e);
        }

        private void btnExportInvoiceToExcel_Click(object sender, EventArgs e)
        {
            if(GlobalSpace.dtInvoice.Rows.Count  == 0)
            {
                Custom.MsgEx("当前没有可导出的发票明细！");
                return;
            }

            string sqlSelect = @"Select Name,FilePath From PurchaseDepartmentFilePathByCMF Where BuyerID='" + fsuserid + "' And Status = 0 And Name = '" + "开具发票明细导出路径" + "'";
            string poNumber = string.Empty;
            string filePath = string.Empty;

            DataTable dtTemp = SQLHelper.GetDataTable(GlobalSpace.FSDBConnstr, sqlSelect);
            if(dtTemp.Rows.Count > 0)
            {
                 filePath = dtTemp.Rows[0]["FilePath"].ToString();
            }
            else
            {
                Custom.MsgEx("当前没有导出发票后的文件路径，请先设置！");
                return;
            }
            //获取通过订单查询供应商代码和名称

            DataRow drVendor = GetVendorIDAndName(GlobalSpace.dtInvoice.Rows[0]["采购单号"].ToString());
            string vendorNumber = drVendor["VendorNumber"].ToString();
            string vendorName = drVendor["VendorName"].ToString();

            if (File.Exists(filePath +"\\"+ vendorName+DateTime.Now.ToString("yyMMdd") + ".xlsx"))
                {
                    if (MessageBoxEx.Show("导出的同名文件已经存在，是否要覆盖？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.OK)
                    {
                        CommonOperate.ExportDataTableToExcel(filePath + "\\"+vendorName + DateTime.Now.ToString("yyMMdd") + ".xlsx",vendorName,GlobalSpace.dtInvoice);
                    }
                    else
                    {
                        return;
                    }
                }
                else
                {
                    CommonOperate.ExportDataTableToExcel(filePath +"\\"+ vendorName + DateTime.Now.ToString("yyMMdd") + ".xlsx", vendorName, GlobalSpace.dtInvoice);
                }           
        }

        //查询构建发票明细DataTable
        private DataTable GetVendorInvoicePOItemDetail(string ponumber)
        {
       //     DataTable dtInvoice = new DataTable();
  //          DataTable dtTemp = new DataTable();
            string sqlSelect00 = @"SELECT
	                                        CONVERT([varchar](100),T1.TransactionDate,(23)) AS 入库日期,
                                            T2.Guid,
	                                        T1.PONumber AS 订单编号,
	                                        T1.POLineNumber AS 行号,
	                                        T1.ItemNumber AS 物料代码,
	                                        T2.ItemDescription AS 物料描述,
	                                        T1.ItemUM AS 单位,
	                                        T1.ItemReceiptQuantity AS 入库数量,
                                            T2.UnitPrice AS 不含税单价,
                                            (T1.ItemReceiptQuantity*T2.UnitPrice) AS 不含税金额
	                                        T2.PricePreTax AS 含税单价,
	                                        (T1.ItemReceiptQuantity*T2.PricePreTax) AS 含税金额
                                            T2.ForeignNumber AS 外贸单号,
                                            T2.Comment1 AS 版数
                                        FROM
	                                        PORV T1,
	                                        PurchaseOrderRecordByCMF T2
                                        WHERE
	                                        T1.TransactionFunctionCode = 'PORV'
                                        AND T1.PONumber = T2.PONumber
                                        AND T1.POLineNumber = T2.LineNumber
                                        AND T1.POReceiptActionType = 'R'
                                        ORDER BY T1.TransactionDate DESC";
            string sqlSelect = @"SELECT
	                                            ForeignNumber AS 外贸单号,
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
	                                            LotNumber AS 厂家批号,
	                                            InternalLotNumber AS 公司批号,
	                                            ExpiredDate AS 到期日期,
	                                            Operator AS 库管员,
	                                            ReceiveDate AS 入库日期
                                            FROM
	                                            dbo.PurchaseOrderRecordHistoryByCMF  Where PONumber='"+ ponumber + "'";
            //foreach遍历订单中所有的行号，来查询每一个物料的入库记录
            return SQLHelper.GetDataTable(GlobalSpace.FSDBConnstr,sqlSelect);
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
        {/*
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
                    dgvPONumberForInvoice.DataSource = SQLHelper.GetDataTable(GlobalSpace.FSDBMRConnstr, sqlSelect);
                }
                else
                {
                    sqlSelect = sqlSelect + sqlSelf;
                    dgvPONumberForInvoice.DataSource = SQLHelper.GetDataTable(GlobalSpace.FSDBMRConnstr, sqlSelect);
                }
            }*/
        }
        //生成最新的订单号 根据订单数量
        private string GeneratePONumber(string vendorNumber,string userid)
        {
            string poNumber = string.Empty;
            string sqlSelect = string.Empty;
            if(PurchaseUser.PurchaseType.Contains("P"))
            {
                sqlSelect = @"Select Count(Id) AS Count From PurchaseOrderRecordByCMF Where Buyer = '" + userid + "' And POItemPlacedDate = '" + DateTime.Now.ToString("yyyy-MM-dd") + "'";
            }
            else if (PurchaseUser.PurchaseType.Contains("M"))
            {
                string[] s = PurchaseUser.PurchaseType.Split('|');
                string sqlCriteria = string.Empty;
                for(int i = 0;i < s.Length;i++)
                {
                    sqlCriteria = " OR Left(PONumber,2) = 'P" + s[i] + "' ";
                }
                sqlSelect = @"Select Count(Id) AS Count From PurchaseOrderRecordByCMF Where Buyer = '" + userid + "' And POItemPlacedDate = '" + DateTime.Now.ToString("yyyy-MM-dd") + "'";
                sqlSelect = sqlSelect + " And " +"  (1 = 1  "+ sqlCriteria+")";
            }          
            int count = Convert.ToInt32(SQLHelper.ExecuteScalar(GlobalSpace.FSDBConnstr, sqlSelect));
            int sequenceNumber = count + 1;

            if(!string.IsNullOrEmpty(PurchaseUser.PONumberSequenceNumberRange))
            {

            }
            return poNumber;
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
                    if (IsVendorPOExist(tbVendorNumber.Text.Trim(), fsuserid))
                    {
                        if (MessageBoxEx.Show("该供应商本日订单已存在，是否直接使用已下达订单？", "提示", MessageBoxButtons.YesNo) == DialogResult.No)
                        {
                            tbVendorFuzzyName.Text = GetVendorName(tbVendorNumber.Text.Trim());
                            tbPOPostfix.Focus();
                        }
                        else
                        {
                            tbVendorNumber.Text = "";
                            tbVendorNumber.Focus();
                        }
                    }
                    else
                    {
                        tbPOPostfix.Focus();
                    }
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
            strItemNumber = dgvPOItemDetail["物料代码", dgvPOItemDetail.CurrentCell.RowIndex].Value.ToString().Trim();
            strPromisedDate = dgvPOItemDetail["需求日期", dgvPOItemDetail.CurrentCell.RowIndex].Value.ToString();
            strItemQuantity = Convert.ToDouble(dgvPOItemDetail["订购数量", dgvPOItemDetail.CurrentCell.RowIndex].Value);
            strItemUnitPrice = Convert.ToDouble(dgvPOItemDetail["单价", dgvPOItemDetail.CurrentCell.RowIndex].Value);
            strFONumber = dgvPOItemDetail["外贸单号", dgvPOItemDetail.CurrentCell.RowIndex].Value.ToString();
            double TaxRate=Convert.ToDouble(dgvPOItemDetail["税率", dgvPOItemDetail.CurrentCell.RowIndex].Value.ToString());
            List<string> itemNotComparePriceList = GetItemNotComparePriceList();

            double standardPrice = Convert.ToDouble(GetItemStandardPrice(strItemNumber));

            string rate = CommonOperate.CompareItemPriceToStandardPrice(strItemUnitPrice,standardPrice);
            string itemType = strItemNumber.Trim().Substring(0, 1);

            #region 价格比较
            if (PurchaseUser.PriceCompare == 0)
            {

                if (Convert.ToDouble(rate) >= 0.1)
                {
                    Custom.MsgEx("该物料下达价格与标准价格相差超出10%，无法下达订单！请先修改物料标准成本");
                    return;
                }
                else if (rate == "0.05")
                {
                    Custom.MsgEx("该物料下达价格与标准价格相差超出5%，请及时修改物料标准成本！");
                }


            }
            else if (PurchaseUser.PriceCompare == 1 || PurchaseUser.PriceCompare == 2)
            {
                //if(!WithoutRestrictItemList.Contains(tbItemNumber.Text))
                //{
              
                if (Convert.ToDouble(rate) >= 5)
                {
                    MessageBoxEx.Show("当前采购价格与四班标准成本相差超过5倍！无法下达订单，请先修改成本。", "提示");
                    return;
                }
                //}              
            }
            else if (PurchaseUser.PriceCompare == 3)
            {
                
                if (itemNotComparePriceList.Contains(strItemNumber))
                {
                    if (Convert.ToDouble(rate) >= 5)
                    {
                        MessageBoxEx.Show("当前采购价格与四班标准成本相差超过5倍！无法下达订单，请先修改成本。", "提示");
                        return;
                    }
                }
                else
                {
                    if (Convert.ToDouble(rate) >= 0.15)
                    {
                        Custom.MsgEx("该物料下达价格与标准价格相差超出15%，无法下达订单！请先修改物料标准成本");
                        return;
                    }
                    else if (rate == "0.1")
                    {
                        Custom.MsgEx("该物料下达价格与标准价格相差超出10%，请及时修改物料标准成本！");
                    }
                }
            }
            #endregion 价格比较
            string sqlSelect = @"Select POStatus From PurchaseOrderRecordByCMF Where Id = '" + strId + "'";

            int poStatus = Convert.ToInt32(SQLHelper.GetItemValue(GlobalSpace.FSDBConnstr, "POStatus", sqlSelect));

            if (poStatus < 3)
            {
                string sqlUpdate = @"Update PurchaseOrderRecordByCMF Set DemandDeliveryDate = '" + strPromisedDate + "',NeededDate='" + strPromisedDate + "',PromisedDate='" + strPromisedDate + "',UnitPrice=" + strItemUnitPrice + ",PricePreTax = "+ strItemUnitPrice*(1+ TaxRate) + " ,POItemQuantity=" + strItemQuantity + ", ForeignNumber = '" + strFONumber + "' Where Id = '" + strId + "'";

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

                string sqlUpdate = @"Update PurchaseOrderRecordByCMF Set DemandDeliveryDate = '" + strPromisedDate + "',NeededDate='" + strPromisedDate + "',PromisedDate='" + strPromisedDate + "',UnitPrice=" + strItemUnitPrice + ",PricePreTax = " + strItemUnitPrice * (1 + TaxRate) + " ,POItemQuantity=" + strItemQuantity + ", ForeignNumber = '" + strFONumber + "',FSupdatedt=GETDATE() Where Id = '" + strId + "'";

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
                Custom.MsgEx("订单和物料状态不允许进行修改！");
            }
        }

        private void 删除该行物料ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string strPONumber = string.Empty;
            string strLineNumber = string.Empty;
            string strItemNumber = string.Empty;
            string strPromisedDate = string.Empty;
            string strId = string.Empty;
            string guid = string.Empty;

            strPONumber = tbPONumberInDetail.Text.ToString();
            strLineNumber = dgvPOItemDetail["行号", dgvPOItemDetail.CurrentCell.RowIndex].Value.ToString();
            strId = dgvPOItemDetail["Id", dgvPOItemDetail.CurrentCell.RowIndex].Value.ToString();
            guid = dgvPOItemDetail["Guid", dgvPOItemDetail.CurrentCell.RowIndex].Value.ToString();

            string sqlSelect = @"Select POStatus From PurchaseOrderRecordByCMF Where Id = '" + strId + "'";
            int poStatus = Convert.ToInt32(SQLHelper.GetItemValue(GlobalSpace.FSDBConnstr, "POStatus", sqlSelect));

            if (poStatus < 3)
            {
                string sqlUpdate = @"Delete From  PurchaseOrderRecordByCMF  Where Guid = '" + guid + "'";
                if (SQLHelper.ExecuteNonQuery(GlobalSpace.FSDBConnstr, sqlUpdate))
                {
                    Custom.MsgEx("删除成功！");
                    ShowPOItemDetail(tbPONumberInDetail.Text.Trim());
                }
                else
                {
                    Custom.MsgEx("删除失败！");
                }
            }
            else if (poStatus == 3)
            {
                //对于大客户的订单，下单后直接写入四班并且产生到货记录，此处还需要同时删除到货记录
                if (PurchaseUser.UserStatus == 2)
                {
                    List<string> sqlList = new List<string>();
                    string sqlDelete = @"Delete From  PurchaseOrderRecordByCMF  Where Guid = '" + guid + "'";
                    string sqlDelete2 = @"Delete From  PurchaseOrderRecordHistoryByCMF  Where ParentGuid = '" + guid + "'";
                    sqlList.Add(sqlDelete);
                    sqlList.Add(sqlDelete2);

                    strItemNumber = dgvPOItemDetail["物料代码", dgvPOItemDetail.CurrentCell.RowIndex].Value.ToString();
                    strPromisedDate = dgvPOItemDetail["需求日期", dgvPOItemDetail.CurrentCell.RowIndex].Value.ToString();

                    FSFunctionLib.FSConfigFileInitialize(GlobalSpace.fsconfigfilepath, fsuserid, fspassword);

                    POMT15 myPomt = new POMT15();

                    myPomt.PONumber.Value = strPONumber;
                    myPomt.POLineNumber.Value = strLineNumber;
                    myPomt.ItemNumber.Value = strItemNumber;
                    myPomt.PromisedDateOld.Value = strPromisedDate;
                    myPomt.POLineSubType.Value = "L";


                    if (FSFunctionLib.fstiClient.ProcessId(myPomt, null))
                    {
                        if (SQLHelper.BatchExecuteNonQuery(GlobalSpace.FSDBConnstr, sqlList))
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


                    string sqlUpdate = @"Delete From  PurchaseOrderRecordByCMF  Where Guid = '" + guid + "'";

                    strItemNumber = dgvPOItemDetail["物料代码", dgvPOItemDetail.CurrentCell.RowIndex].Value.ToString();
                    strPromisedDate = dgvPOItemDetail["需求日期", dgvPOItemDetail.CurrentCell.RowIndex].Value.ToString();

                    FSFunctionLib.FSConfigFileInitialize(GlobalSpace.fsconfigfilepath, fsuserid, fspassword);

                    POMT15 myPomt = new POMT15();

                    myPomt.PONumber.Value = strPONumber;
                    myPomt.POLineNumber.Value = strLineNumber;
                    myPomt.ItemNumber.Value = strItemNumber;
                    myPomt.PromisedDateOld.Value = strPromisedDate;
                    myPomt.POLineSubType.Value = "L";


                    if (FSFunctionLib.fstiClient.ProcessId(myPomt, null))
                    {
                        if (SQLHelper.ExecuteNonQuery(GlobalSpace.FSDBConnstr, sqlUpdate))
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
            }
            else
            {

                Custom.MsgEx("物料状态不允许进行修改！");
            }

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
                        strPromisedDateOld = dgvPOItemDetail.Rows[e.RowIndex].Cells["需求日期旧"].Value.ToString();
                        this.contextMenuStrip1.Show(MousePosition.X, MousePosition.Y);
                    }
                }
            }
        }

        private void dgvPOItemDetail_CellClick(object sender, DataGridViewCellEventArgs e)
        {

        }



        private void tbForeignNumber_Enter(object sender, EventArgs e)
        {
   
        }

        private void tbForeignNumber_Leave(object sender, EventArgs e)
        {
         
        }

        private void 删除该订单ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (DialogResult.OK == MessageBoxEx.Show("确定删除该订单么？删除后，该订单所有信息将被清除", "提示", MessageBoxButtons.OKCancel))
            {
                string ponumber = string.Empty;
                bool  status = false;
                bool fsStatus = false;
                string sqlDelete = string.Empty;

                ponumber = dgvPO.Rows[dgvPO.CurrentCell.RowIndex].Cells["采购单号"].Value.ToString();

                string sqlSelectPOFS = @"SELECT COUNT(Buyer) FROM _NoLock_FS_POHeader WHERE PONumber = '"+ponumber+"'";
                string sqlSelectPO = @"SELECT COUNT(Id) FROM PurchaseOrderRecordByCMF WHERE PONumber = '" + ponumber+"' And IsPurePO = 0";
               
               if(SQLHelper.Exist(GlobalSpace.FSDBMRConnstr,sqlSelectPOFS))
                {
                    fsStatus = true;
                }

                if (SQLHelper.Exist(GlobalSpace.FSDBConnstr, sqlSelectPO))
                {
                    status = true;
                }

                if (status)
                {
                    Custom.MsgEx("当前订单中有子项，不允许直接删除订单！");
                    return;
                }
                else
                {
                    if(fsStatus)
                    {
                        sqlDelete = @"Delete From PurchaseOrderRecordByCMF Where PONumber='" + ponumber + "'";
                        POMT04 pomt04 = new POMT04();
                        pomt04.PONumber.Value = ponumber;
                        FSFunctionLib.FSConfigFileInitialize(GlobalSpace.fsconfigfilepath,fsuserid, fspassword);
                        if (FSFunctionLib.fstiClient.ProcessId(pomt04, null))
                        {
                            if (SQLHelper.ExecuteNonQuery(GlobalSpace.FSDBConnstr, sqlDelete))
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
                        FSFunctionLib.FSExit();
                    }
                    else
                    {
                        sqlDelete = @"Delete From PurchaseOrderRecordByCMF Where PONumber='" + ponumber + "'";
                        if (SQLHelper.ExecuteNonQuery(GlobalSpace.FSDBConnstr, sqlDelete))
                        {
                            MessageBox.Show("订单删除成功！");
                        }
                        else
                        {
                            MessageBox.Show("订单删除失败！");
                        }
                    }

                }
                ShowOrder("0");
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

        private void btnInvoiceViewByVendorNumber_Click(object sender, EventArgs e)
        {
            string sqlSelect = @"Select Distinct Guid, PONumber AS 采购单号 From PurchaseOrderRecordByCMF Where IsGetInvoice = 0 And VendorNumber = '"+tbVendorNumberForInvoice.Text+"' And IsPurePO = 1 And Buyer = '"+fsuserid+"'";
            dgvPONumberForInvoice.DataSource = SQLHelper.GetDataTable(GlobalSpace.FSDBConnstr, sqlSelect);
            dgvPONumberForInvoice.Columns["Guid"].Visible = false;
        }


        private void ExcelToPDF(string fileFromPath,string excelFileName,string fileToPath,string pdfFileName)
        {
            Workbook workbook = new Workbook();
            workbook.LoadFromFile(fileFromPath +"\\"+ excelFileName);
            workbook.SaveToFile(fileToPath + "\\"+pdfFileName+".pdf", FileFormat.PDF);
        }

        private void dgvPO2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnInvoiceViewByPONumber_Click(object sender, EventArgs e)
        {
            if(tbPONumberForInvoice.Text != "")
            {
                dgvPOItemsForInvoiceDetail.DataSource = GetVendorInvoicePOItemDetail(tbPONumberForInvoice.Text);
            }
            else
            {
                Custom.MsgEx("订单号不能为空！");
            }
            
        }

        private void btnInvoiceCheckedPOGetSum_Click_1(object sender, EventArgs e)
        {
            if(dgvPONumberForInvoice.SelectedRows.Count > 0)
            {

            }
            else
            {
                Custom.MsgEx("没有选中的行！");
            }
        }

        private void tbPOForSearch_TextChanged(object sender, EventArgs e)
        {
            tbPOForSearch.Text = tbPOForSearch.Text.ToUpper();
            tbPOForSearch.SelectionStart = tbPOForSearch.Text.Length;
        }

        private void dgvPO3_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            string ponumber = dgvPO3.Rows[e.RowIndex].Cells["采购单号"].Value.ToString();
            if (GetVendorPOItemsDetail(ponumber) != null)
            {
                tbPOForSearch.Text = ponumber;
                dgvPO2.DataSource = GetVendorPOItemsDetail(ponumber);
                dgvPO2.Columns["GUID"].Visible = false;
            }
            else
            {
                DataTable dtTemp2 = (DataTable)dgvPO2.DataSource;
                dtTemp2.Rows.Clear();
                dgvPO2.DataSource = dtTemp2;
            }
        }

        private void dgvPO3_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            dgvPO3_CellContentDoubleClick(sender, e);
        }

        private void btnSendVendorInvoiceEmail_Click(object sender, EventArgs e)
        {

        }

        private void btnInvoiceAllChecked_Click(object sender, EventArgs e)
        {
            if(dgvPOItemsForInvoiceDetail.Rows.Count > 0)
            {
                foreach (DataGridViewRow dgvr in dgvPOItemsForInvoiceDetail.Rows)
                {
                    dgvr.Cells["InvoiceCheck"].Value = true;
                }
            }
        }

        private void btnInvoiceAllUnchecked_Click(object sender, EventArgs e)
        {
            if (dgvPOItemsForInvoiceDetail.Rows.Count > 0)
            {
                foreach (DataGridViewRow dgvr in dgvPOItemsForInvoiceDetail.Rows)
                {
                    dgvr.Cells["InvoiceCheck"].Value = false;
                }
            }
        }

        private void btnEmptyInvoiceList_Click(object sender, EventArgs e)
        {
            if(GlobalSpace.dtInvoice.Rows.Count > 0)
            {
                GlobalSpace.dtInvoice.Rows.Clear();
            }
        }

        private void btnAddToInvoiceList_Click(object sender, EventArgs e)
        {
            int i = 0;
            DataTable dtNew = new DataTable();
            foreach(DataGridViewRow dgvr in dgvPOItemsForInvoiceDetail.Rows)
            {
                if(Convert.ToBoolean(dgvr.Cells["InvoicePOItemCheck"].Value) == true)
                {
                    i++;
                    DataRow dr = dtNew.NewRow();
                    dr = (dgvr.DataBoundItem as DataRowView).Row;
                    dtNew.Rows.Add(dr.ItemArray);
                }
            }

            if(i == 0 )
            {
                Custom.MsgEx("当前没有可用于增加的采购记录！");
            }
            else
            {
                dtNew.Columns.Remove("GUID");
                GlobalSpace.dtInvoice = new DataTable();
                if(GlobalSpace.dtInvoice.Rows.Count > 0)
                {
                    foreach(DataRow dr in dtNew.Rows)
                    {
                        GlobalSpace.dtInvoice.Rows.Add(dr.ItemArray);
                    }
                }
                else
                {
                    GlobalSpace.dtInvoice = dtNew.Copy();
                }

            }


        }

        private void superTabControlPanel3_Click(object sender, EventArgs e)
        {

        }

        private void btnConfirmPOItemGetInvoice_Click(object sender, EventArgs e)
        {
            int i = 0;
            DataTable dtNew = new DataTable();
            foreach (DataGridViewRow dgvr in dgvPOItemsForInvoiceDetail.Rows)
            {
                if (Convert.ToBoolean(dgvr.Cells["InvoicePOItemCheck"].Value) == true)
                {
                    i++;
                    DataRow dr = dtNew.NewRow();
                    dr = (dgvr.DataBoundItem as DataRowView).Row;
                    dtNew.Rows.Add(dr.ItemArray);
                }
            }
            List<string> listUpdate = new List<string>();
            if (i == 0)
            {
                Custom.MsgEx("当前没有可以进行确认的采购记录！");
            }
            else
            {
                foreach(DataRow dr in dtNew.Rows)
                {
                    string sqlUpdate = @"Update PurchaseOrderRecordByCMF Set IsGetInvoice = 1,InvoiceStatusUpdateDateTime = '"+DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss") +"' Where Guid = '" + dr["Guid"].ToString()+"'";
                    listUpdate.Add(sqlUpdate);
                }
                if(SQLHelper.BatchExecuteNonQuery(GlobalSpace.FSDBConnstr,listUpdate))
                {
                    Custom.MsgEx("更新成功！");
                }
                else
                {
                    Custom.MsgEx("更新失败！");
                }
            }

        }

        private void btnConfirmedForeignOrderItem_Click(object sender, EventArgs e)
        {
            ForeignOrderItemAutomaticPlaceOrder foapo = new ForeignOrderItemAutomaticPlaceOrder(fsuserid,fsusername);
            foapo.ShowDialog();
        }

        private void btnMakeProgress_Click(object sender, EventArgs e)
        {
            Purchase.POProgress pop = new Purchase.POProgress(fsuserid);
            pop.Show();
        }

        private void tbFONumber_TextChanged(object sender, EventArgs e)
        {
            tbFONumber.Text = tbFONumber.Text.ToUpper();
            tbFONumber.SelectionStart = tbFONumber.Text.Length;
        }

        private void tbPOForSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(tbPOForSearch.Text !="")
            {
                if(e.KeyChar == (char)13)
                {
                    btnSearchPO2_Click(sender, e);
                }
            }
        }

        private void tbFONumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (tbFONumber.Text != "")
            {
                if (e.KeyChar == (char)13)
                {
                    btnSearchPO2_Click(sender, e);
                }
            }
        }

        private void PrintPODetailForInvoice(object sender, EventArgs e)
        {

        }

        private void btnBatchImportInternal_Click(object sender, EventArgs e)
        {
            ImportDemesticProduct idp = new ImportDemesticProduct(fsuserid,fspassword,fsusername, PurchaseUser.SupervisorID);
            idp.ShowDialog();
        }

        private void dgvPO_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            dgvPO_CellContentClick(sender, e);
        }

        private void btnFO_Click(object sender, EventArgs e)
        {
            ForeignOrderItemAutomaticPlaceOrder foapo = new ForeignOrderItemAutomaticPlaceOrder(fsuserid, fsusername);
            foapo.ShowDialog();
        }

        private void tbVendorNumberForInvoice_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar == (char)13)
            {
                if(!string.IsNullOrEmpty(tbVendorNumberForInvoice.Text))
                {
                    btnInvoiceViewByVendorNumber_Click(sender, e);
                }
            }
        }

        private void cbbVendor_SelectionChangeCommitted(object sender, EventArgs e)
        {
            /*
            string[] vendorinfo = cbbVendor.Text.Split(' ');
            tbVendorNumber.Text = vendorinfo[0];
            tbPONumber.Focus();*/
        }

        private void btnOldRecord_Click(object sender, EventArgs e)
        {
            OldRecord or = new OldRecord(fsuserid);
            or.ShowDialog();
        }

        private void cbbVendor_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            string[] vendorinfo = cbbVendor.Text.Split(' ');
            tbVendorNumber.Text = vendorinfo[0];
            tbPONumber.Focus();
        }

 

        //获取最新订单编号
        private string GeneratePONumberSequenceNumber(string poType,string userID)
        {
            string strReturn = string.Empty;
            string sqlSelect = string.Empty;
            int count = 0;
            int sequenceNumber = 0;

            //检查该供应商订单是否存在，存在则提示
            if (poType == "PP")
            {

                sqlSelect = @"Select Count(Id) From PurchaseOrderRecordByCMF Where Buyer =  '" + fsuserid + "' And POItemPlacedDate = '" + DateTime.Now.ToString("yyyy-MM-dd") + "' And IsPurePO = 1";
                count = Convert.ToInt32(SQLHelper.ExecuteScalar(GlobalSpace.FSDBConnstr, sqlSelect));
                if (PONumberStartNumber + count > PONumberEndNumber)
                {
                    strReturn = "BeyondRange";
                }
                else
                {
                    if((PONumberStartNumber + count).ToString().Length == 1)
                    {
                        strReturn = "00"+ (PONumberStartNumber + count).ToString();
                    }
                    else if((PONumberStartNumber + count).ToString().Length == 2)
                    {
                        strReturn ="0"+ (PONumberStartNumber + count).ToString();
                    }
                    else
                    {
                        strReturn = (PONumberStartNumber + count).ToString();
                    }

                }

            }
            else
            {
                sqlSelect = @"Select Count(Id) From PurchaseOrderRecordByCMF Where Left(PONumber,2) = '" + poType + "' And POItemPlacedDate = '" + DateTime.Now.ToString("yyyy-MM-dd") + "'  And IsPurePO = 1";
                count = Convert.ToInt32(SQLHelper.ExecuteScalar(GlobalSpace.FSDBConnstr, sqlSelect));
                sequenceNumber = count + 1;
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
                sqlSelect = @"Select TOP 1 PONumber From PurchaseOrderRecordByCMF Where Buyer = '" + userID + "' And Left(PONumber,2) = '"+ poType + "' And POItemPlacedDate = '" + DateTime.Now.ToString("yyyy-MM-dd") + "'  AND IsPurePO = 1 ORDER BY Id DESC";
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
                    else if(PONumberStartNumber == 51)
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
                    if(sequenceNumber > PONumberEndNumber)
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
                if(PurchaseUser.PONumberSequenceNumberRange == "1-50")
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
                        strReturn = PONumberStartNumber.ToString().PadLeft(3,'0');
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

        private void POPostFixGotFocus(object sender, EventArgs e)
        {
             if(rbtnAutomatic.Checked)
            {
                if ("P" + cbbPOPrefix.Text == "PJ" || "P" + cbbPOPrefix.Text == "PF")
                {
                    //      Custom.MsgEx("进料加工物料订单序号无法自动生成，请手工填写！");
                    //       btnPlaceOrder.Focus();
                    //         return;
                    tbPOPostfix.Text = "";
                    return;
                }
                string seqNumber = GeneratePONumberSequenceNumber2("P" + cbbPOPrefix.Text, fsuserid);

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

        private void tbPOPostfix_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(!string.IsNullOrEmpty(tbPOPostfix.Text))
            {
                if(e.KeyChar == (char)13)
                {
                    //     btnPlaceOrder_Click(sender, e);
                    MessageBox.Show("111");
                }
            }
        }

        private void btnGetItemInfo_Click(object sender, EventArgs e)
        {
            ItemInfo ii = new ItemInfo();
            ii.ShowDialog();
            tbItemNumber.Text = ItemInfoForSearch.ItemNumber;
            tbItemDescription.Text = ItemInfoForSearch.ItemDescription;
            tbUM.Text = ItemInfoForSearch.ItemUM;
            tbOrderQuantity.Focus();
        }

        private void tbPOPostfix_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            if(!string.IsNullOrEmpty(tbPOPostfix.Text))
            {
                if(e.KeyChar == (char)13)
                {
                    btnPlaceOrder_Click(sender, e);
                }
            }
        }

        private void cbbConfirmPerson_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(!string.IsNullOrEmpty( cbbConfirmPerson.Text))
            {
                if(e.KeyChar ==(char)13)
                {
                    btnAddItemToPO.Focus();
                }
            }
        }

        private void superTabControlPanel4_Click(object sender, EventArgs e)
        {

        }

        private void 关闭改行物料ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string poNumber = tbPONumberInDetail.Text.Trim();        
            string lineNumber = dgvPOItemDetail["行号", dgvPOItemDetail.CurrentCell.RowIndex].Value.ToString();
            string itemNumber = dgvPOItemDetail["物料代码", dgvPOItemDetail.CurrentCell.RowIndex].Value.ToString();
            string promisedDate = dgvPOItemDetail["需求日期", dgvPOItemDetail.CurrentCell.RowIndex].Value.ToString();
            string Id = dgvPOItemDetail["Id", dgvPOItemDetail.CurrentCell.RowIndex].Value.ToString();
            string sqlUpdate = @"Update PurchaseOrderRecordByCMF Set LineStatus = 5  Where Id = '" + Id + "'";
            FSFunctionLib.FSConfigFileInitialize(GlobalSpace.fsconfigfilepath, fsuserid, fspassword);

            POMT12 myPomt12 = new POMT12();
            myPomt12.PONumber.Value = poNumber;
            myPomt12.POLineNumber.Value = lineNumber;
            myPomt12.ItemNumber.Value = itemNumber;
            //myPomt12.PromisedDate.Value = promisedDate;
            myPomt12.PromisedDateOld.Value = strPromisedDateOld;
            myPomt12.POLineSubType.Value = "L";
            myPomt12.POLineStatus.Value = "5";

            if (FSFunctionLib.fstiClient.ProcessId(myPomt12, null))
            { 
                if(SQLHelper.ExecuteNonQuery(GlobalSpace.FSDBConnstr,sqlUpdate))
                {
                    Custom.MsgEx("关闭成功！");
                    ShowPOItemDetail(tbPONumberInDetail.Text.Trim());
                    FSFunctionLib.FSExit();
                }
                else
                {
                    Custom.MsgEx("四班关闭成功，记录修改失败！");
                    FSFunctionLib.FSExit();
                }

            }
            else
            {
                FSFunctionLib.FSErrorMsg("关闭失败");
            }


        }

        private void tbPONumberForInvoice_TextChanged(object sender, EventArgs e)
        {
            tbPONumberForInvoice.Text = tbPONumberForInvoice.Text.ToUpper();
            tbPONumberForInvoice.SelectionStart = tbPONumberForInvoice.Text.Length;
        }

        private void btnPOInvoice_Click(object sender, EventArgs e)
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
            dt.Columns.Add("单价");
            bool isChecked = false;

            foreach (DataGridViewRow dgvr in dgvPOItemsForInvoiceDetail.Rows)
            {
                if (Convert.ToBoolean(dgvr.Cells["InvoiceCheck"].Value))
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
                    dr["到期日期"] = dgvr.Cells["到期日期"].Value.ToString();
                    dr["单价"] = dgvr.Cells["单价"].Value.ToString();
                    dt.Rows.Add(dr.ItemArray);
                }
            }
            if (!isChecked)
            {
                MessageBoxEx.Show("没有选中的行！", "提示");
                return;
            }
            MessageBox.Show(dt.Rows.Count.ToString() + "行记录！");
           Warehouse.PrintPO  pp = new Warehouse.PrintPO(dt, "\\PrintPOInvoice.grf");
            pp.Show();
        }

        private void btnInvoice_Click(object sender, EventArgs e)
        {
            POInvoice po = new POInvoice();
            po.ShowDialog();
        }

        private void 打开改行物料ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string poNumber = tbPONumberInDetail.Text.Trim();
            string lineNumber = dgvPOItemDetail["行号", dgvPOItemDetail.CurrentCell.RowIndex].Value.ToString();
            string itemNumber = dgvPOItemDetail["物料代码", dgvPOItemDetail.CurrentCell.RowIndex].Value.ToString();
            string promisedDate = dgvPOItemDetail["需求日期", dgvPOItemDetail.CurrentCell.RowIndex].Value.ToString();
            string Id = dgvPOItemDetail["Id", dgvPOItemDetail.CurrentCell.RowIndex].Value.ToString();
            string sqlUpdate = @"Update PurchaseOrderRecordByCMF Set LineStatus = 4  Where Id = '" + Id + "'";
            FSFunctionLib.FSConfigFileInitialize(GlobalSpace.fsconfigfilepath, fsuserid, fspassword);

            POMT12 myPomt12 = new POMT12();
            myPomt12.PONumber.Value = poNumber;
            myPomt12.POLineNumber.Value = lineNumber;
            myPomt12.ItemNumber.Value = itemNumber;
            //myPomt12.PromisedDate.Value = promisedDate; //5状态改成4状态 承诺交货日 ？能不能改
            myPomt12.PromisedDateOld.Value = strPromisedDateOld;
            myPomt12.POLineSubType.Value = "L";
            myPomt12.POLineStatus.Value = "4";

            if (FSFunctionLib.fstiClient.ProcessId(myPomt12, null))
            {
                if (SQLHelper.ExecuteNonQuery(GlobalSpace.FSDBConnstr, sqlUpdate))
                {
                    Custom.MsgEx("修改成功！");
                    ShowPOItemDetail(tbPONumberInDetail.Text.Trim());
                    FSFunctionLib.FSExit();
                }
                else
                {
                    Custom.MsgEx("四班修改成功，记录修改失败！");
                    FSFunctionLib.FSExit();
                }

            }
            else
            {
                FSFunctionLib.FSErrorMsg("物料修改失败");
            }
        }

        private void BtnOrderClosed_Click(object sender, EventArgs e)
        {
            //fsuserid, fspassword
            PurchaseOrderClose Poc = new PurchaseOrderClose(fsuserid, fspassword);
            Poc.ShowDialog();
        }

        private void 该订单行作废ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string poNumber = tbPONumberInDetail.Text.Trim();
            string lineNumber = dgvPOItemDetail["行号", dgvPOItemDetail.CurrentCell.RowIndex].Value.ToString();
            string itemNumber = dgvPOItemDetail["物料代码", dgvPOItemDetail.CurrentCell.RowIndex].Value.ToString();
            string promisedDate = dgvPOItemDetail["需求日期", dgvPOItemDetail.CurrentCell.RowIndex].Value.ToString();
            string Id = dgvPOItemDetail["Id", dgvPOItemDetail.CurrentCell.RowIndex].Value.ToString();
            string sqlUpdate = @"Update PurchaseOrderRecordByCMF Set LineStatus = 5, POStatus = -1  Where Id = '" + Id + "'";
            FSFunctionLib.FSConfigFileInitialize(GlobalSpace.fsconfigfilepath, fsuserid, fspassword);

            POMT12 myPomt12 = new POMT12();
            myPomt12.PONumber.Value = poNumber;
            myPomt12.POLineNumber.Value = lineNumber;
            myPomt12.ItemNumber.Value = itemNumber;
            //myPomt12.PromisedDate.Value = promisedDate;
            myPomt12.PromisedDateOld.Value = strPromisedDateOld;
            myPomt12.POLineSubType.Value = "L";
            myPomt12.POLineStatus.Value = "5";

            if (FSFunctionLib.fstiClient.ProcessId(myPomt12, null))
            {
                if (SQLHelper.ExecuteNonQuery(GlobalSpace.FSDBConnstr, sqlUpdate))
                {
                    Custom.MsgEx("取消订单行明细成功！");
                    ShowPOItemDetail(tbPONumberInDetail.Text.Trim());
                    FSFunctionLib.FSExit();
                }
                else
                {
                    Custom.MsgEx("四班关闭成功，记录修改失败！");
                    FSFunctionLib.FSExit();
                }

            }
            else
            {
                FSFunctionLib.FSErrorMsg("操作失败");
            }

        }

        private void 打开该订单ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string poNumber = tbPONumberInDetail.Text.Trim();
            string lineNumber = dgvPOItemDetail["行号", dgvPOItemDetail.CurrentCell.RowIndex].Value.ToString();
            string itemNumber = dgvPOItemDetail["物料代码", dgvPOItemDetail.CurrentCell.RowIndex].Value.ToString();
            string promisedDate = dgvPOItemDetail["需求日期", dgvPOItemDetail.CurrentCell.RowIndex].Value.ToString();
            string Id = dgvPOItemDetail["Id", dgvPOItemDetail.CurrentCell.RowIndex].Value.ToString();
            string sqlUpdate = @"Update PurchaseOrderRecordByCMF Set LineStatus = 4, POStatus = 3  Where Id = '" + Id + "'";
            FSFunctionLib.FSConfigFileInitialize(GlobalSpace.fsconfigfilepath, fsuserid, fspassword);

            POMT12 myPomt12 = new POMT12();
            myPomt12.PONumber.Value = poNumber;
            myPomt12.POLineNumber.Value = lineNumber;
            myPomt12.ItemNumber.Value = itemNumber;
            //myPomt12.PromisedDate.Value = promisedDate;
            myPomt12.PromisedDateOld.Value = strPromisedDateOld;
            myPomt12.POLineSubType.Value = "L";
            myPomt12.POLineStatus.Value = "4";

            if (FSFunctionLib.fstiClient.ProcessId(myPomt12, null))
            {
                if (SQLHelper.ExecuteNonQuery(GlobalSpace.FSDBConnstr, sqlUpdate))
                {
                    Custom.MsgEx("打开订单行明细成功！");
                    ShowPOItemDetail(tbPONumberInDetail.Text.Trim());
                    FSFunctionLib.FSExit();
                }
                else
                {
                    Custom.MsgEx("四班打开成功，记录修改失败！");
                    FSFunctionLib.FSExit();
                }

            }
            else
            {
                FSFunctionLib.FSErrorMsg("操作失败");
            }
        }

        private void tbRequireDept_TextUpdate(object sender, EventArgs e)
        {
            tbRequireDept.Items.Clear();
            string StrRequireDept = tbRequireDept.Text.Trim();
            DataTable dt = SQLHelper.GetDataTable(GlobalSpace.FSDBConnstr, $@"select RequireDept from [dbo].[PurchaseOrderRecordRequireDepts] where Status=1 and RequireDept like '%{StrRequireDept}%'");
            foreach (DataRow dr in dt.Rows)
            {
                tbRequireDept.Items.Add(dr["RequireDept"].ToString().Trim());
            }
            this.tbRequireDept.SelectionStart = this.tbRequireDept.Text.Length;
        }

        private void tbRequireDept_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (tbRequireDept.Items.Count > 0)
                    tbRequireDept.SelectedIndex = 0;

                this.tbRequireDept.SelectionStart = this.tbRequireDept.Text.Length;
            }
        }
    }

}
