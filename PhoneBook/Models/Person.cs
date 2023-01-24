using System;
using System.ComponentModel.DataAnnotations;

namespace PhoneBook.Models
{
    public class Person
    {
        [Key]
        public int Id { get; set; }

        public string Title { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime Dob { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public string Password { get; set; }

        public string LargePicture { get; set; }

        public string MediumPicture { get; set; }

        public string ThumbnailPicture { get; set; }

    }

}
