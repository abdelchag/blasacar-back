using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using blasa.access.management.Core.Application.Interfaces;
using blasa.access.management.persistance.Contexts;
using blasa.access.management.persistance.Repositories;
using blasa.tarvel.DependencyInjectionContainer;
using blasa.travel.Core.Application.Commands;
using blasa.travel.Core.Domain.Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace blasa.hosting
{
    public class Startup
    {
        public Startup(IConfiguration _configuration)
        {
            _configuration = configuration;
        }

        public IConfiguration configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddDbContext<ApplicationDbContext>(options =>
            //    options.UseNpgsql(
            //        configuration.GetConnectionString("DefaultHostingConnection"),
            //        b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));

            //            services.AddScoped<IGenericCommandAsync<Travel>, GenericCommandAsync<Travel>>();
            //            services.AddScoped<IGenericRepositoryAsync<Travel>, GenericRepositoryAsync<Travel>>();
            //DependencyInjectionContainer.RegisterServices(services, configuration);
            //DependencyInjectionContainer.Registerconfigurations(services, configuration);
            //AddPersistence(services, configuration);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Hello World!");
                });
            });
        }

        //private static void AddPersistence(IServiceCollection services, IConfiguration configuration)
        //{
        //    DependencyInjectionContainer.RegisterServices(services, configuration);
        //}
    }
}
