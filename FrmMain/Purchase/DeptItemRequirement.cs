using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using Global;
using Global.Helper;
using Global.Properties;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;

namespace Global.Purchase
{
    public partial class DeptItemRequirement : Office2007Form
    {
        //字符集，此处用于供应商名字模糊查询供应商时使用
        Encoding GB2312 = Encoding.GetEncoding("gb2312");
        Encoding ISO88591 = Encoding.GetEncoding("iso-8859-1");
        public DeptItemRequirement()
        {
            InitializeComponent();
            this.EnableGlass = false;
            MessageBoxEx.EnableGlass = false;
        }

        private void btnRefreshPO_Click(object sender, EventArgs e)
        {
            GetRequireItem();
        }

        private void GetRequireItem()
        {
            string sqlSelect = @"SELECT
	                                                    ID,OperateTime AS 提报日期,rtrim(ltrim(WorkCenter)) AS 需求车间,
	                                                    rtrim(ltrim(ItemNumber)) AS 物料代码,
	                                                    rtrim(ltrim(ItemDescription)) AS 物料描述,
	                                                    rtrim(ltrim(ItemUM)) AS 单位,
	                                                    BuyQuantity AS 需求数量,
	                                                    rtrim(ltrim(InternationalStandards)) AS 检验标准,
                                                        rtrim(ltrim(ForeignNumber)) AS 联系单号,
	                                                    NeedTime AS 需求日期,
	                                                    rtrim(ltrim(Remark)) AS 备注,
	                                                    rtrim(ltrim(VendorName)) AS 指定供应商,
                                                        case when  SYBFlag=0 then '固水'  when  SYBFlag=1 then '粉针' when  SYBFlag=2 then '原料' else '其他' end  AS 事业部
                                                    FROM
	                                                    dbo.SolidBuyList 
                                                    WHERE
	                                                    Flag = 0 order by ID";
            dgvItemRequirement.DataSource = SQLHelper.GetDataTable(GlobalSpace.RYData, sqlSelect);
            dgvItemRequirement.Columns["ID"].Visible = false;
            for (int i = 0; i < this.dgvItemRequirement.Columns.Count; i++)
            {
                //this.dgvSpecification.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
                dgvItemRequirement.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            }
        }
        private void GetRequireItem1()
        {
            string str = string.Empty;
            if (rbtnDay.Checked)
            {

            }
            else
            {

            }
            string sqlSelect = @"SELECT
	                                                    ID,OperateTime AS 提报日期,rtrim(ltrim(WorkCenter)) AS 需求车间,
	                                                    rtrim(ltrim(ItemNumber)) AS 物料代码,
	                                                    rtrim(ltrim(ItemDescription)) AS 物料描述,
	                                                    rtrim(ltrim(ItemUM)) AS 单位,
	                                                    BuyQuantity AS 需求数量,
	                                                    rtrim(ltrim(InternationalStandards)) AS 检验标准,
	                                                    NeedTime AS 需求日期,
	                                                    rtrim(ltrim(Remark)) AS 备注,
	                                                    rtrim(ltrim(VendorName)) AS 指定供应商,
                                                        Flag   AS 状态
                                                    FROM
	                                                    dbo.SolidBuyList 
                                                    WHERE
	                                                    Flag in (1,2) order by ID";
            dgvEdit.DataSource = SQLHelper.GetDataTable(GlobalSpace.RYData, sqlSelect);
            dgvEdit.Columns["ID"].Visible = false;
            for (int i = 0; i < this.dgvEdit.Columns.Count; i++)
            {
                //this.dgvSpecification.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
                dgvEdit.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            }
        }
        private void tbVendorName_KeyPress(object sender, KeyPressEventArgs e)
        {
            string str = string.Empty;
            str = ISO88591.GetString(GB2312.GetBytes(tbVendorName.Text.ToString()));
            cbbVendorNumber.Items.Clear();
            if (e.KeyChar == (char)13)
            {
                if (string.IsNullOrEmpty(tbVendorName.Text.Trim()))
                {
                    MessageBoxEx.Show("查询内容不得为空！");
                }
                else
                {
                    cbbVendorNumber.Text = "";
                    if (cbbVendorNumber.Items.Count > 0)
                    {
                        cbbVendorNumber.Items.Clear();
                    }
                    string strSql = @"SELECT
	                                    VendorID,
	                                    VendorName
                                    FROM
	                                    _NoLock_FS_Vendor	                                    
                                    WHERE
	                                    VendorName like '%" + str + "%'";
                    DataTable dtTemp = SQLHelper.GetDataTableOleDb(GlobalSpace.oledbconnstrFSDBMR, strSql);
                    if (dtTemp.Rows.Count > 0)
                    {
                        if (dtTemp.Rows.Count == 1)
                        {
                            cbbVendorNumber.Text = dtTemp.Rows[0]["VendorID"].ToString() + "|" + dtTemp.Rows[0]["VendorName"].ToString();
                        }
                        else
                        {
                            cbbVendorNumber.Text = "";
                            foreach (DataRow dr in dtTemp.Rows)
                            {
                                cbbVendorNumber.Items.Add(dr["VendorID"].ToString() + "|" + dr["VendorName"].ToString());
                            }
                        }
                    }
                }
            }
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(cbbVendorNumber.Text) || string.IsNullOrWhiteSpace(tbPricePreTax.Text) || string.IsNullOrWhiteSpace(tbRemark.Text))
            {
                MessageBoxEx.Show("信息不能为空！", "提示");
                return;
            }
            List<string> sqlList = new List<string>();
            foreach (DataGridViewRow dgvr in dgvItemRequirement.Rows)
            {
                if (Convert.ToBoolean(dgvr.Cells["Check"].Value))
                {
                    string sqlInsert = @"INSERT INTO [FSDB].[dbo].[PurchaseDepartmentDeptRequirement] (
	                                                            [ItemNumber],
	                                                            [ItemDescription],
	                                                            [UM],
	                                                            [RequireQuantity],
	                                                            [InspectStandard],
	                                                            [RequireDate],
	                                                            [RemarkOriginal],
	                                                            [AppointedVendor],
	                                                            [VendorNumber],
	                                                            [VendorName],
	                                                            [UniqueID],
	                                                            [Remark],PricePreTax,Creator
                                                            )
                                                            VALUES
	                                                            ('" + dgvr.Cells["物料代码"].Value.ToString() + "','" + dgvr.Cells["物料描述"].Value.ToString() + "',	'" + dgvr.Cells["单位"].Value.ToString() + "','" + Convert.ToDouble(dgvr.Cells["需求数量"].Value) + "','" + dgvr.Cells["检验标准"].Value.ToString() + "','" + dgvr.Cells["需求日期"].Value.ToString() + "','" + dgvr.Cells["备注"].Value.ToString() + "','" + dgvr.Cells["指定供应商"].Value.ToString() + "','" + cbbVendorNumber.Text.Split('|')[0] + "','" + cbbVendorNumber.Text.Split('|')[1] + "','" + Convert.ToInt32(dgvr.Cells["ID"].Value) + "','" + tbRemark.Text + "'," + Convert.ToDouble(tbPricePreTax.Text) + ",'" + PurchaseUser.UserID + "' )";
                    sqlList.Add(sqlInsert);
                }
            }
            if (sqlList.Count == 0)
            {
                MessageBoxEx.Show("请选择一条待处理计划！", "提示");
                return;
            }
            else if (sqlList.Count > 1)
            {
                MessageBoxEx.Show("一次只能处理一条需求计划信息", "提示");
                return;
            }
            else
            {
                if (SQLHelper.BatchExecuteNonQuery(GlobalSpace.FSDBConnstr, sqlList))
                {
                    MessageBoxEx.Show("确认成功！", "提示");
                    GetRequireItem();
                }
                else
                {
                    MessageBoxEx.Show("确认失败！", "提示");
                }
            }
        }

        private void DeptItemRequirement_Load(object sender, EventArgs e)
        {
            GetRequireItem();
            if (PurchaseUser.PurchaseType.Contains("P"))
            {
                DataTable dtConfirmType = GetPOItemConfirmPersonList(PurchaseUser.PurchaseType);
                BindingSource bs = new BindingSource();
                bs.DataSource = dtConfirmType.Rows.Cast<DataRow>().ToDictionary(r => r["UserID"].ToString(), r => r["Name"].ToString());
                cbbConfirmPerson.DataSource = bs;
                cbbConfirmPerson.DisplayMember = "Value";
                cbbConfirmPerson.ValueMember = "Key";
                cbbConfirmPerson.SelectedIndex = -1;
            }
        }
        private DataTable GetPOItemConfirmPersonList(string purchaseType)
        {
            string type = string.Empty;

            if (purchaseType.Contains("A"))
            {
                type = "A";
            }
            if (purchaseType.Contains("P"))
            {
                type = "P";
            }
            string sqlSelect = @"Select UserID,(UserID+'|'+Name) AS Name From PurchaseDepartmentRBACByCMF Where POItemConfirmType = '" + type + "'";
            return SQLHelper.GetDataTable(GlobalSpace.FSDBConnstr, sqlSelect);
        }
        private void BtTableExport_Click(object sender, EventArgs e)
        {
            if (dgvItemRequirement.Rows.Count == 0)
            { MessageBox.Show("无数据！"); return; }
            #region
            if (!CheckCodeUnit(dgvItemRequirement))
            {
                MessageBoxEx.Show("物料代码或单位不准确，已红色标示！");
                return;
            }
            #endregion
            string filePath = getExcelpath();
            if (filePath.IndexOf(":") < 0)
            { return; }
            TableToExcel(dgvItemRequirement, filePath);
            MessageBox.Show("导出完成");
            SetFlag1(dgvItemRequirement);
            GetRequireItem();
        }

        private bool CheckCodeUnit(DataGridView dgv)
        {
            bool bl = true;
            for (int i = 0; i < dgv.Rows.Count; i++)
            {
                if (Convert.ToBoolean(dgv.Rows[i].Cells["Check"].Value))
                {
                    string sqlSelect = @"Select  ItemDescription,ItemUM,IsInspectionRequired,PreferredStockroom,PreferredBin,IsLotTraced From _NoLock_FS_Item Where ItemNumber='" + dgv["物料代码", i].Value.ToString() + "'";
                    DataTable dtTemp = SQLHelper.GetDataTableOleDb(GlobalSpace.oledbconnstrFSDBMR, sqlSelect);
                    if (dtTemp.Rows.Count == 1)
                    {
                        if (dgv["单位", i].Value.ToString() != dtTemp.Rows[0]["ItemUM"].ToString())
                        {
                            dgv.Rows[i].DefaultCellStyle.ForeColor = Color.Red;
                            bl = false;
                        }
                    }
                    else
                    {
                        dgv.Rows[i].DefaultCellStyle.ForeColor = Color.Red;
                        bl = false;
                    }
                    if (string.IsNullOrWhiteSpace(dgv["需求数量", i].Value.ToString())||Convert.ToDecimal(dgv["需求数量", i].Value.ToString())<=0)
                    {
                        dgv.Rows[i].DefaultCellStyle.ForeColor = Color.Red;
                        bl = false;
                    }
                }
            }
            return bl;
        }

        private void SetFlag1(DataGridView dt)
        {
            List<int> lint = new List<int>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (Convert.ToBoolean(dt.Rows[i].Cells["Check"].Value))
                {
                    lint.Add(Convert.ToInt32(dt.Rows[i].Cells["ID"].Value));
                }
            }
            string UpdateFlag = "update  [dbo].[SolidBuyList] set Flag=1,ExtractTime=GETDATE()  where ID in (" + string.Join(",", lint.ToArray()) + ")";
            if (SQLHelper.ExecuteNonQuery(GlobalSpace.RYData, UpdateFlag))
            { }
            else
            {
                MessageBox.Show("状态修改失败!");
            }
        }

        private static string getExcelpath()
        {
            SaveFileDialog saveDialog = new SaveFileDialog();
            saveDialog.DefaultExt = "xlsx";
            saveDialog.Filter = "EXCEL表格|*.xlsx";
            //saveDialog.FileName = "条形码";
            saveDialog.ShowDialog();
            return saveDialog.FileName;
        }
        public static void TableToExcel(DataGridView dt, string file)
        {
            //AppDomain.CurrentDomain.AssemblyResolve += CurrentDomain_AssemblyResolve;
            IWorkbook workbook;
            string fileExt = Path.GetExtension(file).ToLower();
            if (fileExt == ".xlsx")
            { workbook = new XSSFWorkbook(); }
            else if (fileExt == ".xls")
            { workbook = new HSSFWorkbook(); }
            else { workbook = null; }
            if (workbook == null) { return; }
            ISheet sheet = string.IsNullOrEmpty(dt.Name) ? workbook.CreateSheet("Sheet1") : workbook.CreateSheet(dt.Name);

            //表头  
            IRow row = sheet.CreateRow(0);
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                ICell cell = row.CreateCell(i);
                cell.SetCellValue(dt.Columns[i].HeaderText);
            }

            //数据  
            for (int i = 0, x = 0; i < dt.Rows.Count; i++, x++)
            {
                if (Convert.ToBoolean(dt.Rows[i].Cells["Check"].Value))
                {
                    IRow row1 = sheet.CreateRow(x + 1);
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        ICell cell = row1.CreateCell(j);
                        if (j == 9)
                        {
                            cell.SetCellValue(dt.Rows[i].Cells[j].Value == null ? "" : Convert.ToDateTime(dt.Rows[i].Cells[j].Value.ToString()).ToString("MMddyy"));
                        }
                        else
                        {
                            cell.SetCellValue(dt.Rows[i].Cells[j].Value == null ? "" : dt.Rows[i].Cells[j].Value.ToString());
                        }
                    }
                }
                else { x--; }
            }

            //转为字节数组  
            MemoryStream stream = new MemoryStream();//读写内存的对象
            workbook.Write(stream);
            var buf = stream.ToArray();//字节数组
            //保存为Excel文件  
            using (FileStream fs = new FileStream(file, FileMode.Create, FileAccess.Write))
            {
                fs.Write(buf, 0, buf.Length);
                fs.Flush();//缓冲区在内存中有个临时区域  盆 两个水缸 //缓冲区装满才会自动提交
            }
        }
        bool blAllSelect = true;
        private void BtAllSelect_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow dgvr in dgvItemRequirement.Rows)
            {
                dgvr.Cells["Check"].Value = blAllSelect;
            }
            blAllSelect = !blAllSelect;
        }

        private void btnRefresh1_Click(object sender, EventArgs e)
        {
            GetRequireItem1();
        }

        private void dgvItemRequirement_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                dgvItemRequirement["Check", e.RowIndex].Value = !Convert.ToBoolean(dgvItemRequirement["Check", e.RowIndex].Value);
            }
        }

        private void tabItem3_Click(object sender, EventArgs e)
        {
            btnEditedRefresh_Click(sender, e);
        }


        private void btnExtract_Click(object sender, EventArgs e)
        {
            if (dgvItemRequirement.Rows.Count == 0)
            { MessageBox.Show("无数据！"); return; }
            #region
            if (!CheckCodeUnit(dgvItemRequirement))
            {
                MessageBoxEx.Show("物料代码或单位不准确或需求数量小于等于零，已红色标示！");
                return;
            }
            #endregion

            BatchExtract(dgvItemRequirement);

        }
        private void BatchExtract(DataGridView dt)
        {
            List<int> lint = new List<int>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (Convert.ToBoolean(dt.Rows[i].Cells["Check"].Value))
                {
                    lint.Add(Convert.ToInt32(dt.Rows[i].Cells["ID"].Value));
                }
            }
            if (lint.Count == 0)
            {
                MessageBoxEx.Show("未选中任何行"); return;
            }
            #region 事务批量添加数据
            SqlConnection con = new SqlConnection(GlobalSpace.RYData);
            con.Open();
            SqlTransaction tran = con.BeginTransaction();//先实例SqlTransaction类，使用这个事务使用的是con 这个连接，使用BeginTransaction这个方法来开始执行这个事务
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.Transaction = tran;
            try
            {

                cmd.CommandText = "update  [dbo].[SolidBuyList] set Flag=1,ExtractTime=GETDATE(),FSTITime=GETDATE()  where ID in (" + string.Join(",", lint.ToArray()) + ")";
                cmd.ExecuteNonQuery();
                cmd.CommandText = "INSERT INTO SolidBuyList_Handle ( SolidBuyList_Handle.PlanID, \n" +
"	SolidBuyList_Handle.ItemNumber, \n" +
"	SolidBuyList_Handle.ItemDescription, \n" +
"	SolidBuyList_Handle.ItemUM, \n" +
"	SolidBuyList_Handle.BuyQuantity, \n" +
"	SolidBuyList_Handle.InternationalStandards, \n" +
"	SolidBuyList_Handle.NeedTime, \n" +
"	SolidBuyList_Handle.OperateTime, \n" +
"	SolidBuyList_Handle.CompanyStatus, \n" +
"	SolidBuyList_Handle.PlanVendorName, \n" +
"	SolidBuyList_Handle.FSTITime, \n" +
"	SolidBuyList_Handle.PurChaseNumber, \n" +
"	SolidBuyList_Handle.ReceiveTime, \n" +
"	SolidBuyList_Handle.ReceiveQuantity, \n" +
"	SolidBuyList_Handle.ForeignNumber, \n" +
"	SolidBuyList_Handle.WorkCenter, \n" +
"	SolidBuyList_Handle.PlanRemark, \n" +
"	SolidBuyList_Handle.SYBFlag,TaxRate) SELECT " +
                    "  convert(nvarchar(255),SolidBuyList.ID),\n" +
"	rtrim(ltrim(SolidBuyList.ItemNumber)),\n" +
"	rtrim(ltrim(SolidBuyList.ItemDescription)),\n" +
"	rtrim(ltrim(SolidBuyList.ItemUM)),\n" +
"	SolidBuyList.BuyQuantity,\n" +
"	rtrim(ltrim(SolidBuyList.InternationalStandards)),\n" +
"	SolidBuyList.NeedTime,\n" +
"	SolidBuyList.OperateTime,\n" +
"	rtrim(ltrim(SolidBuyList.CompanyStatus)),\n" +
"	rtrim(ltrim(SolidBuyList.VendorName)),\n" +
"	SolidBuyList.FSTITime,\n" +
"	SolidBuyList.PurChaseNumber,\n" +
"	SolidBuyList.ReceiveTime,\n" +
"	SolidBuyList.ReceiveQuantity,\n" +
"	rtrim(ltrim(SolidBuyList.ForeignNumber)),\n" +
"	rtrim(ltrim(SolidBuyList.WorkCenter)),\n" +
"	rtrim(ltrim(SolidBuyList.Remark)),\n" +
"	SolidBuyList.SYBFlag,0.13 " +
                    " FROM SolidBuyList where ID in (" + string.Join(",", lint.ToArray()) + ")";
                cmd.ExecuteNonQuery();

                tran.Commit();
                MessageBoxEx.Show("提取完成！");
                GetRequireItem();
            }
            catch (Exception ex)
            {
                tran.Rollback();
                MessageBoxEx.Show("提取失败：" + ex.Message);
            }
            tran.Dispose();
            con.Close();
            #endregion
        }

        private void btnExtractRefresh_Click(object sender, EventArgs e)
        {
            string sqlSelect = @"SELECT
	                                                    ID,PlanID AS 提报序号,
	                                                    rtrim(ltrim(ItemNumber)) AS 物料代码,
	                                                    rtrim(ltrim(ItemDescription)) AS 物料描述,
	                                                    rtrim(ltrim(ItemUM)) AS 单位,
	                                                    BuyQuantity AS 需求数量,
VendorNumber AS 供应商码,VendorName AS 供应商名,ManufacturerNumber AS 生产商码,ManufacturerName AS 生产商名,PricePreTax AS 税前价格,TaxRate AS 税率,Confirmer AS 确认员,Remark AS 备注,
	                                                    rtrim(ltrim(InternationalStandards)) AS 检验标准,
                                                        rtrim(ltrim(ForeignNumber)) AS 联系单号,
	                                                    NeedTime AS 需求日期,
	                                                    rtrim(ltrim(PlanVendorName)) AS 计划指定供应商,
	                                                    rtrim(ltrim(PlanRemark)) AS 计划备注,
                                                        case when  SYBFlag=0 then '固水'  when  SYBFlag=1 then '粉针' when  SYBFlag=2 then '原料' else '其他' end  AS 事业部,rtrim(ltrim(WorkCenter)) AS 需求车间,State,OperateTime AS 提报日期
                                                    FROM
	                                                    dbo.SolidBuyList_Handle 
                                                    WHERE
	                                                    Flag = 0 order by rtrim(ltrim(ItemNumber))";
            dgvEdit.DataSource = SQLHelper.GetDataTable(GlobalSpace.RYData, sqlSelect);
            dgvEdit.Columns["ID"].Visible = false;
            for (int i = 0; i < this.dgvEdit.Columns.Count; i++)
            {
                //this.dgvSpecification.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
                dgvEdit.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                if (dgvEdit.Columns[i].Name == "选择" || dgvEdit.Columns[i].Name == "供应商码" || dgvEdit.Columns[i].Name == "供应商名" || dgvEdit.Columns[i].Name == "生产商码" || dgvEdit.Columns[i].Name == "生产商名" || dgvEdit.Columns[i].Name == "税前价格" || dgvEdit.Columns[i].Name == "备注" || dgvEdit.Columns[i].Name == "检验标准" || dgvEdit.Columns[i].Name == "联系单号")
                {
                    dgvEdit.Columns[i].ReadOnly = false;
                }
                else
                {
                    dgvEdit.Columns[i].ReadOnly = true;
                }
            }
        }

        private void tabControlPanel2_Click(object sender, EventArgs e)
        {

        }

        private void btnAll_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dgvEdit.Rows.Count; i++)
            {
                dgvEdit["选择", i].Value = true;
            }
        }

        private void btnAllNot_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dgvEdit.Rows.Count; i++)
            {
                dgvEdit["选择", i].Value = false;
            }
        }

        private void btnHebing_Click(object sender, EventArgs e)
        {
            string ItemCode = string.Empty;
            string PlanID = string.Empty;
            Decimal PlanQuantity = 0;
            List<int> ID = new List<int>();
            for (int i = 0; i < dgvEdit.Rows.Count; i++)
            {
                if (Convert.ToBoolean(dgvEdit["选择", i].Value) == true)
                {

                    if (dgvEdit["State", i].Value.ToString() == "拆分")
                    {
                        MessageBoxEx.Show("第" + (i + 1) + "行已拆分不能合并"); return;
                    }
                    string Item = dgvEdit["提报序号", i].Value.ToString();
                    if (ItemCode == string.Empty)
                    {
                        ItemCode = dgvEdit["物料代码", i].Value.ToString().Trim().ToUpper();
                    }
                    else
                    {
                        if (ItemCode != dgvEdit["物料代码", i].Value.ToString().Trim().ToUpper())
                        {
                            MessageBoxEx.Show("合并的物料编码不同！"); return;
                        }
                    }
                    if (PlanID == string.Empty)
                    {
                        PlanID = Item;
                    }
                    else
                    {
                        PlanID += "|" + Item;
                    }
                    PlanQuantity += Convert.ToDecimal(dgvEdit["需求数量", i].Value.ToString());
                    ID.Add(Convert.ToInt32(dgvEdit["ID", i].Value.ToString()));
                }
            }

            if (PlanID == string.Empty || PlanID.Split('|').Length == 1)
            {
                MessageBoxEx.Show("请至少选择2条信息！"); return;
            }
            #region 事务批量添加数据
            SqlConnection con = new SqlConnection(GlobalSpace.RYData);
            con.Open();
            SqlTransaction tran = con.BeginTransaction();//先实例SqlTransaction类，使用这个事务使用的是con 这个连接，使用BeginTransaction这个方法来开始执行这个事务
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.Transaction = tran;
            try
            {

                cmd.CommandText = "update  SolidBuyList_Handle set Flag=-1 where ID in (" + string.Join(",", ID) + ")";
                cmd.ExecuteNonQuery();
                cmd.CommandText = "update  SolidBuyList_Handle set Flag=0,PlanID='" + PlanID + "',BuyQuantity=" + PlanQuantity + ",State='合并' where ID = " + ID[0];
                cmd.ExecuteNonQuery();

                tran.Commit();
                MessageBoxEx.Show("合并完成！");
                btnExtractRefresh_Click(sender, e);
            }
            catch (Exception ex)
            {
                tran.Rollback();
                MessageBoxEx.Show("合并失败：" + ex.Message);
            }
            tran.Dispose();
            con.Close();
            #endregion
        }

        private void dgvEdit_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex > -1 && e.Button == MouseButtons.Right)
            {
                //MessageBoxEx.Show("右键");
                dgvEdit.ClearSelection();
                dgvEdit.Rows[e.RowIndex].Selected = true;
                contextMenuStrip1.Tag = e.RowIndex;
                contextMenuStrip1.Show(MousePosition.X, MousePosition.Y);

            }
        }

        private void 拆分ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int RowIndex = Convert.ToInt32(contextMenuStrip1.Tag.ToString());
            if (dgvEdit["State", RowIndex].Value.ToString() == "合并")
            {
                MessageBoxEx.Show("已合并不能拆分"); return;
            }
            decimal CfQuantity = 0;
            if (!Decimal.TryParse(tBQuantity.Text, out CfQuantity) || string.IsNullOrWhiteSpace(tBQuantity.Text))
            {
                MessageBoxEx.Show("请在拆分数量框中输入数值！"); return;
            }
            if (CfQuantity <= 0 || CfQuantity >= Convert.ToInt32(dgvEdit["需求数量", RowIndex].Value.ToString()))
            {
                MessageBoxEx.Show("拆分数量输入范围不正确！"); return;
            }
            #region 事务批量添加数据
            SqlConnection con = new SqlConnection(GlobalSpace.RYData);
            con.Open();
            SqlTransaction tran = con.BeginTransaction();//先实例SqlTransaction类，使用这个事务使用的是con 这个连接，使用BeginTransaction这个方法来开始执行这个事务
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.Transaction = tran;
            try
            {

                cmd.CommandText = "update  SolidBuyList_Handle set BuyQuantity=BuyQuantity-" + CfQuantity + ",State='拆分' where ID =" + dgvEdit["ID", RowIndex].Value.ToString();
                cmd.ExecuteNonQuery();
                cmd.CommandText = "INSERT INTO dbo.SolidBuyList_Handle (" +
                    "	SolidBuyList_Handle.PlanID,\n" +
"	SolidBuyList_Handle.ItemNumber,\n" +
"	SolidBuyList_Handle.ItemDescription,\n" +
"	SolidBuyList_Handle.ItemUM,\n" +
"	SolidBuyList_Handle.BuyQuantity,\n" +
"	SolidBuyList_Handle.InternationalStandards,\n" +
"	SolidBuyList_Handle.NeedTime,\n" +
"	SolidBuyList_Handle.OperateTime,\n" +
"	SolidBuyList_Handle.CompanyStatus,\n" +
"	SolidBuyList_Handle.PlanVendorName,\n" +
"	SolidBuyList_Handle.FSTITime,\n" +
"	SolidBuyList_Handle.PurChaseNumber,\n" +
"	SolidBuyList_Handle.ReceiveTime,\n" +
"	SolidBuyList_Handle.ReceiveQuantity,\n" +
"	SolidBuyList_Handle.ForeignNumber,\n" +
"	SolidBuyList_Handle.WorkCenter,\n" +
"	SolidBuyList_Handle.PlanRemark,\n" +
"	SolidBuyList_Handle.Flag,\n" +
"	SolidBuyList_Handle.SYBFlag,\n" +
"	SolidBuyList_Handle.State,\n" +
"	SolidBuyList_Handle.VendorNumber,\n" +
"	SolidBuyList_Handle.VendorName,\n" +
"	SolidBuyList_Handle.ManufacturerNumber,\n" +
"	SolidBuyList_Handle.ManufacturerName,\n" +
"	SolidBuyList_Handle.PricePreTax,\n" +
"	SolidBuyList_Handle.TaxRate,\n" +
"	SolidBuyList_Handle.Confirmer,\n" +
"	SolidBuyList_Handle.Remark \n" +
                    ") SELECT\n" +
"	SolidBuyList_Handle.PlanID,\n" +
"	SolidBuyList_Handle.ItemNumber,\n" +
"	SolidBuyList_Handle.ItemDescription,\n" +
"	SolidBuyList_Handle.ItemUM,\n" +
CfQuantity +
",	SolidBuyList_Handle.InternationalStandards,\n" +
"	SolidBuyList_Handle.NeedTime,\n" +
"	SolidBuyList_Handle.OperateTime,\n" +
"	SolidBuyList_Handle.CompanyStatus,\n" +
"	SolidBuyList_Handle.PlanVendorName,\n" +
"	SolidBuyList_Handle.FSTITime,\n" +
"	SolidBuyList_Handle.PurChaseNumber,\n" +
"	SolidBuyList_Handle.ReceiveTime,\n" +
"	SolidBuyList_Handle.ReceiveQuantity,\n" +
"	SolidBuyList_Handle.ForeignNumber,\n" +
"	SolidBuyList_Handle.WorkCenter,\n" +
"	SolidBuyList_Handle.PlanRemark,\n" +
"	SolidBuyList_Handle.Flag,\n" +
"	SolidBuyList_Handle.SYBFlag,\n" +
"	SolidBuyList_Handle.State,\n" +
"	SolidBuyList_Handle.VendorNumber,\n" +
"	SolidBuyList_Handle.VendorName,\n" +
"	SolidBuyList_Handle.ManufacturerNumber,\n" +
"	SolidBuyList_Handle.ManufacturerName,\n" +
"	SolidBuyList_Handle.PricePreTax,\n" +
"	SolidBuyList_Handle.TaxRate,\n" +
"	SolidBuyList_Handle.Confirmer,\n" +
"	SolidBuyList_Handle.Remark \n" +
"FROM\n" +
"	dbo.SolidBuyList_Handle where ID = " + dgvEdit["ID", RowIndex].Value.ToString();
                cmd.ExecuteNonQuery();

                tran.Commit();
                MessageBoxEx.Show("拆分完成！");
                btnExtractRefresh_Click(sender, e);
            }
            catch (Exception ex)
            {
                tran.Rollback();
                MessageBoxEx.Show("拆分失败：" + ex.Message);
            }
            tran.Dispose();
            con.Close();
            #endregion
        }

        private void cbbTaxRate_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (Char)Keys.Enter)
            {
                for (int i = 0; i < dgvEdit.Rows.Count; i++)
                {
                    if (Convert.ToBoolean(dgvEdit["选择", i].Value))
                    {
                        dgvEdit["税率", i].Value = cbbTaxRate.Text;
                    }
                }
            }
        }

        private void cbbConfirmPerson_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (Char)Keys.Enter)
            {
                for (int i = 0; i < dgvEdit.Rows.Count; i++)
                {
                    if (Convert.ToBoolean(dgvEdit["选择", i].Value))
                    {
                        dgvEdit["确认员", i].Value = cbbConfirmPerson.Text.Trim().Split('|')[0];
                    }
                }
            }
        }

        private void btnMatch_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dgvEdit.Rows.Count; i++)
            {
                if (Convert.ToBoolean(dgvEdit["选择", i].Value))
                {
                    string sqlSelect = @"SELECT
                                                Id,
                                                ItemNumber AS 物料代码,
                                                ItemDescription AS 物料描述,
                                                VendorNumber AS 供应商码,
                                                VendorName AS 供应商名,
                                                PricePreTax AS 含税价格
                                                FROM
                                                dbo.PurchaseDepartmentDomesticProductItemPrice where ItemNumber ='" + dgvEdit["物料代码", i].Value.ToString().Trim() + "'";
                    DataTable dt = SQLHelper.GetDataTable(GlobalSpace.FSDBConnstr, sqlSelect);
                    if (dt.Rows.Count > 0)
                    {
                        dgvEdit["供应商码", i].Value = dgvEdit["生产商码", i].Value = dt.Rows[0]["供应商码"].ToString().Trim();
                        dgvEdit["供应商名", i].Value = dgvEdit["生产商名", i].Value = dt.Rows[0]["供应商名"].ToString().Trim();
                        dgvEdit["税前价格", i].Value = dt.Rows[0]["含税价格"].ToString().Trim();
                    }
                }
            }
        }

        private void dgvEdit_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void 查找供应商ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dgvEdit.EndEdit();
            int RowIndex = Convert.ToInt32(contextMenuStrip1.Tag.ToString());
            if (string.IsNullOrEmpty(dgvEdit["供应商码", RowIndex].Value.ToString()))
            {
                MessageBoxEx.Show("供应商码为空！"); return;
            }
            string strSql = @"SELECT
	                                    VendorID,
	                                    VendorName
                                    FROM
	                                    _NoLock_FS_Vendor	                                    
                                    WHERE
	                                    VendorID = " + dgvEdit["供应商码", RowIndex].Value.ToString().Trim();
            DataTable dtTemp = SQLHelper.GetDataTableOleDb(GlobalSpace.oledbconnstrFSDBMR, strSql);

            if (dtTemp.Rows.Count == 1)
            {
                dgvEdit["供应商名", RowIndex].Value = dtTemp.Rows[0]["VendorName"].ToString();
                if (string.IsNullOrEmpty(dgvEdit["生产商码", RowIndex].Value.ToString()))
                {
                    dgvEdit["生产商码", RowIndex].Value = dgvEdit["供应商码", RowIndex].Value;
                    dgvEdit["生产商名", RowIndex].Value = dgvEdit["供应商名", RowIndex].Value;
                }
            }

        }
        private void 查找生产商ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dgvEdit.EndEdit();
            int RowIndex = Convert.ToInt32(contextMenuStrip1.Tag.ToString());
            if (string.IsNullOrEmpty(dgvEdit["生产商码", RowIndex].Value.ToString()))
            {
                MessageBoxEx.Show("生产商码为空！"); return;
            }
            string strSql = @"SELECT
	                                    VendorID,
	                                    VendorName
                                    FROM
	                                    _NoLock_FS_Vendor	                                    
                                    WHERE
	                                    VendorID = " + dgvEdit["生产商码", RowIndex].Value.ToString().Trim();
            DataTable dtTemp = SQLHelper.GetDataTableOleDb(GlobalSpace.oledbconnstrFSDBMR, strSql);

            if (dtTemp.Rows.Count == 1)
            {
                dgvEdit["生产商名", RowIndex].Value = dtTemp.Rows[0]["VendorName"].ToString();
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            dgvEdit.EndEdit();
            List<string> lstr = new List<string>();
            for (int i = 0; i < dgvEdit.Rows.Count; i++)
            {
                //                @"SELECT
                //	                                                    ID,PlanID AS 提报序号,
                //	                                                    rtrim(ltrim(ItemNumber)) AS 物料代码,
                //	                                                    rtrim(ltrim(ItemDescription)) AS 物料描述,
                //	                                                    rtrim(ltrim(ItemUM)) AS 单位,
                //	                                                    BuyQuantity AS 需求数量,
                //VendorNumber AS 供应商码,VendorName AS 供应商名,ManufacturerNumber AS 生产商码,ManufacturerName AS 生产商名,PricePreTax AS 税前价格,TaxRate AS 税率,Confirmer AS 确认员,Remark AS 备注,
                //	                                                    rtrim(ltrim(InternationalStandards)) AS 检验标准,
                //	                                                    NeedTime AS 需求日期,
                //	                                                    rtrim(ltrim(PlanVendorName)) AS 计划指定供应商,
                //	                                                    rtrim(ltrim(PlanRemark)) AS 计划备注,
                //                                                        case when  SYBFlag=0 then '固水'  when  SYBFlag=1 then '粉针' when  SYBFlag=2 then '原料' else '其他' end  AS 事业部,rtrim(ltrim(WorkCenter)) AS 需求车间,State,OperateTime AS 提报日期
                //                                                    FROM
                //	                                                    dbo.SolidBuyList_Handle 
                //                                                    WHERE
                //	                                                    Flag = 0 order by rtrim(ltrim(ItemNumber))";
                if (Convert.ToBoolean(dgvEdit["选择", i].Value))
                {
                    lstr.Add(@"update dbo.SolidBuyList_Handle set VendorNumber ='" + dgvEdit["供应商码", i].Value.ToString().Trim() + "',VendorName='" + dgvEdit["供应商名", i].Value.ToString().Trim() + "',ManufacturerNumber ='" + dgvEdit["生产商码", i].Value.ToString().Trim() + "',ManufacturerName ='" + dgvEdit["生产商名", i].Value.ToString().Trim() + "',PricePreTax=" + dgvEdit["税前价格", i].Value.ToString().Trim() + ",TaxRate=" + dgvEdit["税率", i].Value.ToString().Trim() + ",Confirmer='" + dgvEdit["确认员", i].Value.ToString().Trim() + "',Remark='" + dgvEdit["备注", i].Value.ToString().Trim() + "', InternationalStandards='" + dgvEdit["检验标准", i].Value.ToString().Trim() + "', ForeignNumber='" + dgvEdit["联系单号", i].Value.ToString().Trim() + "' where ID=" + dgvEdit["ID", i].Value.ToString());
                }
            }
            if (lstr.Count == 0)
            {
                MessageBoxEx.Show("未选中任何行"); return;
            }
            #region 事务批量添加数据
            SqlConnection con = new SqlConnection(GlobalSpace.RYData);
            con.Open();
            SqlTransaction tran = con.BeginTransaction();//先实例SqlTransaction类，使用这个事务使用的是con 这个连接，使用BeginTransaction这个方法来开始执行这个事务
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.Transaction = tran;

            try
            {
                foreach (string str in lstr)
                {
                    cmd.CommandText = str;
                    cmd.ExecuteNonQuery();
                }
                tran.Commit();
                MessageBoxEx.Show("保存完成！");
                btnExtractRefresh_Click(sender, e);
            }
            catch (Exception ex)
            {
                tran.Rollback();
                MessageBoxEx.Show("保存失败：" + ex.Message);
            }
            tran.Dispose();
            con.Close();
            #endregion
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            string sqlSelect = @"SELECT  ItemNumber
                                                    FROM
	                                                    dbo.SolidBuyList_Handle 
                                                    WHERE
	                                                    (VendorNumber is null or ltrim(rtrim(VendorNumber)) ='' or VendorName is null or ltrim(rtrim(VendorName)) ='' or ManufacturerNumber is null or ltrim(rtrim(ManufacturerNumber)) ='' or ManufacturerName is null or ltrim(rtrim(ManufacturerName)) ='' or Confirmer is null or ltrim(rtrim(Confirmer)) ='' or PricePreTax is null or TaxRate is null or PricePreTax <=0 or TaxRate <=0) and Flag = 0 ";
            DataTable dt = SQLHelper.GetDataTable(GlobalSpace.RYData, sqlSelect);
            if (dt.Rows.Count > 0)
            {
                IEnumerable<String> lstr = dt.DefaultView.ToTable(true, "ItemNumber").Rows.Cast<DataRow>().Select(r => (r["ItemNumber"].ToString()));
                MessageBoxEx.Show("以下物料信息不完整不能提交" + String.Join(",", lstr));
                return;
            }
            if (SQLHelper.ExecuteNonQuery(GlobalSpace.RYData, "Update dbo.SolidBuyList_Handle set Flag = 1,PricePostTax=PricePreTax/(1+TaxRate) where Flag = 0"))
            {
                MessageBoxEx.Show("提交完成");
                btnExtractRefresh_Click(sender, e);
            }
            else
            {
                MessageBoxEx.Show("提交失败");
            }
        }

        private void btnEditedRefresh_Click(object sender, EventArgs e)
        {
            string sqlSelect = @"SELECT
	                                                    ID,PlanID AS 提报序号,
	                                                    rtrim(ltrim(ItemNumber)) AS 物料代码,
	                                                    rtrim(ltrim(ItemDescription)) AS 物料描述,
	                                                    rtrim(ltrim(ItemUM)) AS 单位,
	                                                    BuyQuantity AS 需求数量,
VendorNumber AS 供应商码,VendorName AS 供应商名,ManufacturerNumber AS 生产商码,ManufacturerName AS 生产商名,PricePreTax AS 税前价格,TaxRate AS 税率,PricePostTax AS 税后价格,Confirmer AS 确认员,Remark AS 备注,
	                                                    rtrim(ltrim(InternationalStandards)) AS 检验标准,
                                                        rtrim(ltrim(ForeignNumber)) AS 联系单号,
	                                                    NeedTime AS 需求日期,
	                                                    rtrim(ltrim(PlanVendorName)) AS 计划指定供应商,
	                                                    rtrim(ltrim(PlanRemark)) AS 计划备注,
                                                        case when  SYBFlag=0 then '固水'  when  SYBFlag=1 then '粉针' when  SYBFlag=2 then '原料' else '其他' end  AS 事业部,rtrim(ltrim(WorkCenter)) AS 需求车间,State,OperateTime AS 提报日期
                                                    FROM
	                                                    dbo.SolidBuyList_Handle 
                                                    WHERE
	                                                    Flag = 1 order by rtrim(ltrim(ItemNumber))";
            DgvEdited.DataSource = SQLHelper.GetDataTable(GlobalSpace.RYData, sqlSelect);
            DgvEdited.Columns["ID"].Visible = false;
            for (int i = 0; i < this.DgvEdited.Columns.Count; i++)
            {
                DgvEdited.Columns[i].ReadOnly = true;
            }

            tbPOMiddle.Text = DateTime.Now.ToString("MMddyy");
            tbPOHeader.Text = "P" + PurchaseUser.PurchaseType.Substring(0, 1);
            tbPOPostfix.Text = GeneratePONumberSequenceNumber(tbPOHeader.Text, PurchaseUser.UserID);

        }

        private string GeneratePONumberSequenceNumber(string poType, string UserID)
        {
            string sqlSelect = @"Select TOP 1 PONumber From PurchaseOrderRecordByCMF Where Buyer = '" + UserID + "' And Left(PONumber,2) = '" + poType + "' And POItemPlacedDate = '" + DateTime.Now.ToString("yyyy-MM-dd") + "'  AND IsPurePO = 1 ORDER BY Id DESC";
            string sqlSelectFS = @" SELECT
	                            T1.PONumber
                            FROM
	                            _NoLock_FS_POHeader T1
                            WHERE                               
                                T1.Buyer ='" + UserID + "' AND T1.PONumber LIKE '%" + DateTime.Now.ToString("MMddyy") + "%'  ORDER BY T1.PONumber DESC";
            DataTable dt = SQLHelper.GetDataTable(GlobalSpace.FSDBConnstr, sqlSelect);
            DataTable dtLatestFS = SQLHelper.GetDataTable(GlobalSpace.FSDBMRConnstr, sqlSelectFS);
            int sequenceNumber = 0;
            if (dt.Rows.Count > 0)
            {
                sequenceNumber = Convert.ToInt32(dt.Rows[0]["PONumber"].ToString().Substring(10));
            }
            int sequenceNumberFS = 0;
            if (dtLatestFS.Rows.Count > 0)
            {
                sequenceNumberFS = Convert.ToInt32(dtLatestFS.Rows[0]["PONumber"].ToString().Substring(10));
            }

            if (sequenceNumberFS > sequenceNumber)
            {
                sequenceNumber = sequenceNumberFS;
            }
            return sequenceNumber.ToString().PadLeft(3, '0');

        }


        private void BtnTurnback_Click(object sender, EventArgs e)
        {
            if (SQLHelper.ExecuteNonQuery(GlobalSpace.RYData, "Update dbo.SolidBuyList_Handle set Flag = 0 where Flag = 1"))
            {
                MessageBoxEx.Show("退回完成");
                btnEditedRefresh_Click(sender, e);
            }
            else
            {
                MessageBoxEx.Show("退回失败");
            }
        }

        private void btnPlaceOrder_Click(object sender, EventArgs e)
        {
            List<string> itemNumberList = new List<string>();


            if (DgvEdited.Rows.Count == 0)
            {
                Custom.MsgEx("当前无可用数据！");
                return;
            }
                DataTable dtVendor = (DataTable)DgvEdited.DataSource;
                DataTable dtItem = dtVendor.Copy();
            if (CommonOperate.PlaceOrderWithItemDetail("PP", dtVendor, dtItem, PurchaseUser.UserName, PurchaseUser.UserID, PurchaseUser.SupervisorID, 1))
            {
                Custom.MsgEx("订单已提交审核！");
                if (!SQLHelper.ExecuteNonQuery(GlobalSpace.RYData, "Update dbo.SolidBuyList_Handle set Flag = 2,PlaceOrderTime=GETDATE() where Flag = 1"))
                {
                    MessageBoxEx.Show("订单已提交审核，计划状态更改失败，请联系软件服务处");
                }
                else
                {
                    btnEditedRefresh_Click(sender, e);
                }
                if (itemNumberList.Count > 0)
                {
                    string itemNumbers = string.Empty;
                    for (int x = 0; x < itemNumberList.Count; x++)
                    {
                        itemNumbers = itemNumbers + " " + itemNumberList[x];
                    }
                    MessageBox.Show("以下物料价格超出四班标准价格15%，无法下达订单！", "提示");
                }
                List<string> listSuper = CommonOperate.GetSuperiorNameAndEmail(PurchaseUser.UserID);
                string sqlSelectUserInfo = @"Select Email,Password,Name From PurchaseDepartmentRBACByCMF Where UserID='" + PurchaseUser.UserID + "'";
                DataTable dtUserInfo = SQLHelper.GetDataTable(GlobalSpace.FSDBConnstr, sqlSelectUserInfo);
                string supername = listSuper[0];
                string supermail = listSuper[1];

                if (dtUserInfo.Rows.Count > 0)
                {
                    if (dtUserInfo.Rows[0]["Email"] != DBNull.Value && dtUserInfo.Rows[0]["Email"].ToString() != "")
                    {
                        List<string> smtpList = CommonOperate.GetSMTPServerInfo();
                        if (smtpList.Count > 0)
                        {
                            Email email = new Email();
                            email.fromEmail = dtUserInfo.Rows[0]["Email"].ToString();
                            email.fromPerson = dtUserInfo.Rows[0]["Name"].ToString();
                            email.toEmail = supermail;
                            email.toPerson = supername;
                            email.encoding = "UTF-8";
                            email.smtpServer = smtpList[0];
                            email.userName = dtUserInfo.Rows[0]["Email"].ToString();
                            email.passWord = CommonOperate.Base64Decrypt(dtUserInfo.Rows[0]["Password"].ToString());
                            email.emailTitle = "采购订单审核提醒";
                            email.emailContent = supername + "处长" + "：" + "<br>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + "您好!<br>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;采购员已提交采购订单申请，请及时审批！";

                            if (MailHelper.SendReminderEmail(email))
                            {
                                MessageBoxEx.Show("邮件发送成功！", "提示");
                            }
                            else
                            {
                                MessageBoxEx.Show("邮件发送失败！", "提示");
                            }
                        }
                        else
                        {
                            MessageBoxEx.Show("未设置SMTP服务器IP地址和端口，请联系管理员！", "提示");
                        }
                    }
                    else
                    {
                        MessageBoxEx.Show("邮箱未设置！", "提示");
                    }
                }
            }
            else
            {
                Custom.MsgEx("订单提交审核失败");
            }
        }

        private void tabItem1_Click(object sender, EventArgs e)
        {
            GetRequireItem();
        }

        private void btnHistorySelect_Click(object sender, EventArgs e)
        {
            String StrWhere = String.Empty;
            DateTime Dt = dtpDate.Value;
            if (rbtnMonth.Checked == true)
            {
                StrWhere = "PlaceOrderTime >= '" + Dt.ToString("yyyy-MM") + "-01' and PlaceOrderTime<'" + Dt.AddMonths(1).ToString("yyyy-MM") + "-01'";
            }
            else
            {
                StrWhere = "PlaceOrderTime >= '"+Dt.ToString("yyyy-MM-dd")+ "' and PlaceOrderTime<'" + Dt.AddDays(1).ToString("yyyy-MM-dd") + "'";
            }
            string sqlSelect = @"SELECT
	                                                    ID,PlanID AS 提报序号,
	                                                    rtrim(ltrim(ItemNumber)) AS 物料代码,
	                                                    rtrim(ltrim(ItemDescription)) AS 物料描述,
	                                                    rtrim(ltrim(ItemUM)) AS 单位,
	                                                    BuyQuantity AS 需求数量,
VendorNumber AS 供应商码,VendorName AS 供应商名,ManufacturerNumber AS 生产商码,ManufacturerName AS 生产商名,PricePreTax AS 税前价格,TaxRate AS 税率,PricePostTax AS 税后价格,Confirmer AS 确认员,Remark AS 备注,
	                                                    rtrim(ltrim(InternationalStandards)) AS 检验标准,
                                                        rtrim(ltrim(ForeignNumber)) AS 联系单号,
	                                                    NeedTime AS 需求日期,
	                                                    rtrim(ltrim(PlanVendorName)) AS 计划指定供应商,
	                                                    rtrim(ltrim(PlanRemark)) AS 计划备注,
                                                        case when  SYBFlag=0 then '固水'  when  SYBFlag=1 then '粉针' when  SYBFlag=2 then '原料' else '其他' end  AS 事业部,rtrim(ltrim(WorkCenter)) AS 需求车间,State,OperateTime AS 提报日期,PlaceOrderTime AS 下单日期
                                                    FROM
	                                                    dbo.SolidBuyList_Handle 
                                                    WHERE
	                                                    Flag = 2 and " + StrWhere+" order by PlaceOrderTime, rtrim(ltrim(ItemNumber))";
            DgvHistory.DataSource = SQLHelper.GetDataTable(GlobalSpace.RYData, sqlSelect);
            //DgvHistory.Columns["ID"].Visible = false;
        }

        private void Btntuihui_Click(object sender, EventArgs e)
        {
            List<int> Lint = new List<int>();
            String TbID = string.Empty;
            for (int i = 0; i < dgvEdit.Rows.Count; i++)
            {
                if (Convert.ToBoolean(dgvEdit["选择", i].Value))
                {
                    Lint.Add(Convert.ToInt32(dgvEdit["ID", i].Value));
                    if (TbID == string.Empty)
                    {
                        TbID += dgvEdit["提报序号", i].Value.ToString();
                    }
                    else
                    {
                        TbID +="|"+dgvEdit["提报序号", i].Value.ToString();
                    }
                }
            }
            for (int i = 0; i < dgvEdit.Rows.Count; i++)
            {
                if (!Convert.ToBoolean(dgvEdit["选择", i].Value)&& dgvEdit["State", i].Value.ToString()== "拆分")
                {
                    if (TbID.Split('|').Contains(dgvEdit["提报序号", i].Value.ToString()))
                    {
                        Lint.Add(Convert.ToInt32(dgvEdit["ID", i].Value));
                    }
                }
            }
            if (Lint.Count == 0)
            { MessageBoxEx.Show("未选择任何行！");return; }
            #region 事务处理
            SqlConnection con = new SqlConnection(GlobalSpace.RYData);
            con.Open();
            SqlTransaction tran = con.BeginTransaction();//先实例SqlTransaction类，使用这个事务使用的是con 这个连接，使用BeginTransaction这个方法来开始执行这个事务
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.Transaction = tran;
            try
            {

                cmd.CommandText = "update  [dbo].[SolidBuyList] set Flag=0 where ID in (" + TbID.Replace("|",",") + ")";
                cmd.ExecuteNonQuery();
                cmd.CommandText = "delete from  SolidBuyList_Handle where ID in("+string.Join(",",Lint)+")";
                cmd.ExecuteNonQuery();

                tran.Commit();
                MessageBoxEx.Show("退回完成！");
                GetRequireItem();
                btnExtractRefresh_Click(sender, e);
            }
            catch (Exception ex)
            {
                tran.Rollback();
                MessageBoxEx.Show("退回失败：" + ex.Message);
            }
            tran.Dispose();
            con.Close();
            #endregion
        }

        private void btnTemplateDownload_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveDialog = new SaveFileDialog();

            saveDialog.DefaultExt = "";

            saveDialog.Filter = "Excel文件|*.xlsx";

            saveDialog.FileName = "内包价格模板";

            if (saveDialog.ShowDialog() != DialogResult.OK)

            {

                return;

            }


            FileStream fs = new FileStream(saveDialog.FileName, FileMode.OpenOrCreate);

            BinaryWriter bw = new BinaryWriter(fs);
            byte[] data = Resources.内包价格;
            bw.Write(data, 0, data.Length);
            bw.Close();
            fs.Close();

            if (File.Exists(saveDialog.FileName))

            {

                System.Diagnostics.Process.Start(saveDialog.FileName); //打开文件

            }
        }

        private void btnSelectExcel_Click(object sender, EventArgs e)
        {
            try
            {
                string file = "";
                OpenFileDialog dialog = new OpenFileDialog();
                dialog.Multiselect = true;//该值确定是否可以选择多个文件
                dialog.Title = "请选择文件夹";
                dialog.Filter = "Excel文件(*.xlsx)|*.xlsx|Excel文件(*.xls)|*.xls";
                if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    file = dialog.FileName;
                }
                else return;
                DgvVendor.DataSource = null;
                DgvVendor.DataSource = ExcelToTable(file);
                //#region 设置列宽
                //for (int i = 0; i < this.dgv.Columns.Count; i++)
                //{
                //    this.dgv.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
                //    this.dgv.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                //}
                //#endregion
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public DataTable ExcelToTable(string file)
        {
            // AppDomain.CurrentDomain.AssemblyResolve += CurrentDomain_AssemblyResolve;
            if (file == "") return null;

            DataTable dt = new DataTable();
            IWorkbook workbook;
            string fileExt = Path.GetExtension(file).ToLower();
            string fileoutExt = Path.GetFileNameWithoutExtension(file).ToLower();

            using (FileStream fs = new FileStream(file, FileMode.Open, FileAccess.Read))
            {
                //XSSFWorkbook 适用XLSX格式，HSSFWorkbook 适用XLS格式
                if (fileExt == ".xlsx")//新版本excel2007
                { workbook = new XSSFWorkbook(fs); }
                else if (fileExt == ".xls")//早期版本excel2003
                { workbook = new HSSFWorkbook(fs); }
                else { workbook = null; }
                if (workbook == null) { return null; }
                ISheet sheet = workbook.GetSheetAt(0);//下标为零的工作簿
                //创建表头      FirstRowNum:获取第一个有数据的行好(默认0)
                IRow header = sheet.GetRow(sheet.FirstRowNum);//第一行是头部信息
                List<int> columns = new List<int>();
                for (int i = 0; i < header.LastCellNum; i++)//LastCellNum 获取列的条数
                {
                    object obj = GetValueType(header.GetCell(i));
                    if (obj == null || obj.ToString() == string.Empty)
                    {
                        dt.Columns.Add(new DataColumn("Columns" + i.ToString()));//如果excel没有列头就自定义
                    }
                    else
                        dt.Columns.Add(new DataColumn(obj.ToString()));//获取excel列头
                    columns.Add(i);
                }
                //构建数据   sheet.FirstRowNum + 1 表示去掉列头信息
                for (int i = sheet.FirstRowNum + 1; i <= sheet.LastRowNum; i++)//LastRowNum最后一条数据的行号
                {
                    DataRow dr = dt.NewRow();
                    bool hasValue = false;//判断是否有值
                    foreach (int j in columns)
                    {
                        //if (sheet.GetRow(i) == null) continue;//如果没数据
                        dr[j] = GetValueType(sheet.GetRow(i).GetCell(j));
                        if (dr[j] != null && dr[j].ToString() != string.Empty)//判断至少一列有值
                        {
                            hasValue = true;
                        }
                    }
                    if (hasValue)
                    {
                        dt.Rows.Add(dr);
                    }
                }
            }
            return dt;
        }
        private static object GetValueType(ICell cell)
        {

            if (cell == null)
                return null;
            switch (cell.CellType)
            {
                case CellType.Boolean: //BOOLEAN:  
                    return cell.BooleanCellValue;
                case CellType.Formula: //BOOLEAN: 
                    cell.SetCellType(CellType.String);
                    return cell.StringCellValue;
                case CellType.Numeric: //NUMERIC:  
                    if (DateUtil.IsCellDateFormatted(cell))//判断是否日期
                        return cell.DateCellValue.ToString("yyyy/MM/dd");
                    else
                        return cell.NumericCellValue;
                case CellType.Error: //ERROR:  
                    return cell.ErrorCellValue;
                case CellType.String: //STRING:  
                default:
                    return cell.StringCellValue;

            }
        }

        private void btnMatchLs_Click(object sender, EventArgs e)
        {
            if (DgvVendor.Rows.Count == 0) { MessageBoxEx.Show("临时表无数据！"); return; }
                DataTable dtVendor = (DataTable)DgvVendor.DataSource;

            for (int i = 0; i < dgvEdit.Rows.Count; i++)
            {
                if (Convert.ToBoolean(dgvEdit["选择", i].Value))
                {
                    DataRow[] drs = dtVendor.Select("物料代码='" + dgvEdit["物料代码", i].Value.ToString().Trim() + "'"); 
                    if (drs.Length > 0)
                    {
                        dgvEdit["供应商码", i].Value = dgvEdit["生产商码", i].Value = drs[0]["供应商码"].ToString().Trim();
                        dgvEdit["供应商名", i].Value = dgvEdit["生产商名", i].Value = drs[0]["供应商名"].ToString().Trim();
                        dgvEdit["税前价格", i].Value = drs[0]["含税价格"].ToString().Trim();
                    }
                }
            }
        }

        private void BtnPlanHistorySelect_Click(object sender, EventArgs e)
        {
            String StrWhere = String.Empty;
            DateTime Dt = dtpDate.Value;
            if (rbtnMonth.Checked == true)
            {
                StrWhere = "ExtractTime >= '" + Dt.ToString("yyyy-MM") + "-01' and ExtractTime<'" + Dt.AddMonths(1).ToString("yyyy-MM") + "-01'";
            }
            else
            {
                StrWhere = "ExtractTime >= '" + Dt.ToString("yyyy-MM-dd") + "' and ExtractTime<'" + Dt.AddDays(1).ToString("yyyy-MM-dd") + "'";
            }
            string sqlSelect = @"SELECT
	                                                    ID,OperateTime AS 提报日期,rtrim(ltrim(WorkCenter)) AS 需求车间,
	                                                    rtrim(ltrim(ItemNumber)) AS 物料代码,
	                                                    rtrim(ltrim(ItemDescription)) AS 物料描述,
	                                                    rtrim(ltrim(ItemUM)) AS 单位,
	                                                    BuyQuantity AS 需求数量,
	                                                    rtrim(ltrim(InternationalStandards)) AS 检验标准,
                                                        rtrim(ltrim(ForeignNumber)) AS 联系单号,
	                                                    NeedTime AS 需求日期,
	                                                    rtrim(ltrim(Remark)) AS 备注,
	                                                    rtrim(ltrim(VendorName)) AS 指定供应商,
                                                        case when  SYBFlag=0 then '固水'  when  SYBFlag=1 then '粉针' when  SYBFlag=2 then '原料' else '其他' end  AS 事业部,ReceiveTime AS 到货日期,ReceiveQuantity AS 到货数量,ExtractTime AS 提取日期, case when  Flag=1 then '已处理' when  Flag=2 then '已到货' else '' end AS 状态
                                                    FROM
	                                                    dbo.SolidBuyList 
                                                    WHERE
	                                                    Flag in(1,2) and " + StrWhere + " order by ID";
            DgvHistory.DataSource = SQLHelper.GetDataTable(GlobalSpace.RYData, sqlSelect);
        }
    }
}
