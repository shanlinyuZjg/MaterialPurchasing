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

namespace Global.Purchase
{
    public partial class PrintTest : Office2007Form
    {
        List<string> VendorList = new List<string>();
        private GridppReport Report = new GridppReport();
        DataTable dt = new DataTable();
        string InvoiceNumber = string.Empty;
        public PrintTest(List<string> list,DataTable dtInvoice,string invoiceNumber,string templatePah)
        {
            InitializeComponent();
            this.EnableGlass = false;
            //         DR = dr;
            dt = dtInvoice.Copy();
            VendorList = list;
            InvoiceNumber = invoiceNumber;
            Report.LoadFromFile(Application.StartupPath + templatePah);          
            Report.Initialize += new _IGridppReportEvents_InitializeEventHandler(ShowOrder);
            Report.FetchRecord += new _IGridppReportEvents_FetchRecordEventHandler(ReportFetchRecord);
            this.axGRPrintViewer1.Report = Report;
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            this.axGRPrintViewer1.Print(true);
        }

        private void ShowOrder()
        {
            
            Report.ParameterByName("供应商码").AsString = VendorList[0];
            Report.ParameterByName("供应商名").AsString = VendorList[1];
            Report.ParameterByName("生产商码").AsString = VendorList[2];
            Report.ParameterByName("生产商名").AsString = VendorList[3];    
            Report.ParameterByName("发票号").AsString = InvoiceNumber;
        }

        private void PrintInvoiceItemDetail_Load(object sender, EventArgs e)
        {
            this.axGRPrintViewer1.Refresh();
        }

        private void ReportFetchRecord()
        {
            FillRecordToReport(Report, dt);
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
    }
}
