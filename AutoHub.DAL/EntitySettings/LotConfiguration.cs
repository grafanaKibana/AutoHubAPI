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

            entity.Property(lot => lot.StartTime).IsRequired();

            /*entity.Navigation(lot => lot.LotStatus).AutoInclude();
            entity.Navigation(lot => lot.Creator).AutoInclude();
            entity.Navigation(lot => lot.Creator.UserRole).AutoInclude();
            entity.Navigation(lot => lot.Car).AutoInclude();
            entity.Navigation(lot => lot.Car.CarBrand).AutoInclude();
            entity.Navigation(lot => lot.Car.CarModel).AutoInclude();
            entity.Navigation(lot => lot.Car.CarColor).AutoInclude();
            entity.Navigation(lot => lot.Car.CarStatus).AutoInclude();
            entity.Navigation(lot => lot.Winner).AutoInclude();
            entity.Navigation(lot => lot.Winner.UserRole).AutoInclude();*/

            entity.HasOne(lot => lot.Creator)
                .WithMany(user => user.UserLots)
                .OnDelete(DeleteBehavior.NoAction);

            entity.HasOne(lot => lot.Winner)
                .WithMany(user => user.VictoryLots)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}