using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Global;


namespace 付款单权限增加
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrWhiteSpace(tbLoginName.Text) || string.IsNullOrWhiteSpace(tbOrgCode.Text) || string.IsNullOrWhiteSpace(tbOrgName.Text) || string.IsNullOrWhiteSpace(tbUserID.Text))
            {
                MessageBox.Show("不能有空项！", "提示");
                return;
            }
            //1.检查单位是否已经添加到系统中，没有则添加
            string sqlSelect = @"Select Count(Id) From BMSDepartmentDeptMapping Where DeptOrgNumber='"+tbOrgCode.Text+"'";
            if(!SQLHelper.Exist(GlobalSpace.FSDBConnstr,sqlSelect))
            {
                string sqlInsert = @"Insert Into BMSDepartmentDeptMapping (DeptOrgName,DeptOrgNumber) Values ('"+tbOrgName.Text+"','"+tbOrgCode.Text+"')";
                if(!SQLHelper.ExecuteNonQuery(GlobalSpace.FSDBConnstr,sqlInsert))
                {
                    MessageBox.Show("添加组织信息时出错！", "提示");
                    return;
                }
            }
            //2.根据用户的平台中的单位信息，增加登录部门信息
            string sqlInsert2 = @"Insert Into BMSDepartmentPaymentDeptUser (UserID,LoginDeptName) Values ('" + tbUserID.Text + "','" + tbLoginName.Text + "')";
            if (!SQLHelper.ExecuteNonQuery(GlobalSpace.FSDBConnstr, sqlInsert2))
            {
                MessageBox.Show("添加用户登录信息时出错！", "提示");
                return;
            }
            //3.增加部门映射表中的信息
            string sqlInsert3 = @"INSERT INTO [FSDB].[dbo].[BMSDepartmentPaymentDeptMapping] (
	                                                    [DeptOrgName],
	                                                    [UnitName],
	                                                    [UserID],
	                                                    [LoginDeptName],
	                                                    [BigClasses],
	                                                    [DeptOrgNumber])
                                                    VALUES
	                                                    ( '"+tbOrgName.Text+"', '"+tbOrgName.Text+"', '"+tbUserID.Text+"', '"+tbLoginName.Text+"',  '预算内|预算外|费用', '"+tbOrgCode.Text+"');";
            if (SQLHelper.ExecuteNonQuery(GlobalSpace.FSDBConnstr, sqlInsert3))
            {
                MessageBox.Show("添加成功！", "提示");
                tbLoginName.Text = "";
                tbOrgCode.Text = "";
                tbOrgName.Text = "";
                tbUserID.Text = "";
            }
            else
            {
                MessageBox.Show("添加用户部门映射信息时出错！", "提示");
            }    

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            DataTable dt = GetOrgDataTable();
            DataTable dtShow = dt.Clone();
            DataRow[] drs = dt.Select("组织名称 Not Like '%删除%'");
            DataRow dr = dt.NewRow();
            for(int i = 0;i < drs.Length;i++)
            {
                dr = drs[i];
                dtShow.Rows.Add(dr.ItemArray);
            }
            dgvDetail.DataSource = dtShow;
        }

        //过滤生成表
        private DataTable MakeDataTable(DataTable dt,string filter)
        {
            DataTable dtShow = dt.Clone();
            DataRow[] drs = dt.Select(filter);
            DataRow dr = dt.NewRow();
            for (int i = 0; i < drs.Length; i++)
            {
                dr = drs[i];
                dtShow.Rows.Add(dr.ItemArray);
            }

            return dtShow;
        }

        private DataTable GetOrgDataTable()
        {
            string sqlSelect = @"SELECT
	                                        GOMSTORG AS 组织代码,
	                                        GORGDESC AS 组织名称
                                        FROM
	                                        FSDBMR.dbo.Fin_GACTORG T1
                                        WHERE
	                                      GOMSTORG LIKE '1___-__-___'
                                        AND GOMSTORG NOT LIKE '1A__-__-___'
                                        AND GOMSTORG NOT LIKE '15__-__-___'
                                        AND GOMSTORG NOT LIKE '16__-__-___'
                                        AND GOMSTORG NOT LIKE '17__-__-___'
                                        AND GOMSTORG NOT LIKE '18__-__-___'
                                        AND GOMSTORG NOT LIKE '19__-__-___'
AND GOMSTORG NOT LIKE '10__-__-___'
                                        ";
            return SQLHelper.GetDataTableOleDb(GlobalSpace.oledbconnstrFSDBMR, sqlSelect);
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void dgvDetail_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            dgvDetail_CellClick(sender, e);
        }

        private void dgvDetail_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            tbOrgCode.Text = dgvDetail.Rows[dgvDetail.CurrentCell.RowIndex].Cells["组织代码"].Value.ToString();
            tbOrgName.Text = dgvDetail.Rows[dgvDetail.CurrentCell.RowIndex].Cells["组织名称"].Value.ToString();
        }

        private void tbFind_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar == (char)13)
            {
                if(!string.IsNullOrWhiteSpace(tbFind.Text))
                {
                    string filter = @"组织名称 like '%"+tbFind.Text+"%'";
                    DataTable dt = GetOrgDataTable();
                    dgvDetail.DataSource = MakeDataTable(dt,filter);
                    tbFind.Text = "";
                }
            }
        }

        private void tbLoginName_TextChanged(object sender, EventArgs e)
        {
            tbLoginName.Text = tbLoginName.Text.ToUpper();
            tbLoginName.SelectionStart = tbLoginName.Text.Length;
        }
    }
}
