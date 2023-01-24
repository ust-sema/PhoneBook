using PhoneBook.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PhoneBook.Controllers
{
    public class DetailsController : Controller
    {
        [Authorize]
        public ActionResult Index(int id)
        {
            using (var db = new PhoneBookContext())
            {
                var person = db.Persons.FirstOrDefault(p => p.Id == id);
                return View(person);
            }
        }
    }
}