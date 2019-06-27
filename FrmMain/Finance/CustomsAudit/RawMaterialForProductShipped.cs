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
    public partial class RawMaterialForProductShipped : Office2007Form
    {
        string lotNumber = string.Empty;
        string itemNumber = string.Empty;
        public RawMaterialForProductShipped(string lot,string item)
        {
            this.EnableGlass = false;
            lotNumber = lot;
            itemNumber = item;
            InitializeComponent();
        }

        private void RawMaterialForProductShipped_Load(object sender, EventArgs e)
        {
            string sqlSelect = @"SELECT
	                        T1.CONumber as 销售单号,
	                        T1.IssueType as 销售类型,
	                        T1.CustomerID as 客户代码,
	                        T1.ShipmentDate as 发货日期,
	                        T1.ItemNumber as 物料代码,
	                        T1.ShippedQuantity as 	发货数量,
                            T1.ReversedQuantity as 退回数量
                        FROM
	                        SHIP T1
                        WHERE
	                        T1.LotNumber = '"+lotNumber+"'AND ItemNumber = '"+itemNumber+"' ";
            dgvShipHistory.DataSource = SQLHelper.GetDataTable(GlobalSpace.FSDBConnstr, sqlSelect);
        }
    }
}
