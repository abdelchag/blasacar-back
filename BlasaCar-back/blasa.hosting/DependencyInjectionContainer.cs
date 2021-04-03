using blasa.access.management.persistance.Contexts;
using blasa.travel.Core.Application.Commands;
using blasa.travel.Core.Application.OutputPort;
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
        public static string connectionString = "DefaultConnection";
        public static void AddPersistenceAuth(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AuthentificationDbContext>(options =>
                options.UseNpgsql(
                    configuration.GetConnectionString(connectionString),
                    b => b.MigrationsAssembly(typeof(AuthentificationDbContext).Assembly.FullName)));

            services.AddScoped<access.management.Core.Application.Interfaces.IAuthentificationDbContext>(provider => provider.GetService<AuthentificationDbContext>());
        }


        public static void RegisterServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<TravelDbContext>(options =>
                options.UseNpgsql(
                    configuration.GetConnectionString(connectionString),
                    b => b.MigrationsAssembly(typeof(TravelDbContext).Assembly.FullName)));

            services.AddScoped<IGenericCommandAsync<Travel>, GenericCommandAsync<Travel>>();
            services.AddScoped<IGenericRepositoryAsync<Travel>, GenericRepositoryAsync<Travel>>();
              services.AddScoped<IUserRepositoryAsync, UserRepositoryAsync>();
            services.AddScoped<IUserCommandAsync, UserCommandAsync>();
        }
    }
}
