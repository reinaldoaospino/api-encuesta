using Application;
using Domain.Interfaces.Application;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using infraestructure.Interfaces;
using System;
using System.Text;
using MongoDB.Driver;
using Domain.Interfaces.Infraestructure;
using infraestructure.Repositories;

namespace infraestructure.ioc
{
    public static class ServiceCollectionExtensions
    {
        public static void AddModules( this IServiceCollection services)
        {
            ConfigureApplicationModule(services);
            ConfigureJwtToken(services);
            ConfigureInfraestructureModule(services);
        }

        private static void ConfigureApplicationModule(IServiceCollection services)
        {
            services.AddScoped<ITokenManager, TokenManager>();
            services.AddScoped<IJwtService, JwtService>();
            services.AddScoped<ISurveyManager, SurveyManager>();
        }

        public static void ConfigureInfraestructureModule(IServiceCollection services)
        {
            services.AddScoped<ISurveyRepository, SurveyRepository>();
            services.AddScoped<IMongoService>(provider => new MongoService("Surveys", new MongoClient()));
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
