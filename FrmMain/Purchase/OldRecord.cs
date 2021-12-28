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
    public partial class OldRecord : Office2007Form
    {
        string UserID = string.Empty;
        public OldRecord(string id)
        {
            this.EnableGlass = false;
            MessageBoxEx.EnableGlass = false;
            InitializeComponent();
            UserID = id;
        }

        private void OldRecord_Load(object sender, EventArgs e)
        {

        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            string sql = @"SELECT
                                        T1.VendorNumber,
                                        T1.VendorName,
                                        T1.ManufacturerNumber,
                                        T1.ManufacturerName,
                                        T1.ItemNumber,
                                        T1.OperateDateTime,
                                        T1.Operator,
                                        T1.PONumber,
                                        T1.LineNumberString,
                                        T1.ForeignOrderNumber
                                        FROM
                                        dbo.PurchaseOrderByCMF T1
                                        WHERE
	                                        T1.Operator = '"+UserID+"' ";
            string sqlCriteria = string.Empty;
            if(rbtnFONumber.Checked)
            {
                sqlCriteria = " And ForeignOrderNumber = '" + tbNumber.Text + "' order by Id Desc";
            }
            else if(rbtnPONumber.Checked)
            {
                sqlCriteria = " And PONumber = '" + tbNumber.Text + "' order by Id Desc";
            }
            else
            {
                sqlCriteria = " And ItemNumber = '" + tbNumber.Text + "' order by Id Desc";
            }
            dgv.DataSource = SQLHelper.GetDataTable(GlobalSpace.FSDBConnstr, sql + sqlCriteria);
        }

        private void tbNumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar ==(char)13)
            {
                buttonX1_Click(sender, e);
            }
        }
    }
}
