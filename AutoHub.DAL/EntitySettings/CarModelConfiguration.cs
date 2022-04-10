using AutoHub.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AutoHub.DAL.EntitySettings
{
    public class CarModelConfiguration : IEntityTypeConfiguration<CarModel>
    {
        public void Configure(EntityTypeBuilder<CarModel> builder)
        {
            builder.ToTable(nameof(CarModel));

            builder.HasMany(model => model.Cars)
                .WithOne(car => car.CarModel)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}