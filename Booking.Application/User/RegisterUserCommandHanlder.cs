using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace Booking.Application.User
{
    public class RegisterUserCommandHanlder : IRequestHandler<RegisterUserCommand, Guid>
    {
        private readonly IUserRepository _userRepository;
        private readonly RegisterUserCommandValidation _validation;

        public RegisterUserCommandHanlder(IUserRepository userRepository)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _validation= new RegisterUserCommandValidation();
        }

        public async Task<Guid> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
           var validationResult=_validation.Validate(request);

            var pw= BCrypt.Net.BCrypt.HashPassword(request.CreateUserDto.Password);
            if (!validationResult.IsValid)
            {
                throw new ArgumentException("Invalid user data", nameof(request.CreateUserDto));
            }
            

            //await _userRepository.SaveAsync(user, cancellationToken);
            //return user.Id;
        }
    }
}
