using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentValidation;
using System.Threading.Tasks;
using System.Security.Cryptography.X509Certificates;

namespace Booking.Application.User
{
    public class RegisterUserCommandValidation : AbstractValidator<RegisterUserCommand>
    {
        public RegisterUserCommandValidation() { 
            RuleFor(X => X.CreateUserDto. FirstName)
                .NotEmpty().WithMessage("First name is required.")
                .Length(2, 50).WithMessage("First name must be between 2 and 50 characters.");

            RuleFor(x => x.CreateUserDto.LastName)
                .NotEmpty().WithMessage("Last name is required.")
                .Length(2, 50).WithMessage("Last name must be between 2 and 50 characters.");

            RuleFor(x => x.CreateUserDto.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Invalid email format.")
                .Length(5, 100).WithMessage("Email must be between 5 and 100 characters.");

        }
    }
}
