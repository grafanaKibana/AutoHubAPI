using System;
using AutoHub.DAL.Entities;
using AutoHub.DAL.EntitySettings;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace AutoHub.DAL
{
    public class AutoHubContext : DbContext
    {
        public AutoHubContext()
        {
        }

        public AutoHubContext(DbContextOptions<AutoHubContext> options)
            : base(options)
        {
        }

        //DbSets [Entities]
        public DbSet<Car> Cars { get; set; }
        public DbSet<CarBrand> CarBrands { get; set; }
        public DbSet<CarModel> CarModels { get; set; }
        public DbSet<CarColor> CarColors { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Lot> Lots { get; set; }
        public DbSet<Bid> Bids { get; set; }
        public DbSet<CarStatus> CarStatus { get; set; }
        public DbSet<LotStatus> LotStatus { get; set; }
        public DbSet<UserRole> UserRole { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            IConfigurationRoot configurationRoot = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json")
                .Build();
            
            optionsBuilder.UseSqlServer(
                configurationRoot.GetConnectionString("LocalConnectionString"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            new UserConfiguration(modelBuilder.Entity<User>());
            new CarConfiguration(modelBuilder.Entity<Car>());
            new CarBrandConfiguration(modelBuilder.Entity<CarBrand>());
            new CarModelConfiguration(modelBuilder.Entity<CarModel>());
            new CarColorConfiguration(modelBuilder.Entity<CarColor>());
            new LotConfiguration(modelBuilder.Entity<Lot>());
            new BidConfiguration(modelBuilder.Entity<Bid>());
            new CarStatusConfiguration(modelBuilder.Entity<CarStatus>());
            new LotStatusConfiguration(modelBuilder.Entity<LotStatus>());
            new UserRoleConfiguration(modelBuilder.Entity<UserRole>());
        }
    }
}