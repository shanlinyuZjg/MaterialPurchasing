using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using gregn6Lib;

namespace Global
{
    public partial class PrintReport : Office2007Form
    {
        private GridppReport Report = new GridppReport();
        private DataTable DT = new DataTable();
        public PrintReport(DataTable dtTemp,string grfFilePath)
        {
            InitializeComponent();
            this.EnableGlass = false;
            DT = dtTemp.Copy();
            Report.LoadFromFile(Application.StartupPath + grfFilePath);
            Report.DetailGrid.Recordset.ConnectionString = GlobalSpace.FSDBConnstr;
            Report.Initialize += new _IGridppReportEvents_InitializeEventHandler(ShowOrder);
            Report.FetchRecord += new _IGridppReportEvents_FetchRecordEventHandler(ReportFetchRecord);
            this.axGRPrintViewer1.Report = Report;
        }

        private void ReportFetchRecord()
        {
            FillRecordToReport(Report, DT);
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
                           
        }

        private void PrintPO_Load(object sender, EventArgs e)
        {
            this.axGRPrintViewer1.Refresh();
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            this.axGRPrintViewer1.Print(true);
        }
    }
}
