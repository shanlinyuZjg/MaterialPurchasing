using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using System.Data.SqlClient;
using Global.Helper;
using DevComponents.DotNetBar.SuperGrid;
using DevComponents.DotNetBar.SuperGrid.Style;
using System.Data.OleDb;
using NPOI.XSSF.UserModel;
using NPOI.SS.UserModel;
using System.IO;

namespace Global.Purchase
{
    public partial class Supervisor : Office2007Form
    {
        string userID = string.Empty;
        private Background _Background1 =
           new Background(Color.White, Color.FromArgb(238, 244, 251), 45);

        private Background _Background2 = new Background(Color.FromArgb(249, 249, 234));
        private Background _Background3 = new Background(Color.FromArgb(255, 247, 250));
        public Supervisor()
        {
            InitializeComponent();
        }

        public Supervisor(string fsUserID)
        {
            userID = fsUserID;           
            InitializeComponent();
        }
        /// <summary>
        /// 显示最近50个订单
        /// </summary>
        /// 
        /*
        private void BindCustomerData()
        {
            DataSet _DataSet = new DataSet();

            using (OleDbConnection cn =
                new OleDbConnection(GlobalSpace.oledbconnstrFSDB))
            {
                string sqlSelectPO = @"Select Distinct TOP 20 PONumber AS 采购单号,VendorNumber AS 供应商代码,VendorName AS 供应商名称,POItemPlacedDate AS 订单创建日期,Buyer AS 采购员  from PurchaseOrderRecordByCMF Where Superior = '" + userID+ "' Order By POItemPlacedDate Desc";
                string sqlSelectPODetail = @"SELECT TOP 1000 
                                                      (case POStatus when  '0' then '已准备'
                                                                 when  '1' then '已提交'
                                                                 when  '2' then '已审核'
                                                                 when  '3' then '已下达' 
                                                                when  '4' then '已到货' 
                                                                when  '5' then '已收货' 
                                                                when  '6' then '已入库' 
                                                                when  '7' then '已开票' 
                                                        end     
                                                        ) as 物料状态,  
                                                    POItemPlacedDate	AS 下单日期,
                                                    ForeignNumber	AS 外贸单号,
                                                    PONumber	AS 采购单号,
                                                    ItemNumber	AS 物料代码,
                                                    ItemDescription	AS 物料名称,
                                                    Specification	AS 规格,
                                                    LineUM	AS 单位,
                                                    POItemQuantity	AS 数量,
                                                    QualityCheckStandard	AS 质量标准,
                                                    DemandDeliveryDate	AS 要求到货时间,
                                                    ItemUsedPoint	AS 提报单位,
                                                    Comment1	AS 备注,
                                                    VendorNumber	AS 供应商代码,
                                                    VendorName AS 供应商,
                                                    UnitPrice	AS 单价,
                                                    Comment2	AS 备注,
                                                    ActualDeliveryDate	AS 实际到货日期,
                                                    ActualDeliveryQuantity	AS 实际到货数量,
                                                    LotNumber	AS 供应商批号,
                                                    InvoiceNumber	AS 发票号,
                                                    InvoiceIssuedDateTime	AS 开票时间,
                                                    InstanceID	AS 实例号,
                                                    ContractID	AS 合同号,
                                                    Comment3	AS 备注
                                                    FROM
	                                                    PurchaseOrderRecordByCMF
                                                    WHERE
	                                                    Superior = '" + userID+ "' And IsPurePO = 0 Order By POItemPlacedDate Desc";

                new OleDbDataAdapter(sqlSelectPO, cn).Fill(_DataSet, "Orders");
                new OleDbDataAdapter(sqlSelectPODetail, cn).Fill(_DataSet, "Order Details");

                _DataSet.Relations.Add("2", _DataSet.Tables["Orders"].Columns["采购单号"],
                                       _DataSet.Tables["Order Details"].Columns["采购单号"], false);
            }

            superGridControl1.PrimaryGrid.DataSource = _DataSet;
            superGridControl1.PrimaryGrid.DataMember = "Orders";
        }

        private void BindCustomerData(string sqlSelectPO,string sqlSelectPODetail)
        {
            DataSet _DataSet = new DataSet();

            using (OleDbConnection cn =
                new OleDbConnection(GlobalSpace.oledbconnstrFSDB))
            {               
                new OleDbDataAdapter(sqlSelectPO, cn).Fill(_DataSet, "Orders");
                new OleDbDataAdapter(sqlSelectPODetail, cn).Fill(_DataSet, "Order Details");

                _DataSet.Relations.Add("2", _DataSet.Tables["Orders"].Columns["采购单号"],
                                       _DataSet.Tables["Order Details"].Columns["采购单号"], false);
            }

            superGridControl1.PrimaryGrid.DataSource = _DataSet;
            superGridControl1.PrimaryGrid.DataMember = "Orders";
        }

        private void btnViewPwd_MouseDown(object sender, MouseEventArgs e)
        {
            tbPassword.PasswordChar = new char();
        }

        private void btnViewPwd_MouseUp(object sender, MouseEventArgs e)
        {
            tbPassword.PasswordChar = '*';
        }

        private void btnSetPassword_Click(object sender, EventArgs e)
        {
            if(String.IsNullOrEmpty(tbPassword.Text.Trim()))
            {
                MessageBoxEx.Show("密码不能为空！", "提示");
            }
            else
            {
                string sqlCheck = @"Select Id from PurchaseDepartmentCheckPasswordByCMF Where Supervisor ='"+userID+"'  And IsValid = 0";

                if(SQLHelper.Exist(GlobalSpace.FSDBConnstr, sqlCheck))
                {
                    MessageBoxEx.Show("当前存在未失效密码，请先取消！", "提示");
                }
                else
                {
                    if(dtpFinish.Value <= dtpStart.Value)
                    {
                        MessageBoxEx.Show("结束日期必须晚于开始日期！", "提示");
                    }
                    else
                    {

                        string sqlInsert = @"Insert Into PurchaseDepartmentCheckPasswordByCMF
                        (Password,StartDate,FinishDate,Supervisor) 
                        Values
                        (@Password,@StartDate,@FinishDate,'" + userID + "')";
                        SqlParameter[] sqlparams =
                        {
                    new SqlParameter("@Password",tbPassword.Text.Trim()),
                    new SqlParameter("@StartDate",dtpStart.Value.ToString("yyyy-MM-dd")),
                    new SqlParameter("@FinishDate",dtpFinish.Value.ToString("yyyy-MM-dd"))
                    };


                        if (SQLHelper.ExecuteNonQuery(GlobalSpace.FSDBConnstr, sqlInsert, sqlparams))
                        {
                            MessageBoxEx.Show("设置成功！请点击”发送邮件“按钮给员工发送提醒邮件！", "提示");
                        }
                        else
                        {
                            MessageBoxEx.Show("设置失败！", "提示");
                        }
                    }

                }
                
            }

        }
        */
        private void btnInvalidateCurrentPassword_Click(object sender, EventArgs e)
        {
            string sqlCheck = @"Select Id from PurchaseDepartmentCheckPasswordByCMF Where Supervisor ='" + userID + "' And IsValid = 0";
            
            if(SQLHelper.Exist(GlobalSpace.FSDBConnstr, sqlCheck))
            {
                string id = SQLHelper.GetItemValue(GlobalSpace.FSDBConnstr, "Id", sqlCheck);
                string sqlUpdate = @"Update PurchaseDepartmentCheckPasswordByCMF Set IsValid = 1 Where Id='"+id+"'";

                if(SQLHelper.ExecuteNonQuery(GlobalSpace.FSDBConnstr, sqlUpdate) )
                {
                    MessageBoxEx.Show("取消成功！", "提示");
                }
                else
                {
                    MessageBoxEx.Show("取消失败！", "提示");
                }
            }
            else
            {
                MessageBoxEx.Show("当前不存在有效密码！", "提示");
            }
        }

        private void Supervisor_Load(object sender, EventArgs e)
        {
            MessageBoxEx.EnableGlass = false;
            GetUncheckedPO();
            //dgvAllPO.DataSource = GetAllPO();
    //        BindCustomerData();
        }
        //查询所有采购订单信息
        private DataTable GetAllPO()
        {
            string sqlSelect = @"SELECT
                                                    POItemFSPlacedDateTime	AS 下单日期,
                                                    ForeignNumber	AS 外贸联系单号,
                                                    PONumber	AS 订单号,
                                                    ItemNumber	AS 物料代码,
                                                    ItemDescription	AS 物料名称,
                                                    Specification	AS 规格,
                                                    LineUM	AS 单位,
                                                    POItemQuantity	AS 数量,
                                                    QualityCheckStandard	AS 质量标准,
                                                    DemandDeliveryDate	AS 要求到货时间,
                                                    ItemUsedPoint	AS 提报单位,
                                                    Comment1	AS 备注,
                                                    VendorNumber	AS 供应商代码,
                                                    VendorName AS 供应商,
                                                    UnitPrice	AS 单价,
                                                    Comment2	AS 备注,
                                                    ActualDeliveryDate	AS 实际到货日期,
                                                    ActualDeliveryQuantity	AS 实际到货数量,
                                                    VendorBatchNumber	AS 供应商批号,
                                                    InvoiceNumber	AS 发票号,
                                                    InvoiceIssuedDateTime	AS 开票时间,
                                                    InstanceID	AS 实例号,
                                                    ContractID	AS 合同号,
                                                    Comment3	AS 备注
                                                    FROM
	                                                    PurchaseOrderRecordByCMF
                                                    WHERE
	                                                    Superior = 'MBJ'";
            return SQLHelper.GetDataTable(GlobalSpace.FSDBConnstr, sqlSelect);
        }
        //查询未审核订单
        private void GetUncheckedPO()
        {
            string sqlSelect = @"Select Distinct PONumber as 采购单号,Buyer,VendorNumber as 供应商码 ,VendorName as 供应商名称  From PurchaseOrderRecordByCMF Where Superior='"+userID+ "' And POStatus=1  ORDER BY PONumber ASC";
            //CommonOperate.DataGridViewShow(sqlSelect, GlobalSpace.FSDBConnstr, dgvPO);
            dgvPO.DataSource = SQLHelper.GetDataTable(GlobalSpace.FSDBConnstr, sqlSelect);
            dgvPO.Columns["Buyer"].Visible = false;
        }

        private void ShowOrderByFONumber(string foNumber,string uid, DataGridView dgv)
        {
            string sqlSelect = @"SELECT
                                                T1.Guid,
                                                T1.ForeignNumber AS 外贸单号,
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
	                                        T1.ForeignNumber Like '%" + foNumber + "%' And T1.POStatus = 1 And T1.Superior='" + uid + "'";
            DataTable dtTemp = null;
            try
            {
                dtTemp = SQLHelper.GetDataTable(GlobalSpace.FSDBConnstr, sqlSelect);
            }
            catch (Exception ex)
            {
                MessageBoxEx.Show("发生异常：" + ex.Message);
            }
            dgv.DataSource = dtTemp;
            dgv.Columns["Guid"].Visible = false;
        }
        private void ShowOrder(string uid,string ponumber,DataGridView dgv)
        {
            string sqlSelect = @"SELECT
                                                T1.Guid,
	                                        	T1.ItemNumber AS 物料代码,
	                                            T1.ItemDescription AS 物料描述,
	                                            T1.LineUM AS 单位,
	                                            T1.LineType AS 类型,
	                                            T1.LineStatus AS 状态,
	                                            T1.UnitPrice AS 单价,
	                                            T1.POItemQuantity AS 订购数量,
	                                            T1.DemandDeliveryDate AS 需求日期,
	                                            T1.ForeignNumber AS 外贸单号
                                        FROM
	                                        PurchaseOrderRecordByCMF T1
                                        WHERE
	                                        T1.PONumber = '" + ponumber + "' And T1.POStatus = 1 And T1.Superior='"+uid+"'";
            DataTable dtTemp = null;
            try
            {
                dtTemp = SQLHelper.GetDataTable(GlobalSpace.FSDBConnstr, sqlSelect);
            }
            catch (Exception ex)
            {
                MessageBoxEx.Show("发生异常：" + ex.Message);
            }
            dgv.DataSource = dtTemp;
            dgv.Columns["Guid"].Visible = false;
        }
        
        private void btnSendEMails_Click(object sender, EventArgs e)
        {
            string sqlSelectSupervisorEmail = @"Select Email,Password,Name From PurchaseDepartmentRBACByCMF Where UserID='"+userID+"'";
            string sqlCheck = @"Select count(Id) from PurchaseDepartmentCheckPasswordByCMF Where Supervisor ='" + userID + "' And IsValid = 0";
            if (SQLHelper.Exist(GlobalSpace.FSDBConnstr, sqlCheck))
            {
                string sqlSelectEmails = @"Select Email,Name From PurchaseDepartmentRBACByCMF Where SupervisorID='" + userID+"'";
                string sqlSelectPassword = @"SELECT
	                                                            Password,
	                                                            StartDate,
	                                                            FinishDate,
	                                                            IsValid
                                                            FROM
	                                                            PurchaseDepartmentCheckPasswordByCMF
                                                            WHERE
	                                                            Supervisor = '"+userID+"'   AND IsValid = 0 ORDER BY OperateDateTime DESC";
                DataTable dtTemp = SQLHelper.GetDataTable(GlobalSpace.FSDBConnstr, sqlSelectEmails);
                DataTable dtTempSupervisorEmail = SQLHelper.GetDataTable(GlobalSpace.FSDBConnstr, sqlSelectSupervisorEmail);
                DataTable dtTempPassword = SQLHelper.GetDataTable(GlobalSpace.FSDBConnstr, sqlSelectPassword);
                if (dtTemp.Rows.Count > 0)
                {
                    if(dtTempSupervisorEmail.Rows[0]["Email"].ToString() != DBNull.Value.ToString())
                    {

                        try
                        {
                            Dictionary<string, string> emailList = new Dictionary<string, string>();
                            for (int i = 0; i < dtTemp.Rows.Count; i++)
                            {
                                emailList.Add(dtTemp.Rows[i]["Email"].ToString(), dtTemp.Rows[i]["Name"].ToString());
                            }

                            Email email = new Email();
                            email.fromEmail = dtTempSupervisorEmail.Rows[0]["Email"].ToString();
                            email.fromPerson = dtTempSupervisorEmail.Rows[0]["Name"].ToString();                     
                            email.encoding = "UTF-8";
                            email.smtpServer = "192.168.8.3";
                            email.smtpPort = "25";
                            email.userName = dtTempSupervisorEmail.Rows[0]["Email"].ToString();
                            //对密码进行解密
                            byte[] debytes = Convert.FromBase64String(dtTempSupervisorEmail.Rows[0]["Password"].ToString());
                            string depwd = Encoding.UTF8.GetString(debytes);
                            email.passWord = depwd;
                            email.emailTitle = "采购订单审核用密码已经设置";
                            email.emailContent = "您好！采购订单审核用密码已经设置，当前密码：" + dtTempPassword.Rows[0]["Password"].ToString() + "，有效期自" + dtTempPassword.Rows[0]["StartDate"].ToString() + "至" + dtTempPassword.Rows[0]["FinishDate"].ToString() + "！";
                            MailHelper.SendEmail(email, emailList);

                        }
                        catch (Exception ex)
                        {
                            MessageBoxEx.Show(ex.Message);
                        }
                    }
                    else
                    {
                        MessageBoxEx.Show("当前没有设置发送邮箱，请先设置邮箱！", "提示");
                    }
       
                }
                else
                {
                    MessageBoxEx.Show("没有可用的员工邮箱列表！", "提示");
                    return;
                }
               
            }
            else
            {
                MessageBoxEx.Show("当前没有可用的审核密码，请设置！", "提示");
            }
        }
        
    
        private void dgvPO_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            dgvPO_CellDoubleClick(sender, e);
        }

        private void dgvPO_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.RowIndex >= 0)
            {
                ShowOrder(userID, dgvPO.Rows[e.RowIndex].Cells["采购单号"].Value.ToString(), dgvPOItemDetail);
            }
        }

        private void btnRefreshUnHandledPO_Click(object sender, EventArgs e)
        {
            GetUncheckedPO();
        }

        private void btnMakeAllChecked_Click(object sender, EventArgs e)
        {
            if (dgvPO.Rows.Count > 0)
            {
                foreach (DataGridViewRow dgvr in dgvPO.Rows)
                {
                    dgvr.Cells["POCheckChoose"].Value = true;
                }
            }
        }

        private void btnMakeAllCanceled_Click(object sender, EventArgs e)
        {
            if (dgvPO.Rows.Count > 0)
            {
                foreach (DataGridViewRow dgvr in dgvPO.Rows)
                {
                    dgvr.Cells["POCheckChoose"].Value = false;
                }
            }
        }
        
        private void btnCheckPass_Click(object sender, EventArgs e)
        {
            List<string> sqlPOList = new List<string>();
            Dictionary<string, string> userEmail = new Dictionary<string, string>();
            foreach (DataGridViewRow dgvr in dgvPO.Rows)
            {             
                //订单状态已选中同时为领导已审核状态
                if (Convert.ToBoolean(dgvr.Cells["POCheckChoose"].Value))
                {
                    string sqlUpdatePOItem = @"Update PurchaseOrderRecordByCMF Set POStatus = 2, CheckedWay = 'ManualChecked'  Where PONumber='"+ dgvr.Cells["采购单号"].Value.ToString() + "' And IsPurePO = 0 And POStatus = 1";

                    string buyerID = dgvr.Cells["Buyer"].Value.ToString();
                    string sqlSelect = @"Select Name,Email From PurchaseDepartmentRBACByCMF Where UserID = '" + buyerID + "'";
                    DataTable dtEmail = SQLHelper.GetDataTable(GlobalSpace.FSDBConnstr, sqlSelect);
                    if(dtEmail.Rows.Count > 0)
                    {
                        string email = dtEmail.Rows[0]["Email"].ToString();
                        string name = dtEmail.Rows[0]["Name"].ToString();
                        if(!userEmail.ContainsKey(name))
                        {
                            userEmail.Add(name, email);
                        }
                    }                  
                    sqlPOList.Add(sqlUpdatePOItem);
                }
            }

            if(sqlPOList.Count > 0)
            {
                var result = SQLHelper.BatchExecuteNonQuery(GlobalSpace.FSDBConnstr, sqlPOList);
                if(result)
                {
                    MessageBoxEx.Show("审核成功", "提示");
                    GetUncheckedPO();
                   
                    foreach(var v in userEmail)
                    {
                        string sqlSelectUserInfo = @"Select Email,Password,Name From PurchaseDepartmentRBACByCMF Where UserID='" + userID + "'";
                        DataTable dtUserInfo = SQLHelper.GetDataTable(GlobalSpace.FSDBConnstr, sqlSelectUserInfo);
                        string toName =v.Key;
                        string toEmail = v.Value;

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
                                    email.toEmail = toEmail;
                                    email.toPerson = toName;
                                    email.encoding = "UTF-8";
                                    email.smtpServer = smtpList[0];
                                    email.userName = dtUserInfo.Rows[0]["Email"].ToString();
                                    email.passWord = CommonOperate.Base64Decrypt(dtUserInfo.Rows[0]["Password"].ToString());
                                    email.emailTitle = "采购订单审核完成提醒";
                                    email.emailContent = toName + "：" + "<br>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + "您好!<br>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;采购订单申请已审批，请及时处理！";

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
                else
                {
                    MessageBoxEx.Show("审核失败", "提示");
                }
            }
            else
            {
                MessageBoxEx.Show("没有选中的订单！", "提示");
            }
        }
        /*
        private void btnSearchItem_Click(object sender, EventArgs e)
        {
            string sqlCriteriaType = string.Empty;
            string sqlCriteriaDate = string.Empty;
            string sqlCriteriaOrder = "  Order By POItemPlacedDate Desc";

            string strText = tbSearchItem.Text.Trim();
            string sqlSelectPONumber = string.Empty;
            if (CommonOperate.IsNumberOrString(tbSearchItem.Text.Trim()))
            {
                sqlSelectPONumber = @"SELECT TOP 200  PONumber FROM PurchaseOrderRecordByCMF  WHERE  Superior = '" + userID + "' And ItemNumber='" + strText + "' And IsPurePO = 0 " + sqlCriteriaOrder;
            }
            else
            {
                sqlSelectPONumber = @"SELECT TOP 200  PONumber FROM PurchaseOrderRecordByCMF  WHERE Superior = '" + userID + "' And ItemDescription like '%"+strText+ "%'   And IsPurePO = 0  "+ sqlCriteriaOrder;
            }

            if(rbtnDomestic.Checked)
            {
                sqlCriteriaType = " ForeignNumber = ''";
            }
            else if(rbtnForeign.Checked)
            {
                sqlCriteriaType = "ForeignNumber <> ''";
            }
            else
            {
                sqlCriteriaType = " 1=1 ";
            }

            if(rbtnDate.Checked)
            {
                sqlCriteriaDate = " (POItemPlacedDate >='"+dtpStartDate.Value.ToString("yyyy-MM-dd")+"' And POItemPlacedDate <='"+dtpEndDate.Value.ToString("yyyy-MM-dd") + "')";
            }
            else
            {
                sqlCriteriaDate = " 1=1 ";
            }


            string sqlSelectPO = @"Select   PONumber AS 采购单号,VendorNumber AS 供应商代码,VendorName AS 供应商名称,POItemPlacedDate AS 订单创建日期,Buyer AS 采购员  from PurchaseOrderRecordByCMF Where Superior = '" + userID + "' And PONumber In ("+sqlSelectPONumber+ ")  And IsPurePO = 1  "+" And "+sqlCriteriaType+"  And  "+sqlCriteriaDate+sqlCriteriaOrder;
            string sqlSelectPODetail = @"SELECT  
                                                      (case POStatus when  '0' then '已准备'
                                                                 when  '1' then '已提交'
                                                                 when  '2' then '已审核'
                                                                 when  '3' then '已下达' 
                                                                when  '4' then '已到货' 
                                                                when  '5' then '已收货' 
                                                                when  '6' then '已入库' 
                                                                when  '7' then '已开票' 
                                                        end     
                                                        ) as 物料状态,  
                                                    POItemPlacedDate	AS 下单日期,
                                                    ForeignNumber	AS 外贸单号,
                                                    PONumber	AS 采购单号,
                                                    ItemNumber	AS 物料代码,
                                                    ItemDescription	AS 物料名称,
                                                    Specification	AS 规格,
                                                    LineUM	AS 单位,
                                                    POItemQuantity	AS 数量,
                                                    QualityCheckStandard	AS 质量标准,
                                                    DemandDeliveryDate	AS 要求到货时间,
                                                    ItemUsedPoint	AS 提报单位,
                                                    Comment1	AS 备注,
                                                    VendorNumber	AS 供应商代码,
                                                    VendorName AS 供应商,
                                                    UnitPrice	AS 单价,
                                                    Comment2	AS 备注,
                                                    ActualDeliveryDate	AS 实际到货日期,
                                                    ActualDeliveryQuantity	AS 实际到货数量,
                                                    LotNumber	AS 供应商批号,
                                                    InvoiceNumber	AS 发票号,
                                                    InvoiceIssuedDateTime	AS 开票时间,
                                                    InstanceID	AS 实例号,
                                                    ContractID	AS 合同号,
                                                    Comment3	AS 备注
                                                    FROM
	                                                    PurchaseOrderRecordByCMF
                                                    WHERE
	                                                    Superior = '" + userID + "' And PONumber In ("+sqlSelectPONumber+ ") And IsPurePO = 0    " + " And " + sqlCriteriaType + "  And  " + sqlCriteriaDate + sqlCriteriaOrder;
           
            BindCustomerData(sqlSelectPO, sqlSelectPODetail);
        }

        private void tbSearchItem_TextChanged(object sender, EventArgs e)
        {
            tbSearchItem.Text = tbSearchItem.Text.ToUpper();
            tbSearchItem.SelectionStart = tbSearchItem.TextLength;
        }

        private void tbSearchItem_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar ==(char)13)
            {
                if(tbSearchItem.Text !="")
                {
                    btnSearchItem_Click(sender, e);
                }
            }
        }
        */
        private void superTabControlPanel3_Click(object sender, EventArgs e)
        {

        }
        
        private void rbtnDate_CheckedChanged(object sender, EventArgs e)
        {

        }


        /*
private void btnExportToExcel_Click(object sender, EventArgs e)
{
   DataSet ds = (DataSet)superGridControl1.PrimaryGrid.DataSource;
   DataTable dt = ds.Tables["Order Details"];

   string filePath = tbExportFilePath.Text;
   string sheetname = "Sheet1";

   XSSFWorkbook workbook = new XSSFWorkbook();
   ISheet sheet = workbook.CreateSheet(sheetname);
   IRow rowHead = sheet.CreateRow(0);
   ICell cell;

   //填写表头
   for (int i = 0; i < dt.Columns.Count; i++)
   {
       cell = rowHead.CreateCell(i, CellType.String);
       cell.SetCellValue(dt.Columns[i].Caption);
       //    cell.CellStyle = cellstyle;
   }
   //填写内容
   for (int i = 0; i < dt.Rows.Count; i++)
   {
       IRow row = sheet.CreateRow(i + 1);

       for (int j = 0; j < dt.Columns.Count; j++)
       {
           cell = row.CreateCell(j, CellType.String);
           //       cell.CellStyle = cellstyle2;
           if (j == 2 || j > 4)
           {
               if (dt.Rows[i][j] == DBNull.Value || dt.Rows[i][j].ToString() == "")
               {
                   cell.SetCellValue("");
               }
               else
               {
                   cell.SetCellValue(Convert.ToDouble(dt.Rows[i][j]));
               }

           }
           else
           {
               cell.SetCellValue(dt.Rows[i][j].ToString());
           }
       }


   }
   for (int j = 0; j < dt.Columns.Count; j++)
   {
       sheet.AutoSizeColumn(j);
   }

   if (!File.Exists(filePath))
   {
       try
       {
           using (FileStream fs = new FileStream(filePath, FileMode.Create, FileAccess.Write))
           {
               workbook.Write(fs);
               fs.Close();
           }
           Custom.MsgEx("导出数据成功！");
       }
       catch (Exception)
       {
           throw;
       }
   }
   else
   {
       if (MessageBoxEx.Show("当前同名文件已存在，是否覆盖该文件？", "提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
       {
           try
           {
               using (FileStream fs = new FileStream(filePath, FileMode.Create, FileAccess.Write))
               {
                   workbook.Write(fs);
                   fs.Close();
               }


               Custom.MsgEx("导出数据成功！" + filePath);
           }
           catch (Exception)
           {
               throw;
           }
       }
       else
       {
           return;
       }

   }

}
*/
    }
}
