using AutoHub.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AutoHub.DAL.EntitySettings
{
    public class CarConfiguration
    {
        public CarConfiguration(EntityTypeBuilder<Car> entity)
        {
            entity.ToTable("Car").HasKey(car => car.CarId);
            entity.Property(car => car.Description).IsRequired();
            entity.Property(car => car.Year).IsRequired().HasMaxLength(4);
            entity.Property(car => car.VIN).IsRequired().HasMaxLength(17);
            entity.Property(car => car.Mileage).IsRequired();
            entity.Property(car => car.CostPrice).IsRequired();
            entity.Property(car => car.SellingPrice).IsRequired();

            entity.HasOne(car => car.CarBrand)
                .WithOne()
                .IsRequired();
            entity.HasOne(car => car.CarModel)
                .WithOne()
                .IsRequired();
            entity.HasOne(car => car.CarBrand)
                .WithOne()
                .IsRequired();
        }
    }
}