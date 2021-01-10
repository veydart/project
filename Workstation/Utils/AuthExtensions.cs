using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace Workstation.Utils
{
    public static class AuthExtensions
    {
        public static IServiceCollection AddCustomJwtAuth(this IServiceCollection services)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = false;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        // укзывает, будет ли валидироваться издатель при валидации токена
                        ValidateIssuer = true,
                        
                        // строка, представляющая издателя
                        ValidIssuer = AuthOptions.ISSUER,

                        // будет ли валидироваться потребитель токена
                        ValidateAudience = true,
                        
                        // установка потребителя токена
                        ValidAudience = AuthOptions.AUDIENCE,
                        
                        // будет ли валидироваться время существования
                        ValidateLifetime = true,

                        // установка ключа безопасности
                        IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(),
                        
                        // валидация ключа безопасности
                        ValidateIssuerSigningKey = true,
                    };
                });

            return services;
        }
    }

    class AuthOptions
    {
        public static string ISSUER = "MyAuthServer";     // издатель токена
        public static string AUDIENCE = "MyAuthClient";   // потребитель токена
        const string KEY = "mysupersecret_secretkey!123"; // ключ для шифрации
        public static int LIFETIME = 1;                   // время жизни токена - 10 часов
        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(KEY));
        }
    }
}
