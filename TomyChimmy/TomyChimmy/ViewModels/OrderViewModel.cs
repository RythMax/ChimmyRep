using System.Collections.Generic;
using TomyChimmy.Areas.Identity.Data;
using TomyChimmy.Models;

namespace TomyChimmy.ViewModels
{
    public class OrderViewModel
    {
        public Models.Order Order { get; set; }

        public OrderDetail OrderDetail { get; set; }

        public PayingMethod PayingMethod { get; set; }

        public User User { get; set; }

        public List<OrderDetail> Artículos { get; set; }
    }
}
