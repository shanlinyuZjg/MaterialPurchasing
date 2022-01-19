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
using System.IO;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using NPOI.HSSF.UserModel;
using Global.Properties;
using System.Data.SqlClient;

namespace Global.Purchase
{
    public partial class ManageItemPrice : Office2007Form
    {
        string UserID = string.Empty;
        public ManageItemPrice(string id)
        {
            UserID = id;
            InitializeComponent();
            this.EnableGlass = false;
            MessageBoxEx.EnableGlass = false;
        }

        private void tbItemNumber_TextChanged(object sender, EventArgs e)
        {
            tbItemNumber.Text = tbItemNumber.Text.ToUpper();
            tbItemNumber.SelectionStart = tbItemNumber.MaxLength;
        }

        private void tbItemNumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (CommonOperate.TextBoxCheck(tbItemNumber, e))
            {
                string sqlSelectItemInfo = @"Select  ItemDescription From _NoLock_FS_Item Where ItemNumber='" + tbItemNumber.Text + "'";
                DataTable dtTemp = SQLHelper.GetDataTableOleDb(GlobalSpace.oledbconnstrFSDBMR, sqlSelectItemInfo);
                if (dtTemp.Rows.Count > 0)
                {
                    tbItemDescription.Text = dtTemp.Rows[0]["ItemDescription"].ToString();
                }

                string sqlSelect = @"SELECT
                                                Id,
                                                ItemNumber AS 物料代码,
                                                ItemDescription AS 物料描述,
                                                VendorNumber AS 供应商码,
                                                VendorName AS 供应商名,
                                                PricePreTax AS 含税价格
                                                FROM
                                                dbo.PurchaseDepartmentDomesticProductItemPrice Where ItemNumber = '" + tbItemNumber.Text + "'";
                dgv.DataSource = SQLHelper.GetDataTable(GlobalSpace.FSDBConnstr, sqlSelect);
                dgv.Columns["Id"].Visible = false;
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(tbItemNumber.Text))
            {
                if (string.IsNullOrEmpty(tbItemDescription.Text) || string.IsNullOrEmpty(tbVendorName.Text) || string.IsNullOrEmpty(tbPricePreTax.Text) || string.IsNullOrEmpty(tbVendorNumber.Text))
                {
                    Custom.MsgEx("必填项不得为空！");
                    return;
                }
                string sqlInsert = @"Insert Into PurchaseDepartmentDomesticProductItemPrice ([ItemNumber],
                                                                 [ItemDescription],
                                                                 [VendorNumber],
                                                                 [VendorName],
                                                                 [PricePreTax],
                                                                 [Operator]) Values ('" + tbItemNumber.Text + "','" + tbItemDescription.Text + "','" + tbVendorNumber.Text + "','" + tbVendorName.Text + "'," + Convert.ToDecimal(tbPricePreTax.Text) + ",'" + UserID + "')";
                if (SQLHelper.ExecuteNonQuery(GlobalSpace.FSDBConnstr, sqlInsert))
                {
                    Custom.MsgEx("增加成功！");
                    tbItemNumber.Text = "";
                    tbItemDescription.Text = "";
                    tbPricePreTax.Text = "";
                    tbVendorName.Text = "";
                    tbVendorNumber.Text = "";
                }
                else
                {
                    Custom.MsgEx("增加失败！");
                }
            }
        }

        private void btnModify_Click(object sender, EventArgs e)
        {
            Custom.MsgEx("修改时只修改改行物料含税价格~！");
            if (dgv.SelectedRows.Count == 1)
            {
                dgv.EndEdit();
                string id = dgv.SelectedRows[0].Cells["Id"].Value.ToString();
                double price = Convert.ToDouble(dgv.SelectedRows[0].Cells["含税价格"].Value);
                string sqlUpdate = @"Update PurchaseDepartmentDomesticProductItemPrice Set PricePreTax =" + price + ", OperateDateTime='" + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss") + "',Operator='" + UserID + "' Where Id = " + Convert.ToInt32(id) + "";
                if (SQLHelper.ExecuteNonQuery(GlobalSpace.FSDBConnstr, sqlUpdate))
                {
                    Custom.MsgEx("修改成功！");
                }
                else
                {
                    Custom.MsgEx("修改失败！");
                }
            }
            else
            {
                Custom.MsgEx("当前无选中行或选中了多行！");
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgv.SelectedRows.Count == 1)
            {
                string id = dgv.SelectedRows[0].Cells["Id"].Value.ToString();
                string sqlDelete = @"Delete From  dbo.PurchaseDepartmentDomesticProductItemPrice Where Id = " + Convert.ToInt32(id) + "";
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
            {
                Custom.MsgEx("当前无选中行或选中了多行！");
            }
        }

        private void tbVendorNumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!string.IsNullOrEmpty(tbVendorNumber.Text))
            {
                tbVendorName.Text = GetVendorName(tbVendorNumber.Text);
            }
        }

        //获取供应商名字
        private string GetVendorName(string vendorId)
        {
            string strTemp = "";
            string strSelect = @"Select VendorName From _NoLock_FS_Vendor Where VendorID='" + vendorId + "'";
            try
            {
                DataTable dtTemp = SQLHelper.GetDataTableOleDb(GlobalSpace.oledbconnstrFSDBMR, strSelect);
                if (dtTemp.Rows.Count > 0)
                {
                    strTemp = dtTemp.Rows[0]["VendorName"].ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBoxEx.Show("异常：" + ex.Message);
            }

            return strTemp;
        }

        private void ManageItemPrice_Load(object sender, EventArgs e)
        {

        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            if (dgv.Rows.Count == 0)
            { MessageBox.Show("无数据！"); return; }

            string filePath = getExcelpath();
            if (filePath.IndexOf(":") < 0)
            { return; }
            TableToExcel(dgv, filePath);
            MessageBox.Show("导出完成");
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

        private void btnAll_Click(object sender, EventArgs e)
        {
            string sqlSelect = @"SELECT
                                                Id,
                                                ItemNumber AS 物料代码,
                                                ItemDescription AS 物料描述,
                                                VendorNumber AS 供应商码,
                                                VendorName AS 供应商名,
                                                PricePreTax AS 含税价格
                                                FROM
                                                dbo.PurchaseDepartmentDomesticProductItemPrice ";
            dgv.DataSource = SQLHelper.GetDataTable(GlobalSpace.FSDBConnstr, sqlSelect);
            dgv.Columns["Id"].Visible = false;
        }

        private void btnBatchDelete_Click(object sender, EventArgs e)
        {
            List<int> Lint = new List<int>();
            foreach (DataGridViewRow dgvrow in dgv.Rows)
            {
                if (Convert.ToBoolean(dgvrow.Cells["Check"].Value))
                {
                    Lint.Add(Convert.ToInt32(dgvrow.Cells["Id"].Value));
                }
            }
            if (Lint.Count == 0)
            { MessageBoxEx.Show("未选中任何行！"); return; }
            string sqlDelete = @"Delete From  dbo.PurchaseDepartmentDomesticProductItemPrice Where Id in (" + String.Join(",", Lint) + ")";
            if (SQLHelper.ExecuteNonQuery(GlobalSpace.FSDBConnstr, sqlDelete))
            {
                Custom.MsgEx("批量删除成功！");
                btnAll_Click(sender, e);
            }
            else
            {
                Custom.MsgEx("批量删除失败！");
            }
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
                dgv.DataSource = null;
                dgv.DataSource = ExcelToTable(file);
                #region 设置列宽
                for (int i = 0; i < this.dgv.Columns.Count; i++)
                {
                    this.dgv.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
                    this.dgv.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                }
                #endregion
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

        private void btnBatchImport_Click(object sender, EventArgs e)
        {
            if (dgv.Rows.Count == 0)
            {
                MessageBoxEx.Show("无数据！"); return;
            }
            if (dgv.Columns.Contains("Id"))
            {
                MessageBoxEx.Show("当前不是导入的数据！"); return;
            }
            if (!dgv.Columns.Contains("物料代码") || !dgv.Columns.Contains("物料描述") || !dgv.Columns.Contains("供应商码") || !dgv.Columns.Contains("供应商名") || !dgv.Columns.Contains("含税价格"))
            {
                MessageBoxEx.Show("列名不正确，请下载模板！"); return;
            }
            List<string> Lsql = new List<string>();
            for (int i = 0; i < dgv.Rows.Count; i++)
            {
                if (string.IsNullOrWhiteSpace(dgv["物料代码", i].Value.ToString()) || string.IsNullOrWhiteSpace(dgv["物料描述", i].Value.ToString()) || string.IsNullOrWhiteSpace(dgv["供应商码", i].Value.ToString()) || string.IsNullOrWhiteSpace(dgv["供应商名", i].Value.ToString()) || string.IsNullOrWhiteSpace(dgv["含税价格", i].Value.ToString()))
                {
                    MessageBoxEx.Show(string.Format("第{0}行有空数据！", i + 1));
                    return;
                }
                Decimal PricePreTax;
                if (!Decimal.TryParse(dgv["含税价格", i].Value.ToString().Trim(), out PricePreTax))
                {
                    MessageBoxEx.Show(string.Format("第{0}行含税价格不是数字！", i + 1));
                    return;
                }
                string sqlInsert = @"Insert Into PurchaseDepartmentDomesticProductItemPrice ([ItemNumber],
                                                                 [ItemDescription],
                                                                 [VendorNumber],
                                                                 [VendorName],
                                                                 [PricePreTax],
                                                                 [Operator]) Values ('" + dgv["物料代码", i].Value.ToString().Trim() + "','" + dgv["物料描述", i].Value.ToString().Trim() + "','" + dgv["供应商码", i].Value.ToString().Trim() + "','" + dgv["供应商名", i].Value.ToString().Trim() + "'," + PricePreTax + ",'" + UserID + "')";
                Lsql.Add(sqlInsert);
            }

            #region 事务批量添加数据
            SqlConnection con = new SqlConnection(GlobalSpace.FSDBConnstr);
            con.Open();
            SqlTransaction tran = con.BeginTransaction();//先实例SqlTransaction类，使用这个事务使用的是con 这个连接，使用BeginTransaction这个方法来开始执行这个事务
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.Transaction = tran;
            try
            {
                for (int i = 0; i < Lsql.Count; i++)
                {
                    cmd.CommandText = Lsql[i];
                    cmd.ExecuteNonQuery();
                }
                tran.Commit();
                MessageBoxEx.Show("全部添加完成！");
            }
            catch (Exception ex)
            {
                tran.Rollback();
                MessageBoxEx.Show("添加失败：" + ex.Message);
            }
            tran.Dispose();
            con.Close();
            #endregion
        }
        bool Checkbl = true;
        private void BtnAllSelect_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow dgvrow in dgv.Rows)
            {
                
                   dgvrow.Cells["Check"].Value= Checkbl;
                
            }
            Checkbl = !Checkbl;
        }
    }
}
