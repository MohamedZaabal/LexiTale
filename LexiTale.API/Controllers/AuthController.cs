using LexiTale.Application.Features.Authentication.DTOs;
using LexiTale.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LexiTale.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    /* 
     
     {
  "name": "Mohamed",
  "email": "mohamed@test.com",
  "password": "123456",
  "currentLevel": 2
     }
     
     */
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register( RegisterRequest request)
        {
            await _authService.RegisterAsync(request);
            return Ok(new { message = "User registered successfully" });
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            var result = await _authService.LoginAsync(request);
            return Ok(new { message = result });
        }
    }
}
