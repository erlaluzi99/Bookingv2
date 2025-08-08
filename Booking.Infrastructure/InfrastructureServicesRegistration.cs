using Booking.Application.User;
using Booking.Infrastructure.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


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

            services.AddScoped<IUserRepository, UserRepository>();

            services.AddScoped<IUserService, UserService>();


            return services;
        }
    }
}


