using AutoHub.DAL.Entities;
using AutoHub.DAL.EntitySettings;
using Microsoft.EntityFrameworkCore;

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
        public virtual DbSet<Car> Cars { get; set; }
        public virtual DbSet<CarBrand> CarBrands { get; set; }
        public virtual DbSet<CarModel> CarModels { get; set; }
        public virtual DbSet<CarColor> CarColors { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Lot> Lots { get; set; }
        public virtual DbSet<Bid> Bids { get; set; }
        public virtual DbSet<CarStatus> CarStatus { get; set; }
        public virtual DbSet<LotStatus> LotStatus { get; set; }
        public virtual DbSet<UserRole> UserRole { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                "Data Source=SQL5101.site4now.net;Initial Catalog=db_a7d938_autohubdb;User Id=db_a7d938_autohubdb_admin;Password=db_a7d938");
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