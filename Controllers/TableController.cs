using System.Linq;
using System.Web.Mvc;
using RestoManager.Models;

namespace RestoManager.Controllers
{
    public class TableController : Controller
    {
        RestoManagerDB db = new RestoManagerDB();

        public ActionResult Index()
        {
          
            if (Session["StaffName"] == null)
            {
                return RedirectToAction("Login", "Account");
            }

          
            var masalar = db.Tables.ToList();
            ViewBag.Yemekler = db.MenuItems.ToList();

          
            return View(masalar);
        }
    }
}