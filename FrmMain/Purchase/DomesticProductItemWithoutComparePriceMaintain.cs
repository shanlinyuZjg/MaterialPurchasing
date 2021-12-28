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
    public partial class DomesticProductItemWithoutComparePriceMaintain : Office2007Form
    {
        public DomesticProductItemWithoutComparePriceMaintain()
        {
            InitializeComponent();
            this.EnableGlass = false;
            MessageBoxEx.EnableGlass = false;
        }

        private void DomesticProductItemWithoutComparePriceMaintain_Load(object sender, EventArgs e)
        {
            dgvDetail.DataSource = GetDataTable(0, "");
        }

        private DataTable GetDataTable(int status, string itemNumber)
        {
            string sqlSelect = @"Select ItemNumber AS 物料代码,ItemDescription AS 描述  From PurchaseDepartmentNotComparePrice";
            string sqlCriteria = string.Empty;
            if (status == 0)
            {
                sqlCriteria = " Where  1 =1 ";
            }
            else if (status == 1)
            {
                sqlCriteria = " Where ItemNumber = '" + itemNumber + "'";
            }

            return SQLHelper.GetDataTable(GlobalSpace.FSDBConnstr, sqlSelect + sqlCriteria);
        }

        private void tbItemNumber_TextChanged(object sender, EventArgs e)
        {
            tbItemNumber.Text = tbItemNumber.Text.ToUpper();
            tbItemNumber.SelectionStart = tbItemNumber.Text.Length;
        }

        private void tbItemNumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                if (!string.IsNullOrEmpty(tbItemNumber.Text))
                {
                    string sqlCheckExist = @"Select Count(Id) From PurchaseDepartmentNotComparePrice Where ItemNumber = '" + tbItemNumber.Text + "'";
                    string sqlSelect = @"Select ItemNumber,ItemDescription From  _NoLock_FS_Item Where ItemNumber='" + tbItemNumber.Text + "'";
                    if (SQLHelper.Exist(GlobalSpace.FSDBConnstr, sqlCheckExist))
                    {
                        dgvDetail.DataSource = GetDataTable(1, tbItemNumber.Text);
                    }
                    else
                    {
                        DataTable dt = SQLHelper.GetDataTableOleDb(GlobalSpace.oledbconnstrFSDBMR, sqlSelect);
                        if (dt.Rows.Count > 0)
                        {
                            tbItemDescription.Text = dt.Rows[0]["ItemDescription"].ToString();
                        }
                        else
                        {
                            tbItemDescription.Text = "";
                            Custom.MsgEx("四班中没有此物料代码，请再次确认！");
                            return;
                        }
                    }
                }
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(tbItemDescription.Text))
            {
                string sqlCheckExist = @"Select Count(Id) From PurchaseDepartmentNotComparePrice Where ItemNumber = '" + tbItemNumber.Text + "'";
                if (!SQLHelper.Exist(GlobalSpace.FSDBConnstr, sqlCheckExist))
                {
                    string sqlInsert = @"Insert Into PurchaseDepartmentNotComparePrice (ItemNumber,ItemDescription) Values ('" + tbItemNumber.Text + "','" + tbItemDescription.Text + "')";
                    if (SQLHelper.ExecuteNonQuery(GlobalSpace.FSDBConnstr, sqlInsert))
                    {
                        Custom.MsgEx("增加成功！");
                        dgvDetail.DataSource = GetDataTable(0, "");
                    }
                    else
                    {
                        Custom.MsgEx("增加失败！");
                    }
                }
                else
                {
                    Custom.MsgEx("已存在该物料的信息！");
                }
            }
        }
    }
}
