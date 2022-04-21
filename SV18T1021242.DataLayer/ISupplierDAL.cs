using SV18T1021242.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SV18T1021242.DataLayer
{
    /// <summary>
    /// 
    /// </summary>
    public interface ISupplierDAL
    {

        /// <summary>
        /// Tìm kiếm và lấy danh sách nhà cung cấp dưới dạng phân trang
        /// </summary>
        /// <param name="page">Trang cần xem</param>
        /// <param name="pageSize">Số dòng hiển thị trên mỗi trang</param>
        /// <param name="searchValue">Tên hoặc địa chỉ cần tìm(Tương đối) (nếu là chuỗi rỗng thì lấy tooàn bộ dữ liệu)</param>
        /// <returns></returns>
        IList<Supplier> List(int page, int pageSize, string searchValue);

        /// <summary>
        /// Lấy thông tin 1 nhà cung cấp dựa vào mã nhà cung cấp
        /// </summary>
        /// <param name="customerID">Mã loại hàng cần lấy</param>
        /// <returns></returns>
        Supplier Get(int supplierID);
        /// <summary>
        /// Bổ sung 1 nhà cung cấp mới. Hàm trả về mã
        /// nhà cung cấp được bổ sung.
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        int Add(Supplier data);
        /// <summary>
        /// Cập nhật thông tin của một nhà cung cấp
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        bool Update(Supplier data);
        /// <summary>
        /// Xóa một nhà cung cấp dựa vào mã nhà cung cấp
        /// Lưu ý: không xóa nếu nhà cung cấp đã được sử dụng.
        /// </summary>
        /// <param name="supplierID"></param>
        /// <returns></returns>
        bool Delete(int supplierID);
        /// <summary>
        /// Đếm số nhà cung cấp tìm được
        /// </summary>
        /// <param name="searchValue">Tên hoặc địa chỉ cần tìm</param>
        /// <returns></returns>
        int Count(string searchValue);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="supplierID"></param>
        /// <returns></returns>
        bool InProduct(int supplierID);
    }
}
