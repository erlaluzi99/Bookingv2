using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


using System;

namespace Booking.Infrastructure
{
    public static class InfrastructureServicesRegistration
    {
        public static IServiceCollection ConfigureInfrastructureServices(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddDbContext<BookingDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("BookingConnectionString"));
            });

            return services;
        }
    }
}
