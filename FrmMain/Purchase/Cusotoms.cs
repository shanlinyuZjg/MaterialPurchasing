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
using Global;

namespace Global.Purchase
{
    public partial class Cusotoms : Office2007Form
    {
        public Cusotoms()
        {
            InitializeComponent();
            this.EnableGlass = false;
        }

        private void Cusotoms_Load(object sender, EventArgs e)
        {
            string sqlSelect = @"SELECT ID, ManualNumber FROM CustomsManualNumber WHERE Status = 1 order by ID desc";
            DataTable dt = SQLHelper.GetDataTable(GlobalSpace.FSDBConnstr, sqlSelect);
            CommonOperate.ComboBoxBindEx(cbbe, dt, "ManualNumber", "ID");
            if (cbbe.Items.Count > 0) cbbe.SelectedIndex = 0;
        }

        private void btn_Click(object sender, EventArgs e)
        {
            
            if(!string.IsNullOrWhiteSpace(cbbe.Text))
            {
                GlobalSpace.ShouCeNumber = cbbe.Text;
                this.DialogResult = DialogResult.OK;
            }
            else
            {
                MessageBoxEx.Show("请先选择手册号！", "提示");
            }
        }
    }
}
