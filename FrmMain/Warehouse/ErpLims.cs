using Global.Helper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Global.Warehouse
{
    public partial class ErpLims : Form
    {
        public ErpLims()
        {
            InitializeComponent();
        }

        private void ErpLims_Load(object sender, EventArgs e)
        {
            TbUserID.Text = StockUser.UserID;
        }

        private void TbUserID_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != (char)Keys.Enter) return;
            if (string.IsNullOrWhiteSpace(TbUserID.Text)) { MessageBox.Show("请输入信息！"); return; }
            GetData("UserID", TbUserID.Text.Trim());
        }
        private void GetData(string ziduan, string text)
        {
            string SqlStr = $@"SELECT 
	ID, 
	PONumber 采购单号, 
	VendorNumber 供应商码, 
	VendorName 供应商名, 
	ManufacturerNumber 生产商码, 
	ManufacturerName 生产商名, 
	LineNumber 行号, 
	ItemNumber 物料编码, 
	ItemDescription 物料描述, 
	LineUM 物料单位, 
	DemandDeliveryDate 需求日期, 
	OrderQuantity 订单数量, 
	ReceiveQuantity 入库数量, 
	Stock 库, 
	Bin 位, 
	LotNumber 厂家批号, 
	InternalLotNumber 公司批号, 
	StockKeeper 库管员
FROM
	dbo.ERP_LIMS_Intermediate where Status=2 and ParentID !=0 ";
            if (ziduan == "UserID")
            {
                SqlStr += $@"and LEFT (StockKeeper, 3) = '{ text }'";
            }
			if (ziduan == "ItemCode")
			{
				SqlStr += $@"and ItemNumber = '{ text }'";
			}
			if (ziduan == "ItemName")
			{
				SqlStr += $@"and ItemDescription like '%{ text }%'";
			}
			DGV.DataSource= SQLHelper.GetDataTable(GlobalSpace.SqlRJData, SqlStr);
            DGV.Columns["ID"].ReadOnly = true;
        }

        private void TbItemCode_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != (char)Keys.Enter) return;
            if (string.IsNullOrWhiteSpace(TbItemCode.Text)) { MessageBox.Show("请输入信息！"); return; }
            GetData("ItemCode", TbItemCode.Text.Trim());
        }

        private void TbItemName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != (char)Keys.Enter) return;
            if (string.IsNullOrWhiteSpace(TbItemName.Text)) { MessageBox.Show("请输入信息！"); return; }
            GetData("ItemName", TbItemName.Text.Trim());
        }

        private void BtSelectAll_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < DGV.Rows.Count; i++)
            {
                DGV["Select", i].Value = true;
            }
        }

        private void BtExtract_Click(object sender, EventArgs e)
        {
            List<string> sqlList = new List<string>();
            List<string> IDList = new List<string>();
            foreach (DataGridViewRow dgvr in DGV.Rows)
            {
                if (Convert.ToBoolean(dgvr.Cells["Select"].Value))
                {
                    string ID = dgvr.Cells["ID"].Value.ToString();
                    IDList.Add(ID);
                    string StrSelect = $@"SELECT
	PONumber, 
	VendorNumber, 
	VendorName, 
	ManufacturerNumber, 
	ManufacturerName, 
	LineNumber, 
	ItemNumber, 
	ItemDescription, 
	LineUM, 
	DemandDeliveryDate, 
	OrderQuantity, 
	UnitPrice, 
	ReceiveQuantity, 
	Stock, 
	Bin, 
	InspectionPeriod, 
	LotNumber, 
	InternalLotNumber, 
	LEFT (ManufacturedDate,10) as ManufacturedDate,
	LEFT (ExpiredDate,10) as ExpiredDate, 
	ParentGuid, 
	RetestDate, 
	LotNumberAssign, 
	ItemReceiveType, 
	Supervisor, 
	ForeignNumber, 
	BuyerID, 
	'{StockUser.UserID}' as Operator, 
	'{StockUser.UserID+"|"+StockUser.UserName}' as StockKeeper, 
	0 as Status, 
	FDAFlag, 
	IsFOItem, 
	NumberOfPackages, 
	IsDirectERP, 
	PackageSpecification, 
	PackageOdd, 
	PackageUM, 
	RequireDept, 
	QualityCheckStandard, 
	GSID, 
	IsInvestigation
FROM
	dbo.ERP_LIMS_Intermediate where ID={ID} and Status =2";
                    DataTable DtTemp = SQLHelper.GetDataTable(GlobalSpace.SqlRJData, StrSelect);
                    if (DtTemp.Rows.Count == 0)
                    {
                        MessageBox.Show("未查询到源数据！请刷新后重试！");
                        return ;
                    }
                    string SqlStr = $@"INSERT INTO [dbo].[PurchaseOrderRecordHistoryByCMF] (ErpLimsID,
	PONumber, 
	VendorNumber, 
	VendorName, 
	ManufacturerNumber, 
	ManufacturerName, 
	LineNumber, 
	ItemNumber, 
	ItemDescription, 
	LineUM, 
	DemandDeliveryDate, 
	OrderQuantity, 
	UnitPrice, 
	ReceiveQuantity, 
	Stock, 
	Bin, 
	InspectionPeriod, 
	LotNumber, 
	InternalLotNumber, 
	ManufacturedDate, 
	ExpiredDate, 
	ParentGuid, 
	RetestDate, 
	LotNumberAssign, 
	ItemReceiveType, 
	Supervisor, 
	ForeignNumber, 
	BuyerID, 
	Operator, 
	StockKeeper, 
	Status, 
	FDAFlag, 
	IsFOItem, 
	NumberOfPackages, 
	IsDirectERP, 
	PackageSpecification, 
	PackageOdd, 
	PackageUM, 
	RequireDept, 
	QualityCheckStandard, 
	GSID, 
	IsInvestigation
) values ({ID},
	'{DtTemp.Rows[0]["PONumber"]}', 
	'{DtTemp.Rows[0]["VendorNumber"]}', 
	'{DtTemp.Rows[0]["VendorName"]}', 
	'{DtTemp.Rows[0]["ManufacturerNumber"]}', 
	'{DtTemp.Rows[0]["ManufacturerName"]}', 
	'{DtTemp.Rows[0]["LineNumber"]}', 
	'{DtTemp.Rows[0]["ItemNumber"]}', 
	'{DtTemp.Rows[0]["ItemDescription"]}', 
	'{DtTemp.Rows[0]["LineUM"]}', 
	'{DtTemp.Rows[0]["DemandDeliveryDate"]}', 
	'{DtTemp.Rows[0]["OrderQuantity"]}', 
	'{DtTemp.Rows[0]["UnitPrice"]}', 
	'{DtTemp.Rows[0]["ReceiveQuantity"]}', 
	'{DtTemp.Rows[0]["Stock"]}', 
	'{DtTemp.Rows[0]["Bin"]}', 
	'{DtTemp.Rows[0]["InspectionPeriod"]}', 
	'{DtTemp.Rows[0]["LotNumber"]}', 
	'{DtTemp.Rows[0]["InternalLotNumber"]}', 
	'{DtTemp.Rows[0]["ManufacturedDate"]}', 
	'{DtTemp.Rows[0]["ExpiredDate"]}', 
	'{DtTemp.Rows[0]["ParentGuid"]}', 
	'{DtTemp.Rows[0]["RetestDate"]}', 
	'{DtTemp.Rows[0]["LotNumberAssign"]}', 
	'{DtTemp.Rows[0]["ItemReceiveType"]}', 
	'{DtTemp.Rows[0]["Supervisor"]}', 
	'{DtTemp.Rows[0]["ForeignNumber"]}', 
	'{DtTemp.Rows[0]["BuyerID"]}', 
	'{DtTemp.Rows[0]["Operator"]}', 
	'{DtTemp.Rows[0]["StockKeeper"]}', 
	'{DtTemp.Rows[0]["Status"]}', 
	'{DtTemp.Rows[0]["FDAFlag"]}', 
	'{DtTemp.Rows[0]["IsFOItem"]}', 
	'{DtTemp.Rows[0]["NumberOfPackages"]}', 
	'{DtTemp.Rows[0]["IsDirectERP"]}', 
	'{DtTemp.Rows[0]["PackageSpecification"]}', 
	'{DtTemp.Rows[0]["PackageOdd"]}', 
	'{DtTemp.Rows[0]["PackageUM"]}', 
	'{DtTemp.Rows[0]["RequireDept"]}', 
	'{DtTemp.Rows[0]["QualityCheckStandard"]}', 
	'{DtTemp.Rows[0]["GSID"]}', 
	'{DtTemp.Rows[0]["IsInvestigation"]}'
)";
                    sqlList.Add(SqlStr);
                }
            }
            if (sqlList.Count == 0)  { MessageBox.Show("未选中任何行！"); return; }
			if (SQLHelper.BatchExecuteNonQuery(GlobalSpace.FSDBConnstr, sqlList))
			{
				if (SQLHelper.ExecuteNonQuery(GlobalSpace.SqlRJData, $@"update dbo.ERP_LIMS_Intermediate set Status=3 where ID in ({string.Join(",",IDList)})"))
				{ MessageBox.Show("提取成功"); DGV.DataSource=null; }
				else
				{ MessageBox.Show("提取成功但中间表状态修改失败！请修改状态为四班已提取！"); }
			}
			else
			{ MessageBox.Show("提取失败"); }
        }

        private void BtHistory_Click(object sender, EventArgs e)
        {
			DGV.DataSource = null;
		}
    }
}
