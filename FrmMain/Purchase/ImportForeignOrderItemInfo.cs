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

namespace FrmMain.Purchase
{
    public partial class ImportForeignOrderItemInfo : Office2007Form
    {
        string userID = string.Empty;
        int iIndex = 0;
        public ImportForeignOrderItemInfo(string uid)
        {
            this.EnableGlass = false;
            MessageBoxEx.EnableGlass = false;
            userID = uid;
            InitializeComponent();
        }

        private void btnChooseFile_Click(object sender, EventArgs e)
        {
            CommonOperate.GetExcelFileInfo(tbFilePath, cbbSheet);
        }

        private void btnShow_Click(object sender, EventArgs e)
        {
            if(tbFilePath.Text == "")
            {
                Custom.MsgEx("路径不能为空！");
                return;
            }
            DataTable dt = null;
            string sqlSelect = string.Empty;
            if (dgvDetail.Rows.Count > 0)
            {
                dgvDetail.DataSource = null;
            }

            if (rbtnBox.Checked == true)
            {
                iIndex = 1;
                sqlSelect = @"Select [供应商码],[供应商名],[长],[宽],[高],[材质],[工艺要求及表面处理],[价格]    from [" + "纸盒$" + "]  Where [供应商名] <> ''";
            }

            else if (rbtnLabel.Checked == true)
            {
                iIndex = 2;
                sqlSelect = @"Select [供应商码],[供应商名],[长],[宽],[普通价格],[普通超出基准数量价格],[覆膜价格],[覆膜超出基准数量价格],[透明价格],[易撕价格] From [" + "瓶签$" + "]  Where [长]  <> 0";
            }
            else if (rbtnSpecification.Checked == true)
            {
                iIndex = 3;
                sqlSelect = @"Select [供应商码],[供应商名],[单色价格],[彩色价格]  From [" + "说明书$" + "]  Where [供应商名] <> ''";
            }
            else
            {
                iIndex = 4;
                sqlSelect = @"Select [供应商码],[供应商名],[纸箱价格],[格挡价格]  From [" + "纸箱$" + "]  Where [供应商名] <> ''";
            }
            dt = CommonOperate.ImportExcelFile(sqlSelect, tbFilePath);
            dgvDetail.DataSource = dt;

        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            if(dgvDetail.Rows.Count > 0)
            {
                DataTable dt = (DataTable)dgvDetail.DataSource;
                List<string> sqlInsertList = new List<string>();
                switch(iIndex)
                {
                    case 1:
                        foreach (DataRow dr in dt.Rows)
                        {
                            string sqlInsert = @"Insert Into PurchaseDepartmentForeignOrderPackageBoxByCMF (Length,Width,Height,Texture,ProcessRequirement,Price,Operator,VendorNumber,VendorName) Values (" + Convert.ToDouble(dr["长"])+","+ Convert.ToDouble(dr["宽"]) + ","+ Convert.ToDouble(dr["高"]) + ",'"+ dr["材质"].ToString() + "','"+ dr["工艺要求及表面处理"].ToString() + "',"+Convert.ToDouble(dr["价格"]) +",'"+userID+"','"+ dr["供应商码"].ToString() + "','"+ dr["供应商名"].ToString() + "')";
                            sqlInsertList.Add(sqlInsert);
                        }
                        break;
                    case 2:
                        foreach (DataRow dr in dt.Rows)
                        {
                            string sqlInsert = @"Insert Into PurchaseDepartmentForeignOrderPackageLabelSizeByCMF (Length,Width,Price,CommonHugeQuantityPrice,CoveredPrice,CoveredHugeQuantityPrice,TransparentPrice,ScrapPrice,Operator,VendorNumber,VendorName) Values (" + Convert.ToDouble(dr["长"]) + "," + Convert.ToDouble(dr["宽"]) + "," + Convert.ToDouble(dr["普通价格"]) + ","+ Convert.ToDouble(dr["普通超出基准数量价格"])+ "," + Convert.ToDouble(dr["覆膜价格"]) + "," + Convert.ToDouble(dr["覆膜超出基准数量价格"]) + ","+ Convert.ToDouble(dr["透明价格"]) + ","+ Convert.ToDouble(dr["易撕价格"]) + ",'" + userID + "','" + dr["供应商码"].ToString() + "', '"+ dr["供应商名"].ToString() + "')";
                            sqlInsertList.Add(sqlInsert);                       
                        }
                        break;
                    case 3:
                        foreach (DataRow dr in dt.Rows)
                        {
                            string sqlInsert = @"Insert Into PurchaseDepartmentForeignOrderPackageSpecificationByCMF (SingleColorPrice,ComplexColorPrice,Operator,VendorNumber,VendorName) Values (" + Convert.ToDouble(dr["单色价格"]) + "," + Convert.ToDouble(dr["彩色价格"]) + ",'"+ userID + "','" + dr["供应商码"].ToString() + "','" + dr["供应商名"].ToString() + "')";                        
                            sqlInsertList.Add(sqlInsert);
                        }
                        break;
                    case 4:
                        foreach (DataRow dr in dt.Rows)
                        {
                            string sqlInsert = @"Insert Into PurchaseDepartmentForeignOrderPackageCarbonByCMF (CarbonPrice,CellPrice,Operator,VendorNumber,VendorName) Values (" + Convert.ToDouble(dr["纸箱价格"]) + "," + Convert.ToDouble(dr["格挡价格"]) + ",'" + userID + "','" + dr["供应商码"].ToString() + "','" + dr["供应商名"].ToString() + "')";
                            sqlInsertList.Add(sqlInsert);
                        }
                        break;
                    default:
                        break;
                }

                if(SQLHelper.BatchExecuteNonQuery(GlobalSpace.FSDBConnstr,sqlInsertList))
                {
                    Custom.MsgEx("导入成功！");
                }
                else
                {
                    Custom.MsgEx("导入失败！");
                }
            }
            else
            {
                Custom.MsgEx("当前无可用信息！");
            }
        }
    }
}
