﻿using SV18T1021242.BusinessLayer;
using SV18T1021242.DomainModel;
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
            Shipper model = new Shipper()
            {
                ShipperID = 0
            };
                ViewBag.Title = "Bổ sung người giao hàng";
            return View(model);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="customerID"></param>
        /// <returns></returns>
        [Route("edit/{shipperID}")]
        public ActionResult Edit(int shipperID)
        {
            Shipper model = CommonDataService.GetShipper(shipperID);
            if (model == null)
            {
                return RedirectToAction("Index");
            }
            ViewBag.Title = "Cập nhật người giao hàng";
            return View("Create", model);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Save(Shipper model)
        {
            if (string.IsNullOrWhiteSpace(model.ShipperName))
                ModelState.AddModelError("ShipperName", "Tên không được để trống");
            if (string.IsNullOrWhiteSpace(model.Phone))
                ModelState.AddModelError("Phone", "Số điện thoại không được để trống");

            if (!ModelState.IsValid)
            {
                ViewBag.Title = model.ShipperID == 0 ? "Bổ sung người giao hàng" : "Cập nhật người giao hàng";
                return View("Create", model);
            }

            if (model.ShipperID == 0)
            {
                CommonDataService.AddShipper(model);
                return RedirectToAction("Index");
            }
            else
            {
                CommonDataService.UpdateShipper(model);
                return RedirectToAction("Index");
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="customerID"></param>
        /// <returns></returns>
        [Route("delete/{shipperID}")]
        public ActionResult Delete(int shipperID)
        {
            if (Request.HttpMethod == "POST")
            {
                CommonDataService.DeleteShipper(shipperID);
                return RedirectToAction("Index");
            }
            var model = CommonDataService.GetShipper(shipperID);
            if (model == null)
            {
                return RedirectToAction("Index");
            }
            return View(model);
        }
    }
}