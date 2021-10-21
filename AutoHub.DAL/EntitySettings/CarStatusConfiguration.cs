using System;
using System.Linq;
using AutoHub.DAL.Entities;
using AutoHub.DAL.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AutoHub.DAL.EntitySettings
{
    public class CarStatusConfiguration
    {
        public CarStatusConfiguration(EntityTypeBuilder<CarStatus> entity)
        {
            entity.ToTable("CarStatus").HasKey(status => status.CarStatusId);

            entity.HasData(
                Enum.GetValues(typeof(CarStatusEnum))
                    .Cast<CarStatusEnum>()
                    .Select(s => new CarStatus
                    {
                        CarStatusId = s,
                        CarStatusName = s.ToString()
                    }));
        }
    }
}