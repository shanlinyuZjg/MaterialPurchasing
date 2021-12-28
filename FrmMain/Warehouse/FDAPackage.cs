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

namespace Global.Warehouse
{
    public partial class FDAPackage : Office2007Form
    {
        public DataTable Dt = new DataTable();
        /*
        public FDAPackage()
        {
            this.EnableGlass = false;
            MessageBoxEx.EnableGlass = false;
            InitializeComponent();
      
        }
        */
        
        public FDAPackage(DataTable dt)
        {
            this.EnableGlass = false;
            MessageBoxEx.EnableGlass = false;
            InitializeComponent();
            Dt = dt;
        }
        
        private void FDAPackage_Load(object sender, EventArgs e)
        {            
            string sqlSelect = @"SELECT
	                                            ItemNumber,
	                                            Specification
                                            FROM
	                                            dbo.PurchaseDepartmentStockFDAItemSpecification";
            DataTable dtFDA = SQLHelper.GetDataTable(GlobalSpace.FSDBConnstr, sqlSelect);
           // List<string> fdaList = dtFDA.AsEnumerable().Select(r => r.Field<string>("ItemNumber")).ToList();
            dgv.DataSource = Dt;
            dgv.Columns["Guid"].Visible = false;
            
            foreach (DataGridViewRow dgvr in dgv.Rows)
            {
                DataGridViewComboBoxCell cell = new DataGridViewComboBoxCell();
                List<string> list = new List<string>();
                DataRow[] drs = dtFDA.Select("ItemNumber = '" + dgvr.Cells["物料代码"].Value.ToString() + "'");

                foreach(var v in drs)
                {
                    list.Add(v["Specification"].ToString());
                }
                cell.DataSource = list;
                cell.Value = "123";
                cell.DisplayStyle = DataGridViewComboBoxDisplayStyle.ComboBox;
                dgvr.Cells["包装规格"] = cell;
            }
              
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            foreach(DataGridViewRow  dgvr in dgv.Rows)
            {
                if(dgvr.Cells["包装规格"].Value != DBNull.Value || !string.IsNullOrWhiteSpace(dgvr.Cells["包装规格"].Value.ToString()))
                {
                    if(GlobalSpace.dictFDAItem.ContainsKey(dgvr.Cells["Guid"].Value.ToString()))
                    {
                        GlobalSpace.dictFDAItem.Remove(dgvr.Cells["Guid"].Value.ToString());
                        GlobalSpace.dictFDAItem.Add(dgvr.Cells["Guid"].Value.ToString(), dgvr.Cells["包装规格"].Value.ToString());
                    }
                    else
                    {
                        GlobalSpace.dictFDAItem.Add(dgvr.Cells["Guid"].Value.ToString(), dgvr.Cells["包装规格"].Value.ToString());
                    }
                   
                }
                else
                {
                    MessageBoxEx.Show("进入FDA库的物料必须选择包装规格！", "提示");
                    return;
                }
            }
            this.Close();
        }

        private void FDAPackage_FormClosing(object sender, FormClosingEventArgs e)
        {
            /*
            if(GlobalSpace.dictFDAItem.Count > 0)
            {
                GlobalSpace.dictFDAItem.Clear();
            }*/
        }
    }
}
