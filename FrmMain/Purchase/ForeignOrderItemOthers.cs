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


namespace Global.Purchase
{
    public partial class ForeignOrderItemOthers : Office2007Form
    {
        public ForeignOrderItemOthers()
        {

            this.EnableGlass = false;
            MessageBoxEx.EnableGlass = false;
            InitializeComponent();
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
             if(dgvDetail.SelectedRows.Count > 0)
            {                
                for(int i = 0;i < dgvDetail.SelectedRows.Count;i++)
                {
                    Others others = new Others();
                    others.vendorNumber = dgvDetail.SelectedRows[i].Cells["供应商码"].Value.ToString();
                    others.vendorName = dgvDetail.SelectedRows[i].Cells["供应商名"].Value.ToString();
                    GlobalSpace.othersList.Add(others);
                }
            }
             else
            {
                Custom.MsgEx("没有选中的行！");
            }
        }

        private void rbtnAluminumFoil_CheckedChanged(object sender, EventArgs e)
        {
            dgvDetail.DataSource = GetVendorInfo("铝箔");
        }

        private void rbtnPlasticBoard_CheckedChanged(object sender, EventArgs e)
        {
            dgvDetail.DataSource = GetVendorInfo("塑料托");
        }

        private void rbtnCap_CheckedChanged(object sender, EventArgs e)
        {
            dgvDetail.DataSource = GetVendorInfo("铝塑盖");
        }

        private DataTable GetVendorInfo(string type)
        {
            string sqlSelect = @"SELECT
	                                            VendorNumber AS 供应商码,
	                                            VendorName AS 供应商名,
                                                 Price AS 价格
                                            FROM
	                                            PurchaseDepartmentForeignOrderPackageOthersByCMF
                                            WHERE
	                                            ItemType = '"+type+"'";
            return SQLHelper.GetDataTable(GlobalSpace.FSDBConnstr, sqlSelect);
        }
    }
}
