using System.Collections.Generic;
using TomyChimmy.Areas.Identity.Data;
using TomyChimmy.Models;

namespace TomyChimmy.ViewModels
{
    public class InvoiceViewModel
    {
        public Models.Invoice Invoice { get; set; }

        public InvoiceDetail InvoiceDetail { get; set; }

        public PayingMethod PayingMethod { get; set; }

        public User User { get; set; }

        public List<InvoiceDetail> Articulos { get; set; }
    }
}
