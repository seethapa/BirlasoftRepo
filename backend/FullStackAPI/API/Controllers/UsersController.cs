using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Cosmos;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UsersController : ControllerBase
    {


        //// 🔒 protected endpoint
        //[Authorize]
        //[HttpGet("me")]
        //public IActionResult Me()
        //{
        //    return Ok(new
        //    {
        //        Email = User.FindFirstValue(ClaimTypes.Email),
        //        Role = User.FindFirstValue(ClaimTypes.Role),
        //        FirstName = User.FindFirst("firstName")?.Value,
        //        LastName = User.FindFirst("lastName")?.Value,
        //        CreatedAt = User.FindFirst("createdAt")?.Value
        //    });
        //}
    }
}
