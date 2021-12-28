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

namespace Global.Warehouse
{
    public partial class BatchPORVX : Office2007Form
    {
        //定义简体中文和西欧文编码字符集
        public static Encoding GB2312 = Encoding.GetEncoding("gb2312");
        public static Encoding ISO88591 = Encoding.GetEncoding("iso-8859-1");
        string FSUserID = string.Empty;
        string FSPassword = string.Empty;
        public BatchPORVX(string id,string pwd)
        {
            InitializeComponent();
            this.EnableGlass = false;
            MessageBoxEx.EnableGlass = false;
            FSUserID = id;
            FSPassword = pwd;
        }

        private void tbPONumberOut_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(!string.IsNullOrEmpty(tbPONumberOut.Text))
            {
                if (e.KeyChar == (char)13)
                {
                    tbLineNumberOut.Focus();
                }
            }

        }

        private void tbLineNumberOut_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!string.IsNullOrEmpty(tbLineNumberOut.Text))
            {
                if (e.KeyChar == (char)13)
                {
                    string sqlSelect = @"SELECT
	                                ItemNumber  AS 物料代码,
	                                ReceiptQuantity  AS 入库数量,
	                                Stockroom1  AS 库,
	                                Bin1  AS 位,
	                                LotNumber  AS 厂家批号,
	                                VendorLotNumber  AS 公司批号,
	                                LotUserDefined5  AS 生产商码,
	                                LotDescription AS 生产商名,PromisedDeliveryDate AS 承诺交货日,LotExpirationDate AS 到期日期
                                FROM
	                                PORV
                                WHERE
	                                PONumber = '" + tbPONumberOut.Text+"'   AND POLineNumber = '"+tbLineNumberOut.Text+"'";
                    dgvDetail.DataSource = SQLHelper.GetDataTableOleDb(GlobalSpace.oledbconnstrFSDB, sqlSelect);
                }
            }

            
        }

        private void btnDoit_Click(object sender, EventArgs e)
        {
            List<DataGridViewRow> dgvrList = new List<DataGridViewRow>();
            foreach(DataGridViewRow dgvr in dgvDetail.Rows)
            {
                if(Convert.ToBoolean(dgvr.Cells["Check"].Value))
                {
                    dgvrList.Add(dgvr);
                }
            }
            FSFunctionLib.FSConfigFileInitialize(GlobalSpace.fsconfigfilepath, FSUserID, FSPassword);
            for (int i = 0;i < dgvrList.Count; i++)
            {
                PORVX(dgvrList[i], tbPONumberOut.Text, tbLineNumberOut.Text);
            }
            FSFunctionLib.FSExit();
        }

        //库管员操作退库
        private bool PORVX(DataGridViewRow dgvr,string poNumber,string lineNumber)
        {
            bool bSucceed = false;
            
            PORV02 porv02 = new PORV02();
            porv02.PONumber.Value = poNumber;
            porv02.POLineNumber.Value = lineNumber;
            porv02.POReceiptActionType.Value = "X";
            porv02.ItemNumber.Value = dgvr.Cells["物料代码"].Value.ToString();
            porv02.LotNumber.Value = dgvr.Cells["厂家批号"].Value.ToString().Trim();
            porv02.POLineType.Value = "P";
            porv02.LocationReverseQuantity1.Value = dgvr.Cells["入库数量"].Value.ToString();
            porv02.Stockroom1.Value = dgvr.Cells["库"].Value.ToString();
            porv02.Bin1.Value = dgvr.Cells["位"].Value.ToString();
            porv02.InventoryCategory1.Value = tbStatus.Text;
            porv02.PromisedDate.Value = dgvr.Cells["承诺交货日"].Value.ToString().Substring(5,2)+ dgvr.Cells["承诺交货日"].Value.ToString().Substring(8, 2)+ dgvr.Cells["承诺交货日"].Value.ToString().Substring(2, 2);
                porv02.NewLot.Value = "Y";

            try
            {
                if (FSFunctionLib.fstiClient.ProcessId(porv02, null))
                {
                    bSucceed = true;
                    string itemNumber = string.Empty;
                    string itemUM = string.Empty;
                    string sqlSelectPOItemNumber = @"SELECT
	                                                        T2.VendorItemNumber AS 物料代码,T5.ItemUM AS 单位
                                                        FROM
	                                                        FSDBMR.dbo._NoLock_FS_POHeader T1,
	                                                        FSDBMR.dbo._NoLock_FS_VendorItem T2,
	                                                        FSDBMR.dbo._NoLock_FS_POLine T3,
	                                                        FSDBMR.dbo._NoLock_FS_Vendor T4,
	                                                        FSDBMR.dbo._NoLock_FS_Item T5,
	                                                        FSDBMR.dbo._NoLock_FS_POLineData T6
                                                        WHERE
	                                                        T1.POHeaderKey = T3.POHeaderKey
                                                        AND T2.VendorKey = T4.VendorKey
                                                        AND T2.VendorItemKey = T3.VendorItemKey
                                                        AND T6.POLineKey = T3.POLineKey
                                                        AND T5.ItemNumber = T2.ItemNumber
                                                        AND T1.PONumber = '" + tbPONumberIn.Text.ToUpper()+"'   AND T3.POLineNumberString = '"+tbLineNumberIn.Text+"'";
                    DataTable dtTemp = SQLHelper.GetDataTable(GlobalSpace.FSDBMRConnstr, sqlSelectPOItemNumber);
                    if(dtTemp.Rows.Count > 0)
                    {
                        itemNumber = dtTemp.Rows[0]["物料代码"].ToString();
                        itemUM = dtTemp.Rows[0]["单位"].ToString();
                    }
                    else
                    {
                        Custom.MsgEx("该物料代码不准确，请联系管理员！");
                        return false;
                    }
                    PORV01 porv01 = new PORV01();
                    porv01.PONumber.Value = tbPONumberIn.Text.ToUpper();
                    porv01.POLineNumber.Value = tbLineNumberIn.Text;
                    porv01.POLineUM.Value = itemUM;
                    porv01.POReceiptActionType.Value = "R";
                    porv01.Stockroom1.Value = dgvr.Cells["库"].Value.ToString();
                    porv01.Bin1.Value = dgvr.Cells["位"].Value.ToString();
                
                    porv01.InventoryCategory1.Value = "I";

                    porv01.ReceiptQuantityMove1.Value = dgvr.Cells["入库数量"].Value.ToString();
                    porv01.POLineType.Value = "P";
                    porv01.ItemNumber.Value = itemNumber;
                    porv01.NewLot.Value = "Y";

                    porv01.LotNumberAssignmentPolicy.Value = "C";

                    porv01.LotNumberDefault.Value = dgvr.Cells["厂家批号"].Value.ToString().ToUpper().TrimEnd();
                    porv01.LotNumber.Value = dgvr.Cells["厂家批号"].Value.ToString().ToUpper().TrimEnd();

                    porv01.VendorLotNumber.Value = dgvr.Cells["公司批号"].Value.ToString().ToUpper().TrimEnd();

                    porv01.LotDescription.Value = dgvr.Cells["生产商名"].Value.ToString().TrimEnd();
                    porv01.LotUserDefined5.Value = dgvr.Cells["生产商码"].Value.ToString().TrimEnd();
                    string year = string.Empty;
                    string month = string.Empty;
                    string day = string.Empty;
                   /*
                    if (dgvr.Cells["到期日期"].Value.ToString().Contains("/"))
                    {
                        string[] dates = dgvr.Cells["到期日期"].Value.ToString().Split('/');
                        if(dates[1].Length == 1)
                        {
                            year = dgvr.Cells["到期日期"].Value.ToString().Substring(2, 2);
                            month = "0" + dates[1];
                            if(dates[2].Length == 1)
                            {
                                day = "0"+dates[2];
                            }
                            else
                            {
                                day = dates[2];
                            }
                        }
                        else
                        {
                            year = dgvr.Cells["到期日期"].Value.ToString().Substring(2, 2);
                            month = dates[1];
                            if (dates[2].Length == 1)
                            {
                                day = "0" + dates[2];
                            }
                            else
                            {
                                day = dates[2];
                            }
                        }
                    }
                    else if (dgvr.Cells["到期日期"].Value.ToString().Contains("-"))
                    {
                        string[] dates = dgvr.Cells["到期日期"].Value.ToString().Split('-');
                        if (dates[1].Length == 1)
                        {
                            year = dgvr.Cells["到期日期"].Value.ToString().Substring(2, 2);
                            month = "0" + dates[1];
                            if (dates[2].Length == 1)
                            {
                                day = "0" + dates[2];
                            }
                            else
                            {
                                day = dates[2];
                            }
                        }
                        else
                        {
                            year = dgvr.Cells["到期日期"].Value.ToString().Substring(2, 2);
                            month = dates[1];
                            if (dates[2].Length == 1)
                            {
                                day = "0" + dates[2];
                            }
                            else
                            {
                                day = dates[2];
                            }
                        }
                    }*/
                    porv01.LotExpirationDate.Value = dgvr.Cells["到期日期"].Value.ToString().Substring(5, 2) + dgvr.Cells["到期日期"].Value.ToString().Substring(8, 2) + dgvr.Cells["到期日期"].Value.ToString().Substring(2, 2); 

                    porv01.PromisedDate.Value = tbPromisedDate.Text;


                    string transactionString = porv01.GetString(TransactionStringFormat.fsCDF);
             //        MessageBox.Show(transactionString);
                    
                    if (FSFunctionLib.fstiClient.ProcessId(porv01, null))
                    {
                        /*
                        listResult.Items.Add("Success:");
                        listResult.Items.Add("");
                        listResult.Items.Add(FSFunctionLib.fstiClient.CDFResponse);
                        */
                        return true;
                    }
                    else
                    {
                        FSTIError error = FSFunctionLib.fstiClient.TransactionError;
                        MessageBox.Show(GB2312.GetString(ISO88591.GetBytes(error.Description)));
                    }
                    
                  
                   
                }
                else
                {
                    FSFunctionLib.FSErrorMsg("四班异常信息");
                }
            }
            catch (Exception ex)
            {
                MessageBoxEx.Show("出现异常：" + ex.Message);
            }
            return bSucceed;
        }

        private void BatchPORVX_Load(object sender, EventArgs e)
        {

        }

        //库管员操作入库
        private bool PORV(DataRow dr)
        {
            PORV01 porv01 = new PORV01();
            porv01.PONumber.Value = dr["采购单号"].ToString();
            porv01.POLineNumber.Value = dr["行号"].ToString();
            porv01.POLineUM.Value = dr["单位"].ToString();
            porv01.POReceiptActionType.Value = "R";
            porv01.Stockroom1.Value = dr["库"].ToString();
            porv01.Bin1.Value = dr["位"].ToString();
            if (dr["检验"].ToString().ToUpper() == "Y")
            {
                porv01.InventoryCategory1.Value = "I";
                //          porv01.InspectionCode1.Value = "N";//;
            }
            else
            {
                porv01.InventoryCategory1.Value = "O";
                //              porv01.InspectionCode1.Value = "G";//;
            }
            porv01.ReceiptQuantityMove1.Value = dr["入库数量"].ToString();
            porv01.POLineType.Value = "P";
            porv01.ItemNumber.Value = dr["物料代码"].ToString();
            porv01.NewLot.Value = "Y";
            if (dr["LotNumberAssign"].ToString() == "C")
            {
                porv01.LotNumberAssignmentPolicy.Value = "C";
                if (dr["公司批号"] == DBNull.Value || string.IsNullOrEmpty(dr["公司批号"].ToString()))
                {
                    porv01.LotNumberDefault.Value = dr["厂家批号"].ToString().ToUpper();
                    porv01.LotNumber.Value = dr["厂家批号"].ToString().ToUpper();
                }
                else if (dr["厂家批号"] == DBNull.Value || string.IsNullOrEmpty(dr["厂家批号"].ToString()))
                {
                    porv01.LotNumberDefault.Value = dr["公司批号"].ToString().ToUpper();
                    porv01.LotNumber.Value = dr["公司批号"].ToString().ToUpper();
                }
                else
                {
                    porv01.LotNumberDefault.Value = dr["厂家批号"].ToString().ToUpper();
                    porv01.LotNumber.Value = dr["厂家批号"].ToString().ToUpper();
                }
                porv01.VendorLotNumber.Value = dr["公司批号"].ToString().ToUpper();
                porv01.LotDescription.Value = dr["生产商名"].ToString();
                porv01.LotUserDefined5.Value = dr["生产商码"].ToString();
                //       porv01.POReceiptDate.Value = DateTime.Now.ToString("MMddyy");//此处不确定

                string expiredDate = dr["到期日期"].ToString();
                if (expiredDate.Length == 8)//20200527格式
                {
                    porv01.LotExpirationDate.Value = expiredDate.Substring(4, 4) + expiredDate.Substring(2, 2);
                }
                else if (expiredDate.Length == 10)//2020.05.27格式
                {
                    porv01.LotExpirationDate.Value = expiredDate.Substring(5, 2) + expiredDate.Substring(8, 2) + expiredDate.Substring(2, 2);
                }
                else
                {
                    //此处的日期格式不符合要求，故意赋值，在写入四班时会报错，用以提示具体报错问题
                    porv01.LotExpirationDate.Value = expiredDate;
                }
            }
            else
            {
                porv01.LotNumberAssignmentPolicy.Value = "N";
            }
            porv01.PromisedDate.Value = dr["承诺交货日"].ToString();


            string transactionString = porv01.GetString(TransactionStringFormat.fsCDF);
                        MessageBox.Show(transactionString);
 
            if (FSFunctionLib.fstiClient.ProcessId(porv01, null))
            {
                /*
                listResult.Items.Add("Success:");
                listResult.Items.Add("");
                listResult.Items.Add(FSFunctionLib.fstiClient.CDFResponse);
                */
                return true;
            }
            FSTIError error = FSFunctionLib.fstiClient.TransactionError;
          
            CommonOperate.WriteFSErrorLog("PORV", porv01, error, FSUserID, dr["采购单号"].ToString() + " " + dr["行号"].ToString());
            
            return false;
        }
        //库管员操作入库
     
        private void buttonX1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("test");
            
            FSFunctionLib.FSConfigFileInitialize(GlobalSpace.fsTestconfigfilepath, "S67", "123456");

            
         
            FSFunctionLib.FSExit();
            
        }

        private void dgvDetail_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
     
        }

        private void tbPONumberIn_TextChanged(object sender, EventArgs e)
        {
            tbPONumberIn.Text = tbPONumberIn.Text.ToUpper();
            tbPONumberIn.SelectionStart = tbPONumberIn.Text.Length;
        }

        private void tbPONumberOut_TextChanged(object sender, EventArgs e)
        {
            tbPONumberOut.Text = tbPONumberOut.Text.ToUpper();
            tbPONumberOut.SelectionStart = tbPONumberOut.Text.Length;
        }

        private void tbLineNumberIn_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(!string.IsNullOrWhiteSpace(tbLineNumberIn.Text))
            {
                if(e.KeyChar == (char)13)
                {

                }
            }
        }
    }
}
