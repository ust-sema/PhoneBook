using PhoneBook.Data;
using PhoneBook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PhoneBook.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            var viewModel = new HomeViewModel { PeopleViewModel = People.PlainList(0) };

            return View("Index", viewModel);
        }

        public ActionResult Search(string phrase)
        {
            if (string.IsNullOrWhiteSpace(phrase))
                return Index();

            var viewModel = new HomeViewModel { PeopleViewModel = People.Search(0, phrase) };
            return View("Index", viewModel);
        }

        public ActionResult SortByName()
        {
            var viewModel = new HomeViewModel { PeopleViewModel = People.SortByName(0) };

            return View("Index", viewModel);
        }

        public ActionResult SortByDOB()
        {
            var viewModel = new HomeViewModel { PeopleViewModel = People.SortByDOB(0) };

            return View("Index", viewModel);
        }

        public ActionResult FiltrByDOB(DateTime? date)
        {
            if (!date.HasValue) return Index();

            var viewModel = new HomeViewModel { PeopleViewModel = People.FiltrByDOB(0, date.Value) };

            return View("Index", viewModel);
        }


        public JsonResult LoadRecords()
        {
            var message = "";
            var result = "success";
            try
            {
                Loader.FetchData();
            }
            catch (Exception ex)
            {
                message = ex.Message;
                result = "error";
            }

            return Json(new { result, message }, JsonRequestBehavior.AllowGet);
        }

    }
}