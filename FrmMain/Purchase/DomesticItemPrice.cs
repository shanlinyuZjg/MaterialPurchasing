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

namespace Global.Purchase
{
    public partial class DomesticItemPrice : Office2007Form
    {
        string ItemNumber = string.Empty;
        string VendorNumber = string.Empty;
        string Type = string.Empty;
        public DomesticItemPrice(string itemNumber,string vendorNumber,string type)
        {
            this.EnableGlass = false;
            MessageBoxEx.EnableGlass = false;
            ItemNumber = itemNumber;
            VendorNumber = vendorNumber;
            InitializeComponent();
            Type = type;
        }

        private void DomesticItemPrice_Load(object sender, EventArgs e)
        {
            string sqlSelectItemPrice = @"SELECT
	                                                    ItemNumber AS 物料代码,
	                                                    ItemDescription AS 物料描述,
	                                                    VendorNumber AS 供应商代码,
	                                                    VendorName AS 供应商名称,
	                                                    PricePreTax AS 含税价格
                                                    FROM
	                                                    PurchaseDepartmentDomesticProductItemPrice
                                                    WHERE
	                                                    ItemNumber = '" + ItemNumber + "' And VendorNumber = '" + VendorNumber + "'";

            dgv.DataSource = SQLHelper.GetDataTable(GlobalSpace.FSDBConnstr, sqlSelectItemPrice);
            
        }

        private void dgv_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            dgv_CellDoubleClick(sender, e);
        }

        private void dgv_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.RowIndex >= 0)
            {
                if(Type == "P")
                {
                    GlobalSpace.ItemPrice = Convert.ToDouble(dgv.Rows[e.RowIndex].Cells["含税价格"].Value);

                }
                else
                {
                    GlobalSpace.VialPrice = Convert.ToDouble(dgv.Rows[e.RowIndex].Cells["含税价格"].Value);
                }
                this.Close();
            }

        }
    }
}
