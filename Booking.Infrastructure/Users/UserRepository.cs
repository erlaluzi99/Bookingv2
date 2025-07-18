using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Booking.Application.User;
using Booking.Domain;

namespace Booking.Infrastructure.Users
{
    public class UserRepository : IUserRepository
    {
        private readonly BookingDbContext _context;

            public UserRepository(BookingDbContext DBcontext) {
             _context = DBcontext;
              }
        public Task<Guid> RegisterUserAsync(CreateUserDto createUserDto)
        {
            // Here you would typically interact with a database to save the user.
            // For this example, we will just return a new Guid as if the user was created successfully.
            var newUserId = Guid.NewGuid();
            // Simulate saving the user to the database and returning the new user's ID.
            return Task.FromResult(newUserId);
        }

        public Task SaveAsync(User user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
