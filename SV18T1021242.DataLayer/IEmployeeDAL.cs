using SV18T1021242.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SV18T1021242.DataLayer
{
    public interface IEmployeeDAL
    {
        /// <summary>
        /// Tìm kiếm và lấy danh sách khách hàng dưới dạng phân trang
        /// </summary>
        /// <param name="page">Trang cần xem</param>
        /// <param name="pageSize">Số dòng hiển thị trên mỗi trang</param>
        /// <param name="searchValue">Tên hoặc địa chỉ cần tìm(Tương đối) (nếu là chuỗi rỗng thì lấy tooàn bộ dữ liệu)</param>
        /// <returns></returns>
        IList<Employee> List(int page, int pageSize, string searchValue);

        /// <summary>
        /// Lấy thông tin 1 khách hàng dựa vào mã khách hàng
        /// </summary>
        /// <param name="employeeID">Mã loại hàng cần lấy</param>
        /// <returns></returns>
        Employee Get(int employeeID);
        /// <summary>
        /// Bổ sung 1 khách hàng mới. Hàm trả về mã
        /// khách hàng được bổ sung.
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        int Add(Employee data);
        /// <summary>
        /// Cập nhật thông tin của một khách hàng
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        bool Update(Employee data);
        /// <summary>
        /// Xóa một khách hàng dựa vào mã khách hàng
        /// Lưu ý: không xóa nếu khách hàng đã được sử dụng.
        /// </summary>
        /// <param name="employeeID"></param>
        /// <returns></returns>
        bool Delete(int employeeID);
        /// <summary>
        /// Đếm số khách hàng tìm được
        /// </summary>
        /// <param name="searchValue">Tên hoặc địa chỉ cần tìm</param>
        /// <returns></returns>
        int Count(string searchValue);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="employeeID"></param>
        /// <returns></returns>
        bool InUsed(int employeeID);
    }
}
