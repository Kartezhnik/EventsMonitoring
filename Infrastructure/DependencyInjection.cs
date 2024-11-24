using Domain;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrasructure(this IServiceCollection services)
        {
            services.AddScoped<Context>();

            return services;
        }
    }
}
