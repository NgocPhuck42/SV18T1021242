using SV18T1021242.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace SV18T1021242.DataLayer.SQLServer
{
    /// <summary>
    /// 
    /// </summary>
    public class CategoryDAL : _BaseDAL, ICommonDAL<Category>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectionString"></param>
        public CategoryDAL(string connectionString) : base(connectionString)
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public int Add(Category data)
        {
            int result = 0;

            using (SqlConnection cn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"insert into Categories(CategoryName, Description)
                                    values(@CategoryName, @Description) 
                                    select scope_identity()
                                ";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = cn;

                cmd.Parameters.AddWithValue("@CategoryName", data.CategoryName);
                cmd.Parameters.AddWithValue("@Description", data.Description);


                result = Convert.ToInt32(cmd.ExecuteScalar());

                cn.Close();
            }

            return result;
        }

        public int Count(string searchValue)
        {
            int count = 0;
            if (searchValue != "")
                searchValue = "%" + searchValue + "%";
            using (SqlConnection cn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"SELECT Count(*)
                FROM    Categories
                WHERE    (@searchValue = N'')
                    OR    (
                            (CategoryName LIKE @searchValue)
                           OR(Description LIKE @searchValue)
                        )";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = cn;
                cmd.Parameters.AddWithValue("@searchValue", searchValue);

                count = Convert.ToInt32(cmd.ExecuteScalar());

                cn.Close();
            }

            return count;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="categoryID"></param>
        /// <returns></returns>
        public bool Delete(int categoryID)
        {
            bool result = false;
            using (SqlConnection cn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"Delete  from Categories where CategoryID = @categoryID";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = cn;
                cmd.Parameters.AddWithValue("@categoryID", categoryID);

                result = cmd.ExecuteNonQuery() > 0;

                cn.Close();
            }
            return result;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="categoryID"></param>
        /// <returns></returns>
        public Category Get(int categoryID)
        {
            Category result = null;
            using (SqlConnection cn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"select * from Categories where CategoryID = @categoryID";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = cn;

                cmd.Parameters.AddWithValue("@categoryID", categoryID);
                var dbReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                if (dbReader.Read())
                {
                    result = new Category()
                    {
                        CategoryID = Convert.ToInt32(dbReader["CategoryID"]),
                        CategoryName = Convert.ToString(dbReader["CategoryName"]),
                        Description = Convert.ToString(dbReader["Description"]),

                    };
                }

                cn.Close();
            }
            return result;
        }

        public bool InUsed(int categoryID)
        {
            bool result = false;
            using (SqlConnection cn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"select case when exists(select * from Products where CategoryID = @categoryID) then 1 else 0 end";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = cn;

                cmd.Parameters.AddWithValue("@categoryID", categoryID);
                result = Convert.ToBoolean(cmd.ExecuteScalar());

                cn.Close();
            }
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IList<Category> List(int page, int pageSize, string searchValue)
        {
            List<Category> data = new List<Category>();
            if (searchValue != "")
                searchValue = "%" + searchValue + "%";
            using (SqlConnection cn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand();

                cmd.CommandText = @"SELECT *
                    FROM
                    (
                        SELECT    *, ROW_NUMBER() OVER (ORDER BY CategoryName) AS RowNumber
                        FROM    Categories
                        WHERE    (@searchValue = N'')
                            OR    (
                                    (CategoryName LIKE @searchValue)
                                     OR(Description LIKE @searchValue)
                                )
                    ) AS t
                    WHERE (@PageSize=0) or t.RowNumber BETWEEN (@page - 1) * @pageSize + 1 AND @page * @pageSize;";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = cn;
                cmd.Parameters.AddWithValue("@page", page);
                cmd.Parameters.AddWithValue("@pageSize", pageSize);
                cmd.Parameters.AddWithValue("@searchValue", searchValue);
                var result = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (result.Read())
                {
                    data.Add(new Category()
                    {
                        CategoryID = Convert.ToInt32(result["CategoryID"]),
                        CategoryName = Convert.ToString(result["CategoryName"]),
                        Description = Convert.ToString(result["Description"]),
                        
                    });
                }
                cn.Close();
            }

            return data;
        }

        public IList<Category> List()
        {
            List<Category> data = new List<Category>();
          
            using (SqlConnection cn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand();

                cmd.CommandText = @"SELECT * From Categories";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = cn;

                var result = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (result.Read())
                {
                    data.Add(new Category()
                    {
                        CategoryID = Convert.ToInt32(result["CategoryID"]),
                        CategoryName = Convert.ToString(result["CategoryName"]),
                        Description = Convert.ToString(result["Description"]),

                    });
                }
                cn.Close();
            }

            return data;
        }

        //public IList<Category> ListOfDescription()
        //{
        //    List<Category> data = new List<Category>();

        //    using (SqlConnection cn = OpenConnection())
        //    {
        //        SqlCommand cmd = new SqlCommand();

        //        cmd.CommandText = @"SELECT Description from Categories ";
        //        cmd.CommandType = CommandType.Text;
        //        cmd.Connection = cn;
        //        var result = cmd.ExecuteReader(CommandBehavior.CloseConnection);
        //        while (result.Read())
        //        {
        //            data.Add(new Category()
        //            {
        //                Description = Convert.ToString(result["Description"]),

        //            });
        //        }
        //        cn.Close();
        //    }
        //    return data;
        //}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public bool Update(Category data)
        {
            bool result = false;
            using (SqlConnection cn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"update Categories set
                                    CategoryName = @CategoryName, Description = @Description where CategoryID = @CategoryID";
                cmd.CommandType = CommandType.Text;

                cmd.Connection = cn;


                cmd.Parameters.AddWithValue("@CategoryName", data.CategoryName);
                cmd.Parameters.AddWithValue("@Description", data.Description);


                cmd.Parameters.AddWithValue("@CategoryID", data.CategoryID);
                result = cmd.ExecuteNonQuery() > 0;

                cn.Close();
            }
            return result;
        }
    }
}
