using SV18T1021242.BusinessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SV18T1021242.Web.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Authorize]
    [RoutePrefix("shipper")]
    public class ShipperController : Controller
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        // GET: Shipper
        public ActionResult Index(int page = 1, string searchValue = "")
        {
            {
                int pageSize = 10;
                int rowCount = 0;

                var data = CommonDataService.ListOfShippers(page, pageSize, searchValue, out rowCount);
                Models.ShipperPaginationResult model = new Models.ShipperPaginationResult
                {
                    Page = page,
                    PageSize = pageSize,
                    RowCount = rowCount,
                    SearchValue = searchValue,
                    Data = data
                };
                return View(model);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Create()
        {
            ViewBag.Title = "Bổ sung người giao hàng";
            return View();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="customerID"></param>
        /// <returns></returns>
        [Route("edit/{shipperID}")]
        public ActionResult Edit(int shipperID)
        {
            ViewBag.Title = "Cập nhật người giao hàng";
            return View("Create");
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="customerID"></param>
        /// <returns></returns>
        [Route("delete/{shipperID}")]
        public ActionResult Delete(int shipperID)
        {
            return View();
        }
    }
}