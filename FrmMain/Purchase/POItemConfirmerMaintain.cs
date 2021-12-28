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
    public partial class POItemConfirmerMaintain : Office2007Form
    {
        public POItemConfirmerMaintain()
        {
            this.EnableGlass = false;
            MessageBoxEx.EnableGlass = false;
            InitializeComponent();
            PurchaseUser.Group = "M";
        }

        private void POItemConfirmerMaintain_Load(object sender, EventArgs e)
        {
            dgvDetail.DataSource = GetDataTable(0, "");

            DataTable dtConfirmType = GetPOItemConfirmList(PurchaseUser.Group);
            BindingSource bs = new BindingSource();
            bs.DataSource = dtConfirmType.Rows.Cast<DataRow>().ToDictionary(r => r["UserID"].ToString(), r => r["Name"].ToString());
            cbbConfirmPerson.DataSource = bs;
            cbbConfirmPerson.DisplayMember = "Value";
            cbbConfirmPerson.ValueMember = "Key";
            cbbConfirmPerson.SelectedIndex = -1;
        }

        private DataTable GetPOItemConfirmList(string confirmType)
        {
            string sqlSelect = @"Select UserID,(UserID+'|'+Name) AS Name From PurchaseDepartmentRBACByCMF Where ConfirmGroup = '" + confirmType + "'";
            return SQLHelper.GetDataTable(GlobalSpace.FSDBConnstr, sqlSelect);
        }

        private DataTable GetDataTable(int status,string itemNumber)
        {
            string sqlSelect = @"Select ItemNumber AS 物料代码,ItemDescription AS 描述,Confirmer AS 确认人  From PurchaseDepartmentPOItemConfirmer";
            string sqlCriteria = string.Empty;
            if (status == 0)
            {
                sqlCriteria = " Where  1 =1 ";
            }
            else if(status == 1)
            {
                sqlCriteria = " Where ItemNumber = '"+itemNumber+"'";
            }
         
            return SQLHelper.GetDataTable(GlobalSpace.FSDBConnstr, sqlSelect+sqlCriteria);
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
                    string sqlCheckExist = @"Select Count(Id) From PurchaseDepartmentPOItemConfirmer Where ItemNumber = '"+tbItemNumber.Text+"'";
                    string sqlSelect = @"Select ItemNumber,ItemDescription From  _NoLock_FS_Item Where ItemNumber='"+tbItemNumber.Text+"'";
                    if(SQLHelper.Exist(GlobalSpace.FSDBConnstr,sqlCheckExist))
                    {
                        dgvDetail.DataSource = GetDataTable(1, tbItemNumber.Text);
                    }
                    else
                    {
                        DataTable dt = SQLHelper.GetDataTableOleDb(GlobalSpace.oledbconnstrFSDBMR, sqlSelect);
                        if(dt.Rows.Count > 0)
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
            if(!string.IsNullOrEmpty(cbbConfirmPerson.Text) && !string.IsNullOrEmpty(tbItemDescription.Text))
            {
                string sqlCheckExist = @"Select Count(Id) From PurchaseDepartmentPOItemConfirmer Where ItemNumber = '" + tbItemNumber.Text + "'";
                if(!SQLHelper.Exist(GlobalSpace.FSDBConnstr,sqlCheckExist))
                {
                    string sqlInsert = @"Insert Into PurchaseDepartmentPOItemConfirmer (ItemNumber,ItemDescription,Confirmer,Type) Values ('"+tbItemNumber.Text+"','"+tbItemDescription.Text+"','"+cbbConfirmPerson.Text.Split('|')[0]+"','"+PurchaseUser.Group+"')";
                    if(SQLHelper.ExecuteNonQuery(GlobalSpace.FSDBConnstr,sqlInsert))
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

        private void btnDelete_Click(object sender, EventArgs e)
        {
            string itemNumber = dgvDetail.Rows[dgvDetail.CurrentCell.RowIndex].Cells["物料代码"].Value.ToString();
            string sqlDelete = @"Delete From PurchaseDepartmentPOItemConfirmer Where ItemNumber='"+itemNumber+"'";
            
            if (SQLHelper.ExecuteNonQuery(GlobalSpace.FSDBConnstr,sqlDelete))
            {
                Custom.MsgEx("删除成功！");
                dgvDetail.DataSource = GetDataTable(0, "");
            }
            else
            {
                Custom.MsgEx("删除失败！");
            }
            
        }
    }
}
