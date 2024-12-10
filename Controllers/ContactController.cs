using Microsoft.Ajax.Utilities;
using MyPortfolio_MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyPortfolio_MVC.Controllers
{
    public class ContactController : Controller
    {
        MyPortfolioDb6Entities db = new MyPortfolioDb6Entities();
        public ActionResult Index()
        {
            var contacts = db.TblContacts.ToList(); 
         

            return View(contacts); 


        }

        [HttpGet]
        public ActionResult CreateContact()
        {

            return View();
        }
        [HttpPost]
        public ActionResult CreateContact(TblContact model)
        {
            if (!ModelState.IsValid)
            {

                return View(model);
            }
            db.TblContacts.Add(model);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult DeleteContact(int id)
        {
            var value = db.TblContacts.Find(id);
            db.TblContacts.Remove(value);
            db.SaveChanges();
            return RedirectToAction("Index");

        }

        [HttpGet]
        public ActionResult UpdateContact(int id)
        {
            var value = db.TblContacts.Find(id);
            return View(value);
        }

        [HttpPost]
        public ActionResult UpdateContact(TblContact model)
        {
            var value = db.TblContacts.Find(model.ContactId);
            value.Phone = model.Phone;
            value.Email = model.Email;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}