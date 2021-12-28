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
    public partial class ForeignOrderItemOthers : Office2007Form
    {
        public ForeignOrderItemOthers()
        {

            this.EnableGlass = false;
            MessageBoxEx.EnableGlass = false;
            InitializeComponent();
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
             if(dgvDetail.SelectedRows.Count > 0)
            {     
                if(cbArea.Checked)
                {
                    for (int i = 0; i < dgvDetail.SelectedRows.Count; i++)
                    {
                        Others others = new Others();
                        others.CalType = "Area";
                        others.vendorNumber = dgvDetail.SelectedRows[i].Cells["供应商码"].Value.ToString();
                        others.vendorName = dgvDetail.SelectedRows[i].Cells["供应商名"].Value.ToString();

                        if (dgvDetail.SelectedRows[i].Cells["价格"].Value == null || dgvDetail.SelectedRows[i].Cells["价格"].Value.ToString() == "")
                        {
                            Custom.MsgEx("签类的包材价格不能为空！");
                            return;
                        }
                        others.Price = Convert.ToDouble(dgvDetail.SelectedRows[i].Cells["价格"].Value) * (Convert.ToDouble(dgvDetail.SelectedRows[i].Cells["长"].Value) * Convert.ToDouble(dgvDetail.SelectedRows[i].Cells["宽"].Value) / 1000000);
                        others.remark = "尺寸：" + dgvDetail.SelectedRows[i].Cells["长"].Value.ToString() + "x" + dgvDetail.SelectedRows[i].Cells["宽"].Value.ToString();

                        others.Area = (Convert.ToDouble(dgvDetail.SelectedRows[i].Cells["长"].Value.ToString()) * Convert.ToDouble(dgvDetail.SelectedRows[i].Cells["宽"].Value.ToString())) / 1000000;
                        GlobalSpace.othersList.Add(others);
                    }
                    this.Close();
                }           
                if(cbUnit.Checked)
                {
                    for (int i = 0; i < dgvDetail.SelectedRows.Count; i++)
                    {
                        Others others = new Others();
                        others.CalType = "Unit";
                        others.vendorNumber = dgvDetail.SelectedRows[i].Cells["供应商码"].Value.ToString();
                        others.vendorName = dgvDetail.SelectedRows[i].Cells["供应商名"].Value.ToString();

                        if (dgvDetail.SelectedRows[i].Cells["价格"].Value == null || dgvDetail.SelectedRows[i].Cells["价格"].Value.ToString() == "")
                        {
                            Custom.MsgEx("包材价格不能为空！");
                            return;
                        }
                        others.Price = Convert.ToDouble(dgvDetail.SelectedRows[i].Cells["价格"].Value);                      
                        GlobalSpace.othersList.Add(others);
                    }
                    this.Close();
                }
            }
             else
            {
                Custom.MsgEx("没有选中的行！");
            }
        }

        private void rbtnAluminumFoil_CheckedChanged(object sender, EventArgs e)
        {
            dgvDetail.DataSource = GetVendorInfo("铝箔");
            cbUnit.Checked = true;
            cbArea.Checked = false;
        }

        private void rbtnPlasticBoard_CheckedChanged(object sender, EventArgs e)
        {
            dgvDetail.DataSource = GetVendorInfo("塑料托");
        }

        private void rbtnCap_CheckedChanged(object sender, EventArgs e)
        {
            dgvDetail.DataSource = GetVendorInfo("铝塑盖");
            cbUnit.Checked = true;
            cbArea.Checked = false;
        }

        private DataTable GetVendorInfo(string type)
        {
            string sqlSelect = @"SELECT
	                                            VendorNumber AS 供应商码,
	                                            VendorName AS 供应商名,
                                                 Price AS 价格,Remark AS 备注
                                            FROM
	                                            PurchaseDepartmentForeignOrderPackageOthersByCMF
                                            WHERE
	                                            ItemType = '"+type+"'";
            return SQLHelper.GetDataTable(GlobalSpace.FSDBConnstr, sqlSelect);
        }
        private DataTable GetLabelVendorInfo(string type)
        {
            string sqlSelect = @"SELECT
	                                            VendorNumber AS 供应商码,
	                                            VendorName AS 供应商名,
                                                 Price AS 价格,
                                                 '' AS 长,
                                                 '' AS 宽
                                            FROM
	                                            PurchaseDepartmentForeignOrderPackageOthersByCMF
                                            WHERE
	                                            ItemType = '" + type + "'";
            return SQLHelper.GetDataTable(GlobalSpace.FSDBConnstr, sqlSelect);
        }
        private void rbtnOthers_CheckedChanged(object sender, EventArgs e)
        {
            dgvDetail.DataSource = GetVendorInfo("复合膜");
            cbUnit.Checked = true;
            cbArea.Checked = false;
        }

        private void rbtnCarbonLabel_CheckedChanged(object sender, EventArgs e)
        {
            dgvDetail.DataSource = GetLabelVendorInfo("箱签");
            cbUnit.Checked = false;
            cbArea.Checked = true;
        }

        private void rbtnBoxLabel_CheckedChanged(object sender, EventArgs e)
        {
            dgvDetail.DataSource = GetLabelVendorInfo("盒签");
            dgvDetail.DataSource = GetLabelVendorInfo("箱签");
            cbUnit.Checked = false;
            cbArea.Checked = true;
        }

        private void rbtnBucketLabel_CheckedChanged(object sender, EventArgs e)
        {
            dgvDetail.DataSource = GetLabelVendorInfo("桶签");
            dgvDetail.DataSource = GetLabelVendorInfo("箱签");
            cbUnit.Checked = false;
            cbArea.Checked = true;
        }

        private void rbtnSealLabel_CheckedChanged(object sender, EventArgs e)
        {
            dgvDetail.DataSource = GetLabelVendorInfo("封口签");
            dgvDetail.DataSource = GetLabelVendorInfo("箱签");
            cbUnit.Checked = false;
            cbArea.Checked = true;
        }

        private void rbtnAluminumBag_CheckedChanged(object sender, EventArgs e)
        {
            dgvDetail.DataSource = GetVendorInfo("铝箔袋");
            cbUnit.Checked = true;
            cbArea.Checked = false;
        }

        private void ForeignOrderItemOthers_Load(object sender, EventArgs e)
        {

        }
    }
}
