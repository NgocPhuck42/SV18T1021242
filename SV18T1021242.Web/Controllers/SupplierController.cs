using SV18T1021242.BusinessLayer;
using SV18T1021242.DomainModel;
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
        public ActionResult Index()
        {
            Models.PaginationSearchInput model = Session["SUPPLIER_SEARCH"] as Models.PaginationSearchInput;
            if (model == null)
            {
                model = new Models.PaginationSearchInput()
                {
                    Page = 1,
                    PageSize = 10,
                    SearchValue = ""
                };
            }
            return View(model);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public ActionResult Search(Models.PaginationSearchInput input)
        {
            int rowCount = 0;

            var data = CommonDataService.ListOfSuppliers(input.Page, input.PageSize, input.SearchValue, out rowCount);
            Models.SupplierPaginationResult model = new Models.SupplierPaginationResult
            {
                Page = input.Page,
                PageSize = input.PageSize,
                RowCount = rowCount,
                SearchValue = input.SearchValue,
                Data = data
            };

            Session["SUPPLIER_SEARCH"] = input;

            return View(model);
        }
        public ActionResult Create()
        {
            Supplier model = new Supplier()
            {
                SupplierID = 0
            };
            ViewBag.Title = "Bổ sung nhà cung cấp";
            return View(model);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="customerID"></param>
        /// <returns></returns>
        [Route("edit/{supplierID}")]
        public ActionResult Edit(int supplierID)
        {
            Supplier model = CommonDataService.GetSupplier(supplierID);
            if (model == null)
            {
                return RedirectToAction("Index");
            }
            ViewBag.Title = "Cập nhật nhà cung cấp";
            return View("Create", model);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Save(Supplier model)
        {
            if (string.IsNullOrWhiteSpace(model.SupplierName))
                ModelState.AddModelError("SupplierName", "Tên không được để trống");
            if (string.IsNullOrWhiteSpace(model.ContactName))
                ModelState.AddModelError("ContactName", "Tên giao dịch không được để trống");
            if (string.IsNullOrWhiteSpace(model.ContactName))
                ModelState.AddModelError("ContactName", "Tên giao dịch không được để trống");
            if (string.IsNullOrWhiteSpace(model.Address))
                ModelState.AddModelError("Address", "Địa chỉ không được để trống");
            if (string.IsNullOrWhiteSpace(model.City))
                ModelState.AddModelError("City", "Thành phố không được để trống");
            if (string.IsNullOrWhiteSpace(model.PostalCode))
                ModelState.AddModelError("PostalCode", "Mã bưu điện không được để trống");
            if (string.IsNullOrWhiteSpace(model.Country))
                ModelState.AddModelError("Country", "Quốc gia không được để trống");
            if (string.IsNullOrWhiteSpace(model.Phone))
                ModelState.AddModelError("Phone", "Quốc gia không được để trống");

            if (!ModelState.IsValid)
            {
                ViewBag.Title = model.SupplierID == 0 ? "Bổ sung nhà cung cấp" : "Cập nhật nhà cung cấp";
                return View("Create", model);
            }

            if (model.SupplierID == 0)
            {
                CommonDataService.AddSupplier(model);
                return RedirectToAction("Index");
            }
            else
            {
                CommonDataService.UpdateSupplier(model);
                return RedirectToAction("Index");
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="supplierID"></param>
        /// <returns></returns>
        [Route("delete/{supplierID}")]
        public ActionResult Delete(int supplierID)
        {
            if (Request.HttpMethod == "POST")
            {
                CommonDataService.DeleteSupplier(supplierID);
                return RedirectToAction("Index");
            }
            var model = CommonDataService.GetSupplier(supplierID);
            if (model == null)
            {
                return RedirectToAction("Index");
            }
            return View(model);
        }
    }
}