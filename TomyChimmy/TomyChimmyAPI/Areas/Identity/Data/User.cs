using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace TomyChimmyAPI.Areas.Identity.Data
{
    public class User : IdentityUser
    {
        [PersonalData]
        [Column(TypeName = "nvarchar(100)")]
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
    }
}
