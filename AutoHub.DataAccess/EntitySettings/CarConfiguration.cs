using AutoHub.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AutoHub.DataAccess.EntitySettings;

public class CarConfiguration : IEntityTypeConfiguration<Car>
{
    public void Configure(EntityTypeBuilder<Car> builder)
    {
        builder.ToTable(nameof(Car)).HasKey(car => car.CarId);

        builder.Navigation(car => car.CarBrand).AutoInclude();
        builder.Navigation(car => car.CarModel).AutoInclude();
        builder.Navigation(car => car.CarColor).AutoInclude();
        builder.Navigation(car => car.CarStatus).AutoInclude();

        builder.Property(car => car.Description).IsRequired();
        builder.Property(car => car.Year).IsRequired().HasMaxLength(4);
        builder.Property(car => car.VIN).IsRequired().HasMaxLength(17);
        builder.Property(car => car.Mileage).IsRequired();
        builder.Property(car => car.CostPrice).IsRequired();
        builder.Property(car => car.SellingPrice).IsRequired();
    }
}
