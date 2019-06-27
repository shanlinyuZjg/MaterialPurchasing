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
using FrmMain.Purchase;

namespace Global
{
    public partial class FrmMain:Office2007RibbonForm
    {
        string fsUserID = string.Empty;
        string fsPassword = string.Empty;
        string fsUserName = string.Empty;
        public FrmMain(string id,string pwd,string name)
        {
            this.EnableGlass = false;
            MessageBoxEx.EnableGlass = false;
            fsUserID = id;
            fsPassword = pwd;
            fsUserName = name;
            InitializeComponent();
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            /*
            string sqlSelect = @"Select ItemNumber,ItemDescription,MinimumQuantity From InventoryMaterialReminderInfoByCMF Where Buyer = '"+fsUserID+"'";
            CommonOperate.InventoryItemReminder(SQLHelper.GetDataTable(GlobalSpace.connstr, sqlSelect));
            */
            tssl.Text = "登录账号：" + fsUserID + " 姓名：" + fsUserName + " IP地址：" + GetHostInfo.GetIPAddress() +"  主机："+GetHostInfo.strHostName;
            CommonOperate.SyncServerTime();
        }

        private void ribbonControl1_Click(object sender, EventArgs e)
        {

        }

        private void btniPlaceOrder_Click(object sender, EventArgs e)
        {            
            Purchase.PlaceOrder po = new Purchase.PlaceOrder(fsUserID, fsPassword, fsUserName);
            CommonOperate.BindFormToTabControl(tabCtrlForm, po,btniPlaceOrder.Name, btniPlaceOrder.Text);
        }

        private void btniInportForeignOrder_Click(object sender, EventArgs e)
        {
            Purchase.ForeignOrderItemSubmit fois = new Purchase.ForeignOrderItemSubmit(fsUserID, fsUserName);
            CommonOperate.BindFormToTabControl(tabCtrlForm, fois, btniInportForeignOrder.Name, btniInportForeignOrder.Text);
        }

        private void btniImportNonManufacturingItems_Click(object sender, EventArgs e)
        {
            MessageBoxEx.Show("功能暂未开放！", "提示");
        }

        private void btniGetManufacturingByOrder_Click(object sender, EventArgs e)
        {
            Purchase.GetManufacturerInfoByPO gmibp = new Purchase.GetManufacturerInfoByPO();
            CommonOperate.BindFormToTabControl(tabCtrlForm,gmibp, btniGetManufacturingByOrder.Name, btniGetManufacturingByOrder.Text);
        }

        private void btniPOPrint_Click(object sender, EventArgs e)
        {
            MessageBoxEx.Show("功能暂未开放！", "提示");
        }

        private void btniReturnItemToVendor_Click(object sender, EventArgs e)
        {
            Purchase.ReturnToVendor rtv = new Purchase.ReturnToVendor(fsUserID, fsUserName);
            CommonOperate.BindFormToTabControl(tabCtrlForm, rtv, btniReturnItemToVendor.Name, btniReturnItemToVendor.Text);
        }

        private void btniManageManufacturingInfo_Click(object sender, EventArgs e)
        {
            Purchase.ManageManufacturerInfo mmi = new Purchase.ManageManufacturerInfo(fsUserID, fsUserName);
            CommonOperate.BindFormToTabControl(tabCtrlForm, mmi, btniManageManufacturingInfo.Name, btniManageManufacturingInfo.Text);
        }

        private void btniSendEmailSetting_Click(object sender, EventArgs e)
        {
            Purchase.EmailSetting es = new Purchase.EmailSetting(fsUserID);
            CommonOperate.BindFormToTabControl(tabCtrlForm, es, btniSendEmailSetting.Name, btniSendEmailSetting.Text);
        }

        private void btniManageVendorInfo_Click(object sender, EventArgs e)
        {
            Purchase.VendorEmailSetting ves = new Purchase.VendorEmailSetting(fsUserID);
            CommonOperate.BindFormToTabControl(tabCtrlForm, ves, btniManageVendorInfo.Name, btniManageVendorInfo.Text);
        }

        private void btniWorkArrange_Click(object sender, EventArgs e)
        {
            Purchase.WorkArrangement wa = new Purchase.WorkArrangement(fsUserID, fsUserName);
            CommonOperate.BindFormToTabControl(tabCtrlForm, wa, btniWorkArrange.Name, btniWorkArrange.Text);
        }

        private void btniItemInventory_Click(object sender, EventArgs e)
        {
            Purchase.ItemInventory ii = new Purchase.ItemInventory();
            CommonOperate.BindFormToTabControl(tabCtrlForm, ii, btniItemInventory.Name, btniItemInventory.Text);
        }

        private void btniItemSearch_Click(object sender, EventArgs e)
        {
            Purchase.ItemInfo ii = new Purchase.ItemInfo();
            CommonOperate.BindFormToTabControl(tabCtrlForm, ii, btniItemSearch.Name, btniItemSearch.Text);
        }

        private void FrmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Dispose(true);
        }

        private void btniImportForeignOrderItemInfo_Click(object sender, EventArgs e)
        {
            ImportForeignOrderItemInfo ifoii = new ImportForeignOrderItemInfo(fsUserID);
            CommonOperate.BindFormToTabControl(tabCtrlForm, ifoii, btniImportForeignOrderItemInfo.Name, btniImportForeignOrderItemInfo.Text);
        }

        private void ribbonBar13_ItemClick(object sender, EventArgs e)
        {

        }
    }
}
