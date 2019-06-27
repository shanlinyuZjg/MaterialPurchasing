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

namespace Global.Purchase
{
    public partial class Supervisor : Office2007Form
    {
        string userID = "MBJ";
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
        private void BindCustomerData()
        {
            DataSet _DataSet = new DataSet();

            using (OleDbConnection cn =
                new OleDbConnection(GlobalSpace.oledbconnstrFSDB))
            {
                string sqlSelectPO = @"Select  TOP 20 PONumber AS 采购单号,VendorNumber AS 供应商代码,VendorName AS 供应商名称,POCreatedDate AS 订单创建日期,Buyer AS 采购员  from PurchaseOrdersByCMF Where Supervisor = '"+userID+"' Order By POCreatedDate Desc";
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
	                                                    Superior = '" + userID+ "' Order By POItemPlacedDate Desc";

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
            BindCustomerData();
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
            string sqlSelect = @"Select Distinct PONumber as 采购单号,VendorNumber as 供应商码 ,VendorName as 供应商名称  From PurchaseOrderRecordByCMF Where Superior='"+userID+"' And POStatus=1";
            CommonOperate.DataGridViewShow(sqlSelect, GlobalSpace.FSDBConnstr, dgvPO);
        }

        
        private void ShowOrder(string uid,string ponumber,DataGridView dgv)
        {
            string sqlSelect = @"SELECT
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

        private void btnSetEmail_Click(object sender, EventArgs e)
        {         
            if(tbEmailAccount.Text.Trim() =="" || tbEmailPassword.Text.Trim()=="")
            {
                MessageBoxEx.Show("邮箱账号或密码不能为空！", "提示");
            }
            else
            {
                string email = tbEmailAccount.Text.Trim() + "@reyoung.com";
                byte[] bytes = Encoding.UTF8.GetBytes(tbEmailPassword.Text.Trim());
                string password = Convert.ToBase64String(bytes);
                string sqlUpdate = @"Update PurchaseDepartmentRBACByCMF  Set Email='"+email+"',Password='"+password+"' Where UserID='"+ userID + "'";
                if(SQLHelper.ExecuteNonQuery(GlobalSpace.FSDBConnstr, sqlUpdate) )
                {
                    MessageBoxEx.Show("设置成功！", "提示");
                }
                else
                {
                    MessageBoxEx.Show("设置失败！请联系管理员61075！", "提示");
                }               
            }
              
              
            /*  解密
              byte[] debytes = Convert.FromBase64String(password);
              string depwd = Encoding.UTF8.GetString(debytes);
            */
        }

        private void dgvPO_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            dgvPO_CellDoubleClick(sender, e);
        }

        private void dgvPO_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.RowIndex < 0)
            {
                MessageBoxEx.Show("请双击有效的区域！", "提示");
            }
            else
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
            foreach (DataGridViewRow dgvr in dgvPO.Rows)
            {             
                //订单状态已选中同时为领导已审核状态
                if (Convert.ToBoolean(dgvr.Cells["POCheckChoose"].Value) == true )
                {
                    string sqlUpdatePOItem = @"Update PurchaseOrderRecordByCMF Set POStatus = 2, CheckedWay = 'ManualChecked'  Where PONumber='"+ dgvr.Cells["采购单号"].Value.ToString() + "'";
                    string sqlUpdatePO = @"Update PurchaseOrdersByCMF Set POStatus = 2 Where PONumber='" + dgvr.Cells["采购单号"].Value.ToString() + "'";
                    if(SQLHelper.ExecuteNonQuery(GlobalSpace.FSDBConnstr, sqlUpdatePO) )
                    {
                        CommonOperate.EmptyDataGridView(dgvPOItemDetail);
                    }
                    else
                    {
                        MessageBoxEx.Show("更新订单状态时出错，请联系管理员！", "提示");
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

        private void btnSearchItem_Click(object sender, EventArgs e)
        {
            /*
            if(CommonOperate.IsNumberOrString(tbSearchItem.Text.Trim()))
            {
                MessageBoxEx.Show("正常的物料代码", "提示");
            }
            else
            {
                MessageBoxEx.Show("中文字符！", "提示");
            }*/
            string strText = tbSearchItem.Text.Trim();
            string sqlSelectPONumber = string.Empty;
            if (CommonOperate.IsNumberOrString(tbSearchItem.Text.Trim()))
            {
                sqlSelectPONumber = @"SELECT TOP 20  PONumber FROM PurchaseOrderRecordByCMF  WHERE  Superior = '" + userID + "' And ItemNumber='"+strText+"'  Order By POItemPlacedDate Desc";
            }
            else
            {
                sqlSelectPONumber = @"SELECT TOP 20  PONumber FROM PurchaseOrderRecordByCMF  WHERE Superior = '" + userID + "' And ItemDescription like '%"+strText+"%' Order By POItemPlacedDate Desc";
            }
            string sqlSelectPO = @"Select   PONumber AS 采购单号,VendorNumber AS 供应商代码,VendorName AS 供应商名称,POCreatedDate AS 订单创建日期,Buyer AS 采购员  from PurchaseOrdersByCMF Where Supervisor = '" + userID + "' And PONumber In ("+sqlSelectPONumber+") Order By POCreatedDate Desc";
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
	                                                    Superior = '" + userID + "' And PONumber In ("+sqlSelectPONumber+") Order By POItemPlacedDate Desc";
           
            BindCustomerData(sqlSelectPO, sqlSelectPODetail);
        }
    }
}
