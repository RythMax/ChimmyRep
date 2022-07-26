using System;
using System.ComponentModel.DataAnnotations;

namespace TomyChimmyAPI.Models
{
    public class UserViewModel
    {
        [Key]
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DOB { get; set; }

        public string PhoneNumber { get; set; }
        public string Password { get; set; }
    }
}
