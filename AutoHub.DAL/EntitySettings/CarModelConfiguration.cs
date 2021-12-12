using AutoHub.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AutoHub.DAL.EntitySettings
{
    public class CarModelConfiguration
    {
        public CarModelConfiguration(EntityTypeBuilder<CarModel> entity)
        {
            entity.ToTable("CarModel");

            entity.HasMany(model => model.Cars)
                .WithOne(car => car.CarModel)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}