using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;


namespace Global.Purchase
{
    public partial class DomesticProductItemPriceChoose : Office2007Form
    {
        DataTable dtItemPrice = null;
        public DomesticProductItemPriceChoose(DataTable dt)
        {
            InitializeComponent();
            dtItemPrice = dt;
            this.EnableGlass = false;
            MessageBoxEx.EnableGlass = false;
        }

        private void DomesticProductItemPriceChoose_Load(object sender, EventArgs e)
        {
            dgv.DataSource = dtItemPrice;
        }

        private void dgv_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            dgv_CellDoubleClick(sender, e);
        }

        private void dgv_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            /*      GlobalSpace.DomesticItemPrice.Add(dgv.Rows[e.RowIndex].Cells["物料代码"].Value.ToString());
                  GlobalSpace.DomesticItemPrice.Add(dgv.Rows[e.RowIndex].Cells["物料描述"].Value.ToString());
                  GlobalSpace.DomesticItemPrice.Add(dgv.Rows[e.RowIndex].Cells["供应商代码"].Value.ToString());
                  GlobalSpace.DomesticItemPrice.Add(dgv.Rows[e.RowIndex].Cells["供应商名称"].Value.ToString());*/
    //        GlobalSpace.DomesticItemPriceList.Add("1.0001");

    //        GlobalSpace.DomesticItemPriceList.Add(dgv.Rows[e.RowIndex].Cells["含税价格"].Value.ToString());

            this.Close();
        }
    }
}
