using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Workstation.Utils
{
    public static class SwaggerExtensions
    {
        public static void AddDefaultOptions(this SwaggerGenOptions options)
        {
            options.DescribeAllEnumsAsStrings();

            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Description = "Авторизация с помощью JWT токена.  <b>Пример: Bearer -token-</b>",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey
            });

            var security = new OpenApiSecurityRequirement()
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        },
                        Scheme = "oauth2",
                        Name = "Bearer",
                        In = ParameterLocation.Header,

                    },
                    new List<string>()
                }
            };

            options.AddSecurityRequirement(security);
        }
    }
}