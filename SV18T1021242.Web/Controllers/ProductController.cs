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
        public ActionResult Edit(string productID)
        {
            int id = 0;
            try
            {
                id = Convert.ToInt32(productID);
            }
            catch
            {
            }
            Product model = ProductDataService.GetProduct(id);
            if (model == null)
            {
                return RedirectToAction("Index");
            }
            ViewBag.Title = "Cập nhật Nhân Viên";
            return View(model);
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
                ModelState.AddModelError("ProductName", "Tên không được để trống");
            if (model.CategoryID == 0)
                ModelState.AddModelError("CategoryID", "Vui lòng chọn loại hàng");
            if (model.SupplierID == 0)
                ModelState.AddModelError("SupplierID", "Vui lòng chọn nhà cung cấp");
            if (string.IsNullOrWhiteSpace(model.Unit))
                ModelState.AddModelError("Unit", "Đơn vị tính không được để trống");
            if (model.Price <= 0)
                ModelState.AddModelError("Price", "Giá tiền phải lớn hơn 0");

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
                    ModelState.AddModelError("Photo", "Không đúng định dạng ảnh .JPG, .PNG, .JPEG, .GIF");
                }
            }

            if (!ModelState.IsValid)
            {
                if(model.ProductID==0)
                {
                    ViewBag.Title = "Bổ sung mặt hàng";
                    return View("Create", model);
                }
                else
                {
                    ViewBag.Title = "Cập nhật mặt hàng";
                    return View("edit", model);
                }
                
                
            }

            //return Json(model);

            if (model.ProductID == 0)
            {
                ProductDataService.AddProduct(model);
                return RedirectToAction("Index");
            }
            else
            {
                ProductDataService.UpdateProduct(model);
                return RedirectToAction("Index");
            }
            
        }
        [Route("delete/{productID}")]
        public ActionResult Delete(int productID)
        {

            int id = 0;
            try
            {
                id = Convert.ToInt32(productID);
            }
            catch
            {
                return RedirectToAction("Index");
            }

            if (Request.HttpMethod == "POST")
            {
                ProductDataService.DeleteProduct(id);
                return RedirectToAction("Index");
            }

            var model = ProductDataService.GetProduct(id);
            if (model == null)
                return RedirectToAction("Index");

            return View(model);
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
            ProductPhoto model = new ProductPhoto();
            switch (method)
            {
                case "add":
                    model.PhotoID = 0;
                    model.ProductID = productID;
                    ViewBag.Title = "Bổ sung ảnh";
                    break;
                case "edit":
                    model = ProductDataService.GetOfProductPhoto(Convert.ToInt32(photoID));
                    ViewBag.Title = "Thay đổi ảnh";
                    break;
                case "delete":
                    ProductDataService.DeleteProductPhoto(Convert.ToInt32(photoID));
                    return RedirectToAction("Edit", new { productID = productID });
                default:
                    return RedirectToAction("Index");
            }
            return View(model);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <param name="titlePage"></param>
        /// <param name="attachFile"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult SavePhoto(ProductPhoto model, HttpPostedFileBase uploadPhoto, string titlePage)
        {
            if (string.IsNullOrWhiteSpace(model.Description))
                ModelState.AddModelError("Description", "Mô tả không được để trống");
            if (model.DisplayOrder <= 0)
                ModelState.AddModelError("DisplayOrder", "Thứ tự hiển thị phải lớn hơn 0");

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
                ViewBag.Title = titlePage;
                return View("Photo", model);
            }

            if (model.PhotoID == 0)
                ProductDataService.AddProductPhoto(model);
            else
                ProductDataService.UpdateProductPhoto(model);

            return RedirectToAction("Edit", new { productID = model.ProductID });
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
            ProductAttribute model = new ProductAttribute();

            switch (method)
            {
                case "add":
                    model.AttributeID = 0;
                    model.ProductID = productID;
                    ViewBag.Title = "Bổ sung thuộc tính";
                    break;
                case "edit":
                    model = ProductDataService.GetOfProductAttribute(Convert.ToInt32(attributeID));
                    ViewBag.Title = "Thay đổi thuộc tính";
                    break;
                case "delete":
                    ProductDataService.DeleteProductAttribute(Convert.ToInt32(attributeID));
                    return RedirectToAction("Edit", new { productID = productID });
                default:
                    return RedirectToAction("Index");
            }
            return View(model);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <param name="titlePage"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult SaveAttribute(ProductAttribute model, string titlePage)
        {
            if (string.IsNullOrWhiteSpace(model.AttributeName))
                ModelState.AddModelError("AttributeName", "Tên không được để trống");
            if (string.IsNullOrWhiteSpace(model.AttributeValue))
                ModelState.AddModelError("AttributeValue", "Giá trị thuộc tính không được để trống");
            if (model.DisplayOrder <= 0)
                ModelState.AddModelError("DisplayOrder", "Thứ tự hiển thị phải lớn hơn 0");

            if (!ModelState.IsValid)
            {
                ViewBag.Title = titlePage;
                return View("Attribute", model);
            }
            if (model.AttributeID == 0)
                ProductDataService.AddProductAttribute(model);
            else
                ProductDataService.UpdateProductAttribute(model);
            return RedirectToAction("Edit", new { productID = model.ProductID });
        }
    }
}