using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Booking.Domain;
using FluentValidation;

namespace Booking.Application.User
{
    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, Guid>
    {
        private readonly IUserRepository _userRepository;
        private readonly RegisterUserCommandValidation _validation;

        public RegisterUserCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _validation = new RegisterUserCommandValidation();
        }

        public async Task<Guid> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            
            var validationResult = _validation.Validate(request);
            if (!validationResult.IsValid)
            {
                await _userRepository.SaveAsync(validationResult);
            }

          
            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(request.CreateUserDto.Password);

            
            var newUser = new Booking.Domain.User(
                Guid.NewGuid(),
                request.CreateUserDto.FirstName,
                request.CreateUserDto.LastName,
                request.CreateUserDto.Email,
                hashedPassword,
                request.CreateUserDto.Country
            );

            // Save to database
            await _userRepository.SaveAsync(newUser, cancellationToken);

            return newUser.Id;
        }

    }
}
