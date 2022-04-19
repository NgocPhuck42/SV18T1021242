using SV18T1021242.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SV18T1021242.DataLayer.FakeDB
{
    /// <summary>
    /// Cài đặt chức năng xử lý dữ liệu trên loại hàng
    /// theo kiểu "fake"
    /// </summary>
    public class CategoryDAL : ICategoryDAL
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public int Add(Category data)
        {
            throw new NotImplementedException();
        }

        public int Count(string searchValue)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="categoryID"></param>
        /// <returns></returns>
        public bool Delete(int categoryID)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="categoryID"></param>
        /// <returns></returns>
        public Category Get(int categoryID)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IList<Category> List()
        {
            List<Category> data = new List<Category>();

            data.Add(new Category() { 
                CategoryID = 1,
                CategoryName = "Phúc cute",
                Description = "Có ny"
            });
            data.Add(new Category()
            {
                CategoryID = 2,
                CategoryName = "Dũng xương",
                Description = "Không có ny"
            });

            return data;
        }

        public IList<Category> List(int page, int pageSize, string searchValue)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public bool Update(Category data)
        {
            throw new NotImplementedException();
        }
    }
}
