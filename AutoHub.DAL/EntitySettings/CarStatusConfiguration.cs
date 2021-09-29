using System;
using System.Linq;
using AutoHub.DAL.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AutoHub.DAL.EntitySettings
{
    public class CarStatusConfiguration
    {
        public CarStatusConfiguration(EntityTypeBuilder<CarStatus> entity)
        {
            entity.Property(status => status.CarStatusId).HasConversion<int>();
            entity.Property(status => status.CarStatusName).HasConversion<string>();

            entity.HasData(
                Enum.GetValues(typeof(CarStatus.Status))
                    .Cast<CarStatus.Status>()
                    .Select(status => new CarStatus()
                    {
                        CarStatusId = status,
                        CarStatusName = status.ToString()
                    })
                );
        }
    }
}