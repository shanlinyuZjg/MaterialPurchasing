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
using Global.Purchase;

namespace Global
{
    public partial class FrmMain:Office2007RibbonForm
    {
        string fsUserID = string.Empty;
        string fsPassword = string.Empty;
        string fsUserName = string.Empty;
        string Privilege = string.Empty;
        string LoginID = string.Empty;
        int PONumberStartNumber = 0;
        int PONumberEndNumber = 0;

        public FrmMain(string id,string pwd,string name,string privilege)
        {
            this.EnableGlass = false;
            MessageBoxEx.EnableGlass = false;
            fsUserID = id;
            fsPassword = pwd;
            fsUserName = name;
            Privilege = privilege;
            InitializeComponent();
        }
        public FrmMain(string id, string pwd, string name, string privilege,string loginid)
        {
            this.EnableGlass = false;
            MessageBoxEx.EnableGlass = false;
            fsUserID = id;
            if(fsUserID == "CX")
            {
                fsUserID = loginid;
            }
            fsPassword = pwd;
            fsUserName = name;
            Privilege = privilege;
            LoginID = loginid;
            InitializeComponent();
        }
        //获取用户信息
        private DataTable GetUserInfo(string struserid)
        {
            string strSelect = @"Select UserID,SupervisorID,Status,Email,Name,Password,PurchaseType,POType,PriceCompare,POItemOthersConfirm,PONumberSequenceNumberRange,POTypeWithRange,POTogether,ItemReceiveType,[Group],ConfirmGroup,PartlyNotCompareItemPrice,FSUserID   from PurchaseDepartmentRBACByCMF where UserID='" + struserid + "'";
     //       Custom.Msg(strSelect);
            return SQLHelper.GetDataTable(GlobalSpace.FSDBConnstr, strSelect);
        }
        private void FrmMain_Load(object sender, EventArgs e)
        {
            /*
            string sqlSelect = @"Select ItemNumber,ItemDescription,MinimumQuantity From InventoryMaterialReminderInfoByCMF Where Buyer = '"+fsUserID+"'";
            CommonOperate.InventoryItemReminder(SQLHelper.GetDataTable(GlobalSpace.connstr, sqlSelect));
            */
            Version ver = new Version(Application.ProductVersion);
            tssl.Text = "登录账号：" + fsUserID + " 姓名：" + fsUserName + " IP地址：" + GetHostInfo.GetIPAddress() +"  主机："+GetHostInfo.strHostName +" 当前版本："+ver.Major.ToString()+"."+ver.Minor.ToString()+"."+ver.Build.ToString();
            CommonOperate.SyncServerTime();
            DataTable dtTemp = GetUserInfo(fsUserID);
            PurchaseUser.SupervisorID = dtTemp.Rows[0]["SupervisorID"].ToString();
            PurchaseUser.UserEmail = dtTemp.Rows[0]["Email"].ToString();
            PurchaseUser.UserEmailDecryptedPassword = dtTemp.Rows[0]["Password"].ToString();
            PurchaseUser.UserStatus = Convert.ToInt32(dtTemp.Rows[0]["Status"]);
            PurchaseUser.POItemOthersConfirm = Convert.ToInt32(dtTemp.Rows[0]["POItemOthersConfirm"]);
            PurchaseUser.PurchaseType = dtTemp.Rows[0]["PurchaseType"].ToString();
            PurchaseUser.PriceCompare = Convert.ToInt32(dtTemp.Rows[0]["PriceCompare"]);
            PurchaseUser.POTypeWithRange = dtTemp.Rows[0]["POTypeWithRange"].ToString();
            PurchaseUser.POTogether = Convert.ToInt32(dtTemp.Rows[0]["POTogether"]);
            PurchaseUser.PartlyNotCompareItemPrice = Convert.ToInt32(dtTemp.Rows[0]["PartlyNotCompareItemPrice"]);
            PurchaseUser.Group = dtTemp.Rows[0]["Group"].ToString();
            PurchaseUser.ConfirmGroup = dtTemp.Rows[0]["ConfirmGroup"].ToString();      
            PurchaseUser.UserID = dtTemp.Rows[0]["UserID"].ToString();
            PurchaseUser.UserName = dtTemp.Rows[0]["Name"].ToString();

            if (dtTemp.Rows[0]["ItemReceiveType"] != DBNull.Value)
            {
                PurchaseUser.ItemReceiveType = dtTemp.Rows[0]["ItemReceiveType"].ToString();
            }
            else
            {
                PurchaseUser.ItemReceiveType = "NotSet";
            }

            if (dtTemp.Rows[0]["PONumberSequenceNumberRange"] == DBNull.Value || string.IsNullOrEmpty(dtTemp.Rows[0]["PONumberSequenceNumberRange"].ToString()))
            {
                PurchaseUser.PONumberSequenceNumberRange = "";
            }
            else
            {
                PurchaseUser.PONumberSequenceNumberRange = dtTemp.Rows[0]["PONumberSequenceNumberRange"].ToString();
                string[] range = PurchaseUser.PONumberSequenceNumberRange.Split('-');
                PONumberStartNumber = Convert.ToInt32(range[0]);
                PONumberEndNumber = Convert.ToInt32(range[1]);
            }

            if (dtTemp.Rows[0]["POType"] == DBNull.Value || string.IsNullOrEmpty(dtTemp.Rows[0]["POType"].ToString()))
            {
                PurchaseUser.UserPOType = "";
            }
            else
            {
                PurchaseUser.UserPOType = dtTemp.Rows[0]["POType"].ToString();
            }
        }

        private void ribbonControl1_Click(object sender, EventArgs e)
        {

        }

        private void btniPlaceOrder_Click(object sender, EventArgs e)
        {            
            Purchase.PlaceOrder po = new Purchase.PlaceOrder(fsUserID, fsPassword, fsUserName, Privilege,PONumberStartNumber,PONumberEndNumber);
            CommonOperate.BindFormToTabControl(tabCtrlForm, po, btniPlaceOrder.Name, btniPlaceOrder.Text);

        }

        private void btniInportForeignOrder_Click(object sender, EventArgs e)
        {
            Purchase.ForeignOrderItemSubmit fois = new Purchase.ForeignOrderItemSubmit(fsUserID, fsUserName);
            CommonOperate.BindFormToTabControl(tabCtrlForm, fois, btniInportForeignOrder.Name, btniInportForeignOrder.Text);
        }

        private void btniImportNonManufacturingItems_Click(object sender, EventArgs e)
        {
            //MessageBoxEx.Show("功能暂未开放！", "提示");
            Purchase.AssistantItems ai = new Purchase.AssistantItems(fsUserID);
            CommonOperate.BindFormToTabControl(tabCtrlForm, ai, btniImportNonManufacturingItems.Name, btniImportNonManufacturingItems.Text);
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
            Custom.MsgEx("功能测试中，暂未开放！");
            /*
            Purchase.ReturnToVendor rtv = new Purchase.ReturnToVendor(fsUserID, fsUserName);
            CommonOperate.BindFormToTabControl(tabCtrlForm, rtv, btniReturnItemToVendor.Name, btniReturnItemToVendor.Text);*/
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

        private void btniPrintPO_Click(object sender, EventArgs e)
        {
            Purchase.POItemConfirmerMaintain picm = new Purchase.POItemConfirmerMaintain();
            CommonOperate.BindFormToTabControl(tabCtrlForm, picm, btniPOItemConfirmerMaintain.Name, btniPOItemConfirmerMaintain.Text);
        }

        private void ribbonBar13_ItemClick_1(object sender, EventArgs e)
        {

        }

        private void btnPOItemConfirm_Click(object sender, EventArgs e)
        {
   //         Custom.MsgEx(PurchaseUser.ConfirmGroup);
            if (PurchaseUser.ConfirmGroup == "P")
            {
                POItemConfirmPackage pic = new POItemConfirmPackage(fsUserID, fsPassword);
                CommonOperate.BindFormToTabControl(tabCtrlForm, pic, btnPOItemConfirm.Name, btnPOItemConfirm.Text);
            }
            else if(PurchaseUser.ConfirmGroup == "M" || PurchaseUser.ConfirmGroup == "A" || PurchaseUser.ConfirmGroup == "ALL")
            {
                POItemConfirmAPIS picA = new POItemConfirmAPIS(fsUserID, fsPassword);
                CommonOperate.BindFormToTabControl(tabCtrlForm, picA, btnPOItemConfirm.Name, btnPOItemConfirm.Text);
            }

            
        }

        private void btniDomesticNotComparePrice_Click(object sender, EventArgs e)
        {

            if(PurchaseUser.PartlyNotCompareItemPrice ==  1)
            {
                DomesticProductItemWithoutComparePriceMaintain dpwcpm = new DomesticProductItemWithoutComparePriceMaintain();
                CommonOperate.BindFormToTabControl(tabCtrlForm, dpwcpm, btniDomesticNotComparePrice.Name, btniDomesticNotComparePrice.Text);
            }
            else
            {
                Custom.MsgEx("没有此权限！");
            }
        }

        private void btnPurchaseVial_Click(object sender, EventArgs e)
        {
            Purchase.PurchaseVials pv = new Purchase.PurchaseVials(PONumberStartNumber, PONumberEndNumber);
            CommonOperate.BindFormToTabControl(tabCtrlForm, pv, btnPurchaseVial.Name, btnPurchaseVial.Text);
        }

        private void btniInvoice_Click(object sender, EventArgs e)
        {
            //MessageBox.Show("请用新版发票管理");
            //return;
            Purchase.POInvoice poi = new POInvoice();
            CommonOperate.BindFormToTabControl(tabCtrlForm, poi, btniInvoice.Name, btniInvoice.Text);
        }

        private void btniDeptItemRequirement_Click(object sender, EventArgs e)
        {
            DeptItemRequirement dir = new DeptItemRequirement(PurchaseUser.UserID, PurchaseUser.UserName, PONumberStartNumber);
            CommonOperate.BindFormToTabControl(tabCtrlForm, dir, btniDeptItemRequirement.Name, btniDeptItemRequirement.Text);
        }
        
        private void btniItemRequirMakeOrder_Click(object sender, EventArgs e)
        {
            //DeptItemRequirementPlaceOrder dirpo = new DeptItemRequirementPlaceOrder(PONumberStartNumber, PONumberEndNumber, PurchaseUser.UserID);
            //CommonOperate.BindFormToTabControl(tabCtrlForm, dirpo, btniItemRequirMakeOrder.Name, btniItemRequirMakeOrder.Text);
            POInvoice_MR dirpo1 = new POInvoice_MR( PurchaseUser.UserID,PurchaseUser.UserName); 
            CommonOperate.BindFormToTabControl(tabCtrlForm, dirpo1, btniItemRequirMakeOrder.Name, btniItemRequirMakeOrder.Text);
        }

        private void BtnItemDemand_Click(object sender, EventArgs e)
        {
            MessageBox.Show("暂未上线");
            return;
            ItemDemand dirpo1 = new ItemDemand( PurchaseUser.UserID,PurchaseUser.UserName); 
            CommonOperate.BindFormToTabControl(tabCtrlForm, dirpo1, BtnItemDemand.Name, BtnItemDemand.Text);
        }
    }
}
