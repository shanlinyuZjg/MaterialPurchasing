using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using System.Data.OleDb;
using System.IO;
using SoftBrands.FourthShift.Transaction;
using Global.Helper;

namespace Global.Finance
{
    public partial class ForeignCurrencyARCD : Office2007Form
    {
        string userID = string.Empty;
        string userPassword = string.Empty;

        string fileExtension = string.Empty;
        string xlsConn = string.Empty;
        string xlsxConn = string.Empty;
        string sheetname = string.Empty;

        public ForeignCurrencyARCD(string id,string password)
        {
            InitializeComponent();
            this.EnableGlass = false;
            userID = id;
            userPassword = password;
        }

        private void btnChooseFile_Click(object sender, EventArgs e)
        {
            /*
            string strConn = string.Empty;
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "EXCEL文件(*.xls,*.xlsx)|*.xls;*.xlsx|所有文件(*.*)|*.*";//用引号隔开多种格式
            ofd.FileName = "";
            ofd.FilterIndex = 1;//选中一个文件
            FileInfo file;
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                tbFilePath.Text = ofd.FileName.ToString();
                file = new FileInfo(tbFilePath.Text);
                fileExtension = file.Extension.ToUpper();
                switch (fileExtension)
                {
                    case ".XLS":
                        strConn = @"provider=Microsoft.Jet.OLEDB.4.0; data source=" + tbFilePath.Text.ToString().Trim() + "; extended properties='Excel 8.0; HDR=Yes; IMEX=1'";
                        xlsConn = strConn;
                        break;
                    case ".XLSX":
                        strConn = @"provider=Microsoft.ACE.OLEDB.12.0; data source=" + tbFilePath.Text.ToString().Trim() + "; extended properties='Excel 8.0; HDR=Yes; IMEX=1'";
                        xlsxConn = strConn;
                        break;
                    default:
                        strConn = @"provider=Microsoft.Jet.OLEDB.4.0; data source=" + tbFilePath.Text.ToString().Trim() + "; extended properties='Excel 8.0; HDR=Yes; IMEX=1'";
                        break;
                }
                OleDbConnection oleConn = new OleDbConnection(strConn);
                dgvARCD.Columns.Clear();
                #region 获取表名并保存到comboBox1内
                try
                {
                    oleConn.Open();
                    System.Data.DataTable dtSheetName = oleConn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                    string[] strTableNames = new string[dtSheetName.Rows.Count];
                    cbbSheet.Items.Clear();
                    for (int k = 0; k < dtSheetName.Rows.Count; k++)
                    {
                        cbbSheet.Items.Add(dtSheetName.Rows[k][2].ToString());
                    }
                    oleConn.Close();
                    oleConn.Dispose();
                    cbbSheet.SelectedIndex = 0;
                }
                catch (Exception error)
                {
                    MessageBox.Show(error.Message, "错误提示！");
                }
                finally
                {
                    oleConn.Close();
                    oleConn.Dispose();
                }
                sheetname = cbbSheet.SelectedItem.ToString().Trim();
                #endregion
            }*/
            CommonOperate.GetExcelFileInfo(tbFilePath, cbbSheet);
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            ARCD00 arcd00 = new ARCD00();
    //        arcd00.
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {

            FSFunctionLib.FSConfigFileInitialize("Y:\\Mfgsys\\fs.cfg", "F26", "11111");

            ARCD00 arcd00 = new ARCD00();
            ARCD01 arcd01 = new ARCD01();
            ARCD04 arcd04 = new ARCD04();
            ARCD02 arcd02 = new ARCD02();

            DataTable dt = (DataTable)dgvARCD.DataSource;

            for (int i = 0; i < 3/*dt.Rows.Count*/; i++)
            {
                /*
                arcd00.BankID.Value = "31";
                arcd00.CashSetForm.Value = "M";
                try
                {
                    if (FSFunctionLib.fstiClient.ProcessId(arcd00, null))
                    {
                        arcd01.CashSetForm.Value = "M";
                        arcd01.BankID.Value = "31";
                        arcd01.CashSetNumber.Value = arcd00.CashSetNumber.Value;

                        if (FSFunctionLib.fstiClient.ProcessId(arcd01, null))
                        {
                            arcd04.CashSetForm.Value = "M";
                            arcd04.CashSetNumber.Value = arcd00.CashSetNumber.Value;
                            arcd04.BankID.Value = "31";
                            arcd04.CustomerID.Value = dt.Rows[i]["客户代码"].ToString();
                            arcd04.CashReceiptForm.Value = "M";
                            arcd04.CashReceiptReferenceNumber.Value = dt.Rows[i]["回款参考号"].ToString();
                            arcd04.BankAmount.Value = dt.Rows[i]["合同金额"].ToString();
                            arcd04.BankCurrencyCode.Value = "USD";

                            if (FSFunctionLib.fstiClient.ProcessId(arcd04, null))
                            {
                                arcd02.CashSetForm.Value = "M";
                                arcd02.CashSetNumber.Value = arcd00.CashSetNumber.Value;
                                arcd02.BankChargeAmount.Value = dt.Rows[i]["手续费"].ToString();
                                //arcd02.BankCommissionAmount.Value = tbCharge2.Text.Trim();
                                if (FSFunctionLib.fstiClient.ProcessId(arcd02, null))
                                {
                                    MessageBoxEx.Show("成功啦！", "提示");
                                }
                                else
                                {
                                    FSTIError error = FSFunctionLib.fstiClient.TransactionError;
                                    FSFunctionLib.FSErrorMsg("四班异常");
                                    string sqlInsert2 = @"Insert Into PurchaseDepartmentFSErrorLogByCMF (FSErrorDescription) values ('" + error.Description + "')";
                                    if (SQLHelper.ExecuteNonQuery(GlobalSpace.FSDBConnstr, sqlInsert2) > 0)
                                    {

                                    }
                                    MessageBoxEx.Show("四班异常：" + error.Description, "提示");

                                }
                            }
                            else
                            {
                                FSTIError error = FSFunctionLib.fstiClient.TransactionError;
                                FSFunctionLib.FSErrorMsg("四班异常");
                                string sqlInsert2 = @"Insert Into PurchaseDepartmentFSErrorLogByCMF (FSErrorDescription) values ('" + error.Description + "')";
                                if (SQLHelper.ExecuteNonQuery(GlobalSpace.FSDBConnstr, sqlInsert2) > 0)
                                {

                                }
                                MessageBoxEx.Show("四班异常：" + error.Description, "提示");

                            }
                        }
                        else
                        {
                            FSTIError error = FSFunctionLib.fstiClient.TransactionError;
                            FSFunctionLib.FSErrorMsg("四班异常");
                            string sqlInsert2 = @"Insert Into PurchaseDepartmentFSErrorLogByCMF (FSErrorDescription) values ('" + error.Description + "')";
                            if (SQLHelper.ExecuteNonQuery(GlobalSpace.FSDBConnstr, sqlInsert2) > 0)
                            {

                            }
                            MessageBoxEx.Show("四班异常：" + error.Description, "提示");

                        }
                    }
                    else
                    {
                        FSTIError error = FSFunctionLib.fstiClient.TransactionError;
                        FSFunctionLib.FSErrorMsg("四班异常");
                        string sqlInsert2 = @"Insert Into PurchaseDepartmentFSErrorLogByCMF (FSErrorDescription) values ('" + error.Description + "')";
                        if (SQLHelper.ExecuteNonQuery(GlobalSpace.FSDBConnstr, sqlInsert2) > 0)
                        {

                        }
                        MessageBoxEx.Show("四班异常：" + error.Description, "提示");

                    }
                }
                catch (Exception ex)
                {
                    MessageBoxEx.Show("出现异常：" + ex.Message);
                }*/

                tbCustID.Text = dt.Rows[i]["客户代码"].ToString();
                tbCurrencyReference.Text = dt.Rows[i]["回款参考号"].ToString();
                tbContractAmount.Text = dt.Rows[i]["合同金额"].ToString();
                tbCharge.Text = dt.Rows[i]["手续费"].ToString();
                ARCD();
                /*  }
                  */

                FSFunctionLib.FSExit();
            }
        }
        private void  ARCD()
        {
            FSFunctionLib.FSConfigFileInitialize("Y:\\Mfgsys\\fs.cfg", "F26", "11111");

            ARCD00 arcd00 = new ARCD00();
            ARCD01 arcd01 = new ARCD01();
            ARCD04 arcd04 = new ARCD04();
            ARCD02 arcd02 = new ARCD02();

            arcd00.BankID.Value = "31";
            arcd00.CashSetForm.Value = "M";

            try
            {
                if (FSFunctionLib.fstiClient.ProcessId(arcd00, null))
                {
                    arcd01.CashSetForm.Value = "M";
                    arcd01.BankID.Value = "31";
                    arcd01.CashSetNumber.Value = arcd00.CashSetNumber.Value;

                    if (FSFunctionLib.fstiClient.ProcessId(arcd01, null))
                    {
                        arcd04.CashSetForm.Value = "M";
                        arcd04.CashSetNumber.Value = arcd00.CashSetNumber.Value;
                        arcd04.BankID.Value = "31";
                        arcd04.CustomerID.Value = tbCustID.Text.Trim();
                        arcd04.CashReceiptForm.Value = "M";
                        arcd04.CashReceiptReferenceNumber.Value = tbCurrencyReference.Text.Trim();
                        arcd04.BankAmount.Value = tbContractAmount.Text.Trim();
                        arcd04.BankCurrencyCode.Value = "USD";

                        if (FSFunctionLib.fstiClient.ProcessId(arcd04, null))
                        {
                            arcd02.CashSetForm.Value = "M";
                            arcd02.CashSetNumber.Value = arcd00.CashSetNumber.Value;
                            arcd02.BankChargeAmount.Value = tbCharge.Text.Trim();
                            arcd02.BankCommissionAmount.Value = tbCharge2.Text.Trim();
                            if (FSFunctionLib.fstiClient.ProcessId(arcd02, null))
                            {
                                MessageBoxEx.Show("成功啦！", "提示");
                            }
                            else
                            {
                                FSTIError error = FSFunctionLib.fstiClient.TransactionError;
                                FSFunctionLib.FSErrorMsg("四班异常");
                                string sqlInsert2 = @"Insert Into PurchaseDepartmentFSErrorLogByCMF (FSErrorDescription) values ('" + error.Description + "')";
                                if (SQLHelper.ExecuteNonQuery(GlobalSpace.FSDBConnstr, sqlInsert2))
                                {

                                }
                                MessageBoxEx.Show("四班异常：" + error.Description, "提示");

                            }
                        }
                        else
                        {
                            FSTIError error = FSFunctionLib.fstiClient.TransactionError;
                            FSFunctionLib.FSErrorMsg("四班异常");
                            string sqlInsert2 = @"Insert Into PurchaseDepartmentFSErrorLogByCMF (FSErrorDescription) values ('" + error.Description + "')";
                            if (SQLHelper.ExecuteNonQuery(GlobalSpace.FSDBConnstr, sqlInsert2) )
                            {

                            }
                            MessageBoxEx.Show("四班异常：" + error.Description, "提示");

                        }
                    }
                    else
                    {
                        FSTIError error = FSFunctionLib.fstiClient.TransactionError;
                        FSFunctionLib.FSErrorMsg("四班异常");
                        string sqlInsert2 = @"Insert Into PurchaseDepartmentFSErrorLogByCMF (FSErrorDescription) values ('" + error.Description + "')";
                        if (SQLHelper.ExecuteNonQuery(GlobalSpace.FSDBConnstr, sqlInsert2) )
                        {

                        }
                        MessageBoxEx.Show("四班异常：" + error.Description, "提示");

                    }
                }
                else
                {
                    FSTIError error = FSFunctionLib.fstiClient.TransactionError;
                    FSFunctionLib.FSErrorMsg("四班异常");
                    string sqlInsert2 = @"Insert Into PurchaseDepartmentFSErrorLogByCMF (FSErrorDescription) values ('" + error.Description + "')";
                    if (SQLHelper.ExecuteNonQuery(GlobalSpace.FSDBConnstr, sqlInsert2) )
                    {

                    }
                    MessageBoxEx.Show("四班异常：" + error.Description, "提示");

                }
            }
            catch (Exception ex)
            {
                MessageBoxEx.Show("出现异常：" + ex.Message);
            }


            FSFunctionLib.FSExit();
        }
        private void btnLastStepTest_Click(object sender, EventArgs e)
        {
            FSFunctionLib.FSConfigFileInitialize("Y:\\Mfgsys\\fs.cfg", "CMF", "reyoung");

            ARCD02 arcd02 = new ARCD02();

            arcd02.CashSetForm.Value = "M";
            arcd02.CashSetNumber.Value = "00088293";
            arcd02.BankChargeAmount.Value = "1";
            arcd02.BankCommissionAmount.Value = "2";

            if (FSFunctionLib.fstiClient.ProcessId(arcd02, null))
            {
                MessageBoxEx.Show("成功啦！", "提示");
            }
            else
            {
                FSTIError error = FSFunctionLib.fstiClient.TransactionError;
                FSFunctionLib.FSErrorMsg("四班异常");
                string sqlInsert2 = @"Insert Into PurchaseDepartmentFSErrorLogByCMF (FSErrorDescription) values ('" + error.Description + "')";
                if (SQLHelper.ExecuteNonQuery(GlobalSpace.FSDBConnstr, sqlInsert2) )
                {

                }
                MessageBoxEx.Show("四班异常：" + error.Description, "提示");
            }
        }

        private void btnWriteToFS_Click(object sender, EventArgs e)
        {
            if(dgvARCD.Rows.Count > 0)
            {

            }
            else
            {
                MessageBoxEx.Show("提示", "记录不能为空！");
            }
        }

        private void buttonX2_Click(object sender, EventArgs e)
        {
            if (dgvARCD.Rows.Count > 0)
            {
                dgvARCD.Rows.Clear();
            }
            string sqlSelect = @"SELECT
	                                                CustomerNumber AS 客户代码,
	                                                ReceiptReference AS 回款参考号,
	                                                ContactAmount AS 合同金额,
	                                                BankChargeAmount AS 手续费,
	                                                Bank AS 银行,
	                                                Currency AS 外币
                                                FROM
	                                                FinanceForeignCurrencyARCDByCMF
                                                WHERE
	                                                Status = 0";
            dgvARCD.DataSource = SQLHelper.GetDataTable(GlobalSpace.FSDBConnstr, sqlSelect);
        }
    }
}
