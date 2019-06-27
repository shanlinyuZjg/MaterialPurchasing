using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;
namespace Test
{
    public partial class Form1 : Office2007RibbonForm
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            RibbonTabItem rti = new RibbonTabItem();
            rti.Text = "tabitem";
            RibbonPanel rp = new RibbonPanel();
            rp.Text = "panel";
            rti.Panel = rp;
            rp.Dock = DockStyle.Fill;

            this.rcMenu.Controls.Add(rp);
            this.rcMenu.Items.Add(rti);

            RibbonBar rb = new RibbonBar();
            rb.Size = new Size(90, 90);
            ButtonItem bi = new ButtonItem("bi");
            bi.Text = "www";           
            rb.Items.Add(bi);

            RibbonBar rb2 = new RibbonBar();
            rb2.Size = new Size(90, 90);
            ButtonItem bi2 = new ButtonItem("bi2");
            bi2.Text = "www";
            rb2.Items.Add(bi2);

            rp.Controls.Add(rb);
            rp.Controls.Add(rb2);
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            MessageBoxEx.Show("第1条记录添加失败！", "提示");
        }

        private void buttonX2_Click(object sender, EventArgs e)
        {
            string strTemp = "310096|上海市固安捷物质有限公司";
            MessageBox.Show(strTemp.Substring(0, 6));
            MessageBox.Show(strTemp.Substring(7));
            MessageBox.Show(strTemp.Substring(8));
        }

        private void rcMenu_Click(object sender, EventArgs e)
        {
            dgv.AutoGenerateColumns = false;
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("Name", typeof(string)));
            dt.Columns.Add(new DataColumn("Sex", typeof(string)));
            dt.Columns.Add(new DataColumn("Age", typeof(string)));
            DataRow dr = null;
            for (int i = 0; i <= 5; i++)
            {
                dr = dt.NewRow();
                dr["Name"] = "小明" + i.ToString();
                dr["Sex"] = "性别" + i.ToString();
                dr["Age"] = i.ToString();
                dt.Rows.Add(dr);
            }
            dgv.DataSource = dt;
        }

        private void dgv_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            dgv_CellClick(sender, e);
        }

        private void dgv_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int i = 0;
            foreach (DataGridViewRow dr in dgv.Rows)
            {
                if (Convert.ToBoolean(dr.Cells[0].Value) == true)
                {
                    i += 1;
                }
            }
            MessageBox.Show("CellClick,CellContentClikc共有" + i.ToString() + "行被选中");
        }

        private void dgv_CellMouseLeave(object sender, DataGridViewCellEventArgs e)
        {
            //             int i = 0;
            //                     foreach (DataGridViewRow dr in dgv.Rows)
            //                     {
            //                         if (Convert.ToBoolean(dr.Cells[0].Value) == true)
            //                         {
            //                             i += 1;
            //                         }
            //                     }
            //                     MessageBox.Show("CellMouseLeave共有" + i.ToString() + "行被选中");
            //                 }
        }
        private void dgv_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {
            int i = 0;
            foreach (DataGridViewRow dr in dgv.Rows)
            {
                if (Convert.ToBoolean(dr.Cells[0].Value) == true)
                {
                    i += 1;
                }
            }
            MessageBox.Show("MouseUp共有" + i.ToString() + "行被选中");
        }
    }
}
