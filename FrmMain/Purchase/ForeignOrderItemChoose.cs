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
    public partial class ForeignOrderItemChoose : Office2007Form
    {
        string PONumber = string.Empty;
        string ItemNumber = string.Empty;
        string UserID = string.Empty;
        string SearchWay = string.Empty;
        string VendorNumber = string.Empty;
        string FONumber = string.Empty;

        public ForeignOrderItemChoose(string vendorID,string id)
        {
            UserID = id;
            VendorNumber = vendorID;
            SearchWay = "VendorNumber";
            InitializeComponent();
            this.EnableGlass = false;
        }
        public ForeignOrderItemChoose(string vendornumber,string  fonumber,string itemnumber,string id)
        {
       //     PONumber = ponumber;
            ItemNumber = itemnumber;
            UserID = id;
            SearchWay = "ItemNumber";
            VendorNumber = vendornumber;
            FONumber = fonumber;
            InitializeComponent();
            this.EnableGlass = false;
        }

        private void GetForeignOrderItem(string vendorNumber,string foPONumber,string itemNumber,string id)
        {
            string sqlSelect = @"Select ForeignOrderNumber AS 外贸单号,VendorNumber AS 供应商代码,ItemNumber AS 物料代码,ItemDescription AS 物料描述,ItemUM AS 单位,PurchasePrice AS 价格,Quantity AS 采购数量 From PurchaseDepartmentForeignOrderItemByCMF Where BuyerID='" + id + "' And ForeignOrderNumber='" + foPONumber + "' And IsValid = 1 And Status = 1 And VendorNumber = '"+vendorNumber+"'";
            if(itemNumber !="")
            {
                sqlSelect += " And ItemNumber ='" + itemNumber + "'";
            }          
            dgvFOItem.DataSource = SQLHelper.GetDataTable(GlobalSpace.FSDBConnstr, sqlSelect);

        }

        private void GetForeignOrderItemByVendorNumber(string vendorNumber, string id)
        {
            string sqlSelect = @"Select ForeignOrderNumber AS 外贸单号,VendorNumber AS 供应商码,ItemNumber AS 物料代码,ItemDescription AS 物料描述,ItemUM AS 单位,PurchasePrice AS 价格,Quantity AS 采购数量 From PurchaseDepartmentForeignOrderItemByCMF Where BuyerID='" + id + "' And VendorNumber='" + vendorNumber + "' And IsValid = 1 And Status = 1";

            dgvFOItem.DataSource = SQLHelper.GetDataTable(GlobalSpace.FSDBConnstr, sqlSelect);
        }

        private void ForeignOrderItemChoose_Load(object sender, EventArgs e)
        {
            if(SearchWay == "ItemNumber")
            {
                GetForeignOrderItem(VendorNumber, FONumber, ItemNumber, UserID);
            }
            else if(SearchWay == "VendorNumber")
            {
                GetForeignOrderItemByVendorNumber(VendorNumber, UserID);
            }
        }

        private void dgvFOItem_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            dgvFOItem_CellContentDoubleClick(sender, e);
        }

        private void dgvFOItem_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.RowIndex >= 0)
            {
                GlobalSpace.foItemInfoList = new List<string>();
                GlobalSpace.foItemInfoList.Add(dgvFOItem.Rows[e.RowIndex].Cells["物料代码"].Value.ToString());
                GlobalSpace.foItemInfoList.Add(dgvFOItem.Rows[e.RowIndex].Cells["物料描述"].Value.ToString());
                GlobalSpace.foItemInfoList.Add(dgvFOItem.Rows[e.RowIndex].Cells["单位"].Value.ToString());
                GlobalSpace.foItemInfoList.Add(dgvFOItem.Rows[e.RowIndex].Cells["价格"].Value.ToString());
                GlobalSpace.foItemInfoList.Add(dgvFOItem.Rows[e.RowIndex].Cells["采购数量"].Value.ToString());
                this.Close();
            }
            else
            {
                MessageBoxEx.Show("请点击有效的行", "提示");
            }
        }
    }
}
