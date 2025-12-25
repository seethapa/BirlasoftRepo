using ApplicationCore.Entities;
using Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using ApplicationCore.DTO;
using RegisterRequest = ApplicationCore.DTO.RegisterRequest;
using LoginRequest = ApplicationCore.DTO.LoginRequest;


namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _auth;

        public AuthController(AuthService auth)
        {
            _auth = auth;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequest request)
        {
            await _auth.RegisterAsync(request);

            return StatusCode(
                StatusCodes.Status201Created,
                new { message = "User registered successfully" }
            );
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            var token = await _auth.LoginAsync(request);
            return Ok(new { token });
        }

        // 🔒 protected endpoint
        [Authorize]
        [HttpGet("me")]
        public async Task<IActionResult> Me()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);

            var user = await _auth.GetByEmail(email!);

            return Ok(new
            {
                Email = user!.Email,
                Role = user.Role,
                FirstName = user.FirstName,
                LastName = user.LastName,
                CreatedAt = user.CreatedAt
            });
        }
    }

}

