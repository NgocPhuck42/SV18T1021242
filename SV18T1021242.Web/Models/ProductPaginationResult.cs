using SV18T1021242.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SV18T1021242.Web.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class ProductPaginationResult : BasePaginationResult
    {
        /// <summary>
        /// Danh sách các 
        /// </summary>
        public List<Product> Data { get; set; }
    }
}