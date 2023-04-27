using Global.Helper;
using SoftBrands.FourthShift.Transaction;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Global.Finance
{
    public partial class InvoiceVerifyMR : Form
    {
        string FSID = string.Empty;
        string UserName = string.Empty;
        string FSPassword = string.Empty;

        
        string VendorId = string.Empty;//供应商码
        string VendorName = string.Empty;//供应商名
        string UnvoucheredAccount = string.Empty;//无票
        string VoucheredAccount = string.Empty;//有票
        public InvoiceVerifyMR( string fsID, string fsPassword, string name)
        {
            InitializeComponent();
            UserName = name;
            FSID = fsID;
            FSPassword = fsPassword;
        }
        private void InvoiceVerifyMR_Load(object sender, EventArgs e)
        {
            tbYear.Text = DateTime.Now.ToString("yy");
            tbMonth.Text = DateTime.Now.ToString("MM");
            cbbTaxType.SelectedIndex = 0;
            tbTaxCode.Text = "0";
            GetTaxRate(tbTaxCode.Text);
            BtnAll_Click(null, null);
        }

        private void tbTaxCode_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                tbTaxCode.Text = tbTaxCode.Text.Trim();
                if (!string.IsNullOrEmpty(tbTaxCode.Text))
                {
                    GetTaxRate(tbTaxCode.Text);
                }
                else
                {
                    MessageBox.Show("当前税码不能为空！", "提示");
                }
            }
        }
        private void GetTaxRate(string taxCode)
        {
            tbVATRate.Text = string.Empty;
            tbTaxCode.Tag = string.Empty;//vat账号
            string sqlSelect = @"SELECT 
                             [TaxCode] 税金代码
                             ,[TaxRate] 税率
                             ,[TaxAccrualAccount] 应付税金账号
                           FROM [FS_TaxCode] where [TaxCode]='" + taxCode + "'";
            DataTable dt = SQLHelper.GetDataTable(GlobalSpace.FSDBMRConnstr, sqlSelect);

            if (dt.Rows.Count > 0)
            {
                tbVATRate.Text = dt.Rows[0]["税率"].ToString();
                tbVATRate.Tag = dt.Rows[0]["应付税金账号"].ToString();//vat账号
                tbTaxCode.Tag = taxCode;//税金代码
            }
            else
            {
                MessageBox.Show("未查到该税码对应的税率！", "提示");
            }
        }

        private void BtnAll_Click(object sender, EventArgs e)
        {
            string sqlSelect = @"SELECT
                                                    distinct VendorNumber 供应商码,  VendorName 供应商名, InvoiceNumberS 发票号
                                                FROM
	                                                PurchaseOrderInvoiceRecordMRByCMF where Status=2";
            DGV1.DataSource = SQLHelper.GetDataTable(GlobalSpace.FSDBConnstr, sqlSelect);
            for (int i = 0; i < DGV1.Columns.Count; i++)
            {
                DGV1.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
            }
        }

        private void DGV1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int RowIndex = e.RowIndex;
            if (RowIndex < 0) return;
            VendorId = DGV1["供应商码", RowIndex].Value.ToString();
            VendorName = DGV1["供应商名", RowIndex].Value.ToString();
            TbInvoiceNumberS.Text = DGV1["发票号", RowIndex].Value.ToString();

            string sqlSelect = $@"SELECT VendorNumber 供应商码, 
	VendorName 供应商名, 
	PONumber 采购单号, 
	LineNumber 行号, 
	SequenceNumber 序号, 
	ItemNumber 物料编码, 
	ItemDescription 物料描述, 
	UM 单位, 
	ReceiveQuantity 入库量, 
	UnitPrice 单价, 
	Amount 总价, 
	InvoiceNumberS 发票号, 
    AllAmount 入库总金额,
    InvoiceNumber 四班票号,
    InvoiceTaxedAmount 总税额,
    InvoiceAmount 不含税发票总额,
	InvoiceMatchedQuantity 已匹配数量, 
	ReceiveDate 入库日期, 
	ForeignNumber 联系单号,
	APReceiptLineKey 
                                                FROM
	                                                PurchaseOrderInvoiceRecordMRByCMF where VendorNumber ='{VendorId}' and InvoiceNumberS='{TbInvoiceNumberS.Text}'";
            DGV2.DataSource = SQLHelper.GetDataTable(GlobalSpace.FSDBConnstr, sqlSelect);
            TBstorageAmount.Text = DGV2["入库总金额", 0].Value.ToString();
            TbInvoiceNumber.Text = DGV2["四班票号", 0].Value.ToString();
            TbTax.Text = DGV2["总税额", 0].Value.ToString();
            TbInvoiceAmount.Text = DGV2["不含税发票总额", 0].Value.ToString();

            for (int i = 0; i < DGV2.Columns.Count; i++)
            {
                DGV2.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
            }
        }

        private void TbTax_TextChanged(object sender, EventArgs e)
        {
            GetInvoiceAllAmount();
        }

        private void TbInvoiceAmount_TextChanged(object sender, EventArgs e)
        {
            GetInvoiceAllAmount();
        }
        private void GetInvoiceAllAmount()
        {
            Decimal d1, d2 = 0;
            decimal.TryParse(TbTax.Text,out d1);
            decimal.TryParse(TbInvoiceAmount.Text, out d2);
            TbInvoiceAllAmount.Text = (d1 + d2).ToString();
        }

        private void BtnInvoiceVerify_Click(object sender, EventArgs e)
        {
            if (DGV2.Rows.Count == 0)
            {
                MessageBox.Show("无信息", "提示");
                return;
            }
            if (string.IsNullOrWhiteSpace(tbVATRate.Text))
            {
                MessageBox.Show("当前税码或税率为空！", "提示");
                return;
            }
            TbInvoiceNumber.Text = TbInvoiceNumber.Text.Trim();
            if (string.IsNullOrWhiteSpace(TbInvoiceNumber.Text))
            {
                MessageBox.Show("请输入四班票号！", "提示");
                return;
            }
            //税额 发票金额数据检查
            decimal Tax, InvoiceAmount, StorageAmount = 0;
            if (!decimal.TryParse(TbTax.Text, out Tax))
            {
                MessageBox.Show("总税额转换失败请检查！", "提示");
                return;
            }
            if (!decimal.TryParse(TbInvoiceAmount.Text, out InvoiceAmount))
            {
                MessageBox.Show("不含税发票总金额转换失败请检查！", "提示");
                return;
            }
            if (!decimal.TryParse(TBstorageAmount.Text, out StorageAmount))
            {
                MessageBox.Show("入库总金额转换失败请检查！", "提示");
                return;
            }
            //判断该供应商的发票是否已经存在
            string sqlExist = $@"Select Count(VendorID) From FS_APInvoiceHeader Where InvoiceNumber = '{ TbInvoiceNumber.Text }'  AND VendorID ='{ VendorId }'";
            if (SQLHelper.Exist(GlobalSpace.FSDBMRConnstr, sqlExist))
            {
                MessageBox.Show("该供应商的四班票号码已存在！", "提示");
                return;
            }
            string sqlSelectVendor = @"SELECT	                              
	                                 UnvoucheredAccount AS 无票账号
	                                 ,[VoucheredAccount] AS 有票账号 
                                   FROM  [_NoLock_FS_Vendor] 
                                   WHERE
	                               [VendorID] = '" + VendorId + "' AND [VendorStatus] = 'A'";

            DataTable dt = SQLHelper.GetDataTable(GlobalSpace.FSDBMRConnstr, sqlSelectVendor);
            
            if (dt.Rows.Count == 0)
            {
                MessageBox.Show("未查到该供应商信息或供应商已停用！", "提示");
                return;
            }
            else
            {
                UnvoucheredAccount = dt.Rows[0]["无票账号"].ToString();//无票
                VoucheredAccount = dt.Rows[0]["有票账号"].ToString();//有票
            }
            string sqlUpdate0 = $@"UPDATE PurchaseOrderInvoiceRecordMRByCMF SET  InvoiceNumber='{TbInvoiceNumber.Text.Trim()}',InvoiceTaxedAmount='{TbTax.Text.Trim()}',InvoiceAmount='{TbInvoiceAmount.Text.Trim()}',OperateFinance='{FSID}' WHERE VendorNumber ='{VendorId}' and  InvoiceNumberS='{TbInvoiceNumberS.Text}'";


            SQLHelper.ExecuteNonQuery(GlobalSpace.FSDBConnstr, sqlUpdate0);
            
            if (!FSFunctionLib.FSConfigFileInitialize(GlobalSpace.fsconfigfilepath, FSID, FSPassword))
            {
                MessageBox.Show("四班登录失败！", "提示");
                return;
            }
            
            //增加发票信息
            if (AddAPInvoiceInfo(TbInvoiceNumber.Text.Trim(), VendorId, StorageAmount, InvoiceAmount, cbbTaxType.Text, tbTaxCode.Tag.ToString(), Tax, tbYear.Text, tbMonth.Text))
            {


                for (int j = 0; j < DGV2.Rows.Count; j++)
                {
                    //将发票核销到PO
                    APID03 myAPID03 = new APID03();

                    myAPID03.VendorID.Value = VendorId;
                    myAPID03.InvoiceNumber.Value = TbInvoiceNumber.Text;
                    myAPID03.PONumber.Value = DGV2.Rows[j].Cells["采购单号"].Value.ToString().Trim();
                    myAPID03.POReceiptSequenceNumber.Value = DGV2.Rows[j].Cells["序号"].Value.ToString();
                    myAPID03.PONumberReceiptSequenceNumber.Value = DGV2.Rows[j].Cells["采购单号"].Value.ToString() + "-" + DGV2.Rows[j].Cells["序号"].Value.ToString();
                    myAPID03.LineItemNumber.Value = DGV2.Rows[j].Cells["行号"].Value.ToString();
                    myAPID03.ItemAccountMoCo.Value = DGV2.Rows[j].Cells["物料编码"].Value.ToString();
                    if (DGV2.Rows[j].Cells["入库量"].Value.ToString().Contains("-"))
                    {
                        myAPID03.POReceiptQuantity.Value = DGV2.Rows[j].Cells["入库量"].Value.ToString().Replace("-", "") + "-";
                        myAPID03.InvoiceQuantity.Value = DGV2.Rows[j].Cells["入库量"].Value.ToString().Replace("-", "") + "-";
                    }
                    else
                    {
                        myAPID03.POReceiptQuantity.Value = DGV2.Rows[j].Cells["入库量"].Value.ToString();
                        myAPID03.InvoiceQuantity.Value = DGV2.Rows[j].Cells["入库量"].Value.ToString();
                    }

                    myAPID03.InvoiceMatchedQuantity.Value = DGV2.Rows[j].Cells["已匹配数量"].Value.ToString();

                    myAPID03.LineItemUM.Value = DGV2.Rows[j].Cells["单位"].Value.ToString();
                    //myAPID03.InvoiceControllingUnitCost.Value = (Math.Round(leftAmount / Convert.ToDecimal(dt.Rows[0]["入库数量"]), 8)).ToString().Replace("-", "");//dt.Rows[0]["匹配单价"].ToString();
                    decimal lastAmount = 0;
                    if (j == DGV2.Rows.Count - 1)
                    {
                        lastAmount = Convert.ToDecimal(DGV2.Rows[j].Cells["总价"].Value.ToString()) - StorageAmount + InvoiceAmount;
                    }
                    else
                    {
                        lastAmount = Convert.ToDecimal(DGV2.Rows[j].Cells["总价"].Value.ToString());
                    }

                    if (lastAmount.ToString().Contains("-"))
                    {
                        myAPID03.InvoiceControllingExtendedCost.Value = lastAmount.ToString().Replace("-", "") + "-";
                    }
                    else
                    {
                        myAPID03.InvoiceControllingExtendedCost.Value = lastAmount.ToString();
                    }
                    try
                    {
                        if (!FSFunctionLib.fstiClient.ProcessId(myAPID03, null))
                        {
                            FSTIError error = FSFunctionLib.fstiClient.TransactionError;
                            //  CommonOperate.WriteFSErrorLog("APID03", myAPID03, error, FSID, myAPID03.VendorID.Value + " " + myAPID03.InvoiceNumber.Value + " " + myAPID03.PONumber.Value+" "+ myAPID03.LineItemNumber.Value+" 入库数量："+ myAPID03.POReceiptQuantity.Value);
                            CommonOperate.WriteFSErrorLog("APID03", myAPID03, error, FSID);
                            //       MessageBox.Show("将发票核销到采购订单APID03失败！", "提示");

                            //            string TXT = (GB2312.GetString(ISO88591.GetBytes(error.Description)));
                            MessageBox.Show("APID03: " + error.Description);
                            //                 MessageBox.Show("APID10_1 " + TXT);
                            return;
                        }

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("异常：将发票核销到采购订单APID03失败！" + ex.Message, "提示");
                        return;
                    }



                }
                

                //过账
                if (PostAccount(Tax, StorageAmount, InvoiceAmount, VendorId, TbInvoiceNumber.Text.Trim()))
                {
                    string sqlUpdate = $@"UPDATE PurchaseOrderInvoiceRecordMRByCMF SET Status = 3,FinanceUpdateDateTime= getdate(),InvoiceNumber='{TbInvoiceNumber.Text.Trim()}',InvoiceTaxedAmount={TbTax.Text.Trim()},InvoiceAmount={TbInvoiceAmount.Text.Trim()},OperateFinance='{FSID}' WHERE VendorNumber ='{VendorId}' and  InvoiceNumberS='{TbInvoiceNumberS.Text}'";


                    if (SQLHelper.ExecuteNonQuery(GlobalSpace.FSDBConnstr, sqlUpdate))
                    {
                        
                        MessageBox.Show("过账完成！", "提示");
                    }
                    else
                    {
                        MessageBox.Show("四班过账完成，更新发票记录状态失败！", "提示");
                    }
                    DGV1.DataSource = null;
                    DGV2.DataSource = null;
                    VendorId = string.Empty;
                    VendorName = string.Empty;
                    TbInvoiceNumberS.Text = string.Empty;
                    TBstorageAmount.Text = string.Empty;
                    TbInvoiceNumber.Text = string.Empty;
                    TbTax.Text = string.Empty;
                    TbInvoiceAmount.Text = string.Empty;
                }
                else
                {
                    MessageBox.Show("过账失败！", "提示");
                }
            }
            else
            {
                MessageBox.Show("核销失败！", "提示");
            }
            FSFunctionLib.FSExit();
            
        }


        //过账处理
        private bool PostAccount(decimal amountTax, decimal amountChecked, decimal amountInvoiced, string vendorNumber, string invoiceNumber)
        {
            Decimal s = amountInvoiced - amountChecked;
            APID10 myAPID10 = new APID10();
            APID12 myAPID12 = new APID12();
            if (s > 0)//发票金额>订单金额  差异在借方
            {
                if (APID10(myAPID10, vendorNumber, invoiceNumber, "1000-00-000-123300", "0", s.ToString()))
                {

                }
                else
                {
                    return false;
                }
            }
            else if (s < 0)//发票金额<订单金额 差异在贷方
            {
                s = (-1) * s;
                if (APID10(myAPID10, vendorNumber, invoiceNumber, "1000-00-000-123300", s.ToString(), "0"))
                {

                }
                else
                {
                    return false;
                }
            }
            if (APID10(myAPID10, vendorNumber, invoiceNumber, UnvoucheredAccount, "0", amountChecked.ToString()))
            {
                if (APID10(myAPID10, vendorNumber, invoiceNumber, tbVATRate.Tag.ToString(), "0", amountTax.ToString()))
                {
                    if (APID10(myAPID10, vendorNumber, invoiceNumber, VoucheredAccount, (amountInvoiced + amountTax).ToString(), "0"))
                    {
                        if (APID12(myAPID12, vendorNumber, invoiceNumber))
                        {
                            return true;
                        }
                    }
                }
            }

            return false;
        }
        public bool APID12(APID12 myAPID12, string VendorID, string InvoiceNumber)
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
                MessageBox.Show("APID12: " + error.Description);
                //             MessageBoxEx.Show("APID12_1 " + TXT);
                CommonOperate.WriteFSErrorLog("APID12", myAPID12, error, FSID);
            }
            return false;
        }
        public bool APID10(APID10 myAPID10, string VendorID, string InvoiceNumber, string TransactionAccount, string TransactionAmountCredit, string TransactionAmountDebit)
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
                MessageBox.Show("APID10: " + error.Description);
                //         MessageBoxEx.Show("APID10_1 "+ TXT);
                CommonOperate.WriteFSErrorLog("APID10", myAPID10, error, FSID);
            }
            return false;
        }
        //增加应付账款发票信息
        private bool AddAPInvoiceInfo(string invoiceNumber, string vendorID, decimal vatBaseAmount, decimal poReceiptAoumt, string invoiceType, string taxCode, decimal taxAmount, string year, string period)
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
            myAPID00.VATControllingAmount.Value = taxAmount.ToString(); // tbTaxAmount.Text;
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
                    CommonOperate.WriteFSErrorLog("APID00", myAPID00, error, FSID);
                    MessageBox.Show("增加应付发票APID00失败！" + error.Description, "提示");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("异常：增加应付发票APID00失败!" + ex.Message, "提示");
            }
            return false;
        }

        private void BtnReturn_Click(object sender, EventArgs e)
        {
            if (DGV2.Rows.Count == 0) { MessageBox.Show("无信息"); return; }
            string sqlUpdate = $@"UPDATE PurchaseOrderInvoiceRecordMRByCMF SET Status = 0,FinanceUpdateDateTime= getdate(),OperateFinance='{FSID}',Remarks='财务退回' WHERE VendorNumber ='{VendorId}' and  InvoiceNumberS='{TbInvoiceNumberS.Text}'";


            if (SQLHelper.ExecuteNonQuery(GlobalSpace.FSDBConnstr, sqlUpdate))
            {

                MessageBox.Show("退回完成！", "提示");
                DGV1.DataSource = null;
                DGV2.DataSource = null;
                VendorId = string.Empty;
                VendorName = string.Empty;
                TbInvoiceNumberS.Text = string.Empty;
                TBstorageAmount.Text = string.Empty;
                TbInvoiceNumber.Text = string.Empty;
                TbTax.Text = string.Empty;
                TbInvoiceAmount.Text = string.Empty;
            }
            else
            {
                MessageBox.Show("退回失败！", "提示");
            }
           
        }

        private void BtnHistory_Click(object sender, EventArgs e)
        {
            Purchase.PoInvoiceSelect_MR PS = new Purchase.PoInvoiceSelect_MR(FSID, UserName, "财务");
            PS.ShowDialog();
        }

        private void BtnError_Click(object sender, EventArgs e)
        {
            FSerror fSerror = new FSerror(FSID);
            fSerror.ShowDialog();
        }

        private void TbInvoiceSelect_KeyDown(object sender, KeyEventArgs e)
        {
            string sqlSelect = $@"SELECT
                                                    distinct VendorNumber 供应商码,  VendorName 供应商名, InvoiceNumberS 发票号
                                                FROM
	                                                PurchaseOrderInvoiceRecordMRByCMF where Status=2 and InvoiceNumberS like '%{TbInvoiceSelect.Text.Trim()}%'";
            DGV1.DataSource = SQLHelper.GetDataTable(GlobalSpace.FSDBConnstr, sqlSelect);
            for (int i = 0; i < DGV1.Columns.Count; i++)
            {
                DGV1.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (DGV2.Rows.Count == 0)
            {
                MessageBox.Show("无信息", "提示");
                return;
            }
            //if (string.IsNullOrWhiteSpace(tbVATRate.Text))
            //{
            //    MessageBox.Show("当前税码或税率为空！", "提示");
            //    return;
            //}
            TbInvoiceNumber.Text = TbInvoiceNumber.Text.Trim();
            if (string.IsNullOrWhiteSpace(TbInvoiceNumber.Text))
            {
                MessageBox.Show("请输入四班票号！", "提示");
                return;
            }
            //税额 发票金额数据检查
            decimal Tax, InvoiceAmount, StorageAmount = 0;
            if (!decimal.TryParse(TbTax.Text, out Tax))
            {
                MessageBox.Show("总税额转换失败请检查！", "提示");
                return;
            }
            if (!decimal.TryParse(TbInvoiceAmount.Text, out InvoiceAmount))
            {
                MessageBox.Show("不含税发票总金额转换失败请检查！", "提示");
                return;
            }
            if (!decimal.TryParse(TBstorageAmount.Text, out StorageAmount))
            {
                MessageBox.Show("入库总金额转换失败请检查！", "提示");
                return;
            }
            //判断该供应商的发票是否已经存在
            string sqlExist = $@"Select Count(VendorID) From FS_APInvoiceHeader Where InvoiceNumber = '{ TbInvoiceNumber.Text }'  AND VendorID ='{ VendorId }'";
            if (SQLHelper.Exist(GlobalSpace.FSDBMRConnstr, sqlExist))
            {
                MessageBox.Show("该供应商的四班票号码已存在！", "提示");
                return;
            }
            string sqlSelectVendor = @"SELECT	                              
	                                 UnvoucheredAccount AS 无票账号
	                                 ,[VoucheredAccount] AS 有票账号 
                                   FROM  [_NoLock_FS_Vendor] 
                                   WHERE
	                               [VendorID] = '" + VendorId + "' AND [VendorStatus] = 'A'";

            DataTable dt = SQLHelper.GetDataTable(GlobalSpace.FSDBMRConnstr, sqlSelectVendor);

            if (dt.Rows.Count == 0)
            {
                MessageBox.Show("未查到该供应商信息或供应商已停用！", "提示");
                return;
            }
            else
            {
                UnvoucheredAccount = dt.Rows[0]["无票账号"].ToString();//无票
                VoucheredAccount = dt.Rows[0]["有票账号"].ToString();//有票
            }
            string sqlUpdate0 = $@"UPDATE PurchaseOrderInvoiceRecordMRByCMF SET  InvoiceNumber='{TbInvoiceNumber.Text.Trim()}',InvoiceTaxedAmount='{TbTax.Text.Trim()}',InvoiceAmount='{TbInvoiceAmount.Text.Trim()}',OperateFinance='{FSID}' WHERE VendorNumber ='{VendorId}' and  InvoiceNumberS='{TbInvoiceNumberS.Text}'";


            SQLHelper.ExecuteNonQuery(GlobalSpace.FSDBConnstr, sqlUpdate0);
            string sqlUpdate = $@"UPDATE PurchaseOrderInvoiceRecordMRByCMF SET Status = 3,FinanceUpdateDateTime= getdate(),InvoiceNumber='{TbInvoiceNumber.Text.Trim()}',InvoiceTaxedAmount={TbTax.Text.Trim()},InvoiceAmount={TbInvoiceAmount.Text.Trim()},OperateFinance='{FSID}' WHERE VendorNumber ='{VendorId}' and  InvoiceNumberS='{TbInvoiceNumberS.Text}'";


            if (SQLHelper.ExecuteNonQuery(GlobalSpace.FSDBConnstr, sqlUpdate))
            {

                MessageBox.Show("更新发票记录状态完成！", "提示");
                DGV1.DataSource = null;
                DGV2.DataSource = null;
                VendorId = string.Empty;
                VendorName = string.Empty;
                TbInvoiceNumberS.Text = string.Empty;
                TBstorageAmount.Text = string.Empty;
                TbInvoiceNumber.Text = string.Empty;
                TbTax.Text = string.Empty;
                TbInvoiceAmount.Text = string.Empty;
            }
            else
            {
                MessageBox.Show("更新发票记录状态失败！", "提示");
            }
            
            /*
            if (!FSFunctionLib.FSConfigFileInitialize(GlobalSpace.Testfsconfigfilepath, FSID, FSPassword))
            {
                MessageBox.Show("四班登录失败！", "提示");
                return;
            }

            //增加发票信息
            if (AddAPInvoiceInfo(TbInvoiceNumber.Text.Trim(), VendorId, StorageAmount, InvoiceAmount, cbbTaxType.Text, tbTaxCode.Tag.ToString(), Tax, tbYear.Text, tbMonth.Text))
            {


                for (int j = 0; j < BtnManualVerify.Rows.Count; j++)
                {
                    //将发票核销到PO
                    APID03 myAPID03 = new APID03();

                    myAPID03.VendorID.Value = VendorId;
                    myAPID03.InvoiceNumber.Value = TbInvoiceNumber.Text;
                    myAPID03.PONumber.Value = BtnManualVerify.Rows[j].Cells["采购单号"].Value.ToString().Trim();
                    myAPID03.POReceiptSequenceNumber.Value = BtnManualVerify.Rows[j].Cells["序号"].Value.ToString();
                    myAPID03.PONumberReceiptSequenceNumber.Value = BtnManualVerify.Rows[j].Cells["采购单号"].Value.ToString() + "-" + BtnManualVerify.Rows[j].Cells["序号"].Value.ToString();
                    myAPID03.LineItemNumber.Value = BtnManualVerify.Rows[j].Cells["行号"].Value.ToString();
                    myAPID03.ItemAccountMoCo.Value = BtnManualVerify.Rows[j].Cells["物料编码"].Value.ToString();
                    if (BtnManualVerify.Rows[j].Cells["入库量"].Value.ToString().Contains("-"))
                    {
                        myAPID03.POReceiptQuantity.Value = BtnManualVerify.Rows[j].Cells["入库量"].Value.ToString().Replace("-", "") + "-";
                        myAPID03.InvoiceQuantity.Value = BtnManualVerify.Rows[j].Cells["入库量"].Value.ToString().Replace("-", "") + "-";
                    }
                    else
                    {
                        myAPID03.POReceiptQuantity.Value = BtnManualVerify.Rows[j].Cells["入库量"].Value.ToString();
                        myAPID03.InvoiceQuantity.Value = BtnManualVerify.Rows[j].Cells["入库量"].Value.ToString();
                    }

                    myAPID03.InvoiceMatchedQuantity.Value = BtnManualVerify.Rows[j].Cells["已匹配数量"].Value.ToString();

                    myAPID03.LineItemUM.Value = BtnManualVerify.Rows[j].Cells["单位"].Value.ToString();
                    //myAPID03.InvoiceControllingUnitCost.Value = (Math.Round(leftAmount / Convert.ToDecimal(dt.Rows[0]["入库数量"]), 8)).ToString().Replace("-", "");//dt.Rows[0]["匹配单价"].ToString();
                    decimal lastAmount = 0;
                    if (j == BtnManualVerify.Rows.Count - 1)
                    {
                        lastAmount = Convert.ToDecimal(BtnManualVerify.Rows[j].Cells["总价"].Value.ToString()) - StorageAmount + InvoiceAmount;
                    }
                    else
                    {
                        lastAmount = Convert.ToDecimal(BtnManualVerify.Rows[j].Cells["总价"].Value.ToString());
                    }

                    if (lastAmount.ToString().Contains("-"))
                    {
                        myAPID03.InvoiceControllingExtendedCost.Value = lastAmount.ToString().Replace("-", "") + "-";
                    }
                    else
                    {
                        myAPID03.InvoiceControllingExtendedCost.Value = lastAmount.ToString();
                    }
                    try
                    {
                        if (!FSFunctionLib.fstiClient.ProcessId(myAPID03, null))
                        {
                            FSTIError error = FSFunctionLib.fstiClient.TransactionError;
                            //  CommonOperate.WriteFSErrorLog("APID03", myAPID03, error, FSID, myAPID03.VendorID.Value + " " + myAPID03.InvoiceNumber.Value + " " + myAPID03.PONumber.Value+" "+ myAPID03.LineItemNumber.Value+" 入库数量："+ myAPID03.POReceiptQuantity.Value);
                            CommonOperate.WriteFSErrorLog("APID03", myAPID03, error, FSID);
                            //       MessageBox.Show("将发票核销到采购订单APID03失败！", "提示");

                            //            string TXT = (GB2312.GetString(ISO88591.GetBytes(error.Description)));
                            MessageBox.Show("APID03: " + error.Description);
                            //                 MessageBox.Show("APID10_1 " + TXT);
                            return;
                        }

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("异常：将发票核销到采购订单APID03失败！" + ex.Message, "提示");
                        return;
                    }



                }


                //过账
                if (PostAccount(Tax, StorageAmount, InvoiceAmount, VendorId, TbInvoiceNumber.Text.Trim()))
                {
                    string sqlUpdate = $@"UPDATE PurchaseOrderInvoiceRecordMRByCMF SET Status = 3,FinanceUpdateDateTime= getdate(),InvoiceNumber='{TbInvoiceNumber.Text.Trim()}',InvoiceTaxedAmount={TbTax.Text.Trim()},InvoiceAmount={TbInvoiceAmount.Text.Trim()},OperateFinance='{FSID}' WHERE VendorNumber ='{VendorId}' and  InvoiceNumberS='{TbInvoiceNumberS.Text}'";


                    if (SQLHelper.ExecuteNonQuery(GlobalSpace.FSDBConnstr, sqlUpdate))
                    {

                        MessageBox.Show("过账完成！", "提示");
                    }
                    else
                    {
                        MessageBox.Show("四班过账完成，更新发票记录状态失败！", "提示");
                    }
                    DGV1.DataSource = null;
                    BtnManualVerify.DataSource = null;
                    VendorId = string.Empty;
                    VendorName = string.Empty;
                    TbInvoiceNumberS.Text = string.Empty;
                    TBstorageAmount.Text = string.Empty;
                    TbInvoiceNumber.Text = string.Empty;
                    TbTax.Text = string.Empty;
                    TbInvoiceAmount.Text = string.Empty;
                }
                else
                {
                    MessageBox.Show("过账失败！", "提示");
                }
            }
            else
            {
                MessageBox.Show("核销失败！", "提示");
            }
            FSFunctionLib.FSExit();
            */
        }
    }
}
