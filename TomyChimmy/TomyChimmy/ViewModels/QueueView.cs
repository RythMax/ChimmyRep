using TomyChimmy.Models;
using TomyChimmy.Areas.Identity.Data;
using System.Collections.Generic;

namespace TomyChimmy.ViewModels
{
    public class QueueView
    {
        public Models.Queue Queue { get; set; }

        public Cart Cart { get; set; }

        public PayingMethod PayingMethod { get; set; }

        public User User { get; set; }

        public List<Cart> Articulos { get; set; }
    }
}
