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

namespace Global.Purchase
{
    public partial class DomesticProductItemWithoutReview : Office2007Form
    {
        public DomesticProductItemWithoutReview()
        {
            InitializeComponent();
            this.EnableGlass = false;
            MessageBoxEx.EnableGlass = false;
        }

        private void tbItemNumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(!string.IsNullOrEmpty(tbItemNumber.Text))
            {
                if(e.KeyChar ==(char) 13)
                {
                    string sqlSelect = @"Select ItemDescription From _NoLock_FS_Item Where ItemNumber = '" + tbItemNumber.Text.Trim() + "'";
                    tbItemDescription.Text = SQLHelper.OleDBExecuteScalar(GlobalSpace.oledbconnstrFSDBMR, sqlSelect).ToString();
                }
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if(!string.IsNullOrEmpty(tbItemNumber.Text) && !string.IsNullOrEmpty(tbItemDescription.Text))
            {
                string sqlCheckExist = @"Select Count(Id) From PurchaseDepartmentDomesticProductItemWithoutReviewByCMF Where ItemNumber='"+tbItemNumber.Text.Trim()+"'";
                if(SQLHelper.Exist(GlobalSpace.FSDBConnstr,sqlCheckExist))
                {
                    Custom.MsgEx("该物料代码已存在！");
                    return;
                }
                string sqlInsert = @"Insert Into PurchaseDepartmentDomesticProductItemWithoutReviewByCMF (ItemNumber,ItemDescription) Values ('"+tbItemNumber.Text+"','"+tbItemDescription.Text+"')";

                if(SQLHelper.ExecuteNonQuery(GlobalSpace.FSDBConnstr,sqlInsert))
                {
                    Custom.MsgEx("增加成功！");
                    dgv.DataSource = GetAllItem();
                    tbItemDescription.Text = "";
                    tbItemNumber.Text = "";
                }
                else
                {
                    Custom.MsgEx("增加失败！");
                }
            }
        }

        private DataTable GetAllItem()
        {
            string sqlSelect = @"Select ItemNumber As 物料代码,ItemDescription AS 物料描述 From PurchaseDepartmentDomesticProductItemWithoutReviewByCMF";
            return SQLHelper.GetDataTable(GlobalSpace.FSDBConnstr, sqlSelect);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if(dgv.SelectedRows.Count != 0)
            {
                string itemnumber = dgv.SelectedRows[0].Cells["物料代码"].Value.ToString();
                string sqlDelete = @"Delete From PurchaseDepartmentDomesticProductItemWithoutReviewByCMF Where ItemNumber = '"+itemnumber+"'";
                if(SQLHelper.ExecuteNonQuery(GlobalSpace.FSDBConnstr,sqlDelete))
                {
                    Custom.MsgEx("删除成功！");
                    dgv.DataSource = GetAllItem();
                }
                else
                {
                    Custom.MsgEx("删除失败！");
                }
            }
        }

        private void tbItemNumber_TextChanged(object sender, EventArgs e)
        {
            tbItemNumber.Text = tbItemNumber.Text.ToUpper();
            tbItemNumber.SelectionStart = tbItemNumber.Text.Length;
        }

        private void DomesticProductItemWithoutReview_Load(object sender, EventArgs e)
        {
            dgv.DataSource = GetAllItem();
        }
    }
}
