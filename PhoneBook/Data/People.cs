using PhoneBook.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace PhoneBook.Data
{
    public static class People
    {
        public static PeopleViewModel PlainList(int skip)
        {
            var viewModel = new PeopleViewModel();

            using (var db = new PhoneBookContext())
            {
                viewModel.People = db.Persons.OrderBy(p => p.Id)
                    .Skip(skip).Take(viewModel.Parameters.Take).ToList();
            }
            viewModel.Parameters.Skip = skip + viewModel.Parameters.Take;

            return viewModel;
        }

        /// <summary>
        /// Поиск по имени и фамилии при совпадении любого слова в фразе.
        /// </summary>
        /// <param name="skip">Пропустить skip записей</param>
        /// <param name="phrase">Фраза поиска</param>
        /// <returns></returns>
        
        public static PeopleViewModel Search(int skip, string phrase)
        {
            if (string.IsNullOrWhiteSpace(phrase))
                return PlainList(skip);

            var reg = new System.Text.RegularExpressions.Regex(@"[^\p{L}\s]+");
            phrase = reg.Replace(phrase.Trim(), string.Empty);

            var words = phrase.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            var viewModel = new PeopleViewModel();
            using (var db = new PhoneBookContext())
            {
                viewModel.People = db.Persons
                    .Where(p => words.Any(w => p.FirstName.Contains(w)) || words.Any(w => p.LastName.Contains(w)))
                    .OrderBy(p => p.LastName)
                    .Skip(skip).Take(viewModel.Parameters.Take).ToList();
            }
            viewModel.Parameters.Skip = skip + viewModel.Parameters.Take;
            viewModel.Parameters.Action = "Search";
            viewModel.Parameters.Phrase = phrase;

            return viewModel;
        }

        public static PeopleViewModel SortByName(int skip)
        {
            var viewModel = new PeopleViewModel();

            using (var db = new PhoneBookContext())
            {
                viewModel.People = db.Persons.OrderBy(p => p.LastName).ThenBy(p => p.FirstName)
                    .Skip(skip).Take(viewModel.Parameters.Take).ToList();
            }
            viewModel.Parameters.Skip = skip + viewModel.Parameters.Take;
            viewModel.Parameters.Action = "SortByName";

            return viewModel;
        }

        public static PeopleViewModel SortByDOB(int skip)
        {
            var viewModel = new PeopleViewModel();

            using (var db = new PhoneBookContext())
            {
                viewModel.People = db.Persons.OrderBy(p => p.Dob)
                    .Skip(skip).Take(viewModel.Parameters.Take).ToList();
            }
            viewModel.Parameters.Skip = skip + viewModel.Parameters.Take;
            viewModel.Parameters.Action = "SortByDOB";

            return viewModel;
        }

        public static PeopleViewModel FiltrByDOB(int skip, DateTime date)
        {
            var viewModel = new PeopleViewModel();

            using (var db = new PhoneBookContext())
            {
                viewModel.People = db.Persons.Where(p => DbFunctions.TruncateTime(p.Dob) == date.Date).OrderBy(p => p.FirstName)
                    .Skip(skip).Take(viewModel.Parameters.Take).ToList();
            }
            viewModel.Parameters.Skip = skip + viewModel.Parameters.Take;
            viewModel.Parameters.Action = "FiltrByDOB";
            viewModel.Parameters.Date = date;

            return viewModel;
        }

        public static int Count()
        {
            using (var db = new PhoneBookContext())
            {
                return db.Persons.Count();
            }
        }
    }
}
