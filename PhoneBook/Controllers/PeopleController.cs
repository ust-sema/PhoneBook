using PhoneBook.Data;
using PhoneBook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PhoneBook.Controllers
{
    public class PeopleController : Controller
    {
        /// <summary>
        /// GET: Возвращает вид со списком пользователей, отсортирован по ID
        /// </summary>
        /// <param name="requestParameters">Параметры запроса</param>
        /// <returns>Partial view список пользователей с сортировкой по ID</returns>
        /// 
        public ActionResult Index(RequestParameters requestParameters)
        {
            var viewModel = People.PlainList(requestParameters.Skip);

            return View("Index", viewModel);
        }

        public ActionResult Search(RequestParameters requestParameters)
        {
            var viewModel = People.Search(requestParameters.Skip, requestParameters.Phrase);

            return View("Index", viewModel);
        }

        public ActionResult SortByName(RequestParameters requestParameters)
        {
            var viewModel = People.SortByName(requestParameters.Skip);

            return View("Index", viewModel);
        }

        public ActionResult SortByDOB(RequestParameters requestParameters)
        {
            var viewModel = People.SortByDOB(requestParameters.Skip);

            return View("Index", viewModel);
        }

        public ActionResult FiltrByDOB(RequestParameters requestParameters)
        {
            var viewModel = People.FiltrByDOB(requestParameters.Skip, requestParameters.Date);

            return View("Index", viewModel);
        }
    }
}