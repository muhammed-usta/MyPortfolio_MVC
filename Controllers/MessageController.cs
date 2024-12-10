using MyPortfolio_MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyPortfolio_MVC.Controllers
{
    public class MessageController : Controller
    {
        
        MyPortfolioDb6Entities db = new MyPortfolioDb6Entities();
        public ActionResult Index()
        {
            var values = db.TblMessages.Where(m => m.IsRead == false).ToList();
            return View(values);
        }

        public ActionResult ShowAllMessages()
        {
            var values = db.TblMessages.ToList();
            return View(values);
        }
         public ActionResult DeleteMessage(int id) 
        {
            var value = db.TblMessages.Find(id);
            db.TblMessages.Remove(value);
            db.SaveChanges();
            return RedirectToAction("Index");

        }
        public ActionResult MessageDetail(int id)
        {

            var value = db.TblMessages.Find(id);
            value.IsRead = true;
            db.SaveChanges();
            return View(value);
        }

        [HttpPost]
        public ActionResult MakeMessageRead(int id)
        {
            var value = db.TblMessages.Find(id);
            value.IsRead = true;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public PartialViewResult LastMessages()
        {
            
            var lastMessages = db.TblMessages
                                   .OrderByDescending(m => m.MessageId)  // Mesajları ID'ye göre azalan sırayla al
                                   .Take(3)  // Son 3 mesaj
                                   .ToList();

            return PartialView(lastMessages);  // Partial View olarak döndür
        }
    }
}