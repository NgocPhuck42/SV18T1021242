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
    public interface IShipperDAL
    {
        /// <summary>
        /// Tìm kiếm và lấy danh sách shipper cấp dưới dạng phân trang
        /// </summary>
        /// <param name="page">Trang cần xem</param>
        /// <param name="pageSize">Số dòng hiển thị trên mỗi trang</param>
        /// <param name="searchValue">Tên hoặc địa chỉ cần tìm(Tương đối) (nếu là chuỗi rỗng thì lấy tooàn bộ dữ liệu)</param>
        /// <returns></returns>
        IList<Shipper> List(int page, int pageSize, string searchValue);

        /// <summary>
        /// Lấy thông tin 1 shipper dựa vào mã nhà cung cấp
        /// </summary>
        /// <param name="shipperID">Mã loại shipper cần lấy</param>
        /// <returns></returns>
        Shipper Get(int shipperID);
        /// <summary>
        /// Bổ sung 1 shipper mới. Hàm trả về mã
        /// nhà cung cấp được bổ sung.
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        int Add(Shipper data);
        /// <summary>
        /// Cập nhật thông tin của một shipper
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        bool Update(Shipper data);
        /// <summary>
        /// Xóa một shipper dựa vào mã nhà cung cấp
        /// Lưu ý: không xóa nếu shipper đã được sử dụng.
        /// </summary>
        /// <param name="supplierID"></param>
        /// <returns></returns>
        bool Delete(int shipperID);
        /// <summary>
        /// Đếm số shipper tìm được
        /// </summary>
        /// <param name="searchValue">Tên hoặc địa chỉ cần tìm</param>
        /// <returns></returns>
        int Count(string searchValue);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="supplierID"></param>
        /// <returns></returns>
        bool InUsed(int shipperID);
    }
}
