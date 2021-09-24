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
            
            //TODO: Add settings to Car and Winner if needed
            
            entity.Property(lot => lot.StartTime).IsRequired();
            entity.Property(lot => lot.StartPrice).IsRequired();

            entity.HasOne(lot => lot.User)
                .WithMany(user => user.UserLots);
        }
    }
}