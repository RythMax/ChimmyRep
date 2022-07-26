using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TommyAPI.Models;

namespace TommyAPI.Data
{
    public class TommyAPIContext : IdentityDbContext<User>
    {
        public TommyAPIContext(DbContextOptions<TommyAPIContext> options):base(options)
        {
        }

        public virtual DbSet<Cart> Carts { get; set; }

        public virtual DbSet<Food> Foods { get; set; }

        public virtual DbSet<FoodType> FoodTypes { get; set; }

        public virtual DbSet<Invoice> Invoices { get; set; }

        public virtual DbSet<InvoiceDetail> InvoiceDetails { get; set; }

        public virtual DbSet<Order> Orders { get; set; }

        public virtual DbSet<OrderDetail> OrderDetails { get; set; }

        public virtual DbSet<PayingMethod> PayingMethods { get; set; }

        public virtual DbSet<Queue> Queues { get; set; }

        public virtual DbSet<QueueDetail> QueueDetails { get; set; }

        public virtual DbSet<Status> Statuses { get; set; }
    }
}
