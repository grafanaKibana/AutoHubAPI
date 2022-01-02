using AutoHub.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AutoHub.DAL.EntitySettings
{
    public class CarBrandConfiguration : IEntityTypeConfiguration<CarBrand>
    {
        public void Configure(EntityTypeBuilder<CarBrand> builder)
        {
            builder.ToTable("CarBrand");

            builder.HasMany(brand => brand.Cars)
                .WithOne(car => car.CarBrand)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}