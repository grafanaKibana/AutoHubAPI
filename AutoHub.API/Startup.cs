using AutoHub.API.Extensions;
using AutoHub.API.Filters;
using AutoHub.API.Middlewares;
using AutoHub.DAL;
using AutoHub.DAL.Entities.Identity;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace AutoHub.API
{
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
            services.AddControllers(options =>
                {
                    options.Filters.Add(new ValidModelAttribute());
                })
                .AddFluentValidation(options =>
                {
                    options.RegisterValidatorsFromAssemblyContaining<Startup>();
                });
            services.AddValidators();
            services.AddAutoMapper(typeof(Startup).Assembly);
            services.AddRouting();
            services.AddSingleton(_ => Configuration);
            services.AddDbContext<AutoHubContext>(
                options => options.UseSqlServer(Configuration.GetConnectionString("LocalConnectionString")));
            services.AddServices();
            services.AddAuth(Configuration);
            services.AddSwagger();
            services.AddIdentity<AppUser, AppRole>()
                .AddEntityFrameworkStores<AutoHubContext>()
                .AddDefaultTokenProviders();
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
}