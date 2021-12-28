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
    public partial class ForeignOrderItemCarbon : Office2007Form
    {
        public ForeignOrderItemCarbon()
        {
            this.EnableGlass = false;
            MessageBoxEx.EnableGlass = false;
            InitializeComponent();
        }

        private void tbCarbonLength_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                if (tbCarbonLength.Text == "")
                {
                    Custom.MsgEx("不能为空！");
                }
                else
                {
                    CommonOperate.TextBoxNext(tbCarbonLength, tbCarbonWidth, e);
                    dgvCarbon.DataSource = GetCarbonVendorInfo();
                }
            }
        }

        //查询纸箱供应商信息
        private DataTable GetCarbonVendorInfo()
        {
            string sqlSelect = @"Select CarbonPrice AS 纸箱价格,CellPrice AS 格挡价格,PaperPrice AS 垫板价格,VendorNumber AS 供应商码,VendorName AS 供应商名 From PurchaseDepartmentForeignOrderPackageCarbonByCMF";
            return SQLHelper.GetDataTable(GlobalSpace.FSDBConnstr, sqlSelect);
        }

        private void tbCarbonWidth_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                if (tbCarbonWidth.Text == "")
                {
                    Custom.MsgEx("不能为空！");
                }
                else
                {
                    CommonOperate.TextBoxNext(tbCarbonWidth, tbCarbonHeight, e);
                }
            }
        }

        private void tbCarbonHeight_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                if (tbCarbonHeight.Text != "")
                {
                    tbCarbonArea.Text = GetSingleCarbonArea(Convert.ToDouble(tbCarbonLength.Text), Convert.ToDouble(tbCarbonWidth.Text), Convert.ToDouble(tbCarbonHeight.Text)).ToString();
                    /*
                    string sqlSelect = @"SELECT DISTINCT
	                                                VendorNumber AS 供应商代码,
	                                                VendorName AS 供应商名称
                                                FROM
	                                                PurchaseDepartmentForeignOrderPackageCarbonByCMF";
                    DataTable dt = SQLHelper.GetDataTable(GlobalSpace.FSDBConnstr, sqlSelect);
                    dgvCarbon.DataSource = dt;*/
                    //    CommonOperate.TextBoxNext(tbCarbonHeight, tbCarbonPaperBoardQuantity, e);
                }
                else
                {
                    Custom.MsgEx("不能为空！");
                }
            }
        }
        private double GetSingleCarbonArea(double length, double width, double height)
        {
            return (((length + width) * 2 + 80) * (width + height + 50)) / 1000000;
        }
        /// <summary>
        /// 计算纸箱面积
        /// </summary>
        /// <param name="carbonQuantity">纸箱数量</param>
        /// <param name="paperboardQuantityPerCarbon">一个纸箱中垫板数量</param>
        /// <param name="length">纸箱长度</param>
        /// <param name="width">纸箱宽度</param>
        /// <param name="height">纸箱高度</param>
        /// <returns></returns>
        private double GeSingletCarbonWithPaperboardArea(int paperboardQuantityPerCarbon,double length,double width,double height)
        {
            double sumArea = 0;
            //总面积=单个纸箱面积+单个垫板面积*一个纸箱中垫板数量
            sumArea = (((length+width)*2+80)*(width+height+50))/1000000 + ((length-10)*(width-10)*2)/1000000* paperboardQuantityPerCarbon ;
            return sumArea;
        }
        private void tbCarbonPaperBoardQuantity_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(!cbPaperboard.Checked)
            {
                Custom.MsgEx("请先选中垫板框！");
            }
            else
            {
                if (e.KeyChar == (char)13)
                {
                    if (tbCarbonPaperBoardQuantity.Text != "")
                    {
                        tbCarbonPaperBoardArea.Text = GetSingePaperboardArea(Convert.ToInt32(tbCarbonPaperBoardQuantity.Text), Convert.ToDouble(tbCarbonLength.Text), Convert.ToDouble(tbCarbonWidth.Text), Convert.ToDouble(tbCarbonHeight.Text)).ToString();
                        tbCarbonRequirements.Focus();
                    }
                    else
                    {
                        Custom.MsgEx("不能为空！");
                    }
                }
            }
            
        }
        private double GetSingePaperboardArea(int paperboardQuantityPerCarbon, double length, double width, double height)
        {
            return ((length - 10) * (width - 10)) / 1000000 * paperboardQuantityPerCarbon;
        }
        private double GetSingleCarbonCellArea(int rowQuantity, int ColumnQuantity, double length, double width, double height)
        {
            return (length * height * rowQuantity + width * height * ColumnQuantity);
        }
        private void tbCellLength_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(!cbCell.Checked)
            {
                Custom.MsgEx("请先选中格挡框！");
            }
            else
            {
                if (e.KeyChar == (char)13)
                {
                    if (tbCellLength.Text != "")
                    {
                        CommonOperate.TextBoxNext(tbCellLength, tbCellWidth, e);
                    }
                    else
                    {
                        Custom.MsgEx("不能为空！");
                    }
                }
            }            
        }

        private void tbCellWidth_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                if (tbCellWidth.Text != "")
                {
                    CommonOperate.TextBoxNext(tbCellWidth, tbCellHeight, e);
                }
                else
                {
                    Custom.MsgEx("不能为空！");
                }
            }
        }

        private void tbCellHeight_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                if (tbCellHeight.Text != "")
                {
                    CommonOperate.TextBoxNext(tbCellHeight, tbCellRowQuantity, e);
                }
                else
                {
                    Custom.MsgEx("不能为空！");
                }
            }
        }

        private void tbCellRowQuantity_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                if (tbCellRowQuantity.Text != "")
                {
                    CommonOperate.TextBoxNext(tbCellRowQuantity, tbCellColumnQuantity, e);
                }
                else
                {
                    Custom.MsgEx("不能为空！");
                }
            }
        }

        private void tbCellColumnQuantity_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                if (tbCellColumnQuantity.Text != "")
                {
                    tbCellArea.Text = GetSingleCarbonCellArea(Convert.ToInt32(tbCellRowQuantity.Text), Convert.ToInt32(tbCellColumnQuantity.Text), Convert.ToDouble(tbCellLength.Text), Convert.ToDouble(tbCellWidth.Text), Convert.ToDouble(tbCellHeight.Text) / 1000000).ToString();                   
                }
                else
                {
                    Custom.MsgEx("不能为空！");
                }
            }
        }

        private void btnCarbonSubmit_Click(object sender, EventArgs e)
        {
            if (tbCarbonArea.Text == "")
            {
                MessageBoxEx.Show("提示", "不能有空项！");
            }
            else
            {
                if (dgvCarbon.SelectedRows.Count > 0)
                {
                    double paperArea = 0;
                    double cellArea = 0;
                    int cellLength = 0;
                    int cellWidth = 0;
                    int cellHeight = 0;
                    int cellRow = 0;
                    int cellColumn = 0;
                    if (cbPaperboard.Checked == true)
                    {
                        paperArea = Convert.ToDouble(tbCarbonPaperBoardArea.Text);
                    }
                    if (cbCell.Checked == true)
                    {
                        cellArea = Convert.ToDouble(tbCellArea.Text);
                        cellLength = Convert.ToInt32(tbCellLength.Text);
                        cellWidth = Convert.ToInt32(tbCellWidth.Text);
                        cellHeight = Convert.ToInt32(tbCellHeight.Text);

                        cellRow = Convert.ToInt32(tbCellRowQuantity.Text);
                        cellColumn = Convert.ToInt32(tbCellColumnQuantity.Text);
                    }

                    for (int i = 0; i < dgvCarbon.SelectedRows.Count; i++)
                    {
                        Carbon carbon = new Carbon();
                        carbon.CarbonSize = tbCarbonLength.Text + "×" + tbCarbonWidth.Text + "×" + tbCarbonHeight.Text;
                        carbon.CellSize = cellLength.ToString() + "×" + cellWidth.ToString() + "×" + cellHeight.ToString();
                        carbon.CellSizeLenWidQuantity = "长数:" + cellRow.ToString() + "宽数:" + cellColumn.ToString();

                        carbon.CarbonArea = Convert.ToDouble(tbCarbonArea.Text);
                        carbon.PaperArea = paperArea;
                        carbon.CellArea = cellArea;
                        carbon.CarbonUnitAreaPrice = Convert.ToDouble(dgvCarbon.SelectedRows[i].Cells["纸箱价格"].Value);
                        carbon.PaperUnitAreaPrice = Convert.ToDouble(dgvCarbon.SelectedRows[i].Cells["垫板价格"].Value);
                        carbon.CellUnitAreaPrice = Convert.ToDouble(dgvCarbon.SelectedRows[i].Cells["格挡价格"].Value);
                        carbon.CarbonRequirements = tbCarbonRequirements.Text;
                        carbon.VendorNumber = dgvCarbon.SelectedRows[i].Cells["供应商码"].Value.ToString();
                        carbon.VendorName = dgvCarbon.SelectedRows[i].Cells["供应商名"].Value.ToString();
                        carbon.Bexist = true;
                        GlobalSpace.carbonList.Add(carbon);
                    }
                }
                else
                {
                    Custom.MsgEx("无选中的行！");
                }
                this.Close();
            }
        }

        private void cbPaperboard_CheckedChanged(object sender, EventArgs e)
        {
            if(cbPaperboard.Checked == true)
            {
                tbCarbonPaperBoardQuantity.Focus();
            }
        }

        private void cbCell_CheckedChanged(object sender, EventArgs e)
        {
            if(cbCell.Checked == true)
            {
                tbCellLength.Focus();
            }
        }
    }
}
