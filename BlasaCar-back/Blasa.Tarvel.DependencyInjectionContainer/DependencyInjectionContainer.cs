using blasa.travel.Core.Application.Commands;
using blasa.travel.Core.Application.Repositories;
using blasa.travel.Core.Domain.Entities;
using blasa.travel.persistance.Contexts;
using blasa.travel.persistance.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace blasa.tarvel.DependencyInjectionContainer
{
    public static class DependencyInjectionContainer
    {
        //public static void RegisterServices(IServiceCollection services, IConfiguration configuration)
        //{
        //    services.AddDbContext<ApplicationDbContext>(options =>
        //        options.UseNpgsql(
        //            configuration.GetConnectionString("DefaultConnection"),
        //            b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));

        //    services.AddScoped<IGenericCommandAsync<Travel>, GenericCommandAsync<Travel>>();
        //    services.AddScoped<IGenericRepositoryAsync<Travel>, GenericRepositoryAsync<Travel>>();
        //}
    }
}
