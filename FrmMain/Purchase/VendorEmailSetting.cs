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
using System.Data.SqlClient;

namespace Global.Purchase
{
    public partial class VendorEmailSetting : Office2007Form
    {
        string userID = string.Empty;
        public VendorEmailSetting()
        {
            InitializeComponent();
        }

        public VendorEmailSetting(string id)
        {
            this.EnableGlass = false;
            MessageBoxEx.EnableGlass = false;
            userID = id;
            InitializeComponent();
        }

        private void VendorEmailSetting_Load(object sender, EventArgs e)
        {
            tbVendorName.Focus();
        }

        private void LoadVendorEmail()
        {
            string sqlSelect = @"Select VendorNumber AS 供应商码,VendorName AS 名称,Email AS 邮箱 From PurchaseDepartmentVendorEmailByCMF";
            dgvVendorEmail.DataSource = SQLHelper.GetDataTable(GlobalSpace.FSDBConnstr, sqlSelect);
        }

        private bool IsExist(string vendorid)
        {
            string sqlSelect = @"Select Count(Id) From PurchaseDepartmentVendorEmailByCMF Where VendorNumber='"+vendorid+"'";
            if(SQLHelper.Exist(GlobalSpace.FSDBConnstr, sqlSelect))
            {
                return true;
            }
            return false;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if(IsExist(tbVendorNumber.Text.Trim()))
            {
                MessageBoxEx.Show("该供应商邮箱已存在！", "提示");
                string sqlSelect = @"Select VendorNumber AS 供应商码,VendorName AS 名称,Email AS 邮箱 From PurchaseDepartmentVendorEmailByCMF Where VendorNumber='"+tbVendorNumber.Text.Trim()+"'";
                dgvVendorEmail.DataSource = SQLHelper.GetDataTable(GlobalSpace.FSDBConnstr, sqlSelect);
            }
            else
            {
                if(tbVendorNumber.Text !="" && tbVendorName.Text !="" && tbEmail.Text !="")
                {
                    string sqlInsert = @"Insert Into PurchaseDepartmentVendorEmailByCMF (VendorNumber,VendorName,Email) Values(@VendorNumber,@VendorName,@Email)";
                    SqlParameter[] sqlparams =
                    {
                    new SqlParameter("@VendorNumber",tbVendorNumber.Text.Trim()),
                    new SqlParameter("@VendorName",tbVendorName.Text.Trim()),
                    new SqlParameter("@Email",tbEmail.Text.Trim())
                };
                    if (SQLHelper.ExecuteNonQuery(GlobalSpace.FSDBConnstr, sqlInsert, sqlparams) )
                    {
                        MessageBoxEx.Show("增加成功！", "提示");
                        tbVendorName.Text = "";
                        tbVendorNumber.Text = "";
                        tbEmail.Text = "";
                        LoadVendorEmail();
                    }
                    else
                    {
                        MessageBoxEx.Show("增加失败！", "提示");
                    }
                }
                else
                {
                    MessageBoxEx.Show("供应商代码，名称和邮箱均不能为空！", "提示");
                }

            }
        }

        private void dgvVendorEmail_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            dgvVendorEmail_CellDoubleClick(sender, e);
        }

        private void dgvVendorEmail_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.RowIndex < 0)
            {
                MessageBoxEx.Show("请点击有效区域！", "提示");
            }
            else
            {
                tbVendorNumber.Text = dgvVendorEmail.Rows[e.RowIndex].Cells["供应商码"].Value.ToString();
                tbVendorNumber.Tag = dgvVendorEmail.Rows[e.RowIndex].Cells["供应商码"].Value.ToString();
                tbVendorName.Text = dgvVendorEmail.Rows[e.RowIndex].Cells["名称"].Value.ToString();
                tbEmail.Text = dgvVendorEmail.Rows[e.RowIndex].Cells["邮箱"].Value.ToString();
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if(tbVendorNumber.Tag.ToString() =="")
            {
                MessageBoxEx.Show("该供应商信息不是从下边列表中获得，为确保准确性不能直接进行修改！", "提示");
            }
            else
            {
                if(tbEmail.Text.Trim() !="")
                {
                    string sqlUpdate = @"Update PurchaseDepartmentVendorEmailByCMF Set Email='" + tbEmail.Text.Trim() + "' Where VendorNumber='" + tbVendorNumber.Tag.ToString() + "'";
                    if(SQLHelper.ExecuteNonQuery(GlobalSpace.FSDBConnstr, sqlUpdate) )
                    {
                        MessageBoxEx.Show("更新成功！", "提示");
                        tbVendorNumber.Text = "";
                        tbEmail.Text = "";
                        tbVendorName.Text = "";
                        LoadVendorEmail();
                    }
                    else
                    {
                        MessageBoxEx.Show("更新失败！", "提示");
                    }
                }
                else
                {
                    MessageBoxEx.Show("邮箱不能为空！", "提示");
                }

            }
        }

        private void tbVendorNumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar == (char)13)
            {
                if (tbVendorNumber.Text.Trim() != "")
                {
                    string vendorname = CommonOperate.GetVendorInfo(tbVendorNumber.Text.Trim());
                    if (vendorname == "")
                    {
                        MessageBoxEx.Show("未查到该供应商代码的信息！", "提示");
                    }
                    else
                    {
                        tbVendorName.Text = vendorname;
                        tbEmail.Focus();
                    }
                }
                else
                {
                    MessageBoxEx.Show("供应商代码不能为空！", "提示");
                }
            }

        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string sqlSelect = @"Select VendorNumber AS 供应商码,VendorName AS 名称,Email AS 邮箱 From PurchaseDepartmentVendorEmailByCMF Where VendorNumber='" + tbVendorNumber.Text.Trim() + "'";
            dgvVendorEmail.DataSource = SQLHelper.GetDataTable(GlobalSpace.FSDBConnstr, sqlSelect);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {

        }
    }
}
