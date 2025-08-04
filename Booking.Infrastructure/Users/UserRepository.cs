using Booking.Application.User;
using Booking.Domain;
using Booking.Infrastructure;
using FluentValidation.Validators;
using FluentValidation.Results;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class UserRepository : IUserRepository
{
    private readonly BookingDbContext _context;

    public UserRepository(BookingDbContext dbContext)
    {
        _context = dbContext;
    }

    public async Task SaveAsync(User user, CancellationToken cancellationToken)
    {
        await _context.Users.AddAsync(user, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public Task<Guid> RegisterUserAsync(CreateUserDto createUserDto)
    {
        var newUserId = Guid.NewGuid(); // Placeholder
        return Task.FromResult(newUserId);
    }

    public Task SaveAsync(ValidationResult validationResult)
    {
        if (validationResult.IsValid)
            return Task.CompletedTask;

        
        foreach (var error in validationResult.Errors)
        {
            Console.WriteLine($"Validation Error: {error.PropertyName} - {error.ErrorMessage}");
        }

        throw new ValidationException(validationResult.Errors);
    }
}

