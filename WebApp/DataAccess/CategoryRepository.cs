using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using Models;

namespace DataAccess
{   
    public class CategoryRepository : ConnectionElement
    {
        /// <summary>
        /// Get the last 100 categories that were modfied
        /// </summary>
        /// <returns>List of categories</returns>
        public static List<Category> GetAll()
        {
            SqlConnection conn = null;
            var list = new List<Category>();
            
            try
            {
                conn = OpenConnection();
                var cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "SP_CATEGORY_GET_ALL";
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
        /// Create a new category
        /// </summary>
        /// <param name="category">New category</param>
        /// <returns>status</returns>
        public static bool Create(Category category)
        {
            SqlConnection conn = null;
            var response = false;

            try
            {
                conn = OpenConnection();
                var cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "SP_CATEGORY_CREATE";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Name", category.Name));
                cmd.Parameters.Add(new SqlParameter("@Description", category.Description));
                cmd.Parameters.Add(new SqlParameter("@CreatedBy", category.CreatedBy));
                cmd.Parameters.Add(new SqlParameter("@CreatedDate", category.CreatedDate));

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
        /// Update a existing category
        /// </summary>
        /// <param name="category">Updated category</param>
        /// <returns>status</returns>
        public static bool Edit(Category category)
        {
            SqlConnection conn = null;
            var response = false;

            try
            {
                conn = OpenConnection();
                var cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "SP_CATEGORY_EDIT";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Id", category.Id));
                cmd.Parameters.Add(new SqlParameter("@Name", category.Name));
                cmd.Parameters.Add(new SqlParameter("@Description", category.Description));
                cmd.Parameters.Add(new SqlParameter("@LastUpdatedBy", category.CreatedBy));
                cmd.Parameters.Add(new SqlParameter("@LastUpdatedDate", category.CreatedDate));

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
        /// Delete a existing category
        /// </summary>
        /// <param name="category">Delete category</param>
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
                cmd.CommandText = "SP_CATEGORY_DELETE";
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
        /// <returns>List of Categories</returns>
        private static List<Category> BuildList(SqlDataReader dr)
        {
            var list = new List<Category>();

            while (dr.Read())
            {
                var category = new Category();
                category.Id = Convert.ToInt32(dr["Id"].ToString());
                category.Name = dr["Name"].ToString();
                category.Description = dr["Description"].ToString();
                category.CreatedDate = Convert.ToDateTime(dr["LastUpdatedDate"].ToString());

                list.Add(category);
            }

            return list;
        }
    }   
}
