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
    public partial class FrmStockUserManage : Office2007RibbonForm
    {
        string fsUserID = string.Empty;
        string fsUserName = string.Empty;
        string UserPrivilege = string.Empty;
        public FrmStockUserManage(string id,string name,string privilege)
        {
            this.EnableGlass = false;
            MessageBoxEx.EnableGlass = false;
            InitializeComponent();
            fsUserID = id;
            fsUserName = name;
            UserPrivilege = privilege;
        }

        private void FrmPurchaseDeptConfirmer_Load(object sender, EventArgs e)
        {
            tssl.Text = "登录账号：" + fsUserID + " 姓名：" + fsUserName + " IP地址：" + GetHostInfo.GetIPAddress() + "  主机：" + GetHostInfo.strHostName;
            CommonOperate.SyncServerTime();
            Version ver = new Version(Application.ProductVersion);
            this.Text = this.Text + " 版本：0.1";// +ver.Major.ToString()+"."+ver.Minor.ToString()+"."+ver.Build.ToString();
 //           MessageBox.Show(UserPrivilege);
            if(UserPrivilege.Contains("SSUPER"))
            {
                rbnUser.Visible = true;
            }
            else if(UserPrivilege.Contains("SFILE") || UserPrivilege.Contains("SQA"))
            {
                rbnFileEdition.Visible = true;
            }
        }

        private void btniPOItemConfirm_Click(object sender, EventArgs e)
        {
            // Purchase.PlaceOrder po = new Purchase.PlaceOrder(fsUserID, fsPassword, fsUserName);
            //     CommonOperate.BindFormToTabControl(tabCtrlForm, po, btniPOItemConfirm.Name, btniPOItemConfirm.Text);
            Warehouse.FileEditionManage flm = new Warehouse.FileEditionManage();
           CommonOperate.BindFormToTabControl(tabCtrlForm, flm, btniPOItemConfirm.Name, btniPOItemConfirm.Text);

        }

        private void btniUserManagement_Click(object sender, EventArgs e)
        {
            Warehouse.UserManage um = new Warehouse.UserManage();
            CommonOperate.BindFormToTabControl(tabCtrlForm, um, btniUserManagement.Name, btniUserManagement.Text);
        }
    }
}
