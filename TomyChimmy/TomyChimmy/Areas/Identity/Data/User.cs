using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace TomyChimmy.Areas.Identity.Data
{
    // Add profile data for application users by adding properties to the User class
    public class User : IdentityUser
    {
        [PersonalData]
        [Column(TypeName ="nvarchar(100)")]
        public string Nombres { get; set; }


        [PersonalData]
        [Column(TypeName = "nvarchar(100)")]
        public string Apellidos { get; set; }

        [PersonalData]
        [Column(TypeName = "nvarchar(150)")]
        public string Dirección { get; set; }


        [PersonalData]
        [Column(TypeName = "nchar(11)")]
        public string Cédula { get; set; }

        /*[PersonalData]
        [Column(TypeName = "nvarchar(10)")]
        public string roleName { get; set; }*/
    }
}
