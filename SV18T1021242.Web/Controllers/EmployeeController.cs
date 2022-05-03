using Microsoft.Ajax.Utilities;
using SV18T1021242.BusinessLayer;
using SV18T1021242.DomainModel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SV18T1021242.Web.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Authorize]
    [RoutePrefix("employee")]
    public class EmployeeController : Controller
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        // GET: Employee
        public ActionResult Index(int page = 1, string searchValue = "")
        {
            int pageSize = 10;
            int rowCount = 0;

            var data = CommonDataService.ListOfEmployees(page, pageSize, searchValue, out rowCount);
            Models.EmployeePaginationResult model = new Models.EmployeePaginationResult
            {
                Page = page,
                PageSize = pageSize,
                RowCount = rowCount,
                SearchValue = searchValue,
                Data = data
            };
            return View(model);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Create()
        {
            Employee model = new Employee()
            {
                EmployeeID = 0
            };

            ViewBag.Title = "Bổ sung Nhân Viên";
            return View(model);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="customerID"></param>
        /// <returns></returns>
        [Route("edit/{employeeID?}")]
        public ActionResult Edit(string employeeID)
        {
            int id = 0;
            try
            {
                id = Convert.ToInt32(employeeID);
            }
            catch
            {
            }
            Employee model = CommonDataService.GetEmployee(id);
            if (model == null)
            {
                return RedirectToAction("Index");
            }
            ViewBag.Title = "Cập nhật Nhân Viên";
            return View("Create", model);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <param name="uploadPhoto"></param>
        /// <param name="dateOfBirth"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Save(Employee model, HttpPostedFileBase uploadPhoto, string dateOfBirth)
        {

            if (string.IsNullOrWhiteSpace(model.LastName))
                ModelState.AddModelError("LastName", "Họ không được để trống");
            if (string.IsNullOrWhiteSpace(model.FirstName))
                ModelState.AddModelError("FirstName", "Tên không được để trống");
            if (string.IsNullOrWhiteSpace(model.Email))
                ModelState.AddModelError("Email", "Email không được để trống");
            if (string.IsNullOrWhiteSpace(model.Notes))
                ModelState.AddModelError("Notes", "Nodes không được để trống");

            //Chuyển dateOfBirth (dd/MM/yyyy) sang giá trị kiểu ngày

            try
            {
                model.BirthDate = DateTime.ParseExact(dateOfBirth, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            }
            catch
            {
                ModelState.AddModelError("BirthDate", $"Ngày sinh {dateOfBirth} không đúng định dạng");
            }

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
                    string filePath = System.IO.Path.Combine(Server.MapPath("~/Images/Employees"), fileName);
                    uploadPhoto.SaveAs(filePath);

                    model.Photo = $"Images/Employees/{fileName}";

                }
                else
                {
                    ModelState.AddModelError("Photo", "Định dạng ảnh JPG, PNG, JPEG, GIF không đúng");
                }
            }

            if (!ModelState.IsValid)
            {

                ViewBag.Title = model.EmployeeID == 0 ? "Bổ sung nhân viên" : "Cập nhật nhân viên";
                return View("Create", model);
            }

            //return Json(model);

            if (model.EmployeeID == 0)
            {

                CommonDataService.AddEmployee(model);
                return RedirectToAction("Index");
            }
            else
            {
                CommonDataService.UpdateEmployee(model);
                return RedirectToAction("Index");
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="customerID"></param>
        /// <returns></returns>
        [Route("delete/{employeeID}")]
        public ActionResult Delete(int employeeID)
        {
            
           
            if (Request.HttpMethod == "POST")
            {
                CommonDataService.DeleteEmployee(employeeID);
                return RedirectToAction("Index");
            }
            var model = CommonDataService.GetEmployee(employeeID);
            if (model == null)
            {
                return RedirectToAction("Index");
            }
            return View(model);
        }
    }
}