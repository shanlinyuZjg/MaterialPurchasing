using Global.Helper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Global
{
    public partial class FSerror : Form
    {
        string FSID = string.Empty;
        public FSerror(string fsID)
        {
            InitializeComponent();
            FSID = fsID;
        }

        private void FSerror_Load(object sender, EventArgs e)
        {

        }

        private void BtnError_Click(object sender, EventArgs e)
        {
            string sqlSelect = @"Select Type AS 类型,ErrorContent AS 内容,OperateDateTime AS 日期 From FSErrorLogByCMF Where Operator='" + FSID + "' And Left(OperateDateTime,10)='" + dtpError.Value.ToString("yyyy-MM-dd") + "'  Order By OperateDateTime Desc";
            DGV1.DataSource = SQLHelper.GetDataTable(GlobalSpace.FSDBConnstr, sqlSelect);
        }
    }
}
