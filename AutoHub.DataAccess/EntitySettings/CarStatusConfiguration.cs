using AutoHub.Domain.Entities;
using AutoHub.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Linq;

namespace AutoHub.DataAccess.EntitySettings;

public class CarStatusConfiguration : IEntityTypeConfiguration<CarStatus>
{
    public void Configure(EntityTypeBuilder<CarStatus> builder)
    {
        builder.ToTable(nameof(CarStatus)).HasKey(status => status.CarStatusId);

        builder.HasData(
            Enum.GetValues(typeof(CarStatusEnum))
                .Cast<CarStatusEnum>()
                .Select(status => new CarStatus
                {
                    CarStatusId = status,
                    CarStatusName = status.ToString()
                }));

        builder.HasMany(status => status.Cars)
            .WithOne(car => car.CarStatus)
            .OnDelete(DeleteBehavior.NoAction);
    }
}