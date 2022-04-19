using SV18T1021242.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SV18T1021242.Web.Models
{
    public class CategoryPaginationResult : BasePaginationResult
    {
        public List<Category> Data { get; set; }

    }
}