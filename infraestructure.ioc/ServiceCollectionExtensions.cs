using Application;
using Domain.Abstations;
using Domain.Abstations.Application;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;

namespace infraestructure.ioc
{
    public static class ServiceCollectionExtensions
    {
        public static void AddModules( this IServiceCollection services)
        {
            ConfigureApplicationModule(services);
            ConfigureJwtToken(services);
        }

        private static void ConfigureApplicationModule(IServiceCollection services)
        {
            services.AddScoped<ITokenManager, TokenManager>();
            services.AddScoped<IJwtService, JwtService>();
        }

        private static void ConfigureJwtToken(IServiceCollection services)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = "JwtBearer";
                options.DefaultChallengeScheme = "JwtBearer";


            }).AddJwtBearer("JwtBearer", JwtBearerOptions =>
                {
                    JwtBearerOptions.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes("this is my test key")),
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateLifetime = true,
                        ClockSkew = TimeSpan.FromMinutes(5)
                    };
                });
        }
    }
}
