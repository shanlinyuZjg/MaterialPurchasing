using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using Global;
using Global.Helper;


namespace Global.Warehouse
{
    public partial class FileEditionManage : Office2007Form
    {
        public FileEditionManage()
        {
            this.EnableGlass = false;
            MessageBoxEx.EnableGlass = false;
            InitializeComponent();
        }

        private void FileEditionManage_Load(object sender, EventArgs e)
        {
            dgvGroup.DataSource = GetGroup();
            dgvGroup.Columns["Id"].Visible = false;
            dgvFile.DataSource = GetFile();
            dgvFile.Columns["Id"].Visible = false;
     //       dgvFile.Columns["Status"].Visible = false;
            tbEffectiveDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
        }

        private DataTable GetGroup()
        {
            string sqlSelect = @"Select Id, Name AS 班组,(CASE When Status= 0 THEN '在用'  ELSE '停用' END)AS 状态 From StockGroup Where Status = 0";            
            return SQLHelper.GetDataTable(GlobalSpace.EBRConnStr, sqlSelect);
        }
        
        private void btnAddNewGroup_Click(object sender, EventArgs e)
        {
            if(!string.IsNullOrWhiteSpace(tbGroup.Text))
            {
                string sqlInsert = @"Insert Into StockGroup (Name,OperateDateTime) values ('"+tbGroup.Text.Trim()+"','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                string sqlCheck = @"Select Count(Id) from StockGroup Where Name='"+tbGroup.Text+"' And Status = 0";
                if(SQLHelper.Exist(GlobalSpace.EBRConnStr,sqlCheck))
                {
                    MessageBoxEx.Show("处于在用状态的该班组已存在！", "提示");
                    return;
                }
                if(SQLHelper.ExecuteNonQuery(GlobalSpace.EBRConnStr,sqlInsert))
                {
                    MessageBoxEx.Show("增加成功！", "提示");
                    dgvGroup.DataSource = GetGroup();
                    dgvGroup.Columns["Id"].Visible = false;
                    dgvGroup.Columns["Status"].Visible = false;
                    tbGroup.Text = "";
                }
                else
                {
                    MessageBoxEx.Show("增加失败！", "提示");
                }
            }
            else
            {
                MessageBoxEx.Show("班组名不能为空！", "提示");
            }

        }

        private void btnInactivateGroup_Click(object sender, EventArgs e)
        {
            List<string> sqlList = new List<string>();
            foreach(DataGridViewRow dgvr in dgvGroup.Rows)
            {
                if (Convert.ToBoolean(dgvr.Cells["GroupCheck"].Value))
                {               
                        string sqlUpdate = @"Update StockGroup Set Status= 1,OperateDateTime='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'  Where Id =" + Convert.ToInt32(dgvr.Cells["Id"].Value)+"";
                        sqlList.Add(sqlUpdate);
                }
            }

            if(SQLHelper.BatchExecuteNonQuery(GlobalSpace.EBRConnStr,sqlList))
            {
                MessageBoxEx.Show("更新成功！", "提示");
                dgvGroup.DataSource = GetGroup();
                dgvGroup.Columns["Id"].Visible = false;
          //      dgvGroup.Columns["状态"].Visible = false;
            }
            else
            {
                MessageBoxEx.Show("更新失败！", "提示");
            }
        }

        
        private void btnFileEditionAdd_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrWhiteSpace(tbTracedFileNumber.Text) || string.IsNullOrWhiteSpace(tbFileEdition.Text) || string.IsNullOrWhiteSpace(tbEffectiveDate.Text))
            {
                MessageBoxEx.Show("追溯文件编号、版本号和生效日期不能为空！", "提示");
                return;
            }

            List<string> sqlList = new List<string>();
            bool isExistRecord = false;
            foreach (DataGridViewRow dgvr in dgvGroup.Rows)
            {
                if (Convert.ToBoolean(dgvr.Cells["GroupCheck"].Value))
                {
                        string sqlCheck = @"Select Count(Id) From ControlledFileEditionManage Where Status = 0 And FileTracedNumber='" + tbTracedFileNumber.Text.Trim() + "' And Edition='"+ tbFileEdition.Text.Trim() + "' ";
                        if(!SQLHelper.Exist(GlobalSpace.EBRConnStr,sqlCheck))
                        {
                            string sqlInsert = @"INSERT INTO [RYStockEBR].[dbo].[ControlledFileEditionManage] (
	                                                                    [Stock],
	                                                                    [FileTracedNumber],
	                                                                    [Edition],
	                                                                    [OperateDateTime],EffectiveDate
                                                                    )
                                                                    VALUES('" + dgvr.Cells["班组"].Value.ToString() + "','" + tbTracedFileNumber.Text.Trim() + "','" + tbFileEdition.Text.Trim() + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','"+tbEffectiveDate.Text.Trim()+"')";
                            sqlList.Add(sqlInsert);
                        }         
                        else
                        {
                            isExistRecord = true;
                        }
                }
            }

            if (isExistRecord)
            {
                MessageBoxEx.Show("已存在该版本文件！", "提示");
                return;
            }

            if (sqlList.Count == 0)
            {
                MessageBoxEx.Show("请选择班组！", "提示");
                return;
            }

            if(SQLHelper.BatchExecuteNonQuery(GlobalSpace.EBRConnStr,sqlList))
            {
                MessageBoxEx.Show("增加成功！", "提示");
                dgvFile.DataSource = GetFile();
                dgvFile.Columns["Id"].Visible = false;         
                tbFileEdition.Text = "";
                tbTracedFileNumber.Text = "";
            }
            else
            {
                MessageBoxEx.Show("增加失败！", "提示");
            }

        }

        private DataTable GetFile()
        {
            string sqlSelect = @"Select Id, Stock AS 班组,FileTracedNumber AS 追溯文件编号,Edition AS 版本,(CASE  When Status = 0 then '在用' Else '作废' End) AS 状态,EffectiveDate AS 失效日期 From ControlledFileEditionManage Where Status = 0";
            return SQLHelper.GetDataTable(GlobalSpace.EBRConnStr, sqlSelect);
        }

        private void btnFileEditionAbolish_Click(object sender, EventArgs e)
        {
            List<string> sqlList = new List<string>();
            foreach(DataGridViewRow dgvr in dgvFile.Rows)
            {
                if(Convert.ToBoolean(dgvr.Cells["FileCheck"].Value))
                {
                        string sqlUpdate = @"Update ControlledFileEditionManage Set Status = 1,OperateDateTime='"+DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")+"',EffectiveDate='"+ DateTime.Now.ToString("yyyy-MM-dd") + "' Where Id = '"+Convert.ToInt32(dgvr.Cells["Id"].Value)+"'";
                        sqlList.Add(sqlUpdate);
                }
            }

            if (SQLHelper.BatchExecuteNonQuery(GlobalSpace.EBRConnStr, sqlList))
            {
                MessageBoxEx.Show("更新成功！", "提示");
                dgvFile.DataSource = GetFile();
                dgvFile.Columns["Id"].Visible = false;
        //        dgvFile.Columns["Status"].Visible = false;

            }
            else
            {
                MessageBoxEx.Show("更新失败！", "提示");
            }

        }
    }
}
