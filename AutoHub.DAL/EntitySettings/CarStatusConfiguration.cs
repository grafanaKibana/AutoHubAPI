using System;
using System.Linq;
using AutoHub.DAL.Entities;
using AutoHub.DAL.Enums;
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
                Enum.GetValues(typeof(ECarStatus))
                    .Cast<ECarStatus>()
                    .Select(status => new CarStatus()
                    {
                        CarStatusId = status,
                        CarStatusName = status.ToString()
                    })
                );
        }
    }
}