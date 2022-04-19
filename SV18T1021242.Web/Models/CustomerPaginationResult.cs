using SV18T1021242.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SV18T1021242.Web.Models
{
    /// <summary>
    /// Lưu kết quả tìm kiếm, phân trang cho khách hàng
    /// </summary>
    public class CustomerPaginationResult : BasePaginationResult
    {
        /// <summary>
        /// Danh sách các khách hàng
        /// </summary>
        public List<Customer> Data { get; set; }

    }
}