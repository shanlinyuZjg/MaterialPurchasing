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

namespace Global.Finance
{
    public partial class RawMaterialInfo : Office2007Form
    {
        public RawMaterialInfo()
        {
            this.EnableGlass = false;
            MessageBoxEx.EnableGlass = false;
            InitializeComponent();
        }

        private void RawMaterialInfo_Load(object sender, EventArgs e)
        {          

        }

        private void btnInventory_Click(object sender, EventArgs e)
        {
            if (tbItemNumber.Text != "")
            {
                List<string> list = CommonOperate.GetItemInfo(tbItemNumber.Text.Trim());
                if (list.Count > 0)
                {
                    tbUM.Text = list[0];
                    tbItemDescription.Text = list[1];
                    dgvInventoryItem.DataSource = GetInventoryItem(tbItemNumber.Text.Trim());
                }
                else
                {
                    MessageBoxEx.Show("物料号不正确！", "提示");
                }

            }
        }

        private DataTable GetInventoryItem(string itemNumber)
        {
            string sqlSelect = @" SELECT
                        T1.ItemNumber as 物料代码,
	                    T1.ItemDescription as 描述,
	                    T1.ItemUM as 单位,
	                    T2.Stockroom as 仓库,
	                    T2.Bin as 库位,
	                    T2.InventoryQuantity as 库存量,
                        T2.InventoryCategory as 状态,
	                    T2.LotNumber as 批号,
	                    T2.LotReceiptDate as 入库日期	                    
                        FROM
                        _NoLock_FS_Item T1,
                        _NoLock_FS_ItemInventory T2
                        WHERE
                        T1.ItemKey = T2.ItemKey
                        AND 
                        T1.ItemNumber ='"+itemNumber+"' Order By T2.LotReceiptDate DESC";
            DataTable dtTemp = SQLHelper.GetDataTableOleDb(GlobalSpace.oledbconnstrFSDBMR, sqlSelect);
            return dtTemp;
        }

        private void tbItemNumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar ==(char)13)
            {
                if(tbItemNumber.Text !="")
                {
                    List<string> list = CommonOperate.GetItemInfo(tbItemNumber.Text.Trim());
                    if (list.Count > 0)
                    {
                        tbUM.Text = list[0];
                        tbItemDescription.Text = list[1];
                        dgvInventoryItem.DataSource = GetInventoryItem(tbItemNumber.Text.Trim());
                    }
                    else
                    {
                        MessageBoxEx.Show("物料号不正确！", "提示");
                    }

                }
            }
        }

        private void tbItemNumber_TextChanged(object sender, EventArgs e)
        {
            tbItemNumber.Text = tbItemNumber.Text.ToUpper();
            tbItemNumber.SelectionStart = tbItemNumber.Text.Length;
        }

        private void btnPurchaseHistory_Click(object sender, EventArgs e)
        {
            if(tbItemNumber.Text !="")
            {
                RawMaterialPurchaseHistory rph = new RawMaterialPurchaseHistory(tbItemNumber.Text.Trim());
                rph.Show();
            }
        }
    }
}
