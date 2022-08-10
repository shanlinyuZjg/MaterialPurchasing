using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Data.OleDb;
using System.Windows.Forms;

namespace SpecialOperate
{
    public static class SQLHelper
    {

        #region 返回受影响的行数 + int ExecuteNonQuery(string connstr,string sql, params SqlParameter[] sp)
        /// <summary>
        /// 返回受影响的行数
        /// </summary>
        /// <param name="connstr">数据库连接字符串</param>
        /// <param name="sql">SQL语句</param>
        /// <param name="sp">SQL参数</param>
        /// <returns></returns>
        public static bool ExecuteNonQuery(string connstr, string sql, params SqlParameter[] sp)
        {

            using (SqlConnection conn = new SqlConnection(connstr))
            {
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddRange(sp);
                    conn.Open();
                    if(cmd.ExecuteNonQuery() > 0)
                    {
                        return true;
                    }
                    return false;
                }
            }


        }
        #endregion

        #region 执行查询操作 + object ExecuteScalar(string connstr,string sql,params SqlParameter[] sp)
        /// <summary>
        /// 
        /// </summary>
        /// <param name="connstr"></param>
        /// <param name="sql"></param>
        /// <param name="sp"></param>
        /// <returns></returns>
        public static object ExecuteScalar(string connstr, string sql, params SqlParameter[] sp)
        {

            using (SqlConnection conn = new SqlConnection(connstr))
            {
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddRange(sp);
                    conn.Open();
                    return cmd.ExecuteScalar();
                }
            }


        }
        #endregion
        public static object OleDBExecuteScalar(string connstr, string sql, params SqlParameter[] sp)
        {

            using (OleDbConnection conn = new OleDbConnection(connstr))
            {
                using (OleDbCommand cmd = new OleDbCommand(sql, conn))
                {
                    cmd.Parameters.AddRange(sp);
                    conn.Open();
                    return cmd.ExecuteScalar();
                }
            }
            

        }
        #region 创建SqlDataReader对象
        /// <summary>
        /// 创建SqlDataReader对象
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="sp"></param>
        /// <returns></returns>

        public static SqlDataReader ExecuteReader(string strconn, string sql, params SqlParameter[] sp)
        {
            using (SqlConnection conn = new SqlConnection(strconn))
            {
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddRange(sp);
                    conn.Open();
                    // 微软MSDN上例子中的解释：When using CommandBehavior.CloseConnection, the connection will be closed when the 
                    // IDataReader is closed.
                    return cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection);

                }
            }

        }
        #endregion

        #region 返回第一行某一列的值
        public static string GetItemValue(string connstr, string strItemName, string sql, params SqlParameter[] sp)
        {
            using (SqlConnection conn = new SqlConnection(connstr))
            {
                string strValue = string.Empty;
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddRange(sp);
                conn.Open();
                SqlDataReader sdr = cmd.ExecuteReader();
                if (sdr.Read())
                {
                    strValue = sdr[strItemName].ToString();
                }

                sdr.Close();
                return strValue;
            }

        }
        #endregion

        #region 执行SQL语句，返回DataSet对象 + DataSet GetDataSet(string connstr,string sql,params SqlParameter[] sp)
        /// <summary>
        /// 
        /// </summary>
        /// <param name="connstr"></param>
        /// <param name="sql"></param>
        /// <param name="sp"></param>
        /// <returns></returns>
        /// 
        public static DataSet GetDataSet(string connstr, string sql, params SqlParameter[] sp)
        {
            DataSet ds = new DataSet();
            //此处也可以继续使用sqlcommand语句来进行，只不过sqldataadapter在生成对象时，直接使用new SqlDataAdapter(sql,connStr)即可。
            using (SqlDataAdapter sda = new SqlDataAdapter(sql, connstr))
            {
                sda.SelectCommand.Parameters.AddRange(sp);
                sda.Fill(ds);
            }
            return ds;
        }
        #region 执行SQL语句，返回DataTable对象
        /// <summary>
        ///  执行SQL语句，返回DataTable对象
        /// </summary>
        /// <param name="connstr"></param>
        /// <param name="sql"></param>
        /// <param name="sp"></param>
        /// <returns></returns>
        public static DataTable GetDataTable(string connstr, string sql, params SqlParameter[] sp)
        {
            DataSet ds = new DataSet();
            //此处也可以继续使用sqlcommand语句来进行，只不过sqldataadapter在生成对象时，直接使用new SqlDataAdapter(sql,connStr)即可。
 //           MessageBox.Show(sql);
            using (SqlDataAdapter sda = new SqlDataAdapter(sql, connstr))
            {
                sda.SelectCommand.Parameters.AddRange(sp);
                sda.Fill(ds);
            }
            return ds.Tables[0];
        }
        #endregion

        #endregion
        public static DataTable GetDataTableOleDb(string connstr,string sql, params OleDbParameter[] odp)
        {
            DataSet ds = null;
            using (OleDbConnection oleconn = new OleDbConnection(connstr))
            {
                OleDbDataAdapter oda = new OleDbDataAdapter(sql, oleconn);
                oda.SelectCommand.Parameters.AddRange(odp);
                try
                {
                    oleconn.Open();
                    ds = new DataSet();
                    oda.Fill(ds);
                }
                catch(Exception ex)
                {
                    MessageBox.Show("发生异常：" + ex.Message);
                }
              
            }          
            return ds.Tables[0];
        }

        public static DataTable GetDataTableOleDbSpecial(string connstr, string sql, string paraName,string paraValue)
        {
            DataSet ds = null;
            using (OleDbConnection oleconn = new OleDbConnection(connstr))
            {
                OleDbDataAdapter oda = new OleDbDataAdapter(sql, oleconn);
                oda.SelectCommand.Parameters.Add(paraName, OleDbType.VarChar, 80).Value = paraValue;
                try
                {
                    oleconn.Open();
                    ds = new DataSet();
                    oda.Fill(ds);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("发生异常：" + ex.Message);
                }

            }
            return ds.Tables[0];
        }
        #region 检查是否存在 + bool Exist(string sql,params SqlParameter[] sp)
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sql">这里sql语句中使用count(字段名)的方式，能够保证</param>
        /// <param name="sp"></param>
        /// <returns></returns>
        public static bool Exist(string connstr, string sql, params SqlParameter[] sp)
        {
            int cmdResult = Convert.ToInt32(ExecuteScalar(connstr, sql, sp));
            if (cmdResult == 0)
            {
                return false;
            }
            else
                return true;
        }
        #endregion

        public static bool BatchExecuteNonQuery(string conn,List<string> SqlList)
        {
            SqlConnection Conn = new SqlConnection(conn);
            
            bool state = false;
            SqlTransaction transaction = null;
            if (Conn.State != ConnectionState.Open)
            {
                try
                {
                    Conn.Open();
                }
                catch
                {
                    throw new Exception("数据库无法连接");
                }
            }
            try
            {
                SqlCommand cmd = new SqlCommand();

                transaction = Conn.BeginTransaction();
                cmd.Transaction = transaction;
                cmd.Connection = Conn;
                cmd.CommandType = CommandType.Text;

                for (int i = 0; i < SqlList.Count; i++)
                {
                    cmd.CommandText = SqlList[i].ToString();
                    cmd.ExecuteNonQuery();                  
                }
                transaction.Commit();
                state = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("异常：" + ex.Message);
                state = false;
                transaction.Rollback();
            }
            finally
            {
                Conn.Close();
            }
            return state;
        }

        //将DataTable中的改动快速更新到数据库中
        public static void DataTableUpdate(string connStr,string strSql)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                DataSet ds = new DataSet();
                DataTable dtSelect;
                SqlDataAdapter sda = new SqlDataAdapter();
                sda.SelectCommand = new SqlCommand(strSql, conn);
                SqlCommandBuilder cb = new SqlCommandBuilder(sda);
                sda.Fill(ds);
                dtSelect = ds.Tables[0];
                sda.Update(dtSelect);
                dtSelect.AcceptChanges();
            }
        }
        //快速批量更新
        public static void BatchUpdate()
        {

        }
        /// <summary>
        ///  执行SQL语句，返回List对象
        /// </summary>
        /// <param name="connstr"></param>
        /// <param name="sql"></param>
        /// <param name="sp"></param>
        /// <returns></returns>
        public static List<string> GetList(string connstr, string sql, string strValue,params SqlParameter[] sp)
        {
            DataSet ds = new DataSet();
            //此处也可以继续使用sqlcommand语句来进行，只不过sqldataadapter在生成对象时，直接使用new SqlDataAdapter(sql,connStr)即可。
            //           MessageBox.Show(sql);
            using (SqlDataAdapter sda = new SqlDataAdapter(sql, connstr))
            {
                sda.SelectCommand.Parameters.AddRange(sp);
                sda.Fill(ds);
            }

            return ds.Tables[0].AsEnumerable().Select(r=>r.Field<string>(strValue)).ToList();
        }


    }
}
