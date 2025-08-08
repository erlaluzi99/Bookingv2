using Booking.Application.User;
using Booking.Domain;
using Microsoft.Extensions.Logging;
using System.Security.Cryptography;

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
            // Unique email
            if (_context.Users.Any(u => u.Email == dto.Email))
                throw new InvalidOperationException("Email already registered.");

            // Hashing 
            var hashedPassword = PasswordHelper.Hash(dto.Password);

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

            _logger.LogInformation("User registered: {Email}", dto.Email);
            return user.Id;
        }

        private static class PasswordHelper
        { 
            public static string Hash(string password, int iterations = 100_000)
            {
                using var rng = RandomNumberGenerator.Create();
                var salt = new byte[16];
                rng.GetBytes(salt);

                using var pbkdf2 = new Rfc2898DeriveBytes(password, salt, iterations, HashAlgorithmName.SHA256);
                var hash = pbkdf2.GetBytes(32);

                return $"PBKDF2|{iterations}|{Convert.ToBase64String(salt)}|{Convert.ToBase64String(hash)}";
            }
        }
    }
}
