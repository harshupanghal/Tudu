// Infrastructure/DependencyInjection.cs
using Application.Interfaces;
using Infrastructure.Security;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
    {
    public static class DependencyInjection
        {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
            {
            services.AddScoped<IPasswordHasher, BCryptPasswordHasher>();
            return services;
            }
        }
    }
