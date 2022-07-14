using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TomyChimmy.Areas.Identity.Data;
using TomyChimmy.Models;

namespace TomyChimmy.Data
{
    public class TomyChimmyDbContext : IdentityDbContext<User>
    {
        public TomyChimmyDbContext(DbContextOptions<TomyChimmyDbContext> options)
            : base(options)
        {
        }

        public DbSet<Food> Foods { get; set; }

        public DbSet<FoodType> FoodTypes { get; set; }

        public DbSet<Invoice> Invoices { get; set; }

        public DbSet<InvoiceDetail> InvoiceDetails { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<OrderDetail> OrderDetails { get; set; }

        public DbSet<PayingMethod> PayingMethods { get; set; }

        public DbSet<Queue> Queues { get; set; }

        public DbSet<QueueDetail> QueueDetails { get; set; }

        public DbSet<Status> Statuses { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }
    }
}
