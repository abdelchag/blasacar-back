using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace blasa.access.management.Core.Application
{
    public static class ServiceExtensions
    {
        public static void AddApplication(this IServiceCollection services)
        {
            
            services.AddMediatR(Assembly.GetExecutingAssembly());
          

        }
    }
}
