using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RestoManager.Models;

namespace RestoManager.Controllers
{
    public class MenuController : Controller
    {
        RestoManagerDB db = new RestoManagerDB();

        public ActionResult Index()
        {
            var yemekler = db.MenuItems.ToList();
            return View(yemekler);
        }

        [HttpGet]
        public ActionResult Ekle()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Ekle(MenuItems yeniYemek)
        {
            if (ModelState.IsValid)
            {
                db.MenuItems.Add(yeniYemek);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(yeniYemek);
        }
        public ActionResult Sil(int id)
        {
            var silinecek = db.MenuItems.Find(id);
            if (silinecek != null)
            {
                db.MenuItems.Remove(silinecek);
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }

    }
}