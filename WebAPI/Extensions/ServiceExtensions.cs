using Application.Interfaces;
using Application.Services;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Infrastructure.Authentication;
using Infrastructure.Persistence.Repositories;

namespace WebAPI.Extensions
{
    public static class ServiceExtensions
    {
        public static void AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ILoanService, LoanService>();
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ILoanRepository, LoanRepository>();
            services.AddScoped<IAccountantRepository, AccountantRepository>();
        }
    }
}
