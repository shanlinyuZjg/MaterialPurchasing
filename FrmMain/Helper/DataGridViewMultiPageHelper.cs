using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Windows.Forms;


namespace Global.Helper
{
    class DataGridViewMultiPageHelper
    {
        //此处注意：在不同的窗体中使用分页时，在各自的类中声明一下五个变量，避免冲突。
        static int pageSize = 23;      //每页记录数
        static int recordCount = 0;    //总记录数
        static int pageCount = 0;      //总页数
        static int currentPage = 0;    //当前页
        ///LoadPage方法
        /// <summary>
        /// loaddpage方法
        /// </summary>
        public static void LoadPage(DataTable dt, DataGridView dgv, Label l1, Label l2, Label l3)
        {
            if (currentPage < 1) currentPage = 1;
            if (currentPage > pageCount) currentPage = pageCount;

            int beginRecord;
            int endRecord;
            DataTable dtTemp;
            dtTemp = dt.Clone();

            beginRecord = pageSize * (currentPage - 1);
            if (currentPage == 1) beginRecord = 0;
            endRecord = pageSize * currentPage;

            if (currentPage == pageCount) endRecord = recordCount;
            for (int i = beginRecord; i < endRecord; i++)
            {
                dtTemp.ImportRow(dt.Rows[i]);
            }
            dgv.DataSource = dtTemp;  //datagridview控件名是tf_dgv1
            /*
            l1.Text = "当前页:  " + currentPage.ToString();//当前页
            l2.Text = "总页数:  " + pageCount.ToString();//总页数
            l3.Text = "总记录数:  " + recordCount.ToString();//总记录数*/
        }

        /// <summary>
        /// 分页的方法
        /// </summary>
        /// <param name="str"></param>
        public static bool Paging(string conn, DataTable dt, DataGridView dgv, string strSql, Label l1, Label l2, Label l3)    //str是sql语句
        {
            SqlDataAdapter sda = new SqlDataAdapter(strSql, conn);
            DataSet ds = new DataSet();
            sda.Fill(ds);
            dt = ds.Tables[0];
            recordCount = dt.Rows.Count;
            pageCount = (recordCount / pageSize);
            if ((recordCount % pageSize) > 0)
            {
                pageCount++;
            }

            //默认第一页
            currentPage = 1;
            if (ds.Tables[0].Rows.Count > 0)
            {
                //        MessageBox.Show(ds.Tables[0].Rows.Count.ToString());
                LoadPage(dt, dgv, l1, l2, l3);//调用加载数据的方法
                return true;
            }
            return false;

        }

        public static void FirstPage(DataTable dt, DataGridView dgv, Label l1, Label l2, Label l3)
        {
            currentPage = 1;
            LoadPage(dt, dgv, l1, l2, l3);
        }
        public static void PreviousPage(DataTable dt, DataGridView dgv, Label l1, Label l2, Label l3)
        {
            currentPage--;
            LoadPage(dt, dgv, l1, l2, l3);
        }
        public static void NextPage(DataTable dt, DataGridView dgv, Label l1, Label l2, Label l3)
        {
            currentPage++;
            LoadPage(dt, dgv, l1, l2, l3);
        }
        public static void LastPage(DataTable dt, DataGridView dgv, Label l1, Label l2, Label l3)
        {
            currentPage = pageCount;
            LoadPage(dt, dgv, l1, l2, l3);
        }
    }
}
