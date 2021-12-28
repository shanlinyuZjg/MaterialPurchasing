using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using Global;
using Global.Helper;
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
	                                                    NeedTime AS 需求日期,
	                                                    rtrim(ltrim(Remark)) AS 备注,
	                                                    rtrim(ltrim(VendorName)) AS 指定供应商
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
            dgvItemRequirement1.DataSource = SQLHelper.GetDataTable(GlobalSpace.RYData, sqlSelect);
            dgvItemRequirement1.Columns["ID"].Visible = false;
            for (int i = 0; i < this.dgvItemRequirement1.Columns.Count; i++)
            {
                //this.dgvSpecification.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
                dgvItemRequirement1.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
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
            if(string.IsNullOrWhiteSpace(cbbVendorNumber.Text) || string.IsNullOrWhiteSpace(tbPricePreTax.Text) || string.IsNullOrWhiteSpace(tbRemark.Text))
            {
                MessageBoxEx.Show("信息不能为空！","提示");
                return;
            }
            List<string> sqlList = new List<string>();
            foreach(DataGridViewRow dgvr in dgvItemRequirement.Rows)
            {
                if(Convert.ToBoolean(dgvr.Cells["Check"].Value))
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
	                                                            ('" + dgvr.Cells["物料代码"].Value.ToString()+"','"+ dgvr.Cells["物料描述"].Value.ToString() + "',	'"+ dgvr.Cells["单位"].Value.ToString() + "','"+Convert.ToDouble(dgvr.Cells["需求数量"].Value) + "','" + dgvr.Cells["检验标准"].Value.ToString() + "','" + dgvr.Cells["需求日期"].Value.ToString() + "','"+ dgvr.Cells["备注"].Value.ToString() + "','"+ dgvr.Cells["指定供应商"].Value.ToString() + "','"+cbbVendorNumber.Text.Split('|')[0]+ "','"+ cbbVendorNumber.Text.Split('|')[1] + "','"+Convert.ToInt32(dgvr.Cells["ID"].Value) + "','"+ tbRemark.Text + "',"+Convert.ToDouble(tbPricePreTax.Text)+",'"+PurchaseUser.UserID+"' )";
                    sqlList.Add(sqlInsert);
                }
            }
            if(sqlList.Count == 0)
            {
                MessageBoxEx.Show("请选择一条待处理计划！", "提示");
                return;
            }
            else if(sqlList.Count > 1)
            {
                MessageBoxEx.Show("一次只能处理一条需求计划信息", "提示");
                return;
            }
            else
            {
                if(SQLHelper.BatchExecuteNonQuery(GlobalSpace.FSDBConnstr,sqlList))
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

        }

        private void BtTableExport_Click(object sender, EventArgs e)
        {
            if (dgvItemRequirement.Rows.Count == 0)
            { MessageBox.Show("无数据！"); return; }

            string filePath = getExcelpath();
            if (filePath.IndexOf(":") < 0)
            { return; }
            TableToExcel(dgvItemRequirement, filePath);
            MessageBox.Show("导出完成");
            SetFlag1(dgvItemRequirement);
            GetRequireItem();
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
            string UpdateFlag = "update  [dbo].[SolidBuyList] set Flag=1 where ID in ("+string.Join(",",lint.ToArray())+")";
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
            for (int i = 0,x=0; i < dt.Rows.Count; i++,x++)
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
    }
}
