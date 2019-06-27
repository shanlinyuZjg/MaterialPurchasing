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

namespace Global
{
    public partial class FrmWarehouseDeptStockKeeper : Office2007RibbonForm
    {
        string fsUserID = string.Empty;
        string fsPassword = string.Empty;
        string fsUserName = string.Empty;
        public FrmWarehouseDeptStockKeeper(string id,string password,string name)
        {
            this.EnableGlass = false;
            MessageBoxEx.EnableGlass = false;
            fsUserID = id;
            fsPassword = password;
            fsUserName = name;
            InitializeComponent();
        }


        private void btniReceiveItem_Click(object sender, EventArgs e)
        {
            Warehouse.Stock stock = new Warehouse.Stock(fsUserID, fsPassword, fsUserName);
            CommonOperate.BindFormToTabControl(tabCtrlForm, stock, btniReceiveItem.Name, btniReceiveItem.Text);
        }

        private void btniItemReturned_Click(object sender, EventArgs e)
        {
            Warehouse.StockItemReturned stockir = new Warehouse.StockItemReturned(fsUserID, fsPassword, fsUserName);
            CommonOperate.BindFormToTabControl(tabCtrlForm, stockir, btniItemReturned.Name, btniItemReturned.Text);
        }

        private void FrmWarehouseDeptStockKeeper_Load(object sender, EventArgs e)
        {
            tssl.Text = "登录账号：" + fsUserID + " 姓名：" + fsUserName + " IP地址：" + GetHostInfo.GetIPAddress() + "  主机：" + GetHostInfo.strHostName;
            CommonOperate.SyncServerTime();
        }

        private void buttonItem1_Click(object sender, EventArgs e)
        {
            MessageBox.Show(" FSDB Conn:"+FSDBConn.ip + FSDBConn.database);
            MessageBox.Show(" FSDBMR Conn:" + FSDBMRConn.ip + FSDBMRConn.database);
        }

        private void FrmWarehouseDeptStockKeeper_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Dispose(true);
        }
    }
}
