using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Global.Helper;


namespace Global.Warehouse
{
    public partial class Stock2 : Form
    {
        public Stock2()
        {
            InitializeComponent();
        }

        private void Stock2_Load(object sender, EventArgs e)
        {
            string sqlSelect = @"Select * from FinanceRefundRecordByCMF";
            dgv.DataSource = SQLHelper.GetDataTable(GlobalSpace.FSDBConnstr, sqlSelect);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(dgv.SelectedRows.Count > 0)
            {
                int iIndex = dgv.SelectedCells[0].RowIndex;
                DataTable dt = (DataTable)dgv.DataSource;
                DataTable dtTemp = dt.Copy();
                string strId = dgv.SelectedRows[0].Cells["Id"].Value.ToString();
                if (Convert.ToInt32(tbNum.Text) == 1)
                {
                    DataRow[] drs = dtTemp.Select("Id = '" + strId + "'");
                    DataRow dr = dt.NewRow();
                    dr.ItemArray = drs[0].ItemArray;
                    dr["Id"] = "0";
                    dt.Rows.InsertAt(dr, iIndex+1);
                }
                else
                {
                    int iMax = Convert.ToInt32(tbNum.Text);
                    for(int i = 0;i < iMax; i++)
                    {
                        DataRow[] drs = dtTemp.Select("Id = '" + strId + "'");
                        DataRow dr = dt.NewRow();
                        dr.ItemArray = drs[0].ItemArray;
                        dr["Id"] = "0";
                        dt.Rows.InsertAt(dr, iIndex+i+1);
                    }
                }
            }
            else
            {
                Custom.MsgEx("没有选中的行！");
            }
          
        }
    }
}
