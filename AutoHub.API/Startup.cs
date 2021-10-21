using AutoHub.BLL.Interfaces;
using AutoHub.BLL.Services;
using AutoHub.DAL;
using AutoHub.DAL.Interfaces;
using AutoHub.DAL.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

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
            services.AddControllers();

            services.AddAutoMapper(typeof(Startup).Assembly);

            services.AddRouting();

            services.AddDbContext<AutoHubContext>(
                options => options.UseSqlServer(Configuration.GetConnectionString("LocalConnectionString")));

            services.AddScoped<ICarService, CarService>();
            services.AddScoped<ILotService, LotService>();
            services.AddScoped<IBidService, BidService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ICarBrandService, CarBrandService>();
            services.AddScoped<ICarModelService, CarModelService>();
            services.AddScoped<ICarColorService, CarColorService>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "AutoHub.API", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "AutoHub.API v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}