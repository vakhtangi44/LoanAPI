using Application.DTOs;
using AutoMapper;
using Domain.Entities;

namespace Application.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserDto>();
            CreateMap<UserRegistrationDto, User>();
            CreateMap<Loan, LoanDto>();
            CreateMap<CreateLoanDto, Loan>();
            CreateMap<Accountant, AccountantDto>();
        }
    }
}
