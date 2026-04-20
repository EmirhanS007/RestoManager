using System.Linq;
using System.Web.Mvc;
using RestoManager.Models;

namespace RestoManager.Controllers
{
    public class AccountController : Controller
    {
        RestoManagerDB db = new RestoManagerDB();

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(Staff s)
        {
          
            var user = db.Staff.FirstOrDefault(x => x.Username == s.Username && x.Password == s.Password);

            if (user != null)
            {
                Session["StaffName"] = user.FullName;
                Session["StaffRole"] = user.Role;
                return RedirectToAction("Index", "Table");
            }

            if (s.Username == "admin" && s.Password == "1234")
            {
                Session["StaffName"] = "Emirhan Suna";
                Session["StaffRole"] = "Admin";
                return RedirectToAction("Index", "Table");
            }

            ViewBag.Error = "Kullanıcı adı veya şifre hatalı!";
            return View();
        }

        public ActionResult Logout()
        {
            Session.Clear();
            Session.Abandon();
            return RedirectToAction("Login", "Account");
        }
    }
}