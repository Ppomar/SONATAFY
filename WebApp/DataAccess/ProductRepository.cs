using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using System.Data;
using System.Data.SqlClient;

namespace DataAccess
{
    public class ProductRepository : ConnectionElement
    {
        /// <summary>
        /// Get the last 100 products that were modified
        /// </summary>
        /// <returns>List of products</returns>
        public static List<Product> GetAll()
        {
            SqlConnection conn = null;
            var list = new List<Product>();

            try
            {
                conn = OpenConnection();
                var cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "SP_PRODUCT_GET_ALL";
                cmd.CommandType = CommandType.StoredProcedure;
                var dr = cmd.ExecuteReader();

                list = BuildList(dr);
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

            return list;
        }

        /// <summary>
        /// Get the last 100 products that were modified by name
        /// </summary>
        /// <returns>List of products</returns>
        public static List<Product> FilterProducts(string productName)
        {
            SqlConnection conn = null;
            var list = new List<Product>();

            try
            {
                conn = OpenConnection();
                var cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "SP_PRODUCT_FILTER_NAME";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Name", productName)); ;

                var dr = cmd.ExecuteReader();

                list = BuildList(dr);
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

            return list;
        }

        /// <summary>
        /// Get product by Id
        /// </summary>
        /// <returns>Get a product</returns>
        public static List<Product> GetById(int id)
        {
            SqlConnection conn = null;
            var list = new List<Product>();

            try
            {
                conn = OpenConnection();
                var cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "SP_PRODUCT_GET_BY_ID";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Id", id)); ;

                var dr = cmd.ExecuteReader();

                list = BuildList(dr);
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

            return list;
        }

        /// <summary>
        /// Create a new product
        /// </summary>
        /// <param name="category">New product</param>
        /// <returns>status</returns>
        public static bool Create(Product product)
        {
            SqlConnection conn = null;
            var response = false;

            try
            {
                conn = OpenConnection();
                var cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "SP_PRODUCT_CREATE";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Name", product.Name));
                cmd.Parameters.Add(new SqlParameter("@Presentation", product.Presentation));
                cmd.Parameters.Add(new SqlParameter("@Price", product.Price));
                cmd.Parameters.Add(new SqlParameter("@CategoryId", product.CategoryId));
                cmd.Parameters.Add(new SqlParameter("@CreatedBy", product.CreatedBy));
                cmd.Parameters.Add(new SqlParameter("@CreatedDate", product.CreatedDate));

                cmd.ExecuteNonQuery();

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

        /// <summary>
        /// Update a existing product
        /// </summary>
        /// <param name="category">Updated product</param>
        /// <returns>status</returns>
        public static bool Edit(Product product)
        {
            SqlConnection conn = null;
            var response = false;

            try
            {
                conn = OpenConnection();
                var cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "SP_PRODUCT_EDIT";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Id", product.Id));
                cmd.Parameters.Add(new SqlParameter("@Name", product.Name));
                cmd.Parameters.Add(new SqlParameter("@Presentation", product.Presentation));
                cmd.Parameters.Add(new SqlParameter("@Price", product.Price));
                cmd.Parameters.Add(new SqlParameter("@CategoryId", product.CategoryId));
                cmd.Parameters.Add(new SqlParameter("@LastUpdatedBy", product.CreatedBy));
                cmd.Parameters.Add(new SqlParameter("@LastUpdatedDate", product.CreatedDate));

                cmd.ExecuteNonQuery();

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

        /// <summary>
        /// Delete a existing product
        /// </summary>
        /// <param name="category">Delete product</param>
        /// <returns>status</returns>
        public static bool Delete(int id)
        {
            SqlConnection conn = null;
            var response = false;

            try
            {
                conn = OpenConnection();
                var cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "SP_PRODUCT_DELETE";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Id", id));

                cmd.ExecuteNonQuery();

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

        /// <summary>
        /// Build the list using dataset from database
        /// </summary>
        /// <param name="dr">DataReader with dataset from database</param>
        /// <returns>List of Products</returns>
        private static List<Product> BuildList(SqlDataReader dr)
        {
            var list = new List<Product>();

            while (dr.Read())
            {
                var product = new Product();
                product.Id = Convert.ToInt32(dr["Id"].ToString());
                product.Name = dr["Name"].ToString();
                product.Presentation = dr["Presentation"].ToString();
                product.Price = Convert.ToDecimal(dr["Price"].ToString());
                product.CategoryId = Convert.ToInt32(dr["CategoryId"].ToString());
                product.CategoryName = dr["CategoryName"].ToString();
                product.CreatedDate = Convert.ToDateTime(dr["LastUpdatedDate"].ToString());

                list.Add(product);
            }

            return list;
        }
    }
}
