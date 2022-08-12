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
using System.Data.SqlClient;
using System.Net.Mail;
using System.Net;

namespace Global.Purchase
{
    public partial class VendorEmailSetting : Office2007Form
    {
        string userID = string.Empty;
        public VendorEmailSetting()
        {
            InitializeComponent();
        }

        public VendorEmailSetting(string id)
        {
            this.EnableGlass = false;
            MessageBoxEx.EnableGlass = false;
            userID = id;
            InitializeComponent();
        }
        private string Path = string.Empty;
        private string PoNumber = string.Empty;
        private string VendorNumber = string.Empty;
        private string VendorName = string.Empty;

        /// <summary>
        /// 邮件发送时调用
        /// </summary>
        /// <param name="id">用户ID</param>
        /// <param name="poNumber">采购单号</param>
        /// <param name="path">附件地址</param>
        public VendorEmailSetting(string id, string poNumber, string path,string vendornumber,string vendorname)
        {
            this.EnableGlass = false;
            MessageBoxEx.EnableGlass = false;
            userID = id;
            PoNumber = poNumber;
            Path = path;
            VendorNumber = vendornumber;
            VendorName = vendorname;
            InitializeComponent();
            GBemailsend.Visible = true;
            this.Text = vendornumber+vendorname + " " + poNumber;
        }

    

        private void VendorEmailSetting_Load(object sender, EventArgs e)
        {
            tbVendorNumber.Text = VendorNumber;
            btnSearch_Click(null, null);
        }


        private bool IsExist()
        {
            string sqlSelect = @"Select Count(Id) From PurchaseDepartmentVendorEmailByCMF Where VendorNumber='" + tbVendorNumber.Text + "' and Email='" + tbEmail.Text + "'";
            if (SQLHelper.Exist(GlobalSpace.FSDBConnstr, sqlSelect))
            {
                return true;
            }
            return false;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            tbVendorNumber.Text = tbVendorNumber.Text.Trim();
            tbVendorName.Text = tbVendorName.Text.Trim();
            tbEmail.Text = tbEmail.Text.Trim();
            if (tbVendorNumber.Text != "" && tbVendorName.Text != "" && tbEmail.Text != "")
            {
                if (IsExist())
                {
                    MessageBoxEx.Show("该供应商邮箱已存在！", "提示");

                }
                else
                {

                    string sqlInsert = @"Insert Into PurchaseDepartmentVendorEmailByCMF (VendorNumber,VendorName,Email,EmailName) Values(@VendorNumber,@VendorName,@Email,@EmailName)";
                    SqlParameter[] sqlparams =
                    {
                        new SqlParameter("@VendorNumber",tbVendorNumber.Text.Trim()),
                        new SqlParameter("@VendorName",tbVendorName.Text.Trim()),
                        new SqlParameter("@Email",tbEmail.Text.Trim()),
                        new SqlParameter("@EmailName",tbEmailName.Text.Trim())
                    };
                    if (SQLHelper.ExecuteNonQuery(GlobalSpace.FSDBConnstr, sqlInsert, sqlparams))
                    {
                        MessageBoxEx.Show("增加成功！", "提示");
                        tbVendorName.Text = "";
                        tbVendorNumber.Text = "";
                        tbEmail.Text = "";
                        tbEmailName.Text = "";
                        btnSearch_Click(null, null);
                    }
                    else
                    {
                        MessageBoxEx.Show("增加失败！", "提示");
                    }


                }
            }
            else
            {
                MessageBoxEx.Show("供应商代码，供应商名和邮箱均不能为空！", "提示");
            }
        }



        private void btnUpdate_Click(object sender, EventArgs e)
        {
            List<string> list = new List<string>();
            for (int i = 0; i < dgvVendorEmail.Rows.Count; i++)
            {
                if (Convert.ToBoolean(dgvVendorEmail["EmaiCheck", i].Value))
                    list.Add(dgvVendorEmail["Id", i].Value.ToString());
            }
            if (list.Count == 0) { MessageBox.Show("未选择"); return; }
            string sqlUpdate = @"delete from PurchaseDepartmentVendorEmailByCMF where Id in ('" + string.Join("','", list) + "')";
            if (SQLHelper.ExecuteNonQuery(GlobalSpace.FSDBConnstr, sqlUpdate))
            {
                MessageBoxEx.Show("删除成功！", "提示");
                btnSearch_Click(null, null);
            }
            else
            {
                MessageBoxEx.Show("删除失败！", "提示");
            }
        }

        private void tbVendorNumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                tbVendorNumber.Text = tbVendorNumber.Text.Trim();
                if (tbVendorNumber.Text.Trim() != "")
                {
                    string vendorname = CommonOperate.GetVendorInfo(tbVendorNumber.Text.Trim());
                    if (vendorname == "")
                    {
                        MessageBoxEx.Show("未查到该供应商代码的信息！", "提示");
                    }
                    else
                    {
                        tbVendorName.Text = vendorname;
                        tbEmail.Focus();
                        btnSearch_Click(sender, e);
                    }
                }
                else
                {
                    MessageBoxEx.Show("供应商代码不能为空！", "提示");
                }
            }

        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string sqlSelect = @"Select Id, VendorNumber AS 供应商码,VendorName AS 供应商名,Email AS 邮箱,EmailName AS 姓名 From PurchaseDepartmentVendorEmailByCMF Where VendorNumber like '%" + tbVendorNumber.Text.Trim() + "%'";
            dgvVendorEmail.DataSource = SQLHelper.GetDataTable(GlobalSpace.FSDBConnstr, sqlSelect);
            dgvVendorEmail.Columns["Id"].Visible = false;
        }


        private void dgvVendorEmail_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int i = e.RowIndex;
            if (i > -1)
            {
                dgvVendorEmail["EmaiCheck", i].Value = !Convert.ToBoolean(dgvVendorEmail["EmaiCheck", i].Value);
            }
        }

        private void EmailSend_Click(object sender, EventArgs e)
        {
            string sqlSelectUserInfo = @"Select UserID, Email,Password,Name From PurchaseDepartmentRBACByCMF Where UserID='" + userID + "' or UserID=(select SupervisorID From PurchaseDepartmentRBACByCMF Where UserID='" + userID + "')";
            DataTable dtUserInfo = SQLHelper.GetDataTable(GlobalSpace.FSDBConnstr, sqlSelectUserInfo);
            string UserName = string.Empty;
            string EmailPassword = string.Empty;
            foreach (DataRow dr in dtUserInfo.Rows)
            {
                if(dr["UserID"].ToString()==userID)
                {
                    if (dr["Email"].ToString() == string.Empty)
                    {
                        MessageBox.Show("邮箱未设置！");
                        return;
                    }
                    else
                    {
                        CBme.Tag = dr["Email"].ToString();
                        UserName = dr["Name"].ToString();
                        EmailPassword = dr["Password"].ToString();

                    }
                }
                else
                {
                    CKleader.Tag = dr["Email"].ToString();
                }
            }
            List<string> smtpList = CommonOperate.GetSMTPServerInfo();
            if (smtpList.Count > 0)
            {
                MailMessage mmsg = new MailMessage();
                try
                {
                    mmsg.From = new MailAddress(CBme.Tag.ToString(), UserName);
                    for (int i = 0; i < dgvVendorEmail.Rows.Count; i++)
                    {
                        if (Convert.ToBoolean(dgvVendorEmail["EmaiCheck", i].Value))
                        {
                            mmsg.To.Add(new MailAddress(dgvVendorEmail["邮箱", i].Value.ToString(), dgvVendorEmail["姓名", i].Value.ToString()));
                        }
                    }
                    if (mmsg.To.Count == 0) { MessageBox.Show("未选择收件人！");return; }
                    if (CKleader.Checked)
                    {
                        if (CKleader.Tag != null)
                            if (!string.IsNullOrWhiteSpace(CKleader.Tag.ToString()))
                                mmsg.CC.Add(CKleader.Tag.ToString());

                    }
                    if (CBme.Checked)
                    {
                        mmsg.CC.Add(CBme.Tag.ToString());

                    }
                    mmsg.Subject = "瑞阳制药有限公司采购订单";
                    mmsg.Body = VendorName + "：" + "<br>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + "您好!<br>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;附件是瑞阳制药有限公司采购订单，请查收并及时配货，有问题及时联系！";
                    //IsBodyHtml为True，如果邮件内容中有需要换行等操作的，使用<br>来换行或者其他的标识符
                    mmsg.IsBodyHtml = true;
                    Attachment mailAttach = new Attachment(Path);
                    mmsg.Attachments.Add(mailAttach);
                    for (int i = 0; i < DgvFJ.Rows.Count; i++)
                    {
                        mmsg.Attachments.Add(new Attachment(DgvFJ["FilePath",i].Value.ToString()));
                    }
                    mmsg.BodyEncoding = System.Text.Encoding.GetEncoding("UTF-8");
                    mmsg.Priority = MailPriority.High;
                    SmtpClient smtp = new SmtpClient();
                    smtp.Host = smtpList[0];
                    smtp.Port = Convert.ToInt32(smtpList[1]);
                    smtp.Credentials = new NetworkCredential(CBme.Tag.ToString(), CommonOperate.Base64Decrypt(EmailPassword));
                    smtp.Send(mmsg);
                    mmsg.Dispose();
                    MessageBox.Show("邮件发送完成");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("邮件发送失败:"+ex.Message);
                }
            }
            else
            {
                MessageBoxEx.Show("未设置SMTP服务器IP地址和端口，请联系管理员！", "提示");
            }


        }

        private void AddAttachment_Click(object sender, EventArgs e)
        {
            OpenFileDialog file = new OpenFileDialog();
            if (file.ShowDialog() == DialogResult.OK)
            {
                DgvFJ.Rows.Add(file.FileName);
                
            }
        }

        private void DgvFJ_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                if (DgvFJ.Columns[e.ColumnIndex].Name == "Operate")
                {
                    DgvFJ.Rows.RemoveAt(e.RowIndex);
                }
            }
        }

        private void btnAll_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dgvVendorEmail.Rows.Count; i++)
            {
                dgvVendorEmail["EmaiCheck", i].Value = true;
            }
        }
    }
}
