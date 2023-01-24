using Newtonsoft.Json;
using PhoneBook.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;

namespace PhoneBook.Data
{
    public static class Loader
    {
        public static void FetchData()
        {
            string json;
            using (WebClient wc = new WebClient { Encoding = System.Text.Encoding.UTF8 })
            {
                json = wc.DownloadString(Properties.Settings.Default.RecordsSourceUrl);
            }

            var db = new PhoneBookContext();

            var dict = JsonConvert.DeserializeObject<Dictionary<string, object>>(json);

            var list = JsonConvert.DeserializeObject<List<object>>(dict["results"].ToString());

            foreach (var item in list)
            {
                dict = JsonConvert.DeserializeObject<Dictionary<string, object>>(item.ToString());
                var prop = JsonConvert.DeserializeObject<Dictionary<string, object>>(dict["name"].ToString());

                var person = new Person
                {
                    Title = prop["title"].ToString(),
                    FirstName = prop["first"].ToString(),
                    LastName = prop["last"].ToString(),
                    Email = dict["email"].ToString(),
                    Phone = dict["phone"].ToString()
                };

                prop = JsonConvert.DeserializeObject<Dictionary<string, object>>(dict["dob"].ToString());
                person.Dob = DateTime.Parse(prop["date"].ToString());

                prop = JsonConvert.DeserializeObject<Dictionary<string, object>>(dict["login"].ToString());
                person.Password = prop["password"].ToString();

                prop = JsonConvert.DeserializeObject<Dictionary<string, object>>(dict["picture"].ToString());
                person.LargePicture = prop["large"].ToString();
                person.MediumPicture = prop["medium"].ToString();
                person.ThumbnailPicture = prop["thumbnail"].ToString();

                db.Persons.Add(person);
            }

            db.SaveChanges();
        }
    }
}