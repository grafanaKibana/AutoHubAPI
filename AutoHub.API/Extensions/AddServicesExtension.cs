using AutoHub.BusinessLogic.Interfaces;
using AutoHub.BusinessLogic.Services;
using Microsoft.Extensions.DependencyInjection;

namespace AutoHub.API.Extensions;

public static class AddServicesExtension
{
    public static void AddServices(this IServiceCollection services)
    {
        services.AddScoped<ICarService, CarService>();
        services.AddScoped<ILotService, LotService>();
        services.AddScoped<IBidService, BidService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<ICarBrandService, CarBrandService>();
        services.AddScoped<ICarModelService, CarModelService>();
        services.AddScoped<ICarColorService, CarColorService>();
        services.AddScoped<IAuthenticationService, AuthenticationService>();
        services.AddScoped<IEmailService, EmailService>();
    }
}