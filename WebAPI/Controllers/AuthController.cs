using Application.DTOs.AuthDtos;
using Application.DTOs.UserDtos;
using AutoMapper;
using Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IMapper _mapper;

        public AuthController(IAuthService authService, IMapper mapper)
        {
            _authService = authService;
            _mapper = mapper;
        }

        [HttpPost("login")]
        public async Task<ActionResult<AuthResponse>> Login([FromBody] LoginRequest request)
        {
            try
            {
                var (user, token) = await _authService.AuthenticateAsync(request.Username, request.Password);
                return Ok(new AuthResponse
                {
                    Token = token,
                    User = _mapper.Map<UserDto>(user)
                });
            }
            catch (InvalidOperationException)
            {
                return Unauthorized();
            }
        }
    }
}
