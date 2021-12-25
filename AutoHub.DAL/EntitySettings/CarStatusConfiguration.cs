using AutoHub.DAL.Entities;
using AutoHub.DAL.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Linq;

namespace AutoHub.DAL.EntitySettings
{
    public class CarStatusConfiguration : IEntityTypeConfiguration<CarStatus>
    {
        public void Configure(EntityTypeBuilder<CarStatus> builder)
        {
            builder.ToTable("CarStatus").HasKey(status => status.CarStatusId);

            builder.HasData(
                Enum.GetValues(typeof(CarStatusEnum))
                    .Cast<CarStatusEnum>()
                    .Select(s => new CarStatus
                    {
                        CarStatusId = s,
                        CarStatusName = s.ToString()
                    }));

            builder.HasMany(status => status.Cars)
                .WithOne(car => car.CarStatus)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}