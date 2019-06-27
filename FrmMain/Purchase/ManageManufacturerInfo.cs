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
    public partial class ManageManufacturerInfo : Office2007Form
    {
        string userID = string.Empty;
        string userName = string.Empty;
        public ManageManufacturerInfo(string id,string name)
        {
            this.EnableGlass = false;
            MessageBoxEx.EnableGlass = false;
            userID = id;
            userName = name;
            InitializeComponent();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string sqlSelect= @"SELECT
                                Id,
                                ItemNumber AS 物料代码,
	                            VendorNumber AS 供应商代码,
	                            VendorName AS 供应商,
	                            ManufacturerNumber AS 生产商代码,
	                            ManufacturerName AS 生产商,
                                Operator as 操作人
                            FROM
                                PurchaseDepartmentItemManufacturerInfoByCMF
                            WHERE
                                ItemNumber = '" + tbItemNumber.Text.ToString() + "' and Status = 0";
            dgvManufacturerInfo.DataSource = SQLHelper.GetDataTable(GlobalSpace.FSDBConnstr, sqlSelect);
            dgvManufacturerInfo.Columns[0].Visible = false;

            tbVendorName.Text = "";
            tbVendorNumber.Text = "";
            tbManufacturerName.Text = "";
            tbManufacturerNumber.Text = "";
        }

        private void tbItemNumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                if (string.IsNullOrEmpty(tbItemNumber.Text.ToString()))
                {
                    MessageBoxEx.Show("查询值不能为空！","提示");
                    return;
                }
                else
                {
                    btnSearch_Click(sender, e);
                }

            }
        }

        private void tbVendorNumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                if(tbVendorNumber.Text != "" )
                {
                    string sqlSelect = @"SELECT
                            Id,
	                        ItemNumber AS 物料代码,
	                        VendorNumber AS 供应商代码,
	                        VendorName AS 供应商,
	                        ManufacturerNumber AS 生产商代码,
	                        ManufacturerName AS 生产商
                        FROM
	                        PurchaseDepartmentItemManufacturerInfoByCMF Where VendorNumber = '"+tbVendorNumber.Text.Trim()+"'";
                    dgvManufacturerInfo.DataSource = SQLHelper.GetDataTable(GlobalSpace.FSDBConnstr, sqlSelect);
                    dgvManufacturerInfo.Columns[0].Visible = false;
                    tbItemNumber.Text = "";
                    tbVendorName.Text = "";
                    tbManufacturerName.Text = "";
                    tbManufacturerNumber.Text = "";
                }
                else
                {
                    MessageBoxEx.Show("供应商代码不能为空！", "提示");
                }                
            }
        }

        private void dgvManufacturerInfo_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            dgvManufacturerInfo_CellDoubleClick(sender, e);
        }

        private void dgvManufacturerInfo_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                MessageBox.Show("请选择有效的行！", "提示");
            }
            else
            {
                tbItemNumber.Text = dgvManufacturerInfo["物料代码", e.RowIndex].Value.ToString();
                tbItemNumber.Tag = dgvManufacturerInfo["Id", e.RowIndex].Value.ToString();
                tbVendorNumber.Text = dgvManufacturerInfo["供应商代码", e.RowIndex].Value.ToString();
                tbVendorName.Text = dgvManufacturerInfo["供应商", e.RowIndex].Value.ToString();
                tbManufacturerNumber.Text = dgvManufacturerInfo["生产商代码", e.RowIndex].Value.ToString();
                tbManufacturerName.Text = dgvManufacturerInfo["生产商", e.RowIndex].Value.ToString();
            }
        }

        private void tbItemNumber_TextChanged(object sender, EventArgs e)
        {
            tbItemNumber.Text = tbItemNumber.Text.ToUpper();
            tbItemNumber.SelectionStart = tbItemNumber.Text.Length;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (tbItemNumber.Text == "" || tbVendorName.Text == "" || tbVendorNumber.Text == "" || tbManufacturerName.Text == "" || tbManufacturerNumber.Text == "")
            {
                MessageBoxEx.Show("不能有空项！","提示");
            }
            else
            {
                string sqlCheck = @"Select count(Id) from ItemManufacturerInfoByCMF Where ItemNumber='"+tbItemNumber.Text.Trim()+"' and VendorNumber='"+tbVendorNumber.Text.Trim()+"' and ManufacturerNumber='"+tbManufacturerNumber.Text.Trim()+"'";
                if(SQLHelper.Exist(GlobalSpace.FSDBConnstr, sqlCheck))
                {
                    MessageBoxEx.Show("该物料的生产商和供应商信息已存在，请先查找确认！", "提示");
                }
                else
                {
                    string sqlInsert = @"INSERT INTO ItemManufacturerInfoByCMF (
	                            VendorNumber,
	                            VendorName,
	                            ManufacturerNumber,
	                            ManufacturerName,
	                            ItemNumber,
                                Operator
                          )
                            VALUES
	                            (	                            @VendorNumber ,@VendorName ,@ManufacturerNumber ,@ManufacturerName ,@ItemNumber,
                                    @Operator
	                            )";
                    SqlParameter[] sqlparams =
                    {
                        new SqlParameter("@VendorNumber",tbVendorNumber.Text.ToString()),
                        new SqlParameter("@VendorName",tbVendorName.Text.ToString()),
                        new SqlParameter("@ManufacturerNumber",tbManufacturerNumber.Text.ToString()),
                        new SqlParameter("@ManufacturerName",tbManufacturerName.Text.ToString()),
                        new SqlParameter("@ItemNumber",tbItemNumber.Text.ToString()),
                        new SqlParameter("@Operator",userID)

                    };

                    if (SQLHelper.ExecuteNonQuery(GlobalSpace.FSDBConnstr, sqlInsert, sqlparams))
                    {
                        MessageBoxEx.Show("信息添加成功！", "提示");
                        tbItemNumber.Text = "";
                        tbVendorName.Text = "";
                        tbVendorNumber.Text = "";
                        tbManufacturerName.Text = "";
                        tbManufacturerNumber.Text = "";
                    }
                    else
                    {
                        MessageBoxEx.Show("信息添加失败！", "提示");
                    }
                }

            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (tbItemNumber.Text == "" || tbVendorName.Text == "" || tbVendorNumber.Text == "" || tbManufacturerName.Text == "" || tbManufacturerNumber.Text == "")
            {
                MessageBoxEx.Show("不能有空项！", "提示");
            }
            else
            {
                string sqlUpdate = @"Update PurchaseDepartmentItemManufacturerInfoByCMF Set ItemNumber='"+tbItemNumber.Text.Trim()+"',VendorNumber='"+tbVendorNumber.Text.Trim()+"',VendorName='"+tbVendorName.Text+"',ManufacturerNumber='"+tbManufacturerNumber.Text.Trim()+"',ManufacturerName='"+tbManufacturerName.Text+"',Operator='"+userID+"' Where Id='"+tbItemNumber.Tag+"'";
                if(SQLHelper.ExecuteNonQuery(GlobalSpace.FSDBConnstr, sqlUpdate) )
                {
                    MessageBoxEx.Show("修改成功！", "提示");
                    tbItemNumber.Text = "";
                    tbVendorName.Text = "";
                    tbVendorNumber.Text = "";
                    tbManufacturerName.Text = "";
                    tbManufacturerNumber.Text = "";
                }
                else
                {
                    MessageBoxEx.Show("修改失败！", "提示");
                }
            }
        }
    }
}
