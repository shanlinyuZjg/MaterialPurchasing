using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using Global;
using Global.Helper;
using SoftBrands.FourthShift.Transaction;

namespace Global.Finance
{
    public partial class InvoiceVerify : Office2007Form
    {
        string UnvoucheredAccount = string.Empty;//无票
        string VoucheredAccount = string.Empty;//有票
        string TaxRate = string.Empty;//税率
        string TaxAccrualAccount = string.Empty;//VAT账号
        string UserName = string.Empty;
        string FSID = string.Empty;
        string FSPassword = string.Empty;
        bool IsChose = false;
        int Count = 0;
        DataTable Dt;
        //字符集，此处用于供应商名字模糊查询供应商时使用
        Encoding GB2312 = Encoding.GetEncoding("gb2312");
        Encoding ISO88591 = Encoding.GetEncoding("iso-8859-1");
                 

        decimal InvoiceAmount = 0;

        public InvoiceVerify(string name, string fsID, string fsPassword)
        {
            this.EnableGlass = false;
            MessageBoxEx.EnableGlass = false;
            InitializeComponent();
            UserName = name;
            FSID = fsID;
            FSPassword = fsPassword;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void InvoiceVerify_Load(object sender, EventArgs e)
        {
            tbYear.Text = DateTime.Now.ToString("yy");
            tbMonth.Text = DateTime.Now.ToString("MM");
            tbPrepareToMatchAmount.Text = "0";
        //    MessageBox.Show(FSID+"-"+FSPassword);
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {           
            dgvDetail.DataSource = GetInfo(0);
            dgvDetail.Columns["Id"].Visible = false;
        }

        private DataTable GetInfo(int index, string invoiceNumber =null,string[] invoiceArray = null)
        {
            string sqlSelect = @"SELECT
                                                    InvoiceNumber AS 发票号码,
	                                                PONumber AS 采购单号,
	                                                LineNumber AS 行号,
	                                                VendorNumber AS 供应商码,
	                                                VendorName AS 供应商名,
	                                                ItemNumber AS 物料代码,
	                                                ItemDescription AS 物料描述,
	                                                UM AS 单位,
	                                                ReceiveQuantity AS 入库数量,
	                                                UnitPrice AS 采购单价,Id
                                                FROM
	                                                PurchaseOrderInvoiceRecordByCMF
                                                WHERE ";
            string sqlCriteria = string.Empty;
            if (index == 0)
            {
                 sqlCriteria = @" status = 2 ";
            }
            else if(index == 1)
            {
                sqlCriteria = @" InvoiceNumber='"+invoiceNumber+"'  And status <> 4  ";
            }
            else if(index == 2)
            {
                sqlCriteria = @" InvoiceNumber IN('{0}')  And status <> 4  ";
                sqlCriteria = string.Format(sqlCriteria, string.Join("','", invoiceArray));
            }
            
	        string sqlCriteria2 = @" order by PONumber,LineNumber  ASC";
            return SQLHelper.GetDataTable(GlobalSpace.FSDBConnstr, sqlSelect+ sqlCriteria+ sqlCriteria2);
        }

        //获取所有未处理的发票和订单信息
        private DataTable GetPOAndInvoice(int status)
        {
            string sqlSelect = @"";

            return SQLHelper.GetDataTable(GlobalSpace.FSDBConnstr, sqlSelect);
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
         

            if (string.IsNullOrEmpty(tbTaxCode.Text) || string.IsNullOrEmpty(tbVATRate.Text))
            {
                MessageBoxEx.Show("当前税码或税率为空！", "提示");
                return;
            }

            InvoiceAmount = Convert.ToDecimal(tbTotalAmount.Text);
            
            List<string> idList = new List<string>();


            if(string.IsNullOrEmpty(TaxAccrualAccount))
            {
                MessageBoxEx.Show("当前税号为空！", "提示");
                return;
            }

            if(string.IsNullOrWhiteSpace(tbInvoiceNumber.Text))
            {
                MessageBoxEx.Show("请输入发票号码！", "提示");
                return;
            }

            string vendorNumber = string.Empty;
            string invoiceNumber = string.Empty;
            string poNumber = string.Empty;
            string lineNumber = string.Empty;
            int checkedCount = 0;
            decimal amountChecked = 0;
            string taxCode = string.Empty;
            string invoiceType = string.Empty;
            string year = string.Empty;
            string period = string.Empty;
            string taxAmount = string.Empty;

            DataTable dt0 = (DataTable)dgvDetail.DataSource;
            DataTable dtInvoice = dt0.Clone();

            foreach (DataGridViewRow dgvr in dgvDetail.Rows)
            {
                if (Convert.ToBoolean(dgvr.Cells["Check"].Value))
                {
                    //  amountChecked += Convert.ToDecimal(Convert.ToDouble(dgvr.Cells["采购单价"].Value) * Convert.ToDouble(dgvr.Cells["入库数量"].Value)) / 100 * Convert.ToDecimal(TaxRate);//VAT金额=0.01*税率*计税金额
                    //   drChose = (dgvr.DataBoundItem as DataRowView).Row;
                    amountChecked += Convert.ToDecimal(Convert.ToDouble(dgvr.Cells["采购单价"].Value) * Convert.ToDouble(dgvr.Cells["入库数量"].Value)) ;//VAT金额=0.01*税率*计税金额
                    vendorNumber = dgvr.Cells["供应商码"].Value.ToString();
                    invoiceNumber = tbInvoiceNumber.Text;
                    poNumber = dgvr.Cells["采购单号"].Value.ToString();
                    lineNumber = dgvr.Cells["行号"].Value.ToString();
                    checkedCount++;
                    DataRow dr = dtInvoice.NewRow();
                    dr = (dgvr.DataBoundItem as DataRowView).Row;
                    dtInvoice.Rows.Add(dr.ItemArray);
                    idList.Add(dgvr.Cells["Id"].Value.ToString());
                }
            }
            //判断该供应商的发票是否已经存在
            string sqlExist = @"Select Count(VendorID) From FS_APInvoiceHeader Where InvoiceNumber = '" +invoiceNumber + "'  AND VendorID ='"+vendorNumber+"'";
            if(SQLHelper.Exist(GlobalSpace.FSDBMRConnstr,sqlExist))
            {
                MessageBoxEx.Show("该供应商的发票号码已存在！", "提示");
                return;
            }

            amountChecked = Math.Round(Convert.ToDecimal(amountChecked), 2, MidpointRounding.AwayFromZero);

            if (checkedCount == 0)
            {
                MessageBoxEx.Show("当前无选中行！", "提示");
                return;
            }

            string sqlSelectVendor = @"SELECT	                              
	                                 UnvoucheredAccount AS 无票账号
	                                 ,[VoucheredAccount] AS 有票账号 
                                   FROM  [_NoLock_FS_Vendor] 
                                   WHERE
	                               [VendorID] = '" + vendorNumber + "' AND [VendorStatus] = 'A'";

            DataTable dt = SQLHelper.GetDataTable(GlobalSpace.FSDBMRConnstr, sqlSelectVendor);

            if (dt.Rows.Count == 0)
            {
                MessageBoxEx.Show("未查到该供应商信息或供应商已停用！", "提示");
                return;
            }
            else
            {
                UnvoucheredAccount = dt.Rows[0]["无票账号"].ToString();//无票
                VoucheredAccount = dt.Rows[0]["有票账号"].ToString();//有票
            }


            FSFunctionLib.FSConfigFileInitialize(GlobalSpace.fsconfigfilepath, FSID, FSPassword);
            //增加发票信息
            if (AddAPInvoiceInfo(tbInvoiceNumber.Text,vendorNumber,tbPrepareToMatchAmount.Text,tbTotalAmount.Text,cbbTaxType.Text,tbTaxCode.Text,tbTaxAmount.Text,tbYear.Text,tbMonth.Text))
            {
                //判断记录条数
                if(dtInvoice.Rows.Count == 1)
                {
                    //查询已入库信息
                    string sqlSelectPORev = @"SELECT
	                                                PONumber 采购单号,
	                                                POReceiptSequenceNumber 序号,
	                                                PONumber + '-' + POReceiptSequenceNumber 订单号序号,
	                                                POLineNumber 行号,
	                                                LineItemNumber 物料代码,
	                                                POReceiptQuantity 入库数量,
	                                                LineItemUM 单位,
	                                                POReceiptLocalUnitCost 单价,
	                                                POReceiptLocalExtendedCost AS 金额,
	                                                InvoiceMatchedQuantity AS 已匹配数量,
	                                                InvoiceMatchedControllingAmount 已匹配金额,
	                                                POReceiptQuantity - InvoiceMatchedQuantity AS 匹配数量,
	                                                POReceiptLocalUnitCost AS 匹配单价,
	                                                round(
		                                                (
			                                                POReceiptQuantity - InvoiceMatchedQuantity
		                                                ) * POReceiptLocalUnitCost,
		                                                2
	                                                ) AS 匹配金额
                                                FROM
	                                                dbo.FS_APReceiptLine
                                                WHERE
	                                                PONumber = '{0}'
                                                AND POLineNumber = '{1}' AND POReceiptQuantity={2} ORDER BY APReceiptLineKey DESC";
                    sqlSelectPORev = string.Format(sqlSelectPORev, dtInvoice.Rows[0]["采购单号"].ToString(), dtInvoice.Rows[0]["行号"].ToString(), Convert.ToDouble(dtInvoice.Rows[0]["入库数量"]));
                    DataTable dtPORev = SQLHelper.GetDataTable(GlobalSpace.FSDBMRConnstr, sqlSelectPORev);

                    for (int i = dtPORev.Rows.Count - 1; i >= 0; i--)
                    {
                        if (dtPORev.Rows[i]["入库数量"].ToString() == dtPORev.Rows[i]["已匹配数量"].ToString())
                        {
                            dtPORev.Rows.RemoveAt(i);
                        }
                    }
                    if (!VerifyInvoiceToPO(dtPORev, vendorNumber, invoiceNumber,InvoiceAmount))
                    {
                        MessageBoxEx.Show("匹配订单失败！", "提示");
                        return;
                    }
                }
                else
                {
                    for (int j = 0; j < dtInvoice.Rows.Count; j++)
                    {
                        //查询已入库信息
                        string sqlSelectPORev = @"SELECT
	                                                PONumber 采购单号,
	                                                POReceiptSequenceNumber 序号,
	                                                PONumber + '-' + POReceiptSequenceNumber 订单号序号,
	                                                POLineNumber 行号,
	                                                LineItemNumber 物料代码,
	                                                POReceiptQuantity 入库数量,
	                                                LineItemUM 单位,
	                                                POReceiptLocalUnitCost 单价,
	                                                POReceiptLocalExtendedCost AS 金额,
	                                                InvoiceMatchedQuantity AS 已匹配数量,
	                                                InvoiceMatchedControllingAmount 已匹配金额,
	                                                POReceiptQuantity - InvoiceMatchedQuantity AS 匹配数量,
	                                                POReceiptLocalUnitCost AS 匹配单价,
	                                                round(
		                                                (
			                                                POReceiptQuantity - InvoiceMatchedQuantity
		                                                ) * POReceiptLocalUnitCost,
		                                                2
	                                                ) AS 匹配金额
                                                FROM
	                                                dbo.FS_APReceiptLine
                                                WHERE
	                                                PONumber = '{0}'
                                                AND POLineNumber = '{1}' AND POReceiptQuantity={2} ORDER BY APReceiptLineKey DESC";
                        sqlSelectPORev = string.Format(sqlSelectPORev, dtInvoice.Rows[j]["采购单号"].ToString(), dtInvoice.Rows[j]["行号"].ToString(), Convert.ToDouble(dtInvoice.Rows[j]["入库数量"]));
                        DataTable dtPORev = SQLHelper.GetDataTable(GlobalSpace.FSDBMRConnstr, sqlSelectPORev);

                        for (int i = dtPORev.Rows.Count - 1; i >= 0; i--)
                        {
                            if (dtPORev.Rows[i]["入库数量"].ToString() == dtPORev.Rows[i]["已匹配数量"].ToString())
                            {
                                dtPORev.Rows.RemoveAt(i);
                            }
                        }

                        //匹配订单信息
                        if (j < dtInvoice.Rows.Count - 1)
                        {
                            if (VerifyInvoiceToPO(dtPORev, vendorNumber, invoiceNumber))
                            {
                                InvoiceAmount = InvoiceAmount - Math.Round(Convert.ToDecimal(dtPORev.Rows[0]["入库数量"]) * Convert.ToDecimal(dtPORev.Rows[0]["单价"]), 2, MidpointRounding.AwayFromZero);  ;
                            }
                            else
                            {
                                MessageBoxEx.Show("匹配订单失败！", "提示");
                                return;
                            }
                        }
                        else
                        {
                            if (!VerifyInvoiceToPO(dtPORev, vendorNumber, invoiceNumber, InvoiceAmount))
                            {
                                MessageBoxEx.Show("匹配订单失败！", "提示");
                                return;
                            }
                        }

                    }
                }
     
                //过账
                if (PostAccount(dt, Convert.ToDecimal(tbPrepareToMatchAmount.Text), Convert.ToDecimal(tbTotalAmount.Text), vendorNumber, invoiceNumber))
                {
                    string sqlUpdate = @"UPDATE PurchaseOrderInvoiceRecordByCMF SET Status = 4,FinanceUpdateDateTime='" + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss") + "' WHERE Id IN ('{0}')";

                    sqlUpdate = string.Format(sqlUpdate, string.Join("','", idList.ToArray()));

                    if (SQLHelper.ExecuteNonQuery(GlobalSpace.FSDBConnstr, sqlUpdate))
                    {
                        dgvDetail.DataSource = GetInfo(0);
                        dgvDetail.Columns["Id"].Visible = false;
                        MessageBoxEx.Show("过账完成！", "提示");
                        tbTaxAmount.Text = "";
                        tbTaxCode.Text = "";
                        tbVATRate.Text = "";
                        tbTotalAmount.Text = "";
                        tbPrepareToMatchAmount.Text = "";
                        tbInvoiceNumber.Text = "";
                    }
                    else
                    {
                        MessageBoxEx.Show("过账完成，更新发票记录状态失败！", "提示");
                    }              
                }
                else
                {
                    MessageBoxEx.Show("过账失败！", "提示");
                }
            }
            else
            {
                MessageBoxEx.Show("核销失败！", "提示");
            }
            FSFunctionLib.FSExit();
        }

        private void tbVATCode_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {/*
                if(!string.IsNullOrEmpty(tbVATCode.Text.Trim()))
                {
                    string sqlSelect = @"SELECT 
                             [TaxCode] 税金代码
                             ,[TaxDescription] 税金说明
                             ,[TaxRate] 税率
                             ,[TaxAccrualAccount] 应付税金账号
                           FROM [FS_TaxCode] where [TaxCode]='" + tbVATCode.Text.Trim() + "'";
                }
                */
            }

        }
      

        public bool APID10_1(APID10 myAPID10,string VendorID, string InvoiceNumber, string TransactionAccount, string TransactionAmountCredit, string TransactionAmountDebit)
        {
            myAPID10.VendorID.Value = VendorID;
            myAPID10.InvoiceNumber.Value = InvoiceNumber;
            myAPID10.TransactionAccount.Value = TransactionAccount;
            myAPID10.TransactionAmountCredit.Value = TransactionAmountCredit;//贷方
            myAPID10.TransactionAmountDebit.Value = TransactionAmountDebit;//借方

            //myAPID10.VendorID.Value = "370739";
            //myAPID10.InvoiceNumber.Value = "IA2001";
            //myAPID10.TransactionAccount.Value = "1AP3-70-739-212100";
            //myAPID10.TransactionAmountCredit.Value = "310";//贷方
            //myAPID10.TransactionAmountDebit.Value = "0.00";//借方

            if (FSFunctionLib.fstiClient.ProcessId(myAPID10, null))
            {
                return true;
            }
            else
            {

                FSTIError error = FSFunctionLib.fstiClient.TransactionError;

       //         string TXT = (GB2312.GetString(ISO88591.GetBytes(error.Description)));
                MessageBoxEx.Show("APID10_1 "+ error.Description);
       //         MessageBoxEx.Show("APID10_1 "+ TXT);
                CommonOperate.WriteFSErrorLog("APID10", myAPID10, error, FSID);                
            }
            return false;
        }
        public bool APID10_2(APID10 myAPID10,string VendorID, string InvoiceNumber, string TransactionAccount, string TransactionAmountCredit, string TransactionAmountDebit)
        {
            myAPID10.VendorID.Value = VendorID;
            myAPID10.InvoiceNumber.Value = InvoiceNumber;
            myAPID10.TransactionAccount.Value = TransactionAccount;
            myAPID10.TransactionAmountCredit.Value = TransactionAmountCredit;//贷方
            myAPID10.TransactionAmountDebit.Value = TransactionAmountDebit;//借方
            //myAPID10.VendorID.Value = "370739";
            //myAPID10.InvoiceNumber.Value = "IA2001";
            //myAPID10.TransactionAccount.Value = "1AP3-70-739-212101";
            //myAPID10.TransactionAmountCredit.Value = "0.00";//贷方
            //myAPID10.TransactionAmountDebit.Value = "274.34";//借方
            if (FSFunctionLib.fstiClient.ProcessId(myAPID10, null))
            {
                return true;
            }
            else
            {
                FSTIError error = FSFunctionLib.fstiClient.TransactionError;

     //           string TXT = (GB2312.GetString(ISO88591.GetBytes(error.Description)));
                MessageBoxEx.Show("APID10_2 " + error.Description);
  //              MessageBoxEx.Show("APID10_2 " + TXT);
                CommonOperate.WriteFSErrorLog("APID10", myAPID10, error, FSID);
            }
            return false;
        }
        public bool APID10_3(APID10 myAPID10,string VendorID, string InvoiceNumber, string TransactionAccount, string TransactionAmountCredit, string TransactionAmountDebit)
        {
            myAPID10.VendorID.Value = VendorID;
            myAPID10.InvoiceNumber.Value = InvoiceNumber;
            myAPID10.TransactionAccount.Value = TransactionAccount;
            myAPID10.TransactionAmountCredit.Value = TransactionAmountCredit;//贷方
            myAPID10.TransactionAmountDebit.Value = TransactionAmountDebit;//借方

            //myAPID10.VendorID.Value = "370739";
            //myAPID10.InvoiceNumber.Value = "IA2001";
            //myAPID10.TransactionAccount.Value = "1AP3-70-739-212100";
            //myAPID10.TransactionAmountCredit.Value = "310";//贷方
            //myAPID10.TransactionAmountDebit.Value = "0.00";//借方
            if (FSFunctionLib.fstiClient.ProcessId(myAPID10, null))
            {
                return true;
            }
            else
            {
                FSTIError error = FSFunctionLib.fstiClient.TransactionError;

       //         string TXT = (GB2312.GetString(ISO88591.GetBytes(error.Description)));
                MessageBoxEx.Show("APID10_3 " + error.Description);
       //         MessageBoxEx.Show("APID10_3 " + TXT);
                CommonOperate.WriteFSErrorLog("APID10", myAPID10, error, FSID);
            }
            return false;
        }
        public bool APID10_4(APID10 myAPID10,string VendorID, string InvoiceNumber, string TransactionAccount, string TransactionAmountCredit, string TransactionAmountDebit)
        {
            myAPID10.VendorID.Value = VendorID;
            myAPID10.InvoiceNumber.Value = InvoiceNumber;
            myAPID10.TransactionAccount.Value = TransactionAccount;
            myAPID10.TransactionAmountCredit.Value = TransactionAmountCredit;//贷方
            myAPID10.TransactionAmountDebit.Value = TransactionAmountDebit;//借方

            //myAPID10.VendorID.Value = "370739";
            //myAPID10.InvoiceNumber.Value = "IA2001";
            //myAPID10.TransactionAccount.Value = "1000-13-005-217112";
            //myAPID10.TransactionAmountCredit.Value = "0.00";//贷方
            //myAPID10.TransactionAmountDebit.Value = "35.66";//借方
            if (FSFunctionLib.fstiClient.ProcessId(myAPID10, null))
            {
                return true;
            }
            else
            {
                FSTIError error = FSFunctionLib.fstiClient.TransactionError;

         //       string TXT = (GB2312.GetString(ISO88591.GetBytes(error.Description)));
                MessageBoxEx.Show("APID10_4 " + error.Description);
     //           MessageBoxEx.Show("APID10_4 " + TXT);
                CommonOperate.WriteFSErrorLog("APID10", myAPID10, error, FSID);
            }
            return false;
        }
        public bool APID12_1(APID12 myAPID12,string VendorID, string InvoiceNumber)
        {           
            myAPID12.VendorID.Value = VendorID;
            myAPID12.InvoiceNumber.Value = InvoiceNumber;

            //myAPID12.VendorID.Value = "370739";
            //myAPID12.InvoiceNumber.Value = "IA2001";
            if (FSFunctionLib.fstiClient.ProcessId(myAPID12, null))
            {
                return true;
            }
            else
            {
                FSTIError error = FSFunctionLib.fstiClient.TransactionError;

      //          string TXT = (GB2312.GetString(ISO88591.GetBytes(error.Description)));
                 MessageBoxEx.Show("APID12_1 " + error.Description);
   //             MessageBoxEx.Show("APID12_1 " + TXT);
                CommonOperate.WriteFSErrorLog("APID12", myAPID12, error, FSID);
            }
            return false;
        }

    
        //增加应付账款发票信息
        private bool AddAPInvoiceInfo(string invoiceNumber,string vendorID,string vatBaseAmount,string poReceiptAoumt,string invoiceType,string taxCode,string taxAmount,string year,string period)
        {
            string poNumber = string.Empty;
            string lineNumber = string.Empty;

            APID00 myAPID00 = new APID00();
            myAPID00.VendorID.Value = vendorID;//供应商代码
            myAPID00.InvoiceNumber.Value = invoiceNumber;//发票号                                                 
            myAPID00.InvoiceType.Value = invoiceType;// cbbTaxType.Text;//发票类型                                                     
            myAPID00.POReceiptControllingAmount.Value = poReceiptAoumt.ToString();// (Convert.ToDouble(dtInvoice.Rows[0]["入库数量"]) * Convert.ToDouble(dtInvoice.Rows[0]["采购单价"])).ToString();
            myAPID00.TaxCode.Value = taxCode; // cbbTaxCode.Text;
            myAPID00.VATBaseControllingAmount.Value = vatBaseAmount.ToString();// (Convert.ToDouble(dtInvoice.Rows[0]["入库数量"]) * Convert.ToDouble(dtInvoice.Rows[0]["采购单价"])).ToString();
            myAPID00.VATControllingAmount.Value = taxAmount; // tbTaxAmount.Text;
            myAPID00.AccountingPeriod.Value = period; // tbMonth.Text;
            myAPID00.AccountingYear.Value = year; //tbYear.Text;

            try
            {
                if (FSFunctionLib.fstiClient.ProcessId(myAPID00, null))
                {
                    return true;
                }
                else
                {

                    FSTIError error = FSFunctionLib.fstiClient.TransactionError;
        //            string TXT = (GB2312.GetString(ISO88591.GetBytes(error.Description)));
                    MessageBox.Show("APID10_1 " + error.Description);
            //        MessageBox.Show("APID10_1 " + TXT);
                    CommonOperate.WriteFSErrorLog("APID00", myAPID00, error, FSID);
                    MessageBoxEx.Show("增加应付发票APID00失败！", "提示");                   
                }
            }
            catch (Exception ex)
            {
                MessageBoxEx.Show("异常：增加应付发票APID00失败!"+ex.Message, "提示");
            }
            return false;
        }
        //将发票核销到PO 1对1
        private bool  VerifyInvoiceToPO(DataTable dt,string vendorID,string invoiceNumber)
        {
            //将发票核销到PO
            APID03 myAPID03 = new APID03();
           
            myAPID03.VendorID.Value = vendorID;
            myAPID03.InvoiceNumber.Value = invoiceNumber;
            myAPID03.PONumber.Value = dt.Rows[0]["采购单号"].ToString();
            myAPID03.POReceiptSequenceNumber.Value = dt.Rows[0]["序号"].ToString();
            myAPID03.PONumberReceiptSequenceNumber.Value = dt.Rows[0]["订单号序号"].ToString();
            myAPID03.LineItemNumber.Value = dt.Rows[0]["行号"].ToString();
            myAPID03.ItemAccountMoCo.Value = dt.Rows[0]["物料代码"].ToString();
            myAPID03.POReceiptQuantity.Value = dt.Rows[0]["入库数量"].ToString();
            myAPID03.InvoiceMatchedQuantity.Value = dt.Rows[0]["已匹配数量"].ToString();
            myAPID03.InvoiceQuantity.Value = dt.Rows[0]["匹配数量"].ToString();
            myAPID03.LineItemUM.Value = dt.Rows[0]["单位"].ToString();
            myAPID03.InvoiceControllingUnitCost.Value = dt.Rows[0]["匹配单价"].ToString();

            try
            {
                if (!FSFunctionLib.fstiClient.ProcessId(myAPID03, null))
                {
                    FSTIError error = FSFunctionLib.fstiClient.TransactionError;
                //  CommonOperate.WriteFSErrorLog("APID03", myAPID03, error, FSID, myAPID03.VendorID.Value + " " + myAPID03.InvoiceNumber.Value + " " + myAPID03.PONumber.Value+" "+ myAPID03.LineItemNumber.Value+" 入库数量："+ myAPID03.POReceiptQuantity.Value);
                CommonOperate.WriteFSErrorLog("APID03", myAPID03, error, FSID);
                 
             //       string TXT = (GB2312.GetString(ISO88591.GetBytes(error.Description)));
                    MessageBox.Show("APID10_1 " + error.Description);
          //          MessageBox.Show("APID10_1 " + TXT);
                    MessageBoxEx.Show("将发票核销到采购订单APID03失败！", "提示");
                    return false;
                }
                 
            }
            catch (Exception ex)
            {
                MessageBoxEx.Show("异常：将发票核销到采购订单APID03失败！" + ex.Message, "提示");
            }
  
            return true;
        }
        //将发票核销到PO 1对1
        private bool VerifyInvoiceToPO(DataTable dt, string vendorID, string invoiceNumber,decimal leftAmount)
        {
            //将发票核销到PO
            APID03 myAPID03 = new APID03();

            myAPID03.VendorID.Value = vendorID;
            myAPID03.InvoiceNumber.Value = invoiceNumber;
            myAPID03.PONumber.Value = dt.Rows[0]["采购单号"].ToString();
            myAPID03.POReceiptSequenceNumber.Value = dt.Rows[0]["序号"].ToString();
            myAPID03.PONumberReceiptSequenceNumber.Value = dt.Rows[0]["订单号序号"].ToString();
            myAPID03.LineItemNumber.Value = dt.Rows[0]["行号"].ToString();
            myAPID03.ItemAccountMoCo.Value = dt.Rows[0]["物料代码"].ToString();
            if(dt.Rows[0]["入库数量"].ToString().Contains("-"))
            {
                myAPID03.POReceiptQuantity.Value = dt.Rows[0]["入库数量"].ToString()+ "-" ;
                myAPID03.InvoiceQuantity.Value = dt.Rows[0]["匹配数量"].ToString()+ "-";
            }
            else
            {
                myAPID03.POReceiptQuantity.Value = dt.Rows[0]["入库数量"].ToString();
                myAPID03.InvoiceQuantity.Value = dt.Rows[0]["匹配数量"].ToString();
            }

            myAPID03.InvoiceMatchedQuantity.Value = dt.Rows[0]["已匹配数量"].ToString();
            
            myAPID03.LineItemUM.Value = dt.Rows[0]["单位"].ToString();
            myAPID03.InvoiceControllingUnitCost.Value = (Math.Round(leftAmount/Convert.ToDecimal(dt.Rows[0]["入库数量"]),8)).ToString() ;//dt.Rows[0]["匹配单价"].ToString();

            try
            {
                if (!FSFunctionLib.fstiClient.ProcessId(myAPID03, null))
                {
                    FSTIError error = FSFunctionLib.fstiClient.TransactionError;
                    //  CommonOperate.WriteFSErrorLog("APID03", myAPID03, error, FSID, myAPID03.VendorID.Value + " " + myAPID03.InvoiceNumber.Value + " " + myAPID03.PONumber.Value+" "+ myAPID03.LineItemNumber.Value+" 入库数量："+ myAPID03.POReceiptQuantity.Value);
                    CommonOperate.WriteFSErrorLog("APID03", myAPID03, error, FSID);
             //       MessageBoxEx.Show("将发票核销到采购订单APID03失败！", "提示");
               
        //            string TXT = (GB2312.GetString(ISO88591.GetBytes(error.Description)));
                    MessageBoxEx.Show("APID10_1 " + error.Description);
   //                 MessageBoxEx.Show("APID10_1 " + TXT);
                    return false;
                }

            }
            catch (Exception ex)
            {
                MessageBoxEx.Show("异常：将发票核销到采购订单APID03失败！" + ex.Message, "提示");
            }

            return true;
        }
       
        //过账处理
        private bool PostAccount(DataTable dt,decimal amountChecked,decimal amountInvoiced,string vendorNumber,string invoiceNumber)
        {
            Decimal s = Convert.ToDecimal(tbTotalAmount.Text) - Convert.ToDecimal(tbPrepareToMatchAmount.Text);
            APID10 myAPID10 = new APID10();
            APID12 myAPID12 = new APID12();
            if (s > 0)//发票金额>订单金额  差异在借方
            {
                if (APID10_1(myAPID10, vendorNumber, invoiceNumber, "1000-00-000-123300", "0", s.ToString()))
                {
                    if (APID10_2(myAPID10, vendorNumber, invoiceNumber, UnvoucheredAccount.ToString(), "0", tbPrepareToMatchAmount.Text))
                    {
                        if (APID10_3(myAPID10, vendorNumber, invoiceNumber, TaxAccrualAccount.ToString(), "0", tbTaxAmount.Text))
                        {
                            if (APID10_4(myAPID10, vendorNumber, invoiceNumber, VoucheredAccount.ToString(), (Convert.ToDecimal(tbTotalAmount.Text) + Convert.ToDecimal(tbTaxAmount.Text)).ToString(), "0"))
                            {
                                if (APID12_1(myAPID12, vendorNumber, invoiceNumber))
                                {
                                    return true;
                                }
                            }
                        }
                    }
                }
            }
            else if(s < 0)//发票金额<订单金额 差异在贷方
            {
                s = (-1)*s;
                if (APID10_1(myAPID10, vendorNumber, invoiceNumber, "1000-00-000-123300", s.ToString(), "0"))
                {
                    if (APID10_2(myAPID10, vendorNumber, invoiceNumber, UnvoucheredAccount.ToString(), "0", tbPrepareToMatchAmount.Text))
                    {
                        if (APID10_3(myAPID10, vendorNumber, invoiceNumber, TaxAccrualAccount.ToString(), "0", tbTaxAmount.Text))
                        {
                            if (APID10_4(myAPID10, vendorNumber, invoiceNumber, VoucheredAccount.ToString(),(Convert.ToDecimal(tbTotalAmount.Text)+Convert.ToDecimal(tbTaxAmount.Text)).ToString() , "0"))
                            {
                                if (APID12_1(myAPID12, vendorNumber, invoiceNumber))
                                {
                                    return true;
                                }
                            }
                        }
                    }
                }
            }
            else//无差异
            {
                if (APID10_2(myAPID10, vendorNumber, invoiceNumber, UnvoucheredAccount.ToString(), "0", tbPrepareToMatchAmount.Text))
                {
                    if (APID10_3(myAPID10, vendorNumber, invoiceNumber, TaxAccrualAccount.ToString(), "0", tbTaxAmount.Text))
                    {
                        if (APID10_4(myAPID10, vendorNumber, invoiceNumber, VoucheredAccount.ToString(), (Convert.ToDecimal(tbTotalAmount.Text) + Convert.ToDecimal(tbTaxAmount.Text)).ToString(), "0"))
                        {
                            if (APID12_1(myAPID12, vendorNumber, invoiceNumber))
                            {
                                return true;
                            }
                        }
                    }
                }
            }
            return false;
        }

        private void GetTaxRate(string taxCode)
        {
            string sqlSelect = @"SELECT 
                             [TaxCode] 税金代码
                             ,[TaxRate] 税率
                             ,[TaxAccrualAccount] 应付税金账号
                           FROM [FS_TaxCode] where [TaxCode]='" + taxCode + "'";
            DataTable dt = SQLHelper.GetDataTable(GlobalSpace.FSDBMRConnstr, sqlSelect);

            if(dt.Rows.Count > 0)
            {
                TaxRate = dt.Rows[0]["税率"].ToString();
                TaxAccrualAccount = dt.Rows[0]["应付税金账号"].ToString();//vat账号
                tbVATRate.Text = TaxRate;        
            }
            else
            {
                MessageBoxEx.Show("未查到该税码对应的税率！", "提示");
            }           
        }

        double sum = 0;
        private void dgvDetail_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == 0 && (bool)dgvDetail.Rows[e.RowIndex].Cells[0].Value == true)
                {
                    sum = sum +Convert.ToDouble( dgvDetail.Rows[e.RowIndex].Cells[10].Value);
                    tbPrepareToMatchAmount.Text = sum.ToString();
                    //MessageBox.Show("Test");
                }
                else if (e.ColumnIndex == 0 && (bool)dgvDetail.Rows[e.RowIndex].Cells[0].Value == false)
                {
                    sum = sum - Convert.ToDouble( dgvDetail.Rows[e.RowIndex].Cells[10].Value);
                    tbPrepareToMatchAmount.Text = sum.ToString();
                    // MessageBox.Show("Teasdasdadasdst");
                }
            }
            catch (Exception ex)
            {
                MessageBoxEx.Show("异常：" + ex.Message);
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void dgvDetail_CellClick(object sender, DataGridViewCellEventArgs e)
        {
          
        }

        private void tnYes_Click(object sender, EventArgs e)
        {
            if(dgvDetail.Rows.Count == 0)
            {
                MessageBoxEx.Show("当前无可用行！", "提示");
                return;
            }

            Count++;
            decimal poAmount = 0;
            DataTable dt = (DataTable)dgvDetail.DataSource;
            Dt = dt.Clone();
            
            foreach (DataGridViewRow dgvr in dgvDetail.Rows)
            {
                if (Convert.ToBoolean(dgvr.Cells["Check"].Value))
                {
                    poAmount += Math.Round(Convert.ToDecimal(Convert.ToDecimal(dgvr.Cells["入库数量"].Value) * Convert.ToDecimal(dgvr.Cells["采购单价"].Value)), 2, MidpointRounding.AwayFromZero);
                    if(dgvr.Cells["行号"].Value.ToString().Length == 1)
                    {
                        dgvr.Cells["行号"].Value = "00"+dgvr.Cells["行号"].Value.ToString();
                    }
                    else if (dgvr.Cells["行号"].Value.ToString().Length == 2)
                    {
                        dgvr.Cells["行号"].Value = "0" + dgvr.Cells["行号"].Value.ToString();
                    }

                    DataRow dr = Dt.NewRow();
                    dr = (dgvr.DataBoundItem as DataRowView).Row;//微软提供的唯一的转换DataRow
                    Dt.Rows.Add(dr.ItemArray);
                }
            }
            //tbPrepareToMatchAmount.Text = poAmount.ToString();
            tbPrepareToMatchAmount.Text = Math.Round(Convert.ToDecimal(poAmount), 2, MidpointRounding.AwayFromZero).ToString();//
            tbTaxCode.Focus();
        }

        private void btnConfirmPORev_Click(object sender, EventArgs e)
        {
            Count++;
        }

        private void tabControlPanel1_Click(object sender, EventArgs e)
        {

        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {

        }

        private void btnViewRecord_Click(object sender, EventArgs e)
        {
            string sqlSelect = @"SELECT
                                                    InvoiceNumber AS 发票号码,
	                                                PONumber AS 采购单号,
	                                                LineNumber AS 行号,
	                                                VendorNumber AS 供应商码,
	                                                VendorName AS 供应商名,
	                                                ItemNumber AS 物料代码,
	                                                ItemDescription AS 物料描述,
	                                                UM AS 单位,
	                                                ReceiveQuantity AS 入库数量,
	                                                UnitPrice AS 采购单价
                                                FROM
	                                                PurchaseOrderInvoiceRecordByCMF
                                                WHERE
	                                                Status = 4 And  (FinanceUpdateDateTime >'"+dtpStart.Value.AddDays(-1).ToString("yyyy-mm-dd")+"' And FinanceUpdateDateTime <'"+dtpStart.Value.AddDays(1).ToString("yyyy-mm-dd")+"' ) order by PONumber,LineNumber  ASC";
            DataTable dt = SQLHelper.GetDataTable(GlobalSpace.FSDBConnstr, sqlSelect);
            dgvRecord.DataSource = dt;
        }

        private void btnViewErrorRecord_Click(object sender, EventArgs e)
        {
            string sqlSelect = @"Select Type AS 类型,ErrorContent AS 内容,OperateDateTime AS 日期 From FSErrorLogByCMF Where Operator='" + FSID + "' And Left(OperateDateTime,10)='" + dtpError.Value.ToString("yyyy-MM-dd") + "'  Order By OperateDateTime Desc";
            dgvRecord.DataSource = SQLHelper.GetDataTable(GlobalSpace.FSDBConnstr, sqlSelect);
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            string str = GB2312.GetString(ISO88591.GetBytes("  ′?·￠?±?′??DD×ü·?àà?ê1y?ê"));
            MessageBox.Show(str);
        }

        private void tbTaxCode_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar == (char)13)
            {
                if(!string.IsNullOrEmpty(tbTaxCode.Text))
                {
                    GetTaxRate(tbTaxCode.Text);
                    tbTaxAmount.Focus();
                }
                else
                {
                    MessageBoxEx.Show("当前税码不能为空！","提示");
                }
            }
        }

        private void tbInvoiceNumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar == (char)13)
            {
                if(!string.IsNullOrEmpty(tbInvoiceNumber.Text))
                {
                    if(!tbInvoiceNumber.Text.Contains("IA"))
                    {

                        if (tbInvoiceNumber.Text.Contains("，"))
                        {
                            MessageBoxEx.Show("请使用英文逗号！", "提示");
                            return;
                        }

                        if (!tbInvoiceNumber.Text.Contains(","))
                        {
                            dgvDetail.DataSource = GetInfo(1, tbInvoiceNumber.Text);
                            dgvDetail.Columns["Id"].Visible = false;
                            tbInvoiceNumber.Text = "IA" + tbInvoiceNumber.Text.Substring(4, 4);
                            tbTaxCode.Focus();
                        }
                        else
                        {
                            string[] invoiceArray = tbInvoiceNumber.Text.Split(',');
                            dgvDetail.DataSource = GetInfo(2, tbInvoiceNumber.Text,invoiceArray);
                            dgvDetail.Columns["Id"].Visible = false;              
                            tbTaxCode.Focus();
                        }
                    }
                }
                else
                {
                    MessageBoxEx.Show("当前发票号码不能为空！", "提示");
                }
            }
        }

        private void btnMakeAllChecked_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow dgvr in dgvDetail.Rows)
            {
                dgvr.Cells["Check"].Value = true;
            }
        }
    }
}
