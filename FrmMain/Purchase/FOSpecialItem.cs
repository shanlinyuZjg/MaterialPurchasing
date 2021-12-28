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
    public partial class FOSpecialItem : Office2007Form
    {
        public FOSpecialItem()
        {
            InitializeComponent();
            this.EnableGlass = false;
            MessageBoxEx.EnableGlass = false;
        }

        private void textBoxX1_TextChanged(object sender, EventArgs e)
        {
            tbItemNumber.Text = tbItemNumber.Text.ToUpper();
            tbItemNumber.SelectionStart = tbItemNumber.Text.Length;
        }

        private void FOSpecialItem_Load(object sender, EventArgs e)
        {
            ShowItem();
        }
        private void ShowItem()
        {
            string sqlSelect = @"Select Id,ItemNumber AS 物料代码,ItemDescription AS 物料描述 From PurchaseDepartmentForeignOrderItemNotInByCMF";
            dgv.DataSource  = SQLHelper.GetDataTable(GlobalSpace.FSDBConnstr, sqlSelect);
            dgv.Columns["Id"].Visible = false;
        }

        private void tbItemNumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar == (char)13)
            {
                if(!string.IsNullOrEmpty(tbItemNumber.Text))
                {
                    string sqlSelect = @"Select ItemDescription From _NoLock_FS_Item Where ItemNumber = '"+tbItemNumber.Text.ToUpper()+"'";

                    tbItemDescription.Text = SQLHelper.OleDBExecuteScalar(GlobalSpace.oledbconnstrFSDBMR, sqlSelect).ToString();
                }
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            string sqlCheck = @"Select Count(Id) From  PurchaseDepartmentForeignOrderItemNotInByCMF  Where ItemNumber = '"+tbItemNumber.Text+"'";
            if(SQLHelper.Exist(GlobalSpace.FSDBConnstr,sqlCheck))
            {
                Custom.MsgEx("该物料代码已存在！");
                return;
            }
            else
            {
                string sqlInsert = @"Insert INTO PurchaseDepartmentForeignOrderItemNotInByCMF (ItemNumber,ItemDescription) Values ('"+tbItemNumber.Text+"','"+tbItemDescription.Text+"')";
                if(SQLHelper.ExecuteNonQuery(GlobalSpace.FSDBConnstr,sqlInsert))
                {
                    Custom.MsgEx("添加成功！");
                    ShowItem();
                }
                else
                {
                    Custom.MsgEx("添加失败！");
                    return;
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if(dgv.SelectedRows.Count == 1)
            {
                string id = dgv.SelectedRows[0].Cells["Id"].Value.ToString();
                string sqlDel = @"Delete From PurchaseDepartmentForeignOrderItemNotInByCMF Where Id = "+id+"";
                if(SQLHelper.ExecuteNonQuery(GlobalSpace.FSDBConnstr,sqlDel))
                {
                    Custom.MsgEx("删除成功！");
                    ShowItem();
                }
                else
                {
                    Custom.MsgEx("删除失败！");
                    return;
                }
            }
            else
            {
                Custom.MsgEx("无选中的行或者选中了多行！");
            }
        }
    }
}
