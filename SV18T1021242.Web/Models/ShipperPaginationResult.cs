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
    public class ShipperPaginationResult : BasePaginationResult
    {
        /// <summary>
        /// 
        /// </summary>
        public List<Shipper> Data { get; set; }

    }
}