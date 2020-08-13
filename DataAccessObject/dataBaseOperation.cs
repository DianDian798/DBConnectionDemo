using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace DataAccessObject
{
    public class dataBaseOperation : database
    {
        private string connection = "";
        public dataBaseOperation(string connection)
        {
            this.connection = connection;
        }
        public bool executeCommand(string sql, SqlParameter[] pars)
        {
            using (SqlConnection conn = new SqlConnection(this.connection))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand(sql, conn);

                    if (pars != null)
                    {
                        cmd.Parameters.AddRange(pars);
                    }
                    conn.Open();
                    int count = cmd.ExecuteNonQuery();

                    return count > 0 ? true : false;
                }
                catch(Exception e)
                {
                    throw e;
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        public SqlDataReader getData(string sql, SqlParameter[] pars)
        {
            //连接数据库
            using (SqlConnection conn = new SqlConnection(this.connection))
            {
                try
                {
                    //执行命令
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    conn.Open();
                    if (pars != null)
                    {
                        cmd.Parameters.AddRange(pars);
                    }
                    //如果存储过程CommandType.StoredProcedure
                    cmd.CommandType = CommandType.Text;
                    //打开连接

                    //执行度命令，返回
                    SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                    return dr;
                }
                catch (Exception e)
                {
                    throw e;
                }
                finally
                {
                    conn.Close();
                }
            }
        }
    }

    public class testDB : IFactory
    {
        public dataBaseOperation createConnection()
        {
            string infor = ConfigurationSettings.AppSettings["test"].ToString();
            dataBaseOperation test = new dataBaseOperation(infor);
            return test;
        }
    }
}
