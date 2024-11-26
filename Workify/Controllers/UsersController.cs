using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Workify.DTOs;
using Workify.Models;
using Workify.Services;

namespace Workify.Controllers
    {

        [ApiController]
        [Route("api/[controller]")]
        public class UsersController : ControllerBase
        {
            private readonly IUserService _userService;

            public UsersController(IUserService userService)
            {
                _userService = userService;
            }

            [HttpPost("register")]
            public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
            {
                try
                {
                    var message = await _userService.RegisterUserAsync(registerDto);
                    return Ok(new { Message = message });
                }
                catch (ArgumentException ex)
                {
                    return BadRequest(new { Error = ex.Message });
                }
            }

            [HttpPost("login")]
            public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
            {
                try
                {
                    var token = await _userService.AuthenticateUserAsync(loginDto);
                    return Ok(new { Token = token });
                }
                catch (UnauthorizedAccessException ex)
                {
                    return Unauthorized(new { Error = ex.Message });
                }
            }
        }

    }
