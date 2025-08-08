﻿namespace Booking.Application.User
{
    public record CreateUserDto
    {
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }
        public required string Country { get; set; }
    }
}