using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Global.Finance
{
    public partial class FinanceAP : Form
    {
        DataSet dsTemp = null;
        DataRelation OrderAndDetailRelation = null;
        public FinanceAP()
        {
            InitializeComponent();
        }

        private void Finance_Load(object sender, EventArgs e)
        {
            BindPurchaseData();
        }

        private void BindPurchaseData()
        {
            dsTemp = new DataSet();

            string strSelectOrder = @"Select * from POOrders";
            string strSelectOrderDetails = @"Select * from POOrderDetails";

            using (OleDbConnection conn = new OleDbConnection(GlobalSpace.FSDBConnstr))
            {
                OleDbDataAdapter adapter = new OleDbDataAdapter(strSelectOrder, conn);
                adapter.Fill(dsTemp, "POOrders");
                new OleDbDataAdapter(strSelectOrderDetails, conn).Fill(dsTemp, "POOrderDetails");
                OrderAndDetailRelation = dsTemp.Relations.Add("OrderAndDetail", dsTemp.Tables["POOrders"].Columns["PONumber"], dsTemp.Tables["POOrderDetails"].Columns["PONumber"], false);
                sgc.PrimaryGrid.DataSource = dsTemp;
                sgc.PrimaryGrid.DataMember = "POOrders";
            }
        }
    }
}
