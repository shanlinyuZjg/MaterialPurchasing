using gregn6Lib;
using System;
using System.Data;
using System.Windows.Forms;
using Global.Helper;
using DevComponents.DotNetBar;

namespace Global.Warehouse
{
    public partial class WarehousePOItemDetailPrint :Office2007Form
    {
        private GridppReport Report = new GridppReport();
        private GridppReport subReport = new GridppReport();
        DataTable dtVendorInfo;
        DataTable dtPOItemDetail;
        string username = string.Empty;
        string ponumber = string.Empty;
        public WarehousePOItemDetailPrint(DataTable dtVendor,DataTable dtPO,string name)
        {
            this.EnableGlass = false;
            dtVendorInfo = dtVendor;
            dtPOItemDetail = dtPO;
            username = name;
            InitializeComponent();
            Report.LoadFromFile(Application.StartupPath + "\\POItemDetailPrint.grf");
            Report.ControlByName("subReportPODetail").AsSubReport.Report = subReport;
            Report.FetchRecord += new _IGridppReportEvents_FetchRecordEventHandler(Report_FetchRecord);
            subReport.FetchRecord += new _IGridppReportEvents_FetchRecordEventHandler(SubReport_FetchRecord);
            this.axGRPrintViewer1.Report = Report;
            this.axGRPrintViewer1.Start();
        }

        private void Report_FetchRecord()
        {
            Report.ParameterByName("库管员").AsString = username;
            FillRecordToReport(Report, dtVendorInfo);
        }

        private void SubReport_FetchRecord()
        {
            ponumber = Report.FieldByName("采购单号").AsString;
            dtPOItemDetail.DefaultView.RowFilter = "采购单号='"+ponumber+"'";
            DataTable dttemp = dtPOItemDetail.DefaultView.ToTable();
            FillRecordToReport(subReport, dttemp);
        }


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

        private struct MatchFieldPairType
        {
            public IGRField grField;
            public int MatchColumnIndex;
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            this.axGRPrintViewer1.Print(true);
        }
    }
}
