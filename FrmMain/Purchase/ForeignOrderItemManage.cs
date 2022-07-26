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
using Global;

namespace Global.Purchase
{
    public partial class ForeignOrderItemManage:Office2007Form
    {
        string UserID = string.Empty;
        public ForeignOrderItemManage(string userID)
        {
            InitializeComponent();
            this.EnableGlass = false;
            MessageBoxEx.EnableGlass = false;
            UserID = userID;
        }

        private void ForeignOrderItemManage_Load(object sender, EventArgs e)
        {
            //tabControl1.SelectedTabIndex = 0;
            GetSpec();
            GetBox();
            GetCarbon();
        }
        //获取所有纸箱的信息
        private void GetCarbon()
        {
            string sqlSelect = @"Select VendorNumber As  供应商码,VendorName AS 供应商名,CarbonPrice AS 纸箱价格,CellPrice AS 格挡价格,PaperPrice as 垫板价格,Id  From PurchaseDepartmentForeignOrderPackageCarbonByCMF order by Id desc";
            dgvCarbon.DataSource = SQLHelper.GetDataTable(GlobalSpace.FSDBConnstr, sqlSelect);
           

        }
        //获取所有说明书的信息
        private void GetSpec()
        {
            string sqlSelect = @"Select VendorNumber As  供应商码,VendorName AS 供应商名,SingleColorPrice AS 单色价格,ComplexColorPrice AS 彩色价格,Id  From PurchaseDepartmentForeignOrderPackageSpecificationByCMF order by Id desc";
            dgvSpecification.DataSource = SQLHelper.GetDataTable(GlobalSpace.FSDBConnstr, sqlSelect);
            //dgvSpecification.Columns["Id"].Visible = false;
            for (int i = 0; i < this.dgvSpecification.Columns.Count; i++)
            {
                //this.dgvSpecification.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
                this.dgvSpecification.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            }

        }
        //获取所有纸盒的信息
        private void GetBox()
        {
            string sqlSelect = @"Select Length as 长度,Width as 宽度,Height as 高度,Texture as 材质,ProcessRequirement as 处理工艺,Price AS 价格,VendorNumber AS 供应商码 ,VendorName AS 供应商名,Id From PurchaseDepartmentForeignOrderPackageBoxByCMF ";
            dgvBox.DataSource= SQLHelper.GetDataTable(GlobalSpace.FSDBConnstr, sqlSelect);
            for (int i = 0; i < this.dgvBox.Columns.Count; i++)
            {
                //if (this.dgvBox.Columns[i].Name != "BoxCheck")
                //{
                //    this.dgvBox.Columns[i].ReadOnly = true;
                //}
                //this.dgvSpecification.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
                this.dgvBox.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            }
        }
        //获取所有纸箱的信息
        private DataTable GetCarton()
        {
            string sqlSelect = @"Select Length as 长度,Width as 宽度,Height as 高度,Texture as 材质,ProcessRequirement as 处理工艺,Price AS 价格,VendorNumber AS 供应商码 ,VendorName AS 供应商名 From PurchaseDepartmentForeignOrderPackageBoxByCMF ";
            return SQLHelper.GetDataTable(GlobalSpace.FSDBConnstr, sqlSelect);
        }
        //获取所有标签的信息
        private DataTable GetLabel()
        {
            string sqlSelect = @"Select  VendorNumber AS 供应商码 ,VendorName AS 供应商名,Length as 长度,Width as 宽度,CommonPIPrice as 普通签有尺寸价格,CommonAreaPriceAboveBaseQuantityPriceWithSize AS 普通签有尺寸超过基准数量面积价格,CoveredPIPrice AS 覆膜签有尺寸价格,TransparentAreaPrice 透明签面积价格,ScrappedAreaPrice AS 易撕签面积价格  From PurchaseDepartmentForeignOrderPackageLabelSizeByCMF ";
            return SQLHelper.GetDataTable(GlobalSpace.FSDBConnstr, sqlSelect);
        }
        private bool isNumber(string str)
        {
            return true;
        }
        private void btnSpecCreate_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrWhiteSpace(tbSpecComplexPrice.Text) || string.IsNullOrWhiteSpace(tbSpecSinglePrice.Text)||string.IsNullOrWhiteSpace(tbSpecVendorName.Text)||string.IsNullOrWhiteSpace(tbSpecVendorNumber.Text))
            {
                MessageBoxEx.Show("内容不能为空！","提示");
                return;
            }
            string sqlInsert = @"INSERT INTO [FSDB].[dbo].[PurchaseDepartmentForeignOrderPackageSpecificationByCMF] (
	                                            [VendorNumber],
	                                            [VendorName],
	                                            [SingleColorPrice],
	                                            [ComplexColorPrice],
	                                            [Operator]
                                            )
                                            VALUES
	                                            ( '"+tbSpecVendorNumber.Text+"',  '"+tbSpecVendorName.Text+"','"+tbSpecSinglePrice.Text+"','"+tbSpecComplexPrice
                                                .Text+"', '"+UserID+"' )";
            if(SQLHelper.ExecuteNonQuery(GlobalSpace.FSDBConnstr,sqlInsert))
            {
                MessageBoxEx.Show("增加成功！", "提示");
                GetSpec();
            }
            else
            {
                MessageBoxEx.Show("增加失败！", "提示");
            }
        }

        private void btnSpecDelete_Click(object sender, EventArgs e)
        {
            List<string> idList = new List<string>();
            foreach(DataGridViewRow dgvr in dgvSpecification.Rows)
            {
                if(Convert.ToBoolean(dgvr.Cells["SpecCheck"].Value))
                {
                    idList.Add(dgvr.Cells["Id"].Value.ToString());
                }
            }

            string sqlUpdate = @"Delete From PurchaseDepartmentForeignOrderPackageSpecificationByCMF Where Id IN ('{0}')";
            sqlUpdate = string.Format(sqlUpdate, string.Join("','",idList.ToArray()));
            if(SQLHelper.ExecuteNonQuery(GlobalSpace.FSDBConnstr,sqlUpdate))            
                {
                    MessageBoxEx.Show("删除成功！", "提示");
                    GetSpec();
                }
                else
                {
                    MessageBoxEx.Show("删除失败！", "提示");
                }
        }

        private void btnBoxAdd_Click(object sender, EventArgs e)
        {
            //if (string.IsNullOrWhiteSpace(tbBoxVendorID.Text) || string.IsNullOrWhiteSpace(tbBoxName.Text) || string.IsNullOrWhiteSpace(tbBoxProcess.Text) || string.IsNullOrWhiteSpace(tbBoxTexture.Text)|| string.IsNullOrWhiteSpace(tbBoxLength.Text) || string.IsNullOrWhiteSpace(tbBoxWidth.Text) || string.IsNullOrWhiteSpace(tbBoxHeight.Text) || string.IsNullOrWhiteSpace(tbBoxPrice.Text))
                if (string.IsNullOrWhiteSpace(tbBoxVendorID.Text) || string.IsNullOrWhiteSpace(tbBoxName.Text))
            {
                MessageBoxEx.Show("供应商编码、供应商名称不能为空！", "提示");
                return;
            }
            string sqlInsert = @"INSERT INTO [dbo].[PurchaseDepartmentForeignOrderPackageBoxByCMF] ([VendorNumber], [VendorName],[ProcessRequirement], [Texture], [Length], [Width], [Height], [Price], [Operator] )
VALUES
	( '"+ tbBoxVendorID.Text.Trim()+ "', '"+ tbBoxName.Text.Trim() + "',  '"+ tbBoxProcess.Text.Trim() + "', '"+ tbBoxTexture.Text.Trim() + "', "+(string.IsNullOrWhiteSpace(tbBoxLength.Text)?"null":tbBoxLength.Text.Trim()) + ", "+ (string.IsNullOrWhiteSpace(tbBoxWidth.Text) ? "null" : tbBoxWidth.Text.Trim()) + ","+ (string.IsNullOrWhiteSpace(tbBoxHeight.Text) ? "null" : tbBoxHeight.Text.Trim())+ ","+ (string.IsNullOrWhiteSpace(tbBoxPrice.Text) ? "null" : tbBoxPrice.Text.Trim()) + ", '" + UserID + "' )";
            if (SQLHelper.ExecuteNonQuery(GlobalSpace.FSDBConnstr, sqlInsert))
            {
                MessageBoxEx.Show("增加成功！", "提示");
                GetBox();
            }
            else
            {
                MessageBoxEx.Show("增加失败！", "提示");
            }
        }

        private void btnBoxDelete_Click(object sender, EventArgs e)
        {
            List<string> idList = new List<string>();
            foreach (DataGridViewRow dgvr in dgvBox.Rows)
            {
                if (Convert.ToBoolean(dgvr.Cells["BoxCheck"].Value))
                {
                    idList.Add(dgvr.Cells["Id"].Value.ToString());
                }
            }

            string sqlUpdate = @"Delete From PurchaseDepartmentForeignOrderPackageBoxByCMF Where Id IN ('{0}')";
            sqlUpdate = string.Format(sqlUpdate, string.Join("','", idList.ToArray()));
            if (SQLHelper.ExecuteNonQuery(GlobalSpace.FSDBConnstr, sqlUpdate))
            {
                MessageBoxEx.Show("删除成功！", "提示");
                GetBox();
            }
            else
            {
                MessageBoxEx.Show("删除失败！", "提示");
            }
        }

        private void dgvBox_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvBox.Columns[e.ColumnIndex].Name == "BoxCheck")
            {
                dgvBox["BoxCheck", e.RowIndex].Value = !Convert.ToBoolean(dgvBox["BoxCheck", e.RowIndex].Value);
            }
        }
        //SpecCheck
        private void dgvSpecification_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvSpecification.Columns[e.ColumnIndex].Name == "SpecCheck")
            {
                dgvSpecification["SpecCheck", e.RowIndex].Value = !Convert.ToBoolean(dgvSpecification["SpecCheck", e.RowIndex].Value);
            }
        }
    }
}
