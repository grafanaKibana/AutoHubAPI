using AutoHub.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AutoHub.DataAccess.EntitySettings;

public class CarBrandConfiguration : IEntityTypeConfiguration<CarBrand>
{
    public void Configure(EntityTypeBuilder<CarBrand> builder)
    {
        builder.ToTable(nameof(CarBrand));

        builder.HasMany(brand => brand.Cars)
            .WithOne(car => car.CarBrand)
            .OnDelete(DeleteBehavior.Cascade);
    }
}