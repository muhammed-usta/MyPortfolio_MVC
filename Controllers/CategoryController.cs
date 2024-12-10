﻿using MyPortfolio_MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyPortfolio_MVC.Controllers
{
    
    public class CategoryController : Controller
    {
        MyPortfolioDb6Entities db = new MyPortfolioDb6Entities();
        public ActionResult Index()
        {
            var values = db.TblCategories.ToList();
            return View(values);
        }
        [HttpGet]
        public ActionResult CreateCategory()
        {
            
            return View();
        }
        [HttpPost]
        public ActionResult CreateCategory(TblCategory model)
        {
            if (!ModelState.IsValid)
            {
             
                return View(model);
            }
            db.TblCategories.Add(model);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult DeleteCategory(int id)
        {
            var value = db.TblCategories.Find(id);
            db.TblCategories.Remove(value);
            db.SaveChanges();
            return RedirectToAction("Index");
           
        }

        [HttpGet]
        public ActionResult UpdateCategory(int id)
        {
            var value = db.TblCategories.Find(id);
            return View(value); 
        }

        [HttpPost]
        public ActionResult UpdateCategory(TblCategory category)
        {
            var value = db.TblCategories.Find(category.CategoryId);
            value.Name = category.Name;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }


}