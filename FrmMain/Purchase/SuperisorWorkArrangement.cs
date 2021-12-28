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
    public partial class SuperisorWorkArrangement : Office2007Form
    {
        public string userID = string.Empty;
        public SuperisorWorkArrangement(string id)
        {
            userID = id;
            this.EnableGlass = false;
            MessageBoxEx.EnableGlass = false;
            InitializeComponent();
        }

        private void SuperisorWorkArrangement_Load(object sender, EventArgs e)
        {
            CommonOperate.ComboBoxBind(cbbStaff, CommonOperate.GetSubordinate(userID), "Name", "UserID");
            dgvAllTask.DataSource = GetTask(userID, 9);
        }

        private void btnAssignTask_Click(object sender, EventArgs e)
        {
            if(dtpFinishDate.Value < dtpStartDate.Value)
            {
                MessageBoxEx.Show("截止日期不能早于开始日期", "提示");
            }
            else if(tbTaskSubject.Text =="" || rtbTaskDetail.Text =="")
            {
                MessageBoxEx.Show("任务标题和详情不能为空！", "提示");
            }
            else
            {
                string sqlInsert = @"Insert into PurchaseDepartmentTaskArrangementByCMF(SupervisorID,BuyerID,TaskSubject,TaskDetail,StartDate,FinishDate) values('"+userID+"','"+cbbStaff.SelectedValue.ToString()+"','"+tbTaskSubject.Text+"','"+rtbTaskDetail.Text+"','"+dtpStartDate.Value.ToString("yyyy-MM-dd")+"','"+dtpFinishDate.Value.ToString("yyyy-MM-dd")+"')";
                if(SQLHelper.ExecuteNonQuery(GlobalSpace.FSDBConnstr, sqlInsert) )
                {
                    MessageBoxEx.Show("任务下达成功！", "提示");
                    tbTaskSubject.Text = "";
                    cbbStaff.Text = ""; 
                    rtbTaskDetail.Text = "";
                    dgvAllTask.DataSource = GetTask(userID, 0);
                }
                else
                {
                    MessageBoxEx.Show("下达失败，请联系管理员查找原因！", "提示");
                }
            }
        }

        private DataTable GetTask(string userid,int status)
        {
            DataTable dtTemp = null;
            string sqlStatus = " And Status = "+status+"";
            string sqlOrder = " Order by OperateDateTime  Desc";
            string sqlSelect = @"Select BuyerName as 执行者,TaskSubject as 任务主题,TaskDetail as 任务详情,StartDate as 开始日期,FinishDate as 截止日期,(case when Status = 1 then '完成' else '未完成'  end)as 状态,Comment as 备注 From PurchaseDepartmentTaskArrangementByCMF  Where SupervisorID='" + userid + "'"; 
            switch(status)
            {
                case 0:
                    sqlSelect = sqlSelect + sqlStatus + sqlOrder;
                    break;
                case 1:
                    sqlSelect = sqlSelect + sqlStatus + sqlOrder;
                    break;
                case 9:
                    sqlSelect = sqlSelect + sqlOrder;
                    break;
                default:
                    break;
            }
            dtTemp = SQLHelper.GetDataTable(GlobalSpace.FSDBConnstr, sqlSelect);
            return dtTemp;
        }

        private void btnViewAll_Click(object sender, EventArgs e)
        {
            GetTask(userID, 9);
        }

        private void btnViewFinished_Click(object sender, EventArgs e)
        {
            GetTask(userID, 1);

        }

        private void btnUnfinished_Click(object sender, EventArgs e)
        {
            GetTask(userID, 0);

        }
    }
}
