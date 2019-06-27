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
    public partial class InventoryMaterialReminder :Office2007Form
    {
        string userID = string.Empty;
        public InventoryMaterialReminder()
        {
            InitializeComponent();
        }
        public InventoryMaterialReminder(string strid)
        {
            this.EnableGlass = false;
            MessageBoxEx.EnableGlass = false;
            userID = strid;
            InitializeComponent();
        }

        private void InventoryMaterialReminder_Load(object sender, EventArgs e)
        {
            LoadInventoryItemToRemind(userID);
            LoadInventoryItemSetting(userID);
        }
        //加载当前需要提醒的物料信息
        private void LoadInventoryItemToRemind(string id)
        {
            string sqlSelect = @"Select ItemNumber,ItemDescription,MinimumQuantity From PurchaseDepartmentInventoryMaterialReminderInfoByCMF Where Buyer = '" + id + "'";
            dgvInventory.DataSource = CommonOperate.InventoryItemReminder(SQLHelper.GetDataTable(GlobalSpace.FSDBConnstr, sqlSelect));
        }

        //加载当前设置库存提醒的物料信息
        private void LoadInventoryItemSetting(string id)
        {
            string sqlSelect = @"Select ItemNumber as 物料代码,ItemDescription as 物料描述,MinimumQuantity as 库存最低数量 From PurchaseDepartmentInventoryMaterialReminderInfoByCMF Where Buyer = '" + id + "'";
            dgvInventoryItemSetting.DataSource =SQLHelper.GetDataTable(GlobalSpace.FSDBConnstr, sqlSelect);
        }

        private void tbItemNumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar ==(char)13)
            {
                if(tbItemNumber.Text !="")
                {
                    List<string> list = CommonOperate.GetItemInfo(tbItemNumber.Text.Trim());
                    if(list.Count > 0)
                    {
                        tbItemDescription.Text = list[1];
                        tbUM.Text = list[0];
                        tbMinimumQuantity.Focus();
                    }
                }
            }
        }

        private void btnPlace_Click(object sender, EventArgs e)
        {
            if(tbItemNumber.Text !="" && tbItemDescription.Text !="" && tbMinimumQuantity.Text !="")
            {
                string sqlInsert = @"Insert Into PurchaseDepartmentInventoryMaterialReminderInfoByCMF (ItemNumber,ItemDescription,UM,MinimumQuantity) Values('"+tbItemNumber.Text.Trim()+"','"+tbItemDescription.Text.Trim()+"','"+tbUM.Text.Trim()+"','"+tbMinimumQuantity.Text.Trim()+"')";
                if(SQLHelper.ExecuteNonQuery(GlobalSpace.FSDBConnstr, sqlInsert) )
                {
                    MessageBoxEx.Show("设置成功！","提示");
                    tbItemDescription.Text = "";
                    tbItemNumber.Text = "";
                    tbMinimumQuantity.Text = "";
                    LoadInventoryItemSetting(userID);
                }
                else
                {
                    MessageBoxEx.Show("设置失败！", "提示");
                }
            }
            else
            {
                MessageBoxEx.Show("物料代码，描述和最低库存数量不能为空！", "提示");
            }
        }

        private void tbItemNumber_TextChanged(object sender, EventArgs e)
        {
            tbItemNumber.Text = tbItemNumber.Text.ToUpper();
            tbItemNumber.SelectionStart = tbItemNumber.TextLength;
        }

        private void tbMinimumQuantity_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar ==(char)13)
            {
                btnPlace_Click(sender, e);
            }
        }
    }
}
