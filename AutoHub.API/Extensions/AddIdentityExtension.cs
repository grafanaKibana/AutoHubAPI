using AutoHub.API.Constants;
using AutoHub.DataAccess;
using AutoHub.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace AutoHub.API.Extensions;

public static class AddIdentityExtension
{
    public static void AddIdentity(this IServiceCollection services)
    {
        services.AddIdentity<ApplicationUser, ApplicationRole>()
            .AddRoleManager<RoleManager<ApplicationRole>>()
            .AddSignInManager<SignInManager<ApplicationUser>>()
            .AddEntityFrameworkStores<AutoHubContext>()
            .AddDefaultTokenProviders();

        services.Configure<IdentityOptions>(options =>
        {
            options.Password.RequireDigit = false;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequiredLength = UserRestrictions.MinPasswordLenght;
            options.User.RequireUniqueEmail = true;
        });
    }
}
