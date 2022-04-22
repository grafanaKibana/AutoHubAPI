using AutoHub.DataAccess.EntitySettings;
using AutoHub.Domain.Entities;
using AutoHub.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

namespace AutoHub.DataAccess;

public class AutoHubContext : IdentityDbContext<ApplicationUser, ApplicationRole, int>
{
    public AutoHubContext(DbContextOptions<AutoHubContext> options) : base(options)
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
        optionsBuilder.UseLazyLoadingProxies();
        optionsBuilder.LogTo(Console.WriteLine);
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        new RoleConfiguration().Configure(builder.Entity<ApplicationRole>());
        new UserConfiguration().Configure(builder.Entity<ApplicationUser>());
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