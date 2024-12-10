using MyPortfolio_MVC.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyPortfolio_MVC.Controllers
{
    public class TestimonialController : Controller
    {
        MyPortfolioDb6Entities db = new MyPortfolioDb6Entities();

        public ActionResult Index()
        {
            var values = db.TblTestimonials.ToList();

            return View(values); 

        }
        [HttpGet]
        public ActionResult CreateTestimonial()
        {

            return View();
        }

        [HttpPost]
        public ActionResult CreateTestimonial(TblTestimonial model)
        {
            if (model.ImageFile != null)
            {
                var currentDirectory = AppDomain.CurrentDomain.BaseDirectory;
                var saveLocation = currentDirectory + "images\\";
                var fileName = Path.Combine(saveLocation, model.ImageFile.FileName);
                model.ImageFile.SaveAs(fileName);
                model.ImageUrl = "/images/" + model.ImageFile.FileName;
            }

            db.TblTestimonials.Add(model);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult DeleteTestimonial(int id)
        {
            var value = db.TblTestimonials.Find(id);
            db.TblTestimonials.Remove(value);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]

        public ActionResult UpdateTestimonial(int id)
        {
            var testimonial = db.TblTestimonials.Find(id);
            return View(testimonial);
        }

        [HttpPost]
        public ActionResult UpdateTestimonial(TblTestimonial model)
        {
            var value = db.TblTestimonials.Find(model.TestimonialId);

            if (value != null)
            {
                value.NameSurname = model.NameSurname;
                value.Title = model.Title;
                value.Comment = model.Comment;
             
                if (model.ImageFile != null)
                {
                    if (!string.IsNullOrEmpty(value.ImageUrl))
                    {
                        var currentDirectory = AppDomain.CurrentDomain.BaseDirectory;
                        var oldFilePath = currentDirectory + value.ImageUrl.Replace("/", "\\");
                        if (System.IO.File.Exists(oldFilePath))
                        {
                            System.IO.File.Delete(oldFilePath);
                        }
                    }
                    var saveLocation = AppDomain.CurrentDomain.BaseDirectory + "images\\";
                    var fileName = Path.Combine(saveLocation, model.ImageFile.FileName);
                    model.ImageFile.SaveAs(fileName);
                    value.ImageUrl = "/images/" + model.ImageFile.FileName;
                }
          
                db.SaveChanges();
            }

            return RedirectToAction("Index");

        }
    }
}