using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Global;
using Global.Helper;


namespace 原生程序测试
{
    public partial class 物料计算 : Form
    {
        public 物料计算()
        {
            InitializeComponent();
        }

        private void btnGet_Click(object sender, EventArgs e)
        {
            /* 1.获取物料BOM
                2.根据物料BOM，计算需求数量
                3.获取物料的当前数量
                4.根据生产日期计算缺口数量
                5.根据缺口物料，生成采购订单或采购信息
            */
            DataTable dtTemp = GetBOM(tbItemNumber.Text,GlobalSpace.FSDBConnstr);
            dgvBOM.DataSource = dtTemp;
            dgvCurrentQuantity.DataSource = GetCurrentStockComponentItemQuantity(dtTemp, GlobalSpace.FSDBConnstr);
            dgvCurrentRequireQuantity.DataSource = RequiredComponentItemQuantity(dtTemp, Convert.ToDouble(tbQuantity.Text));
        }
        //获取物料BOM
        private DataTable GetBOM(string itemNumber,string connStr)
        {
            string sqlSelect = @"SELECT
	                                                T1.ComponentItemNumber AS 物料代码,
                                                    T1.ComponentItemUM AS 单位,
	                                                T1.RequiredQuantity AS 用量,
	                                                T1.ScrapPercent AS 损耗
                                                FROM
	                                               _NoLock_FS_BillOfMaterial T1,
	                                                _NoLock_FS_Item T2
                                                WHERE
	                                                T1.ParentItemKey = T2.ItemKey
                                                AND T1.ComponentItemUM <> 'HR' 
                                                AND T2.ItemNumber = '"+itemNumber+ "'  Order BY T1.ComponentItemNumber  ASC";
            return SQLHelper.GetDataTable(connStr, sqlSelect);
        }

        //正向递归获取多层子项
        private DataTable GetMultiLayerBOMDetail(string itemNumber,int iLayer)
        {
            string sqlSelect = @"SELECT
	                                        '' AS 层数,
	                                        '' AS 行号,
	                                        '' AS 物料代码,
                                            '' AS 描述,
	                                        '' AS 单位,
	                                        '' AS 用量,
	                                        '' AS 损耗 From _NoLock_FS_BillOfMaterial Where 1=2";
            DataTable dtFinal = SQLHelper.GetDataTableOleDb(GlobalSpace.oledbconnstrFSDBMR, sqlSelect);

            string sqlSelectBOMDetail = @"SELECT
                                                    T1.OperationSequenceNumber AS 行号,
	                                                T1.ComponentItemNumber AS 物料代码,(
                                                    SELECT
                                                    ItemDescription
                                                    FROM
                                                    _NoLock_FS_Item
                                                    WHERE
                                                    ItemNumber = T1.ComponentItemNumber
                                                    ) AS 描述,
                                                    T1.ComponentItemUM AS 单位,
	                                                T1.RequiredQuantity AS 用量,
	                                                T1.ScrapPercent AS 损耗
                                                FROM
	                                               _NoLock_FS_BillOfMaterial T1,
	                                                _NoLock_FS_Item T2
                                                WHERE
	                                                T1.ParentItemKey = T2.ItemKey
                                                AND T1.ComponentItemUM <> 'HR' 
                                                AND T2.ItemNumber = '" + itemNumber + "'  Order BY T1.ComponentItemNumber  ASC";
            DataTable dtBOMDetail = SQLHelper.GetDataTableOleDb(GlobalSpace.oledbconnstrFSDBMR, sqlSelectBOMDetail);

            if (dtBOMDetail.Rows.Count > 0)
            {
                    foreach(DataRow dr in dtBOMDetail.Rows)
                    {
                            DataRow drFinal = dtFinal.NewRow();
                            drFinal["层数"] = iLayer.ToString();
                            drFinal["行号"] = dr["行号"].ToString();
                            drFinal["物料代码"] = dr["物料代码"].ToString();
                            drFinal["单位"] = dr["单位"].ToString();
                            drFinal["用量"] = dr["用量"].ToString();
                            drFinal["损耗"] = dr["损耗"].ToString();
                            drFinal["描述"] = dr["描述"].ToString();
                            dtFinal.Rows.Add(drFinal.ItemArray);

                            if (dr["物料代码"].ToString().Substring(0,1) == "F" || dr["物料代码"].ToString().Substring(0, 1) == "S")
                            {                        
                                DataTable dtRetrive = GetMultiLayerBOMDetail(dr["物料代码"].ToString(), iLayer+1);
                                foreach(DataRow drRetrive in dtRetrive.Rows)
                                {
                                    DataRow drNew = dtFinal.NewRow();
                                    drNew["层数"] = drRetrive["层数"].ToString();
                                    drNew["行号"] = drRetrive["行号"].ToString();
                                    drNew["物料代码"] = drRetrive["物料代码"].ToString();
                                    drNew["单位"] = drRetrive["单位"].ToString();
                                    drNew["用量"] = drRetrive["用量"].ToString();
                                    drNew["损耗"] = drRetrive["损耗"].ToString();
                                    drNew["描述"] = drRetrive["描述"].ToString();
                                    dtFinal.Rows.Add(drNew.ItemArray);
                                }
                            }                   
                    }
            }
            
            return dtFinal;
        }


        //根据物料BOM，计算需求数量
        private DataTable RequiredComponentItemQuantity(DataTable dtBOM,double productQuantity)
        {
            dtBOM.Columns.Add("订单用量");
            int baseNumber = 0;
            foreach(DataRow dr in dtBOM.Rows)
            {
                baseNumber = GetBaseNumber(Convert.ToDouble(dr["用量"]));
                dr["订单用量"] = productQuantity * (Convert.ToDouble(dr["用量"]) + (Convert.ToDouble(dr["损耗"]) / 100/baseNumber));             }

            return dtBOM;
        }

        //获取物料的当前数量
        private DataTable GetCurrentStockComponentItemQuantity(DataTable dtBOM,string connStr)
        {
            string[] itemNumberArray = dtBOM.AsEnumerable().Select(r => r.Field<string>("物料代码")).ToArray();
            string sqlSelect = @"SELECT
	                                                T2.ItemNumber AS 物料代码,
	                                                T1.OnHandQuantity AS 库存量 ,T1.InInspectionQuantity AS 在检量,
	                                                T1.OnOrderQuantity AS 在订量 	                                                
                                                FROM
	                                                _NoLock_FS_ItemData T1
                                                LEFT JOIN _NoLock_FS_Item T2 ON T1.ItemKey = T2.ItemKey
                                                WHERE
	                                                T2.ItemNumber IN ('{0}') Order BY T2.ItemNumber";

            sqlSelect = string.Format(sqlSelect, string.Join("','", itemNumberArray));
            return SQLHelper.GetDataTable(connStr, sqlSelect);
        }
        //判断数值是否小于1
        private bool IsSmallerThanOne(double number)
        {
            if(number <= 1)
            {
                return true;
            }
            return false;
        }
        //获取小数的精确位数，用于损耗中的计算（小于1的数必须再除以相应的位数）
        private int GetBaseNumber(double number)
        {
            int baseNumber = 0;
            if(number < 1)
            {
                if(number*10 >= 1)
                {
                    baseNumber = 10;
                }
                else if(number*100 >= 1)
                {
                    baseNumber = 100;
                }
                else if (number * 1000 >= 1)
                {
                    baseNumber = 1000;
                }
                else if (number * 10000 >= 1)
                {
                    baseNumber = 10000;
                }
                else if (number * 100000 >= 1)
                {
                    baseNumber = 100000;
                }
                else if (number * 1000000 >= 1)
                {
                    baseNumber = 1000000;
                }
                else if (number * 10000000 >= 1)
                {
                    baseNumber = 10000000;
                }
            }
            else
            {
                baseNumber = 1;
            }
            return baseNumber;
        }
        private void tbItemNumber_TextChanged(object sender, EventArgs e)
        {
            tbItemNumber.Text = tbItemNumber.Text.ToUpper();
            tbItemNumber.SelectionStart = tbItemNumber.Text.Length;
        }

        private void tbItemNumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar == (char)13)
            {

                //btnGet_Click(sender, e);
                dgvBOM.DataSource = GetMultiLayerBOMDetail(tbItemNumber.Text.Trim(), 0);
            }
        }

        private void 物料计算_Load(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }
        //
    }
}
