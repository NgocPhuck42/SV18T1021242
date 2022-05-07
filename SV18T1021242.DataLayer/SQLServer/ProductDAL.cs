using SV18T1021242.DomainModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SV18T1021242.DataLayer.SQLServer
{
    public class ProductDAL : _BaseDAL, ICommonDAL<Product>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectionString"></param>
        public ProductDAL(string connectionString) : base(connectionString)
        {

        }

        public int Add(Product data)
        {
            int result = 0;

            using (SqlConnection cn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"insert into Products(ProductName, Unit, Price, Photo)
                                    values(@ProductName, @Unit, @Price, @Photo) 
                                    select scope_identity()
                                ";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = cn;

                cmd.Parameters.AddWithValue("@ProductName", data.ProductName);
                
                cmd.Parameters.AddWithValue("@Unit", data.Unit);
                cmd.Parameters.AddWithValue("@Price", data.Price);
                cmd.Parameters.AddWithValue("@Photo", data.Photo);
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
                FROM    Products
                WHERE    (@searchValue = N'')
                    OR    (
                            (CategoryID LIKE @searchValue)
                         OR (SupplierID LIKE @searchValue)
                         OR (ProductName LIKE @searchValue)
                        )";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = cn;
                cmd.Parameters.AddWithValue("@searchValue", searchValue);

                count = Convert.ToInt32(cmd.ExecuteScalar());

                cn.Close();
            }

            return count;
        }

        public bool Delete(int productID)
        {
            bool result = false;
            using (SqlConnection cn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"Delete  from Products where ProductID = @productID";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = cn;
                cmd.Parameters.AddWithValue("@productID", productID);

                result = cmd.ExecuteNonQuery() > 0;

                cn.Close();
            }
            return result;
        }

        public Product Get(int productID)
        {
            Product result = null;
            using (SqlConnection cn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"select * 
                                    from Products 
	                                 where ProductID = @productID";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = cn;

                cmd.Parameters.AddWithValue("@productID", productID);
                var dbReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                if (dbReader.Read())
                {
                    result = new Product()
                    {
                        ProductID = Convert.ToInt32(dbReader["ProductID"]),
                        ProductName = Convert.ToString(dbReader["ProductName"]),
                        CategoryID = Convert.ToInt32(dbReader["CategoryID"]),
                        SupplierID = Convert.ToInt32(dbReader["SupplierID"]),
                        Unit = Convert.ToString(dbReader["Unit"]),
                        Price = Convert.ToDecimal(dbReader["Price"]),
                        Photo = Convert.ToString(dbReader["Photo"])
                    };
                }

                cn.Close();
            }
            return result;
        }

        public bool InUsed(int productID)
        {
            bool result = false;
            using (SqlConnection cn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"select case when exists(select * from ProductAttributes as pa, ProductPhotos as pp where pa.ProductID = productID and pp.ProductID =productID) then 1 else 0 end";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = cn;

                cmd.Parameters.AddWithValue("@productID", productID);
                result = Convert.ToBoolean(cmd.ExecuteScalar());

                cn.Close();
            }
            return result;
        }

        public IList<Product> List(int page, int pageSize, string searchValue)
        {
            List<Product> data = new List<Product>();
            if (searchValue != "")
                searchValue = "%" + searchValue + "%";
            using (SqlConnection cn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand();

                cmd.CommandText = @"SELECT *
                    FROM
                    (
                        SELECT    *, ROW_NUMBER() OVER (ORDER BY ProductName) AS RowNumber
                        FROM    Products
                        WHERE    (@searchValue = N'')
                            OR    (
                                    (CategoryID LIKE @searchValue)
                                 OR (SupplierID LIKE @searchValue)
                                 OR (ProductName LIKE @searchValue)
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
                    data.Add(new Product()
                    {

                        ProductID = Convert.ToInt32(result["ProductID"]),
                        ProductName = Convert.ToString(result["ProductName"]),
                        CategoryID = Convert.ToInt32(result["CategoryID"]),
                        SupplierID = Convert.ToInt32(result["SupplierID"]),
                        Unit = Convert.ToString(result["Unit"]),
                        Price = Convert.ToDecimal(result["Price"]),
                        Photo = Convert.ToString(result["Photo"])
                    });
                }
                cn.Close();
            }

            return data;
        }

        public bool Update(Product data)
        {
            bool result = false;
            using (SqlConnection cn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"update Products set
                                    ProductName = @ProductName, CategoryID = @CategoryID, SupplierID = @SupplierID , Unit = @Unit, Price = @Price, Photo=@Photo
                                     where ProductID = @ProductID";
                cmd.CommandType = CommandType.Text;

                cmd.Connection = cn;


                cmd.Parameters.AddWithValue("@ProductName", data.ProductName);
                cmd.Parameters.AddWithValue("@CategoryID", data.CategoryID);
                cmd.Parameters.AddWithValue("@SupplierID", data.SupplierID);
                cmd.Parameters.AddWithValue("@Unit", data.Unit);
                cmd.Parameters.AddWithValue("@Price", data.Price);
                cmd.Parameters.AddWithValue("@Photo", data.Photo);

                cmd.Parameters.AddWithValue("@ProductID", data.ProductID);
                result = cmd.ExecuteNonQuery() > 0;

                cn.Close();
            }
            return result;
        }
    }
}
