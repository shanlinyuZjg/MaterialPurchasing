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
using DevComponents.DotNetBar;

namespace Global.Purchase
{
    public partial class ForeignOrderItemSpecification : Office2007Form
    {
        string sqlSelect = @"Select VendorNumber As  供应商码,VendorName AS 供应商名,SingleColorPrice AS 单色价格,ComplexColorPrice AS 彩色价格  From PurchaseDepartmentForeignOrderPackageSpecificationByCMF";
        public ForeignOrderItemSpecification()
        {
            this.EnableGlass = false;
            MessageBoxEx.EnableGlass = false;
            InitializeComponent();
        }

        private void ForeignOrderItemSpecification_Load(object sender, EventArgs e)
        {
            dgvSpecification.DataSource = SQLHelper.GetDataTable(GlobalSpace.FSDBConnstr, sqlSelect);
        }

        private void cbSpecificationMannual_CheckedChanged(object sender, EventArgs e)
        {
            if(cbSpecificationMannual.Checked == true)
            {
                string sql = @"Select VendorNumber As  供应商码,VendorName AS 供应商名,'' AS 单色价格,'' AS 彩色价格  From PurchaseDepartmentForeignOrderPackageSpecificationByCMF";
                dgvSpecification.DataSource = SQLHelper.GetDataTable(GlobalSpace.FSDBConnstr, sql);
            }
            else
            {
                string sql = @"Select VendorNumber As  供应商码,VendorName AS 供应商名,SingleColorPrice AS 单色价格,ComplexColorPrice AS 彩色价格  From PurchaseDepartmentForeignOrderPackageSpecificationByCMF";
                dgvSpecification.DataSource = SQLHelper.GetDataTable(GlobalSpace.FSDBConnstr, sql);
            }
        }

        private void btnBoxAddWithoutRecord_Click(object sender, EventArgs e)
        {
            if(rbtnComplexColor.Checked == false && rbtnSingleColor.Checked == false)
            {
                Custom.MsgEx("必须选择说明书的颜色！");
                return;
            }
            if (dgvSpecification.SelectedRows.Count > 0)
            {
                for (int i = 0; i < dgvSpecification.SelectedRows.Count; i++)
                {
                    if (dgvSpecification.SelectedRows[i].Cells["单色价格"].Value == null && dgvSpecification.SelectedRows[i].Cells["单色价格"].Value.ToString() == "" && dgvSpecification.SelectedRows[i].Cells["彩色价格"].Value == null && dgvSpecification.SelectedRows[i].Cells["彩色价格"].Value.ToString() == "")
                    {
                        Custom.MsgEx("单色价格和彩色价格不能同时为空！");
                        dgvSpecification.SelectedRows[i].DefaultCellStyle.BackColor = Color.Red;
                        return;
                    }
                    Specification specification = new Specification();
                    specification.VendorName = dgvSpecification.SelectedRows[i].Cells["供应商名"].Value.ToString();
                    specification.VendorNumber = dgvSpecification.SelectedRows[i].Cells["供应商码"].Value.ToString();

                    if (dgvSpecification.SelectedRows[i].Cells["单色价格"].Value != null && dgvSpecification.SelectedRows[i].Cells["单色价格"].Value.ToString() != "")
                    {
                        specification.Color = "单色";
                        specification.Price = Convert.ToDouble(dgvSpecification.SelectedRows[i].Cells["单色价格"].Value);
                    }
                    if (dgvSpecification.SelectedRows[i].Cells["彩色价格"].Value != null && dgvSpecification.SelectedRows[i].Cells["彩色价格"].Value.ToString() != "")
                    {
                        specification.Color = "彩色";
                        specification.Price = Convert.ToDouble(dgvSpecification.SelectedRows[i].Cells["彩色价格"].Value);
                    }
                    GlobalSpace.specificationList.Add(specification);
                }
                this.Close();
            }
            else
            {
                Custom.MsgEx("没有选中的行！");
            }
        }

        private void btnBoxSubmit_Click(object sender, EventArgs e)
        {
            if (rbtnComplexColor.Checked == false && rbtnSingleColor.Checked == false)
            {
                Custom.MsgEx("必须选择说明书的颜色！");
                return;
            }
            if(cbSpecificationMannual.Checked == true)
            {
                Custom.MsgEx("请选中手工按钮！");
                return;
            }
            if (dgvSpecification.SelectedRows.Count > 0)
            {
                for(int i = 0; i < dgvSpecification.SelectedRows.Count ; i++)
                {
                    Specification specification = new Specification();
                    specification.VendorName = dgvSpecification.SelectedRows[i].Cells["供应商名"].Value.ToString();
                    specification.VendorNumber = dgvSpecification.SelectedRows[i].Cells["供应商码"].Value.ToString();
                    if (rbtnSingleColor.Checked == true)
                    {
                        if (dgvSpecification.SelectedRows[i].Cells["单色价格"].Value != null && dgvSpecification.SelectedRows[i].Cells["单色价格"].Value.ToString() != "")
                        {
                            specification.Color = "单色";
                            specification.Price = Convert.ToDouble(dgvSpecification.SelectedRows[i].Cells["单色价格"].Value);
                        }
                        else
                        {
                            Custom.MsgEx("单色价格没有填写完整！");
                        }
                    }
                    if (rbtnComplexColor.Checked == true)
                    {
                        if (dgvSpecification.SelectedRows[i].Cells["彩色价格"].Value != null && dgvSpecification.SelectedRows[i].Cells["彩色价格"].Value.ToString() != "")
                        {
                            specification.Color = "彩色";
                            specification.Price = Convert.ToDouble(dgvSpecification.SelectedRows[i].Cells["彩色价格"].Value);
                        }
                        else
                        {
                            Custom.MsgEx("彩色价格没有填写完整！");
                        }
                    }
                 
                    GlobalSpace.specificationList.Add(specification);
                }
                this.Close();
            }
            else
            {
                Custom.MsgEx("没有选中的行！");
            }
        }

        private void rbtnSingleColor_CheckedChanged(object sender, EventArgs e)
        {
            if(rbtnSingleColor.Checked == true)
            {
                if(dgvSpecification.Rows.Count > 0)
                {
                    if(dgvSpecification.Columns["单色价格"].Visible == false)
                    {
                        dgvSpecification.Columns["单色价格"].Visible = true;
                        dgvSpecification.Columns["彩色价格"].Visible = false;
                    }
                    else
                    {
                        dgvSpecification.Columns["彩色价格"].Visible = false;
                    }
                }
            }
        }

        private void rbtnComplexColor_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtnComplexColor.Checked == true)
            {
                if (dgvSpecification.Rows.Count > 0)
                {
                    if (dgvSpecification.Columns["彩色价格"].Visible == false)
                    {
                        dgvSpecification.Columns["单色价格"].Visible = false;
                        dgvSpecification.Columns["彩色价格"].Visible = true;
                    }
                    else
                    {
                        dgvSpecification.Columns["单色价格"].Visible = false;
                    }
                }
            }
        }
    }
}
