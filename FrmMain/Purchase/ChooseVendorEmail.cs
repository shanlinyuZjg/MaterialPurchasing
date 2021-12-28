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
    public partial class ChooseVendorEmail : Office2007Form
    {
        string VendorID = string.Empty;
        public ChooseVendorEmail(string id)
        {
            this.EnableGlass = false;
            MessageBoxEx.EnableGlass = false;
            VendorID = id;
            InitializeComponent();
        }

        private void ChooseVendorEmail_Load(object sender, EventArgs e)
        {
            string sqlSelect = @"Select VendorName AS 供应商名, Email AS 邮箱 From PurchaseDepartmentVendorEmailByCMF Where VendorNumber = '" +  VendorID+ "'";
            dgvEmail.DataSource = SQLHelper.GetDataTable(GlobalSpace.FSDBConnstr, sqlSelect);
        }

        private void dgvEmail_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.RowIndex < 0 )
            {
                Custom.MsgEx("请点击有效范围！");
            }
            else
            {
                GlobalSpace.vendorEmailList.Add(dgvEmail.CurrentRow.Cells["供应商名"].Value.ToString());
                GlobalSpace.vendorEmailList.Add(dgvEmail.CurrentRow.Cells["邮箱"].Value.ToString());
            }
        }

        private void dgvEmail_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            dgvEmail_CellDoubleClick(sender, e);
        }
    }
}
