using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using System.Data.SqlClient;
using Global.Helper;

namespace Global.Purchase
{
    public partial class CheckPasswordSetting : Office2007Form
    {
        string userID = string.Empty;
        public CheckPasswordSetting(string userid)
        {
            userID = userid;
            this.EnableGlass = false;
            MessageBoxEx.EnableGlass = false;
            InitializeComponent();
        }

        private void btnViewPwd_MouseDown(object sender, MouseEventArgs e)
        {
            tbPassword.PasswordChar = new char();
        }

        private void btnViewPwd_MouseUp(object sender, MouseEventArgs e)
        {
            tbPassword.PasswordChar = '*';
        }

        private void btnSetPassword_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(tbPassword.Text.Trim()))
            {
                MessageBoxEx.Show("密码不能为空！", "提示");
            }
            else
            {
                string sqlCheck = @"Select Id from PurchaseDepartmentCheckPasswordByCMF Where Supervisor ='" + userID + "'  And IsValid = 0";

                if (SQLHelper.Exist(GlobalSpace.FSDBConnstr, sqlCheck))
                {
                    MessageBoxEx.Show("当前存在未失效密码，请先取消！", "提示");
                }
                else
                {
                    if (dtpFinish.Value <= dtpStart.Value)
                    {
                        MessageBoxEx.Show("结束日期必须晚于开始日期！", "提示");
                    }
                    else
                    {

                        string sqlInsert = @"Insert Into PurchaseDepartmentCheckPasswordByCMF
                        (Password,StartDate,FinishDate,Supervisor) 
                        Values
                        (@Password,@StartDate,@FinishDate,'" + userID + "')";
                        SqlParameter[] sqlparams =
                        {
                    new SqlParameter("@Password",tbPassword.Text.Trim()),
                    new SqlParameter("@StartDate",dtpStart.Value.ToString("yyyy-MM-dd")),
                    new SqlParameter("@FinishDate",dtpFinish.Value.ToString("yyyy-MM-dd"))
                    };


                        if (SQLHelper.ExecuteNonQuery(GlobalSpace.FSDBConnstr, sqlInsert, sqlparams) )
                        {
                            MessageBoxEx.Show("设置成功！请点击”发送邮件“按钮给员工发送提醒邮件！", "提示");
                        }
                        else
                        {
                            MessageBoxEx.Show("设置失败！", "提示");
                        }
                    }

                }

            }
        }

        private void btnInvalidateCurrentPassword_Click(object sender, EventArgs e)
        {
            string sqlCheck = @"Select Id from PurchaseDepartmentCheckPasswordByCMF Where Supervisor ='" + userID + "' And IsValid = 0";

            if (SQLHelper.Exist(GlobalSpace.FSDBConnstr, sqlCheck))
            {
                string id = SQLHelper.GetItemValue(GlobalSpace.FSDBConnstr, "Id", sqlCheck);
                string sqlUpdate = @"Update PurchaseDepartmentCheckPasswordByCMF Set IsValid = 1 Where Id='" + id + "'";

                if (SQLHelper.ExecuteNonQuery(GlobalSpace.FSDBConnstr, sqlUpdate))
                {
                    MessageBoxEx.Show("取消成功！", "提示");
                }
                else
                {
                    MessageBoxEx.Show("取消失败！", "提示");
                }
            }
            else
            {
                MessageBoxEx.Show("当前不存在有效密码！", "提示");
            }
        }

        private void btnSendEMails_Click(object sender, EventArgs e)
        {
            string sqlSelectSupervisorEmail = @"Select Email,Password,Name From PurchaseDepartmentRBACByCMF Where UserID='" + userID + "'";
            string sqlCheck = @"Select count(Id) from PurchaseDepartmentCheckPasswordByCMF Where Supervisor ='" + userID + "' And IsValid = 0";
            if (SQLHelper.Exist(GlobalSpace.FSDBConnstr, sqlCheck))
            {
                string sqlSelectEmails = @"Select Email,Name From PurchaseDepartmentRBACByCMF Where SupervisorID='" + userID + "'";
                string sqlSelectPassword = @"SELECT
	                                                            Password,
	                                                            StartDate,
	                                                            FinishDate,
	                                                            IsValid
                                                            FROM
	                                                            PurchaseDepartmentCheckPasswordByCMF
                                                            WHERE
	                                                            Supervisor = '" + userID + "'   AND IsValid = 0 ORDER BY OperateDateTime DESC";
                DataTable dtTemp = SQLHelper.GetDataTable(GlobalSpace.FSDBConnstr, sqlSelectEmails);
                DataTable dtTempSupervisorEmail = SQLHelper.GetDataTable(GlobalSpace.FSDBConnstr, sqlSelectSupervisorEmail);
                DataTable dtTempPassword = SQLHelper.GetDataTable(GlobalSpace.FSDBConnstr, sqlSelectPassword);
                if (dtTemp.Rows.Count > 0)
                {
                    if (dtTempSupervisorEmail.Rows[0]["Email"].ToString() != DBNull.Value.ToString())
                    {

                        try
                        {

                            Dictionary<string, string> emailList = new Dictionary<string, string>();
                            for (int i = 0; i < dtTemp.Rows.Count; i++)
                            {
                                emailList.Add(dtTemp.Rows[i]["Email"].ToString(), dtTemp.Rows[i]["Name"].ToString());
                            }


                            Email email = new Email();
                            email.fromEmail = dtTempSupervisorEmail.Rows[0]["Email"].ToString();
                            email.fromPerson = dtTempSupervisorEmail.Rows[0]["Name"].ToString();
                            email.encoding = "UTF-8";
                            email.smtpServer = "192.168.8.3";
                            email.smtpPort = "25";
                            email.userName = dtTempSupervisorEmail.Rows[0]["Email"].ToString();
                            //对密码进行解密
                            byte[] debytes = Convert.FromBase64String(dtTempSupervisorEmail.Rows[0]["Password"].ToString());
                            string depwd = Encoding.UTF8.GetString(debytes);
                            email.passWord = depwd;
                            email.emailTitle = "采购订单审核用密码已经设置";
                            email.emailContent = "您好！采购订单审核用密码已经设置，当前密码：" + dtTempPassword.Rows[0]["Password"].ToString() + "，有效期自" + dtTempPassword.Rows[0]["StartDate"].ToString() + "至" + dtTempPassword.Rows[0]["FinishDate"].ToString() + "！";
                            MailHelper.SendEmail(email, emailList);


                        }
                        catch (Exception ex)
                        {
                            MessageBoxEx.Show(ex.Message);
                        }
                    }
                    else
                    {
                        MessageBoxEx.Show("当前没有设置发送邮箱，请先设置邮箱！", "提示");
                    }

                }
                else
                {
                    MessageBoxEx.Show("没有可用的员工邮箱列表！", "提示");
                    return;
                }

            }
            else
            {
                MessageBoxEx.Show("当前没有可用的审核密码，请设置！", "提示");
            }
        }
    }
}
