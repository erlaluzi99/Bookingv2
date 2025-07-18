using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace Booking.Application.User
{
    public class RegisterUserCommand : IRequest<Guid>
    {
        public CreateUserDto CreateUserDto { get; set; }
    }
}
