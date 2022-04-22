using AutoHub.Domain.Entities.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AutoHub.DataAccess.EntitySettings;

internal class UserConfiguration : IEntityTypeConfiguration<ApplicationUser>
{
    public void Configure(EntityTypeBuilder<ApplicationUser> builder)
    {
        builder.HasMany(user => user.UserBids)
            .WithOne(bid => bid.User)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasMany(user => user.UserLots)
            .WithOne(lot => lot.Creator)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasMany(user => user.VictoryLots)
            .WithOne(lot => lot.Winner)
            .OnDelete(DeleteBehavior.NoAction);
    }
}