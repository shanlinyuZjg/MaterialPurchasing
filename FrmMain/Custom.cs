using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;


namespace Global
{
    public class Custom
    {
        public static void MsgEx(string strText)
        {
            MessageBoxEx.Show(strText, "提示");
        }
        public static void Msg(string strText)
        {
            MessageBox.Show(strText, "提示");
        }
    }
}
