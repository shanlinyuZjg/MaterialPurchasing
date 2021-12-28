using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using Global.Helper;
using gregn6Lib;

namespace Global.Warehouse
{
    public partial class PrintPreview : Office2007Form
    {
        private GridppReport Report = new GridppReport();
        private DataRow DR;
        private DataTable DTRecord;
        string fileNumber = string.Empty;
        string printDate = string.Empty;
        int isPrint = 0;
        bool IsEBRM = false;
        bool IsBatchPrint = false;
        int Count = 0;
        public PrintPreview(DataTable dt, string grpFilePath,int count)
        {
            InitializeComponent();
            this.EnableGlass = false;
            Count = count;
            DTRecord = dt.Copy();
            Report.LoadFromFile(Application.StartupPath + grpFilePath);
            Report.DetailGrid.Recordset.ConnectionString = GlobalSpace.FSDBConnstr;
            Report.Initialize += new _IGridppReportEvents_InitializeEventHandler(ShowOrderOneByOne);
            Report.FetchRecord += new _IGridppReportEvents_FetchRecordEventHandler(ReportFetchRecord);
            this.axGRPrintViewer1.Report = Report;
        }

        private void ReportFetchRecord()
        {
            FillRecordToReport(Report, DTRecord);
            this.EnableGlass = false;
            MessageBoxEx.EnableGlass = false;
        }

        private struct MatchFieldPairType
        {
            public IGRField grField;
            public int MatchColumnIndex;
        }
        // 将 DataTable 的数据转储到 Grid++Report 的数据集中
        public static void FillRecordToReport(IGridppReport Report, DataTable dt)
        {
            MatchFieldPairType[] MatchFieldPairs = new MatchFieldPairType[Math.Min(Report.DetailGrid.Recordset.Fields.Count, dt.Columns.Count)];

            //根据字段名称与列名称进行匹配，建立DataReader字段与Grid++Report记录集的字段之间的对应关系
            int MatchFieldCount = 0;
            for (int i = 0; i < dt.Columns.Count; ++i)
            {
                foreach (IGRField fld in Report.DetailGrid.Recordset.Fields)
                {
                    if (String.Compare(fld.Name, dt.Columns[i].ColumnName, true) == 0)
                    {
                        MatchFieldPairs[MatchFieldCount].grField = fld;
                        MatchFieldPairs[MatchFieldCount].MatchColumnIndex = i;
                        ++MatchFieldCount;
                        break;
                    }
                }
            }


            // 将 DataTable 中的每一条记录转储到 Grid++Report 的数据集中去
            foreach (DataRow dr in dt.Rows)
            {
                Report.DetailGrid.Recordset.Append();

                for (int i = 0; i < MatchFieldCount; ++i)
                {
                    if (!dr.IsNull(MatchFieldPairs[i].MatchColumnIndex))
                        MatchFieldPairs[i].grField.Value = dr[MatchFieldPairs[i].MatchColumnIndex];
                }

                Report.DetailGrid.Recordset.Post();
            }
        }
        public void ShowOrder()
        {
            //文件编号固定格式：年度后2位+月份（2位）+030（仓库代码）+三位流水号。
            //1.判断该条记录是否已经存在，如果存在，则直接获取文件编号
            bool isExistRecord = false;
            string sqlExist = @"Select Count(Id) From PurchaseOrderBatchRecordByCMF Where Guid='" + DR["Guid"].ToString() + "'";
            if (SQLHelper.Exist(GlobalSpace.FSDBConnstr, sqlExist))
            {
                isExistRecord = true;
            }

            //     string fileNumber = string.Empty;
            if (isExistRecord)
            {
                string sqlSelectFileNumber = @"Select FileNumber From PurchaseOrderBatchRecordByCMF Where Guid='" + DR["Guid"].ToString() + "'";
                fileNumber = SQLHelper.ExecuteScalar(GlobalSpace.FSDBConnstr, sqlSelectFileNumber).ToString();
            }
            else
            {
                string sqlExistDay = @"Select Count(Id) From PurchaseOrderBatchRecordByCMF Where CreateDate='" + DateTime.Now.ToString("yyyy-MM-dd") + "'";
                //2.判断当天记录是否存在，如果存在，则获取最新一条记录的文件编号进行处理；不存在则产生当天的第一条记录。
                if (SQLHelper.Exist(GlobalSpace.FSDBConnstr, sqlExistDay))
                {
                    string sqlSelectLatest = @"Select FileNumber  From PurchaseOrderBatchRecordByCMF Where CreateDate='" + DateTime.Now.ToString("yyyy-MM-dd") + "' Order By Id Desc";
                    string lastFileNumber = SQLHelper.ExecuteScalar(GlobalSpace.FSDBConnstr, sqlSelectLatest).ToString();
                    int number = Convert.ToInt32(lastFileNumber.Substring(lastFileNumber.Length - 4, 4));
                    string newNumber = string.Empty;
                    if (number + 1 < 10)
                    {
                        newNumber = "000" + (number + 1).ToString();
                    }
                    else if (number + 1 < 100)
                    {
                        newNumber = "00" + (number + 1).ToString();
                    }
                    else if (number + 1 < 1000)
                    {
                        newNumber = "0" + (number + 1).ToString();
                    }
                    else
                    {
                        newNumber = (number + 1).ToString();
                    }
                    fileNumber = DateTime.Now.ToString("yyMM") + "003" + newNumber;
                    /*
                    string sqlInsert = @"Insert Into PurchaseOrderBatchRecordByCMF (Guid,FileNumber) Values('" + DR["Guid"].ToString() + "','" + fileNumber + "')";
                    if (!SQLHelper.ExecuteNonQuery(GlobalSpace.FSDBConnstr, sqlInsert))
                    {
                        MessageBoxEx.Show("生成记录编号时出错！", "提示");
                        return;
                    }*/
                }
                else
                {
                  /*  string sqlInsert = @"Insert Into PurchaseOrderBatchRecordByCMF (Guid,FileNumber) Values('" + DR["Guid"].ToString() + "','" + DateTime.Now.ToString("yyMM") + "0300001" + "')";
                    if (!SQLHelper.ExecuteNonQuery(GlobalSpace.FSDBConnstr, sqlInsert))
                    {
                        MessageBoxEx.Show("生成本日第一条记录编号时出错！", "提示");
                        return;
                    }*/
                    fileNumber = DateTime.Now.ToString("yyMM") + "0030001";
                }
            }
            if(string.IsNullOrEmpty(printDate))
            {
                Report.ParameterByName("ApplyDate").AsString = DR["ApplyDate"].ToString().Replace("-", ".");
            }
            else
            {
                Report.ParameterByName("ApplyDate").AsString = printDate;
            }
            //格式：  Report.ParameterByName("生产商代码").AsString =  strMNumber;       
            Report.ParameterByName("FileNumber").AsString = fileNumber;         
            Report.ParameterByName("ItemNumber").AsString = DR["ItemNumber"].ToString();
            Report.ParameterByName("ItemDescription").AsString = DR["ItemDescription"].ToString();
            Report.ParameterByName("VendorLotNumber").AsString = DR["VendorLotNumber"].ToString();
            Report.ParameterByName("InternalLotNumber").AsString = DR["InternalLotNumber"].ToString();
            Report.ParameterByName("MfgName").AsString = DR["MfgName"].ToString();
            Report.ParameterByName("VendorName").AsString = DR["VendorName"].ToString();
            Report.ParameterByName("Packages").AsString = DR["PackageQuantity"].ToString();
            Report.ParameterByName("UM").AsString = "总标示值（" + DR["UM"].ToString() + "）";
            Report.ParameterByName("PackageOddUM").AsString = "零头标示值（" + DR["UM"].ToString() + "）";
            Report.ParameterByName("PackageUM").AsString = "整包件数（" + DR["PackageUM"].ToString() + "）";
            Report.ParameterByName("Quantity").AsString = DR["Quantity"].ToString();
            Report.ParameterByName("PackageSpecification").AsString = DR["PackageSpecification"].ToString();
            Report.ParameterByName("PackageOdd").AsString = DR["PackageOdd"].ToString();
            Report.ParameterByName("MfgDate").AsString = DR["MfgDate"].ToString();
            Report.ParameterByName("ExpiredDate").AsString = DR["ExpiredDate"].ToString();
            Report.ParameterByName("FileTracedNumber").AsString = StockUser.FileTracedNumber;
            Report.ParameterByName("FileEdition").AsString = StockUser.FileEdition;
            Report.ParameterByName("EffectiveDate").AsString = StockUser.EffectiveDate;
            Report.ParameterByName("FONumber").AsString = DR["FONumber"].ToString();
        }
        public void ShowOrderDt()
        {
          
            //格式：  Report.ParameterByName("生产商代码").AsString =  strMNumber;       
            double totalQuantity = 0;
            double totalOdd = 0;
            foreach(DataRow dr in DTRecord.Rows)
            {
                DR = dr;
                totalOdd += Convert.ToDouble(DR["PackageOdd"]);
                totalQuantity += Convert.ToDouble(DR["Quantity"]);
            }
            if (string.IsNullOrEmpty(printDate))
            {
                Report.ParameterByName("ApplyDate").AsString = DR["ApplyDate"].ToString().Replace("-", ".");
            }
            else
            {
                Report.ParameterByName("ApplyDate").AsString = printDate;
            }
            Report.ParameterByName("FileNumber").AsString = fileNumber;
            Report.ParameterByName("ItemNumber").AsString = DR["ItemNumber"].ToString();
            Report.ParameterByName("ItemDescription").AsString = DR["ItemDescription"].ToString();
            Report.ParameterByName("VendorLotNumber").AsString = DR["VendorLotNumber"].ToString();
            Report.ParameterByName("InternalLotNumber").AsString = DR["InternalLotNumber"].ToString();
            Report.ParameterByName("MfgName").AsString = DR["MfgName"].ToString();
            Report.ParameterByName("VendorName").AsString = DR["VendorName"].ToString();
            Report.ParameterByName("Packages").AsString = DR["PackageQuantity"].ToString();
            Report.ParameterByName("UM").AsString = "总标示值（" + DR["UM"].ToString() + "）";
            Report.ParameterByName("PackageOddUM").AsString = "零头标示值（" + DR["UM"].ToString() + "）";
            Report.ParameterByName("PackageUM").AsString = "整包件数（" + DR["PackageUM"].ToString() + "）";
            Report.ParameterByName("Quantity").AsString = totalQuantity.ToString();// DR["Quantity"].ToString();
            Report.ParameterByName("PackageSpecification").AsString = DR["PackageSpecification"].ToString();
            Report.ParameterByName("PackageOdd").AsString = totalOdd.ToString(); //DR["PackageOdd"].ToString();
            Report.ParameterByName("MfgDate").AsString = DR["MfgDate"].ToString();
            Report.ParameterByName("ExpiredDate").AsString = DR["ExpiredDate"].ToString();
            Report.ParameterByName("FileTracedNumber").AsString = StockUser.FileTracedNumber;
            Report.ParameterByName("FileEdition").AsString = StockUser.FileEdition;
            Report.ParameterByName("EffectiveDate").AsString = StockUser.EffectiveDate;
            Report.ParameterByName("FONumber").AsString = DR["FONumber"].ToString();
        }
        public void ShowOrderMEBR()
        {
            //格式：  Report.ParameterByName("生产商代码").AsString =  strMNumber;       
            double totalQuantity = 0;
            double totalOdd = 0;
            foreach (DataRow dr in DTRecord.Rows)
            {
                DR = dr;
                totalOdd += Convert.ToDouble(DR["PackageOdd"]);
                totalQuantity += Convert.ToDouble(DR["Quantity"]);
            }
            if (string.IsNullOrEmpty(printDate))
            {
                Report.ParameterByName("ApplyDate").AsString = DR["ApplyDate"].ToString().Replace("-", ".");
            }
            else
            {
                Report.ParameterByName("ApplyDate").AsString = printDate;
            }
            Report.ParameterByName("FileNumber").AsString = DR["FileNumber"].ToString(); ;
            Report.ParameterByName("ItemNumber").AsString = DR["ItemNumber"].ToString();
            Report.ParameterByName("ItemDescription").AsString = DR["ItemDescription"].ToString();
            Report.ParameterByName("VendorLotNumber").AsString = DR["VendorLotNumber"].ToString();
            Report.ParameterByName("InternalLotNumber").AsString = DR["InternalLotNumber"].ToString();
            Report.ParameterByName("MfgName").AsString = DR["MfgName"].ToString();
            Report.ParameterByName("VendorName").AsString = DR["VendorName"].ToString();
            Report.ParameterByName("Packages").AsString = DR["PackageQuantity"].ToString();
            Report.ParameterByName("UM").AsString = "总标示值（" + DR["UM"].ToString() + "）";
            Report.ParameterByName("PackageOddUM").AsString = "零头标示值（" + DR["UM"].ToString() + "）";
            Report.ParameterByName("PackageUM").AsString = "整包件数（" + DR["PackageUM"].ToString() + "）";
            Report.ParameterByName("Quantity").AsString = totalQuantity.ToString();// DR["Quantity"].ToString();
            Report.ParameterByName("PackageSpecification").AsString = DR["PackageSpecification"].ToString();
            Report.ParameterByName("PackageOdd").AsString = totalOdd.ToString(); //DR["PackageOdd"].ToString();
            Report.ParameterByName("MfgDate").AsString = DR["MfgDate"].ToString();
            Report.ParameterByName("ExpiredDate").AsString = DR["ExpiredDate"].ToString();
            Report.ParameterByName("FileTracedNumber").AsString = StockUser.FileTracedNumber;
            Report.ParameterByName("FileEdition").AsString = StockUser.FileEdition;
            Report.ParameterByName("EffectiveDate").AsString = StockUser.EffectiveDate;
            Report.ParameterByName("FONumber").AsString = DR["FONumber"].ToString();
        }
        //批量打印用
        public void ShowOrderBatchPrint()
        {
            //格式：  Report.ParameterByName("生产商代码").AsString =  strMNumber;       
            double totalQuantity = 0;
            double totalOdd = 0;
            foreach (DataRow dr in DTRecord.Rows)
            {
                DR = dr;
                DataRow[] drs = DTRecord.Select("FileNumber='" + DR["FileNumber"].ToString() + "'");
                if(drs.Length > 1)
                {
                    for(int i=0;i < drs.Length;i++)
                    {
                        totalOdd += Convert.ToDouble(drs[i]["PackageOdd"]);
                        totalQuantity += Convert.ToDouble(drs[i]["Quantity"]);
                    }
                }
                else
                {
                    totalOdd = Convert.ToDouble(DR["PackageOdd"]);
                    totalQuantity = Convert.ToDouble(DR["Quantity"]);
                }
                Report.ParameterByName("ApplyDate").AsString = DR["ApplyDate"].ToString().Replace("-", ".");
                Report.ParameterByName("FileNumber").AsString = DR["FileNumber"].ToString();
                Report.ParameterByName("ItemNumber").AsString = DR["ItemNumber"].ToString();
                Report.ParameterByName("ItemDescription").AsString = DR["ItemDescription"].ToString();
                Report.ParameterByName("VendorLotNumber").AsString = DR["VendorLotNumber"].ToString();
                Report.ParameterByName("InternalLotNumber").AsString = DR["InternalLotNumber"].ToString();
                Report.ParameterByName("MfgName").AsString = DR["MfgName"].ToString();
                Report.ParameterByName("VendorName").AsString = DR["VendorName"].ToString();
                Report.ParameterByName("Packages").AsString = DR["PackageQuantity"].ToString();
                Report.ParameterByName("UM").AsString = "总标示值（" + DR["UM"].ToString() + "）";
                Report.ParameterByName("PackageOddUM").AsString = "零头标示值（" + DR["UM"].ToString() + "）";
                Report.ParameterByName("PackageUM").AsString = "整包件数（" + DR["PackageUM"].ToString() + "）";
                Report.ParameterByName("Quantity").AsString = totalQuantity.ToString();// DR["Quantity"].ToString();
                Report.ParameterByName("PackageSpecification").AsString = DR["PackageSpecification"].ToString();
                Report.ParameterByName("PackageOdd").AsString = totalOdd.ToString(); //DR["PackageOdd"].ToString();
                Report.ParameterByName("MfgDate").AsString = DR["MfgDate"].ToString();
                Report.ParameterByName("ExpiredDate").AsString = DR["ExpiredDate"].ToString();
                Report.ParameterByName("FileTracedNumber").AsString = StockUser.FileTracedNumber;
                Report.ParameterByName("FileEdition").AsString = StockUser.FileEdition;
                Report.ParameterByName("EffectiveDate").AsString = StockUser.EffectiveDate;
                Report.ParameterByName("FONumber").AsString = DR["FONumber"].ToString();
                this.axGRPrintViewer1.Refresh();
                this.axGRPrintViewer1.Print(false);
            }
        }
        public void ShowOrderOneByOne()
        {
         
            DR = DTRecord.Rows[Count];
            Report.ParameterByName("ApplyDate").AsString = DateTime.Now.ToString("yyyy-MM-dd");
            Report.ParameterByName("PayForm").AsString = DR["付款方式"].ToString();
            Report.ParameterByName("SNumber").AsString = DR["序号"].ToString();
            Report.ParameterByName("DeptOrgCode").AsString = DR["组织代码"].ToString();
            Report.ParameterByName("BusinessUnit").AsString = DR["业务单位"].ToString();
            Report.ParameterByName("BudgetProject").AsString = DR["预算工程"].ToString();
            Report.ParameterByName("SubProject").AsString = DR["项目"].ToString();
            Report.ParameterByName("ProjectCode").AsString = DR["工程代码"].ToString();
            Report.ParameterByName("Budgeted").AsString = DR["预算类型"].ToString();
            if (DR["计划内"].ToString() == "是")
            {
                Report.ParameterByName("Planned").AsString = "计划内";
            }
            else
            {
                Report.ParameterByName("Planned").AsString = "计划外";
            }
            Report.ParameterByName("VendorNumber").AsString = DR["供应商码"].ToString();
            Report.ParameterByName("VendorName").AsString = DR["供应商名"].ToString();
            Report.ParameterByName("ContractNumber").AsString = DR["合同编码"].ToString();
            Report.ParameterByName("BankName").AsString = DR["银行"].ToString();
            Report.ParameterByName("BankAccount").AsString = DR["银行账户"].ToString();
            Report.ParameterByName("ItemNumber").AsString = DR["物料代码"].ToString();
            Report.ParameterByName("ItemDescription").AsString = DR["物料描述"].ToString();
            Report.ParameterByName("ItmUM").AsString = DR["单位"].ToString();
            Report.ParameterByName("Quantity").AsString = DR["数量"].ToString();
            Report.ParameterByName("Price").AsString = DR["单价"].ToString();
            Report.ParameterByName("ItemAmount").AsString = DR["物料总金额"].ToString();
            Report.ParameterByName("PaymentType").AsString = DR["类型"].ToString();
            Report.ParameterByName("PaymentDetail").AsString = DR["计划内容"].ToString();
            Report.ParameterByName("TotalAmount").AsString = DR["总金额"].ToString();
            Report.ParameterByName("CapitalForm").AsString = DR["金额大写"].ToString();
        }

        //获取到最新流水号
        private string GetFileTracedNumber()
        {
            string s = string.Empty;


            return s;
        }

        private void SetFileTracedNumber()
        {

        }

        private void PrintPO_Load(object sender, EventArgs e)
        {
            this.axGRPrintViewer1.Refresh();
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            if(isPrint == 2)
            {
                this.axGRPrintViewer1.Print(true);
            }
            else
            {
                MessageBoxEx.Show("请先进行确认！", "提示");
            }          
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            this.axGRPrintViewer1.Refresh();
        }

        private void btnConfirm_Click1(object sender, EventArgs e)
        {
            //文件编号固定格式：WHO:0000001,其余厂区：年度后2位+月份（2位）+003（仓库代码）+四位流水号，同时每个月从0001开始。   
           
            fileNumber = GetFileNumber(StockUser.Stock);
            List<string> guidList = new List<string>();
            string sqlSelectGuid = @"Select Guid From EBR_ReceiveRecordForInspect";
            guidList = SQLHelper.GetList(GlobalSpace.EBRConnStr, sqlSelectGuid, "Guid");
            int iCount = 0;
            List<string> sqlList = new List<string>();
            foreach(DataRow dr in DTRecord.Rows)
            {
                if(guidList.Contains(dr["Guid"].ToString()))
                {
                    iCount++;
                }
                else
                {
                    string sqlInsert = @"INSERT INTO EBR_ReceiveRecordForInspect  (
	                                                                    [PONumber],
	                                                                    [VendorName],
	                                                                    [ManufacturerName],
	                                                                    [LineNumber],
	                                                                    [ItemNumber],
	                                                                    [ItemDescription],
	                                                                    [LineUM],
	                                                                    [ReceiveQuantity],
	                                                                    [LotNumber],
	                                                                    [InternalLotNumber],
	                                                                    [ExpiredDate],
	                                                                    [Operator],
	                                                                    [ForeignNumber],
	                                                                    [Guid],
	                                                                    [ManufacturedDate],
	                                                                    [FileEdition],
	                                                                    [FileNumber],
	                                                                    [FileTracedNumber],[PackageQuantity],PackageUM,PackageOdd,PackageSpecification
                                                                    )
                                                                    VALUES
	                                                                    ( '" + dr["PONumber"].ToString() + "','" + dr["VendorName"].ToString() + "','" + dr["MfgName"].ToString() + "','" + dr["LineNumber"].ToString() + "','" + dr["ItemNumber"].ToString() + "','" + dr["ItemDescription"].ToString().Replace("'", "''") + "','" + dr["UM"].ToString() + "','" + Convert.ToDouble(dr["Quantity"]) + "','" + dr["VendorLotNumber"].ToString() + "','" + dr["InternalLotNumber"].ToString() + "','" + dr["ExpiredDate"].ToString() + "','" + StockUser.UserName + "','" + dr["FONumber"].ToString() + "','" + dr["Guid"].ToString() + "','" + dr["MfgDate"].ToString() + "','" + StockUser.FileEdition + "','" + fileNumber + "','" + StockUser.FileTracedNumber + "','" + dr["PackageQuantity"].ToString() + "','" + dr["PackageUM"].ToString() + "','" + dr["PackageOdd"].ToString() + "','" + dr["PackageSpecification"].ToString() + "')";
                    sqlList.Add(sqlInsert);
                }
            }
            
            if(iCount > 0)
            {
                MessageBoxEx.Show("记录中存在已写入信息，只对未写入的处理！", "提示");
            }

            if (SQLHelper.BatchExecuteNonQuery(GlobalSpace.EBRConnStr,sqlList))
            {
                isPrint = 2;
                this.axGRPrintViewer1.Refresh();
                ShowOrderDt();
                MessageBoxEx.Show("确认成功，请进行打印！", "提示");
            }
            else
            {
                MessageBoxEx.Show("确认失败！", "提示");
            }            
        }

        private string GetFileNumber(string area)
        {
            string filenumber = string.Empty;
            /******
             * WHO厂区的格式暂不启用
             * 
             */
            /*      if(area == "WHO")
                  {
                      // 文件编号固定格式：年度后2位 + 月份（2位）+003（仓库代码）+四位流水号，每个月1号开始从-0001开始。   
                  string sqlExistDay = @"Select Count(Id) From EBR_ReceiveRecordForInspect";
                      //2.判断当天记录是否存在，如果存在，则获取最新一条记录的文件编号进行处理；不存在则产生当天的第一条记录。
                      if (SQLHelper.Exist(GlobalSpace.EBRConnStr, sqlExistDay))
                      {
                          string sqlSelectLatest = @"Select FileNumber  From EBR_ReceiveRecordForInspect Order By Id Desc";
                          string lastFileNumber = SQLHelper.ExecuteScalar(GlobalSpace.EBRConnStr, sqlSelectLatest).ToString();
                          int number = Convert.ToInt32(lastFileNumber);
                          string newNumber = string.Empty;
                          if (number + 1 < 10)
                          {
                              newNumber = "000000" + (number + 1).ToString();
                          }
                          else if (number + 1 < 100)
                          {
                              newNumber = "00000" + (number + 1).ToString();
                          }
                          else if (number + 1 < 1000)
                          {
                              newNumber = "0000" + (number + 1).ToString();
                          }
                          else if (number + 1 < 10000)
                          {
                              newNumber = "000" + (number + 1).ToString();
                          }
                          else if (number + 1 < 100000)
                          {
                              newNumber = "00" + (number + 1).ToString();
                          }
                          else if (number + 1 < 1000000)
                          {
                              newNumber = "0" + (number + 1).ToString();
                          }
                          else
                          {
                              newNumber = (number + 1).ToString();
                          }
                          filenumber =  newNumber;                   
                      }
                      else
                      {
                          filenumber =  "0000001";
                      }
                  }
                  else
                  {*/
            // 文件编号固定格式：年度后2位 + 月份（2位）+003（仓库代码）+四位流水号，每个月1号开始从0001开。   

            if (DateTime.Now.ToString("dd") == "01")
            {
                string sqlSelectExist = @"Select Count(Id) From EBR_ReceiveRecordForInspect Where ApplyDate='" + DateTime.Now.ToString("yyyy-MM-dd") + "' And FileTracedNumber='" + StockUser.FileTracedNumber + "' And FileEdition='" + StockUser.FileEdition + "'";
                if (!SQLHelper.Exist(GlobalSpace.EBRConnStr, sqlSelectExist))
                {
                    filenumber = DateTime.Now.ToString("yyMM") + "0030001";
                }
                else
                {
                    string sqlSelectLatest = @"Select FileNumber  From EBR_ReceiveRecordForInspect Where  FileTracedNumber='" + StockUser.FileTracedNumber + "' And FileEdition='" + StockUser.FileEdition + "'  Order By Id Desc";
                    string lastFileNumber = SQLHelper.ExecuteScalar(GlobalSpace.EBRConnStr, sqlSelectLatest).ToString();
                    int number = Convert.ToInt32(lastFileNumber.Substring(lastFileNumber.Length - 4, 4));
                    string newNumber = string.Empty;
                    if (number + 1 < 10)
                    {
                        newNumber = "000" + (number + 1).ToString();
                    }
                    else if (number + 1 < 100)
                    {
                        newNumber = "00" + (number + 1).ToString();
                    }
                    else if (number + 1 < 1000)
                    {
                        newNumber = "0" + (number + 1).ToString();
                    }
                    else
                    {
                        newNumber = (number + 1).ToString();
                    }
                    filenumber = DateTime.Now.ToString("yyMM") + "003" + newNumber;
                }
            }
            else
            {
                string sqlExistDay = @"Select Count(Id) From EBR_ReceiveRecordForInspect Where  FileTracedNumber='" + StockUser.FileTracedNumber + "' And FileEdition='" + StockUser.FileEdition + "'";
                //2.判断当天记录是否存在，如果存在，则获取最新一条记录的文件编号进行处理；不存在则产生当天的第一条记录。
                if (SQLHelper.Exist(GlobalSpace.EBRConnStr, sqlExistDay))
                {
                    string sqlSelectLatest = @"Select FileNumber  From EBR_ReceiveRecordForInspect Where  FileTracedNumber='" + StockUser.FileTracedNumber + "' And FileEdition='" + StockUser.FileEdition + "'  Order By FileNumber Desc";
                    string lastFileNumber = SQLHelper.ExecuteScalar(GlobalSpace.EBRConnStr, sqlSelectLatest).ToString();
                    int number = Convert.ToInt32(lastFileNumber.Substring(lastFileNumber.Length - 4, 4));
                    string newNumber = string.Empty;
                    if (number + 1 < 10)
                    {
                        newNumber = "000" + (number + 1).ToString();
                    }
                    else if (number + 1 < 100)
                    {
                        newNumber = "00" + (number + 1).ToString();
                    }
                    else if (number + 1 < 1000)
                    {
                        newNumber = "0" + (number + 1).ToString();
                    }
                    else
                    {
                        newNumber = (number + 1).ToString();
                    }
                    filenumber = DateTime.Now.ToString("yyMM") + "003" + newNumber;
                }
                else
                {
                    filenumber = DateTime.Now.ToString("yyMM") + "0030001";
                }
            }
    //   }
            return filenumber;
        }

        private void btnBatchPrint_Click(object sender, EventArgs e)
        {
            ShowOrderBatchPrint();
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            Count++;
            if(Count == DTRecord.Rows.Count)
            {
                MessageBoxEx.Show("已到最后一条！", "提示");
                return;
            }
            this.axGRPrintViewer1.Refresh();
            ShowOrderOneByOne();
        }
    }
}
