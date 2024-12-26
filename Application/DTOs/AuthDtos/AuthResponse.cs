using Application.DTOs.UserDtos;

namespace Application.DTOs.AuthDtos
{
    public class AuthResponse
    {
        public string? Token { get; set; }
        public UserDto? User { get; set; }
    }
}
