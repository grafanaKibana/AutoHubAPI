using AutoHub.DAL.Entities;
using AutoHub.DAL.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AutoHub.DAL.EntitySettings
{
    public class CarConfiguration
    {
        public CarConfiguration(EntityTypeBuilder<Car> entity)
        {
            entity.ToTable("Car").HasKey(car => car.CarId);
            entity.Property(car => car.Brand).IsRequired().HasMaxLength(20);
            entity.Property(car => car.Model).IsRequired().HasMaxLength(20);
            entity.Property(car => car.Description).IsRequired();
            entity.Property(car => car.Color).IsRequired();
            entity.Property(car => car.Year).IsRequired().HasMaxLength(4);
            entity.Property(car => car.VIN).IsRequired().HasMaxLength(17);
            entity.Property(car => car.Mileage).IsRequired();
            entity.Property(car => car.CostPrice).IsRequired()/*.HasPrecision(2)*/;
            entity.Property(car => car.SellingPrice).IsRequired()/*.HasPrecision(2)*/;
            entity.Property(car => car.CarStatusId).HasConversion<int>();

            entity.HasData(
                new Car
                {
                    CarId = 1,
                    Brand = "Audi",
                    Model = "RS6",
                    Description = "Audi endows the RS6 Avant with a twin-turbocharged 4.0-liter V-8, " +
                                  "which generates 591 horsepower and 590 pound-feet of torque. ... " +
                                  "The combination helped rocket our 5031-pound test car to 60 mph in 3.1 seconds " +
                                  "and complete the quarter-mile in 11.5 ticks at 120 mph.",
                    Color = "Nardo Gray",
                    VIN = "3B7KF23Z42M211215",
                    Year = 2021,
                    CostPrice = 92000,
                    SellingPrice = 138000,
                    Mileage = 12302,
                    CarStatusId = CarStatusId.OnHold
                },
                new Car
                {
                    CarId = 2,
                    Brand = "Audi",
                    Model = "RS5 Sportback",
                    Description = "Under the RS5's sinewy clamshell hood sits a twin-turbo 2.9-liter " +
                                  "V-6 that pumps out 444 horsepower and 443 pound-feet of torque. The " +
                                  "power routes through Audi's rear-biased Quattro all-wheel-drive system" +
                                  " via a smooth-shifting eight-speed automatic transmission",
                    Color = "Turbo Blue",
                    VIN = "1FUPDXYB3PP469921",
                    Year = 2020,
                    CostPrice = 68500,
                    SellingPrice = 88300,
                    Mileage = 32161,
                    CarStatusId = CarStatusId.Sold
                });
        }
    }
}