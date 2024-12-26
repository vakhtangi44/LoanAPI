using Application.DTOs.AuthDtos;
using Application.DTOs.UserDtos;
using AutoMapper;
using Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController(IAuthService authService, IMapper mapper) : ControllerBase
    {
        [HttpPost("login")]
        public async Task<ActionResult<AuthResponse>> Login([FromBody] LoginRequest request)
        {
            try
            {
                var (user, token) = await authService.AuthenticateAsync(request.Username!, request.Password!);
                return Ok(new AuthResponse
                {
                    Token = token,
                    User = mapper.Map<UserDto>(user)
                });
            }
            catch (InvalidOperationException)
            {
                return Unauthorized();
            }
        }
    }
}
