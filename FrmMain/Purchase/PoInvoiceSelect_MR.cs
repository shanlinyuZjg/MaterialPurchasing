using Global.Helper;
using gregn6Lib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Global.Purchase
{
    public partial class PoInvoiceSelect_MR : Form
    {
        private string UserID = string.Empty;
        private string UserName = string.Empty;
        private string Department = string.Empty;
        public PoInvoiceSelect_MR(string userID, string userName,string department)
        {
            InitializeComponent();
            UserID = userID;
            UserName = userName;
            Department = department;
        }

        private void PoInvoiceSelect_MR_Load(object sender, EventArgs e)
        {

        }

        private void BtnSelect_Click(object sender, EventArgs e)
        {
            string sqlSelect = string.Empty;
            if (Department == "供应")
            {
                sqlSelect = $@"SELECT
                                                    distinct VendorNumber 供应商码,  VendorName 供应商名, InvoiceNumberS 发票号,Status
                                                FROM
	                                                PurchaseOrderInvoiceRecordMRByCMF where Status >0 and Operator ='{UserID}' and UpdateDateTime >='{dateTimePicker1.Value.ToString("yyyy-MM-dd")}' and UpdateDateTime <'{dateTimePicker2.Value.AddDays(1).ToString("yyyy-MM-dd")}' and VendorNumber like '%{TbVendorNumber.Text.Trim()}%' and VendorName like '%{TbVendorName.Text.Trim()}%' and InvoiceNumberS like '%{TbInvoiceS.Text.Trim()}%'";
            }
            else if (Department == "审计")
            {
                sqlSelect = $@"SELECT
                                                    distinct VendorNumber 供应商码,  VendorName 供应商名, InvoiceNumberS 发票号,Status
                                                FROM
	                                                PurchaseOrderInvoiceRecordMRByCMF where Status >0 and OperateAudit ='{UserID}' and AuditUpdateDateTime >='{dateTimePicker1.Value.ToString("yyyy-MM-dd")}' and AuditUpdateDateTime <'{dateTimePicker2.Value.AddDays(1).ToString("yyyy-MM-dd")}' and VendorNumber like '%{TbVendorNumber.Text.Trim()}%' and VendorName like '%{TbVendorName.Text.Trim()}%' and InvoiceNumberS like '%{TbInvoiceS.Text.Trim()}%'";
            }
            else
            {
                sqlSelect = $@"SELECT
                                                    distinct VendorNumber 供应商码,  VendorName 供应商名, InvoiceNumberS 发票号,Status
                                                FROM
	                                                PurchaseOrderInvoiceRecordMRByCMF where Status >0 and OperateFinance ='{UserID}' and FinanceUpdateDateTime >='{dateTimePicker1.Value.ToString("yyyy-MM-dd")}' and FinanceUpdateDateTime <'{dateTimePicker2.Value.AddDays(1).ToString("yyyy-MM-dd")}' and VendorNumber like '%{TbVendorNumber.Text.Trim()}%' and VendorName like '%{TbVendorName.Text.Trim()}%' and InvoiceNumberS like '%{TbInvoiceS.Text.Trim()}%'";
            }
            DGV1.DataSource = SQLHelper.GetDataTable(GlobalSpace.FSDBConnstr, sqlSelect);
            for (int i = 0; i < DGV1.Columns.Count; i++)
            {
                DGV1.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
            }
        }

        private void DGV1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int RowIndex = e.RowIndex;
            if (RowIndex < 0) return;

            string sqlSelect = $@"SELECT 
	Remarks 备注, 
    ForeignNumber 联系单号,
	cast(ReceiveDate as date) 入库日期, 
	PONumber 采购单号, 
	LineNumber 行号,  
	ItemNumber 物料编码, 
	ItemDescription 物料描述, 
	UM 单位, 
	ReceiveQuantity 入库量, 
    OrderQuantity 订单量,
    LotNumber 厂家批号,
    InnerLotNumber 公司批号,
	UnitPrice 单价, 
	Amount 总价, 
	InvoiceNumberS 发票号, 
    AllAmount 入库总金额,
    InvoiceNumber 四班票号,
    InvoiceTaxedAmount 总税额,
    InvoiceAmount 不含税发票总额,
	InvoiceMatchedQuantity 已匹配数量, 
	VendorNumber 供应商码, 
	VendorName 供应商名,
    Buyer 采购员,
    Stockkeeper 库管员,
    ManufacturerID 生产商码,
    ManufacturerName 生产商名,
	SequenceNumber 序号,
	Id,APReceiptLineKey 
                                                FROM
	                                                PurchaseOrderInvoiceRecordMRByCMF where VendorNumber ='{DGV1["供应商码", RowIndex].Value.ToString()}' and InvoiceNumberS='{DGV1["发票号", RowIndex].Value.ToString()}'";
            DGV2.DataSource = SQLHelper.GetDataTable(GlobalSpace.FSDBConnstr, sqlSelect);

            for (int i = 0; i < DGV2.Columns.Count; i++)
            {
                //DGV2.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
            }
        }
        private void BtnPrint_Click(object sender, EventArgs e)
        {
            if (DGV2.Rows.Count == 0) return;
            Print();
        }
        GridppReport Report = new GridppReport();
        public void Print()
        {
            Report = new GridppReport();
            Report.LoadFromFile(".\\应付发票入库单.grf");
            //设置与数据源的连接串，因为在设计时指定的数据库路径是绝对路径。
            Report.DetailGrid.Recordset.ConnectionString = "";
            Report.FetchRecord += new _IGridppReportEvents_FetchRecordEventHandler(ReportFetchRecord);
            //连接报表事件
            Report.Initialize += new _IGridppReportEvents_InitializeEventHandler(ReportInitialize);
            Report.PrintPreview(true);
        }
        private void ReportFetchRecord()
        {
            FillRecordToReport(Report, (DataTable)(DGV2.DataSource));
        }
        private void ReportInitialize()
        {
            //Report.DetailGrid.PrintAdaptFitText = true;
            Report.ParameterByName("Vendor").AsString = DGV2["供应商码",0].Value.ToString()+"  "+ DGV2["供应商名", 0].Value.ToString();
            Report.ParameterByName("InvoiceNumbers").AsString = DGV2["发票号", 0].Value.ToString();
            Report.ParameterByName("ReceiveAmount").AsString = DGV2["入库总金额", 0].Value.ToString();
            Report.ParameterByName("TaxPreAmount").AsString = DGV2["不含税发票总额", 0].Value.ToString();
            Report.ParameterByName("TaxAmount").AsString = DGV2["总税额", 0].Value.ToString();
            Report.ParameterByName("FSinvoiceNumber").AsString = DGV2["四班票号", 0].Value.ToString();

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
