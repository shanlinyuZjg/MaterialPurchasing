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
using Global.Purchase;

namespace Global
{
    public partial class FrmPurchaseDeptSupervisor : Office2007RibbonForm
    {
        string userID = string.Empty;
        string userName = string.Empty;
        public FrmPurchaseDeptSupervisor(string id,string name)
        {
            userID = id;
            userName = name; 
            this.EnableGlass = false;
            MessageBoxEx.EnableGlass = false;
            InitializeComponent();
        }


        private void FrmPurchaseDeptSupervisor_Load(object sender, EventArgs e)
        {
            Version ver = new Version(Application.ProductVersion);
            tssl.Text = "登录账号：" + userID + " 姓名：" + userName + " IP地址：" + GetHostInfo.GetIPAddress() + "  主机：" + GetHostInfo.strHostName + " 当前版本：" + ver.Major.ToString() + "." + ver.Minor.ToString() + "." + ver.Build.ToString();
            CommonOperate.SyncServerTime();
        }

        private void btniManagePO_Click(object sender, EventArgs e)
        {
            Purchase.Supervisor super = new Purchase.Supervisor(userID);
            CommonOperate.BindFormToTabControl(tabCtrlForm, super, btniDomesticProductCheck.Name, btniDomesticProductCheck.Text);
        }

        private void btniCheckForeignOrder_Click(object sender, EventArgs e)
        {
            Purchase.SupervisorForeignOrderItemCheck sfoic = new Purchase.SupervisorForeignOrderItemCheck(userID);
            CommonOperate.BindFormToTabControl(tabCtrlForm, sfoic, btniForeignProductCheck.Name, btniForeignProductCheck.Text);
        }

        private void btniWorkArrangement_Click(object sender, EventArgs e)
        {
            Purchase.SuperisorWorkArrangement swa = new Purchase.SuperisorWorkArrangement(userID);
            CommonOperate.BindFormToTabControl(tabCtrlForm, swa, btniWorkArrangement.Name, btniWorkArrangement.Text);
        }

        private void btniCommonSetting_Click(object sender, EventArgs e)
        {
            Purchase.CheckPasswordSetting cps = new Purchase.CheckPasswordSetting(userID);
            CommonOperate.BindFormToTabControl(tabCtrlForm, cps, btniCheckPasswordSetting.Name, btniCheckPasswordSetting.Text);
        }

        private void btnEmailSetting_Click(object sender, EventArgs e)
        {
            Purchase.EmailSetting email = new Purchase.EmailSetting(userID);
            CommonOperate.BindFormToTabControl(tabCtrlForm, email, btnEmailSetting.Name, btnEmailSetting.Text);
        }

        private void FrmPurchaseDeptSupervisor_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Dispose(true);
        }

        private void btniPOProgress_Click(object sender, EventArgs e)
        {
            Purchase.SupervisorProgress super = new Purchase.SupervisorProgress(userID);
            CommonOperate.BindFormToTabControl(tabCtrlForm, super, btniPOProgress.Name, btniPOProgress.Text);
        }

        private void buttonItem1_Click(object sender, EventArgs e)
        {
            DeptItemRequirement dir = new DeptItemRequirement();
            CommonOperate.BindFormToTabControl(tabCtrlForm, dir, btniDeptItemRequirement.Name, btniDeptItemRequirement.Text);
        }

        private void ribbonControl1_Click(object sender, EventArgs e)
        {

        }
    }
}
