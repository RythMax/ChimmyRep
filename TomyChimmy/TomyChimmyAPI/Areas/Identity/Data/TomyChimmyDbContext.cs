using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TomyChimmy.Models;
using TomyChimmyAPI.Models;

namespace TomyChimmyAPI.Areas.Identity.Data
{
    public class TomyChimmyDbContext : IdentityDbContext<User>
    {
        public TomyChimmyDbContext(DbContextOptions<TomyChimmyDbContext> options):base(options)
        {
        }
        public DbSet<Cart> Carts { get; set; }

        public DbSet<Food> Foods { get; set; }

        public DbSet<FoodType> FoodTypes { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<OrderDetail> OrderDetails { get; set; }

        public DbSet<Invoice> Invoices { get; set; }

        public DbSet<InvoiceDetail> InvoiceDetails { get; set; }

        public DbSet<PayingMethod> PayingMethods { get; set; }

        public DbSet<Queue> Queues { get; set; }

        public DbSet<QueueDetail> QueueDetails { get; set; }

        public DbSet<Status> Statuses { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
