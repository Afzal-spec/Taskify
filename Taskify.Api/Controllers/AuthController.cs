using Microsoft.AspNetCore.Mvc;
using Taskify.Api.DTOs;
using Taskify.Api.DTOs.Auth;
using Taskify.Api.Services;

namespace Taskify.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AuthService authService;

        public AuthController(AuthService authService)
        {
            this.authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto dto)
        {
            var user = await authService.RegisterAsync(dto);
            if (user == null)
                return BadRequest("User already exists!");

            return Ok(new { message = "Registered successfully", user });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto dto)
        {
            var token = await authService.LoginAsync(dto);
            if (token == null)
                return Unauthorized("Invalid credentials");

            return Ok(new { token });
        }
    }
}
