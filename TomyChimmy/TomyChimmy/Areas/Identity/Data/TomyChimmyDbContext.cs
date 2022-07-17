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

        public DbSet<Cart> Carts { get; set; }

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

//Creo que estas lineas no son necesarias

/*
            
            builder.Entity<Cart>(entity =>
            {
                //entity.Property(e => e.CartId).IsUnicode(false);

                entity.Property(e => e.Cantidad).HasDefaultValueSql("((1))");

                entity.Property(e => e.Username).IsUnicode(false);

                builder
                .Entity<Cart>()
                .Property(e => e.CartId)
                .ValueGeneratedOnAdd();

                entity.HasOne(d => d.Food)
                .WithMany(f => f.Cart)
                .HasForeignKey(d => d.ID_Comidas)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Cart_ID_Comidas");
            });
            
            builder.Entity<Cart>(entity =>
            {
                //entity.Property(e => e.CartId).IsUnicode(false);

                entity.Property(e => e.Cantidad).HasDefaultValueSql("((1))");

                entity.Property(e => e.Username).IsUnicode(false);

                entity.HasOne(d => d.Food)
                .WithMany(f => f.Cart)
                .HasForeignKey(d => d.ID_Comidas)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Cart_ID_Comidas");
            });

            builder.Entity<FoodType>(entity =>
            {
                entity.Property(e => e.Detalle).IsUnicode(false);
            });
Pero si se necesitan, estaran en el S1E3 MVC NetCore Part 2
 */