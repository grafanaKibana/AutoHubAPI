using AutoHub.BLL.Interfaces;
using AutoHub.BLL.Services;
using AutoHub.DAL.Interfaces;
using AutoHub.DAL.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace AutoHub.API.Extensions
{
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
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }
    }
}