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
using SoftBrands.FourthShift.Transaction;

namespace Global.Warehouse
{
    public partial class ItemIMTR : Office2007Form
    {
        bool bImtr = false;
        public ItemIMTR()
        {
            this.EnableGlass = false;
            MessageBoxEx.EnableGlass = false;
            InitializeComponent();
        }

        private void tbItemNumber_TextChanged(object sender, EventArgs e)
        {
            tbItemNumber.Text = tbItemNumber.Text.ToUpper();
            tbItemNumber.SelectionStart = tbItemNumber.Text.Length;
        }

        private void tbItemNumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(!string.IsNullOrWhiteSpace(tbItemNumber.Text))
            {
                if(e.KeyChar == (char)13)
                {
                    GetCurrentStock(tbItemNumber.Text.Trim());
                    bImtr = true;
                }
            }
        }

        //获取当前物料的实时库存
        private void GetCurrentStock(string itemNumber)
        {
            string sqlSelect = @"SELECT
                                                    dbo._NoLock_FS_Item.ItemNumber AS 物料代码,
	                                                dbo._NoLock_FS_Item.ItemDescription AS 物料描述,
	                                                dbo._NoLock_FS_Item.ItemUM AS 单位,
	                                                dbo._NoLock_FS_ItemInventory.Stockroom AS 库,
	                                                dbo._NoLock_FS_ItemInventory.Bin AS 位,
	                                                dbo._NoLock_FS_ItemInventory.InventoryCategory AS 状态,
	                                                dbo._NoLock_FS_ItemInventory.InventoryQuantity AS 现有数量,
	                                                dbo._NoLock_FS_ItemInventory.LotNumber AS 批号
                                                FROM
	                                                dbo._NoLock_FS_ItemInventory
                                                INNER JOIN dbo._NoLock_FS_Item ON dbo._NoLock_FS_ItemInventory.ItemKey = dbo._NoLock_FS_Item.ItemKey
                                                WHERE
	                                                dbo._NoLock_FS_Item.ItemNumber = '" + itemNumber + "' AND InventoryCategory = 'I'";
            dgvDetail.DataSource = SQLHelper.GetDataTableOleDb(GlobalSpace.oledbconnstrFSDBMR, sqlSelect);
        }

        private void btnIMTR_Click(object sender, EventArgs e)
        {
            if(!bImtr)
            {
                MessageBoxEx.Show("当前状态不允许执行该操作！", "提示");
                return;
            }
            List<string> poList = new List<string>();
            FSFunctionLib.FSConfigFileInitialize(GlobalSpace.fsconfigfilepath, StockUser.UserID, StockUser.Password);
            int icount = 0;
            foreach (DataGridViewRow dgvr in dgvDetail.Rows)
            {
                if (Convert.ToBoolean(dgvr.Cells["Check"].Value) == true)
                {
                    IMTR01 imtr = new IMTR01();
                    imtr.ItemNumber.Value = dgvr.Cells["物料代码"].Value.ToString();
                    imtr.InventoryQuantity.Value = dgvr.Cells["现有数量"].Value.ToString();
                    imtr.StockroomFrom.Value = dgvr.Cells["库"].Value.ToString();
                    imtr.BinFrom.Value = dgvr.Cells["位"].Value.ToString();
                    imtr.InventoryCategoryFrom.Value = "I";
                    imtr.LotNumberFrom.Value = dgvr.Cells["批号"].Value.ToString();

                    imtr.StockroomTo.Value = dgvr.Cells["库"].Value.ToString();
                    imtr.BinTo.Value = dgvr.Cells["位"].Value.ToString();
                    imtr.InventoryCategoryTo.Value = "O";
                    imtr.LotNumberTo.Value = dgvr.Cells["批号"].Value.ToString();
                    imtr.LotIdentifier.Value = "E";


                    if (!FSFunctionLib.fstiClient.ProcessId(imtr, null))
                    {
                        FSTIError error = FSFunctionLib.fstiClient.TransactionError;
                        CommonOperate.WriteFSErrorLog("IMTR", imtr, error, StockUser.UserID, imtr.ItemNumber.Value+" "+ imtr.InventoryQuantity.Value+" "+ imtr.LotNumberFrom.Value);
                        icount++;
                    }
                }
            }
            FSFunctionLib.FSExit();
            if (icount > 0)
            {
                MessageBoxEx.Show("部分物料出现移库失败，请查看报错信息！", "提示");
            }
            else
            {
                MessageBoxEx.Show("全部移库成功！", "提示");
                GetCurrentStock(tbItemNumber.Text.Trim());
            }
        }

        private void btnError_Click(object sender, EventArgs e)
        {
            bImtr = false;
            string sqlSelect = @"Select Type AS 类型,ErrorContent AS 内容,OperateDateTime AS 日期 From FSErrorLogByCMF Where Operator='" + StockUser.UserID + "' And Left(OperateDateTime,10)='" + DateTime.Now.ToString("yyyy-MM-dd") + "'  Order By OperateDateTime Desc";
            dgvDetail.DataSource = SQLHelper.GetDataTable(GlobalSpace.FSDBConnstr, sqlSelect);
        }

        private void ItemIMTR_Load(object sender, EventArgs e)
        {

        }

        private void btnView_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(tbItemNumber.Text))
            {
                    GetCurrentStock(tbItemNumber.Text.Trim());
                    bImtr = true;
            }
            /*
            List<string> sqlList = new List<string>();
            foreach(DataGridViewRow dgvr in dgvDetail.Rows)
            {
                //遍历转码 
                string sqlUpdate = @"Update xxx set xxx='"+(dgvr.Cells[""].Value.ToString()).toLatin+"' Where _Row=";
                sqlList.Add(sqlUpdate);
            }
            //调用带事务的批量执行语句的方法
            */
        }

        private void btnMakeALlChecked_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow dgvr in dgvDetail.Rows)
            {
                dgvr.Cells["Check"].Value = true;
            }
        }
    }
}
