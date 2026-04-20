using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using RestoManager.Models;

namespace RestoManager.Controllers
{
    public class StaffController : Controller
    {
        RestoManagerDB db = new RestoManagerDB();

        public ActionResult PersonelListesi()
        {
          
            if (Session["StaffRole"]?.ToString() != "Admin")
            {
                return RedirectToAction("Index", "Table");
            }

            var personelListesi = db.Staff.ToList();
            return View(personelListesi);
        }

        public ActionResult Ekle()
        {
            if (Session["StaffRole"]?.ToString() != "Admin")
            {
                return RedirectToAction("Index", "Table");
            }
            return View();
        }

        [HttpPost]
        public ActionResult Ekle(Staff yeniPersonel)
        {
            if (ModelState.IsValid)
            {
                db.Staff.Add(yeniPersonel);
                db.SaveChanges();
                return RedirectToAction("PersonelListesi");
            }
            
            return View(yeniPersonel);
        }

     
        public ActionResult Sil(int id)
        {
       
            var currentUserId = Session["StaffId"] != null ? (int)Session["StaffId"] : 0;

            if (id == currentUserId)
            {
                return RedirectToAction("PersonelListesi");
            }

            var silinecek = db.Staff.Find(id);
            if (silinecek != null)
            {
                db.Staff.Remove(silinecek);
                db.SaveChanges();
            }

            return RedirectToAction("PersonelListesi");
        }
    }
}