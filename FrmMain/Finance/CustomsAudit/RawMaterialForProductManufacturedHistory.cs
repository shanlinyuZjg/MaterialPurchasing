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

namespace Global.Finance.CustomsAudit
{
    public partial class RawMaterialForProductManufacturedHistory : Office2007Form
    {
        string lotNumber = string.Empty;
        string itemNumber = string.Empty;
        
        public RawMaterialForProductManufacturedHistory(string itemnumber,string lotnumber)
        {
            lotNumber = lotnumber ;
            itemNumber = itemnumber;
            this.EnableGlass = false;
            MessageBoxEx.EnableGlass = false;
            InitializeComponent();
        }

        private void RawMaterialForProductManufacturedHistory_Load(object sender, EventArgs e)
        {
            string moNumber = GetMONumber(itemNumber, lotNumber);
            if(moNumber !="")
            {
                string sqlSelect = @"SELECT
	                                                TransactionDate as 入库日期,
	                                                MONumber as 生产订单编号,
                                                    ItemNumber as 物料代码,
	                                                ItemUM1 as 单位,
	                                                ReceivingType as 类型,
	                                                ReceiptQuantity as 入库数量,
	                                                LotNumber as 批号
                                                FROM
	                                                MORV T1
                                                WHERE
	                                                T1.MONumber = '"+moNumber+"'";
                dgvProductHistory.DataSource = SQLHelper.GetDataTable(GlobalSpace.FSDBConnstr, sqlSelect);
            }
            else
            {
                MessageBoxEx.Show("未查到批号物料的生产记录！", "提示");
            }
          
        }

        private string GetMONumber(string itemnumber,string lotnumber)
        {
            string sqlSelect = @"SELECT
	                                    IssueType,
	                                    OrderNumber
                                    FROM
	                                    PICK T1
                                    WHERE
	                                    T1.ComponentItemNumber = '"+itemnumber+"' AND T1.LotNumber = '"+lotnumber+"' AND T1.IssueType = 'I'";
            string moNumber = string.Empty;

            DataTable dtTemp = SQLHelper.GetDataTable(GlobalSpace.FSDBConnstr, sqlSelect);
            if(dtTemp.Rows.Count > 0)
            {
                moNumber = dtTemp.Rows[0]["OrderNumber"].ToString();
            }
            else
            {
                moNumber = "";
            }
            return moNumber;
        }

        private void dgvProductHistory_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.RowIndex >= 0)
            {
                string lotnumber = dgvProductHistory.Rows[e.RowIndex].Cells["批号"].Value.ToString();
                string monumber = dgvProductHistory.Rows[e.RowIndex].Cells["物料代码"].Value.ToString();
                RawMaterialForProductShipped rfps = new RawMaterialForProductShipped(lotnumber, monumber);
                rfps.Show();
            }

        }

        private void dgvProductHistory_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            dgvProductHistory_CellContentDoubleClick(sender, e);
        }

        private void btnProductHistory_Click(object sender, EventArgs e)
        {
            string lotnumber = dgvProductHistory.CurrentRow.Cells["批号"].Value.ToString();
            string monumber = dgvProductHistory.CurrentRow.Cells["物料代码"].Value.ToString();
            RawMaterialForProductShipped rfps = new RawMaterialForProductShipped(lotnumber, monumber);
            rfps.Show();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            btnProductHistory_Click(sender, e);
        }
    }
}
