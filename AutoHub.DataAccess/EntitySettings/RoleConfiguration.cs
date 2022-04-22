using AutoHub.Domain.Entities.Identity;
using AutoHub.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Linq;

namespace AutoHub.DataAccess.EntitySettings;

public class RoleConfiguration : IEntityTypeConfiguration<ApplicationRole>
{
    public void Configure(EntityTypeBuilder<ApplicationRole> builder)
    {
        builder.HasData(
            Enum.GetValues(typeof(UserRoleEnum))
                .Cast<UserRoleEnum>()
                .Select((role, index) => new ApplicationRole
                {
                    Id = index + 1,
                    Name = role.ToString(),
                    NormalizedName = role.ToString().Normalize().ToUpperInvariant(),
                    ConcurrencyStamp = Guid.NewGuid().ToString(),
                }));
    }
}