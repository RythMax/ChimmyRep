using System.Collections.Generic;
using TommyAPI.Models;

namespace TommyAPI.ViewModels
{
    public class QueueDetailsView
    {
        public Models.Queue Queue { get; set; }

        public QueueDetail QueueDetail { get; set; }

        public PayingMethod PayingMethod { get; set; }

        public User User { get; set; }

        public List<QueueDetail> Artículos { get; set; }
    }
}
