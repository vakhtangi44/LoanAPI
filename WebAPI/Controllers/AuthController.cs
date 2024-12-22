using Application.DTOs.Auth;
using Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate(LoginRequest loginRequest)
        {
            var token = await _authService.AuthenticateAsync(loginRequest.UserName, loginRequest.Password);
            return Ok(new { Token = token });
        }

        [HttpPost("refresh")]
        public async Task<IActionResult> RefreshToken(RefreshTokenRequest refreshTokenRequest)
        {
            var token = await _authService.RefreshTokenAsync(refreshTokenRequest.RefreshToken);
            return Ok(new { Token = token });
        }
    }
}
