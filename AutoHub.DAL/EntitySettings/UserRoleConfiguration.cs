using AutoHub.DAL.Entities;
using AutoHub.DAL.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Linq;

namespace AutoHub.DAL.EntitySettings
{
    public class UserRoleConfiguration
    {
        public UserRoleConfiguration(EntityTypeBuilder<UserRole> entity)
        {
            entity.ToTable("UserRole").HasKey(role => role.UserRoleId);

            entity.HasData(
                Enum.GetValues(typeof(UserRoleEnum))
                    .Cast<UserRoleEnum>()
                    .Select(u => new UserRole
                    {
                        UserRoleId = u,
                        UserRoleName = u.ToString()
                    }));

            entity.HasMany(role => role.Users)
                .WithOne(user => user.UserRole)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}