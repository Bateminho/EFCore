﻿// <auto-generated />
using System;
using ConsoleApp.db;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace ConsoleApp.Migrations
{
    [DbContext(typeof(MyDbContext))]
    partial class MyDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("CanteenRating", b =>
                {
                    b.Property<int>("CanteensCanteenId")
                        .HasColumnType("int");

                    b.Property<int>("RatingsRatingId")
                        .HasColumnType("int");

                    b.HasKey("CanteensCanteenId", "RatingsRatingId");

                    b.HasIndex("RatingsRatingId");

                    b.ToTable("CanteenRating");
                });

            modelBuilder.Entity("ConsoleApp.db.Entities.Canteen", b =>
                {
                    b.Property<int>("CanteenId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CanteenId"));

                    b.Property<float>("AVGRating")
                        .HasColumnType("real");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CanteenName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ZipCode")
                        .HasColumnType("int");

                    b.HasKey("CanteenId");

                    b.ToTable("Canteens", (string)null);
                });

            modelBuilder.Entity("ConsoleApp.db.Entities.Customer", b =>
                {
                    b.Property<int>("CustomerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CustomerId"));

                    b.Property<string>("CustomerCPR")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Rating")
                        .HasColumnType("int");

                    b.HasKey("CustomerId");

                    b.ToTable("Customers", (string)null);
                });

            modelBuilder.Entity("ConsoleApp.db.Entities.Meal", b =>
                {
                    b.Property<int>("MealId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MealId"));

                    b.Property<int>("CanteenId")
                        .HasColumnType("int");

                    b.Property<string>("MealName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.HasKey("MealId");

                    b.HasIndex("CanteenId");

                    b.ToTable("Meals", (string)null);
                });

            modelBuilder.Entity("ConsoleApp.db.Entities.Rating", b =>
                {
                    b.Property<int>("RatingId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RatingId"));

                    b.Property<int>("CanteenId")
                        .HasColumnType("int");

                    b.Property<int>("CustomerId")
                        .HasMaxLength(11)
                        .HasColumnType("int");

                    b.Property<DateTime>("Datetime")
                        .HasColumnType("datetime2");

                    b.Property<float>("RatingValue")
                        .HasColumnType("real");

                    b.HasKey("RatingId");

                    b.HasIndex("CustomerId");

                    b.ToTable("Ratings", (string)null);
                });

            modelBuilder.Entity("ConsoleApp.db.Entities.Reservation", b =>
                {
                    b.Property<int>("ReservationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ReservationId"));

                    b.Property<int>("CanteenId")
                        .HasColumnType("int");

                    b.Property<int>("CustomerId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Reservationtime")
                        .HasColumnType("datetime2");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.HasKey("ReservationId");

                    b.HasIndex("CanteenId");

                    b.HasIndex("CustomerId");

                    b.ToTable("Reservations", (string)null);
                });

            modelBuilder.Entity("ConsoleApp.db.Entities.ReservationList", b =>
                {
                    b.Property<int>("ReservationId")
                        .HasColumnType("int");

                    b.Property<int>("MealId")
                        .HasColumnType("int");

                    b.HasKey("ReservationId", "MealId");

                    b.HasIndex("MealId");

                    b.ToTable("ReservationList", (string)null);
                });

            modelBuilder.Entity("CanteenRating", b =>
                {
                    b.HasOne("ConsoleApp.db.Entities.Canteen", null)
                        .WithMany()
                        .HasForeignKey("CanteensCanteenId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ConsoleApp.db.Entities.Rating", null)
                        .WithMany()
                        .HasForeignKey("RatingsRatingId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ConsoleApp.db.Entities.Meal", b =>
                {
                    b.HasOne("ConsoleApp.db.Entities.Canteen", "Canteen")
                        .WithMany("Meals")
                        .HasForeignKey("CanteenId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired()
                        .HasConstraintName("FK_Meals_Canteens");

                    b.Navigation("Canteen");
                });

            modelBuilder.Entity("ConsoleApp.db.Entities.Rating", b =>
                {
                    b.HasOne("ConsoleApp.db.Entities.Customer", "Customer")
                        .WithMany("Ratings")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("ConsoleApp.db.Entities.Reservation", b =>
                {
                    b.HasOne("ConsoleApp.db.Entities.Canteen", "Canteen")
                        .WithMany()
                        .HasForeignKey("CanteenId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ConsoleApp.db.Entities.Customer", "Customer")
                        .WithMany("Reservations")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired()
                        .HasConstraintName("FK_Reservations_Customers");

                    b.Navigation("Canteen");

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("ConsoleApp.db.Entities.ReservationList", b =>
                {
                    b.HasOne("ConsoleApp.db.Entities.Meal", "Meal")
                        .WithMany("ReservationLists")
                        .HasForeignKey("MealId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_ReservationList_Meals");

                    b.HasOne("ConsoleApp.db.Entities.Reservation", "Reservation")
                        .WithMany("ReservationLists")
                        .HasForeignKey("ReservationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_ReservationList_Reservations");

                    b.Navigation("Meal");

                    b.Navigation("Reservation");
                });

            modelBuilder.Entity("ConsoleApp.db.Entities.Canteen", b =>
                {
                    b.Navigation("Meals");
                });

            modelBuilder.Entity("ConsoleApp.db.Entities.Customer", b =>
                {
                    b.Navigation("Ratings");

                    b.Navigation("Reservations");
                });

            modelBuilder.Entity("ConsoleApp.db.Entities.Meal", b =>
                {
                    b.Navigation("ReservationLists");
                });

            modelBuilder.Entity("ConsoleApp.db.Entities.Reservation", b =>
                {
                    b.Navigation("ReservationLists");
                });
#pragma warning restore 612, 618
        }
    }
}
