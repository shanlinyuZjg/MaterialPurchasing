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

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (tbPO.Text != "")
            {
                tbMN.Text = "";
                tbMO.Text = "";
                string sql = @"Select ManufacturerNumber,ManufacturerName From PurchaseOrderRecordByCMF Where PONumber=@PONumber";
                SqlParameter[] sqlparam = { new SqlParameter("@PONumber", tbPO.Text) };

                DataTable dtTemp = SQLHelper.GetDataSet(GlobalSpace.FSDBConnstr, sql, sqlparam).Tables[0];
                if (dtTemp.Rows.Count > 0)
                {
                    tbMN.Text = dtTemp.Rows[0]["ManufacturerName"].ToString();
                    tbMO.Text = dtTemp.Rows[0]["ManufacturerNumber"].ToString();
                }
                else
                {
                    MessageBoxEx.Show("未查到相关记录！","提示");
                }
            }
            else
            {
                MessageBoxEx.Show("订单号不能为空！","提示");
            }
        }

        private void tbPO_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar ==(char)13)
            {
                if (tbPO.Text != "")
                {
                    tbMN.Text = "";
                    tbMO.Text = "";
                    string sql = @"Select ManufacturerNumber,ManufacturerName From PurchaseOrderRecordByCMF Where PONumber=@PONumber";
                    SqlParameter[] sqlparam = { new SqlParameter("@PONumber", tbPO.Text) };

                    DataTable dtTemp = SQLHelper.GetDataSet(GlobalSpace.FSDBConnstr, sql, sqlparam).Tables[0];
                    if (dtTemp.Rows.Count > 0)
                    {
                        tbMN.Text = dtTemp.Rows[0]["ManufacturerName"].ToString();
                        tbMO.Text = dtTemp.Rows[0]["ManufacturerNumber"].ToString();
                    }
                    else
                    {
                        MessageBoxEx.Show("未查到相关记录！", "提示");
                    }
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
