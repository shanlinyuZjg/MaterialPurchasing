using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;


namespace Global.Purchase
{
    public partial class ForeignOrderPackageSpecificationSetting : Office2007Form
    {
        public string userID = string.Empty;

        public ForeignOrderPackageSpecificationSetting(string userid)
        {
            userID = userid;
            this.EnableGlass = false;
            MessageBoxEx.EnableGlass = false;
            InitializeComponent();
        }

        private void tbLabelLength_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(tbCarbonLength.Text !="")
            {
                CommonOperate.TextBoxNext(tbCarbonLength, tbCarbonWidth, e);
            }
            else
            {
                MessageBoxEx.Show("长度不能为空！","提示");
            }
           
        }

        private void tbLabelWidth_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (tbCarbonWidth.Text != "")
            {
                CommonOperate.TextBoxNext(tbCarbonWidth, tbCarbonHeight, e);
            }
            else
            {
                MessageBoxEx.Show("宽度不能为空！", "提示");
            }         
        }

        private void tbLabelHeight_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar ==(char)13)
            {
                if(tbCarbonHeight.Text !="")
                {
                    tbCarbonArea.Text = GetCarbonArea(Convert.ToDouble(tbCarbonLength.Text.Trim()), Convert.ToDouble(tbCarbonWidth.Text.Trim()), Convert.ToDouble(tbCarbonHeight.Text.Trim())).ToString();
                }
                else
                {
                    MessageBoxEx.Show("高度不能为空！", "提示");
                }
            }
            CommonOperate.TextBoxNext(tbCarbonHeight, tbCarbonPaperBoardQuantity, e);           
        }
        /// <summary>
        /// 计算纸箱垫板面积
        /// </summary>
        /// <param name="count">垫板数量</param>
        /// <param name="length">长度</param>
        /// <param name="width">宽度</param>
        /// <returns></returns>
        private double GetCarbonBoardArea(int count, double length, double width)
        {
            double area = 0.00;

            area = (length - 10) * (width - 10) / 1000000;

            return area*count;

        }
        /// <summary>
        /// 计算纸箱面积
        /// </summary>
        /// <param name="length">长度</param>
        /// <param name="width">宽度</param>
        /// <param name="height">高度</param>
        /// <returns></returns>
        private double GetCarbonArea(double length,double width,double height)
        {
            double area = 0.00;
            double Length = 0.00;
            double Width = 0.00;
            Length = (length + width) * 2 + 80;
            Width = width + height + 50;
            area = Length * Width/1000000;

            return area;
        }
        private void tbCarbonPaperBoardQuantity_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar ==(char)13)
            {
                if(tbCarbonPaperBoardQuantity.Text !="")
                {
                    tbCarbonPaperBoardArea.Text = (GetCarbonBoardArea(Convert.ToInt32(tbCarbonPaperBoardQuantity.Text.Trim()), Convert.ToDouble(tbCarbonLength.Text.Trim()), Convert.ToDouble(tbCarbonWidth.Text))).ToString();
                }
                else
                {
                    MessageBoxEx.Show("垫板数量不能为空！", "提示");
                }               
            }
            btnCarbonSubmit.Focus();
        }

        private void tbLabelLength_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar ==(char)13)
            {
                if(tbLabelLength.Text !="")
                {
                    CommonOperate.TextBoxNext(tbLabelLength, tbLabelWidth, e);
                }
                else
                {
                    MessageBoxEx.Show("标签长度不能为空！", "提示");
                }
            }
        }

        private void tbLabelWidth_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                if (tbLabelWidth.Text != "")
                {
                    tbLabelArea.Text = GetLabelArea(Convert.ToDouble(tbLabelLength.Text.Trim()),Convert.ToDouble(tbLabelWidth.Text.Trim())).ToString();
                    btnLabelSubmit.Focus();
                }
                else
                {
                    MessageBoxEx.Show("标签宽度不能为空！", "提示");
                }
            }
        }
        /// <summary>
        /// 计算标签面积
        /// </summary>
        /// <param name="length">标签长度</param>
        /// <param name="width">标签宽度</param>
        /// <returns></returns>
        private double GetLabelArea(double length,double width)
        {
            double area = 0;
            area = length * width/1000000;

            return area;
        }

        private void tbBoxLength_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                if (tbBoxLength.Text != "")
                {
                    CommonOperate.TextBoxNext(tbBoxLength, tbBoxWidth, e);
                }
                else
                {
                    MessageBoxEx.Show("纸盒长度不能为空！", "提示");
                }
            }
        }

        private void tbBoxWidth_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                if (tbBoxWidth.Text != "")
                {
                    CommonOperate.TextBoxNext(tbBoxWidth, tbBoxHeight, e);
                }
                else
                {
                    MessageBoxEx.Show("纸盒宽度不能为空！", "提示");
                }
            }
        }

        private void tbBoxHeight_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                if (tbBoxHeight.Text != "")
                {
                    CommonOperate.TextBoxNext(tbBoxHeight, tbBoxTexture, e);
                }
                else
                {
                    MessageBoxEx.Show("纸盒高度不能为空！", "提示");
                }
            }
        }

        private void tbBoxTexture_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                if (tbBoxTexture.Text != "")
                {
                    CommonOperate.TextBoxNext(tbBoxTexture, tbBoxProcessRequirement, e);
                }
                else
                {
                    MessageBoxEx.Show("纸盒材质不能为空！", "提示");
                }
            }
        }

        private void tbBoxProcessRequirement_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                if (tbBoxProcessRequirement.Text != "")
                {
                    btnBoxSubmit.Focus();
                }
                else
                {
                    MessageBoxEx.Show("纸盒处理工艺要求不能为空！", "提示");
                }
            }
        }
    }
}
