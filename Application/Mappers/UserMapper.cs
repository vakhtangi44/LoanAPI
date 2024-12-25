using Application.DTOs;
using Domain.Entities;
namespace Application.Mappers
{
    public static class UserMapper
    {
        public static UserDto ToDto(User user) =>
            new UserDto
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

        public static User ToEntity(UserDto dto) =>
            new User(dto.FirstName, dto.LastName, dto.UserName, dto.Email, dto.Age, dto.MonthlyIncome, string.Empty);
    }
}
