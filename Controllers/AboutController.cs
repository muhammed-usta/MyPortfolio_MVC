using MyPortfolio_MVC.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyPortfolio_MVC.Controllers
{
    public class AboutController : Controller
    {
        MyPortfolioDb6Entities db=new MyPortfolioDb6Entities();
        public ActionResult Index()
        {
            var values = db.TblAbouts.ToList();
            return View(values);
        }

        [HttpGet]
        public ActionResult CreateAbout()
        {

            return View();
        }

        [HttpPost]
        public ActionResult CreateAbout(TblAbout model)
        {

            
                if (model.ImageFile != null)
                {
                    var currentDirectory = AppDomain.CurrentDomain.BaseDirectory;
                    var saveLocation = currentDirectory + "images\\";
                    var fileName = Path.Combine(saveLocation, model.ImageFile.FileName);
                    model.ImageFile.SaveAs(fileName);
                    model.ImageUrl = "/images/" + model.ImageFile.FileName;
                }

                if (model.PdfFile != null)
                {
                    var currentDirectory = AppDomain.CurrentDomain.BaseDirectory;
                    var pdfSaveLocation = currentDirectory + "cvfiles\\";
                    var pdfFileName = Path.Combine(pdfSaveLocation, model.PdfFile.FileName);
                    model.PdfFile.SaveAs(pdfFileName);
                    model.CvUrl = "/cvfiles/" + model.PdfFile.FileName;
                }
                db.TblAbouts.Add(model);
                db.SaveChanges();
                return RedirectToAction("Index");
           


           
        }

        public ActionResult DeleteAbout(int id)
        {
            var value = db.TblAbouts.Find(id);
            db.TblAbouts.Remove(value);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]

        public ActionResult UpdateAbout(int id)
        {
            var about = db.TblAbouts.Find(id);
            return View(about);
        }

        [HttpPost]
        public ActionResult UpdateAbout(TblAbout model)
        {
           
           
                var value = db.TblAbouts.Find(model.AboutId);

                if (value != null)
                {
                    value.Title = model.Title;
                    value.Description = model.Description;


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

                    if (model.PdfFile != null)
                    {
                        if (!string.IsNullOrEmpty(value.CvUrl))
                        {
                            var currentDirectory = AppDomain.CurrentDomain.BaseDirectory;
                            var oldFilePath = currentDirectory + value.CvUrl.Replace("/", "\\");
                            if (System.IO.File.Exists(oldFilePath))
                            {
                                System.IO.File.Delete(oldFilePath);
                            }
                        }
                        var saveLocation = AppDomain.CurrentDomain.BaseDirectory + "cvfiles\\";
                        var fileName = Path.Combine(saveLocation, model.PdfFile.FileName);
                        model.PdfFile.SaveAs(fileName);
                        value.CvUrl = "/cvfiles/" + model.PdfFile.FileName;
                    }

                    db.SaveChanges();
                }

                return RedirectToAction("Index");
            
           


        }
        


    }
}