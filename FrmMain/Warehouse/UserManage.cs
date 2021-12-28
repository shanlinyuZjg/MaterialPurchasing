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
    public partial class UserManage : Office2007Form
    {
        public UserManage()
        {
            InitializeComponent();
            this.EnableGlass = false;
            MessageBoxEx.EnableGlass = false;
        }
        /*
        private void btnAddNewGroup_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(tbGroup.Text))
            {
                string sqlInsert = @"Insert Into StockGroup (Name,OperateDateTime) values ('" + tbGroup.Text.Trim() + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                string sqlCheck = @"Select Count(Id) from StockGroup Where Name='" + tbGroup.Text + "' And Status = 0";
                if (SQLHelper.Exist(GlobalSpace.EBRConnStr, sqlCheck))
                {
                    MessageBoxEx.Show("处于在用状态的该班组已存在！", "提示");
                    return;
                }
                if (SQLHelper.ExecuteNonQuery(GlobalSpace.EBRConnStr, sqlInsert))
                {
                    MessageBoxEx.Show("增加成功！", "提示");

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

  
        */
        private void UserManage_Load(object sender, EventArgs e)
        {     
            dgvGroupFile.DataSource = GetFile();
            dgvGroupFile.Columns["Id"].Visible = false;
            dgvGroupFile.Columns["Status"].Visible = false;
            dgvUser.DataSource = GetUser();
            dgvUser.Columns["Id"].Visible = false;
            dgvUser.Columns["Status"].Visible = false;
        }
        private DataTable GetFile()
        {
            string sqlSelect = @"Select Id, Stock AS 班组,FileTracedNumber AS 追溯文件编号,Edition AS 版本,(CASE  When Status = 0 then '在用' Else '废弃' End) AS 状态,EffectiveDate AS 生效日期,Status From ControlledFileEditionManage Where Status = 0";
            return SQLHelper.GetDataTable(GlobalSpace.EBRConnStr, sqlSelect);
        }
        private DataTable GetGroup()
        {
            string sqlSelect = @"Select Id, Name AS 班组,(CASE When Status= 0 THEN '在用'  ELSE '停用' END)AS 状态, OperateDateTime AS 操作时间,Status From StockGroup Where Status = 0";

            return SQLHelper.GetDataTable(GlobalSpace.EBRConnStr, sqlSelect);
        }
        private DataTable GetUser()
        {
            string sqlSelect = @"SELECT
	                                            Id,
	                                            UserID AS 代码,
	                                            RTRIM(UserName) AS 姓名,
	                                            InternalNumber AS 内部编号,
	                                            District AS 班组,
	                                            (
		                                            CASE
		                                            WHEN Status = 0 THEN
			                                            '在用'
		                                            ELSE
			                                            '停用'
		                                            END
	                                            ) AS 状态,Status
                                            FROM
	                                            StockKeeper Where Status = 0
                                            ORDER BY UserID ASC";

            return SQLHelper.GetDataTable(GlobalSpace.EBRConnStr, sqlSelect);
        }
        private void labelX7_Click(object sender, EventArgs e)
        {

        }

        private void btnRefreshGroupFile_Click(object sender, EventArgs e)
        {
            dgvUser.DataSource = GetUser();
            dgvUser.Columns["Id"].Visible = false;
            dgvUser.Columns["Status"].Visible = false;
        }

        private void btnAddUser_Click(object sender, EventArgs e)
        {
            List<string> idList = new List<string>();
            string fileTracedNumber = string.Empty;
            string fileEdition = string.Empty;
            string effectiveDate = string.Empty;
            string stock = string.Empty;
            foreach(DataGridViewRow dgvr in dgvGroupFile.Rows)
            {
                if(Convert.ToBoolean(dgvr.Cells["FileCheck"].Value))
                {
                    idList.Add(dgvr.Cells["Id"].Value.ToString());
                    fileTracedNumber = dgvr.Cells["追溯文件编号"].Value.ToString();
                    fileEdition = dgvr.Cells["版本"].Value.ToString();
                    effectiveDate = dgvr.Cells["生效日期"].Value.ToString();
                    stock = dgvr.Cells["班组"].Value.ToString();
                }
            }
            if (idList.Count == 0)
            {
                MessageBoxEx.Show("请选择一个班组！", "提示");
                return;
            }
            else if (idList.Count > 1)
            {
                MessageBoxEx.Show("一个库管员只能选择一个班组！", "提示");
                return;
            }
            
            if(string.IsNullOrWhiteSpace(tbUserID.Text) || string.IsNullOrWhiteSpace(tbUserName.Text) || string.IsNullOrWhiteSpace(tbInternalNumber.Text) || string.IsNullOrWhiteSpace(cbbType.Text))
            {
                MessageBoxEx.Show("用户ID、姓名、内部编号和类型都不能为空！", "提示");
                return;
            }

            string sqlCheckID = @"Select Count(Id) From StockKeeper Where UserID='"+tbUserID.Text.Trim()+"' And InternalNumber='"+tbInternalNumber.Text+"' And Status = 0";
            string sqlCheckInternalNumber = @"Select Count(Id) From StockKeeper Where  InternalNumber='" + tbInternalNumber.Text + "' And Status = 0";
            if (SQLHelper.Exist(GlobalSpace.EBRConnStr, sqlCheckID) || SQLHelper.Exist(GlobalSpace.EBRConnStr, sqlCheckInternalNumber))
            {
                MessageBoxEx.Show("库管员代码或内部号码已存在！", "提示");
            }
            else
            {
                string sqlInsert = @"INSERT INTO [RYStockEBR].[dbo].[StockKeeper] (
	                                            [UserID],
	                                            [InternalNumber],
	                                            [UserName],
	                                            [Type],
	                                            [FileTracedNumber],
	                                            [FileEdition],
	                                            [EffectiveDate],District
                                            )
                                            VALUES('"+tbUserID.Text+"','"+tbInternalNumber.Text+"','"+tbUserName.Text+"','"+cbbType.Text.Split('|')[0]+"','"+fileTracedNumber+"','"+fileEdition+"','"+effectiveDate+"','"+stock+"');";
                if(SQLHelper.ExecuteNonQuery(GlobalSpace.EBRConnStr,sqlInsert))
                {
                    MessageBoxEx.Show("增加成功！", "提示");
                    dgvUser.DataSource = GetUser();
                    dgvUser.Columns["Id"].Visible = false;
                    dgvUser.Columns["Status"].Visible = false;
                    tbInternalNumber.Text = "";
                    tbUserID.Text = "";
                    tbUserName.Text = "";
                    cbbType.Text = "";
                }
                else
                {
                    MessageBoxEx.Show("增加失败！", "提示");
                }
            }

        }

        private void btnInActivate_Click(object sender, EventArgs e)
        {
            List<string> sqlList = new List<string>();
            foreach(DataGridViewRow dgvr in dgvUser.Rows)
            {
                if(Convert.ToBoolean(dgvr.Cells["UserCheck"].Value))
                {                    
                    string sqlInsert = @"INSERT INTO [RYStockEBR].[dbo].[StockGroupKeeperRecord] (
	                                                                                                [UserID],
	                                                                                                [InternalNumber],
	                                                                                                [Stock],
	                                                                                                [UserName],
	                                                                                                [Type],
	                                                                                                [IsVial],
	                                                                                                [IsDirectERP],
	                                                                                                [IsERP],
	                                                                                                [RecordArea],
	                                                                                                [FileTracedNumber],
	                                                                                                [FileEdition],
	                                                                                                [EffectiveDate],
	                                                                                                [District],
	                                                                                                ChangeReason,
	                                                                                                UpdateDateTime
                                                                                                ) SELECT
	                                                                                                UserID,
	                                                                                                InternalNumber,
	                                                                                                Stock,
	                                                                                                UserName,
	                                                                                                Type,
	                                                                                                IsVial,
	                                                                                                IsDirectERP,
	                                                                                                IsERP,
	                                                                                                RecordArea,
	                                                                                                FileTracedNumber,
	                                                                                                FileEdition,
	                                                                                                EffectiveDate,
	                                                                                                District,'删除用户','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")+"'  FROM  dbo.StockKeeper  WHERE    Id = " + Convert.ToInt32(dgvr.Cells["Id"].Value)+"";
                    string sqlDelete = @"Delete From  StockKeeper Where Id = " + Convert.ToInt32(dgvr.Cells["Id"].Value) + "";
                    sqlList.Add(sqlInsert);
                    sqlList.Add(sqlDelete);
                }
            }
            if(SQLHelper.BatchExecuteNonQuery(GlobalSpace.EBRConnStr,sqlList))
            {
                MessageBoxEx.Show("删除成功！", "提示");
                dgvUser.DataSource = GetUser();
                dgvUser.Columns["Id"].Visible = false;
                dgvUser.Columns["Status"].Visible = false;
            }
            else
            {
                MessageBoxEx.Show("删除失败！", "提示");
            }
        }

        private void btnHistory_Click(object sender, EventArgs e)
        {

        }
    }
}
