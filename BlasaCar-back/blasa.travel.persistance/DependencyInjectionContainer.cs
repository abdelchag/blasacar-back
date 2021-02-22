using blasa.travel.Core.Application.Repositories;
using blasa.travel.Core.Domain.Entities;
using blasa.travel.persistance.Contexts;
using blasa.travel.persistance.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace blasa.travel.persistance
{
    public static class DependencyInjectionContainer
    {
        public static void AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseNpgsql(
                    configuration.GetConnectionString("DefaultConnection"),
                    b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));
            services.AddScoped<IApplicationDbContext>(provider => provider.GetService<ApplicationDbContext>());
            services.AddScoped<IGenericRepositoryAsync<Travel>>(provider => provider.GetService<TravelRepositoryAsync>());

       
        }
    }
}
