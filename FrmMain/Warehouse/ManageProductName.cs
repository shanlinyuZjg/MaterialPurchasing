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

namespace Global.Warehouse
{
    public partial class ManageProductName : Office2007Form
    {
        public ManageProductName()
        {
            InitializeComponent();
            this.EnableGlass = false;
            MessageBoxEx.EnableGlass = false;
        }

        private void tbItemNumber_TextChanged(object sender, EventArgs e)
        {
            tbItemNumber.Text = tbItemNumber.Text.ToUpper();
            tbItemNumber.SelectionStart = tbItemNumber.Text.Length;
        }

        private void tbItemNumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar == (char)13)
            {
                if(!string.IsNullOrEmpty(tbItemNumber.Text))
                {
                    DataTable dt = GetItemInfo(tbItemNumber.Text.Trim());
                    if(dt.Rows.Count > 0)
                    {
                        dgvDetail.DataSource = dt;
                    }
                    else
                    {
                        MessageBoxEx.Show("未查到该物料信息，请先添加！", "提示");
                    }

                    //显示物料描述
                    string sqlSelect = @"select ItemDescription from FSDBMR.dbo._NoLock_FS_Item where ItemNumber='"+tbItemNumber.Text.Trim()+"'";
                    DataTable dtDesc = SQLHelper.GetDataTableOleDb(GlobalSpace.oledbconnstrFSDBMR, sqlSelect);
                    if(dtDesc.Rows.Count > 0)
                    {
                        tbItemDescription.Text = dtDesc.Rows[0]["ItemDescription"].ToString();
                    }
                    else
                    {
                        MessageBoxEx.Show("四班里未查到该物料信息，请先添加！", "提示");
                    }
                }
            }
        }

        private DataTable GetItemInfo(string itemNumber)
        {
            string sqlSelect = @"Select ItemNumber AS 物料代码,ProductName AS 品名 From PurchaseDepartmentStockProductName Where ItemNumber='"+itemNumber+"'";
            return SQLHelper.GetDataTable(GlobalSpace.FSDBConnstr, sqlSelect);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if(!string.IsNullOrWhiteSpace(tbItemNumber.Text) && !string.IsNullOrWhiteSpace(tbItemDescription.Text) && !string.IsNullOrWhiteSpace(tbProductName.Text))
            {
                string sqlCheck = @"Select Count(Id) From PurchaseDepartmentStockProductName Where ItemNumber='"+tbItemNumber.Text+"'";
                string sqlInsert = @"Insert Into PurchaseDepartmentStockProductName  (ItemNumber,ProductName) values('"+tbItemNumber.Text+"','"+tbProductName.Text+"')";

                if(SQLHelper.Exist(GlobalSpace.FSDBConnstr,sqlCheck))
                {
                    MessageBoxEx.Show("已有该物料信息，不能重复添加！", "提示");
                }
                else
                {
                    if(SQLHelper.ExecuteNonQuery(GlobalSpace.FSDBConnstr,sqlInsert))
                    {
                        MessageBoxEx.Show("添加成功！", "提示");
                        dgvDetail.DataSource = GetItemInfo(tbItemNumber.Text.Trim());
                    }
                    else
                    {
                        MessageBoxEx.Show("添加失败！", "提示");
                    }
                }
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            string sqlCheck = @"Select Count(Id) From PurchaseDepartmentStockProductName Where ItemNumber='" + tbItemNumber.Text + "'";
            string sqlUpdate = @"Update PurchaseDepartmentStockProductName Set ProductName='"+tbProductName.Text+"' where ItemNumber='"+ tbItemNumber.Text + "'";
            if (SQLHelper.Exist(GlobalSpace.FSDBConnstr, sqlCheck))
            {             
                if (SQLHelper.ExecuteNonQuery(GlobalSpace.FSDBConnstr, sqlUpdate))
                {
                    MessageBoxEx.Show("更新成功！", "提示");
                    dgvDetail.DataSource = GetItemInfo(tbItemNumber.Text.Trim());
                }
                else
                {
                    MessageBoxEx.Show("更新失败！", "提示");
                }
            }
            else
            {

                MessageBoxEx.Show("该物料信息不存在，无法修改！", "提示");
            }

        }

        private void dgvDetail_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            dgvDetail_CellClick(sender, e);
        }

        private void dgvDetail_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            tbProductName.Text = dgvDetail.Rows[dgvDetail.CurrentCell.RowIndex].Cells["品名"].Value.ToString();
        }

        private void tbItemNumber_Click(object sender, EventArgs e)
        {
            tbItemNumber.Text = "";
            tbItemNumber.ForeColor = Color.Black;
        }
    }
}
