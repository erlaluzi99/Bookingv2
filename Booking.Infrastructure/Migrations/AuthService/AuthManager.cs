using Booking.Domain;
using Booking.Infrastructure.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Booking.Infrastructure.AuthService
{
    public class AuthManager : IAuthManager
    {
        private readonly BookingDbContext _context;
        private readonly IConfiguration _config;

        public AuthManager(BookingDbContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        public async Task<string?> LoginAsync(string email, string password)
        {
            var user = await _context.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Email == email);
            if (user is null) return null;

            if (!VerifyPassword(password, user.Password))
                return null;

            //check if user is an owner
            var isOwner = await _context.Owners.AsNoTracking().AnyAsync(o => o.UserId == user.Id);

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(ClaimTypes.Name, $"{user.FirstName} {user.LastName}"),
                new Claim("role", isOwner ? "Owner" : "User")
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(2),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        // format match
        private static bool VerifyPassword(string password, string stored)
        {
            try
            {
                var parts = stored.Split('|');
                if (parts.Length != 4 || parts[0] != "PBKDF2") return false;

                var iterations = int.Parse(parts[1]);
                var salt = Convert.FromBase64String(parts[2]);
                var expectedHash = Convert.FromBase64String(parts[3]);

                using var pbkdf2 = new Rfc2898DeriveBytes(password, salt, iterations, HashAlgorithmName.SHA256);
                var actual = pbkdf2.GetBytes(32);
                return CryptographicOperations.FixedTimeEquals(actual, expectedHash);
            }
            catch
            {
                return false;
            }
        }
    }
}

