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
    public class ProductDAL : _BaseDAL, IProductDAL
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
                cmd.CommandText = @"insert into Products(ProductName, CategoryID, SupplierID, Unit, Price, Photo)
                                        values(@ProductName, @CategoryID, @SupplierID , @Unit, @Price, @Photo) 
                                        select scope_identity()
                                    ";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = cn;

                cmd.Parameters.AddWithValue("@ProductName", data.ProductName);
                cmd.Parameters.AddWithValue("@CategoryID", data.CategoryID);
                cmd.Parameters.AddWithValue("@SupplierID", data.SupplierID);
                cmd.Parameters.AddWithValue("@Unit", data.Unit);
                cmd.Parameters.AddWithValue("@Price", data.Price);
                cmd.Parameters.AddWithValue("@Photo", data.Photo);
                result = Convert.ToInt32(cmd.ExecuteScalar());

                cn.Close();
            }

            return result;
        }

        public int AddAttribute(ProductAttribute data)
        {
            int result = 0;

            using (SqlConnection cn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"insert into ProductAttributes(ProductID, AttributeName,  AttributeValue, DisplayOrder)
                                        values(@ProductID, @AttributeName, @AttributeValue , @DisplayOrder) 
                                        select scope_identity()
                                    ";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = cn;

                cmd.Parameters.AddWithValue("@ProductID", data.ProductID);
                cmd.Parameters.AddWithValue("@AttributeName", data.AttributeName);
                cmd.Parameters.AddWithValue("@AttributeValue", data.AttributeValue);
                cmd.Parameters.AddWithValue("@DisplayOrder", data.DisplayOrder);
                result = Convert.ToInt32(cmd.ExecuteScalar());

                cn.Close();
            }

            return result;
        }

        public int AddProductPhoto(ProductPhoto data)
        {
            int result = 0;

            using (SqlConnection cn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"insert into ProductPhotos(ProductID,  Photo, Description, DisplayOrder, IsHidden)
                                        values(@ProductID, @Photo , @Description, @DisplayOrder, @IsHidden) 
                                        select scope_identity()
                                    ";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = cn;

                cmd.Parameters.AddWithValue("@ProductID", data.ProductID);
                cmd.Parameters.AddWithValue("@Photo", data.Photo);
                cmd.Parameters.AddWithValue("@Description", data.Description);
                cmd.Parameters.AddWithValue("@DisplayOrder", data.DisplayOrder);
                cmd.Parameters.AddWithValue("@IsHidden", data.IsHidden);

                result = Convert.ToInt32(cmd.ExecuteScalar());

                cn.Close();
            }

            return result;
        }

        public int Count(string searchValue, int categoryID, int supplierID)
        {
            int count = 0;
            if (searchValue != "")
                searchValue = "%" + searchValue + "%";
            using (SqlConnection cn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"SELECT    COUNT(*)
                                    FROM    Products
                                    WHERE	((@searchValue = N'') OR (ProductName LIKE @searchValue))
		                                AND	((@categoryID = 0) OR (CategoryID = @categoryID))
		                                AND	((@supplierID = 0) OR (SupplierID = @supplierID))
                            ";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = cn;
                cmd.Parameters.AddWithValue("@searchValue", searchValue);
                cmd.Parameters.AddWithValue("@categoryID", categoryID);
                cmd.Parameters.AddWithValue("@supplierID", supplierID);

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

        public bool DeleteAttribute(int attributeID)
        {
            bool result = false;
            using (SqlConnection cn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"Delete  from ProductAttributes where AttributeID = @attributeID";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = cn;
                cmd.Parameters.AddWithValue("@attributeID", attributeID);

                result = cmd.ExecuteNonQuery() > 0;

                cn.Close();
            }
            return result;
        }

        public bool DeleteProductPhoto(int photoID)
        {
            bool result = false;
            using (SqlConnection cn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"Delete  from ProductPhotos where photoID = @photoID";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = cn;
                cmd.Parameters.AddWithValue("@photoID", photoID);

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

        public ProductAttribute GetProductAttribute(int attributeID)
        {
            ProductAttribute result = null;
            using (SqlConnection cn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"select * 
                                        from ProductAttributes 
                                      where attributeID = @attributeID";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = cn;

                cmd.Parameters.AddWithValue("@attributeID", attributeID);
                var dbReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                if (dbReader.Read())
                {
                    result = new ProductAttribute()
                    {
                        AttributeID = Convert.ToInt32(dbReader["AttributeID"]),
                        ProductID = Convert.ToInt32(dbReader["ProductID"]),
                        AttributeName = Convert.ToString(dbReader["AttributeName"]),
                        AttributeValue = Convert.ToString(dbReader["AttributeValue"]),
                        DisplayOrder = Convert.ToInt32(dbReader["DisplayOrder"])
                    };
                }

                cn.Close();
            }
            return result;
        }

        public ProductPhoto GetProductPhoto(int photoID)
        {
            ProductPhoto result = null;
            using (SqlConnection cn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"select * 
                                        from ProductPhotos 
                                      where photoID = @photoID";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = cn;

                cmd.Parameters.AddWithValue("@photoID", photoID);
                var dbReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                if (dbReader.Read())
                {
                    result = new ProductPhoto()
                    {
                        PhotoID = Convert.ToInt32(dbReader["PhotoID"]),
                        ProductID = Convert.ToInt32(dbReader["ProductID"]),
                        Photo = Convert.ToString(dbReader["Photo"]),
                        Description = Convert.ToString(dbReader["Description"]),
                        DisplayOrder = Convert.ToInt32(dbReader["DisplayOrder"]),
                        IsHidden = Convert.ToBoolean(dbReader["IsHidden"]),
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
                cmd.CommandText = @"select case when exists(select * from ProductAttributes as pa , ProductPhotos as pp  where pa.ProductID = @productID and pp.ProductID =@productID) then 1 else 0 end";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = cn;

                cmd.Parameters.AddWithValue("@productID", productID);
                result = Convert.ToBoolean(cmd.ExecuteScalar());

                cn.Close();
            }
            return result;
        }

        public IList<Product> List(int page, int pageSize, string searchValue, int categoryID, int supplierID)
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
                                        WHERE	((@searchValue = N'') OR (ProductName LIKE @searchValue))
		                                    AND	((@categoryID = 0) OR (CategoryID = @categoryID))
		                                    AND	((@supplierID = 0) OR (SupplierID = @supplierID))
                                    ) AS t
                                    WHERE (@pageSize = 0) OR (t.RowNumber BETWEEN (@page - 1) * @pageSize + 1 AND @page * @pageSize)";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = cn;
                cmd.Parameters.AddWithValue("@page", page);
                cmd.Parameters.AddWithValue("@pageSize", pageSize);
                cmd.Parameters.AddWithValue("@searchValue", searchValue);
                cmd.Parameters.AddWithValue("@categoryId", categoryID);
                cmd.Parameters.AddWithValue("@supplierId", supplierID);

                var result = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                while (result.Read())
                {
                    data.Add(new Product()
                    {

                        ProductID = Convert.ToInt32(result["ProductID"]),
                        ProductName = Convert.ToString(result["ProductName"]),
                        Unit = Convert.ToString(result["Unit"]),
                        Price = Convert.ToDecimal(result["Price"]),
                        Photo = Convert.ToString(result["Photo"])
                    });
                }
                cn.Close();
            }

            return data;
        }

        public List<ProductAttribute> ListProductAttributes(int productID)
        {
            List<ProductAttribute> data = new List<ProductAttribute>();
            using (SqlConnection cn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand();

                cmd.CommandText = @"SELECT * from ProductAttributes where productID=@productID ";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = cn;
                cmd.Parameters.AddWithValue("@productID", productID);

                var result = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                while (result.Read())
                {
                    data.Add(new ProductAttribute()
                    {
                        AttributeID = Convert.ToInt32(result["AttributeID"]),
                        ProductID = Convert.ToInt32(result["ProductID"]),
                        AttributeName = Convert.ToString(result["AttributeName"]),
                        AttributeValue = Convert.ToString(result["AttributeValue"]),
                        DisplayOrder = Convert.ToInt32(result["DisplayOrder"])
                    });
                }
                cn.Close();
            }

            return data;
        }

        public List<ProductPhoto> ListProductPhotos(int productID)
        {
            List<ProductPhoto> data = new List<ProductPhoto>();
            using (SqlConnection cn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand();

                cmd.CommandText = @"SELECT * from ProductPhotos where productID=@productID ";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = cn;
                cmd.Parameters.AddWithValue("@productID", productID);

                var result = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                while (result.Read())
                {
                    data.Add(new ProductPhoto()
                    {

                        PhotoID = Convert.ToInt32(result["PhotoID"]),
                        ProductID = Convert.ToInt32(result["ProductID"]),
                        Photo = Convert.ToString(result["Photo"]),
                        Description = Convert.ToString(result["Description"]),
                        DisplayOrder = Convert.ToInt32(result["DisplayOrder"]),
                        IsHidden = Convert.ToBoolean(result["IsHidden"]),

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

        public bool UpdateAttribute(ProductAttribute data)
        {
            bool result = false;
            using (SqlConnection cn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"update ProductAttributes set
                                        ProductID = @ProductID, AttributeName = @AttributeName, AttributeValue = @AttributeValue , DisplayOrder = @DisplayOrder
                                         where AttributeID = @AttributeID";
                cmd.CommandType = CommandType.Text;

                cmd.Connection = cn;


                cmd.Parameters.AddWithValue("@ProductID", data.ProductID);
                cmd.Parameters.AddWithValue("@AttributeName", data.AttributeName);
                cmd.Parameters.AddWithValue("@AttributeValue", data.AttributeValue);
                cmd.Parameters.AddWithValue("@DisplayOrder", data.DisplayOrder);


                cmd.Parameters.AddWithValue("@AttributeID", data.AttributeID);
                result = cmd.ExecuteNonQuery() > 0;

                cn.Close();
            }
            return result;
        }

        public bool UpdateProductPhoto(ProductPhoto data)
        {
            bool result = false;
            using (SqlConnection cn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"update ProductPhotos set
                                        ProductID = @ProductID, Photo = @Photo, Description = @Description , DisplayOrder = @DisplayOrder , IsHidden = @IsHidden
                                         where PhotoID = @PhotoID";
                cmd.CommandType = CommandType.Text;

                cmd.Connection = cn;


                cmd.Parameters.AddWithValue("@ProductID", data.ProductID);
                cmd.Parameters.AddWithValue("@Photo", data.Photo);
                cmd.Parameters.AddWithValue("@Description", data.Description);
                cmd.Parameters.AddWithValue("@DisplayOrder", data.DisplayOrder);
                cmd.Parameters.AddWithValue("@IsHidden ", data.IsHidden);

                cmd.Parameters.AddWithValue("@PhotoID", data.PhotoID);
                result = cmd.ExecuteNonQuery() > 0;

                cn.Close();
            }
            return result;
        }
    }
}
