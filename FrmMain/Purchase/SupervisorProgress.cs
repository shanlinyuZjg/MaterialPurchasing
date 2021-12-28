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
using NPOI.XSSF.UserModel;
using System.IO;
using NPOI.SS.UserModel;
using Global;
using Global.Helper;


namespace Global.Purchase
{
    public partial class SupervisorProgress : Office2007Form
    {
        string userID = string.Empty;
        public SupervisorProgress(string id)
        {
            InitializeComponent();
            this.EnableGlass = false;
            MessageBoxEx.EnableGlass = false;
            userID = id;
        }

        private void btnSearchItem_Click(object sender, EventArgs e)
        {
            string sqlCriteriaItem = string.Empty;
            string sqlCriteriaType = string.Empty;
            string sqlCriteriaDate = string.Empty;
            string sqlSelectPONumber = string.Empty;

            string sqlCriteriaOrder = "  Order By POItemPlacedDate Desc";


            if (rbtnDomestic.Checked)
            {
                sqlCriteriaType = " IsFOItem = 0  ";
            }
            else if (rbtnForeign.Checked)
            {
                sqlCriteriaType = "   IsFOItem = 1    ";
            }
            else
            {
                sqlCriteriaType = "  1=1  ";
            }

            if (rbtnDate.Checked)
            {
                sqlCriteriaDate = " (POItemPlacedDate >='" + dtpStartDate.Value.AddDays(-1).ToString("yyyy-MM-dd") + "' And POItemPlacedDate <='" + dtpEndDate.Value.AddDays(1).ToString("yyyy-MM-dd") + "')";
            }
            else
            {
                sqlCriteriaDate = " 1=1 ";
            }

            if(string.IsNullOrEmpty(tbSearchItem.Text))
            {
                sqlCriteriaItem = " And 1 = 1 ";
            }
            else
            {
                sqlCriteriaItem = tbSearchItem.Text.Trim();
                if(rbtnFONumber.Checked)
                {
                    sqlCriteriaItem = " And ForeignNumber Like'%" + sqlCriteriaItem + "%' ";
                }
                else if (rbtnItemNumber.Checked)
                {
                    sqlCriteriaItem = " And ItemNumber='" + sqlCriteriaItem + "' ";
                }
                else if (rbtnItemDescription.Checked)
                {
                    sqlCriteriaItem = " And ItemDescription like '%" + sqlCriteriaItem + "%'  ";
                }
                else if(rbtnVendorName.Checked)
                {
                    sqlCriteriaItem = " And VendorName like '%" + sqlCriteriaItem + "%'  ";
                }
            }

            sqlSelectPONumber = @"SELECT  Distinct  PONumber FROM PurchaseOrderRecordByCMF  WHERE  Superior = '" + userID + "'  And IsPurePO = 0 " + sqlCriteriaItem;

            string sqlSelectPO = @"Select   PONumber AS 采购单号,VendorNumber AS 供应商代码,VendorName AS 供应商名称,POItemPlacedDate AS 订单创建日期,Buyer AS 采购员  from PurchaseOrderRecordByCMF Where Superior = '" + userID + "' And PONumber In (" + sqlSelectPONumber + ")  And IsPurePO = 1  " + " And " + sqlCriteriaType + "  And  " + sqlCriteriaDate + sqlCriteriaOrder;

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
                                                    ActualDeliveryDate	AS 实际到货日期,
                                                    ItemUsedPoint	AS 提报单位,
                                                    Comment1	AS 备注,
                                                    VendorNumber	AS 供应商代码,
                                                    VendorName AS 供应商,
                                                    PricePreTax	AS 含税价格,
                                                    Comment2	AS 备注,                
                                                    ActualDeliveryQuantity	AS 实际到货数量,
                                                    LotNumber	AS 供应商批号,
                                                    InvoiceNumber	AS 发票号,
                                                    InvoiceIssuedDateTime	AS 开票时间,
                                                    InstanceID	AS 实例号,
                                                    ContractID	AS 合同号,
                                                    Comment3	AS 备注,Guid
                                                    FROM
	                                                    PurchaseOrderRecordByCMF
                                                    WHERE
	                                                    Superior = '" + userID + "' And PONumber In (" + sqlSelectPONumber + ") And IsPurePO = 0    " + " And " + sqlCriteriaType + "  And  " + sqlCriteriaDate + sqlCriteriaItem+sqlCriteriaOrder;
            
            BindCustomerData(sqlSelectPO, sqlSelectPODetail);
        }

        private void tbSearchItem_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                if (tbSearchItem.Text != "")
                {
                    btnSearchItem_Click(sender, e);
                }
            }
        }

        private void tbSearchItem_TextChanged(object sender, EventArgs e)
        {
            tbSearchItem.Text = tbSearchItem.Text.ToUpper();
            tbSearchItem.SelectionStart = tbSearchItem.TextLength;
        }

        private void BindCustomerData(string sqlSelectPO, string sqlSelectPODetail)
        {
            DataSet _DataSet = new DataSet();
            List<string> guidList = new List<string>();
            DataTable dt = SQLHelper.GetDataTable(GlobalSpace.FSDBConnstr, sqlSelectPODetail);
            DataTable dtPO = SQLHelper.GetDataTable(GlobalSpace.FSDBConnstr, sqlSelectPO);
            guidList = dt.AsEnumerable().Select(r => r.Field<string>("Guid")).ToList();
            string sqlselect = @"Select ReceiveQuantity,ParentGuid From PurchaseOrderRecordHistoryByCMF Where Status = 2 And ParentGuid IN('{0}')";
            sqlselect = string.Format(sqlselect, string.Join("','", guidList.ToArray()));
            DataTable dtQuantity = SQLHelper.GetDataTable(GlobalSpace.FSDBConnstr, sqlselect);

            foreach(DataRow dr in dt.Rows)
            {
                DataRow[] drs = dtQuantity.Select("ParentGuid = '" + dr["Guid"].ToString() + "'");
                double quantity = 0;
                for(int i = 0;i <drs.Length;i++)
                {
                    quantity +=Convert.ToDouble(drs[i]["ReceiveQuantity"]);
                }
                dr["实际到货数量"] = quantity;
            }
            dt.TableName = "Orders";
            dtPO.TableName = "Order Details";
            /*
            using (OleDbConnection cn =
                new OleDbConnection(GlobalSpace.oledbconnstrFSDB))
            {
                new OleDbDataAdapter(sqlSelectPO, cn).Fill(_DataSet, "Orders");
                new OleDbDataAdapter(sqlSelectPODetail, cn).Fill(_DataSet, "Order Details");

                //    _DataSet.Relations.Add("2", _DataSet.Tables["Orders"].Columns["采购单号"],  _DataSet.Tables["Order Details"].Columns["采购单号"], false);
                    _DataSet.Relations.Add("2", _DataSet.Tables["Orders"].Columns["采购单号"],  dt.Columns["采购单号"], false);
            }
            */
            _DataSet.Tables.Add(dtPO.Copy());
            _DataSet.Tables.Add(dt.Copy());
            _DataSet.Relations.Add("2", _DataSet.Tables["Orders"].Columns["采购单号"], _DataSet.Tables["Order Details"].Columns["采购单号"], false);
          //  _DataSet.Relations.Add("2", _DataSet.Tables["Order Details"].Columns["采购单号"], _DataSet.Tables["Orders"].Columns["采购单号"], false);
            superGridControl1.PrimaryGrid.DataSource = _DataSet;
            superGridControl1.PrimaryGrid.DataMember = "Orders";
       //     superGridControl1.PrimaryGrid.DataMember = "Order Details";
        }

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
            for (int m = 0; m< dt.Columns.Count; m++)
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

        private void SupervisorProgress_Load(object sender, EventArgs e)
        {

        }

        private void rbtnFONumber_CheckedChanged(object sender, EventArgs e)
        {
            if(rbtnFONumber.Checked)
            {
                rbtnForeign.Checked = true;
                tbSearchItem.Focus();
            }
        }
    }
}
