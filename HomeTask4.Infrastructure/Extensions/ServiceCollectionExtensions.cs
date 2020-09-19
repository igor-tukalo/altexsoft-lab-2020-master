using HomeTask4.Infrastructure.Data;
using HomeTask4.SharedKernel.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace HomeTask4.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddScoped<IRepository, EfRepository>();

            return services;
        }
    }
}
