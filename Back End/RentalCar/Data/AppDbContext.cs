using System;
using Microsoft.EntityFrameworkCore;
using RentalCar.Models;

namespace RentalCar.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<MsCar> MsCar {set; get;}
    public DbSet<MsCarImages> MsCarImages {set; get;}
    public DbSet<MsCustomer> MsCustomer {set; get;}
    public DbSet<MsEmployee> MsEmployee {set; get;}
    public DbSet<TrMaintenance> TrMaintenance {set; get;}
    public DbSet<TrPayment> TrPayment {set; get;}
    public DbSet<TrRental> TrRental {set; get;}

        protected override void OnModelCreating(ModelBuilder modelBuilder){

            modelBuilder.UseCollation("SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<MsCar>(entity =>
            {
                entity.Property(e => e.Car_id)
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.name)
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.model)
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.license_plate)
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.transmission)
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.price_per_day)
                    .HasColumnType("decimal(18, 2)");
            });

            modelBuilder.Entity<MsCarImages>(entity =>
            {
                entity.Property(e => e.Image_car_id)
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.image_link)
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");
            });

            modelBuilder.Entity<MsCustomer>(entity =>
            {
                entity.Property(e => e.Customer_id)
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.email)
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.password)
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.name)
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.phone_number)
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.address)
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.driver_license_number)
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");
            });


            modelBuilder.Entity<MsEmployee>(entity =>
            {
                entity.Property(e => e.Employee_id)
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.name)
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.position)
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.email)
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.phone_number)
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");
            });

 
            modelBuilder.Entity<TrMaintenance>(entity =>
            {
                entity.Property(e => e.Maintenance_id)
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.description)
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");


                entity.Property(e => e.cost)
                    .HasColumnType("decimal(18, 2)");
            });


            modelBuilder.Entity<TrRental>(entity =>
            {
                entity.Property(e => e.Rental_id)
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");


                entity.Property(e => e.total_price)
                    .HasColumnType("decimal(18, 2)");
            });


            modelBuilder.Entity<TrPayment>(entity =>
            {
                entity.Property(e => e.Payment_id)
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");


                entity.Property(e => e.amount)
                    .HasColumnType("decimal(18, 2)");
            });
        }

}
