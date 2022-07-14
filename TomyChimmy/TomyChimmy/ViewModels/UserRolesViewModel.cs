using System.Collections;
using System.Collections.Generic;

namespace TomyChimmy.ViewModels
{
    public class UserRolesViewModel
    {
        public string Id { get; set; }

        public string Nombres { get; set; }

        public string Apellidos { get; set; }

        public string UserName { get; set; }

        public string Email { get; set; }

        public IEnumerable<string> Roles { get; set; }
    }
}
