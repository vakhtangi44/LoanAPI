using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace WebAPI.Extensions
{
    public static class AuthenticationExtensions
    {
        public static void AddJwtAuthentication(this IServiceCollection services, string key)
        {
            var keyBytes = Encoding.UTF8.GetBytes(key);
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(keyBytes)
                    };
                });
        }
    }
}
