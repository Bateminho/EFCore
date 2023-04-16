using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;
using System.Xml.Linq;

namespace Data
{
    public class MyDbContext : DbContext
    {
        public DbSet<Meal> Meals { get; set; }
        public DbSet<Canteen> Canteens { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<AutomatedDeliverySystem> AutomatedDeliverySystems { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Rating> Ratings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure primary keys
            modelBuilder.Entity<Meal>().HasKey(m => m.MealID);
            modelBuilder.Entity<Canteen>().HasKey(c => c.CanteenID);
            modelBuilder.Entity<Reservation>().HasKey(r => r.ReservationID);
            modelBuilder.Entity<AutomatedDeliverySystem>().HasKey(a => a.DropInGuest);
            modelBuilder.Entity<Customer>().HasKey(c => c.CustomerCPR);
            modelBuilder.Entity<Rating>().HasKey(r => r.RatingID);

            // Configure foreign keys
            modelBuilder.Entity<Canteen>()
                .HasOne(c => c.AutomatedDeliverySystem)
                .WithMany(a => a.Canteens)
                .HasForeignKey(c => c.DropInGuest);

            modelBuilder.Entity<Meal>()
                .HasOne(m => m.Canteen)
                .WithMany(c => c.Meals)
                .HasForeignKey(m => m.CanteenID);

            modelBuilder.Entity<Meal>()
                .HasOne(m => m.Reservation)
                .WithMany(r => r.Meals)
                .HasForeignKey(m => m.ReservationID);

            modelBuilder.Entity<Reservation>()
                .HasOne(r => r.Customer)
                .WithMany(c => c.Reservations)
                .HasForeignKey(r => r.CustomerCPR);

            modelBuilder.Entity<Rating>()
                .HasOne(r => r.Customer)
                .WithMany(c => c.Ratings)
                .HasForeignKey(r => r.CustomerCPR);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Configure database provider and connection string
            optionsBuilder.UseSqlServer($"Data Source=localhost;Initial Catalog=MyDB;User ID=SA;Password=Bzva8210;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");
        }
    }

}
