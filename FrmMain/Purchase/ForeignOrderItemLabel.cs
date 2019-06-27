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


namespace Global.Purchase
{
    public partial class ForeignOrderItemLabel : Office2007Form
    {
        double Quantity = 0;
        string sqlLabelLengthCriterion = string.Empty;
        string sqlLabelWidthCriterion = string.Empty;
        string sqlLabelSelect = @"Select Length as 长度,Width as 宽度,Price as 普通签价格,CommonHugeQuantityPrice AS 普通签超过基准数量价格,CoveredPrice AS 覆膜签价格,CoveredHugeQuantityPrice AS 覆膜签超过基准数量价格,TransparentPrice 透明签价格,ScrapPrice AS 易撕签价格,VendorNumber AS 供应商码 ,VendorName AS 供应商名  From PurchaseDepartmentForeignOrderPackageLabelSizeByCMF ";

        public ForeignOrderItemLabel(double quantity)
        {
            Quantity = quantity;
            this.EnableGlass = false;
            MessageBoxEx.EnableGlass = false;
            InitializeComponent();
        }

        private void tbLabelLength_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (tbLabelLength.Text != "")
            {
                if (e.KeyChar == (char)13)
                {
                    string sqlLabelTemp = sqlLabelSelect;
                    sqlLabelLengthCriterion = @" Length =" + Convert.ToDouble(tbLabelLength.Text.Trim()) + "";
                    sqlLabelTemp = sqlLabelTemp + " Where " + sqlLabelLengthCriterion;
                    CommonOperate.DataGridViewShow(sqlLabelTemp, GlobalSpace.FSDBConnstr, dgvLabel);
                    CommonOperate.TextBoxNext(tbLabelLength, tbLabelWidth, e);
                }
            }
        }

        private void tbLabelWidth_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (tbLabelWidth.Text != "")
            {
                if (e.KeyChar == (char)13)
                {
                    string sqlLabelTemp = sqlLabelSelect;
                    sqlLabelWidthCriterion = @" Width = " + Convert.ToDouble(tbLabelWidth.Text.Trim()) + "";
                    sqlLabelTemp = sqlLabelTemp + " Where " + sqlLabelLengthCriterion + " And " + sqlLabelWidthCriterion;
                    CommonOperate.DataGridViewShow(sqlLabelTemp, GlobalSpace.FSDBConnstr, dgvLabel);
                    tbLabelMannualArea.Text = GetSingleLabelArea(Convert.ToDouble(tbLabelLength.Text.Trim()), Convert.ToDouble(tbLabelWidth.Text.Trim())).ToString();
                }

            }
        }

        private void btnLabelAddWithoutRecord_Click(object sender, EventArgs e)
        {
            if (cbLabelMannual.Checked == true)
            {
               if(dgvLabel.SelectedRows.Count > 0 )
                {
                    for(int i = 0; i < dgvLabel.SelectedRows.Count; i++ )
                    {
                        Global.Label label = new Global.Label();
                        label.LabelSize = tbLabelLength.Text + "×" + tbLabelWidth.Text;
                        label.VendorNumber = dgvLabel.SelectedRows[i].Cells["供应商码"].Value.ToString();
                        label.VendorName = dgvLabel.SelectedRows[i].Cells["供应商名"].Value.ToString();
                        label.BExist = true;
                        double labelUnitPrice = 0;
                        double baseQuantity = Convert.ToDouble(tbBaseQuantity.Text.Trim());
                        string labelRequirements = string.Empty;

                        if (rbtnLabelCommon.Checked == true)
                        {
                            if (Quantity >= baseQuantity)
                            {
                                if (dgvLabel.SelectedRows[i].Cells["普通签超过基准数量价格"].Value == null || dgvLabel.SelectedRows[i].Cells["普通签超过基准数量价格"].Value.ToString() == "")
                                {
                                    Custom.MsgEx("第" + (i + 1).ToString() + "行普通签超过基准数量价格不能为空！");
                                    return;
                                }
                                labelUnitPrice = Math.Round(Convert.ToDouble(dgvLabel.SelectedRows[i].Cells["普通签超过基准数量价格"].Value) * Convert.ToDouble(tbLabelMannualArea.Text.Trim()), 3);
                            }
                            else
                            {
                                if(dgvLabel.SelectedRows[i].Cells["普通签价格"].Value == null || dgvLabel.SelectedRows[i].Cells["普通签价格"].Value.ToString() == "")
                                {
                                    Custom.MsgEx("第" + (i + 1).ToString() + "行普通签价格不能为空！");
                                    return;
                                }
                                labelUnitPrice = Convert.ToDouble(Convert.ToDouble(dgvLabel.SelectedRows[i].Cells["普通签价格"].Value));
                            }
                            labelRequirements = "普通标签";
                        }
                        if (rbtnLabelCovered.Checked == true)
                        {
                            if (Quantity >= baseQuantity)
                            {
                                if (dgvLabel.SelectedRows[i].Cells["覆膜签超过基准数量价格"].Value == null || dgvLabel.SelectedRows[i].Cells["覆膜签超过基准数量价格"].Value.ToString() == "")
                                {
                                    Custom.MsgEx("第" + (i + 1).ToString() + "行覆膜签超过基准价格不能为空！");
                                    return;
                                }
                                double totalAmount = Convert.ToDouble(dgvLabel.SelectedRows[i].Cells["覆膜签超过基准数量价格"].Value) * Convert.ToDouble(tbLabelMannualArea.Text.Trim()) * Quantity;
                                labelUnitPrice = Math.Round(totalAmount / Quantity, 3);
                            }
                            else
                            {
                                if (dgvLabel.SelectedRows[i].Cells["覆膜签价格"].Value == null || dgvLabel.SelectedRows[i].Cells["覆膜签价格"].Value.ToString() == "")
                                {
                                    Custom.MsgEx("第" + (i + 1).ToString() + "行覆膜签价格不能为空！");
                                    return;
                                }
                                labelUnitPrice = Convert.ToDouble(Convert.ToDouble(dgvLabel.SelectedRows[i].Cells["覆膜签价格"].Value));
                            }
                            labelRequirements = "覆膜标签";
                        }
                        if (rbtnLabelTransparent.Checked == true)
                        {
                            if (dgvLabel.SelectedRows[i].Cells["透明签价格"].Value == null || dgvLabel.SelectedRows[i].Cells["透明签价格"].Value.ToString() == "")
                            {
                                Custom.MsgEx("第" + (i + 1).ToString() + "行透明签价格不能为空！");
                                return;
                            }
                            double totalAmount = Convert.ToDouble(dgvLabel.SelectedRows[i].Cells["透明签价格"].Value) * Convert.ToDouble(tbLabelMannualArea.Text.Trim()) * Quantity;
                            labelUnitPrice = Math.Round(totalAmount / Quantity, 3);
                            labelRequirements = "透明标签";
                        }
                        if (rbtnLabelScrap.Checked == true)
                        {
                            if (dgvLabel.SelectedRows[i].Cells["易撕签价格"].Value == null || dgvLabel.SelectedRows[i].Cells["易撕签价格"].Value.ToString() == "")
                            {
                                Custom.MsgEx("第" + (i + 1).ToString() + "行透明签价格不能为空！");
                                return;
                            }
                            double totalAmount = Convert.ToDouble(dgvLabel.SelectedRows[i].Cells["易撕签价格价格"].Value) * Convert.ToDouble(tbLabelMannualArea.Text.Trim()) * Quantity;
                            labelUnitPrice = Math.Round(totalAmount / Quantity, 3);
                            labelRequirements = "易撕标签";
                        }
                        label.Price = labelUnitPrice;
                        label.LabelRequirements = labelRequirements;
                        GlobalSpace.labelList.Add(label);
                    }               
                }
               else
                {
                    Custom.MsgEx("无选中的行！");
                }
                this.Close();
            }
            else
            {
                Custom.MsgEx("未选中手工！");
            }
        }
        private double GetSingleLabelArea( double length, double width)
        {
            double sumArea = 0;
            sumArea = (length * width) / 1000000 ;
            return sumArea;
        }
        private void btnLabelSubmit_Click(object sender, EventArgs e)
        {
            if (cbLabelMannual.Checked == true)
            {
                Custom.MsgEx("手工记录只能点击无记录添加按钮！");
            }
            else
            {
                double labelPrice = 0;
                int k = 0;
                double baseQuantity = Convert.ToDouble(tbBaseQuantity.Text.Trim());
                string requirements = string.Empty;

                if (rbtnLabelCommon.Checked == true)
                {
                    if (Quantity >= baseQuantity)
                    {
                        k = 1;
                    }
                    else
                    {
                        k = 2;
                    }
                    requirements = "普通标签";
                }

                if (rbtnLabelCovered.Checked == true)
                {
                    if (Quantity >= baseQuantity)
                    {
                        k = 3;
                    }
                    else
                    {
                        k = 4;
                    }
                    requirements = "覆膜标签";
                }

                    if (rbtnLabelTransparent.Checked == true)
                    {
                        k = 5;
                        requirements = "透明标签";
                    }
                    if (rbtnLabelScrap.Checked == true)
                    {
                        k = 6;
                        requirements = "易撕标签";
                    }

                    if (dgvLabel.SelectedRows.Count > 0)
                    {
                        for (int i = 0; i < dgvLabel.SelectedRows.Count; i++)
                        {
                            switch (k)
                            {
                                case 1:
                                    labelPrice = Convert.ToDouble(tbLabelMannualArea.Text.Trim()) * Convert.ToDouble(dgvLabel.SelectedRows[i].Cells["普通签超过基准数量价格"].Value);
                                    break;
                                case 2:
                                    labelPrice = Convert.ToDouble(dgvLabel.SelectedRows[i].Cells["普通签价格"].Value);
                                    break;
                                case 3:
                                    labelPrice = Convert.ToDouble(tbLabelMannualArea.Text.Trim()) * Convert.ToDouble(dgvLabel.SelectedRows[i].Cells["覆膜签超过基准数量价格"].Value);
                                    break;
                                case 4:
                                    labelPrice = Convert.ToDouble(dgvLabel.SelectedRows[i].Cells["覆膜签价格"].Value);
                                    break;
                                case 5:
                                    labelPrice = Convert.ToDouble(tbLabelMannualArea.Text.Trim()) * Convert.ToDouble(dgvLabel.SelectedRows[i].Cells["透明签价格"].Value);
                                break;
                                case 6:
                                    labelPrice = Convert.ToDouble(tbLabelMannualArea.Text.Trim()) * Convert.ToDouble(dgvLabel.SelectedRows[i].Cells["易撕签价格"].Value);
                                break;
                        }
                            Label label = new Label();
                            label.LabelSize = dgvLabel.SelectedRows[i].Cells["长度"].Value.ToString() + "×" + dgvLabel.SelectedRows[i].Cells["宽度"].Value.ToString();
                            label.Price = labelPrice;
                            label.BExist = false;
                            label.IType = k;
                            label.LabelRequirements = requirements;
                            label.VendorNumber = dgvLabel.SelectedRows[i].Cells["供应商码"].Value.ToString();
                            label.VendorName = dgvLabel.SelectedRows[i].Cells["供应商名"].Value.ToString();
                            GlobalSpace.labelList.Add(label);
                        }
                        this.Close();
                    }
                    else
                    {
                        Custom.MsgEx("没有选中的行！");
                    }
                }
            }
        

        

        private void rbtnLabelCoveredExist_CheckedChanged(object sender, EventArgs e)
        {
         
        }

        private void tbLabelWidth_KeyUp(object sender, KeyEventArgs e)
        {

        }
    

        private void ForeignOrderItemLabel_Load(object sender, EventArgs e)
        {
            tbOrderQuantity.Text = Quantity.ToString();
        }

        private void cbLabelMannual_CheckedChanged(object sender, EventArgs e)
        {
            if (cbLabelMannual.Checked == true)
            {
                string sqlSelect = @"Select Distinct VendorNumber AS 供应商码,VendorName AS 供应商名,'' AS 普通签价格,''AS 普通签超过基准数量价格,'' AS 覆膜签价格,'' AS 覆膜签超过基准数量价格,'' AS 透明签价格,'' AS 易撕签价格 From PurchaseDepartmentForeignOrderPackageLabelSizeByCMF";
                dgvLabel.DataSource = SQLHelper.GetDataTable(GlobalSpace.FSDBConnstr, sqlSelect);
            }
            else
            {
                DataTable dt = null;
                dgvLabel.DataSource = dt;
            }
        }
    }
}
