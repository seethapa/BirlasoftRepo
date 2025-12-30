using ApplicationCore.DTO;
using ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.Interfaces
{
    public interface IAuthService
    {
        Task<AuthResponse> RegisterAsync(RegisterRequest request);
        Task<AuthResponse> LoginAsync(LoginRequest request);
        // Task<bool> IsEmailUniqueAsync(string email);
        Task<Usermodel?> GetUserByEmailAsync(string email);
        
    }
}
