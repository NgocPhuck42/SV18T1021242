﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SV18T1021242.DomainModel;

namespace SV18T1021242.DataLayer
{
    /// <summary>
    /// Định nghĩa các phép xử lý dữ liệu liên quan đến loại hàng
    /// </summary>
    public interface ICategoryDAL
    {
        /// <summary>
        /// Lấy danh sách các loại hàng
        /// </summary>
        /// <returns></returns>
        IList<Category> List(int page, int pageSize, string searchValue);

        /// <summary>
        /// Lấy thông tin 1 loại hàng dựa vào mã loại hàng
        /// </summary>
        /// <param name="categoryID">Mã loại hàng cần lấy</param>
        /// <returns></returns>
        Category Get(int categoryID);
        /// <summary>
        /// Bổ sung 1 loại hàng mới. Hàm trả về mã của
        /// loại hàng được bổ sung.
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        int Add(Category data);
        /// <summary>
        /// Cập nhật thông tin của một loại hàng
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        bool Update(Category data);
        /// <summary>
        /// Xóa một loại hàng dựa vào mã loại hàng
        /// Lưu ý: không xóa nếu loại hàng đã được sử dụng.
        /// </summary>
        /// <param name="categoryID"></param>
        /// <returns></returns>
        bool Delete(int categoryID);
        /// <summary>
        /// Đếm số khách hàng tìm được
        /// </summary>
        /// <param name="searchValue">Tên hoặc địa chỉ cần tìm</param>
        /// <returns></returns>
        int Count(string searchValue);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="categoryID"></param>
        /// <returns></returns>
        bool InCategory(int categoryID);
        IList<Category> ListOfDescription();

    }
}
