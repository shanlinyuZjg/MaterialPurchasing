using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using System.Data.SqlClient;
using Global.Helper;
using FrmMain;

namespace Global.Purchase
{
    public partial class ForeignOrderItemSubmit : Office2007Form
    {
        string userID = string.Empty;
        string userName = string.Empty;
              
        public ForeignOrderItemSubmit()
        {
            InitializeComponent();
        }

        public ForeignOrderItemSubmit(string userid,string username)
        {
            userID = userid;
            userName = username;
            MessageBoxEx.EnableGlass = false;
            InitializeComponent();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            foreach(Control ctl in groupPanel1.Controls)
            {
                if(ctl is TextBox)
                {
                    if(((TextBox)ctl).Text == "")
                    {
                        MessageBoxEx.Show("不能有空项！", "提示");
                        return;
                    }
                }
            }

            //判断当前标签、纸盒和纸箱的List数据是否为空，如果都为空，说明没有进行选择，程序直接返回。
            if(GlobalSpace.labelList.Count == 0 && GlobalSpace.boxList.Count == 0&& GlobalSpace.carbonList.Count == 0 && GlobalSpace.othersList.Count == 0&&GlobalSpace.specificationList.Count == 0)
            {
                MessageBoxEx.Show("提示", "包材没有选择，请选择后再进行添加！");
                return;
            }

            string supervisorID = string.Empty;
            string SpecificationDescription = string.Empty;
            string Requirements = string.Empty;
            double packagePrice = 0;
            double totalAmount = 0;
            supervisorID = CommonOperate.GetSuperiorId(userID);
            List<string> sqlList = new List<string>();
            if (rbtnLabel.Checked == true)
            {
                SpecificationDescription = "标签尺寸："+tbLabelSize.Text;          
                Requirements = tbLabelRequirements.Text;

                for (int i = 0; i < GlobalSpace.labelList.Count; i++)
                {
                    string sqlInsert = @"Insert Into PurchaseDepartmentForeignOrderItemByCMF (
                                        ForeignOrderNumber,
	                                    ItemNumber,ItemUM,
	                                    ItemDescription,
	                                    VendorNumber,
	                                    VendorName,	                                    PurchasePrice,Quantity,SupervisorID,BuyerID,SpecificationDescription,TotalAmount,Requirements)
                                    Values 
                                    ('" + tbForeignOrderNumber.Text.Trim() + "','" + tbItemNumber.Text.Trim() + "','" + tbUM.Text.Trim() + "','" + tbItemDescription.Text.Trim() + "','" + GlobalSpace.labelList[i].VendorNumber + "','" + GlobalSpace.labelList[i].VendorName + "',"+ GlobalSpace.labelList[i].Price+ ", "+Convert.ToDouble(tbQuantity.Text)+",'" + supervisorID + "','" + userID + "','" + SpecificationDescription + "',"+ Convert.ToDouble(tbQuantity.Text.Trim()) * GlobalSpace.labelList[i].Price + ",'" + Requirements + "')";
                    sqlList.Add(sqlInsert);
                }

            }
            if (rbtnCarbon.Checked == true)
            {
                SpecificationDescription = @"纸箱尺寸：" + tbCarbonSize.Text + "；格挡尺寸：" + tbCellSize.Text + "；纸箱垫板面积：" + tbCarbonAndPaperArea.Text + "；格挡面积：" + tbCellArea.Text + "";
                Requirements = tbCarbonRequirements.Text;

                for(int i = 0; i < GlobalSpace.carbonList.Count ; i++)
                {
                    string sqlInsert = @"Insert Into PurchaseDepartmentForeignOrderItemByCMF (
                                        ForeignOrderNumber,
	                                    ItemNumber,ItemUM,
	                                    ItemDescription,
	                                    VendorNumber,
	                                    VendorName,
	                                    PurchasePrice,Quantity,SupervisorID,BuyerID,SpecificationDescription,TotalAmount,Requirements)
                                    Values 
                                    ('" + tbForeignOrderNumber.Text.Trim() + "','" + tbItemNumber.Text.Trim() + "','" + tbUM.Text.Trim() + "','" + tbItemDescription.Text.Trim() + "','"+ GlobalSpace.carbonList[i].VendorNumber+ "','"+ GlobalSpace.carbonList[i].VendorName + "',"+ GlobalSpace.carbonList[i].CarbonUnitAreaPrice+ "," + Convert.ToDouble(tbQuantity.Text) + ",'" + supervisorID + "','" + userID + "','" + SpecificationDescription+"纸箱垫板价格："+ GlobalSpace.carbonList[i].CarbonUnitAreaPrice + "格挡价格：" + GlobalSpace.carbonList[i].CellUnitAreaPrice + "',"+ Convert.ToDouble(tbQuantity.Text.Trim()) * (GlobalSpace.carbonList[i].CarbonUnitAreaPrice*(GlobalSpace.carbonList[i].CarbonArea+ GlobalSpace.carbonList[i].PaperArea) + GlobalSpace.carbonList[i].CellUnitAreaPrice* GlobalSpace.carbonList[i].CellArea) + ",'" + Requirements + "')";
                    sqlList.Add(sqlInsert);
                }                                  
            }

            if (rbtnBox.Checked == true)
            {
                SpecificationDescription = "纸盒尺寸：" + tbBoxSize.Text.Trim();
                Requirements = "材质：" +tbBoxTexture.Text.Trim()+"，处理工艺："+tbBoxProcess.Text.Trim();
                for (int i = 0; i < GlobalSpace.boxList.Count; i++)
                {
                    string sqlInsert = @"Insert Into PurchaseDepartmentForeignOrderItemByCMF (
                                        ForeignOrderNumber,
	                                    ItemNumber,ItemUM,
	                                    ItemDescription,
	                                    VendorNumber,
	                                    VendorName,
	                                    PurchasePrice,Quantity,SupervisorID,BuyerID,SpecificationDescription,TotalAmount,Requirements)
                                    Values 
                                    ('" + tbForeignOrderNumber.Text.Trim() + "','" + tbItemNumber.Text.Trim() + "','" + tbUM.Text.Trim() + "','" + tbItemDescription.Text.Trim() + "','" + GlobalSpace.boxList[i].vendorNumber + "','" + GlobalSpace.boxList[i].vendorName + "',"+ GlobalSpace.boxList[i].BoxPrice+ ", " + Convert.ToDouble(tbQuantity.Text) + ",'" + supervisorID + "','" + userID + "','" + SpecificationDescription + "',"+Convert.ToDouble(tbQuantity.Text.Trim()) * GlobalSpace.boxList[i].BoxPrice + ",'" + Requirements + "')";
                    sqlList.Add(sqlInsert);
                }
            }

            if (rbtnSpecification.Checked == true)
            {
                SpecificationDescription = "颜色：" + tbSpecificationRemark.Text.Trim();
                for (int i = 0; i < GlobalSpace.specificationList.Count; i++)
                {
                    string sqlInsert = @"Insert Into PurchaseDepartmentForeignOrderItemByCMF (
                                        ForeignOrderNumber,
	                                    ItemNumber,ItemUM,
	                                    ItemDescription,
	                                    VendorNumber,
	                                    VendorName,
	                                    PurchasePrice,Quantity,SupervisorID,BuyerID,SpecificationDescription,TotalAmount,Requirements)
                                    Values 
                                    ('" + tbForeignOrderNumber.Text.Trim() + "','" + tbItemNumber.Text.Trim() + "','" + tbUM.Text.Trim() + "','" + tbItemDescription.Text.Trim() + "','" + GlobalSpace.specificationList[i].VendorNumber + "','" + GlobalSpace.specificationList[i].VendorName + "'," + GlobalSpace.specificationList[i].Price + ", " + Convert.ToDouble(tbQuantity.Text) + ",'" + supervisorID + "','" + userID + "','" + SpecificationDescription + "'," + Convert.ToDouble(tbQuantity.Text.Trim()) * GlobalSpace.specificationList[i].Price + ",'" + Requirements + "')";
                    sqlList.Add(sqlInsert);
                }
            }

            if (rbtnOthers.Checked == true)
            {
                SpecificationDescription = tbOthersRemark.Text;
            
                for (int i = 0; i < GlobalSpace.othersList.Count; i++)
                {
                    string sqlInsert = @"Insert Into PurchaseDepartmentForeignOrderItemByCMF (
                                        ForeignOrderNumber,
	                                    ItemNumber,ItemUM, 
	                                    ItemDescription,
	                                    VendorNumber,
	                                    VendorName,
	                                    PurchasePrice,Quantity,SupervisorID,BuyerID,SpecificationDescription,TotalAmount,Requirements)
                                    Values 
                                    ('" + tbForeignOrderNumber.Text.Trim() + "','" + tbItemNumber.Text.Trim() + "','" + tbUM.Text.Trim() + "','" + tbItemDescription.Text.Trim() + "','" + GlobalSpace.othersList[i].vendorNumber + "','" + GlobalSpace.othersList[i].vendorName + "'," + GlobalSpace.othersList[i].Price + ", " + Convert.ToDouble(tbQuantity.Text)* GlobalSpace.othersList[i].Price + ",'" + supervisorID + "','" + userID + "','" + SpecificationDescription + "'," + totalAmount + ",'" + Requirements + "')";
                    sqlList.Add(sqlInsert);
                }


            }            

          
            if(SQLHelper.BatchExecuteNonQuery(GlobalSpace.FSDBConnstr, sqlList))
            {
                MessageBoxEx.Show("添加成功！", "提示");

            }
            else
            {
                MessageBoxEx.Show("添加失败！", "提示");
            }
      
            tbQuantity.Text = "";
            tbItemNumber.Text = "";
            tbUM.Text = "";
            tbItemDescription.Text = "";
            LoadFOItemDetail(userID,99);


            if (rbtnBox.Checked == true)
            {
                foreach (Control ctl in gpBox.Controls)
                {
                    if (ctl is TextBox)
                    {
                        if (((TextBox)ctl).Text != "")
                        {
                            ctl.Text = "";                            
                        }
                    }
                }
            }
            if(rbtnCarbon.Checked == true)
            {
                foreach (Control ctl in gpCarbon.Controls)
                {
                    if (ctl is TextBox)
                    {
                        if (((TextBox)ctl).Text != "")
                        {
                            ctl.Text = "";
                        }
                    }
                }
            }

            if(rbtnLabel.Checked == true)
            {
                foreach (Control ctl in gpLabel.Controls)
                {
                    if (ctl is TextBox)
                    {
                        if (((TextBox)ctl).Text != "")
                        {
                            ctl.Text = "";
                        }
                    }
                }
            }

            if (rbtnOthers.Checked == true)
            {
                foreach (Control ctl in gpOthers.Controls)
                {
                    if (ctl is TextBox)
                    {
                        if (((TextBox)ctl).Text != "")
                        {
                            ctl.Text = "";
                        }
                    }
                }
            }


            rbtnBox.Checked = false;
            rbtnCarbon.Checked = false;
            rbtnLabel.Checked = false;

        }

        private void tbForeignOrderNumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            CommonOperate.TextBoxNext(tbForeignOrderNumber, tbItemNumber, e);
        }



        private void tbItemNumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar == (char)13)
            {
                List<string> list = CommonOperate.GetItemInfo(tbItemNumber.Text.Trim());
                if (list.Count > 0)
                {
                    tbUM.Text = list[0];
                    tbItemDescription.Text = list[1];
                }
                else
                {
                    MessageBoxEx.Show("未查到该物料的信息！", "提示");
                }
                tbQuantity.Text = "";
          
                tbQuantity.Text = "";
                CommonOperate.TextBoxNext(tbItemNumber, tbQuantity, e);
            }
           
        }

     

        private void tbPrice_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar ==(char)13)
            {
                btnAdd_Click(sender,e);
            }
        }

        private void ForeignOrderItemCheck_Load(object sender, EventArgs e)
        {
            LoadFOItemDetail(userID,99);
        }

        private void LoadFOItemDetail(string id,int status)
        {
            string sqlSelect = @"Select Id, ForeignOrderNumber AS 外贸单号, ItemNumber AS 物料代码,ItemDescription AS 物料描述,ItemUM AS 单位,VendorNumber AS 供应商码,VendorName AS 供应商名,PurchasePrice AS 价格,Quantity AS 采购数量,TotalAmount AS 总金额,SpecificationDescription AS 规格,Requirements AS 要求 From PurchaseDepartmentForeignOrderItemByCMF Where BuyerID='" + id + "'  And IsValid = 0 And Status = "+status+"";
            dgvUnHandledItem.DataSource = SQLHelper.GetDataTable(GlobalSpace.FSDBConnstr, sqlSelect);
            dgvUnHandledItem.Columns["Id"].Visible = false;
            dgvUnHandledItem.Columns["外贸单号"].ReadOnly = true;
            dgvUnHandledItem.Columns["物料代码"].ReadOnly = true;
            dgvUnHandledItem.Columns["物料描述"].ReadOnly = true;
            dgvUnHandledItem.Columns["单位"].ReadOnly = true;
        }

        private void tbItemNumber_TextChanged(object sender, EventArgs e)
        {
            tbItemNumber.Text = tbItemNumber.Text.ToUpper();
            tbItemNumber.SelectionStart = tbItemNumber.Text.Length;//避免光标在输入字母的前面
        }

        private void btnChooseItemAndSpecification_Click(object sender, EventArgs e)
        {
            if(GlobalSpace.boxList.Count > 0 )
            {
                GlobalSpace.boxList.Clear();
            }
            if (GlobalSpace.carbonList.Count > 0)
            {
                GlobalSpace.carbonList.Clear();
            }
            if (GlobalSpace.labelList.Count > 0)
            {
                GlobalSpace.labelList.Clear();
            }
            if (GlobalSpace.specificationList.Count > 0)
            {
                GlobalSpace.specificationList.Clear();
            }


            if (string.IsNullOrEmpty(tbQuantity.Text.Trim()))
            {
                Custom.MsgEx("采购数量不能为空！");
                return;
            }
            if(tbItemDescription.Text.Contains("签"))
            {
                Purchase.ForeignOrderItemLabel FOLabel = new Purchase.ForeignOrderItemLabel(Convert.ToDouble(tbQuantity.Text.Trim()));
                FOLabel.ShowDialog();
            }
            else if (tbItemDescription.Text.Contains("盒"))
            {
                Purchase.ForeignOrderItemBox FOBox = new Purchase.ForeignOrderItemBox();
                FOBox.ShowDialog();
            }
            else if (tbItemDescription.Text.Contains("箱"))
            {
                Purchase.ForeignOrderItemCarbon FOCarbon = new Purchase.ForeignOrderItemCarbon();
                FOCarbon.ShowDialog();
            }
            else if (tbItemDescription.Text.Contains("说明书"))
            {
                Purchase.ForeignOrderItemSpecification FOSpecification = new Purchase.ForeignOrderItemSpecification();
                FOSpecification.ShowDialog();
            }
            else
            {
                Purchase.ForeignOrderItemOthers FOOthers = new Purchase.ForeignOrderItemOthers();
                FOOthers.ShowDialog();
            }


            if (GlobalSpace.labelList.Count > 0)
            {
                tbLabelSize.Text = GlobalSpace.labelList[0].LabelSize;
                tbLabelRequirements.Text = GlobalSpace.labelList[0].LabelRequirements;
                rbtnLabel.Checked = true;
                rbtnOthers.Checked = false;
            }
            if (GlobalSpace.boxList.Count > 0)
            {
                tbBoxSize.Text = GlobalSpace.boxList[0].BoxSize;
                tbBoxTexture.Text = GlobalSpace.boxList[0].BoxTexture;
                tbBoxProcess.Text = GlobalSpace.boxList[0].BoxProcessRequirements;            
                rbtnBox.Checked = true;
                rbtnOthers.Checked = false;
            }
            if (GlobalSpace.specificationList.Count > 0)
            {
                tbSpecificationRemark.Text = GlobalSpace.specificationList[0].Color;
                rbtnSpecification.Checked = true;
                rbtnOthers.Checked = false;
            }
            if (GlobalSpace.carbonList.Count > 0)
            {
                Custom.MsgEx(GlobalSpace.carbonList.Count.ToString());
                tbCarbonSize.Text = GlobalSpace.carbonList[0].CarbonSize;
                tbCellSize.Text = GlobalSpace.carbonList[0].CellSize;        
                tbCarbonAndPaperArea.Text = (GlobalSpace.carbonList[0].CarbonArea+ GlobalSpace.carbonList[0].PaperArea).ToString();
                tbCellArea.Text = GlobalSpace.carbonList[0].CellArea.ToString();                      
                rbtnCarbon.Checked = true;
                rbtnOthers.Checked = false;
            }
        }

        private void tbOthersPrice_KeyPress(object sender, KeyPressEventArgs e)
        {
          
        }

        private void tbForeignOrderNumber_TextChanged(object sender, EventArgs e)
        {
            tbForeignOrderNumber.Text = tbForeignOrderNumber.Text.ToUpper();
            tbForeignOrderNumber.SelectionStart = tbForeignOrderNumber.Text.Length;//避免光标在输入字母的前面
        }

        private void tbCarbonPrice_TextChanged(object sender, EventArgs e)
        {

        }

        private void labelX21_Click(object sender, EventArgs e)
        {

        }

        private void tbQuantity_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar == (char)13)
            {
                btnChooseItemAndSpecification.Focus();
            }

        }

        private void btnChooseItemAndSpecification_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar == (char)13)
            {
                btnChooseItemAndSpecification_Click(sender, e);
            }
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            if(dgvUnHandledItem.SelectedRows.Count > 0)
            {
                List<string> sqlList = new List<string>();
                for(int i = 0;i < dgvUnHandledItem.SelectedRows.Count; i++)
                {
                    string sqlUpdate = @"Update PurchaseDepartmentForeignOrderItemByCMF Set Status = 0 Where Id= "+Convert.ToInt32(dgvUnHandledItem.SelectedRows[i].Cells["Id"].Value)+"";
                    sqlList.Add(sqlUpdate);
                }

                if(SQLHelper.BatchExecuteNonQuery(GlobalSpace.FSDBConnstr,sqlList))
                {
                    Custom.MsgEx("提交成功！");
                }
                else
                {
                    Custom.MsgEx("提交失败！");
                }
            }
            else
            {
                Custom.MsgEx("无选中的行！");
            }
        }

        private void rbUnSubmitted_CheckedChanged(object sender, EventArgs e)
        {
            if(rbUnSubmitted.Checked == true)
            {
                LoadFOItemDetail(userID, 99);
            }
        }


        private void rbSubmitted_CheckedChanged(object sender, EventArgs e)
        {
            if (rbSubmitted.Checked == true)
            {
                LoadFOItemDetail(userID, 0);
            }
        }

        private void rbPassed_CheckedChanged(object sender, EventArgs e)
        {
            if (rbPassed.Checked == true)
            {
                LoadFOItemDetail(userID, 1);
            }
        }

        private void 修改此项ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string sqlUpdate = @"Update PurchaseDepartmentForeignOrderItemByCMF Set  VendorNumber = '"+ dgvUnHandledItem.SelectedRows[0].Cells["供应商码"].Value.ToString() + "',VendorName = '"+ dgvUnHandledItem.SelectedRows[0].Cells["供应商名"].Value.ToString() + "',PurchasePrice="+Convert.ToDouble(dgvUnHandledItem.SelectedRows[0].Cells["价格"].Value) +", Quantity="+Convert.ToDouble(dgvUnHandledItem.SelectedRows[0].Cells["采购数量"].Value)+",TotalAmount="+Convert.ToDouble(dgvUnHandledItem.SelectedRows[0].Cells["总金额"].Value)+", SpecificationDescription='"+ dgvUnHandledItem.SelectedRows[0].Cells["规格"].Value.ToString() + "',Requirements= '"+ dgvUnHandledItem.SelectedRows[0].Cells["要求"].Value.ToString() + "' Where Id=" + Convert.ToInt32(dgvUnHandledItem.SelectedRows[0].Cells["Id"].Value)+"";
            //Custom.Msg(dgvUnHandledItem.SelectedRows[0].Cells["Id"].Value.ToString());
            if(SQLHelper.ExecuteNonQuery(GlobalSpace.FSDBConnstr,sqlUpdate))
            {
                Custom.MsgEx("修改成功！");
            }
            else
            {
                Custom.MsgEx("修改失败！");
            }
            LoadFOItemDetail(userID, 99);
        }

        private void 删除该项ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string sqlDelete = @"Update PurchaseDepartmentForeignOrderItemByCMF Set Status = 9 Where Id="+ Convert.ToInt32(dgvUnHandledItem.SelectedRows[0].Cells["Id"].Value) + "";
            if (SQLHelper.ExecuteNonQuery(GlobalSpace.FSDBConnstr, sqlDelete))
            {
                Custom.MsgEx("删除成功！");
            }
            else
            {
                Custom.MsgEx("删除失败！");
            }
            LoadFOItemDetail(userID, 99);
        }

        private void dgvUnHandledItem_MouseClick(object sender, MouseEventArgs e)
        {

        }

        private void dgvUnHandledItem_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if(e.Button == MouseButtons.Right)
            {
                if(e.RowIndex >= 0)
                {
                    if(e.ColumnIndex >= 0)
                    {
                        dgvUnHandledItem.ClearSelection();
                        dgvUnHandledItem.Rows[e.RowIndex].Selected = true;
                        this.contextMenuStrip1.Show(MousePosition.X, MousePosition.Y);
                    }
                }
            }
        }
    }
}
