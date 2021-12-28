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
using Global;


namespace Global.Purchase
{
    public partial class ManageItemPrice : Office2007Form
    {
        string UserID = string.Empty;
        public ManageItemPrice(string id)
        {
            UserID = id;
            InitializeComponent();
            this.EnableGlass = false;
            MessageBoxEx.EnableGlass = false;
        }

        private void tbItemNumber_TextChanged(object sender, EventArgs e)
        {
            tbItemNumber.Text = tbItemNumber.Text.ToUpper();
            tbItemNumber.SelectionStart = tbItemNumber.MaxLength;
        }

        private void tbItemNumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(CommonOperate.TextBoxCheck(tbItemNumber,e))
            {                
                string sqlSelectItemInfo = @"Select  ItemDescription From _NoLock_FS_Item Where ItemNumber='" + tbItemNumber.Text + "'";
                DataTable dtTemp = SQLHelper.GetDataTableOleDb(GlobalSpace.oledbconnstrFSDBMR, sqlSelectItemInfo);
                if(dtTemp.Rows.Count > 0)
                {
                    tbItemDescription.Text = dtTemp.Rows[0]["ItemDescription"].ToString();
                }

                string sqlSelect = @"SELECT
                                                Id,
                                                ItemNumber AS 物料代码,
                                                ItemDescription AS 物料描述,
                                                VendorNumber AS 供应商码,
                                                VendorName AS 供应商名,
                                                PricePreTax AS 含税价格
                                                FROM
                                                dbo.PurchaseDepartmentDomesticProductItemPrice Where ItemNumber = '" + tbItemNumber.Text + "'";
                dgv.DataSource = SQLHelper.GetDataTable(GlobalSpace.FSDBConnstr, sqlSelect);
                dgv.Columns["Id"].Visible = false;
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if(!string.IsNullOrEmpty(tbItemNumber.Text))
            {
                if(string.IsNullOrEmpty(tbItemDescription.Text) || string.IsNullOrEmpty(tbVendorName.Text)|| string.IsNullOrEmpty(tbPricePreTax.Text) || string.IsNullOrEmpty(tbVendorNumber.Text))
                {
                    Custom.MsgEx("必填项不得为空！");
                    return;
                }
                string sqlInsert = @"Insert Into PurchaseDepartmentDomesticProductItemPrice ([ItemNumber],
                                                                 [ItemDescription],
                                                                 [VendorNumber],
                                                                 [VendorName],
                                                                 [PricePreTax],
                                                                 [Operator]) Values ('" + tbItemNumber.Text+"','"+tbItemDescription.Text+"','"+tbVendorNumber.Text+"','"+tbVendorName.Text+"',"+Convert.ToDecimal(tbPricePreTax.Text)+",'"+UserID+"')";
                if (SQLHelper.ExecuteNonQuery(GlobalSpace.FSDBConnstr, sqlInsert))
                {
                    Custom.MsgEx("增加成功！");
                    tbItemNumber.Text = "";
                    tbItemDescription.Text = "";
                    tbPricePreTax.Text = "";
                    tbVendorName.Text = "";
                    tbVendorNumber.Text = "";
                }
                else
                {
                    Custom.MsgEx("增加失败！");
                }
            }
        }

        private void btnModify_Click(object sender, EventArgs e)
        {
            Custom.MsgEx("修改时只修改改行物料含税价格~！");
            if(dgv.SelectedRows.Count == 1)
            {
                string id = dgv.SelectedRows[0].Cells["Id"].Value.ToString();
                double price = Convert.ToDouble(dgv.SelectedRows[0].Cells["含税价格"].Value);
                string sqlUpdate = @"Update PurchaseDepartmentDomesticProductItemPrice Set PricePreTax ="+price+ ", OperateDateTime='"+DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss")+"',Operator='"+UserID+"' Where Id = " + Convert.ToInt32(id)+"";
                if (SQLHelper.ExecuteNonQuery(GlobalSpace.FSDBConnstr, sqlUpdate))
                {
                    Custom.MsgEx("修改成功！");
                }
                else
                {
                    Custom.MsgEx("修改失败！");
                }
            }
            else
            {
                Custom.MsgEx("当前无选中行或选中了多行！");
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgv.SelectedRows.Count == 1)
            {
                string id = dgv.SelectedRows[0].Cells["Id"].Value.ToString();
                string sqlDelete = @"Delete From  dbo.PurchaseDepartmentDomesticProductItemPrice Where Id = "+Convert.ToInt32(id)+"";
                if(SQLHelper.ExecuteNonQuery(GlobalSpace.FSDBConnstr,sqlDelete))
                {
                    Custom.MsgEx("删除成功！");
                }
                else
                {
                    Custom.MsgEx("删除失败！");
                }
            }
            else
            {
                Custom.MsgEx("当前无选中行或选中了多行！");
            }
        }

        private void tbVendorNumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!string.IsNullOrEmpty(tbVendorNumber.Text))
            {
                tbVendorName.Text = GetVendorName(tbVendorNumber.Text);
            }
        }

        //获取供应商名字
        private string GetVendorName(string vendorId)
        {
            string strTemp = "";
            string strSelect = @"Select VendorName From _NoLock_FS_Vendor Where VendorID='" + vendorId + "'";
            try
            {
                DataTable dtTemp = SQLHelper.GetDataTableOleDb(GlobalSpace.oledbconnstrFSDBMR, strSelect);
                if (dtTemp.Rows.Count > 0)
                {
                    strTemp = dtTemp.Rows[0]["VendorName"].ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBoxEx.Show("异常：" + ex.Message);
            }

            return strTemp;
        }

        private void ManageItemPrice_Load(object sender, EventArgs e)
        {

        }
    }
}
