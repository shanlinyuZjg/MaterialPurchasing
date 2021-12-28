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
using SoftBrands.FourthShift.Transaction;

namespace Global.Warehouse
{
    public partial class ItemIMTRByOrder : Office2007Form
    {
        bool bImtr = false;
        private string UserID = string.Empty;
        private string Password = string.Empty;
        public ItemIMTRByOrder(string id,string password)
        {
            this.EnableGlass = false;
            MessageBoxEx.EnableGlass = false;
            InitializeComponent();
            UserID = id;
            Password = password;
        }

        private void btnViewByItemNumber_Click(object sender, EventArgs e)
        {
            bImtr = true;
            dgvDetail.DataSource = GetRecord("ItemNumber", tbItemNumber.Text.Trim());
            dgvDetail.Columns["Guid"].Visible = false;
            dgvDetail.Columns["Status"].Visible = false;
        }

        private void btnViewAppointedDate_Click(object sender, EventArgs e)
        {
            bImtr = true;
            dgvDetail.DataSource = GetRecord("Date", dtpFS.Value.ToString("yyyy-MM-dd"));
            dgvDetail.Columns["Guid"].Visible = false;
            dgvDetail.Columns["Status"].Visible = false;
        }

        private void btnImtr_Click(object sender, EventArgs e)
        {
            List<string> poList = new List<string>();
            FSFunctionLib.FSConfigFileInitialize(GlobalSpace.fsconfigfilepath, UserID, Password);
            int count = 0;
            int fscount = 0;
            foreach (DataGridViewRow dgvr in dgvDetail.Rows)
            {
                if (Convert.ToBoolean(dgvr.Cells["Check"].Value) == true)
                {
                    count++;
                    IMTR01 imtr = new IMTR01();
                    imtr.ItemNumber.Value = dgvr.Cells["物料代码"].Value.ToString();
                    imtr.InventoryQuantity.Value = dgvr.Cells["入库数量"].Value.ToString();
                    imtr.StockroomFrom.Value = dgvr.Cells["库"].Value.ToString();
                    imtr.BinFrom.Value = dgvr.Cells["位"].Value.ToString();
                    imtr.InventoryCategoryFrom.Value = "I";
                    imtr.LotNumberFrom.Value = dgvr.Cells["厂家批号"].Value.ToString();

                    imtr.StockroomTo.Value = dgvr.Cells["库"].Value.ToString();
                    imtr.BinTo.Value = dgvr.Cells["位"].Value.ToString();
                    imtr.InventoryCategoryTo.Value = "O";
                    imtr.LotNumberTo.Value = dgvr.Cells["厂家批号"].Value.ToString();
                    imtr.LotIdentifier.Value = "E";

                    if (!FSFunctionLib.fstiClient.ProcessId(imtr, null))
                    {
                        FSTIError error = FSFunctionLib.fstiClient.TransactionError;
                        CommonOperate.WriteFSErrorLog("IMTR", imtr, error, UserID);
                        poList.Add(dgvr.Cells["采购单号"].Value.ToString() + " " + dgvr.Cells["行号"].Value.ToString() + " " + dgvr.Cells["物料代码"].Value.ToString() + " " + dgvr.Cells["描述"].Value.ToString() + " " + dgvr.Cells["公司批号"].Value.ToString());
                    }
                    else
                    {
                        fscount++;
                    }
                }
            }
            FSFunctionLib.FSExit();
            if (poList.Count > 0)
            {
                MessageBoxEx.Show("以下订单：" + string.Join(",", poList.ToArray()) + " 移库失败！", "提示");
            }
            else
            {
                if(count == fscount)
                {
                    MessageBoxEx.Show("移库成功！", "提示");
                }
            }
        }

        private void btnError_Click(object sender, EventArgs e)
        {
            bImtr = false;
            string sqlSelect = @"Select Type AS 类型,ErrorContent AS 内容,OperateDateTime AS 日期 From FSErrorLogByCMF Where Operator='" + UserID + "' And Left(OperateDateTime,10)='" + DateTime.Now.ToString("yyyy-MM-dd") + "'  Order By OperateDateTime Desc";
            dgvDetail.DataSource = SQLHelper.GetDataTable(GlobalSpace.FSDBConnstr, sqlSelect);
        }

        //获取当前未提交的入库物料信息
        private DataTable GetRecord(string criteria,string value)
        {
            string sqlCriteria = string.Empty;
            string sqlSelect = @"SELECT
	                                            Guid,
                                                FSOperateDate AS 入库日期,
	                                            PONumber AS 采购单号,
	                                            LineNumber AS 行号,
	                                            ItemNumber AS 物料代码,
	                                            ItemDescription AS 描述,
	                                            LineUM AS 单位,
	                                            ReceiveQuantity AS 入库数量,
                                                LotNumber AS 厂家批号,
	                                            InternalLotNumber AS 公司批号,
	                                            Stock AS 库,
	                                            Bin AS 位,	                                          
	                                            ExpiredDate AS 到期日期,
                                                VendorNumber AS 供应商码,
	                                            VendorName AS 供应商名,
	                                            ManufacturerNumber AS 生产商码,
	                                            ManufacturerName AS 生产商名,
	                                            Status
                                            FROM
	                                            dbo.PurchaseOrderRecordHistoryByCMF  Where Status = 2 And ";
            switch(criteria)
            {
                case "ItemNumber":
                    sqlCriteria = @" ItemNumber='"+value+ "' And Operator='" + UserID+ "' order by FSOperateDate Desc";
                    break;
                case "Date":
                    sqlCriteria = @"  FSOperateDate='" + value + "' And Operator='" + UserID + "' order by FSOperateDate Desc";
                    break;
            }
            return SQLHelper.GetDataTable(GlobalSpace.FSDBConnstr, sqlSelect+sqlCriteria);
        }

        private void btnMakeAllChecked_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow dgvr in dgvDetail.Rows)
            {
                dgvr.Cells["Check"].Value = true;
            }
        }

        private void ItemIMTRByOrder_Load(object sender, EventArgs e)
        {

        }
    }
}
