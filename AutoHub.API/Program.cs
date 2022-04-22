using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace AutoHub.API;

public class Program
{
    public static void Main(string[] args)
    {
        CreateHostBuilder(args).ConfigureAppConfiguration((hostContext, builder) =>
        {
            if (hostContext.HostingEnvironment.IsDevelopment())
            {
                builder.AddUserSecrets<Program>();
            }
        }).Build().Run();
    }

    public static IHostBuilder CreateHostBuilder(string[] args) => Host.CreateDefaultBuilder(args).ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });
}