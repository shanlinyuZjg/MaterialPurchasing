﻿using System;
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
    public partial class FrmPurchaseDeptInvoiceAudit : Office2007RibbonForm
    {
        string fsUserID = string.Empty;
        string fsUserName = string.Empty;
        public FrmPurchaseDeptInvoiceAudit(string id,string name,string loginID)
        {
            this.EnableGlass = false;
            MessageBoxEx.EnableGlass = false;
            InitializeComponent();
            if (id == "CX" || string.IsNullOrWhiteSpace(id))
            {
                fsUserID = loginID;
            }
            else
            {
                fsUserID = id;
            }
            fsUserName = name;
        }

        private void FrmPurchaseDeptConfirmer_Load(object sender, EventArgs e)
        {
            tssl.Text = "登录账号：" + fsUserID + " 姓名：" + fsUserName + " IP地址：" + GetHostInfo.GetIPAddress() + "  主机：" + GetHostInfo.strHostName;
            CommonOperate.SyncServerTime();
            Version ver = new Version(Application.ProductVersion);
            this.Text = this.Text + " 版本："+ver.Major.ToString()+"."+ver.Minor.ToString()+"."+ver.Build.ToString();
        }

        private void btniPOItemConfirm_Click(object sender, EventArgs e)
        {
            // Purchase.PlaceOrder po = new Purchase.PlaceOrder(fsUserID, fsPassword, fsUserName);
            //     CommonOperate.BindFormToTabControl(tabCtrlForm, po, btniPOItemConfirm.Name, btniPOItemConfirm.Text);
            Global.Audit.InvoiceAuditMR poi = new Global.Audit.InvoiceAuditMR(fsUserID,fsUserName);
            CommonOperate.BindFormToTabControl(tabCtrlForm, poi, btniPOItemConfirm.Name, btniPOItemConfirm.Text);

        }
    }
}
