using Booking.api.DTOs;
using Booking.Application.User;
using Booking.Infrastructure.Contracts;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Booking.api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ISender _sender;
        private readonly IAuthManager _authManager;

        public UserController(ISender sender, IAuthManager authManager)
        {
            _sender = sender;
            _authManager = authManager;
        }

        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] CreateUserDto dto, CancellationToken ct)
        {
            var id = await _sender.Send(new RegisterUserCommand { CreateUserDto = dto }, ct);
            return CreatedAtAction(nameof(Register), new { id }, new { id });
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto login)
        {
            if (string.IsNullOrWhiteSpace(login.Email) || string.IsNullOrWhiteSpace(login.Password))
                return BadRequest("Email and password are required.");

            var token = await _authManager.LoginAsync(login.Email, login.Password);
            if (token == null) return Unauthorized("Invalid email or password");
            return Ok(new { token });
        }
    }
}


