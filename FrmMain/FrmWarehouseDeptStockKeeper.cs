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
        public FrmWarehouseDeptStockKeeper(string id,string password,string name,string privilege)
        {
            this.EnableGlass = false;
            MessageBoxEx.EnableGlass = false;
            StockUser.UserID = id;
            StockUser.Password = password;
            StockUser.Privilege = privilege;
            StockUser.UserName = name;
            InitializeComponent();
        }

        private void btniReceiveItem_Click(object sender, EventArgs e)
        {
            Warehouse.Stock stock = new Warehouse.Stock(StockUser.UserID, StockUser.Password, StockUser.UserName, StockUser.Privilege);
            CommonOperate.BindFormToTabControl(tabCtrlForm, stock, btniReceiveItem.Name, btniReceiveItem.Text);
        }

        private void btniItemReturned_Click(object sender, EventArgs e)
        {
            Warehouse.StockItemReturned stockir = new Warehouse.StockItemReturned(StockUser.UserID, StockUser.Password,StockUser.UserName);
            CommonOperate.BindFormToTabControl(tabCtrlForm, stockir, btniItemReturned.Name, btniItemReturned.Text);
        }

        private void FrmWarehouseDeptStockKeeper_Load(object sender, EventArgs e)
        {
            Version ver = new Version(Application.ProductVersion);
            tssl.Text = "登录账号：" + StockUser.UserID + " 姓名：" + StockUser.UserName + " IP地址：" + GetHostInfo.GetIPAddress() + "  主机：" + GetHostInfo.strHostName + " 当前版本：" + ver.Major.ToString() + "." + ver.Minor.ToString() + "." + ver.Build.ToString();
            CommonOperate.SyncServerTime();

            string sqlCheckGMP = @"Select IsGMP From PurchaseOrderGMP";
            int gmpStatus = Convert.ToInt32(SQLHelper.ExecuteScalar(GlobalSpace.FSDBConnstr, sqlCheckGMP));

            if(gmpStatus ==1)
            {
                rbnSpecialItem.Visible = false;
            }

            string sqlSelectUserInfo = @"Select UserName,Type,InternalNumber,District,IsVial,IsDirectERP,IsERP,FileTracedNumber,FileEdition,EffectiveDate,Stock,RecordArea From StockKeeper Where UserID='" + StockUser.UserID+"'";
            DataTable dt = SQLHelper.GetDataTable(GlobalSpace.EBRConnStr, sqlSelectUserInfo);
            StockUser.Type = dt.Rows[0]["Type"].ToString();

            if (dt.Rows[0]["RecordArea"] != DBNull.Value || dt.Rows[0]["RecordArea"].ToString() != "")
            {
                StockUser.RecordArea = dt.Rows[0]["RecordArea"].ToString().Trim();
            }
            else
            {
                StockUser.RecordArea = "NotSet";
            }

            if (dt.Rows[0]["FileTracedNumber"] != DBNull.Value || dt.Rows[0]["FileTracedNumber"].ToString() != "")
            {
                StockUser.FileTracedNumber = dt.Rows[0]["FileTracedNumber"].ToString();
            }
            else
            {
                StockUser.FileTracedNumber = "NotSet";
            }

            if (dt.Rows[0]["FileEdition"] != DBNull.Value || dt.Rows[0]["FileEdition"].ToString() != "")
            {
                StockUser.FileEdition = dt.Rows[0]["FileEdition"].ToString();
            }
            else
            {
                StockUser.FileEdition = "NotSet";
            }
            if (dt.Rows[0]["EffectiveDate"] != DBNull.Value || dt.Rows[0]["EffectiveDate"].ToString() != "")
            {
                StockUser.EffectiveDate = dt.Rows[0]["EffectiveDate"].ToString();
            }
            else
            {
                StockUser.EffectiveDate = "NotSet";
            }


            if (dt.Rows[0]["District"] != DBNull.Value || dt.Rows[0]["District"].ToString() !="")
            {
                StockUser.District = dt.Rows[0]["District"].ToString().Trim();
            }
            else
            {
                StockUser.District = "NotSet";
            }
            if(dt.Rows[0]["InternalNumber"] != DBNull.Value || dt.Rows[0]["InternalNumber"].ToString() !="")
            {
                StockUser.Number = dt.Rows[0]["InternalNumber"].ToString();
            }
            else
            {
                StockUser.Number = "NotSet";
            }

            if (dt.Rows[0]["Stock"] != DBNull.Value || dt.Rows[0]["Stock"].ToString() != "")
            {
                StockUser.Stock = dt.Rows[0]["Stock"].ToString();
            }
            else
            {
                StockUser.Stock = "NotSet";
            }

            StockUser.UserName = dt.Rows[0]["UserName"].ToString().Trim();
            StockUser.IsVial = dt.Rows[0]["IsVial"].ToString();
            StockUser.IsDirectERP = dt.Rows[0]["IsDirectERP"].ToString();
            StockUser.IsERP = dt.Rows[0]["IsERP"].ToString();
            /*   StockUser.UserName = "XXX";
               StockUser.UserID = "S74";
               StockUser.Number = "50";
               */
            //Version ver = new Version(Application.ProductVersion);
            // tsslStock.Text = "登录账号：" + StockUser.UserID + " 姓名：" + StockUser.UserName + " IP地址：" + GetHostInfo.GetIPAddress() + "  主机：" + GetHostInfo.strHostName + " 当前版本：" + ver.Major.ToString() + "." + ver.Minor.ToString() + "." + ver.Build.ToString();
            //this.Text += "  版本：0.1";
            // + ver.Major.ToString() + "." + ver.Minor.ToString() + "." + ver.Build.ToString();
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

        private void ribbonControl1_Click(object sender, EventArgs e)
        {

        }

        private void tabCtrlForm_Click(object sender, EventArgs e)
        {

        }

        private void btniSpecialItem_Click(object sender, EventArgs e)
        {
            Warehouse.SpecialItem si = new Warehouse.SpecialItem();
            CommonOperate.BindFormToTabControl(tabCtrlForm, si, btniSpecialItem.Name, btniSpecialItem.Text);
        }

        private void ribbonBar2_ItemClick(object sender, EventArgs e)
        {

        }
    }
}
