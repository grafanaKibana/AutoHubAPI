using AutoHub.API.Models.CarBrandModels;
using AutoHub.API.Models.CarColorModels;
using AutoHub.API.Models.CarModelModels;
using AutoHub.API.Models.CarModels;
using AutoHub.API.Models.UserModels;
using AutoHub.API.Validators;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace AutoHub.API.Extensions
{
    public static class AddValidatorsExtension
    {
        public static void AddValidators(this IServiceCollection services)
        {
            //Registration validators for CarBrand models
            services.AddTransient<IValidator<CarBrandCreateRequestModel>, CarBrandCreateRequestModelValidator>();
            services.AddTransient<IValidator<CarBrandUpdateRequestModel>, CarBrandUpdateRequestModelValidator>();

            //Registration validators for CarModel models
            services.AddTransient<IValidator<CarModelCreateRequestModel>, CarModelCreateRequestModelValidator>();
            services.AddTransient<IValidator<CarModelUpdateRequestModel>, CarModelUpdateRequestModelValidator>();

            //Registration validators for CarColor models
            services.AddTransient<IValidator<CarColorCreateRequestModel>, CarColorCreateRequestModelValidator>();
            services.AddTransient<IValidator<CarColorUpdateRequestModel>, CarColorUpdateRequestModelValidator>();

            //Registration validators for Car models
            services.AddTransient<IValidator<CarCreateRequestModel>, CarCreateRequestModelValidator>();
            services.AddTransient<IValidator<CarUpdateRequestModel>, CarUpdateRequestModelValidator>();

            //Registration validators for User models
            services.AddTransient<IValidator<UserLoginRequestModel>, UserLoginRequestModelValidator>();
            services.AddTransient<IValidator<UserRegisterRequestModel>, UserRegisterRequestModelValidator>();
            services.AddTransient<IValidator<UserUpdateRequestModel>, UserUpdateRequestModelValidator>();
        }
    }
}
