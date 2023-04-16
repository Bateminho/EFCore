using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;
using System.Xml.Linq;
using ConsoleApp.db.Entities;

namespace ConsoleApp.db
{
    public class MyDbContext : DbContext
    {
	    private const string DbName = "EFCORE";
	    private const string ConnectionString = $"Data Source=localhost;Initial Catalog={DbName};User ID=SA;Password=<YourStrong@Passw0rd>;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";

		public DbSet<Meal> Meals { get; set; }
        public DbSet<Canteen> Canteens { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Rating> Ratings { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			//// Configure primary keys
			//modelBuilder.Entity<Meal>().HasKey(m => m.MealId);
			//modelBuilder.Entity<Canteen>().HasKey(c => c.CanteenId);
			//modelBuilder.Entity<Reservation>().HasKey(r => r.ReservationId);
			//modelBuilder.Entity<AutomatedDeliverySystem>().HasKey(a => a.ADSId);
			//modelBuilder.Entity<Customer>().HasKey(c => c.CustomerId);
			//modelBuilder.Entity<Rating>().HasKey(r => r.RatingId);

			modelBuilder.Entity<Meal>(entity =>
			{
				entity.ToTable("Meals");

				entity.HasOne(e => e.Canteen)
					.WithMany(c => c.Meals)
					.HasForeignKey(e => e.CanteenId)
					.IsRequired()
					.OnDelete(DeleteBehavior.Restrict)
					.HasConstraintName("FK_Meals_Canteens");
			});

			modelBuilder.Entity<Canteen>(entity =>
			{
				entity.ToTable("Canteens");

				entity.HasKey(e => e.CanteenId);

				// other properties and configurations for Canteen entity
			});

			modelBuilder.Entity<ReservationList>(entity =>
			{
				entity.ToTable("ReservationLists");

				// Specify composite primary key
				entity.HasKey(e => new { e.ReservationId, e.MealId });

				entity.HasOne(e => e.Reservation)
					.WithMany(r => r.ReservationLists)
					.HasForeignKey(e => e.ReservationId)
					.IsRequired()
					.OnDelete(DeleteBehavior.Cascade)
					.HasConstraintName("FK_ReservationLists_Reservations");

				entity.HasOne(e => e.Meal)
					.WithMany(m => m.ReservationLists)
					.HasForeignKey(e => e.MealId)
					.IsRequired()
					.OnDelete(DeleteBehavior.Cascade)
					.HasConstraintName("FK_ReservationLists_Meals");
			});


			modelBuilder.Entity<ReservationList>(entity =>
			{
				entity.ToTable("ReservationList");

				entity.HasKey(e => new { e.ReservationId, e.MealId });

				entity.HasOne(e => e.Reservation)
					.WithMany(r => r.ReservationLists)
					.HasForeignKey(e => e.ReservationId)
					.IsRequired()
					.OnDelete(DeleteBehavior.Cascade)
					.HasConstraintName("FK_ReservationList_Reservations");

				entity.HasOne(e => e.Meal)
					.WithMany(m => m.ReservationLists)
					.HasForeignKey(e => e.MealId)
					.IsRequired()
					.OnDelete(DeleteBehavior.Cascade)
					.HasConstraintName("FK_ReservationList_Meals");
			});

			modelBuilder.Entity<Reservation>(entity =>
			{
				entity.ToTable("Reservations");

				entity.HasOne(e => e.Customer)
					.WithMany(c => c.Reservations)
					.HasForeignKey(e => e.CustomerId)
					.IsRequired()
					.OnDelete(DeleteBehavior.Restrict)
					.HasConstraintName("FK_Reservations_Customers");
			});

			modelBuilder.Entity<Customer>(entity =>
			{
				entity.ToTable("Customers");

				entity.HasKey(e => e.CustomerId);

				// other properties and configurations for Customer entity
			});

			modelBuilder.Entity<Rating>(entity =>
			{
				entity.ToTable("Ratings");

				entity.HasKey(e => e.RatingId);

				// other properties and configurations for Rating entity
			});



		}


		protected override void OnConfiguring(DbContextOptionsBuilder options)
				=> options.UseSqlServer(ConnectionString);
		//optionsBuilder.UseInMemoryDatabase(databaseName: "AutoSystem");
	}
    
    
}
//modelBuilder.Entity<Meal>(entity =>
//{
//	entity.ToTable("Meals");

//	entity.HasKey(e => e.MealID);

//	entity.Property(e => e.MealName)
//		.IsRequired();

//	entity.Property(e => e.MealType)
//		.IsRequired();

//	entity.Property(e => e.ReservationID)
//		.IsRequired();

//	entity.Property(e => e.CanteenID)
//		.IsRequired();

//	entity.HasOne(e => e.Canteen)
//		.WithMany()
//		.HasForeignKey(e => e.CanteenID)
//		.OnDelete(DeleteBehavior.Cascade);

//	entity.HasOne(e => e.Reservation)
//		.WithMany()
//		.HasForeignKey(e => e.ReservationID)
//		.OnDelete(DeleteBehavior.Restrict);
//});

//modelBuilder.Entity<Canteen>(entity =>
//{
//	entity.ToTable("Canteens");

//	entity.HasKey(e => e.CanteenID);

//	// other properties and configurations for Canteen entity
//});

//modelBuilder.Entity<Reservation>(entity =>
//{
//	entity.ToTable("Reservations");

//	entity.HasKey(e => e.ReservationID);

//	// other properties and configurations for Reservation entity
//});
//}
//}