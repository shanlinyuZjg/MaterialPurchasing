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
    public partial class ForeignOrderItemBox : Office2007Form
    {
        string sqlBoxLengthCriterion = string.Empty;
        string sqlBoxWidthCriterion = string.Empty;
        string sqlBoxHeightCriterion = string.Empty;
        string sqlBoxSelect = @"Select Length as 长度,Width as 宽度,Height as 高度,Texture as 材质,ProcessRequirement as 处理工艺,Price AS 价格,VendorNumber AS 供应商码 ,VendorName AS 供应商名 From PurchaseDepartmentForeignOrderPackageBoxByCMF ";
        public ForeignOrderItemBox()
        {
            this.EnableGlass = false;
            MessageBoxEx.EnableGlass = false;
            InitializeComponent();
        }

        private void tbBoxLength_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (tbBoxLength.Text != "")
            {
                if (e.KeyChar == (char)13)
                {
                    string sqlBoxTemp = sqlBoxSelect;
                    sqlBoxLengthCriterion = @" Length >=" + (Convert.ToDouble(tbBoxLength.Text.Trim()) - 5) + " and Length <=" + (Convert.ToDouble(tbBoxLength.Text.Trim()) + 5) + "";
                    sqlBoxTemp = sqlBoxTemp + " Where  " + sqlBoxLengthCriterion;
                    CommonOperate.DataGridViewShow(sqlBoxTemp, GlobalSpace.FSDBConnstr, dgvBox);
                    CommonOperate.TextBoxNext(tbBoxLength, tbBoxWidth, e);

                }

            }
        }

        private void tbBoxWidth_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (tbBoxWidth.Text != "")
            {
                if (e.KeyChar == (char)13)
                {
                    string sqlBoxTemp = sqlBoxSelect;
                    sqlBoxWidthCriterion = @"  Width >=" + (Convert.ToDouble(tbBoxWidth.Text.Trim()) - 5) + " and Width <=" + (Convert.ToDouble(tbBoxWidth.Text.Trim()) + 5) + "";
                    sqlBoxTemp = sqlBoxTemp + " Where  " + sqlBoxLengthCriterion + "   And  " + sqlBoxWidthCriterion;
                    CommonOperate.DataGridViewShow(sqlBoxTemp, GlobalSpace.FSDBConnstr, dgvBox);
                    CommonOperate.TextBoxNext(tbBoxWidth, tbBoxHeight, e);
                }

            }
        }

        private void tbBoxHeight_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (tbBoxHeight.Text != "")
            {
                if (e.KeyChar == (char)13)
                {
                    string sqlBoxTemp = sqlBoxSelect;
                    sqlBoxHeightCriterion = @"  Height >=" + (Convert.ToDouble(tbBoxHeight.Text.Trim()) - 5) + " and Height <=" + (Convert.ToDouble(tbBoxHeight.Text.Trim()) + 5) + "";
                    sqlBoxTemp = sqlBoxTemp + " Where  " + sqlBoxLengthCriterion + "   And  " + sqlBoxWidthCriterion + "   And  " + sqlBoxHeightCriterion;
                    CommonOperate.DataGridViewShow(sqlBoxTemp, GlobalSpace.FSDBConnstr, dgvBox);
                }
            }
        }    

        private void btnBoxAddWithoutRecord_Click(object sender, EventArgs e)
        {
            if (cbBoxMannual.Checked == true)
            {
                if(dgvBox.SelectedRows.Count > 0)
                {
                    for(int i = 0; i < dgvBox.SelectedRows.Count;i++ )
                    {
                        Box box = new Box();
                        box.BoxSize = tbBoxLength.Text + "x" + tbBoxWidth.Text + "x" + tbBoxHeight.Text;
                        box.BExist = false;                        
                        box.BoxTexture = tbBoxTexture.Text;
                        box.BoxProcessRequirements = tbBoxProcessRequirement.Text;
                        box.vendorNumber = dgvBox.SelectedRows[i].Cells["供应商码"].Value.ToString();
                        box.vendorName = dgvBox.SelectedRows[i].Cells["供应商名"].Value.ToString();
                        if (dgvBox.SelectedRows[i].Cells["价格"].Value == null || dgvBox.SelectedRows[i].Cells["价格"].Value.ToString() == "")
                        {
                            Custom.MsgEx("价格不能为空！");
                            return;
                        }
                        box.BoxPrice = Convert.ToDouble( dgvBox.SelectedRows[i].Cells["价格"].Value);
                        GlobalSpace.boxList.Add(box);
                    }

                    this.Close();
                }
                else
                {
                    Custom.MsgEx("没有选中的行！");
                }
                
            }
            else
            {
                Custom.MsgEx("未选中手工输入！");
            }
        }

        private void btnBoxSubmit_Click(object sender, EventArgs e)
        {
            if(cbBoxMannual.Checked)
            {
                Custom.MsgEx("当前已选择手工，请点击\"无记录添加\"按钮！");
                return;
            }
            if (dgvBox.SelectedRows.Count > 0)
            {
                for (int i = 0; i < dgvBox.SelectedRows.Count; i++)
                {
                    Box box = new Box();
                    box.BoxSize = dgvBox.SelectedRows[i].Cells["长度"].Value.ToString() + "x" + dgvBox.SelectedRows[i].Cells["宽度"].Value.ToString() + "x" + dgvBox.SelectedRows[i].Cells["高度"].Value.ToString();
                    box.BExist = true;
                    box.BoxPrice = Convert.ToDouble(dgvBox.SelectedRows[i].Cells["价格"].Value);
                    box.BoxTexture = dgvBox.SelectedRows[i].Cells["材质"].Value.ToString();
                    box.BoxProcessRequirements = dgvBox.SelectedRows[i].Cells["处理工艺"].Value.ToString();
                    box.vendorNumber = dgvBox.SelectedRows[i].Cells["供应商码"].Value.ToString();
                    box.vendorName = dgvBox.SelectedRows[i].Cells["供应商名"].Value.ToString();
                    GlobalSpace.boxList.Add(box);
                }
                this.Close();
            }
            else
            {
                Custom.MsgEx("无选中的行！");
            }
        }

        private void cbBoxMannual_CheckedChanged(object sender, EventArgs e)
        {
            if(cbBoxMannual.Checked == true)
            {
                string sqlSelect = @"Select Distinct VendorNumber AS 供应商码,VendorName AS 供应商名,'' AS 价格 From PurchaseDepartmentForeignOrderPackageBoxByCMF";
                dgvBox.DataSource = SQLHelper.GetDataTable(GlobalSpace.FSDBConnstr, sqlSelect);
            }
            else
            {
                DataTable dt = null;
                dgvBox.DataSource = dt;
            }
        }

        private void ForeignOrderItemBox_Load(object sender, EventArgs e)
        {

        }
    }
}
