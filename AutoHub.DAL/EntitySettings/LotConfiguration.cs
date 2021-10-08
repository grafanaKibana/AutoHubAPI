using System;
using AutoHub.DAL.Entities;
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

            entity.HasOne(lot => lot.Creator)
                .WithMany(user => user.UserLots)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(lot => lot.Winner)
                .WithOne()
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}