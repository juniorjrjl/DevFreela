using System.Text;
using DevFreela.API.Mappers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace DevFreela.API;

public static class Extension
{
    
    public static IServiceCollection AddAPI(this IServiceCollection services, WebApplicationBuilder builder)
        =>services.AddControllerMappers()
            .AddSwaggerConfig()
            .AddAuthenticationConfig(builder);

    private static IServiceCollection AddControllerMappers(this IServiceCollection services)
        => services.AddScoped<IProjetcMapper, ProjetcMapper>()
            .AddScoped<IUserMapper, UserMapper>()
            .AddScoped<ISkillMapper, SkillMapper>();

    private static IServiceCollection AddSwaggerConfig(this IServiceCollection services)
        => services.AddSwaggerGen(opt =>{
                opt.SwaggerDoc("v1", new OpenApiInfo { Title= "DevFreela.API", Version = "v1" });
                opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "JWT Authorization header usando o esquema Bearer."
                });

                opt.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        Array.Empty<string>()
                    }
                });
            });

    private static IServiceCollection AddAuthenticationConfig(this IServiceCollection services, WebApplicationBuilder builder)
    {
        services.AddAuthentication(opt =>{
            opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            opt.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        })
            .AddJwtBearer(opt =>
            {
                var jwtKey = builder.Configuration["Jwt:Key"];
                ArgumentNullException.ThrowIfNull(jwtKey);
                opt.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,

                    ValidIssuer = builder.Configuration["Jwt:Issuer"],
                    ValidAudience = builder.Configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
                };
        });
        return services;
    }

}