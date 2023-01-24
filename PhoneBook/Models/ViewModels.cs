using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PhoneBook.Models
{
    /// <summary>
    /// Класс параметров для передачи в PeopleController
    /// </summary>
    public class RequestParameters
    {
        /// <summary>
        /// Пропустить записей
        /// </summary>
        public int Skip { get; set; }
        /// <summary>
        /// Длина списка
        /// </summary>
        public int Take { get; set; }
        /// <summary>
        /// Имя метода контроллера
        /// </summary>
        public string Action { get; set; }
        /// <summary>
        /// Фраза для поиска по ФИО
        /// </summary>
        public string Phrase { get; set; }
        /// <summary>
        /// Дата для фильтрации по дню рождения
        /// </summary>
        public DateTime Date { get; set; }
    }

    public class PeopleViewModel
    {
        public RequestParameters Parameters { get; set; }
        public List<Person> People { get; set; }

        public PeopleViewModel()
        {
            Parameters = new RequestParameters { Take = Properties.Settings.Default.PeopleListCount, Action = "" };
        }
    }

    public class HomeViewModel
    {
        public int RecordsLoaded { get; set; }
        public Person CurrentUser { get; set; }
        public PeopleViewModel PeopleViewModel { get; set; }

        public HomeViewModel()
        {
            RecordsLoaded = Data.People.Count();
        }
    }

    public class LoginViewModel
    {
        [Required(ErrorMessage = "Обязательное поле")]
        [MaxLengthAttribute(100)]
        [Display(Name = "Пользователь")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Обязательное поле")]
        [MaxLengthAttribute(12)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }
    }
}