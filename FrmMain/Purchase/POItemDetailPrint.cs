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
    public partial class POItemDetailPrint : Office2007Form
    {
        DataTable dtForPOItemPrint = new DataTable();
        Dictionary<string, string> dictForPOItemPrint = new Dictionary<string, string>();
        private GridppReport Report = new GridppReport();
        public POItemDetailPrint()
        {
            InitializeComponent();
        }
        public POItemDetailPrint(DataTable dt,Dictionary<string,string> dict)
        {
            InitializeComponent();
            this.EnableGlass = false;
            MessageBoxEx.EnableGlass = false;
            dtForPOItemPrint = dt;
            dictForPOItemPrint = dict;
            Report.LoadFromFile(Application.StartupPath + "\\PurchasePODetail.grf");
            Report.DetailGrid.Recordset.ConnectionString = GlobalSpace.oledbconnstrFSDBMR;
            Report.Initialize += new _IGridppReportEvents_InitializeEventHandler(GetPOItemDetail);
            Report.FetchRecord += new _IGridppReportEvents_FetchRecordEventHandler(ReportFetchRecord);
            this.axGRPrintViewer1.Report = Report;         
        }

        private void GetPOItemDetail()
        {
            //将Dictionary中的值赋给报表中的控件
            if(string.IsNullOrEmpty(dictForPOItemPrint["外贸单号"]))
            {
                Report.ParameterByName("外贸单号").AsString = " ";
            }
            else
            {
                Report.ParameterByName("外贸单号").AsString = "外贸单号：" + dictForPOItemPrint["外贸单号"];
            }
            Report.ParameterByName("供应商代码").AsString = dictForPOItemPrint["供应商代码"];
            Report.ParameterByName("供应商名称").AsString = dictForPOItemPrint["供应商名称"];
            Report.ParameterByName("采购单号").AsString = dictForPOItemPrint["采购单号"];
            Report.ParameterByName("采购员").AsString = dictForPOItemPrint["采购员"];
            Report.ParameterByName("库管员").AsString = dictForPOItemPrint["库管员"];
        }

        private void ReportFetchRecord()
        {
            FillRecordToReport(Report, dtForPOItemPrint);
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

        private struct MatchFieldPairType
        {
            public IGRField grField;
            public int MatchColumnIndex;
        }

        private void POItemDetailPrint_Load(object sender, EventArgs e)
        {
            this.axGRPrintViewer1.Refresh();
        }


        private void btnPrint_Click(object sender, EventArgs e)
        {
            this.axGRPrintViewer1.Print(true);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
