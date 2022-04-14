using AutoHub.API.Extensions;
using AutoHub.API.Middlewares;
using AutoHub.BusinessLogic.Configuration;
using AutoHub.DataAccess;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace AutoHub.API;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers()
                .AddFluentValidation(options => options.RegisterValidatorsFromAssembly(typeof(Startup).Assembly));
        services.AddAutoMapper(typeof(Startup).Assembly);
        services.AddRouting();
        services.AddDbContext<AutoHubContext>(options => options.UseSqlServer(Configuration.GetConnectionString("LocalConnectionString")));
        services.AddServices();
        services.AddSwagger();
        services.AddIdentity();
        services.AddAuth(Configuration);
        services.Configure<MailConfiguration>(Configuration.GetSection("MailConfiguration"));
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            app.UseSwaggerDocumentation();
        }

        app.UseMiddleware<ApplicationExceptionMiddleware>();

        app.UseHttpsRedirection();

        app.UseRouting();

        app.UseAuthentication();
        app.UseAuthorization();

        app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
    }
}
