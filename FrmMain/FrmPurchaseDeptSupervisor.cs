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
            tssl.Text = "登录账号：" + userID + " 姓名：" + userName + " IP地址：" + GetHostInfo.GetIPAddress() + "  主机：" + GetHostInfo.strHostName;
            CommonOperate.SyncServerTime();
        }

        private void btniManagePO_Click(object sender, EventArgs e)
        {
            Purchase.Supervisor super = new Purchase.Supervisor(userID);
            CommonOperate.BindFormToTabControl(tabCtrlForm, super, btniManagePO.Name, btniManagePO.Text);
        }

        private void btniCheckForeignOrder_Click(object sender, EventArgs e)
        {
            Purchase.SupervisorForeignOrderItemCheck sfoic = new Purchase.SupervisorForeignOrderItemCheck(userID);
            CommonOperate.BindFormToTabControl(tabCtrlForm, sfoic, btniCheckForeignOrder.Name, btniCheckForeignOrder.Text);
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
    }
}
