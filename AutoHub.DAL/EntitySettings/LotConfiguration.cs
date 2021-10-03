using System;
using AutoHub.DAL.Entities;
using AutoHub.DAL.Enums;
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

            /*entity.HasData(
                new Lot
                {
                    LotId = 1,
                    CreatorId = 1,
                    CarId = 2,
                    Winner = null,
                    StartTime = DateTime.UtcNow,
                    EndTime = DateTime.UtcNow.AddDays(7),
                    StartPrice = 92000,
                    LastBid = 92000 * 1.2m,
                    LotStatusId = LotStatusId.InProgress
                });
                */

            /*entity.HasData(
                new Lot
                {
                    LotId = 1,
                    Creator = new AutoHubContext().User.Find(1),
                    Car = new AutoHubContext().Car.Find(1),
                    Winner = null,
                    StartTime = DateTime.UtcNow,
                    EndTime = DateTime.UtcNow.AddDays(7),
                    StartPrice = new AutoHubContext().Car.Find(1).SellingPrice,
                    LastBid = new AutoHubContext().Car.Find(1).SellingPrice * 1.2m,
                    LotStatusId = LotStatusId.InProgress
                }
            );*/
        }
    }
}