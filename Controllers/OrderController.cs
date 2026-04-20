using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RestoManager.Models;

namespace RestoManager.Controllers
{
    public class OrderController : Controller
    {
        RestoManagerDB db = new RestoManagerDB();

        public ActionResult MasaDetay(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index", "Table");
            }

            var tiklananMasa = db.Tables.Find(id);

            if (tiklananMasa == null)
            {
                return RedirectToAction("Index", "Table");
            }

            ViewBag.Menu = db.MenuItems.Where(x => x.IsActive == true).ToList();

            var aktifSiparis = db.Orders.FirstOrDefault(x => x.TableId == id && x.IsPaid == false);
            ViewBag.Siparisler = aktifSiparis != null ? aktifSiparis.OrderItems.ToList() : new List<OrderItems>();

            if (tiklananMasa.IsOccupied == false)
            {
                ViewBag.Mesaj = "Bu masa şu an BOŞ. Yeni sipariş eklenebilir.";
            }
            else
            {
                ViewBag.Mesaj = "Bu masa DOLU. İşte masadaki siparişler...";
            }

            return View(tiklananMasa);
        }

        [HttpPost]
        public ActionResult SiparisEkle(int masaId, int urunId)
        {
            var aktifSiparis = db.Orders.FirstOrDefault(x => x.TableId == masaId && x.IsPaid == false);

            if (aktifSiparis == null)
            {
                aktifSiparis = new Orders()
                {
                    TableId = masaId,
                    OrderDate = DateTime.Now,
                    TotalAmount = 0,
                    IsPaid = false
                };
                db.Orders.Add(aktifSiparis);

                var masa = db.Tables.Find(masaId);
                if (masa != null)
                {
                    masa.IsOccupied = true;
                }
                db.SaveChanges();
            } 
            var urun = db.MenuItems.Find(urunId);
            var yenikalem = new OrderItems()
            {
                OrderId = aktifSiparis.Id,
                MenuItemId = urunId,
                Quantity = 1,
            };

            db.OrderItems.Add(yenikalem);
            aktifSiparis.TotalAmount += urun.Price;
            db.SaveChanges();

            return RedirectToAction("MasaDetay", new { id = masaId });
        }
            [HttpPost]
        public ActionResult HesabiKapat(int masaId)
        {
            var aktifSiparis=db.Orders.FirstOrDefault(x => x.TableId == masaId && x.IsPaid == false);
            if (aktifSiparis != null)
            {
                aktifSiparis.IsPaid = true;
                var masa = db.Tables.Find(masaId);
                if (masa != null)
                {
                    masa.IsOccupied = false;
                }
                db.SaveChanges();
            }
            return RedirectToAction("Index", "Table");
        }
    }

}