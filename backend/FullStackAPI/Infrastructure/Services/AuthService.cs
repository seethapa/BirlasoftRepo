using ApplicationCore.DTO;
using ApplicationCore.Entities;
using Infrastructure.Persistence;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using BCrypt.Net;

namespace Infrastructure.Services
{
    public class AuthService
    {
        private readonly Container _container;
        private readonly IConfiguration _configuration;

        public AuthService(
            CosmosContainerFactory factory,
            IConfiguration configuration)
        {
            _container = factory.GetContainer("Users");
            _configuration = configuration;
        }

        // REGISTER
        public async Task RegisterAsync(RegisterRequest request)
        {
            var email = request.Email.Trim().ToLowerInvariant();

            var existing = await GetByEmail(email);
            if (existing != null)
                throw new InvalidOperationException("User already exists");

            var user = new Usermodel
            {
                Id = Guid.NewGuid().ToString(),
                Email = email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password),
                FirstName = request.FirstName,
                LastName = request.LastName,
                Role = "User",
                CreatedAt = DateTime.UtcNow
            };

            await _container.CreateItemAsync(
                user,
                new PartitionKey(user.Email)
            );
        }

        // LOGIN
        public async Task<string> LoginAsync(LoginRequest request)
        {
            var email = request.Email.Trim().ToLowerInvariant();
            var user = await GetByEmail(email);

            if (user == null ||
                !BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
                throw new UnauthorizedAccessException("Invalid credentials");

            return GenerateToken(user);
        }

        // GET USER
        public async Task<Usermodel?> GetByEmail(string email)
        {
            var query = new QueryDefinition(
                "SELECT * FROM c WHERE c.email = @email")
                .WithParameter("@email", email);

            var iterator = _container.GetItemQueryIterator<Usermodel>(query);
            var result = await iterator.ReadNextAsync();

            return result.FirstOrDefault();
        }

        // JWT
        public string GenerateToken(Usermodel user)
        {
            var jwtKey = _configuration["Jwt:Key"];
            var issuer = _configuration["Jwt:Issuer"];
            var audience = _configuration["Jwt:Audience"];
            var expiryStr = _configuration["Jwt:ExpiryMinutes"];

            if (string.IsNullOrEmpty(jwtKey))
                throw new Exception("JWT Key is missing");

            if (!int.TryParse(expiryStr, out int expiryMinutes))
                expiryMinutes = 60;

            var claims = new[]
            {
        new Claim(ClaimTypes.Email, user.Email),
        new Claim(ClaimTypes.Role, user.Role),
   new Claim("firstName", user.FirstName ?? ""),
    new Claim("lastName", user.LastName ?? ""),
    new Claim("createdAt", user.CreatedAt.ToString("O")) // ISO 8601 
    };

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(jwtKey)
            );

            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(expiryMinutes),
                signingCredentials: new SigningCredentials(
                    key, SecurityAlgorithms.HmacSha256)
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
