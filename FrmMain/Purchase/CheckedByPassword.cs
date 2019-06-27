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
    public partial class CheckedByPassword : Office2007Form
    {
        string checkPassword;
        List<string> sqlPOList = new List<string>();
        public CheckedByPassword()
        {
            InitializeComponent();
        }


        public CheckedByPassword(string strpwd,List<string> list)
        {
            checkPassword = strpwd;
            sqlPOList = list;
            InitializeComponent();
        }

        private void CheckedByPassword_Load(object sender, EventArgs e)
        {
            tbPassword.Focus();
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(tbPassword.Text.Trim()))
            {
                MessageBoxEx.Show("密码不能为空！", "提示");
            }
            else
            {
                if(tbPassword.Text.Trim() == checkPassword)
                {
                    
                    var result = SQLHelper.BatchExecuteNonQuery(GlobalSpace.FSDBConnstr, sqlPOList);
                    if(result)
                    {
                        MessageBoxEx.Show("订单审核通过！", "提示");
                        this.Close();
                    }
                    else
                    {
                        MessageBoxEx.Show("订单审核失败！", "提示");
                        this.Close();
                    }
                }
                else
                {
                    MessageBoxEx.Show("密码不正确！", "提示");
                    this.Close();
                }
            }
        }

        private void tbPassword_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar == (char)13)
            {
                btnConfirm_Click(sender, e);
            }
        }
    }
}
