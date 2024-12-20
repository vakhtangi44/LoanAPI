using Application.DTOs;
using Domain.Entities;

namespace Application.Mappers
{
    public static class UserMapper
    {
        public static UserDto ToDto(User user)
        {
            return new UserDto
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                UserName = user.UserName,
                Email = user.Email,
                Age = user.Age,
                MonthlyIncome = user.MonthlyIncome,
                IsBlocked = user.IsBlocked
            };
        }

        public static User ToEntity(UserDto dto)
        {
            return new User
            {
                Id = dto.Id,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                UserName = dto.UserName,
                Email = dto.Email,
                Age = dto.Age,
                MonthlyIncome = dto.MonthlyIncome,
                IsBlocked = dto.IsBlocked
            };
        }
    }
}
