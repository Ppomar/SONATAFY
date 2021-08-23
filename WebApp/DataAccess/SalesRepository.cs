using System;
using System.Collections.Generic;
using Models;
using System.Data;
using System.Data.SqlClient;

namespace DataAccess
{
    public class SalesRepository : ConnectionElement
    {       
        public static bool CreatePurchase(List<Product> products)
        {
            SqlConnection conn = null;
            var response = false;

            try
            {
                conn = OpenConnection();

                using (var tckcmd = new SqlCommand())
                {                 
                    tckcmd.Connection = conn;
                    tckcmd.CommandText = "SP_SALES_CREATE_TICKET";
                    tckcmd.CommandType = CommandType.StoredProcedure;
                    tckcmd.Parameters.Add(new SqlParameter("@CreatedBy", Environment.UserName));
                    tckcmd.Parameters.Add(new SqlParameter("@CreatedDate", DateTime.Now));
                    tckcmd.ExecuteNonQuery();
                }

                foreach (var product in products)
                {
                    var cmd = new SqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandText = "SP_SALES_CREATE_PURCHASE";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@ProductId", product.Id));
                    cmd.Parameters.Add(new SqlParameter("@Quantity", product.Quantity));
                    cmd.Parameters.Add(new SqlParameter("@CreatedBy", product.CreatedBy));
                    cmd.Parameters.Add(new SqlParameter("@CreatedDate", product.CreatedDate));

                    cmd.ExecuteNonQuery();
                }               

                response = true;
            }
            catch (Exception ex)
            {

            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
            }

            return response;
        }
    }
}
