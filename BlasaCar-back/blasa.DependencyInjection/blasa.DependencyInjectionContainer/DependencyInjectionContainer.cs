using blasa.travel.persistance.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace blasa.DependencyInjectionContainer
{
    //public static class DependencyInjectionContainer
    //{
    //    //public static void RegisterServices(IServiceCollection services, IConfiguration configuration)
    //    //{
    //    //    services.AddDbContext<ApplicationDbContext>(options =>
    //    //        options.UseNpgsql(
    //    //            configuration.GetConnectionString("DefaultConnection"),
    //    //            b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));

    //    //    services.AddScoped<IGenericCommandAsync<Travel>, GenericCommandAsync<Travel>>();
    //    //    services.AddScoped<IGenericRepositoryAsync<Travel>, GenericRepositoryAsync<Travel>>();
    //    //}
    //}
}
