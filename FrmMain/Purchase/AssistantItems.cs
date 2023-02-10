using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Global;
using DevComponents.DotNetBar;
using Global.Helper;

namespace Global.Purchase
{
    public partial class AssistantItems : Office2007Form
    {
        string UserID = string.Empty;
        string UserName = string.Empty;
        string SupervisorID = string.Empty;

        public AssistantItems(string id)
        {
            this.EnableGlass = false;
            MessageBoxEx.EnableGlass = false;
            InitializeComponent();
            UserID = id;
        }

        private void btnViewAllItems_Click(object sender, EventArgs e)
        {
            dgvDetail.DataSource = GetItemInfoFromBPM();
            dgvDetail.Columns["Guid"].Visible = false;
            dgvDetail.Columns["Key"].Visible = false;
        }

        //获取BPM中物料信息
        private DataTable GetItemInfoFromBPM()
        {
            string sqlSelect = @"SELECT
	                                        Guid,
	                                        ItemCode_new AS 物料代码,
	                                        ItemDesc AS 物料描述,
	                                        Unit AS 单位,
	                                        UnitPrice AS  采购单价,
	                                        pinpai AS 品牌,
	                                        NeedCount AS 采购数量,
	                                        gongying AS 供应商名称,
	                                        UsingDept 需求部门,
                                            [Key],
	                                        Remark AS 备注
                                        FROM
	                                        Z_MaterialsPlan_Detail_info
                                        WHERE
                                            [Key] = '3' And  ERP = 0  And
	                                        InTime >= '" + DateTime.Now.AddMonths(-1).ToString("yyyy-MM") + "-01' And InTime <= '" + DateTime.Now.AddMonths(1).ToString("yyyy-MM") + "-01' And gongying <>'未匹配到供应商' Order BY gongying, ItemCode_new ";
            return SQLHelper.GetDataTable(GlobalSpace.connstrBPM, sqlSelect);
            }
        
        //获取所有A类物料代码        
        private Dictionary<string,string>  ItemDict()
        {
            string sqlSelect = @"Select ItemNumber,ItemUM From _NoLock_FS_Item Where Left(ItemNumber,1) = 'A'";
            return SQLHelper.GetDataTable(GlobalSpace.FSDBMRConnstr, sqlSelect).Rows.Cast<DataRow>().ToDictionary(x => x["ItemNumber"].ToString(), x => x["ItemUM"].ToString());
        }

        //获取所有供应商代码和名称        
        private Dictionary<string, string> VendorDict()
        {
            string sqlSelect = @"Select VendorID,VendorName From _NoLock_FS_Vendor";
            return SQLHelper.GetDataTableOleDb(GlobalSpace.oledbconnstrFSDBMR, sqlSelect).Rows.Cast<DataRow>().ToDictionary(x => x["VendorID"].ToString(), x => x["VendorName"].ToString());
        }

        private void btnMakeVendorWithVendorNumber_Click(object sender, EventArgs e)
        {
            DataTable dtTemp = (DataTable)dgvDetail.DataSource;
            if (!dgvDetail.Columns.Contains("供应商代码"))
            {
                dtTemp.Columns.Add("供应商代码");
            }
            Dictionary<string, string> dict = VendorDict();
            for(int i = 0;i < dtTemp.Rows.Count; i++)
            {
                if(dict.ContainsValue(dtTemp.Rows[i]["供应商名称"].ToString().Trim()))
                {
                    dtTemp.Rows[i]["供应商代码"] = dict.FirstOrDefault(q => q.Value == dtTemp.Rows[i]["供应商名称"].ToString()).Key;
                }
            }

            dgvDetail.DataSource =dtTemp;
            dgvDetail.Columns["Guid"].Visible = false;
            dgvDetail.Columns["Key"].Visible = false;
        }

        private void btnPO_Click(object sender, EventArgs e)
        {
            double TaxRate = Convert.ToDouble(TbTaxRate.Text);
            //1、获取去重的供应商代码列表
            DataTable dtTemp = (DataTable)dgvDetail.DataSource;

            if(dtTemp.Rows.Count == 0)
            {
                Custom.MsgEx("无可用的数据！");
                return;
            }
            DataView dv = dtTemp.DefaultView;
            DataTable dtVendor = dv.ToTable(true, "供应商代码","供应商名称");
            DataTable dtItem = dtTemp.Copy();
            //2、遍历供应商代码列表，根据供应商代码查找对应的物料信息并写入四班
            List<string> guidList = new List<string>();
            List<string> sqlList = new List<string>();
            if(CommonOperate.PlaceAssistantItemOrderWithItemDetail("PA", dtVendor, dtItem, UserName, UserID, SupervisorID, 2,out guidList,TaxRate))
            {
                for(int i = 0; i< guidList.Count;i++)
                {
                    string sqlUpdate = @"UPDATE Z_MaterialsPlan_Detail_info SET ERP = 1 Where Guid = '"+guidList[i]+"'";
                    sqlList.Add(sqlUpdate);
                }
                if(SQLHelper.BatchExecuteNonQuery(GlobalSpace.connstrBPM,sqlList))
                {
                    Custom.MsgEx("订单提交成功，请返回下单界面写入四班！");
                }
                else
                {
                    Custom.MsgEx("订单提交成功，BPM状态更新失败，请联系管理员！");
                }
            }
            else
            {
                Custom.MsgEx("订单提交失败");
            }
        }

        private void AssistantItems_Load(object sender, EventArgs e)
        {
        }
    }
}
