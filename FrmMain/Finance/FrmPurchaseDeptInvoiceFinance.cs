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
    public partial class FrmPurchaseDeptInvoiceFinance : Office2007RibbonForm
    {
        string fsUserID = string.Empty;
        string fsUserName = string.Empty;
        string Password = string.Empty;
        public FrmPurchaseDeptInvoiceFinance(string id, string pw, string name)
        {
            this.EnableGlass = false;
            MessageBoxEx.EnableGlass = false;
            InitializeComponent();
            fsUserID = id;
            fsUserName = name;
            Password = pw;
        }

        private void FrmPurchaseDeptConfirmer_Load(object sender, EventArgs e)
        {
            
            CommonOperate.SyncServerTime();
            Version ver = new Version(Application.ProductVersion);
            tssl.Text = "登录账号：" + fsUserID + " 姓名：" + fsUserName + " IP地址：" + GetHostInfo.GetIPAddress() + "  主机：" + GetHostInfo.strHostName + "  版本：" + ver.Major.ToString() + "." + ver.Minor.ToString() + "." + ver.Build.ToString();
        }

        private void btniPOItemConfirm_Click(object sender, EventArgs e)
        {
            // Purchase.PlaceOrder po = new Purchase.PlaceOrder(fsUserID, fsPassword, fsUserName);
            //     CommonOperate.BindFormToTabControl(tabCtrlForm, po, btniPOItemConfirm.Name, btniPOItemConfirm.Text);
            /*
            Purchase.POInvoice poi = new Purchase.POInvoice();
            CommonOperate.BindFormToTabControl(tabCtrlForm, poi, btniPOItemConfirm.Name, btniPOItemConfirm.Text);*/
            Finance.InvoiceVerify iv = new Finance.InvoiceVerify(fsUserID,Password,fsUserName);
            CommonOperate.BindFormToTabControl(tabCtrlForm, iv, btniPOItemConfirm.Name, btniPOItemConfirm.Text); 

        }

        private void buttonItem1_Click(object sender, EventArgs e)
        {
            Finance.InvoiceVerifyMR iv = new Finance.InvoiceVerifyMR(fsUserID, Password, fsUserName);
            CommonOperate.BindFormToTabControl(tabCtrlForm, iv, buttonItem1.Name, buttonItem1.Text);
        }
    }
}
