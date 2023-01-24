using PhoneBook.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace PhoneBook.Data
{
    public class PhoneBookContext : DbContext
    {
        public DbSet<Person> Persons { get; set; }

        public PhoneBookContext() : base("PhoneBook")
        {

        }
    }
}
