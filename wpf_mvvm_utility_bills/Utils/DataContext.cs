using wpf_mvvm_utility_bills.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Globalization;


namespace wpf_mvvm_utility_bills.Utils
{
    public class DataContext : DbContext
    {
        public DbSet<MeterDevice> Meters { get; set; } = null!;

        public DbSet<Supplier> Suppliers { get; set; } = null!;

        public DbSet<ResourceType> ResourceTypes { get; set; } = null!;

        public DbSet<ValueLog> ValueLog { get; set; } = null!;

        public DbSet<PaidLog> PaidLog { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(@"DataSource=data.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ResourceType>(t =>
            {
                t.ToTable("ResourceTypes");
                t.HasKey("Id");
                t.Property("Id").IsRequired();
            });
            
            modelBuilder.Entity<Supplier>(t =>
            {
                t.ToTable("Suppliers");
                t.HasKey("Id");
                t.Property("Id").IsRequired();

            });

            modelBuilder.Entity<MeterDevice>(t =>
            {
                t.ToTable("MeterDevices");
                t.HasKey("Id");
            });

            modelBuilder.Entity<ValueLog>(t =>
            {
                t.ToTable("ValueLog");
                t.HasKey("Id");
            });

            modelBuilder.Entity<PaidLog>(t =>
            {
                t.ToTable("PaidLog");
                t.HasKey("Id");
            });


            var converter = new ValueConverter<DateTime?, string>(val => val.Value.ToString("dd-MM-yyyy HH:mm"),
                val => DateTime.ParseExact(val, "dd-MM-yyyy HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None));
            modelBuilder.Entity<MeterDevice>().Property(f => f.DateExpired).HasConversion(converter);
            modelBuilder.Entity<MeterDevice>().Property(f => f.DateIssued).HasConversion(converter);
        }

    }
}
