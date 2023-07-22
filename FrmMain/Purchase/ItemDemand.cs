using Global.Helper;
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
    public partial class ItemDemand : Form
    {
        private string UserID = string.Empty;
        private string UserName = string.Empty;
        public ItemDemand(string userID, string userName)
        {
            InitializeComponent();
            UserID = userID;
            UserName = userName;
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            GetRequireItem();
        }
        private void GetRequireItem()
        {
            string sqlSelect = @"SELECT
	                                                    ID,OperateTime AS 提报日期,rtrim(ltrim(WorkCenter)) AS 需求车间,
	                                                    rtrim(ltrim(ItemNumber)) AS 物料代码,
	                                                    rtrim(ltrim(ItemDescription)) AS 物料描述,
	                                                    rtrim(ltrim(ItemUM)) AS 单位,
	                                                    BuyQuantity AS 需求数量,
	                                                    rtrim(ltrim(InternationalStandards)) AS 检验标准,
                                                        rtrim(ltrim(ForeignNumber)) AS 联系单号,
	                                                    NeedTime AS 需求日期,
	                                                    rtrim(ltrim(Remark)) AS 备注,
	                                                    rtrim(ltrim(VendorName)) AS 指定供应商,
                                                        case when  SYBFlag=0 then '固水'  when  SYBFlag=1 then '粉针' when  SYBFlag=2 then '原料' when  SYBFlag=3 then '大客户' else '其他' end  AS 事业部
                                                    FROM
	                                                    dbo.SolidBuyList 
                                                    WHERE
	                                                    Flag = 0 order by ID";
            dgvItemRequirement.DataSource = SQLHelper.GetDataTable(GlobalSpace.RYData, sqlSelect);
            dgvItemRequirement.Columns["ID"].Visible = false;
            for (int i = 0; i < this.dgvItemRequirement.Columns.Count; i++)
            {
                //this.dgvSpecification.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
                dgvItemRequirement.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            }
        }

        private void dgvItemRequirement_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                dgvItemRequirement["Check", e.RowIndex].Value = !Convert.ToBoolean(dgvItemRequirement["Check", e.RowIndex].Value);
            }
        }
        bool blAllSelect = true;
        private void BtAllSelect_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow dgvr in dgvItemRequirement.Rows)
            {
                dgvr.Cells["Check"].Value = blAllSelect;
            }
            blAllSelect = !blAllSelect;
        }

        private void btnExtract_Click(object sender, EventArgs e)
        {
            if (dgvItemRequirement.Rows.Count == 0)
            { MessageBox.Show("无数据！"); return; }
            #region
            if (!CheckCodeUnit(dgvItemRequirement))
            {
                MessageBox.Show("物料代码或单位不准确、需求数量小于等于零、无需求日期，已红色标示！");
                return;
            }
            #endregion
            BatchExtract(dgvItemRequirement);
        }
        private void BatchExtract(DataGridView dt)
        {
            List<int> lint = new List<int>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (Convert.ToBoolean(dt.Rows[i].Cells["Check"].Value))
                {
                    lint.Add(Convert.ToInt32(dt.Rows[i].Cells["ID"].Value));
                }
            }
            if (lint.Count == 0)
            {
                MessageBox.Show("未选中任何行"); return;
            }
            

            string str = $@"update  [dbo].[SolidBuyList] set Flag=1,ExtractTime=GETDATE(),FSTITime=GETDATE(),PlanReceiverID='{UserID}',PlanReceiverName='{UserName}'  where ID in ({string.Join(",", lint.ToArray()) }) and Flag=0";
            if (SQLHelper.ExecuteNonQuery(GlobalSpace.RYData, str))
            { MessageBox.Show("提取完成"); }
        }
        private bool CheckCodeUnit(DataGridView dgv)
        {
            bool bl = true;
            for (int i = 0; i < dgv.Rows.Count; i++)
            {
                if (Convert.ToBoolean(dgv.Rows[i].Cells["Check"].Value))
                {
                    if (string.IsNullOrWhiteSpace(dgv["需求日期", i].Value.ToString()))
                    {
                        dgv.Rows[i].DefaultCellStyle.ForeColor = Color.Red;
                        return false;
                    }
                    string sqlSelect = @"Select  ItemDescription,ItemUM,IsInspectionRequired,PreferredStockroom,PreferredBin,IsLotTraced From _NoLock_FS_Item Where ItemNumber='" + dgv["物料代码", i].Value.ToString() + "'";
                    DataTable dtTemp = SQLHelper.GetDataTableOleDb(GlobalSpace.oledbconnstrFSDBMR, sqlSelect);
                    if (dtTemp.Rows.Count == 1)
                    {
                        if (dgv["单位", i].Value.ToString() != dtTemp.Rows[0]["ItemUM"].ToString())
                        {
                            dgv.Rows[i].DefaultCellStyle.ForeColor = Color.Red;
                            bl = false;
                        }
                    }
                    else
                    {
                        dgv.Rows[i].DefaultCellStyle.ForeColor = Color.Red;
                        bl = false;
                    }
                    if (string.IsNullOrWhiteSpace(dgv["需求数量", i].Value.ToString()) || Convert.ToDecimal(dgv["需求数量", i].Value.ToString()) <= 0)
                    {
                        dgv.Rows[i].DefaultCellStyle.ForeColor = Color.Red;
                        bl = false;
                    }
                }
            }
            return bl;
        }
    }
}
