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
using System.IO;
using NPOI.XSSF.UserModel;
using NPOI.SS.UserModel;

namespace Global.Purchase
{
    public partial class POProgress : Office2007Form
    {
        string UserID = string.Empty;

        public POProgress(string id)
        {
            UserID = id;
            this.EnableGlass = false;
            MessageBoxEx.EnableGlass = false;
            InitializeComponent();
        }

        private void textBoxX2_TextChanged(object sender, EventArgs e)
        {
            tbForeignNumber.Text = tbForeignNumber.Text.ToUpper();
            tbForeignNumber.SelectionStart = tbForeignNumber.Text.Length;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string sqlSelect = sqlSelect = @"SELECT  
                                                      (case POStatus when '-1' then '已取消' when  '0' then '已准备'
                                                                 when  '1' then '已提交'
                                                                 when  '2' then '已审核'
                                                                 when  '3' then '已下达' 
                                                                when  '4' then '已到货' 
                                                                when  '5' then '已收货' 
                                                                when  '6' then '已入库' 
                                                                when  '7' then '已开票' 
                                                                when  '66' then '多次到货' 
                                                        end     
                                                        ) as 物料状态,                                                    
                                                    ForeignNumber	AS 外贸单号,
                                                    PONumber	AS 采购单号,
                                                    LineNumber AS 行号,
                                                    ItemNumber	AS 物料代码,
                                                    ItemDescription	AS 物料名称,
                                                    Specification	AS 规格,
                                                    LineUM	AS 单位,
                                                    POItemQuantity	AS 数量,
                                                   PricePreTax	AS 含税价格,                                                                                                  
                                                    Comment1	AS 备注1,
                                                    VendorName AS 供应商,                                                   
                                                    POItemPlacedDate	AS 下单日期,
                                                    DemandDeliveryDate	AS 要求到货时间,                                                  
                                                    ActualDeliveryDate	AS 实际到货日期,
                                                    ActualDeliveryQuantity	AS 实际到货数量,
                                                    Comment2	AS 备注2,
                                                    LotNumber	AS 供应商批号,
                                                    InvoiceNumber	AS 发票号,
                                                    InvoiceIssuedDateTime	AS 开票时间,
                                                    InstanceID	AS 实例号,
                                                    ContractID	AS 合同号,
                                                    Comment3	AS 备注3,
                                                      ItemUsedPoint	AS 提报单位,
                                                     QualityCheckStandard	AS 质量标准,Guid
                                                    FROM
	                                                    PurchaseOrderRecordByCMF
                                                    WHERE  ";
            string sqlFilter = string.Empty;
            string sqlSort = @"  Order By POItemPlacedDate Desc ";

            if(!(rbItem.Checked || rbDate.Checked || rbForeignNumber.Checked || rbVendor.Checked))
            {
                Custom.MsgEx("没有选择查询类型！");
                return;
            }

            if(rbItem.Checked)
            {
                if(tbItem.Text != "")
                {
                    if(CommonOperate.IsNumberOrString(tbItem.Text.Trim()))
                    {
                        sqlFilter = @" Buyer = '" + UserID + "' And     ItemNumber = '" + tbItem.Text.Trim() + "'  ";
                    }
                    else
                    {
                        sqlFilter = @" Buyer = '" + UserID + "' And     ItemDescription like '%" + tbItem.Text.Trim() + "%'  ";
                    }
                }
            }
            else if(rbVendor.Checked)
            {
                if (tbVendor.Text != "")
                {
                    if (CommonOperate.IsNumberOrString(tbVendor.Text.Trim()))
                    {
                        sqlFilter = @" Buyer = '" + UserID + "' And    VendorNumber = '" + tbVendor.Text.Trim() + "'   And IsPurePO = 0";
                    }
                    else
                    {
                        sqlFilter = @" Buyer = '" + UserID + "' And    VendorName like '%" + tbVendor.Text.Trim() + "%'   And IsPurePO = 0";
                    }
                }                
            }
            else if(rbForeignNumber.Checked)
            {
                if(tbForeignNumber.Text != "")
                {
                    sqlFilter = @"  ForeignNumber like '%" + tbForeignNumber.Text.Trim() + "%'  ";
                }
            }
            else if(rbDate.Checked)
            {
                if(tbDateStart.Text != "" && tbDateFinish.Text != "")
                {
                    sqlFilter = @" Buyer = '" + UserID + "' And    POItemPlacedDate >='" + tbDateStart.Text.Trim()+"' And POItemPlacedDate <='"+tbDateFinish.Text.Trim()+"' And IsPurePO = 0";
                }
            }
            sqlSelect = sqlSelect + sqlFilter + sqlSort;
            DataTable dtDetail = SQLHelper.GetDataTable(GlobalSpace.FSDBConnstr, sqlSelect);
            List<string> guidList = new List<string>();
            guidList = dtDetail.AsEnumerable().Select(r => r.Field<string>("Guid")).ToList();
            string sqlselect = @"Select ReceiveQuantity,ParentGuid From PurchaseOrderRecordHistoryByCMF Where Status = 2 And ParentGuid IN('{0}')";
            sqlselect = string.Format(sqlselect, string.Join("','", guidList.ToArray()));
            DataTable dtQuantity = SQLHelper.GetDataTable(GlobalSpace.FSDBConnstr, sqlselect);

            foreach (DataRow dr in dtDetail.Rows)
            {
                DataRow[] drs = dtQuantity.Select("ParentGuid = '" + dr["Guid"].ToString() + "'");
                double quantity = 0;
                for (int i = 0; i < drs.Length; i++)
                {
                    quantity += Convert.ToDouble(drs[i]["ReceiveQuantity"]);
                }
                dr["实际到货数量"] = quantity;
            }
            dgvPOItemProgress.DataSource = dtDetail;
        }

        private void POProgress_Load(object sender, EventArgs e)
        {
            tbDateStart.Text = DateTime.Now.ToString("yyyy-MM-dd");
            tbDateFinish.Text = DateTime.Now.ToString("yyyy-MM-dd");
        }

        private void rbItem_CheckedChanged(object sender, EventArgs e)
        {
            if(rbItem.Checked)
            {
                tbItem.Focus();
                tbVendor.Text = "";
                tbForeignNumber.Text = "";
            }
        }

        private void rbVendor_CheckedChanged(object sender, EventArgs e)
        {
            if(rbVendor.Checked)
            {
                tbVendor.Focus();
                tbItem.Text = "";
                tbForeignNumber.Text = "";
            }
        }

        private void rbForeignNumber_CheckedChanged(object sender, EventArgs e)
        {
            if(rbForeignNumber.Checked)
            {
                tbForeignNumber.Focus();
                tbItem.Text = "";
                tbVendor.Text = "";
            }
        }

        private void rbDate_CheckedChanged(object sender, EventArgs e)
        {
            if(rbDate.Checked)
            {
                tbDateStart.Focus();
                tbItem.Text = "";
                tbVendor.Text = "";
                tbForeignNumber.Text = "";
            }
        }

        private void tbItem_TextChanged(object sender, EventArgs e)
        {
            tbItem.Text = tbItem.Text.ToUpper();
            tbItem.SelectionStart = tbItem.Text.Length;
        }

        private void tbItem_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar == (char)13 && rbItem.Checked && tbItem.Text !="")
            {
                btnSearch_Click(sender, e);
            }
        }

        private void tbVendor_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13 && rbVendor.Checked && rbVendor.Text !="")
            {
                btnSearch_Click(sender, e);
            }
        }

        private void tbForeignNumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13 && rbForeignNumber.Checked && tbForeignNumber.Text !="")
            {
                btnSearch_Click(sender, e);
            }
        }

        private void tbDateFinish_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                btnSearch_Click(sender, e);
            }
        }

        private void btnExportToExcel_Click(object sender, EventArgs e)
        {
            /*
            string sqlSelect = sqlSelect = @"SELECT  
                                                      (case POStatus when  '0' then '已准备'
                                                                 when  '1' then '已提交'
                                                                 when  '2' then '已审核'
                                                                 when  '3' then '已下达' 
                                                                when  '4' then '已到货' 
                                                                when  '5' then '已收货' 
                                                                when  '6' then '已入库' 
                                                                when  '7' then '已开票' 
                                                                when  '66' then '多次到货' 
                                                        end     
                                                        ) as 物料状态,                                                    
                                                    ForeignNumber	AS 外贸单号,
                                                    PONumber	AS 采购单号,
                                                    LineNumber AS 行号,
                                                    ItemNumber	AS 物料代码,
                                                    ItemDescription	AS 物料名称,
                                                    Specification	AS 规格,
                                                    LineUM	AS 单位,
                                                    POItemQuantity	AS 数量,
                                                   PricePreTax	AS 含税价格,                                                                                                  
                                                    Comment1	AS 备注1,
                                                    VendorName AS 供应商,                                                   
                                                    POItemPlacedDate	AS 下单日期,
                                                    DemandDeliveryDate	AS 要求到货时间,                                                  
                                                    ActualDeliveryDate	AS 实际到货日期,
                                                    ActualDeliveryQuantity	AS 实际到货数量,
                                                    Comment2	AS 备注2,
                                                    LotNumber	AS 供应商批号,
                                                    InvoiceNumber	AS 发票号,
                                                    InvoiceIssuedDateTime	AS 开票时间,
                                                    InstanceID	AS 实例号,
                                                    ContractID	AS 合同号,
                                                    Comment3	AS 备注3,
                                                      ItemUsedPoint	AS 提报单位,
                                                     QualityCheckStandard	AS 质量标准
                                                    FROM
	                                                    PurchaseOrderRecordByCMF
                                                    WHERE POItemPlacedDate='"+dateTimePicker1.Value.ToString("yyyy-MM-dd")+"'    Order By POItemPlacedDate Desc ";
*/

            DataTable dt = (DataTable)dgvPOItemProgress.DataSource; //SQLHelper.GetDataTable(GlobalSpace.FSDBConnstr, sqlSelect);

            string filePath = tbExportFilePath.Text;
            string sheetname = "Sheet1";

            XSSFWorkbook workbook = new XSSFWorkbook();
            ISheet sheet = workbook.CreateSheet(sheetname);
            IRow rowHead = sheet.CreateRow(0);
            ICell cell;

            //填写表头
            for(int m = 0; m < dt.Columns.Count; m++)
            {
                cell = rowHead.CreateCell(m, CellType.String);
                cell.SetCellValue(dt.Columns[m].Caption);
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
                    /*  if (j == 2 || j > 4)
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
                      {*/
                    cell.SetCellValue(dt.Rows[i][j].ToString());
                    //     }
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
    }
}
