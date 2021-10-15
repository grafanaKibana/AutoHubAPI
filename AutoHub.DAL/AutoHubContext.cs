using System;
using AutoHub.DAL.Entities;
using AutoHub.DAL.EntitySettings;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace AutoHub.DAL
{
    public class AutoHubContext : DbContext
    {
        //DbSets [Entities]
        public DbSet<Car> Car { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<Lot> Lot { get; set; }
        public DbSet<CarStatus> CarStatus { get; set; }
        public DbSet<LotStatus> LotStatus { get; set; }
        public DbSet<UserRole> UserRole { get; set; }

        public AutoHubContext()
        {
            
        }
        
        public AutoHubContext(DbContextOptions<AutoHubContext> options)
           : base(options)
        {
        }

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
            new LotConfiguration(modelBuilder.Entity<Lot>());
            new CarStatusConfiguration(modelBuilder.Entity<CarStatus>());
            new LotStatusConfiguration(modelBuilder.Entity<LotStatus>());
            new UserRoleConfiguration(modelBuilder.Entity<UserRole>());
        }
    }
}