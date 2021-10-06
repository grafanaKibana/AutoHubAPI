using System;
using AutoHub.DAL.Entities;
using AutoHub.DAL.Enums;
using AutoHub.DAL.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AutoHub.DAL.EntitySettings
{
    public class LotConfiguration
    {
        public LotConfiguration(EntityTypeBuilder<Lot> entity)
        {
            entity.ToTable("Lot").HasKey(lot => lot.LotId);

            entity.Property(lot => lot.StartTime).IsRequired().HasDefaultValue(DateTime.UtcNow);
            entity.Property(lot => lot.StartPrice)
                .IsRequired() /*.HasDefaultValue(entity.Property(lot => lot.Car.SellingPrice))*/;

            entity.HasOne(lot => lot.Creator)
                .WithMany(user => user.UserLots);
        }
    }
}