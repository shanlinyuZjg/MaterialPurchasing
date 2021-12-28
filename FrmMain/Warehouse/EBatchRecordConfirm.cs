using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;


namespace Global.Warehouse
{
    public partial class EBatchRecordConfirm : Office2007Form
    {
        public EBatchRecordConfirm()
        {
            this.EnableGlass = false;
            MessageBoxEx.EnableGlass = false;
            InitializeComponent();
        }

        private void RePaint(GroupBox groupBox1, PaintEventArgs e)
        {
            e.Graphics.Clear(groupBox1.BackColor);
            e.Graphics.DrawString(groupBox1.Text, groupBox1.Font, Brushes.Red, 10, 1);
            e.Graphics.DrawLine(Pens.Red, 1, 7, 8, 7);
            e.Graphics.DrawLine(Pens.Red, e.Graphics.MeasureString(groupBox1.Text, groupBox1.Font).Width + 8, 7, groupBox1.Width - 2, 7);
            e.Graphics.DrawLine(Pens.Red, 1, 7, 1, groupBox1.Height - 2);
            e.Graphics.DrawLine(Pens.Red, 1, groupBox1.Height - 2, groupBox1.Width - 2, groupBox1.Height - 2);
            e.Graphics.DrawLine(Pens.Red, groupBox1.Width - 2, 7, groupBox1.Width - 2, groupBox1.Height - 2);

        }

        private void groupBox1_Paint(object sender, PaintEventArgs e)
        {
            RePaint(shape, e);
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            string lotnumber = string.Empty;
            string approved = string.Empty;
            string condition = string.Empty;
            string express = string.Empty;
            string expressRecord = string.Empty;
            string shape = string.Empty;
            string color = string.Empty;
            string font = string.Empty;
            string clean = string.Empty;
            string complete = string.Empty;
            string outerclean = string.Empty;
            string gross = string.Empty;
            string net = string.Empty;
            string approvednumber = string.Empty;
            string mfgreport = string.Empty;
            string mfgdate = string.Empty;
            string validdate = string.Empty;
            string expresscontamination = string.Empty;
            string capa = string.Empty;
            string other = string.Empty;
            string reported = string.Empty;
            string receive = string.Empty;
            string result = string.Empty;
            string problem = string.Empty;
            //批号
            if (mfgLotNumber.Checked)
            {
                lotnumber = "生产商批号";
            }
            else if(vendorLotNumber.Checked)
            {
                lotnumber = "供应商批号";
            }
            else
            {
                MessageBoxEx.Show("批号来源未选择！", "提示");
                return;
            }
            //已批准供应商
            if (approvedY.Checked)
            {
                approved = "是否为质量管理部门已批准供应商  是";
            }
            else if (approvedN.Checked)
            {
                approved = "是否为质量管理部门已批准供应商  否";
            }
            else
            {
                MessageBoxEx.Show("是否为已批准供应商未选择！", "提示");
                return;
            }
            //储存条件
            if (temGeneralp.Checked)
            {
                condition = "常温";
            }
            else if (tempCool.Checked)
            {
                condition = "阴凉";
            }
            else if (tempCold.Checked)
            {
                condition = "冷藏";
            }
            else if (tempOther.Checked)
            {
                condition = "其他";
            }
            else
            {
                MessageBoxEx.Show("未选择储存条件！", "提示");
                return;
            }

            //运输条件符合
            if (expressY.Checked)
            {
                express = "运输条件是否符合  是";
            }
            else if (expressN.Checked)
            {
                express = "运输条件是否符合  否";
            }
            else
            {
                MessageBoxEx.Show("运输条件是否符合未选择！", "提示");
                return;
            }
            //运输控制记录
            if (expressRecordY.Checked)
            {
                expressRecord = "是否有运输过程控制记录  是";
            }
            else if (expressRecordNot.Checked)
            {
                expressRecord = "是否有运输过程控制记录  否";
            }
            else
            {
                MessageBoxEx.Show("是否有运输控制记录未选择！", "提示");
                return;
            }
            //颜色
            if (colorY.Checked)
            {
                color = "颜色  是";
            }
            else if (colorN.Checked)
            {
                color = "颜色  否";
            }
            else
            {
                MessageBoxEx.Show("颜色未选择！", "提示");
                return;
            }
            //形状
            if (shapeY.Checked)
            {
                shape = "形状  是";
            }
            else if (shapeN.Checked)
            {
                shape = "形状  否";
            }
            else
            {
                MessageBoxEx.Show("形状未选择！", "提示");
                return;
            }
            //字体
            if (fontY.Checked)
            {
                font = "字体  是";
            }
            else if (fontN.Checked)
            {
                color = "字体  否";
            }
            else
            {
                MessageBoxEx.Show("字体未选择！", "提示");
                return;
            }
            //需要清洁
            if (cleanY.Checked)
            {
                clean = "需要清洁  是";
            }
            else if (cleanN.Checked)
            {
                color = "需要清洁  否";
            }
            else
            {
                MessageBoxEx.Show("清洁未选择！", "提示");
                return;
            }
            //外包装完整
            if (outerCompleteY.Checked)
            {
                complete = "外包装完整  是";
            }
            else if (outerCompleteN.Checked)
            {
                complete = "外包装完整  否";
            }
            else
            {
                MessageBoxEx.Show("外包装是否完整未选择！", "提示");
                return;
            }
            //外包装整洁
            if (outerClearY.Checked)
            {
                outerclean = "外包装整洁  是";
            }
            else if (outerClearN.Checked)
            {
                outerclean = "外包装整洁  否";
            }
            else
            {
                MessageBoxEx.Show("外包装整洁未选择！", "提示");
                return;
            }

            //毛重
            if (grossY.Checked)
            {
                gross = "毛重  是";
            }
            else if (grossN.Checked)
            {
                gross = "毛重  否";
            }
            else
            {
                MessageBoxEx.Show("毛重未选择！", "提示");
                return;
            }
            //净重
            if (netY.Checked)
            {
                net = "净重  是";
            }
            else if (netN.Checked)
            {
                net = "净重  否";
            }
            else
            {
                MessageBoxEx.Show("净重未选择！", "提示");
                return;
            }
            //批准文号
            if (approvedNumberY.Checked)
            {
                approvednumber = "批准文号  是";
            }
            else if (approvedNumberN.Checked)
            {
                approvednumber = "批准文号  否";
            }
            else
            {
                MessageBoxEx.Show("批准文号未选择！", "提示");
                return;
            }
            //生产商报告
            if (mfgReportY.Checked)
            {
                mfgreport = "生产商（口岸）报告  是";
            }
            else if (mfgReportN.Checked)
            {
                mfgreport = "生产商（口岸）报告  否";
            }
            else
            {
                MessageBoxEx.Show("生产商报告未选择！", "提示");
                return;
            }
            //生产日期
            if (mfgDateY.Checked)
            {
                mfgdate = "生产日期  是";
            }
            else if (mfgDateN.Checked)
            {
                mfgdate = "生产日期  否";
            }
            else
            {
                MessageBoxEx.Show("生产日期未选择！", "提示");
                return;
            }
            //有效期
            if (validDateY.Checked)
            {
                validdate = "有效期/复验期  是";
            }
            else if (validDateN.Checked)
            {
                validdate = "有效期/复验期  否";
            }
            else
            {
                MessageBoxEx.Show("有效期未选择！", "提示");
                return;
            }
            //交通工具内造成污染
            if (expressContaminationY.Checked)
            {
                expresscontamination = "运输工具内是否有造成污染、交叉污染风险的物料  是";
            }
            else if (expressContaminationN.Checked)
            {
                expresscontamination = "运输工具内是否有造成污染、交叉污染风险的物料  否";
            }
            else
            {
                MessageBoxEx.Show("造成污染风险的物料未选择！", "提示");
                return;
            }
            //其他影响物料质量
            if (othersY.Checked)
            {
                other = "外包装整洁  是";
            }
            else if (othersN.Checked)
            {
                other = "外包装整洁  否";
            }
            else
            {
                MessageBoxEx.Show("其他可能影响物料质量的问题未选择！", "提示");
                return;
            }
            //偏差已处理
            if (capaY.Checked)
            {
                capa = "偏差是否已处理关闭  是";
            }
            else if (capaN.Checked)
            {
                capa = "偏差是否已处理关闭  否";
            }
            else
            {
                MessageBoxEx.Show("偏差是否已处理未选择！", "提示");
                return;
            }
            //验收有偏差
            if (receiveY.Checked)
            {
                receive = "验收过程是否出现偏差  是";
            }
            else if (receiveN.Checked)
            {
                receive = "验收过程是否出现偏差  否";
            }
            else
            {
                MessageBoxEx.Show("验收过程未选择！", "提示");
                return;
            }
            //问题报告给质量部门
            if (problemY.Checked)
            {
                problem = " 是";
            }
            else if (problemN.Checked)
            {
                problem = "否";
            }
            else if (problemNotNeed.Checked)
            {
                problem = "不需报告";
            }
            else
            {
                MessageBoxEx.Show("有问题向质量管理部门报告未选择！", "提示");
                return;
            }
            //结论
            if (resultY.Checked)
            {
                result = "接收入库";
            }
            else if (resultN.Checked)
            {
                result = "退回供应商";
            }
            else if (resultOther.Checked)
            {
                result = "其他";
            }
            else
            {
                MessageBoxEx.Show("结论未选择！", "提示");
                return;
            }
        }

        private void problemY_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
