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
    public partial class EmailSetting : Office2007Form
    {
        string userID = string.Empty;

        public EmailSetting(string id)
        {
            userID = id;
            this.EnableGlass = false;
            MessageBoxEx.EnableGlass = false;
            InitializeComponent();
        }

        private void btnSetEmail_Click(object sender, EventArgs e)
        {
            if (tbEmailAccount.Text.Trim() == "" || tbEmailPassword.Text.Trim() == "")
            {
                MessageBoxEx.Show("邮箱账号或密码不能为空！", "提示");
            }
            else
            {
                string email = tbEmailAccount.Text.Trim() + "@reyoung.com";
                byte[] bytes = Encoding.UTF8.GetBytes(tbEmailPassword.Text.Trim());
                string password = Convert.ToBase64String(bytes);
                string sqlUpdate = @"Update PurchaseDepartmentRBACByCMF  Set Email='" + email + "',Password='" + password + "' Where UserID='" + userID + "'";
                if (SQLHelper.ExecuteNonQuery(GlobalSpace.FSDBConnstr, sqlUpdate))
                {
                    MessageBoxEx.Show("设置成功！", "提示");
                }
                else
                {
                    MessageBoxEx.Show("设置失败！请联系管理员61075！", "提示");
                }
            }
        }

        private void btnViewPwd_MouseDown(object sender, MouseEventArgs e)
        {
            tbEmailPassword.PasswordChar = new char();
        }

        private void btnViewPwd_MouseUp(object sender, MouseEventArgs e)
        {
            tbEmailPassword.PasswordChar = '*';
        }
    }
}
