using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;

namespace AutoHub.API.Extensions;

public static class AddFluentValidationExtension
{
    extension(IServiceCollection services)
    {
        public void AddFluentValidation()
        {
            services.AddFluentValidationAutoValidation();
            services.AddFluentValidationClientsideAdapters();
            services.AddValidatorsFromAssembly(typeof(Program).Assembly);
        }
    }
}