using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Booking.Application.User;
using FluentValidation;
using FluentValidation.Results;

namespace Booking.Application.User
{
    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, Guid>
    {
        private readonly IUserService _userService;
        private readonly RegisterUserCommandValidation _validation;

        public RegisterUserCommandHandler(IUserService userService)
        {
            _userService = userService;
            _validation = new RegisterUserCommandValidation();
        }

        public async Task<Guid> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            ValidationResult validationResult = _validation.Validate(request);
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            return await _userService.RegisterUserAsync(request.CreateUserDto, cancellationToken);
        }
    }
}
