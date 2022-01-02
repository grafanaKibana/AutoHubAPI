using System;
using System.Runtime.CompilerServices;
using AutoHub.DAL.Entities;
using AutoHub.DAL.Entities.Identity;
using AutoHub.DAL.EntitySettings;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AutoHub.DAL
{
    public class AutoHubContext : IdentityDbContext<AppUser, AppRole, int>
    {
        public AutoHubContext()
        {
        }

        public AutoHubContext(DbContextOptions<AutoHubContext> options)
            : base(options)
        {
        }
        //DbSets [Entities]
        public virtual DbSet<Car> Cars { get; set; }
        public virtual DbSet<CarBrand> CarBrands { get; set; }
        public virtual DbSet<CarModel> CarModels { get; set; }
        public virtual DbSet<CarColor> CarColors { get; set; }
        public virtual DbSet<Lot> Lots { get; set; }
        public virtual DbSet<Bid> Bids { get; set; }
        public virtual DbSet<CarStatus> CarStatus { get; set; }
        public virtual DbSet<LotStatus> LotStatus { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                "Data Source=SQL5101.site4now.net;Initial Catalog=db_a7d938_autohubdb;User Id=db_a7d938_autohubdb_admin;Password=db_a7d938");
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            new RoleConfiguration().Configure(builder.Entity<AppRole>());
            new UserConfiguration().Configure(builder.Entity<AppUser>());
            new CarConfiguration().Configure(builder.Entity<Car>());
            new CarBrandConfiguration().Configure(builder.Entity<CarBrand>());
            new CarModelConfiguration().Configure(builder.Entity<CarModel>());
            new CarColorConfiguration().Configure(builder.Entity<CarColor>());
            new LotConfiguration().Configure(builder.Entity<Lot>());
            new BidConfiguration().Configure(builder.Entity<Bid>());
            new CarStatusConfiguration().Configure(builder.Entity<CarStatus>());
            new LotStatusConfiguration().Configure(builder.Entity<LotStatus>());
        }
    }
}