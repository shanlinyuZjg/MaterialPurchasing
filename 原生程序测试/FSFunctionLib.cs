using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SoftBrands.FourthShift.Transaction;


namespace 原生程序测试
{
    class FSFunctionLib
    {
        public static FSTIClient fstiClient = null;
        public static bool FSConfigFileInitialize(string strConfigFilePath, string userid, string password)
        {
            try
            {
                fstiClient = new FSTIClient();
                fstiClient.InitializeByConfigFile(strConfigFilePath, true, false);
                if (fstiClient.IsLogonRequired)
                {
                    if (FSLogin(userid, password) != false)
                    {
                        return true;
                    }
                    else
                    {
                        MessageBox.Show("账号或密码错误，请确认！");
                        return false;
                    }
                }
                else
                {
                    return true;
                }
            }
            catch (FSTIApplicationException exception)
            {
                MessageBox.Show(exception.Message, "FSTI程序异常");
                FSExit();
            }

            return false;
        }

        public static bool FSLogin(string userid, string password)
        {
            string message = string.Empty;
            int status = 0;
            try
            {
                status = fstiClient.Logon(userid, password, ref message);

                if (status > 0)
                {
                    //           MessageBox.Show("账号或密码错误，请确认！");
                    //      FSFunctionLib.ErrorMsg("错误原因：");
                    FSTIError error = fstiClient.TransactionError;
                    MessageBox.Show("错误原因：" + error.Description);
                }
                //以下代码测试用，后期删除
                else
                {
                    MessageBox.Show("登陆成功！");
                    return true;
                }
            }
            catch (FSTIApplicationException exception)
            {
                MessageBox.Show(exception.Message, "FSTI程序异常");
                fstiClient.Terminate();
                fstiClient = null;
            }
            return false;
        }

        public static void FSExit()
        {
            //if(fstiClient.Status == TransactionStatus.fsSupportedAndDisabled)
            //{

            if (fstiClient != null)
            {
                MessageBox.Show("当前是在线状态，即将退出！");
                fstiClient.Terminate();
                fstiClient = null;
            }
            //}
            //else
            //{
            //    MessageBox.Show("fstiException");
            //}
        }

        public static void FSErrorMsg(string strMgs)
        {
            //定义简体中文和西欧文编码字符集
            Encoding GB2312 = Encoding.GetEncoding("gb2312");
            Encoding ISO88591 = Encoding.GetEncoding("iso-8859-1");

            FSTIError error = fstiClient.TransactionError;
            MessageBox.Show(strMgs + ":" + GB2312.GetString(ISO88591.GetBytes(error.Description)));
        }
    }
}
