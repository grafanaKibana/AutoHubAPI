using AutoHub.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AutoHub.DAL.EntitySettings
{
    public class CarColorConfiguration
    {
        public CarColorConfiguration(EntityTypeBuilder<CarColor> entity)
        {
            entity.ToTable("CarColor");
        }
    }
}