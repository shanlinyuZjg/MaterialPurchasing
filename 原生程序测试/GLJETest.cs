using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Global.Helper;
using Global;
namespace 原生程序测试
{
    public partial class GLJETest : Form
    {
        //字符集
        Encoding GB2312 = Encoding.GetEncoding("gb2312");
        Encoding ISO88591 = Encoding.GetEncoding("iso-8859-1");
        public GLJETest()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Show(tbBatchNo.Text.Trim());
        }

        void Show(string batchNo)
        {
            string sqlSelect = @"SELECT
                                                T1._Row,
	                                            T1.GDACTNO,
	                                            T1.GDAMOUNT,
	                                            T1.GDDESCRP AS Desp,
	                                            T1.GDCRDBI,
	                                            T1.GDAMOUNT
                                            FROM
	                                            Fin_GLDTL T1
                                            WHERE
	                                            T1.GDBTCHNO = '" + batchNo + "'";
            dgvDetail.DataSource = SQLHelper.GetDataTableOleDb(GlobalSpace.oledbconnstrFSDBTR, sqlSelect);
        }

        string Cn2Latin(string cnStr)
        {
            return ISO88591.GetString(GB2312.GetBytes(cnStr));
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            List<string> sqlList = new List<string>();

            foreach(DataGridViewRow dgvr in dgvDetail.Rows)
            {
                if(Convert.ToBoolean(dgvr.Cells["Check"].Value))
                {
                    string sqlUpdate = @"Update Fin_GLDTL Set GDDESCRP='"+dgvr.Cells["Desp"].Value.ToString()+"' Where _Row='"+dgvr.Cells["_Row"].Value.ToString()+"'";
                    sqlList.Add(sqlUpdate);
                }
            }
           if(SQLHelper.BatchExecuteNonQuery(GlobalSpace.connstrFSDBTR,sqlList))
            {
                MessageBox.Show("更新成功！", "提示");
                Show(tbBatchNo.Text.Trim());
                
            }
           else
            {
                MessageBox.Show("更新失败！", "提示");
            }
        }

        private void btnConvertCn2Latin_KeyPress(object sender, KeyPressEventArgs e)
        {
           
        }

        private void btnConvertCn2Latin_Click(object sender, EventArgs e)
        {
            if(!string.IsNullOrWhiteSpace(tbCn.Text))
            {
                tbLatin.Text = Cn2Latin(tbCn.Text);
            }
            
        }

        private void tbCn_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!string.IsNullOrEmpty(tbCn.Text))
            {
                if (e.KeyChar == (char)13)
                {
                    tbLatin.Text = Cn2Latin(tbCn.Text);
                }
            }
        }

        private void tbBatchNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(!string.IsNullOrWhiteSpace(tbBatchNo.Text))
            {
                if(e.KeyChar == (char)13)
                {
                    Show(tbBatchNo.Text.Trim());
                }
            }
        }
    }
}
