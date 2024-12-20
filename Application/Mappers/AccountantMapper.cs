using Application.DTOs;
using Domain.Entities;

namespace Application.Mappers
{
    public static class AccountantMapper
    {
        public static AccountantDto ToDto(Accountant accountant)
        {
            return new AccountantDto
            {
                Id = accountant.Id,
                FirstName = accountant.FirstName,
                LastName = accountant.LastName
            };
        }

        public static Accountant ToEntity(AccountantDto dto)
        {
            return new Accountant
            {
                Id = dto.Id,
                FirstName = dto.FirstName,
                LastName = dto.LastName
            };
        }
    }
}