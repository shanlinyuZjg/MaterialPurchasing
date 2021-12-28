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
    public partial class SQLServerAlwaysOn : Form
    {
        public string DBConn = "server=192.168.8.177;database=DWTEST;uid=sa;pwd=ReyoungDB1!";
        public string DBConn3 = "server=192.168.8.169;database=DWTEST;uid=sa;pwd=ReyoungDB1!;ApplicationIntent = ReadOnly";
        public string DBConnNewUserWrite = "server=192.168.8.177;database=DWTEST;uid=dwtest;pwd=reyoung";
        public string DBConnNewUserRead = "server=192.168.8.177;database=DWTEST;uid=dwtestread;pwd=reyoung";

        public SQLServerAlwaysOn()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            List<string> sqlList = new List<string>();
            for(int i = 0; i < 100; i++)
            {
                string sqlInsert = @"Insert Into DWTest (Name,Sex,Age) Values ('王二花"+i.ToString()+"','"+i.ToString()+"',"+i+") ";
                sqlList.Add(sqlInsert);
            }
            if(SQLHelper.BatchExecuteNonQuery(DBConn,sqlList))
            {
                MessageBox.Show("Yest");
            }
            else
            {
                MessageBox.Show("No!");

            }
        }

        private void SQLServerAlwaysOn_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string sql = @"Select  * from DWTEST";
            dataGridView1.DataSource = SQLHelper.GetDataTable(DBConn+";"+tbString.Text , sql);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string sql = @"Select  * from DWTEST";
            dataGridView1.DataSource = SQLHelper.GetDataTable(DBConn3, sql);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            List<string> sqlList = new List<string>();
            for (int i = 0; i < 100; i++)
            {
                string sqlInsert = @"Insert Into DWTest (Name,Sex,Age) Values ('王二花" + i.ToString() + "','" + i.ToString() + "'," + i + ") ";
                sqlList.Add(sqlInsert);
            }
            if (SQLHelper.BatchExecuteNonQuery(DBConnNewUserWrite, sqlList))
            {
                MessageBox.Show("Yest");
            }
            else
            {
                MessageBox.Show("No!");

            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            string sql = @"Select  * from DWTEST";
            dataGridView1.DataSource = SQLHelper.GetDataTable(DBConnNewUserRead, sql);
        }
    }
}
