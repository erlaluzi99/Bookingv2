using Booking.Application.User;
using Booking.Domain;
using Microsoft.Extensions.Logging;

namespace Booking.Infrastructure.Users
{
    public class UserService : IUserService
    {
        private readonly BookingDbContext _context;
        private readonly ILogger<UserService> _logger;

        public UserService(BookingDbContext context, ILogger<UserService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<Guid> RegisterUserAsync(CreateUserDto dto, CancellationToken cancellationToken)
        {
            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(dto.Password);

            var user = new User(
                Guid.NewGuid(),
                dto.FirstName,
                dto.LastName,
                dto.Email,
                hashedPassword,
                dto.Country
            );

            _context.Users.Add(user);
            await _context.SaveChangesAsync(cancellationToken);
            return user.Id;
        }
    }
}
