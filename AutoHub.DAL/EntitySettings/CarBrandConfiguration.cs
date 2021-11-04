using AutoHub.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AutoHub.DAL.EntitySettings
{
    public class CarBrandConfiguration
    {
        public CarBrandConfiguration(EntityTypeBuilder<CarBrand> entity)
        {
            entity.ToTable("CarBrand");

            entity.HasMany(brand => brand.Cars)
                .WithOne(car => car.CarBrand)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}