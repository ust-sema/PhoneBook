using PhoneBook.Data;
using PhoneBook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace PhoneBook.Controllers
{
    public class LoginController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(LoginViewModel login)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Error = "Неверные данные, проверьте правильность ввода.";
                return View();
            }

            using (var db = new PhoneBookContext())
            {
                var user = db.Persons.FirstOrDefault(p => p.Email == login.Username && p.Password == login.Password);
                if (user != null)
                {
                    FormsAuthentication.SetAuthCookie(user.Email, false);
                    Session["CurrentUser"] = user;
                    return RedirectToAction("Index", "Home");
                }
            }

            ModelState.Remove("Password");
            ViewBag.Error = "Неверные имя пользователя или пароль. Повторите ввод.";
            return View();
        }

        public ActionResult Logout()
        {
            Session["CurrentUser"] = null;
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }
    }
}