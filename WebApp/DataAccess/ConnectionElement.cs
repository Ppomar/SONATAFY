using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace DataAccess
{
    public class ConnectionElement
    {
        private static string ConnectionString = ConfigurationManager.ConnectionStrings["SalesDBConnection"].ToString();

        internal static SqlConnection OpenConnection()
        {
            var conn = new SqlConnection(ConnectionString);
            conn.Open();
            return conn;
        }
    }
}
