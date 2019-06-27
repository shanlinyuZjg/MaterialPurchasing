using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using System.Data.SqlClient;
using Global.Helper;
using System.Data.OleDb;

namespace Global.Purchase
{
    public partial class ItemInfo : Office2007Form
    {
        //定义简体中文和西欧文编码字符集
        Encoding GB2312 = Encoding.GetEncoding("gb2312");
        Encoding ISO88591 = Encoding.GetEncoding("iso-8859-1");  //定义简体中文和西欧文编码字符集

        public ItemInfo()
        {
            this.EnableGlass = false;
            InitializeComponent();
        }

        private void tbItemFuzzyName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                if (string.IsNullOrEmpty(tbItemFuzzyName.Text.ToString()))
                {
                    MessageBox.Show("物料名称不能为空！");
                }
                else
                {
                    string str = ISO88591.GetString(GB2312.GetBytes(tbItemFuzzyName.Text.ToString()));
                    GetItemInfo_Dgv(str);
                }
            }
        }

        private void GetItemInfo_Dgv(string strItemName)
        {
            if(dgvItemDetail.Rows.Count > 0)
            {
                CommonOperate.EmptyDataGridView(dgvItemDetail);
            }
            string strSql = @"select TOP 300
                              ItemNumber AS 物料代码,
                              ItemDescription AS 物料描述,
                              ItemUM AS 单位
                              from _NoLock_FS_Item where ItemDescription like '%"+strItemName+"%'";
            dgvItemDetail.DataSource = SQLHelper.GetDataTableOleDb(GlobalSpace.oledbconnstrFSDBMR, strSql);
        }

        private void ItemInfo_Load(object sender, EventArgs e)
        {

        }

        private void tbItemFuzzyName_TextChanged(object sender, EventArgs e)
        {
            tbItemFuzzyName.Text = tbItemFuzzyName.Text.ToUpper();
            tbItemFuzzyName.SelectionStart = tbItemFuzzyName.Text.Length;
        }
    }
}
