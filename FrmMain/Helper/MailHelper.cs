using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Windows.Forms;

namespace Global.Helper
{
     class MailHelper
    {
        public static bool SendEmailWithAttachment(Email email,string filePath)
        {
            bool bSucceed = false;
            MailMessage mmsg = new MailMessage();
            try
            {                            
                mmsg.From = new MailAddress(email.fromEmail,email.fromPerson);
                mmsg.To.Add(email.toEmail);
                mmsg.Subject = email.emailTitle;
                mmsg.Body = email.emailContent;
                //IsBodyHtml为True，如果邮件内容中有需要换行等操作的，使用<br>来换行或者其他的标识符
                mmsg.IsBodyHtml = true;
                Attachment mailAttach = new Attachment(filePath);
                mmsg.Attachments.Add(mailAttach);
                mmsg.BodyEncoding =System.Text.Encoding.GetEncoding(email.encoding);
                mmsg.Priority = MailPriority.High;
                SmtpClient smtp = new SmtpClient();
                smtp.Host = email.smtpServer;
                smtp.Credentials = new NetworkCredential(email.userName, email.passWord);
                smtp.Send(mmsg);
                mmsg.Dispose();
                bSucceed = true;
            }
            catch (Exception)
            {
                throw;
            }
            return bSucceed;
        }
        public static bool SendReminderEmail(Email email)
        {
            bool bSucceed = false;
            MailMessage mmsg = new MailMessage();
            try
            {
                mmsg.From = new MailAddress(email.fromEmail, email.fromPerson);
                mmsg.To.Add(email.toEmail);
                mmsg.Subject = email.emailTitle;
                mmsg.Body = email.emailContent;
                //IsBodyHtml为True，如果邮件内容中有需要换行等操作的，使用<br>来换行或者其他的标识符
                mmsg.IsBodyHtml = true;         
                mmsg.BodyEncoding = System.Text.Encoding.GetEncoding(email.encoding);
                mmsg.Priority = MailPriority.High;
                SmtpClient smtp = new SmtpClient();
                smtp.Host = email.smtpServer;
                smtp.Credentials = new NetworkCredential(email.userName, email.passWord);
                smtp.Send(mmsg);
                mmsg.Dispose();
                bSucceed = true;
            }
            catch (Exception)
            {
                throw;
            }
            return bSucceed;
        }
        public static void SendEmail(Email email,Dictionary<string,string> emailList)
        {
            string nameList = string.Empty;
            try
            {
                foreach(var item in emailList)
                {
                    nameList += item.Value+"-";
                    MailAddress from = new MailAddress(email.fromEmail, email.fromPerson);
                    MailAddress to = new MailAddress(item.Key, item.Value);
                    MailMessage mmsg = new MailMessage(from, to);
                    mmsg.Subject = email.emailTitle;
                    //由于邮件内容默认是按照html文件格式输出，因此对于换行和空格必须使用html的代码，而不是C#自身的\r\n方式.如果是采用的文本格式，则可以使用C#自身。mmsg.BodyFormat = MailFormat.Html/MailFormat.Text
                    mmsg.Body = item.Value+ ":<BR>&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp" + email.emailContent;
                    mmsg.IsBodyHtml = true;
                    mmsg.BodyEncoding = System.Text.Encoding.GetEncoding(email.encoding);
                    mmsg.Priority = MailPriority.High;
                    SmtpClient smtp = new SmtpClient(email.smtpServer, Convert.ToInt32(email.smtpPort));
                    smtp.Credentials = new NetworkCredential(email.userName, email.passWord);
                    smtp.Send(mmsg);
                    mmsg.Dispose();
                }
                MessageBox.Show("给"+nameList+"发送邮件成功！","提示");          
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public static void SendMailTest(string senderMail, string smtpServer, string smtpServerPort, string senderUserName, string senderUserPass, string UserMail, string MailTitle, string MailContent)
        {
            try
            {
                System.Net.Mail.MailMessage myMail = new System.Net.Mail.MailMessage();
                myMail = new System.Net.Mail.MailMessage(senderMail, UserMail);//发件人邮箱，收件人邮箱
                myMail.Subject = MailTitle;//"服务停滞，请及时处理";
                myMail.Body = MailContent;//txtEContent.Text.Trim();邮件正文

                System.Net.Mail.SmtpClient client = new System.Net.Mail.SmtpClient(smtpServer, Convert.ToInt32(smtpServerPort));//smtp服务器，端口
                client.Credentials = new System.Net.NetworkCredential(senderUserName, senderUserPass);//发件邮箱用户名，密码
                client.Send(myMail);
                            MessageBox.Show("邮件发送成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
             
            }
            catch (Exception ex)
            {
                          MessageBox.Show(ex.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
              
            }
        }

        public static void SendMailTest2()
        {
            MailAddress from = new MailAddress("changmingfu@reyoung.com");
            MailAddress to= new MailAddress("changmingfu@reyoung.com");
            MailMessage msg1 = new MailMessage(from, to);
           
            SmtpClient client = new SmtpClient("192.168.8.3", 25);
            client.Credentials = CredentialCache.DefaultNetworkCredentials;
            client.Credentials = new NetworkCredential("changmingfu@reyoung.com", "xxxxyyyzzz123!");

            msg1.Subject = "ERROR";
            msg1.Body = "来源：XXXX ";

            client.Send(msg1);
        }
    }

    class Email
    {
        public string fromEmail { get; set; }
        public string fromPerson { get; set; }
        public string toEmail { get; set; }
        public string toPerson { get; set; }
        public string encoding { get; set; }
        public string smtpServer { get; set; }
        public string smtpPort { get; set; }
        public string userName { get; set; }
        public string passWord { get; set; }
        public string emailTitle { get; set; }
        public string emailContent { get; set; }

    }
}
