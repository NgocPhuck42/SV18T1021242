using SV18T1021242.BusinessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SV18T1021242.Web.Controllers
{
    [Authorize]
    [RoutePrefix("supplier")]
    public class SupplierController : Controller
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        // GET: Supplier
        public ActionResult Index(int page = 1, string searchValue = "")
        {
            int pageSize = 10;
            int rowCount = 0;

            var data = CommonDataService.ListOfSuppliers(page, pageSize, searchValue, out rowCount);
            Models.SupplierPaginationResult model = new Models.SupplierPaginationResult
            {
                Page = page,
                PageSize = pageSize,
                RowCount = rowCount,
                SearchValue = searchValue,
                Data = data
            };
            return View(model);
        }
        public ActionResult Create()
        {
            ViewBag.Title = "Bổ sung nhà cung cấp";
            return View();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="customerID"></param>
        /// <returns></returns>
        [Route("edit/{supplierID}")]
        public ActionResult Edit(int supplierID)
        {
            ViewBag.Title = "Cập nhật nhà cung cấp";
            return View("Create");
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="customerID"></param>
        /// <returns></returns>
        [Route("delete/{supplierID}")]
        public ActionResult Delete(int supplierID)
        {
            return View();
        }
    }
}