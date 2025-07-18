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
            await Task.CompletedTask; // Simulating async operation, replace with actual async code if needed

            var user = new Booking.Domain.User 
            {
                FirstName = request.CreateUserDto.FirstName,
                LastName = request.CreateUserDto.LastName,
                Email = request.CreateUserDto.Email,
                Password = request.CreateUserDto.Password,
                CreatedOnUtc = DateTime.UtcNow
            };

            // Save the user to the repository (assuming SaveAsync is a method in IUserRepository)
            await _userRepository.SaveAsync(user, cancellationToken);

            // Return the ID of the created user
            return user.Id;
        }
    }
}
