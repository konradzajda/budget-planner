using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace Tivix.BudgetPlanner.Api.Extensions;

public static class SwaggerServiceCollectionExtensions
{
    public static void AddSwagger(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSwaggerGen(options =>
        {
            var scheme = new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer",
                Name = "Authorization",
            };

            options.AddSecurityDefinition("OAuth", scheme);

            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                { 
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Id = "OAuth",
                            Type = ReferenceType.SecurityScheme,
                        }
                    }, 
                    Array.Empty<string>()
                }
            });
        });
    }
}