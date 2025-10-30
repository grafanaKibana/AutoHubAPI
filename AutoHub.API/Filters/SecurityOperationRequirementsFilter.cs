using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace AutoHub.API.Filters;

using JetBrains.Annotations;

[UsedImplicitly]
public class SecurityOperationRequirementsFilter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        var methodCustomAttributes = context.MethodInfo.GetCustomAttributes(true).ToList();
        var typeCustomAttributes = context.MethodInfo.DeclaringType?.GetCustomAttributes(true).ToList() ?? [];

        var requiresAuthentication = methodCustomAttributes.Exists(x => x is AuthorizeAttribute) ||
                                     typeCustomAttributes.Exists(x => x is AuthorizeAttribute);

        var allowAnonymous = methodCustomAttributes.Exists(x => x is AllowAnonymousAttribute) ||
                             typeCustomAttributes.Exists(x => x is AllowAnonymousAttribute);

        if (requiresAuthentication && !allowAnonymous)
        {
            operation.Security = new List<OpenApiSecurityRequirement>
            {
                new()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Id = JwtBearerDefaults.AuthenticationScheme,
                                Type = ReferenceType.SecurityScheme
                            }
                        },
                        new List<string>()
                    }
                }
            };
        }
    }
}