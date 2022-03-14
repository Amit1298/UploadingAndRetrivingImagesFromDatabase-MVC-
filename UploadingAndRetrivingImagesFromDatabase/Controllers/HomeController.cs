using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UploadingAndRetrivingImagesFromDatabase.Models;

namespace UploadingAndRetrivingImagesFromDatabase.Controllers
{
    public class HomeController : Controller
    {
        EmployeeDBEntities db = new EmployeeDBEntities();
        // GET: Home
        public ActionResult Index()
        {
            var data = db.Students.ToList();
            return View(data);
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Student s)
        {
            string fileName = Path.GetFileNameWithoutExtension(s.ImageFile.FileName);
            string extension = Path.GetExtension(s.ImageFile.FileName);
            fileName = fileName + extension;
            s.image_path = "~/images/" + fileName;
            fileName = Path.Combine(Server.MapPath("~/images/"), fileName);
            s.ImageFile.SaveAs(fileName);
            db.Students.Add(s);
            int a = db.SaveChanges();
            if (a > 0)
            {
                ViewBag.Message = "<script>alert('Record Inserted')</script>";
                ModelState.Clear();
            }
            else
            {
                ViewBag.Message = "<script>alert('Record not Inserted')</script>";
            }
            return View();
        }
    }
}