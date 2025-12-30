using ApplicationCore.Entities;
using Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CosmoAuthController : ControllerBase
    {
        private readonly UserService _users;
        private readonly JwtService _jwt;

        public CosmoAuthController(UserService users, JwtService jwt)
        {
            _users = users;
            _jwt = jwt;
        }

        [HttpGet]
        public IActionResult Root()
        {
            return Ok("Cosmo Auth API is running");
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequest req)
        {
            var existing = await _users.GetByEmail(req.Email);
            if (existing != null)
                return BadRequest("User already exists");

            var user = new usermodel
            {
                Id = Guid.NewGuid().ToString(),
                Email = req.Email,   // 👈 MUST NOT BE NULL
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(req.Password),
                Role = "User",
                CreatedAt = DateTime.UtcNow
            };
            Console.WriteLine($"Creating user with email: {user.Email}");
            await _users.Create(user);
            return Ok("Registered");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest req)
        {
            var user = await _users.GetByEmail(req.Email);
            if (user == null ||
                !BCrypt.Net.BCrypt.Verify(req.Password, user.PasswordHash))
                return Unauthorized("Invalid credentials");

            var token = _jwt.GenerateToken(user);
            return Ok(new { token });
        }

        // 🔒 protected endpoint
        [Authorize]
        [HttpGet("me")]
        public IActionResult Me()
        {
            return Ok(new
            {
                Email = User.FindFirstValue(ClaimTypes.Email),
                Role = User.FindFirstValue(ClaimTypes.Role)
            });
        }
    }
}
