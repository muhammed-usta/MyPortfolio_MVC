using MyPortfolio_MVC.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyPortfolio_MVC.Controllers
{
    public class BannerController : Controller
    {
        MyPortfolioDb6Entities db = new MyPortfolioDb6Entities();
        public ActionResult Index()
        {
            var values = db.TblBanners.ToList();
            var activebanner=values.FirstOrDefault(b=>b.IsShown==true);
         ViewBag.ActiveBanner=activebanner;
            return View(values);
        }

        [HttpGet]
        public ActionResult CreateBanner()
        {

            return View();
        }

        [HttpPost]
        public ActionResult CreateBanner(TblBanner model)
        {
            model.IsShown = false;
            db.TblBanners.Add(model);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult DeleteBanner(int id)
        {
            var value = db.TblBanners.Find(id);
            db.TblBanners.Remove(value);
            db.SaveChanges();
            return RedirectToAction("Index");

        }

        [HttpGet]
        public ActionResult UpdateBanner(int id)
        {
            var value = db.TblBanners.Find(id);
            return View(value);
        }

        [HttpPost]
        public ActionResult UpdateBanner(TblBanner model)
        {
            var value = db.TblBanners.Find(model.BannerId);
            value.Title = model.Title;
            value.Description = model.Description;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult ShowBanner (int id)
        {
            var allBanners = db.TblBanners.ToList();
            foreach (var banner in allBanners)
            {
                banner.IsShown = false;
            }

            var value = db.TblBanners.Find(id);
            if (value !=null)
            {
                value.IsShown = true;
            }

           
            db.SaveChanges();
            return RedirectToAction("Index");

        }

        public ActionResult UnShowBanner(int id)
        {
            var value = db.TblBanners.Find(id);
            if (value != null)
            {
                value.IsShown = false;
            }
                

            db.SaveChanges();
            return RedirectToAction("Index");

        }



    }
}