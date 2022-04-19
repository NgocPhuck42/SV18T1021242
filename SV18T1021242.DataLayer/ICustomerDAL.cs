using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SV18T1021242.DomainModel;

namespace SV18T1021242.DataLayer
{
    /// <summary>
    /// Xử lý các dữ liệu về khách hàng
    /// </summary>
    public interface ICustomerDAL
    {
        /// <summary>
        /// Tìm kiếm và lấy danh sách khách hàng dưới dạng phân trang
        /// </summary>
        /// <param name="page">Trang cần xem</param>
        /// <param name="pageSize">Số dòng hiển thị trên mỗi trang</param>
        /// <param name="searchValue">Tên hoặc địa chỉ cần tìm(Tương đối) (nếu là chuỗi rỗng thì lấy tooàn bộ dữ liệu)</param>
        /// <returns></returns>
        IList<Customer> List(int page, int pageSize, string searchValue);

        /// <summary>
        /// Lấy thông tin 1 khách hàng dựa vào mã khách hàng
        /// </summary>
        /// <param name="customerID">Mã loại hàng cần lấy</param>
        /// <returns></returns>
        Customer Get(int customerID);
        /// <summary>
        /// Bổ sung 1 khách hàng mới. Hàm trả về mã
        /// khách hàng được bổ sung.
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        int Add(Customer data);
        /// <summary>
        /// Cập nhật thông tin của một khách hàng
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        bool Update(Customer data);
        /// <summary>
        /// Xóa một khách hàng dựa vào mã khách hàng
        /// Lưu ý: không xóa nếu khách hàng đã được sử dụng.
        /// </summary>
        /// <param name="customerID"></param>
        /// <returns></returns>
        bool Delete(int customerID);
        /// <summary>
        /// Đếm số khách hàng tìm được
        /// </summary>
        /// <param name="searchValue">Tên hoặc địa chỉ cần tìm</param>
        /// <returns></returns>
        int Count(string searchValue);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="customerID"></param>
        /// <returns></returns>
        bool InUsed(int customerID);

    }
}
