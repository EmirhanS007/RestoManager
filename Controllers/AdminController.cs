using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RestoManager.Controllers
{
    public class AdminController : Controller
    {
        public ActionResult Index()
        {
            if (Session["StaffRole"]?.ToString() != "Admin")
                return RedirectToAction("Index", "Table");

            return View();
        }
    }
}