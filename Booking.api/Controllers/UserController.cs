using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Booking.api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class UserController (ISender _sender) : ControllerBase
    {
        //USE MEDIATR SENDER
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] CreateUserDto createUserDto)
        {
            var command = new RegisterUserCommand { CreateUserDto = createUserDto };
            var result = await _sender.Send(command);
            return Ok(result);
        }
    }
}
