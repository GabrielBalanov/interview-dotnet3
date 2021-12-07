using GroceryStoreAPI.BusinessLogic;
using GroceryStoreAPI.Interfaces.BusinessLogic;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GroceryStoreAPI.Extensions
{
    public static class BusinessLogicExtensions
    {
        public static IServiceCollection AddBusinessLogic(this IServiceCollection services)
        {
            services.AddScoped<ICustomerLogic, CustomerLogic>();
            return services;
        }
    }
}
