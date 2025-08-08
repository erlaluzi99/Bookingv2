using System;
using System.Threading;
using System.Threading.Tasks;

namespace Booking.Application.User
{
    public interface IUserService
    {
        Task<Guid> RegisterUserAsync(CreateUserDto dto, CancellationToken cancellationToken);
    }
}
