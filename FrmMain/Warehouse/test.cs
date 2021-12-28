using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Global;
using Global.Helper;


namespace Global.Warehouse
{
    public partial class test : Form
    {
        public test()
        {
            InitializeComponent();
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
			PrintPreview pp = new PrintPreview(GetPaymentRecord(0,"粉针事业部"), "\\付款通知单.grf", 0);
			pp.ShowDialog();
		}

		private System.Data.DataTable GetPaymentRecord(int status, string deptname)
		{
			string sqlSelect = @"SELECT
                                            T1.Id,
                                            T1.SequenceNumber AS 序号,
	                                        T1.DeptName AS 申请部门,
                                            T1.DeptOrgNumber AS 组织代码,
	                                        T1.CreateDate AS 提交日期,
	                                        T1.PersonInCharge AS 经办人,
                                                            (
		                                        CASE T1.Status
		                                        WHEN 0 THEN
			                                        '已提交'
		                                        WHEN 1 THEN
			                                        '已审核'
		                                        WHEN 2 THEN
			                                        '已付款'
		                                        END
	                                        ) AS 状态,
	                                        T1.UnitName AS 业务单位,
                                            T1.PayForm AS 付款方式,
	                                        T1.ProjectName AS 预算工程,
                                            T1.ProjectCode AS 工程代码,
	                                        T1.SubProjectName AS 项目,
                                             (
		                                        CASE T1.IsPrePayment
		                                        WHEN 0 THEN
			                                        '否'
		                                        ELSE
			                                        '是'
		                                        END
	                                        ) AS 预付款,     
	                                        T1.TotalAmount AS 总金额,
	                                        T1.TotalAmountCapitalForm AS 金额大写,
	                                        T1.VendorNumber AS 供应商码,
	                                        T1.VendorName AS 供应商名,
	                                        T1.VendorBankName AS 银行,
	                                        T1.VendorBankAccount AS 银行账户,
	                                        (
		                                        CASE T1.IsPlanned
		                                        WHEN 0 THEN
			                                        '是'
		                                        ELSE
			                                        '否'
		                                        END
	                                        ) AS 计划内,                                                
	                                        T1.PlanContent AS 计划内容,	                        
	                                        T1.PaymentType AS 类型,
	                                        T1.BudgetType AS 预算类型,
	                                        T1.AppledAmount AS 申请付款金额,
	                                        T1.ItemNumber AS 物料代码,
	                                        T1.ItemDescription AS 物料描述,
	                                        T1.ItemUM AS 单位,
	                                        T1.ItemQuantity AS 数量,
	                                        T1.ItemUnitPrice AS 单价,
	                                        T1.ItemTotalAmount AS 物料总金额,
                                            T1.ContractNumber AS 合同编码,
                                            T1.InvoiceNumber AS 发票号码
                                        FROM
	                                        dbo.BMSDepartmentPaymentHistory T1   ";
			string sqlCriteria = string.Empty;

			switch (status)
			{
				case 0:
				case 1:
					sqlCriteria = "  Where T1.Status < 2 And T1.DeptName = '" + deptname + "'";
					break;
				case 2:
					sqlCriteria = " Where T1.Status = 2 And T1.DeptName = '" + deptname + "'";
					break;
			}
			return SQLHelper.GetDataTable(GlobalSpace.FSDBConnstr, sqlSelect + sqlCriteria);
		}
	}
}
