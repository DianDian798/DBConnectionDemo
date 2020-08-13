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
    interface database
    {
        SqlDataReader getData(string sql, SqlParameter[] pars);

        bool executeCommand(string sql, SqlParameter[] pars);
    }

    interface IFactory
    {
        dataBaseOperation createConnection();
    }
}
