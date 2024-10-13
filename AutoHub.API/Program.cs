using System;
using AutoHub.API.Extensions;
using AutoHub.API.Middlewares;
using AutoHub.BusinessLogic.Configuration;
using AutoHub.BusinessLogic.DataSeeding;
using AutoHub.DataAccess;
using AutoHub.Domain.Entities.Identity;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AutoHubContext>(opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("AzureSqlServerConnectionString")));
builder.Configuration.AddEnvironmentVariables().AddUserSecrets<Program>();
builder.Services.AddControllers();
builder.Services.AddServices();
builder.Services.AddFluentValidation();
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddRouting();
builder.Services.AddSwagger();
builder.Services.AddIdentity();
builder.Services.AddAuth(builder.Configuration);
builder.Services.AddQuartz();
builder.Services.Configure<MailConfiguration>(builder.Configuration.GetSection("MailConfiguration"));

var app = builder.Build();

if (builder.Environment.IsDevelopment())
{
    using (var scope = app.Services.CreateScope())
    {
        var services = scope.ServiceProvider;
        
        try
        {
            var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
            await UserDataSeeding.AddDefaultAdmin(userManager);
        }
        catch (Exception ex)
        {
            var logger = services.GetRequiredService<ILogger<Program>>();
            logger.LogError(ex, "An error occurred while seeding the database.");
        }
    }
    
    app.UseDeveloperExceptionPage();
    app.UseHttpsRedirection();
}

app.UseSwaggerDocumentation();
app.UseRedocDocumentation();
app.UseMiddleware<ApplicationExceptionMiddleware>();
app.UseMiddleware<RedirectMiddleware>();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

await app.RunAsync();

