using Global.Helper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Global.Warehouse
{
    public partial class ManageBatchRecordUpdateHistory : Form
    {
        public ManageBatchRecordUpdateHistory()
        {
            InitializeComponent();
        }
        private string UserId = string.Empty;
        private string UserName = string.Empty;
        public ManageBatchRecordUpdateHistory(string id,string name)
        {
            InitializeComponent();
            UserId = id;
            UserName = name;
        }

        private void Btnchazhao_Click(object sender, EventArgs e)
        {
            string Sqlstr = @"SELECT ModifyContent as 修改内容,Modifier as 修订人,RevisionReason as 修订原因,ModifyDateTime as 修改时间,
	                                                FileNumber AS 受控流水号,ApplyDate AS 请验日期,
	                                                VendorName AS 供应商名,
	                                                ManufacturerName AS 生产商名,
	                                                ItemNumber AS 物料编码,
	                                                ItemDescription AS 品名,
                                                    LineUM AS 单位,
	                                                LotNumber AS 批号,
	                                                InternalLotNumber AS 公司批号,
	                                                ManufacturedDate AS 生产日期,
	                                                ExpiredDate AS 过期日期,
	                                                ReceiveQuantity AS 入库数量,
	                                                PackageQuantity AS 整包件数,
	                                                PackageSpecification AS 包装规格,	                                                
	                                                PackageOdd AS 零头标示值,
                                                    PackageUM AS 整包件数单位,
	                                                FileTracedNumber AS 追溯文件编号,
	                                                FileEdition AS 版本,EffectiveDate AS 生效日期,Guid,Operator AS 库管员,ForeignNumber AS 联系单号,QualityCheckStandard AS 检验标准,
                                            Checker as 复核人,Conclusion as 结论,ConclusionText as 结论其他内容,IsAnyDeviation as 物料验收过程是否出现偏差,DeviationNumber as 偏差编号,deviationIsClosed as 偏差是否已处理关闭,IsReport as 问题是否已报告,QualityManageIdea as 质量管理部门意见,Sign as 签名,SignDate as 签名日期,IsRequireClean as 是否需要清洁,PollutionSituation as 污染情况,CleanMethod as 清洁方式,IsComplete as 外包装是否完整,
                              DamageSituation as 损坏情况,CauseInvestigation1 as 原因调查1,IsSealed as 外包装是否密封,UnsealedCondition as 不密封情况,CauseInvestigation2 as 原因调查2,IsAnyMaterialWithPollutionRisk as 运输工具内是否存在造成污染交叉污染的物料,IsAnyProblemAffectedMaterialQuality as 是否有其他可能影响物料质量的问题,Question as 问题,CauseInvestigation3 as 原因调查3,LotNumberType as 批号类型,IsApprovedVendor as 是否为质量管理部门批准的供应商,StorageCondition as 规定贮存条件,TransportTemperature as 运输条件检查结果,TransportCondition as 运输条件是否符合,TransportationControlRecord as 是否有运输条件控制记录,Shape as 形状是否一致,Colour as 颜色是否一致,Font as 字体是否一致,RoughWeight as 有无毛重,NetWeight as 有无净重,ApprovalNumber as 有无批准文号,ReportType as 报告类型,Report as 有无报告
                                                FROM
	                                                EBR_ReceiveRecordForInspectUpdateHistory
                                                WHERE   Modifier='" + UserId + "|" + UserName + "' and  Left((CONVERT([varchar],ModifyDateTime,(120))),7)='" + dtpDate.Value.ToString("yyyy-MM") + "' and ModifyVisible=1 order by ModifyID";
            dgvRUH.DataSource = null;
            dgvRUH.DataSource = SQLHelper.GetDataTable(GlobalSpace.EBRConnStr, Sqlstr);
            dgvRUH.Columns["Check"].Visible = false;
            dgvRUH.Columns["Guid"].Visible = false;
            for (int i = 0; i < this.dgvRUH.Columns.Count; i++)
            {
                this.dgvRUH.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
                this.dgvRUH.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            }
        }

        private void ManageBatchRecordUpdateHistory_Load(object sender, EventArgs e)
        {
            dgvRUH.Columns["Check"].Visible = false;
        }

        private void dgvRUH_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int RowIndex = e.RowIndex;
            if (RowIndex < 0) return;
            dgvRUH["Check", RowIndex].Value = !Convert.ToBoolean(dgvRUH["Check", RowIndex].Value);
        }

        private void BtnchazhaoAll_Click(object sender, EventArgs e)
        {
            string Sqlstr = @"SELECT ModifyContent as 修改内容,Modifier as 修订人,RevisionReason as 修订原因,ModifyDateTime as 修改时间,ModifyVisible as 可见,
	                                                FileNumber AS 受控流水号,ApplyDate AS 请验日期,
	                                                VendorName AS 供应商名,
	                                                ManufacturerName AS 生产商名,
	                                                ItemNumber AS 物料编码,
	                                                ItemDescription AS 品名,
                                                    LineUM AS 单位,
	                                                LotNumber AS 批号,
	                                                InternalLotNumber AS 公司批号,
	                                                ManufacturedDate AS 生产日期,
	                                                ExpiredDate AS 过期日期,
	                                                ReceiveQuantity AS 入库数量,
	                                                PackageQuantity AS 整包件数,
	                                                PackageSpecification AS 包装规格,	                                                
	                                                PackageOdd AS 零头标示值,
                                                    PackageUM AS 整包件数单位,
	                                                FileTracedNumber AS 追溯文件编号,
	                                                FileEdition AS 版本,EffectiveDate AS 生效日期,Guid,Operator AS 库管员,ForeignNumber AS 联系单号,QualityCheckStandard AS 检验标准,
                                            Checker as 复核人,Conclusion as 结论,ConclusionText as 结论其他内容,IsAnyDeviation as 物料验收过程是否出现偏差,DeviationNumber as 偏差编号,deviationIsClosed as 偏差是否已处理关闭,IsReport as 问题是否已报告,QualityManageIdea as 质量管理部门意见,Sign as 签名,SignDate as 签名日期,IsRequireClean as 是否需要清洁,PollutionSituation as 污染情况,CleanMethod as 清洁方式,IsComplete as 外包装是否完整,
                              DamageSituation as 损坏情况,CauseInvestigation1 as 原因调查1,IsSealed as 外包装是否密封,UnsealedCondition as 不密封情况,CauseInvestigation2 as 原因调查2,IsAnyMaterialWithPollutionRisk as 运输工具内是否存在造成污染交叉污染的物料,IsAnyProblemAffectedMaterialQuality as 是否有其他可能影响物料质量的问题,Question as 问题,CauseInvestigation3 as 原因调查3,LotNumberType as 批号类型,IsApprovedVendor as 是否为质量管理部门批准的供应商,StorageCondition as 规定贮存条件,TransportTemperature as 运输条件检查结果,TransportCondition as 运输条件是否符合,TransportationControlRecord as 是否有运输条件控制记录,Shape as 形状是否一致,Colour as 颜色是否一致,Font as 字体是否一致,RoughWeight as 有无毛重,NetWeight as 有无净重,ApprovalNumber as 有无批准文号,ReportType as 报告类型,Report as 有无报告,ModifyID
                                                FROM
	                                                EBR_ReceiveRecordForInspectUpdateHistory
                                                WHERE   Modifier='" + UserId + "|" + UserName + "' and  Left((CONVERT([varchar],ModifyDateTime,(120))),7)='" + dtpDate.Value.ToString("yyyy-MM") + "'  order by ModifyID";
            dgvRUH.DataSource = null;
            dgvRUH.DataSource = SQLHelper.GetDataTable(GlobalSpace.EBRConnStr, Sqlstr);
            dgvRUH.Columns["Check"].Visible = true; 
            dgvRUH.Columns["ModifyID"].Visible = false;
            dgvRUH.Columns["Guid"].Visible = false;
            for (int i = 0; i < this.dgvRUH.Columns.Count; i++)
            {
                this.dgvRUH.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
                this.dgvRUH.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            }
        }

        private void Btnyincang_Click(object sender, EventArgs e)
        {
            List<int> Lint = new List<int>();
            foreach (DataGridViewRow dgvrow in dgvRUH.Rows)
            {
                if (Convert.ToBoolean(dgvrow.Cells["Check"].Value))
                    Lint.Add(Convert.ToInt32(dgvrow.Cells["ModifyID"].Value));
            }
            string Sqlstr = "update  [dbo].[EBR_ReceiveRecordForInspectUpdateHistory] set ModifyVisible =0 where ModifyID in ("+string.Join(",",Lint)+")";
            if (SQLHelper.ExecuteNonQuery(GlobalSpace.EBRConnStr, Sqlstr))
            { MessageBox.Show("已隐藏成功"); }
        }

        private void Btnxianshi_Click(object sender, EventArgs e)
        {
            List<int> Lint = new List<int>();
            foreach (DataGridViewRow dgvrow in dgvRUH.Rows)
            {
                if (Convert.ToBoolean(dgvrow.Cells["Check"].Value))
                    Lint.Add(Convert.ToInt32(dgvrow.Cells["ModifyID"].Value));
            }
            string Sqlstr = "update  [dbo].[EBR_ReceiveRecordForInspectUpdateHistory] set ModifyVisible =1 where ModifyID in (" + string.Join(",", Lint) + ")";
            if (SQLHelper.ExecuteNonQuery(GlobalSpace.EBRConnStr, Sqlstr))
            { MessageBox.Show("已显示成功"); }
        }
        bool BtnVisible = true;
        private void ManageBatchRecordUpdateHistory_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == (Keys.U | Keys.Alt))//按下alt+s键
            {
                e.Handled = true;//将Handled设置为true，指示已经处理过KeyPress事件
                BtnchazhaoAll.Visible = BtnVisible;
                Btnyincang.Visible = BtnVisible;
                Btnxianshi.Visible = BtnVisible;
                BtnVisible = !BtnVisible;
            }
        }
    }
}
