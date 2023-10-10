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
    public partial class PrintBatchRecord : Office2007Form
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
        bool IsSaved = false;//记录是否已保存
        public PrintBatchRecord(DataRow dr, string grpFilePath)
        {
            //PrintPORecord.grf
            InitializeComponent();
            this.EnableGlass = false;
            DR = dr;
            Report.LoadFromFile(Application.StartupPath + grpFilePath);
            Report.DetailGrid.Recordset.ConnectionString = GlobalSpace.FSDBConnstr;
            Report.Initialize += new _IGridppReportEvents_InitializeEventHandler(ShowOrder);
            //        Report.FetchRecord += new _IGridppReportEvents_FetchRecordEventHandler(ReportFetchRecord);
            this.axGRPrintViewer1.Report = Report;
        }
        public PrintBatchRecord(DataTable dt, string grpFilePath)
        {
            //PrintPORecord.grf
            InitializeComponent();
            this.EnableGlass = false;
            DTRecord = dt.Copy();
            Report.LoadFromFile(Application.StartupPath + grpFilePath);
            Report.DetailGrid.Recordset.ConnectionString = GlobalSpace.FSDBConnstr;
            Report.Initialize += new _IGridppReportEvents_InitializeEventHandler(ShowOrderDt);
            //        Report.FetchRecord += new _IGridppReportEvents_FetchRecordEventHandler(ReportFetchRecord);
            this.axGRPrintViewer1.Report = Report;
        }
        public PrintBatchRecord(Boolean isEBRM,DataTable dt, string grpFilePath)
        {
            //PrintPORecord.grf
            InitializeComponent();
            this.EnableGlass = false;
            DTRecord = dt.Copy();
            Report.LoadFromFile(Application.StartupPath + grpFilePath);
            Report.DetailGrid.Recordset.ConnectionString = GlobalSpace.FSDBConnstr;
            Report.Initialize += new _IGridppReportEvents_InitializeEventHandler(ShowOrderMEBR);
            //        Report.FetchRecord += new _IGridppReportEvents_FetchRecordEventHandler(ReportFetchRecord);
            this.axGRPrintViewer1.Report = Report;
            IsEBRM = isEBRM;
            isPrint = 2;
        }
        public PrintBatchRecord(DataTable dt, string grpFilePath, int count, int finished,bool Saved)
        {
            InitializeComponent();
            this.EnableGlass = false;
            this.IsSaved = Saved;
            Count = count;
            DTRecord = dt.Copy();
            Report.LoadFromFile(Application.StartupPath + grpFilePath);
            Report.DetailGrid.Recordset.ConnectionString = GlobalSpace.FSDBConnstr;
            Report.Initialize += new _IGridppReportEvents_InitializeEventHandler(ShowOrderOneByOne);
            this.axGRPrintViewer1.Report = Report;
        }
        private void ReportFetchRecord()
        {
            //     FillRecordToReport(Report, DT);
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
        /*       public void ShowOrderOneByOne()
               {
                   //格式：  Report.ParameterByName("生产商代码").AsString =  strMNumber;       
           //        double totalQuantity = 0;
              //     double totalOdd = 0;

                   DR = DTRecord.Rows[Count];
                   Report.ParameterByName("ApplyDate").AsString = DR["ApplyDate"].ToString().Replace("-", ".");
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
                   Report.ParameterByName("Quantity").AsString =  DR["Quantity"].ToString();
                   Report.ParameterByName("PackageSpecification").AsString = DR["PackageSpecification"].ToString();
                   Report.ParameterByName("PackageOdd").AsString = DR["PackageOdd"].ToString();
                   Report.ParameterByName("MfgDate").AsString = DR["MfgDate"].ToString();
                   Report.ParameterByName("ExpiredDate").AsString = DR["ExpiredDate"].ToString();
                   Report.ParameterByName("FileTracedNumber").AsString = StockUser.FileTracedNumber;
                   Report.ParameterByName("FileEdition").AsString = StockUser.FileEdition;
                   Report.ParameterByName("EffectiveDate").AsString = StockUser.EffectiveDate;
                   Report.ParameterByName("FONumber").AsString = DR["FONumber"].ToString();
               }
        */
        public void ShowOrderOneByOne()
        {
            //格式：  Report.ParameterByName("生产商代码").AsString =  strMNumber;       
            //        double totalQuantity = 0;
            //     double totalOdd = 0;

            DR = DTRecord.Rows[Count];


            //           totalOdd += Convert.ToDouble(DR["PackageOdd"]);
            //           totalQuantity += Convert.ToDouble(DR["Quantity"]);
            //      if (string.IsNullOrEmpty(printDate))
            //        {
            Report.ParameterByName("ApplyDate").AsString = DR["ApplyDate"].ToString().Replace("-", ".");
            //         }
            /*          else
                      {
                          Report.ParameterByName("ApplyDate").AsString = printDate;
                      }*/
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
            Report.ParameterByName("PackageUM").AsString = "整包件数（" + (DR["PackageUM"].ToString().Trim().Length == 0 ? "  " : DR["PackageUM"].ToString().Trim()) + "）";
            Report.ParameterByName("Quantity").AsString = DR["Quantity"].ToString();
            Report.ParameterByName("PackageSpecification").AsString = DR["PackageSpecification"].ToString().Trim();
            Report.ParameterByName("PackageOdd").AsString = DR["PackageOdd"].ToString();

            if (DR["MfgDate"] == DBNull.Value)
            { Report.ParameterByName("ywMfgDate").AsString = "生产日期（□有☑无）"; }
            else if (string.IsNullOrWhiteSpace(DR["MfgDate"].ToString()))
            { Report.ParameterByName("ywMfgDate").AsString = "生产日期（□有☑无）"; }
            else
            {
                Report.ParameterByName("MfgDate").AsString = DR["MfgDate"].ToString().Trim().Replace("-", ".");
                Report.ParameterByName("ywMfgDate").AsString = "生产日期（☑有□无）";
            }

            if (DR["ExpiredDate"] == DBNull.Value)
            { Report.ParameterByName("ywExpiredDate").AsString = "有效期/复验期（□有☑无）"; }
            else if (string.IsNullOrWhiteSpace(DR["ExpiredDate"].ToString()))
            { Report.ParameterByName("ywExpiredDate").AsString = "有效期/复验期（□有☑无）"; }
            else
            {
                Report.ParameterByName("ExpiredDate").AsString = DR["ExpiredDate"].ToString().Trim().Replace("-", ".");
                Report.ParameterByName("ywExpiredDate").AsString = "有效期/复验期（☑有□无）";
            }
            if (IsSaved)
            {
                Report.ParameterByName("FileTracedNumber").AsString = DR["FileTracedNumber"].ToString().Trim();
                Report.ParameterByName("FileEdition").AsString = DR["FileEdition"].ToString().Trim();
                Report.ParameterByName("EffectiveDate").AsString = DR["EffectiveDate"].ToString().Trim();
            }
            else
            {
                Report.ParameterByName("FileTracedNumber").AsString = StockUser.FileTracedNumber;
                Report.ParameterByName("FileEdition").AsString = StockUser.FileEdition;
                Report.ParameterByName("EffectiveDate").AsString = StockUser.EffectiveDate;
            }
            String Gang = "-";
            Report.ParameterByName("FONumber").AsString = DR["FONumber"] == DBNull.Value ? Gang : (String.IsNullOrWhiteSpace(DR["FONumber"].ToString()) ? Gang : DR["FONumber"].ToString());
            //新增字段
            //if (DR["ReceiverAPP"] != DBNull.Value)  //手写
            //    Report.ParameterByName("ReceiverAPP").AsString = DR["ReceiverAPP"].ToString();
            //if (DR["Checker"] != DBNull.Value)
            //    Report.ParameterByName("Checker").AsString = DR["Checker"].ToString();

            if (DR["QualityCheckStandard"] != DBNull.Value)
                Report.ParameterByName("QualityCheckStandard").AsString = DR["QualityCheckStandard"].ToString().Trim();

            if (DR["Conclusion"] == DBNull.Value)
            {
                Report.ParameterByName("Conclusion").AsString = "结论： □接收入库  □退回供应商  □其他：______________________________________________________";
            }
            else if (DR["Conclusion"].ToString() == "接收入库")
            {
                Report.ParameterByName("Conclusion").AsString = "结论： ☑接收入库  □退回供应商  □其他：______________________________________________________";
            }
            else if (DR["Conclusion"].ToString() == "退回供应商")
            {
                Report.ParameterByName("Conclusion").AsString = "结论： □接收入库  ☑退回供应商  □其他：______________________________________________________";
            }
            else if (DR["Conclusion"].ToString() == "其他")
            {
                Report.ParameterByName("Conclusion").AsString = "结论： □接收入库  □退回供应商  ☑其他：" + (DR["ConclusionText"] == DBNull.Value ? "" : DR["ConclusionText"].ToString().Trim());
            }
            else
            { Report.ParameterByName("Conclusion").AsString = "结论： □接收入库  □退回供应商  □其他：______________________________________________________"; }
            int IsAnyDeviationBJ = 0;
            if (DR["IsAnyDeviation"] == DBNull.Value)
            {
                Report.ParameterByName("IsAnyDeviation").AsString = "物料验收过程是否出现偏差（□是  □否）";
            }
            else if (DR["IsAnyDeviation"].ToString() == "是")
            {
                Report.ParameterByName("IsAnyDeviation").AsString = "物料验收过程是否出现偏差（☑是  □否）";
            }
            else if (DR["IsAnyDeviation"].ToString() == "否")
            {
                Report.ParameterByName("IsAnyDeviation").AsString = "物料验收过程是否出现偏差（□是  ☑否）";
                IsAnyDeviationBJ = 1;
            }
            else
            {
                Report.ParameterByName("IsAnyDeviation").AsString = "物料验收过程是否出现偏差（□是  □否）";
            }
            if (IsAnyDeviationBJ == 1)
            {
                Report.ParameterByName("DeviationNumber").AsString = Gang;
                Report.ParameterByName("deviationIsClosed").AsString = "偏差是否已处理关闭（☒是  ☒否）";
            }
            else
            {
                Report.ParameterByName("DeviationNumber").AsString = DR["DeviationNumber"] == DBNull.Value ? "" : DR["DeviationNumber"].ToString().Trim();

                if (DR["deviationIsClosed"] == DBNull.Value)
                {
                    Report.ParameterByName("deviationIsClosed").AsString = "偏差是否已处理关闭（□是   □否）";
                }
                else if (DR["deviationIsClosed"].ToString() == "是")
                {
                    Report.ParameterByName("deviationIsClosed").AsString = "偏差是否已处理关闭（☑是  □否）";
                }
                else if (DR["deviationIsClosed"].ToString() == "否")
                {
                    Report.ParameterByName("deviationIsClosed").AsString = "偏差是否已处理关闭（□是  ☑否）";
                }
                else
                {
                    Report.ParameterByName("deviationIsClosed").AsString = "偏差是否已处理关闭（□是  □否）";
                }
            }
            int IsReportBJ = 0;
            if (DR["IsReport"] == DBNull.Value)
            {
                Report.ParameterByName("IsReport").AsString = "以上如有问题是否已向质量管理部门报告   （□是  □否  □不需报告）";
            }
            else if (DR["IsReport"].ToString() == "是")
            {
                Report.ParameterByName("IsReport").AsString = "以上如有问题是否已向质量管理部门报告   （☑是  □否  □不需报告）";
            }
            else if (DR["IsReport"].ToString() == "否")
            {
                Report.ParameterByName("IsReport").AsString = "以上如有问题是否已向质量管理部门报告   （□是  ☑否  □不需报告）";
            }
            else if (DR["IsReport"].ToString() == "不需报告")
            {
                Report.ParameterByName("IsReport").AsString = "以上如有问题是否已向质量管理部门报告   （□是  □否  ☑不需报告）";
                IsReportBJ = 1;
            }
            else
            {
                Report.ParameterByName("IsReport").AsString = "以上如有问题是否已向质量管理部门报告   （□是  □否  □不需报告）";
            }
            if (IsReportBJ == 1)
            {
                Report.ParameterByName("QualityManageIdea").AsString = Gang;
                Report.ParameterByName("Sign").AsString = Gang;
                Report.ParameterByName("SignDate").AsString = Gang;
            }
            else
            {
                Report.ParameterByName("QualityManageIdea").AsString = DR["QualityManageIdea"] == DBNull.Value ? "" : DR["QualityManageIdea"].ToString().Trim();
                Report.ParameterByName("Sign").AsString = DR["Sign"] == DBNull.Value ? "" : DR["Sign"].ToString().Trim();
                Report.ParameterByName("SignDate").AsString = DR["SignDate"] == DBNull.Value ? "" : DR["SignDate"].ToString().Trim().Replace("-", ".");
            }
            int IsRequireCleanBJ = 0;
            if (DR["IsRequireClean"] == DBNull.Value)
            {
                Report.ParameterByName("IsRequireClean").AsString = "外包装是否清洁\r\n（□是   □否）";
            }
            else if (DR["IsRequireClean"].ToString() == "是")
            {
                Report.ParameterByName("IsRequireClean").AsString = "外包装是否清洁\r\n（☑是   □否）";
            }
            else if (DR["IsRequireClean"].ToString() == "否")
            {
                Report.ParameterByName("IsRequireClean").AsString = "外包装是否清洁\r\n（□是   ☑否）";
                IsRequireCleanBJ = 1;
            }
            else
            {
                Report.ParameterByName("IsRequireClean").AsString = "外包装是否清洁\r\n（□是   □否）";
            }
            if (IsRequireCleanBJ == 1)
            {
                Report.ParameterByName("PollutionSituation").AsString = Gang;
                Report.ParameterByName("CleanMethod").AsString = Gang;
            }
            else
            {
                Report.ParameterByName("PollutionSituation").AsString = DR["PollutionSituation"] == DBNull.Value ? "" : DR["PollutionSituation"].ToString().Trim();
                Report.ParameterByName("CleanMethod").AsString = DR["CleanMethod"] == DBNull.Value ? "" : DR["CleanMethod"].ToString().Trim();
            }
            int IsCompleteBJ = 0;
            if (DR["IsComplete"] == DBNull.Value)
            {
                Report.ParameterByName("IsComplete").AsString = "外包装是否完整\r\n（□是   □否）";
            }
            else if (DR["IsComplete"].ToString() == "是")
            {
                Report.ParameterByName("IsComplete").AsString = "外包装是否完整\r\n（☑是   □否）";
                IsCompleteBJ = 1;
            }
            else if (DR["IsComplete"].ToString() == "否")
            {
                Report.ParameterByName("IsComplete").AsString = "外包装是否完整\r\n（□是   ☑否）";
            }
            else
            {
                Report.ParameterByName("IsComplete").AsString = "外包装是否完整\r\n（□是   □否）";
            }
            if (IsCompleteBJ == 1)
            {
                Report.ParameterByName("DamageSituation").AsString = Gang;
                Report.ParameterByName("CauseInvestigation1").AsString = Gang;
            }
            else
            {
                Report.ParameterByName("DamageSituation").AsString = DR["DamageSituation"] == DBNull.Value ? "" : DR["DamageSituation"].ToString().Trim();
                Report.ParameterByName("CauseInvestigation1").AsString = DR["CauseInvestigation1"] == DBNull.Value ? "" : DR["CauseInvestigation1"].ToString().Trim();
            }
            int IsSealedBJ = 0;
            if (DR["IsSealed"] == DBNull.Value)
            {
                Report.ParameterByName("IsSealed").AsString = "外包装是否密封\r\n（□是   □否）";
            }
            else if (DR["IsSealed"].ToString() == "是")
            {
                Report.ParameterByName("IsSealed").AsString = "外包装是否密封\r\n（☑是   □否）";
                IsSealedBJ = 1;
            }
            else if (DR["IsSealed"].ToString() == "否")
            {
                Report.ParameterByName("IsSealed").AsString = "外包装是否密封\r\n（□是   ☑否）";
            }
            else
            {
                Report.ParameterByName("IsSealed").AsString = "外包装是否密封\r\n（□是   □否）";
            }
            if (IsSealedBJ == 1)
            {
                Report.ParameterByName("UnsealedCondition").AsString = Gang;
                Report.ParameterByName("CauseInvestigation2").AsString = Gang;
            }
            else
            {
                Report.ParameterByName("UnsealedCondition").AsString = DR["UnsealedCondition"] == DBNull.Value ? "" : DR["UnsealedCondition"].ToString().Trim();
                Report.ParameterByName("CauseInvestigation2").AsString = DR["CauseInvestigation2"] == DBNull.Value ? "" : DR["CauseInvestigation2"].ToString().Trim();
            }
            if (DR["IsAnyMaterialWithPollutionRisk"] == DBNull.Value)
            {
                Report.ParameterByName("IsAnyMaterialWithPollutionRisk").AsString = "运输工具内是否有造成污染、交叉污染风险的物料（□有 □无）";
            }
            else if (DR["IsAnyMaterialWithPollutionRisk"].ToString() == "有")
            {
                Report.ParameterByName("IsAnyMaterialWithPollutionRisk").AsString = "运输工具内是否有造成污染、交叉污染风险的物料（☑有 □无）";
            }
            else if (DR["IsAnyMaterialWithPollutionRisk"].ToString() == "无")
            {
                Report.ParameterByName("IsAnyMaterialWithPollutionRisk").AsString = "运输工具内是否有造成污染、交叉污染风险的物料（□有 ☑无）";
            }
            else
            {
                Report.ParameterByName("IsAnyMaterialWithPollutionRisk").AsString = "运输工具内是否有造成污染、交叉污染风险的物料（□有 □无）";
            }
            int QuestionBJ = 0;
            if (DR["IsAnyProblemAffectedMaterialQuality"] == DBNull.Value)
            {
                Report.ParameterByName("IsAnyProblemAffectedMaterialQuality").AsString = "是否有其他可能影响物料质量的问题    （□有 □无）";
            }
            else if (DR["IsAnyProblemAffectedMaterialQuality"].ToString() == "有")
            {
                Report.ParameterByName("IsAnyProblemAffectedMaterialQuality").AsString = "是否有其他可能影响物料质量的问题    （☑有 □无）";
            }
            else if (DR["IsAnyProblemAffectedMaterialQuality"].ToString() == "无")
            {
                Report.ParameterByName("IsAnyProblemAffectedMaterialQuality").AsString = "是否有其他可能影响物料质量的问题    （□有 ☑无）";
                QuestionBJ = 1;
            }
            else
            {
                Report.ParameterByName("IsAnyProblemAffectedMaterialQuality").AsString = "是否有其他可能影响物料质量的问题    （□有 □无）";
            }
            if (QuestionBJ == 1)
            {
                Report.ParameterByName("Question").AsString = "问题 :\r\n                             " + Gang;
                Report.ParameterByName("CauseInvestigation3").AsString = Gang;
            }
            else
            {
                Report.ParameterByName("Question").AsString = "问题 :" + (DR["Question"] == DBNull.Value ? "" : DR["Question"].ToString().Trim());
                Report.ParameterByName("CauseInvestigation3").AsString = DR["CauseInvestigation3"] == DBNull.Value ? "" : DR["CauseInvestigation3"].ToString().Trim();
            }

            if (DR["LotNumberType"] == DBNull.Value)
            {
                Report.ParameterByName("LotNumberType").AsString = "□生产商批号 □供应商批号";
            }
            else if (DR["LotNumberType"].ToString() == "生产商批号")
            {
                Report.ParameterByName("LotNumberType").AsString = "☑生产商批号 □供应商批号";
            }
            else if (DR["LotNumberType"].ToString() == "供应商批号")
            {
                Report.ParameterByName("LotNumberType").AsString = "□生产商批号 ☑供应商批号";
            }
            else
            {
                Report.ParameterByName("LotNumberType").AsString = "□生产商批号 □供应商批号";
            }

            if (DR["IsApprovedVendor"] == DBNull.Value)
            {
                Report.ParameterByName("IsApprovedVendor").AsString = "是否为质量管理部门批准的供应商\r\n（□是 □否）";
            }
            else if (DR["IsApprovedVendor"].ToString() == "是")
            {
                Report.ParameterByName("IsApprovedVendor").AsString = "是否为质量管理部门批准的供应商\r\n（☑是 □否）";
            }
            else if (DR["IsApprovedVendor"].ToString() == "否")
            {
                Report.ParameterByName("IsApprovedVendor").AsString = "是否为质量管理部门批准的供应商\r\n（□是 ☑否）";
            }
            else
            {
                Report.ParameterByName("IsApprovedVendor").AsString = "是否为质量管理部门批准的供应商\r\n（□是 □否）";
            }
            //□常温 □阴凉 □冷藏 □其他
            if (DR["StorageCondition"] == DBNull.Value)
            {
                Report.ParameterByName("StorageCondition").AsString = "□常温 □阴凉 □冷藏 □其他";
            }
            else if (DR["StorageCondition"].ToString() == "常温")
            {
                Report.ParameterByName("StorageCondition").AsString = "☑常温 □阴凉 □冷藏 □其他";
            }
            else if (DR["StorageCondition"].ToString() == "阴凉")
            {
                Report.ParameterByName("StorageCondition").AsString = "□常温 ☑阴凉 □冷藏 □其他";
            }
            else if (DR["StorageCondition"].ToString() == "冷藏")
            {
                Report.ParameterByName("StorageCondition").AsString = "□常温 □阴凉 ☑冷藏 □其他";
            }
            else if (DR["StorageCondition"].ToString() == "其他")
            {
                Report.ParameterByName("StorageCondition").AsString = "□常温 □阴凉 □冷藏 ☑其他";
            }
            else
            {
                Report.ParameterByName("StorageCondition").AsString = "□常温 □阴凉 □冷藏 □其他";
            }
            // ℃
            Report.ParameterByName("TransportTemperature").AsString = (DR["TransportTemperature"] == DBNull.Value ? "   " : (DR["TransportTemperature"].ToString().Trim() == "" ? "   " : DR["TransportTemperature"].ToString().Trim())) + "℃";
            if (DR["TransportCondition"] == DBNull.Value)
            {
                Report.ParameterByName("TransportCondition").AsString = "□是 □否";
            }
            else if (DR["TransportCondition"].ToString() == "是")
            {
                Report.ParameterByName("TransportCondition").AsString = "☑是 □否";
            }
            else if (DR["TransportCondition"].ToString() == "否")
            {
                Report.ParameterByName("TransportCondition").AsString = "□是 ☑否";
            }
            else
            {
                Report.ParameterByName("TransportCondition").AsString = "□是 □否";
            }

            if (DR["TransportationControlRecord"] == DBNull.Value)
            {
                Report.ParameterByName("TransportationControlRecord").AsString = "□是 □否";
            }
            else if (DR["TransportationControlRecord"].ToString() == "有")
            {
                Report.ParameterByName("TransportationControlRecord").AsString = "☑是 □否";
            }
            else if (DR["TransportationControlRecord"].ToString() == "无")
            {
                Report.ParameterByName("TransportationControlRecord").AsString = "□是 ☑否";
            }
            else
            {
                Report.ParameterByName("TransportationControlRecord").AsString = "□是 □否";
            }
            //外包装与生产商通常包装是否一致：     形状（□是  □否）       颜色（□是  □否）        字体（□是  □否）
            String bzyizhi = "外包装与生产商通常包装是否一致：     形状";
            if (DR["Shape"] == DBNull.Value)
            {
                bzyizhi += "（□是 □否）";
            }
            else if (DR["Shape"].ToString() == "是")
            {
                bzyizhi += "（☑是 □否）";
            }
            else if (DR["Shape"].ToString() == "否")
            {
                bzyizhi += "（□是 ☑否）";
            }
            else
            {
                bzyizhi += "（□是 □否）";
            }
            bzyizhi += "       颜色";
            if (DR["Colour"] == DBNull.Value)
            {
                bzyizhi += "（□是 □否）";
            }
            else if (DR["Colour"].ToString() == "是")
            {
                bzyizhi += "（☑是 □否）";
            }
            else if (DR["Colour"].ToString() == "否")
            {
                bzyizhi += "（□是 ☑否）";
            }
            else
            {
                bzyizhi += "（□是 □否）";
            }
            bzyizhi += "        字体";
            if (DR["Font"] == DBNull.Value)
            {
                bzyizhi += "（□是 □否）";
            }
            else if (DR["Font"].ToString() == "是")
            {
                bzyizhi += "（☑是 □否）";
            }
            else if (DR["Font"].ToString() == "否")
            {
                bzyizhi += "（□是 ☑否）";
            }
            else
            {
                bzyizhi += "（□是 □否）";
            }
            Report.ParameterByName("bzyizhi").AsString = bzyizhi;
            //毛重（□有  □无）
            if (DR["RoughWeight"] == DBNull.Value)
            {
                Report.ParameterByName("RoughWeight").AsString = "毛重（□有  □无）";
            }
            else if (DR["RoughWeight"].ToString() == "有")
            {
                Report.ParameterByName("RoughWeight").AsString = "毛重（☑有  □无）";
            }
            else if (DR["RoughWeight"].ToString() == "无")
            {
                Report.ParameterByName("RoughWeight").AsString = "毛重（□有  ☑无）";
            }
            else
            {
                Report.ParameterByName("RoughWeight").AsString = "毛重（□有  □无）";
            }
            //净重（□有  □无）
            if (DR["NetWeight"] == DBNull.Value)
            {
                Report.ParameterByName("NetWeight").AsString = "净重（□有  □无）";
            }
            else if (DR["NetWeight"].ToString() == "有")
            {
                Report.ParameterByName("NetWeight").AsString = "净重（☑有  □无）";
            }
            else if (DR["NetWeight"].ToString() == "无")
            {
                Report.ParameterByName("NetWeight").AsString = "净重（□有  ☑无）";
            }
            else
            {
                Report.ParameterByName("NetWeight").AsString = "净重（□有  □无）";
            }
            //批准文号（□有  □无）
            if (DR["ApprovalNumber"] == DBNull.Value)
            {
                Report.ParameterByName("ApprovalNumber").AsString = "批准文号（□有  □无）";
            }
            else if (DR["ApprovalNumber"].ToString() == "有")
            {
                Report.ParameterByName("ApprovalNumber").AsString = "批准文号（☑有  □无）";
            }
            else if (DR["ApprovalNumber"].ToString() == "无")
            {
                Report.ParameterByName("ApprovalNumber").AsString = "批准文号（□有  ☑无）";
            }
            else
            {
                Report.ParameterByName("ApprovalNumber").AsString = "批准文号（□有  □无）";
            }
            //生产商（口岸）报告（□有   □无）
            String StrReport = String.Empty;
            /*
            if (DR["ReportType"] == DBNull.Value)
            {
                StrReport = "生产商（口岸）报告";
            }
            else if (DR["ReportType"].ToString() == "生产商报告")
            {
                StrReport = "生产商报告";
            }
            else if (DR["ReportType"].ToString() == "生产商口岸报告")
            {
                StrReport = "生产商口岸报告";
            }
            else
            {
                StrReport = "生产商（口岸）报告";
            }
            */

            StrReport += "生产商报告\r\n";
            if (DR["Report"] == DBNull.Value)
            {
                StrReport += "（□有  □无）";
            }
            else if (DR["Report"].ToString() == "有")
            {
                StrReport += "（☑有  □无）";
            }
            else if (DR["Report"].ToString() == "无")
            {
                StrReport += "（□有  ☑无）";
            }
            else
            {
                StrReport += "（□有  □无）";
            }
            Report.ParameterByName("Report").AsString = StrReport;
            if (StockUser.District == "工业园1号仓库")  //20220701 及以后
            { Report.ParameterByName("RecordNumber").AsString = "记录编号：JW-201"; }
            else
            { Report.ParameterByName("RecordNumber").AsString = "记录编号：JW-005"; }
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
