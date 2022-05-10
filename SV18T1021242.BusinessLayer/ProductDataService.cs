using SV18T1021242.DataLayer;
using SV18T1021242.DomainModel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SV18T1021242.BusinessLayer
{
    /// <summary>
    /// 
    /// </summary>
    public static class ProductDataService
    {
        private static readonly IProductDAL productDB;

        static ProductDataService()
        {
            string provider = ConfigurationManager.ConnectionStrings["DB"].ProviderName;

            string connectionString = ConfigurationManager.ConnectionStrings["DB"].ConnectionString;

            if (provider == "SQLServer")
            {
                productDB = new DataLayer.SQLServer.ProductDAL(connectionString);
            }
        }

        #region Products
        /// <summary>
        /// 
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="searchValue"></param>
        /// <param name="rowCount"></param>
        /// <returns></returns>
        public static List<Product> ListOfProducts(int page, int pageSize, string searchValue, int categoryID, int supplierID,out int rowCount)
        {
            rowCount = productDB.Count(searchValue, categoryID, supplierID);
            return productDB.List(page, pageSize, searchValue, categoryID, supplierID).ToList();
        }
        public static Product GetProduct(int productID)
        {
            return productDB.Get(productID);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static List<Product> ListOfProducts()
        {
            return productDB.List().ToList();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static int AddProduct(Product data)
        {
            return productDB.Add(data);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static bool UpdateProduct(Product data)
        {
            return productDB.Update(data);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="productID"></param>
        /// <returns></returns>
        public static bool DeleteProduct(int productID)
        {
            if (productDB.InUsed(productID))
                return false;
            return productDB.Delete(productID);
        }
        public static bool InUsed(int productID)
        {
            return productDB.InUsed(productID);
        }
        #endregion

        #region ProductAttributes
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static List<ProductAttribute> ListOfProductAttributes(int productID)
        {
            return productDB.ListProductAttributes(productID).ToList();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static ProductAttribute GetOfProductAttribute(int attributeID)
        {
            return productDB.GetProductAttribute(attributeID);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static int AddProductAttribute(ProductAttribute data)
        {
            return productDB.AddAttribute(data);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static bool UpdateProductAttribute(ProductAttribute data)
        {
            return productDB.UpdateAttribute(data);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="attributeID"></param>
        /// <returns></returns>
        public static bool DeleteProductAttribute(int attributeID)
        {
            return productDB.DeleteAttribute(attributeID);
        }
        #endregion

        #region ProductPhotos
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static List<ProductPhoto> ListOfProductPhotos(int productID)
        {
            return productDB.ListProductPhotos(productID).ToList();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static ProductPhoto GetOfProductPhoto(int photoID)
        {
            return productDB.GetProductPhoto(photoID);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static int AddProductPhoto(ProductPhoto data)
        {
            return productDB.AddProductPhoto(data);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static bool UpdateProductPhoto(ProductPhoto data)
        {
            return productDB.UpdateProductPhoto(data);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="photoID"></param>
        /// <returns></returns>
        public static bool DeleteProductPhoto(int photoID)
        {
            return productDB.DeleteProductPhoto(photoID);
        }
        #endregion

    }
}
