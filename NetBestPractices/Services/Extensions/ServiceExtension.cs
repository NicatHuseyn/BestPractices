using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Services.ProductServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Extensions
{
    public static class ServiceExtension
    {
        public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IProductService,ProductService>();

            return services;
        }
    }
}
