using MediatR;

namespace Booking.Application.User
{
    public class RegisterUserCommand : IRequest<Guid>
    {
        public CreateUserDto CreateUserDto { get; set; } = default!; 
    }
}

