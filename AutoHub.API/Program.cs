using System;
using System.Threading.Tasks;
using AutoHub.BusinessLogic.DataSeeding;
using AutoHub.Domain.Entities.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace AutoHub.API;

public class Program
{
    public static async Task Main(string[] args)
    {
        var host = CreateHostBuilder(args).ConfigureAppConfiguration((hostContext, builder) =>
        {
            if (hostContext.HostingEnvironment.IsDevelopment())
            {
                builder.AddUserSecrets<Program>();
            }
        }).Build();
        
        using (var scope = host.Services.CreateScope())
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
        
        await host.RunAsync();
    }

    public static IHostBuilder CreateHostBuilder(string[] args) => Host.CreateDefaultBuilder(args).ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });
}
