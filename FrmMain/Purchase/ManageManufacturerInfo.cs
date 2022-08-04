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
using System.Data.SqlClient;

namespace Global.Purchase
{
    public partial class ManageManufacturerInfo : Office2007Form
    {
        string userID = string.Empty;
        string userName = string.Empty;
        public ManageManufacturerInfo(string id,string name)
        {
            this.EnableGlass = false;
            MessageBoxEx.EnableGlass = false;
            userID = id;
            userName = name;
            InitializeComponent();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string sqlSelect= @"SELECT
                                Id,
                                ItemNumber AS 物料代码,
	                            VendorNumber AS 供应商代码,
	                            VendorName AS 供应商,
	                            ManufacturerNumber AS 生产商代码,
	                            ManufacturerName AS 生产商,
                                Operator as 操作人
                            FROM
                                ItemManufacturerInfoByCMF
                            WHERE
                                ItemNumber like '%" + tbItemNumber.Text.ToString() + "%' and Status = 0";
            dgvManufacturerInfo.DataSource = SQLHelper.GetDataTable(GlobalSpace.FSDBConnstr, sqlSelect);
            dgvManufacturerInfo.Columns["Id"].Visible = false;

            tbVendorName.Text = "";
            tbVendorNumber.Text = "";
            tbManufacturerName.Text = "";
            tbManufacturerNumber.Text = "";
        }

        private void tbItemNumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                btnSearch_Click(sender, e);
            }
        }

        private void tbVendorNumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                if(tbVendorNumber.Text != "" )
                {
                    string sqlSelect = @"SELECT
                            Id,
	                        ItemNumber AS 物料代码,
	                        VendorNumber AS 供应商代码,
	                        VendorName AS 供应商,
	                        ManufacturerNumber AS 生产商代码,
	                        ManufacturerName AS 生产商
                        FROM
	                        ItemManufacturerInfoByCMF Where VendorNumber = '" + tbVendorNumber.Text.Trim()+"' and Status = 0";
                    dgvManufacturerInfo.DataSource = SQLHelper.GetDataTable(GlobalSpace.FSDBConnstr, sqlSelect);
                    dgvManufacturerInfo.Columns["Id"].Visible = false;
                    tbItemNumber.Text = "";
                    tbVendorName.Text = "";
                    tbManufacturerName.Text = "";
                    tbManufacturerNumber.Text = "";
                }
                else
                {
                    MessageBoxEx.Show("供应商代码不能为空！", "提示");
                }                
            }
        }


        private void dgvManufacturerInfo_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                MessageBox.Show("请选择有效的行！", "提示");
            }
            else
            {
                tbItemNumber.Text = dgvManufacturerInfo["物料代码", e.RowIndex].Value.ToString();
                tbItemNumber.Tag = dgvManufacturerInfo["Id", e.RowIndex].Value.ToString();
                tbVendorNumber.Text = dgvManufacturerInfo["供应商代码", e.RowIndex].Value.ToString();
                tbVendorName.Text = dgvManufacturerInfo["供应商", e.RowIndex].Value.ToString();
                tbManufacturerNumber.Text = dgvManufacturerInfo["生产商代码", e.RowIndex].Value.ToString();
                tbManufacturerName.Text = dgvManufacturerInfo["生产商", e.RowIndex].Value.ToString();
            }
        }

        private void tbItemNumber_TextChanged(object sender, EventArgs e)
        {
            tbItemNumber.Text = tbItemNumber.Text.ToUpper();
            tbItemNumber.SelectionStart = tbItemNumber.Text.Length;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (tbItemNumber.Text.Trim() == "" || tbVendorName.Text.Trim() == "" || tbVendorNumber.Text.Trim() == "" || tbManufacturerName.Text.Trim() == ""|| tbManufacturerNumber.Text.Trim() == "")
            {
                MessageBoxEx.Show("不能有空项！","提示");
            }
            else
            {
                string sqlCheck = @"Select count(Id) from ItemManufacturerInfoByCMF Where ItemNumber='"+tbItemNumber.Text.Trim()+"' and VendorNumber='"+tbVendorNumber.Text.Trim()+"' and ManufacturerNumber='"+tbManufacturerNumber.Text.Trim()+ "' and Status=0";
                if(SQLHelper.Exist(GlobalSpace.FSDBConnstr, sqlCheck))
                {
                    MessageBoxEx.Show("该物料的生产商和供应商信息已存在，请先查找确认！", "提示");
                }
                else
                {
                    string sqlInsert = string.Empty;
                    string mNumber = string.Empty;
                   
                        mNumber = tbManufacturerNumber.Text.Trim().ToUpper();
                     sqlInsert = @"INSERT INTO ItemManufacturerInfoByCMF (
	                            VendorNumber,
	                            VendorName,
	                            ManufacturerNumber,
	                            ManufacturerName,
	                            ItemNumber,
                                Operator
                          )
                            VALUES
	                            (	                            @VendorNumber ,@VendorName ,@ManufacturerNumber ,@ManufacturerName ,@ItemNumber,
                                    @Operator
	                            )";
                    SqlParameter[] sqlparams =
                    {
                        new SqlParameter("@VendorNumber",tbVendorNumber.Text.Trim().ToUpper()),
                        new SqlParameter("@VendorName",tbVendorName.Text.ToString().Trim()),
                        new SqlParameter("@ManufacturerNumber",mNumber),
                        new SqlParameter("@ManufacturerName",tbManufacturerName.Text.ToString()),
                        new SqlParameter("@ItemNumber",tbItemNumber.Text.Trim()),
                        new SqlParameter("@Operator",userID)

                    };

                    if (SQLHelper.ExecuteNonQuery(GlobalSpace.FSDBConnstr, sqlInsert, sqlparams))
                    {
                        MessageBoxEx.Show("信息添加成功！", "提示");
                        

                        string sqlSelect = @"SELECT
                            Id,
	                        ItemNumber AS 物料代码,
	                        VendorNumber AS 供应商代码,
	                        VendorName AS 供应商,
	                        ManufacturerNumber AS 生产商代码,
	                        ManufacturerName AS 生产商
                        FROM
	                        ItemManufacturerInfoByCMF Where VendorNumber = '" + tbVendorNumber.Text.Trim().ToUpper() + "' and Status=0 Order by Id Desc";
                        dgvManufacturerInfo.DataSource = SQLHelper.GetDataTable(GlobalSpace.FSDBConnstr, sqlSelect);
                        dgvManufacturerInfo.Columns["Id"].Visible = false;
                        tbItemNumber.Text = "";
                        tbVendorName.Text = "";
                        tbVendorNumber.Text = "";
                        tbManufacturerName.Text = "";
                        tbManufacturerNumber.Text = "";


                    }
                    else
                    {
                        MessageBoxEx.Show("信息添加失败！", "提示");
                    }
                }

            }
        }

        private string GenerateManufacturerNumber()
        {
            string sqlSelect = @"SELECT MAX(ManufacturerNumber) AS MaxNumber FROM ItemManufacturerInfoByCMF ";
            return (Convert.ToInt32(SQLHelper.ExecuteScalar(GlobalSpace.FSDBConnstr, sqlSelect)) + 1).ToString();            
        }
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (tbItemNumber.Text == "" || tbVendorName.Text == "" || tbVendorNumber.Text == "" || tbManufacturerName.Text == "" || tbManufacturerNumber.Text == "")
            {
                MessageBoxEx.Show("不能有空项！", "提示");
            }
            else
            {
                string sqlUpdate = @"Update ItemManufacturerInfoByCMF Set ItemNumber='" + tbItemNumber.Text.Trim()+"',VendorNumber='"+tbVendorNumber.Text.Trim()+"',VendorName='"+tbVendorName.Text+"',ManufacturerNumber='"+tbManufacturerNumber.Text.Trim()+"',ManufacturerName='"+tbManufacturerName.Text+"',Operator='"+userID+"' Where Id='"+tbItemNumber.Tag+"'";
                if(SQLHelper.ExecuteNonQuery(GlobalSpace.FSDBConnstr, sqlUpdate) )
                {
                    MessageBoxEx.Show("修改成功！", "提示");
                    tbItemNumber.Text = "";
                    tbVendorName.Text = "";
                    tbVendorNumber.Text = "";
                    tbManufacturerName.Text = "";
                    tbManufacturerNumber.Text = "";
                }
                else
                {
                    MessageBoxEx.Show("修改失败！", "提示");
                }
            }
        }

        private void ManageManufacturerInfo_Load(object sender, EventArgs e)
        {
            //判断是否是生产商编码管理员
            string strSql1 = @"SELECT
	                                    Manufacturer
                                    FROM
	                                    PurchaseDepartmentRBACByCMF	                                    
                                    WHERE
	                                    UserID='"+userID+"'";
            DataTable dt = SQLHelper.GetDataTable(GlobalSpace.FSDBConnstr, strSql1);
            if (dt.Rows.Count == 1)
            {
                if (dt.Rows[0]["Manufacturer"] != DBNull.Value)
                    if (Convert.ToBoolean(dt.Rows[0]["Manufacturer"]))
                        return;
            }
            tabPage3.Parent = null;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvManufacturerInfo.SelectedRows.Count == 1)
            {
                string id = dgvManufacturerInfo.SelectedRows[0].Cells["Id"].Value.ToString();
                //string sqlDelete = @"Delete From ItemManufacturerInfoByCMF Where Id = '" + id + "'";
                string sqlDelete = @"Update ItemManufacturerInfoByCMF set Status=1,DeleteOperator='"+userID+ "',DelDateTime=getdate() Where Id = '" + id + "'";
                if (SQLHelper.ExecuteNonQuery(GlobalSpace.FSDBConnstr, sqlDelete))
                {
                    Custom.MsgEx("删除成功！");
                }
                else
                {
                    Custom.MsgEx("删除失败！");
                }
            }
            else
            { Custom.MsgEx("请先选中一行！"); }
        }
        //字符集，此处用于供应商名字模糊查询供应商时使用
        Encoding GB2312 = Encoding.GetEncoding("gb2312");
        Encoding ISO88591 = Encoding.GetEncoding("iso-8859-1");
        private void ADD_Click(object sender, EventArgs e)
        {
            tbManufacturerNum_M.Text = tbManufacturerNum_M.Text.Trim().ToUpper();
            tbManufacturerName_M.Text = tbManufacturerName_M.Text.Trim();
            if(string.IsNullOrWhiteSpace(tbManufacturerNum_M.Text)|| string.IsNullOrWhiteSpace(tbManufacturerName_M.Text))
            { MessageBox.Show("请输入生产商编码及名称");return; }
            //查询四班编码名称是否已占用
            string strSql = @"SELECT
	                                    VendorID as 供应商码,
	                                    VendorName as 供应商名
                                    FROM
	                                    _NoLock_FS_Vendor	                                    
                                    WHERE
	                                    VendorID = '" + tbManufacturerNum_M.Text + "'";
            if ((SQLHelper.GetDataTableOleDb(GlobalSpace.oledbconnstrFSDBMR, strSql)).Rows.Count == 1)
            {
                MessageBox.Show("四班编码已存在！"); return;
            }
            strSql = @"SELECT
	                                    VendorID as 供应商码,
	                                    VendorName as 供应商名
                                    FROM
	                                    _NoLock_FS_Vendor	                                    
                                    WHERE
	                                    VendorName = '" + ISO88591.GetString(GB2312.GetBytes(tbManufacturerName_M.Text)) + "' and VendorStatus='A'";
            if ((SQLHelper.GetDataTableOleDb(GlobalSpace.oledbconnstrFSDBMR, strSql)).Rows.Count >0)
            {
                MessageBox.Show("四班供应商名称已存在！"); return;
            }
            //查询生产商编码是否已存在
            string strSql1 = @"SELECT
	                                    *
                                    FROM
	                                    ManufacturerNumberName	                                    
                                    WHERE
	                                    ManufacturerNumber = '" + tbManufacturerNum_M.Text + "'";
            if ((SQLHelper.GetDataTable(GlobalSpace.FSDBConnstr, strSql1)).Rows.Count == 1)
            {
                MessageBox.Show("生产商编码已存在！"); return;
            }
            strSql1 = @"SELECT
	                                    *
                                    FROM
	                                    ManufacturerNumberName	                                    
                                    WHERE
	                                    ManufacturerName = '" + tbManufacturerName_M.Text + "'";
            if ((SQLHelper.GetDataTable(GlobalSpace.FSDBConnstr, strSql1)).Rows.Count >0)
            {
                MessageBox.Show("生产商名称已存在！"); return;
            }
            //Insert
            string sqlInsert = @"INSERT INTO ManufacturerNumberName (
	                            ManufacturerNumber,
	                            ManufacturerName,
                                Operator
                          )
                            VALUES
	                            (@ManufacturerNumber ,@ManufacturerName ,@Operator
	                            )";
            SqlParameter[] sqlparams =
            {
                        new SqlParameter("@ManufacturerNumber",tbManufacturerNum_M.Text),
                        new SqlParameter("@ManufacturerName",tbManufacturerName_M.Text),
                        new SqlParameter("@Operator",userID)

                    };

            if (SQLHelper.ExecuteNonQuery(GlobalSpace.FSDBConnstr, sqlInsert, sqlparams))
            {
                MessageBoxEx.Show("生产商添加成功！", "提示");
                All_Click(null, null);
            }
            else 
            {
                MessageBoxEx.Show("生产商添加失败！", "提示");
            }

            }

        private void All_Click(object sender, EventArgs e)
        {
            string sqlSelect = @"SELECT
                            
	                        ManufacturerNumber AS 生产商码,
	                        ManufacturerName AS 生产商名
                        FROM
	                        ManufacturerNumberName Where Status=1 order by ManufacturerNumber";
            DgvManufacturer.DataSource = SQLHelper.GetDataTable(GlobalSpace.FSDBConnstr, sqlSelect);
        }

        private void DgvManufacturer_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int RowIndex = e.RowIndex;
            if (RowIndex < 0) return;
            tbManufacturerNum_M.Text = DgvManufacturer["生产商码", RowIndex].Value.ToString();
            tbManufacturerName_M.Text = DgvManufacturer["生产商名", RowIndex].Value.ToString();
        }

        private void Update_Click(object sender, EventArgs e)
        {
            tbManufacturerNum_M.Text = tbManufacturerNum_M.Text.Trim().ToUpper();
            tbManufacturerName_M.Text = tbManufacturerName_M.Text.Trim();
            if (string.IsNullOrWhiteSpace(tbManufacturerNum_M.Text) || string.IsNullOrWhiteSpace(tbManufacturerName_M.Text))
            { MessageBox.Show("请输入生产商编码及名称"); return; }
            //查询四班名称是否已占用
            string  strSql = @"SELECT
	                                    VendorID as 供应商码,
	                                    VendorName as 供应商名
                                    FROM
	                                    _NoLock_FS_Vendor	                                    
                                    WHERE
	                                    VendorName = '" + ISO88591.GetString(GB2312.GetBytes(tbManufacturerName_M.Text)) + "' and VendorStatus='A'";
            if ((SQLHelper.GetDataTableOleDb(GlobalSpace.oledbconnstrFSDBMR, strSql)).Rows.Count > 0)
            {
                MessageBox.Show("四班供应商名称已存在！"); return;
            }
            //查询生产商名称是否已存在
            string  strSql1 = @"SELECT
	                                    *
                                    FROM
	                                    ManufacturerNumberName	                                    
                                    WHERE
	                                    ManufacturerName = '" + tbManufacturerName_M.Text + "'";
            if ((SQLHelper.GetDataTable(GlobalSpace.FSDBConnstr, strSql1)).Rows.Count > 0)
            {
                MessageBox.Show("生产商名称已存在！"); return;
            }
            //update
            string sqlInsert = @"update ManufacturerNumberName set ManufacturerName='"+ tbManufacturerName_M.Text + "',  UpdateHistory=CONCAT(CONCAT(UpdateHistory,CONCAT(ManufacturerName,CONVERT(varchar(100), GETDATE(), 20))),'" + userID + ";'), Operator='" + userID+ "' where ManufacturerNumber='"+ tbManufacturerNum_M.Text + "' and Status=1"; 
            if (SQLHelper.ExecuteNonQuery(GlobalSpace.FSDBConnstr, sqlInsert))
            {
                MessageBoxEx.Show("生产商修改成功！", "提示");
                All_Click(null, null);
                //修改物料生产商管理中的生产商名称
                SQLHelper.ExecuteNonQuery(GlobalSpace.FSDBConnstr, "update ItemManufacturerInfoByCMF set ManufacturerName='" + tbManufacturerName_M.Text + "',  UpdateHistory=CONCAT(CONCAT(UpdateHistory,CONCAT(ManufacturerName,CONVERT(varchar(100), GETDATE(), 20))),'" + userID + ";')  where ManufacturerNumber='" + tbManufacturerNum_M.Text + "'");
            }
            else
            {
                MessageBoxEx.Show("生产商修改失败！", "提示");
            }
        }

        private void ForthShift_Click(object sender, EventArgs e)
        {
            tbManufacturerNum_M.Text = tbManufacturerNum_M.Text.Trim().ToUpper();
            tbManufacturerName_M.Text = tbManufacturerName_M.Text.Trim();
            if (string.IsNullOrWhiteSpace(tbManufacturerNum_M.Text) || string.IsNullOrWhiteSpace(tbManufacturerName_M.Text))
            { MessageBox.Show("请输入生产商编码及名称"); return; }
            //查询四班编码是否已存在
            string strSql = @"SELECT
	                                    VendorID as 供应商码,
	                                    VendorName as 供应商名
                                    FROM
	                                    _NoLock_FS_Vendor	                                    
                                    WHERE
	                                    VendorID = '" + tbManufacturerNum_M.Text + "'";
            if ((SQLHelper.GetDataTableOleDb(GlobalSpace.oledbconnstrFSDBMR, strSql)).Rows.Count == 1)
            {
                //MessageBox.Show("四班编码已存在！"); 
            }
            else 
            {
                MessageBox.Show("四班编码不存在！"); return;
            }
            //update
            string sqlInsert = @"update ManufacturerNumberName set Status=0,  UpdateHistory=CONCAT(CONCAT(UpdateHistory,CONCAT('已录四班',CONVERT(varchar(100), GETDATE(), 20))),'" + userID + ";'), Operator='" + userID + "' where ManufacturerNumber='" + tbManufacturerNum_M.Text + "' and Status=1";
            if (SQLHelper.ExecuteNonQuery(GlobalSpace.FSDBConnstr, sqlInsert))
            {
                MessageBoxEx.Show("生产商已录四班操作成功！", "提示");
                All_Click(null, null);
            }
            else
            {
                MessageBoxEx.Show("生产商已录四班操作失败！", "提示");
            }
        }

        private void SoftDelete_Click(object sender, EventArgs e)
        {
            tbManufacturerNum_M.Text = tbManufacturerNum_M.Text.Trim().ToUpper();
            tbManufacturerName_M.Text = tbManufacturerName_M.Text.Trim();
            if (string.IsNullOrWhiteSpace(tbManufacturerNum_M.Text) || string.IsNullOrWhiteSpace(tbManufacturerName_M.Text))
            { MessageBox.Show("请输入生产商编码及名称"); return; }
            //查询物料编码是否已占用该生产商
            string sqlSelect = @"SELECT
                                Id,
                                ItemNumber AS 物料代码,
	                            VendorNumber AS 供应商代码,
	                            VendorName AS 供应商,
	                            ManufacturerNumber AS 生产商代码,
	                            ManufacturerName AS 生产商,
                                Operator as 操作人
                            FROM
                                ItemManufacturerInfoByCMF
                            WHERE
                                ManufacturerNumber = '" + tbManufacturerNum_M.Text + "'";
            if (SQLHelper.GetDataTable(GlobalSpace.FSDBConnstr, sqlSelect).Rows.Count > 0)
            {
                MessageBox.Show("物料生产商管理中已占用该生厂商，无法停用！"); return;
            }
            //update
            string sqlInsert = @"delete from ManufacturerNumberName where ManufacturerNumber='" + tbManufacturerNum_M.Text + "'";
            if (SQLHelper.ExecuteNonQuery(GlobalSpace.FSDBConnstr, sqlInsert))
            {
                MessageBoxEx.Show("生产商删除成功！", "提示");
                All_Click(null, null);
            }
            else
            {
                MessageBoxEx.Show("生产商删除失败！", "提示");
            }
        }

        private void TbCode_KeyPress(object sender, KeyPressEventArgs e)
        {
            string sqlSelect = @"SELECT
                            
	                        ManufacturerNumber AS 生产商码,
	                        ManufacturerName AS 生产商名
                        FROM
	                        ManufacturerNumberName Where ManufacturerNumber='"+ TbCode.Text.Trim().ToUpper()+ "' and Status=1 order by ManufacturerNumber";
            DgvManufacturerSelect.DataSource = SQLHelper.GetDataTable(GlobalSpace.FSDBConnstr, sqlSelect);
        }

        private void TbName_KeyPress(object sender, KeyPressEventArgs e)
        {
            string sqlSelect = @"SELECT
                            
	                        ManufacturerNumber AS 生产商码,
	                        ManufacturerName AS 生产商名
                        FROM
	                        ManufacturerNumberName Where ManufacturerName like '%" + TbName.Text.Trim().ToUpper() + "%' and Status=1 order by ManufacturerNumber";
            DgvManufacturerSelect.DataSource = SQLHelper.GetDataTable(GlobalSpace.FSDBConnstr, sqlSelect);
        }

        private void DgvManufacturerSelect_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int RowIndex = e.RowIndex;
            if (RowIndex < 0) return;
            tbManufacturerNumber.Text = DgvManufacturerSelect["生产商码", RowIndex].Value.ToString();
            tbManufacturerName.Text = DgvManufacturerSelect["生产商名", RowIndex].Value.ToString();
            tabControl1.SelectedTab=tabPage1;
        }
    }
}
