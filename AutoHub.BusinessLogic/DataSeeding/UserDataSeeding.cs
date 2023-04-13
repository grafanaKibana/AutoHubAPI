using System;
using System.Threading.Tasks;
using AutoHub.Domain.Entities.Identity;
using AutoHub.Domain.Enums;
using Microsoft.AspNetCore.Identity;

namespace AutoHub.BusinessLogic.DataSeeding;

public static class UserDataSeeding
{
    public static async Task AddDefaultAdmin(UserManager<ApplicationUser> userManager)
    {
        var adminUsername = "admin";
        var adminEmail = "reshetnik.nikita@gmail.com";
        var adminPassword = "adminPasSw0Rd!@#";
        if (await userManager.FindByEmailAsync(adminEmail) is null)
        {
            var admin = new ApplicationUser
            {
                UserName = adminUsername,
                NormalizedUserName = adminUsername.ToUpperInvariant(),
                Email = adminEmail,
                NormalizedEmail = adminEmail.ToUpperInvariant(),
                EmailConfirmed = true,
                FirstName = "Nikita",
                LastName = "Reshetnik",
                RegistrationTime = DateTime.UtcNow,
            };

            var result = await userManager.CreateAsync(admin, adminPassword);

            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(admin, UserRoleEnum.Administrator.ToString());
            }
        }
    }
}