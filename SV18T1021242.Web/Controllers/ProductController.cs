using Microsoft.Ajax.Utilities;
using SV18T1021242.BusinessLayer;
using SV18T1021242.DomainModel;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SV18T1021242.Web.Controllers
{
    [Authorize]
    [RoutePrefix("product")]

    public class ProductController : Controller
    {
        // GET: Product
        public ActionResult Index()
        {
            Models.PaginationSearchInput model = Session["PRODUCT_SEARCH"] as Models.PaginationSearchInput;
            if (model == null)
            {
                model = new Models.PaginationSearchInput()
                {
                    Page = 1,
                    PageSize = 10,
                    SearchValue = "",
                    CategoryID = 0,
                    SupplierID = 0
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
            var data = ProductDataService.ListOfProducts(input.Page, input.PageSize, input.SearchValue, input.CategoryID, input.SupplierID, out rowCount);
            Models.ProductPaginationResult model = new Models.ProductPaginationResult
            {
                Page = input.Page,
                PageSize = input.PageSize,
                SearchValue = input.SearchValue,
                CategoryID = input.CategoryID,
                SupplierID = input.SupplierID,
                RowCount = rowCount,
                Data = data
            };

            Session["PRODUCT_SEARCH"] = input;

            return View(model);
            //try
            //{
            //    int rowCount = 0;
            //    int pageSize = 10;
            //    var model = ProductDataService.ListOfProducts(page, pageSize, searchValue, category, supplier, out rowCount);
            //    ViewBag.RowCount = rowCount;
            //    ViewBag.Page = page;

            //    int pageCount = rowCount / pageSize;
            //    if (rowCount % pageSize > 0)
            //        pageCount++;
            //    ViewBag.PageCount = pageCount;
            //    return View(model);
            //}
            //catch (Exception ex)
            //{
            //    return Content(ex.Message);
            //}
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Create()
        {
            Product model = new Product()
            {
                ProductID = 0
            };

            ViewBag.Title = "Bổ sung Nhân Viên";
            return View(model);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="productID"></param>
        /// <returns></returns>
        [Route("edit/{productID}")]
        public ActionResult Edit(int productID)
        {
            //int id = 0;
            //try
            //{
            //    id = Convert.ToInt32(productID);
            //}
            //catch
            //{
            //}
            //Product model = CommonDataService.GetProduct(id);
            //if (model == null)
            //{
            //    return RedirectToAction("Index");
            //}
            //ViewBag.Title = "Cập nhật Nhân Viên";
            return View("Edit");
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="productID"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Save(Product model, HttpPostedFileBase uploadPhoto, string formatPrice)
        {

            if (string.IsNullOrWhiteSpace(model.ProductName))
                ModelState.AddModelError("ProductName", "Họ không được để trống");
            if (string.IsNullOrWhiteSpace(model.Unit))
                ModelState.AddModelError("Unit", "Unit không được để trống");



            // Xử lí file ảnh
            if (uploadPhoto != null)
            {
                var allowedExtensions = new[] { ".JPG", ".PNG", ".JPEG", ".GIF" };

                //    string fileName = Path.GetFileNameWithoutExtension(uploadPhoto.FileName);
                string fileExtension = Path.GetExtension(uploadPhoto.FileName).ToUpper();
                if (allowedExtensions.Contains(fileExtension))
                {
                    string fileName = $"{DateTime.Now.Ticks}_{uploadPhoto.FileName}";
                    //  fileName = DateTime.Now.ToString("yyyyMMdd") + "-" + fileName.Trim() + fileExtension;
                    //      string UploadPath = ConfigurationManager.AppSettings["UserImagePath"].ToString();
                    //  model.Photo = UploadPath + fileName;
                    string filePath = System.IO.Path.Combine(Server.MapPath("~/Images/Products"), fileName);
                    uploadPhoto.SaveAs(filePath);

                    model.Photo = $"Images/Products/{fileName}";

                }
                else
                {
                    ModelState.AddModelError("Photo", "Định dạng ảnh JPG, PNG, JPEG, GIF không đúng");
                }
            }

            if (!ModelState.IsValid)
            {

                ViewBag.Title = model.ProductID == 0 ? "Bổ sung mặt hàng" : "Cập nhật mặt hàng";
                return View("Create", model);
            }

            //return Json(model);

            if (model.ProductID == 0)
            {


            }
            else
            {

            }
            return View();
        }
        [Route("delete/{productID}")]
        public ActionResult Delete(int productID)
        {

            if (Request.HttpMethod == "POST")
            {
                
            }
            
            //if (model == null)
            //{
            //    return RedirectToAction("Index");
            //}
            return View();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="method"></param>
        /// <param name="productID"></param>
        /// <param name="photoID"></param>
        /// <returns></returns>
        [Route("photo/{method}/{productID}/{photoID?}")]
        public ActionResult Photo(string method, int productID, int? photoID)
        {
            switch (method)
            {
                case "add":
                    ViewBag.Title = "Bổ sung ảnh";
                    break;
                case "edit":
                    ViewBag.Title = "Thay đổi ảnh";
                    break;
                case "delete":
                    return RedirectToAction("Edit", new { productID = productID });
                default:
                    return RedirectToAction("Index");
            }
            return View();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="method"></param>
        /// <param name="productID"></param>
        /// <param name="attributeID"></param>
        /// <returns></returns>
        [Route("attribute/{method}/{productID}/{attributeID?}")]
        public ActionResult Attribute(string method, int productID, int? attributeID)
        {
            switch (method)
            {
                case "add":
                    ViewBag.Title = "Bổ sung thuộc tính";
                    break;
                case "edit":
                    ViewBag.Title = "Thay đổi thuộc tính";
                    break;
                case "delete":
                    return RedirectToAction("Edit", new { productID = productID });
                default:
                    return RedirectToAction("Index");
            }
            return View();
        }

    }
}