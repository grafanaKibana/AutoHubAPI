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
        }
    }
}