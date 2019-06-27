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

namespace Global.Finance
{
    public partial class FinanceAR : Office2007Form
    {

        public FinanceAR()
        {
            InitializeComponent();
        }

        private void FinanceAR_Load(object sender, EventArgs e)
        {
            FSFunctionLib.FSConfigFileInitialize("Z:\\Mfgsys\\fs.cfg", "F24", "123456");
        }

        private void btnLoadInvoiceInfo_Click(object sender, EventArgs e)
        {
            string strSelect = @"SELECT 单据编号,发票号码,合计金额,合计税额 FROM InvoiceInfo";
            dgvInvoiceInfo.DataSource = SQLHelper.GetDataTable(GlobalSpace.FSDBConnstr, strSelect);
        }
    }
}
