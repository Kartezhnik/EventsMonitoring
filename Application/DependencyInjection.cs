using MediatR;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Application.Services;
using Domain.Abstractions;
using Domain;

namespace Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            var assembly = typeof(DependencyInjection).Assembly;

            services.AddMediatR(configuration => configuration.RegisterServicesFromAssembly(assembly));
            services.AddValidatorsFromAssembly(assembly);

            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<Context>();

            return services;
        }
    }
}
