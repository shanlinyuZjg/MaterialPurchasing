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
    public partial class WorkArrangement : Office2007Form
    {
        string userID = string.Empty;
        string userName = string.Empty;
        public WorkArrangement(string strID,string strName)
        {
            userID = strID;
            userName = strName;
            this.EnableGlass = false;
            MessageBoxEx.EnableGlass = false;
            InitializeComponent();
        }

        private void WorkArrangement_Load(object sender, EventArgs e)
        {
            dgvTask.DataSource = GetTask("UnFinished",userID);
            dgvTask.Columns["Id"].Visible = false;
        }

        private DataTable GetTask(string status,string strid)
        {
            DataTable dtTemp = null;    
            switch (status)
            {
                case "UnFinished":
                     string sqlSelectUnFinished = @"Select Id,TaskSubject as 主题,StartDate as 开始日期,FinishDate as 截止日期,OperateDateTime  as 下达日期 From PurchaseDepartmentTaskArrangementByCMF Where Status=0 And BuyerID='"+strid+"'";
                    dtTemp = SQLHelper.GetDataTable(GlobalSpace.FSDBConnstr, sqlSelectUnFinished);
                    break;
                case "Finished":
                     string sqlSelectFinished = @"Select Id, TaskSubject as 主题, StartDate as 开始日期,FinishDate as 截止日期,OperateDateTime  as 下达日期 From PurchaseDepartmentTaskArrangementByCMF Where Status=1 And BuyerID='" + strid + "'";
                    dtTemp = SQLHelper.GetDataTable(GlobalSpace.FSDBConnstr, sqlSelectFinished);
                    break;
                case "All":
                     string sqlSelectAll = @"Select Id,TaskSubject as 主题,StartDate as 开始日期,FinishDate as 截止日期,OperateDateTime  as 下达日期 From PurchaseDepartmentTaskArrangementByCMF Where   BuyerID='" + strid + "' ";
                    dtTemp = SQLHelper.GetDataTable(GlobalSpace.FSDBConnstr, sqlSelectAll);
                    break;
                default:
                    string sqlSelectDefault = @"Select Id,TaskSubject as 主题, StartDate as 开始日期,FinishDate as 截止日期,OperateDateTime  as 下达日期 From PurchaseDepartmentTaskArrangementByCMF Where Status=0 And BuyerID='" + strid + "' ";
                    dtTemp = SQLHelper.GetDataTable(GlobalSpace.FSDBConnstr, sqlSelectDefault);
                    break;
            }
            return dtTemp;
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            if (rbUnFinished.Checked)
            {
                dgvTask.DataSource = GetTask("UnFinished",userID);
            }
            else if (rbFinished.Checked)
            {
                dgvTask.DataSource = GetTask("Finished", userID);
            }
            else if (rbAll.Checked)
            {
                dgvTask.DataSource = GetTask("All", userID);
            }
        }

        private void dgvTask_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void dgvTask_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
           
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            string strId = dgvTask.CurrentRow.Cells["Id"].Value.ToString();
            string sqlUpdate = @"update PurchaseDepartmentTaskArrangementByCMF Set Status = 1  Where Id='" + strId + "'";

            if (rtbFinishedTaskComment.Text =="")
            {
                if (DialogResult.Yes == MessageBoxEx.Show("没有填写任务完成情况，确定提交么？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2))
                {                                  
                    if(SQLHelper.ExecuteNonQuery(GlobalSpace.FSDBConnstr,sqlUpdate) )
                    {
                        MessageBoxEx.Show("提交成功！", "提示");
                    }
                    else
                    {
                        MessageBoxEx.Show("提交失败！", "提示");
                    }
                }
            }
            else
            {
                if (SQLHelper.ExecuteNonQuery(GlobalSpace.FSDBConnstr, sqlUpdate) )
                {
                    MessageBoxEx.Show("提交成功！", "提示");
                }
                else
                {
                    MessageBoxEx.Show("提交失败！", "提示");
                }
            }

        }

        private void dgvTask_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            dgvTask_CellDoubleClick(sender, e);
        }

        private void dgvTask_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            string strId = dgvTask.CurrentRow.Cells["Id"].Value.ToString();
            string sqlSelect = @"Select TaskSubject,TaskDetail,Status From PurchaseDepartmentTaskArrangementByCMF Where Id='" + strId + "' And BuyerID = '" + userID + "'";
            DataTable dtTemp = SQLHelper.GetDataTable(GlobalSpace.FSDBConnstr, sqlSelect);
            if (dtTemp.Rows.Count > 0)
            {
                tbTaskSubject.Text = dtTemp.Rows[0]["TaskSubject"].ToString();
                rtbTaskDetail.Text = dtTemp.Rows[0]["TaskDetail"].ToString();
                if(dtTemp.Rows[0]["Status"].ToString() == "1")
                {
                    rtbFinishedTaskComment.Text = "已完成";
                    btnSubmit.Enabled = false;
                }
            }
        }
    }
}
