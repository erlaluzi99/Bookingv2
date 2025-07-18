using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Application.User
{
    public interface IUserRepository
    {
        //create user

        Task<Guid> RegisterUserAsync(CreateUserDto createUserDto);
        Task SaveAsync(Domain.User user, CancellationToken cancellationToken);
    }
}
