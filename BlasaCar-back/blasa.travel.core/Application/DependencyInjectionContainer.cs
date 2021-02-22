using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using blasa.travel.Core.Application.Commands;
using blasa.travel.Core.Domain.Entities;

namespace blasa.travel.Core.Application
{
    public static class DependencyInjectionContainer
    {
        public static void AddCoreApplication(this IServiceCollection services, IConfiguration configuration)
        {
            
            //services.AddScoped< IGenericCommandAsync<Travel>>(provider => provider.GetService<genericRepositoryAsync>());

       
        }
    }
}
