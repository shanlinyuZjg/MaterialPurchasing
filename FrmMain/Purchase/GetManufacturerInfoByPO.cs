using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using System.Data.SqlClient;
using Global.Helper;

namespace Global.Purchase
{
    public partial class GetManufacturerInfoByPO : Office2007Form
    {
        public GetManufacturerInfoByPO()
        {
            this.EnableGlass = false;
            MessageBoxEx.EnableGlass = false;
            InitializeComponent();
        }

      

        private void tbPO_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar ==(char)13)
            {
                if (tbPO.Text != "")
                {
                    string sql = @"Select LineNumber AS 行号,ItemNumber AS 物料代码,ItemDescription AS 物料描述,POItemQuantity AS 数量,UnitPrice AS 单价,VendorNumber AS 供应商码,VendorName AS 供应商名,ManufacturerNumber AS 生产商码,ManufacturerName AS 生产商名 From PurchaseOrderRecordByCMF Where PONumber='" + tbPO.Text + "' And IsPurePO = 0";
                    dgv.DataSource = SQLHelper.GetDataTable(GlobalSpace.FSDBConnstr, sql);
                }
                else
                {
                    MessageBoxEx.Show("订单号不能为空！", "提示");
                }
            }
        }

        private void tbPO_TextChanged(object sender, EventArgs e)
        {
            tbPO.Text = tbPO.Text.ToUpper();
            tbPO.SelectionStart = tbPO.Text.Length;
        }
    }
}
