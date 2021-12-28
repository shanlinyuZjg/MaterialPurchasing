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
using System.IO;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;

namespace Global.Purchase
{
    public partial class ImportDemesticProduct : Office2007Form
    {
        string UserID = string.Empty;
        string Password = string.Empty;
        string UserName = string.Empty;
        string SupervisorID = string.Empty;
        public ImportDemesticProduct(string userid,string pwd,string name,string superid)
        {
            this.EnableGlass = false;
            MessageBoxEx.EnableGlass = false;
            UserID = userid;
            Password = pwd;
            UserName = name;
            SupervisorID = superid;
            InitializeComponent();
        }

        private void btnChooseFile_Click(object sender, EventArgs e)
        {
            CommonOperate.GetExcelFileInfo(tbFilePath, cbbSheetName);
        }

        private void btnShowExcelContent_Click(object sender, EventArgs e)
        {
            if (tbFilePath.Text == "" || cbbSheetName.Text == "")
            {
                Custom.MsgEx("当前无可用的导入文件！");
                return;
            }
            string sqlSelect = string.Empty;
            if(rbtnOriginalData.Checked)
            {
                sqlSelect = @"select   [成品代码],[代码] AS 物料代码,[描述],[计划量], [供应商代码],[供应商名称],[采购单价],[确认人],[承诺到货]  from[" + cbbSheetName.SelectedItem.ToString().Trim() + "] Order BY [代码]";
            }
            else if(rbtnPurchaseData.Checked)
            {
                sqlSelect = @"select   [物料代码],[描述] AS 物料描述,[采购单价],[采购数量],[供应商代码],[供应商名称],[确认人],[承诺到货]  from[" + cbbSheetName.SelectedItem.ToString().Trim() + "] Order BY [物料代码]";
            }
           else
            {
                sqlSelect = @"select   [物料代码],[描述] AS [物料描述],[供应商代码],[供应商名称],[含税价格]  from[" + cbbSheetName.SelectedItem.ToString().Trim() + "] Order BY [物料代码]";
            }
            DataTable dtTemp = CommonOperate.ImportExcelFile(sqlSelect, tbFilePath);
            dgvExcelContent.DataSource = dtTemp;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if(rbtnPurchaseData.Checked)
            {
                Custom.MsgEx("当前数据不允许进行此操作！");
                return;
            }
            DataTable dt = (DataTable)dgvExcelContent.DataSource;
            List<string> sqlList = new List<string>();
            string[] itemNumberArray = dt.AsEnumerable().Select(r => r.Field<string>("物料代码")).ToArray();
            string sqlSelect = @"SELECT
	                                                T2.ItemNumber AS 物料代码,
	                                                T1.OnHandQuantity AS 库存量 ,
	                                                T1.OnOrderQuantity AS 在订量 ,
	                                                T1.InInspectionQuantity AS 在检量
                                                FROM
	                                                _NoLock_FS_ItemData T1
                                                LEFT JOIN _NoLock_FS_Item T2 ON T1.ItemKey = T2.ItemKey
                                                WHERE
	                                                T2.ItemNumber IN ('{0}') Order BY T2.ItemNumber";

            sqlSelect = string.Format(sqlSelect, string.Join("','", itemNumberArray));
            DataTable dtTemp = SQLHelper.GetDataTableOleDb(GlobalSpace.oledbconnstrFSDBMR, sqlSelect);

            dt.Columns.Add("库存量");
            dt.Columns.Add("在订量");
            dt.Columns.Add("在检量");
            dt.Columns.Add("合计量");
            dt.Columns.Add("采购数量");

            for (int i = 0;i < dt.Rows.Count; i++)
            {
                DataRow[] drs = dtTemp.Select("物料代码='" + dt.Rows[i]["物料代码"].ToString() + "'");
                if (drs.Length == 0)
                {
                    dt.Rows[i]["库存量"] = 0;
                    dt.Rows[i]["在订量"] = 0;
                    dt.Rows[i]["在检量"] = 0;
                    dt.Rows[i]["合计量"] = 0;
                    dt.Rows[i]["采购数量"] = 0;
                }
                else
                {
                    dt.Rows[i]["库存量"] = drs[0]["库存量"];
                    dt.Rows[i]["在订量"] = drs[0]["在订量"];
                    dt.Rows[i]["在检量"] = drs[0]["在检量"];
                    dt.Rows[i]["合计量"] = Convert.ToDouble(drs[0]["库存量"]) + Convert.ToDouble(drs[0]["在订量"]) + Convert.ToDouble(drs[0]["在检量"]);
                    dt.Rows[i]["采购数量"] = 0;
                    /*
                    DataTable dtParent = CommonOperate.GetBOMCompomnentItemQuantity(drs[0]["物料代码"].ToString());
                    if (dtParent.Rows.Count > 0)
                    {
                        if (dtParent.Rows.Count == 1)
                        {
                            dt.Rows[i]["成品代码"] = dtParent.Rows[0]["ItemNumber"];
                            dt.Rows[i]["成品描述"] = dtParent.Rows[0]["ItemDescription"];
                            dt.Rows[i]["规格"] = 1/Convert.ToDouble(dtParent.Rows[0]["RequiredQuantity"]);
                            dt.Rows[i]["计算后计划量"] = Convert.ToDouble(dt.Rows[i]["计划量"]) * Convert.ToDouble(dtParent.Rows[0]["RequiredQuantity"]);
                        }
                        else
                        {
                            int irow = dtParent.DefaultView.ToTable(true, "RequiredQuantity").Rows.Count;
                            if(irow == 1)
                            {
                                dt.Rows[i]["成品代码"] = dtParent.Rows[0]["ItemNumber"];
                                dt.Rows[i]["成品描述"] = dtParent.Rows[0]["ItemDescription"];
                                dt.Rows[i]["规格"] = 1 / Convert.ToDouble(dtParent.Rows[0]["RequiredQuantity"]);
                                dt.Rows[i]["计算后计划量"] = Convert.ToDouble(dt.Rows[i]["计划量"]) * Convert.ToDouble(dtParent.Rows[0]["RequiredQuantity"]);
                            }
                            else
                            {
                                dt.Rows[i]["成品描述"] = "多个规格不一致父项，请手动填写规格";
                            }
                        }
                    }
                    */
                }
            }

            dgvExcelContent.DataSource = dt;
            /*
            for(int j = 0;j < dgvExcelContent.Rows.Count; j++)
            {
                if(dgvExcelContent.Rows[j].Cells["成品描述"].Value.ToString().Contains("多个规格"))
                {
                    dgvExcelContent.Rows[j].DefaultCellStyle.BackColor = Color.LightGoldenrodYellow;
                }
            }
            */
        }

        private void btnExportToExcel_Click(object sender, EventArgs e)
        {
            if(dgvExcelContent.Rows.Count == 0)
            {
                Custom.MsgEx("当前没有可导出内容！");
                return;
            }
            DataTable dt = (DataTable)dgvExcelContent.DataSource;
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
                    if(dt.Columns[j].ColumnName == "计划量" || dt.Columns[j].ColumnName == "采购单价" || dt.Columns[j].ColumnName == "库存量" || dt.Columns[j].ColumnName == "在订量" || dt.Columns[j].ColumnName == "在检量" || dt.Columns[j].ColumnName == "合计量" || dt.Columns[j].ColumnName == "采购数量")
                    {
                        if(dt.Rows[i][j] == DBNull.Value || dt.Rows[i][j].ToString() == "")
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

            GC.Collect();
        }

        private void btnPlaceOrder_Click(object sender, EventArgs e)
        {
            List<string> itemNumberList = new List<string>();


            if (dgvExcelContent.Rows.Count == 0)
            {
                Custom.MsgEx("当前无可用数据！");
                return;
            }
            if(rbtnPurchaseData.Checked)
            {
                DataTable dtVendor = null;
                DataTable dtItem = null;
                if(dgvExcelContent.Rows.Count > 0)
                {
                    dtVendor = (DataTable)dgvExcelContent.DataSource;
                    dtItem = dtVendor.Copy();
                }

                if(CommonOperate.PlaceOrderWithItemDetail("PP",dtVendor,dtItem,UserName,UserID,SupervisorID,1,Convert.ToDouble(tbTaxRate.Text)))
                {
                    Custom.MsgEx("订单已提交审核！");
                    if(itemNumberList.Count > 0 )
                    {
                        string itemNumbers = string.Empty;
                        for(int x = 0;x < itemNumberList.Count;x++)
                        {
                            itemNumbers = itemNumbers+ " "+ itemNumberList[x];
                        }
                        MessageBox.Show("以下物料价格超出四班标准价格15%，无法下达订单！", "提示");
                    }
                    List<string> listSuper = CommonOperate.GetSuperiorNameAndEmail(UserID);
                    string sqlSelectUserInfo = @"Select Email,Password,Name From PurchaseDepartmentRBACByCMF Where UserID='" + UserID + "'";
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
                                email.emailContent = supername + "处长" + "：" + "<br>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + "您好!<br>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;采购员已提交采购订单申请，请及时审批！";

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
                else
                {
                    Custom.MsgEx("订单提交审核失败");
                }
            }
            else
            {
                Custom.MsgEx("当前数据不可用！");
            }
        }

        private void ImportDemesticProduct_Load(object sender, EventArgs e)
        {
            if(rbtnOriginalData.Checked)
            {
                btnPlaceOrder.Enabled = false;
            }
        }

        private void rbtnOriginalData_CheckedChanged(object sender, EventArgs e)
        {
            if(rbtnOriginalData.Checked)
            {
                btnPlaceOrder.Enabled = false;
                btnImportDomesticProductPrice.Enabled = false;
                btnSearch.Enabled = true;
            }
        }



        private void btnImportDomesticProductPrice_Click(object sender, EventArgs e)
        {
            if(dgvExcelContent.Rows.Count == 0)
            {
                Custom.MsgEx("当前无可用数据！");
                return;
            }
            DataTable dt = (DataTable)dgvExcelContent.DataSource;
            string sqlSelect = @"Select Id, ItemNumber,VendorNumber,PricePreTax  From PurchaseDepartmentDomesticProductItemPrice";
            DataTable dtOriginal = SQLHelper.GetDataTable(GlobalSpace.FSDBConnstr, sqlSelect);
            List<string> sqlList = new List<string>();
            string sql = string.Empty;
            foreach(DataRow dr in dt.Rows)
            {
                DataRow[] drs = dt.Select("ItemNumber='" + dr["物料代码"].ToString()+"' And VendorNumber = '"+ dr["供应商代码"].ToString() + "'");
                if(drs.Length > 0)
                {
                    sql = @"Update PurchaseDepartmentDomesticProductItemPrice Set PricePreTax = "+Convert.ToDouble(dr["含税价格"])+" Where Id = "+Convert.ToInt32(dr["Id"])+"";
                }
                else
                {
                    sql = @"Insert Into PurchaseDepartmentDomesticProductItemPrice ([ItemNumber],
                                                                 [ItemDescription],
                                                                 [VendorNumber],
                                                                 [VendorName],
                                                                 [PricePreTax],
                                                                 [Operator]) Values ('"+dr["物料代码"].ToString()+ "','" + dr["物料描述"].ToString() + "','" + dr["供应商代码"].ToString() + "','" + dr["供应商名称"].ToString() + "',"+Convert.ToDecimal(dr["含税价格"]) +",'"+UserID+"')";
                }
                sqlList.Add(sql);
            }
           
            if(SQLHelper.BatchExecuteNonQuery(GlobalSpace.FSDBConnstr,sqlList))
            {
                Custom.MsgEx("导入成功！");
            }
            else
            {
                Custom.MsgEx("导入失败！");

            }
        }

        private void rbtnPurchaseData_CheckedChanged(object sender, EventArgs e)
        {
            if(rbtnPurchaseData.Checked)
            {
                btnPlaceOrder.Enabled = true;
                btnImportDomesticProductPrice.Enabled = false;
                btnSearch.Enabled = false;
            }
        }

        private void rbtnItemPrice_CheckedChanged(object sender, EventArgs e)
        {
            if(rbtnItemPrice.Checked)
            {
                btnPlaceOrder.Enabled = false;
                btnImportDomesticProductPrice.Enabled = true;
                btnSearch.Enabled = false;
            }
        }

        private void btnManageItemPrice_Click(object sender, EventArgs e)
        {
            ManageItemPrice mip = new ManageItemPrice(UserID);
            mip.ShowDialog();
        }

        private void btnItemWithoutReview_Click(object sender, EventArgs e)
        {
            DomesticProductItemWithoutReview dpwr = new DomesticProductItemWithoutReview();
            dpwr.ShowDialog();
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
   
        }
    }
}
