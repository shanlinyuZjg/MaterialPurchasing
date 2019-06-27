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
    public partial class ForeignOrderItemChooseToSubmit : Office2007Form
    {
        string userID = string.Empty;
        double dQuantity = 0;
        string sqlBoxLengthCriterion = string.Empty;
        string sqlBoxWidthCriterion = string.Empty;
        string sqlBoxHeightCriterion = string.Empty;
        string sqlLabelLengthCriterion = string.Empty;
        string sqlLabelWidthCriterion = string.Empty;
        //List<Carbon> carbonList = new List<Carbon>();
        //List<Label> labelList = new List<Label>();
        //List<Box> boxList = new List<Box>();

        string sqlBoxSelect = @"Select Length as 长度,Width as 宽度,Height as 高度,Texture as 材质,ProcessRequirement as 处理工艺,Price AS 价格,VendorNumber AS 供应商码 ,VendorName AS 供应商名 From PurchaseDepartmentForeignOrderPackageBoxByCMF ";      
        string sqlLabelSelect = @"Select Length as 长度,Width as 宽度,Price as 价格,CoveredPrice as 覆膜价格,VendorNumber AS 供应商码 ,VendorName AS 供应商名  From PurchaseDepartmentForeignOrderPackageLabelSizeByCMF ";
        public ForeignOrderItemChooseToSubmit(string id,double quantity)
        {
            userID = id;
            dQuantity = quantity;
            this.EnableGlass = false;
            MessageBoxEx.EnableGlass = false;
            InitializeComponent();
        }

        private void tbBoxLength_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(tbBoxLength.Text !="")
            {
                if(e.KeyChar==(char)13)
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
            if(tbBoxWidth.Text !="")
            {
                if(e.KeyChar ==(char)13)
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
                if(e.KeyChar ==(char)13)
                {
                    string sqlBoxTemp = sqlBoxSelect;
                    sqlBoxHeightCriterion = @"  Height >=" + (Convert.ToDouble(tbBoxHeight.Text.Trim()) - 5) + " and Height <=" + (Convert.ToDouble(tbBoxHeight.Text.Trim()) + 5) + "";
                    sqlBoxTemp = sqlBoxTemp + " Where  " + sqlBoxLengthCriterion + "   And  " + sqlBoxWidthCriterion + "   And  " + sqlBoxHeightCriterion;
                    CommonOperate.DataGridViewShow(sqlBoxTemp, GlobalSpace.FSDBConnstr, dgvBox);
                }

            }
        }

        private void tbLabelLength_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(tbLabelLength.Text !="")
            {
                if(e.KeyChar ==(char)13)
                {
                    string sqlLabelTemp = sqlLabelSelect;
                    sqlLabelLengthCriterion = @" Length ="+Convert.ToDouble(tbLabelLength.Text.Trim())+"";
                    sqlLabelTemp = sqlLabelTemp + " Where " + sqlLabelLengthCriterion;
                    CommonOperate.DataGridViewShow(sqlLabelTemp, GlobalSpace.FSDBConnstr, dgvLabel);
                    CommonOperate.TextBoxNext(tbLabelLength, tbLabelWidth, e);
                }
            }
        }

        private void tbLabelWidth_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(tbLabelWidth.Text !="")
            {
                if(e.KeyChar ==(char)13)
                {                    
                    string sqlLabelTemp = sqlLabelSelect;
                    sqlLabelWidthCriterion = @" Width = " + Convert.ToDouble(tbLabelWidth.Text.Trim()) + "";
                    sqlLabelTemp = sqlLabelTemp + " Where " + sqlLabelLengthCriterion + " And " + sqlLabelWidthCriterion;
                    CommonOperate.DataGridViewShow(sqlLabelTemp, GlobalSpace.FSDBConnstr, dgvLabel);
                  
                }
            }
        }

        private void dgvBox_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            dgvBox_CellDoubleClick(sender, e);
        }

        private void dgvBox_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.RowIndex >= 0)
            {
                tbBoxLength2.Text = dgvBox.Rows[e.RowIndex].Cells["长度"].Value.ToString();
                tbBoxWidth2.Text = dgvBox.Rows[e.RowIndex].Cells["宽度"].Value.ToString();
                tbBoxHeight2.Text = dgvBox.Rows[e.RowIndex].Cells["高度"].Value.ToString();
                tbBoxTexture.Text = dgvBox.Rows[e.RowIndex].Cells["材质"].Value.ToString();
                tbBoxProcessRequirement.Text = dgvBox.Rows[e.RowIndex].Cells["处理工艺"].Value.ToString();
                tbBoxUnitPrice.Text = dgvBox.Rows[e.RowIndex].Cells["价格"].Value.ToString();
            }

        }

        private void btnBoxSubmit_Click(object sender, EventArgs e)
        {
            if(dgvBox.SelectedRows.Count > 0)
            {
                for(int i = 0;i < dgvBox.SelectedRows.Count;i++)
                {
                    Box box = new Box();
                    box.BoxSize = dgvBox.SelectedRows[i].Cells["长度"].Value.ToString() + "x" + dgvBox.SelectedRows[i].Cells["宽度"].Value.ToString() + "x" + dgvBox.SelectedRows[i].Cells["高度"].Value.ToString();
                    box.BExist = true;
                    box.BoxPrice = Convert.ToDouble( dgvBox.SelectedRows[i].Cells["价格"].Value);
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

        private void btnLabelSubmit_Click(object sender, EventArgs e)
        {
            if(rbtnLabelMannual.Checked == true)
            {               
                Custom.MsgEx("手工记录只能点击无记录添加按钮！");                
            } 
            else
            {
                double labelPrice = 0;
                int k = 0;
                string requirements = string.Empty;

                if(rbtnLabelCommonExist.Checked == true)
                {
                    if (dQuantity >= 200000)
                    {
                        labelPrice = Convert.ToDouble(tbLabelCommonNotExistPrice.Text);
                        k = 1;
                        requirements = "普通标签";
                    }
                    else
                    {
                        labelPrice = Convert.ToDouble(tbLabelCommonExistPrice.Text);
                        k = 2;
                        requirements = "普通标签";

                    }
                }

                if(rbtnLabelCoveredExist.Checked == true)
                {
                    labelPrice = Convert.ToDouble(tbLabelCoveredExistPrice.Text);
                    k = 3;
                    requirements = "覆膜标签";

                }
                if (rbtnLabelTransparent.Checked == true)
                {
                    labelPrice = Convert.ToDouble(tbLabelTransparentPrice.Text);
                    k = 4;
                    requirements = "透明标签";

                }
                if (rbtnLabelScrap.Checked == true)
                {
                    labelPrice = Convert.ToDouble(tbLabelScrapPrice.Text);
                    k = 5;
                    requirements = "易撕标签";

                }

                if (dgvLabel.SelectedRows.Count > 0)
                {
                    for(int i = 0; i < dgvLabel.SelectedRows.Count; i++)
                    {
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

        /*
        private void GetLabelAmount(double area)
        {
            if (rbtnLabelTransparent.Checked == true)
            {
                //透明标签金额=长*宽/1000000*透明标签单位面积的价格
                GlobalSpace.labelList.Add((area / 1000000 * Convert.ToDouble(tbLabelTransparentPrice.Text.Trim())).ToString());
                GlobalSpace.labelList.Add("透明标签");
            }
            else
            {
                if (rbtnLabelCoveredExist.Checked == true)
                {
                    //已有规格覆膜标签金额=采购数量*价格
                    GlobalSpace.labelList.Add((Convert.ToDouble(tbLabelCoveredExistPrice.Text.Trim())).ToString());
                    GlobalSpace.labelList.Add("覆膜标签，已有规格");
                }
                else
                {
                    if (rbtnLabelCoveredNotExist.Checked == true)
                    {
                        //当前无规格的覆膜标签金额=长*宽/1000000*覆膜标签单位面积的价格
                        GlobalSpace.labelList.Add((area/1000000  * Convert.ToDouble(tbLabelCoveredNotExistPrice.Text.Trim())).ToString());
                        GlobalSpace.labelList.Add("覆膜标签，规格不存在");
                    }
                    else
                    {
                        if (rbtnLabelCommonExist.Checked == true)
                        {
                            //如果采购数量不小于20万，则按照面积收费，否则按照已有价格按照单价*数量收费
                            if (dQuantity >= 200000)
                            {
                                //已有规格标签金额=标签面积*单位面积价格
                                GlobalSpace.labelList.Add((area / 1000000  * Convert.ToDouble(tbLabelCommonExistPrice.Text.Trim())).ToString());
                                GlobalSpace.labelList.Add("普通标签，已有规格");
                            }
                            else
                            {
                                //已有规格普通标签金额=采购数量*价格
                                GlobalSpace.labelList.Add((Convert.ToDouble(tbLabelCommonExistPrice.Text.Trim())).ToString());
                                GlobalSpace.labelList.Add("普通标签，已有规格");
                            }
                        }
                        else
                        {
                            if (rbtnLabelCommonNotExist.Checked == true)
                            {
                                //普通标签标签金额=长*宽/1000000*透明标签单位面积的价格
                                GlobalSpace.labelList.Add((area / 1000000 * Convert.ToDouble(tbLabelCommonNotExistPrice.Text.Trim())).ToString());
                                GlobalSpace.labelList.Add("普通标签，规格不存在");
                            }
                        }
                    }
                }
            }
        }
        */
        private void dgvLabel_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            dgvLabel_CellDoubleClick(sender, e);
        }

        private void dgvLabel_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                if(dQuantity == 0)
                {
                    MessageBoxEx.Show("采购数量不能为空！", "提示");
                }
                else
                {
                    tbLabelLength2.Text = dgvLabel.Rows[e.RowIndex].Cells["长度"].Value.ToString();
                    tbLabelWidth2.Text = dgvLabel.Rows[e.RowIndex].Cells["宽度"].Value.ToString();
                    tbLabelCommonExistPrice.Text = dgvLabel.Rows[e.RowIndex].Cells["价格"].Value.ToString();
                    tbLabelCoveredExistPrice.Text = dgvLabel.Rows[e.RowIndex].Cells["覆膜价格"].Value.ToString();
                    tbLabelArea.Text = (Convert.ToDouble(tbLabelLength2.Text.Trim()) * Convert.ToDouble(tbLabelWidth2.Text.Trim())).ToString();
                }             
            }
        }

        private void btnCarbonSubmit_Click(object sender, EventArgs e)
        {
            if(tbCarbonArea.Text =="")
            {
                MessageBoxEx.Show("提示", "不能有空项！");
            }
            else
            {
                if(dgvCarbon.SelectedRows.Count > 0)
                {
                    double paperArea = 0;
                    double cellArea = 0;
                    int cellLength = 0;
                    int cellWidth = 0;
                    int cellHeight = 0;
                    int cellRow = 0;
                    int cellColumn = 0;
                    if(cbPaperboard.Checked == true)
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

                    for(int i = 0;i < dgvCarbon.SelectedRows.Count;i++)
                    {
                        Carbon carbon = new Carbon();
                        carbon.CarbonSize = tbCarbonLength.Text + "×" + tbCarbonWidth.Text + "×" + tbCarbonHeight.Text;
                        carbon.CellSize = cellLength.ToString() + "×" + cellWidth.ToString() + "×" + cellHeight.ToString(); 
                        carbon.CellSizeLenWidQuantity = "长数:" +cellRow.ToString()+"宽数:"+cellColumn.ToString() ;
                        
                        carbon.CarbonArea = Convert.ToDouble(tbCarbonArea.Text) ;
                        carbon.PaperArea = paperArea ;
                        carbon.CellArea = cellArea;
                        carbon.CarbonUnitAreaPrice = Convert.ToDouble(tbCarbonUnitPrice.Text) ;
                        carbon.CellUnitAreaPrice = Convert.ToDouble(tbCellUnitPrice.Text) ;
                        carbon.CarbonRequirements = tbCarbonRequirements.Text;
                        carbon.VendorNumber = dgvCarbon.SelectedRows[i].Cells["供应商代码"].Value.ToString();
                        carbon.VendorName = dgvCarbon.SelectedRows[i].Cells["供应商名称"].Value.ToString();
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
            string sqlSelect = @"Select PaperPrice AS 纸张价格,CellPrice AS 格挡价格,VendorNumber AS 供应商码,VendorName AS 供应商名 From PurchaseDepartmentForeignOrderPackageBoxByCMF";
            return SQLHelper.GetDataTable(GlobalSpace.FSDBConnstr,sqlSelect);
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
                    string sqlSelect = @"SELECT DISTINCT
	                                                VendorNumber AS 供应商代码,
	                                                VendorName AS 供应商名称
                                                FROM
	                                                PurchaseDepartmentForeignOrderPackageCarbonByCMF Where Status = 0";
                    DataTable dt = SQLHelper.GetDataTable(GlobalSpace.FSDBConnstr, sqlSelect);
                    dgvCarbon.DataSource = dt;
                    //    CommonOperate.TextBoxNext(tbCarbonHeight, tbCarbonPaperBoardQuantity, e);
                }
                else
                {
                    Custom.MsgEx("不能为空！");
                }
            }
        }
        private double GetSingePaperboardArea(int paperboardQuantityPerCarbon, double length, double width, double height)
        {
            return ((length - 10) * (width - 10) * 2) / 1000000 * paperboardQuantityPerCarbon ;
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

        private double GetLabelArea(double labelQuantity,double length,double width)
        {
            double sumArea = 0;
            sumArea = (length*width)/1000000 * labelQuantity;

            return sumArea;
        }

        private void ForeignOrderItemChooseToSubmit_Load(object sender, EventArgs e)
        {
            GetCarbonAndCellUnitPrice();
        }
        //获取纸箱和格挡的单价
        private void GetCarbonAndCellUnitPrice()
        {
            string sqlSelect = @"Select Item,PricePreTax From PurchaseDepartmentForeignOrderPackageCarbonByCMF Where Item='"+ "纸箱" + "' Or Item='"+"格挡"+"'";
            DataTable dt = SQLHelper.GetDataTable(GlobalSpace.FSDBConnstr, sqlSelect);
            if(dt.Rows.Count == 2)
            {
                foreach(DataRow dr in dt.Rows)
                {
                    if(dr["Item"].ToString() == "纸箱")
                    {
                        tbCarbonUnitPrice.Text = dr["PricePreTax"].ToString();
                    }
                    if (dr["Item"].ToString() == "格挡")
                    {
                        tbCellUnitPrice.Text = dr["PricePreTax"].ToString();
                    }
                }
            }
            else
            {
                Custom.MsgEx("未查到纸箱和格挡的价格信息！");
            }
        }
        private double GetLabelCommonExistPrice()
        {
            double price = 0;

            return price;
        }

        private double GetLabelCommonNotExistPrice()
        {
            double price = 0;

            return price;
        }

        private void rbtnTransparent_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void tbLabelQuantity_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        //获取已有规格标签的尺寸
        private string GetExistLabelSpecification()
        {
            return "";
        }

        private void rbtnLabelCommonExist_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtnLabelCommonExist.Checked == true)
            {
                if (dQuantity == 0)
                {
                    MessageBoxEx.Show("采购数量不能为空！", "提示");
                }
                else
                {
                    if(dQuantity >= 200000)
                    {
                        tbLabelCommonExistPrice.Text = "5";
                    }
                }
            }
        }

        private void rbtnLabelCommonNotExist_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void btnLabelAddNewSpecification_Click(object sender, EventArgs e)
        {

        }

        private void btnBoxAddNewSpecification_Click(object sender, EventArgs e)
        {

        }

        private void tbCarbonPaperBoardQuantity_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar == (char)13)
            {
                if(tbCarbonPaperBoardQuantity.Text !="")
                {
                    tbCarbonPaperBoardArea.Text = GetSingePaperboardArea(Convert.ToInt32(tbCarbonPaperBoardQuantity.Text),Convert.ToDouble(tbCarbonLength.Text), Convert.ToDouble(tbCarbonWidth.Text), Convert.ToDouble(tbCarbonHeight.Text)).ToString();
                }
                else
                {
                    Custom.MsgEx("不能为空！");
                }
            }
        }

        private void tbCellLength_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar ==(char) 13)
            {
                if(tbCellLength.Text !="")
                {
                    CommonOperate.TextBoxNext(tbCellLength, tbCellWidth, e);
                }
                else
                {
                    Custom.MsgEx("不能为空！");
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
                    tbCellArea.Text = GetSingleCarbonCellArea(Convert.ToInt32(tbCellRowQuantity.Text), Convert.ToInt32(tbCellColumnQuantity.Text), Convert.ToDouble(tbCellLength.Text), Convert.ToDouble(tbCellWidth.Text), Convert.ToDouble(tbCellHeight.Text)/1000000).ToString();
                    CommonOperate.TextBoxNext(tbCellColumnQuantity, tbCellUnitPrice, e);
                }
                else
                {
                    Custom.MsgEx("不能为空！");
                }
            }
        }
        private double GetSingleCarbonCellArea(int rowQuantity,int ColumnQuantity,double length,double width,double height)
        {
            return (length*height*rowQuantity+width*height*ColumnQuantity) ;
        }

        private void rbtnLabelMannual_CheckedChanged(object sender, EventArgs e)
        {
            if(rbtnLabelMannual.Checked == true)
            {
                tbLabelMannualArea.Text = (Convert.ToDouble(tbLabelLength.Text.Trim()) * Convert.ToDouble(tbLabelWidth.Text.Trim())).ToString();
            }

        }

        private void dgvCarbon_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            dgvCarbon_CellContentDoubleClick(sender, e);
        }

        private void dgvCarbon_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                tbCarbonVendorNumber.Text = dgvCarbon["供应商码", e.RowIndex].Value.ToString();
                tbCarbonVendorName.Text = dgvCarbon["供应商名", e.RowIndex].Value.ToString();
                tbCarbonUnitPrice.Text =  dgvCarbon["纸张价格", e.RowIndex].Value.ToString();
                tbCellUnitPrice.Text = dgvCarbon["格挡价格", e.RowIndex].Value.ToString();
            }            
        }

        private void btnLabelAddWithoutRecord_Click(object sender, EventArgs e)
        {
            if(rbtnLabelMannual.Checked == true)
            {
                Label label = new Label();
                label.LabelSize = tbLabelLength.Text + "×" + tbLabelWidth.Text;
                label.VendorNumber = tbLabelVendorNumber.Text;
                label.VendorName = tbLabelVendorName.Text;
                label.BExist = true;
                double labelUnitPricePerSquareMeter = 0;
                string labelRequirements = string.Empty;
                
                if(rbtnLabelCommonNotExist.Checked == true)
                {
                    labelUnitPricePerSquareMeter = Convert.ToDouble(tbLabelCommonNotExistPrice.Text);
                    labelRequirements = "普通标签，无价格";
                }
                if(rbtnLabelCoveredNotExist.Checked == true)
                {
                    labelUnitPricePerSquareMeter = Convert.ToDouble(tbLabelCoveredExistPrice.Text);
                    labelRequirements = "覆膜标签，无价格";
                }
                if (rbtnLabelTransparent.Checked == true)
                {
                    labelUnitPricePerSquareMeter = Convert.ToDouble(tbLabelTransparentPrice.Text);
                    labelRequirements = "透明标签";
                }
                if (rbtnLabelScrap.Checked == true)
                {
                    labelUnitPricePerSquareMeter = Convert.ToDouble(tbLabelScrapPrice.Text);
                    labelRequirements = "易撕标签";
                }
                label.Price = labelUnitPricePerSquareMeter;
                label.LabelRequirements = labelRequirements;
                label.BExist = false;
                GlobalSpace.labelList.Add(label);
                this.Close();
            }
            else
            {
                Custom.MsgEx("未选中手工！");
            }
        }


        private void btnLabelRepeatAdd_Click(object sender, EventArgs e)
        {

        }

        private void btnBoxAddWithoutRecord_Click(object sender, EventArgs e)
        {
            if(rbtnBoxMannual.Checked == true)
            {
                Box box = new Box();
                box.BoxSize = tbBoxLength.Text + "x" +tbBoxWidth.Text+ "x" + tbBoxHeight.Text;
                box.BExist = false;
                box.BoxPrice = Convert.ToDouble(tbBoxUnitPrice.Text);
                box.BoxTexture = tbBoxTexture.Text;
                box.BoxProcessRequirements = tbBoxProcessRequirement.Text;
                box.vendorNumber = tbBoxVendorNumber.Text;
                box.vendorName = tbBoxVendorNumber.Text;
                GlobalSpace.boxList.Add(box);
                this.Close();
            }
            else
            {
                Custom.MsgEx("未选中手工输入！");
            }
        }
    }
}
