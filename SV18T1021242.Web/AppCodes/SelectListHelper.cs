using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SV18T1021242.BusinessLayer;
using SV18T1021242.DomainModel;
namespace SV18T1021242.Web
{
    /// <summary>
    /// Cung cấp 1 hàm tiện ích liên quan đến danh sách chọn trong thẻ select
    /// </summary>
    public static class SelectListHelper
    {
        /// <summary>
        /// Hiển thị danh sách quốc gia
        /// </summary>
        /// <returns></returns>
        public static List<SelectListItem> Countries()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            list.Add(new SelectListItem(){ Value="", Text= "---Chọn quốc gia---" });
            foreach (var c in CommonDataService.ListOfCountries())
            {
                list.Add(new SelectListItem()
                {
                    Value = c.CountryName,
                    Text = c.CountryName
                });
            }

            return list;
        }
    }
}