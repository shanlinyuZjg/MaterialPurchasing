using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Global.Helper;
using System.Data.SqlClient;

namespace Global
{
    public partial class test : Form
    {
        DataTable dt = null;
        public test()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string sqlSelect = @"Select * From PORV";
            dt = SQLHelper.GetDataTable(GlobalSpace.FSDBConnstr, sqlSelect);
            MessageBox.Show("共计"+dt.Rows.Count.ToString()+"条数据！");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(GlobalSpace.FSDBConnstr))
                {
                    SqlBulkCopy bulkCopy = new SqlBulkCopy(conn);
                    bulkCopy.DestinationTableName = "PORV";
                    bulkCopy.BatchSize = dt.Rows.Count;
                    conn.Open();

                    if (dt != null && dt.Rows.Count != 0)
                    {
                        bulkCopy.WriteToServer(dt);
                    }
                }
                MessageBox.Show("添加成功！");
            }
            catch (Exception)
            {
                throw;
            }

        }
    }
}
