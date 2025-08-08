using Booking.Application.User;
using Booking.Infrastructure.AuthService;
using Booking.Infrastructure.Contracts;
using Booking.Infrastructure.Users;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Booking.Infrastructure
{
    public static class InfrastructureServicesRegistration
    {
        public static IServiceCollection ConfigureInfrastructureServices(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            //handler
            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(Assembly.Load("Booking.Application"));
            });

          
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IAuthManager, AuthManager>();

            return services;
        }
    }
}

