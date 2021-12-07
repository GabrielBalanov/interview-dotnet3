using GroceryStoreAPI.Interfaces.Repositories;
using GroceryStoreAPI.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GroceryStoreAPI.Extensions
{
    public static class RepositoryExtensions
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services, IConfiguration config)
        {
            services
                .AddScoped<ICustomerRepository, CustomerRepository>();

            return services;
        }
    }
}
