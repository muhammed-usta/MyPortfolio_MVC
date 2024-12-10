using MyPortfolio_MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyPortfolio_MVC.Controllers
{
    public class ExperienceController : Controller
    {
  MyPortfolioDb6Entities db = new MyPortfolioDb6Entities();
        public ActionResult Index()
        {
            var values = db.TblExperiences.ToList(); 
            return View(values);
        }

        [HttpGet]
        public ActionResult CreateExperience()
        {

            return View();
        }

        [HttpPost]
        public ActionResult CreateExperience(TblExperience model)
        {
            db.TblExperiences.Add(model);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult DeleteExperience(int id)
        {
            var values = db.TblExperiences.Find(id);
            db.TblExperiences.Remove(values);
            db.SaveChanges();
            return RedirectToAction("Index");

        }

        [HttpGet]

        public ActionResult UpdateExperience(int id)
        {
            var experience = db.TblExperiences.Find(id);
            return View(experience);
        }

        [HttpPost]
        public ActionResult UpdateExperience(TblExperience model)
        {
            var value = db.TblExperiences.Find(model.ExperienceId);
            value.CompanyName = model.CompanyName;
            value.Title = model.Title;
            value.StartDate = model.StartDate;
            value.EndDate = model.EndDate;
            value.Description = model.Description;
 
            db.SaveChanges();
            return RedirectToAction("Index");

        }

        

    }
}