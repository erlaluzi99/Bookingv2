using Booking.api.DTOs;
using Booking.Application.User;
using Booking.Infrastructure.Contracts;
using Booking.Infrastructure.Migrations;
using MediatR;
using Microsoft.AspNetCore.Http;
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
        public async Task<IActionResult> Register([FromBody] CreateUserDto createUserDto)
        {
            var command = new RegisterUserCommand { CreateUserDto = createUserDto };
            var result = await _sender.Send(command);
            return Ok(result);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto loginRequest)
        {
            var token = await _authManager.LoginAsync(loginRequest.Email, loginRequest.Password);

            if (token == null)
                return Unauthorized("Invalid email or password");

            return Ok(new { Token = token });
        }
    }
}

