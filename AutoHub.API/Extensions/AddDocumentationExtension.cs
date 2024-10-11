using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;
using System.IO;
using System.Reflection;
using AutoHub.API.Filters;

namespace AutoHub.API.Extensions;

using Swashbuckle.AspNetCore.ReDoc;

public static class AddDocumentationExtension
{
    public static void AddSwagger(this IServiceCollection services)
    {
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "AutoHub.API",
                Version = "v1",
                Contact = new OpenApiContact
                {
                    Name = "Nikita Reshetnik",
                    Email = "reshetnik.nikita@gmail.com"
                }
            });

            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Description = "Please insert JWT token into field (without \"Bearer\")",
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                Scheme = JwtBearerDefaults.AuthenticationScheme,
                BearerFormat = "JWT",
            });
            
            c.OperationFilter<SecurityOperationRequirementsFilter>();

            var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
        });
    }

    public static IApplicationBuilder UseSwaggerDocumentation(this IApplicationBuilder app)
    {
        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.RoutePrefix = string.Empty;
            c.DefaultModelsExpandDepth(-1);
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "AutoHub.API v1");
        });
        return app;
    }

    public static IApplicationBuilder UseRedocDocumentation(this IApplicationBuilder app)
    {
        app.UseReDoc(c =>
        {
            c.SpecUrl("/swagger/v1/swagger.json");
            c.DocumentTitle = "AutoHub.API";
            c.RoutePrefix = "docs";
            c.ConfigObject = new ConfigObject
            {
                HideHostname = true,
                HideDownloadButton = true
            };
        });
        return app;
    }
}
