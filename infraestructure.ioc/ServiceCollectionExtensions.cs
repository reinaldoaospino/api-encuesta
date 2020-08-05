using System;
using System.Text;
using Application;
using MongoDB.Driver;
using Infraestructure;
using Application.Managers;
using Application.Services;
using Infraestructure.Interfaces;
using Infraestructure.Repositories;
using Domain.Interfaces.Application;
using Microsoft.IdentityModel.Tokens;
using System.Diagnostics.CodeAnalysis;
using Domain.Interfaces.Infraestructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;

namespace infraestructure.ioc
{
    [ExcludeFromCodeCoverage]
    public static class ServiceCollectionExtensions
    {
        public static void AddModules( this IServiceCollection services, IConfiguration configuration)
        {
            ConfigureApplicationModule(services);
            ConfigureJwtToken(services,configuration);
            ConfigureInfraestructureModule(services);
        }

        private static void ConfigureApplicationModule(IServiceCollection services)
        {
            services.AddScoped<IJwtService, JwtService>();
            services.AddScoped<IEmailManager, EmailManager>();
            services.AddScoped<ITokenManager, TokenManager>();
            services.AddScoped<ISurveyManager, SurveyManager>();
            services.AddScoped<IAnswerManager, AnswerManager>();
        }

        public static void ConfigureInfraestructureModule(IServiceCollection services)
        {
            services.AddScoped<IAuthRepository, AuthRepository>();        
            services.AddScoped<IEmailRepository, EmailRepository>();
            services.AddScoped<ISurveyRepository, SurveyRepository>();
            services.AddScoped<IAnswerRepository, AnswerRepository>();
            services.AddScoped<IDescriptionGeneratorService, DescriptionGeneratorService>();
            services.AddScoped<IMongoService>(provider => new MongoService("Surveys", new MongoClient()));
        }

        private static void ConfigureJwtToken(IServiceCollection services, IConfiguration configuration)
        {
            var key = configuration["AppSettings:SecretKey"];
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = "JwtBearer";
                options.DefaultChallengeScheme = "JwtBearer";


            }).AddJwtBearer("JwtBearer", JwtBearerOptions =>
                {
                    JwtBearerOptions.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key)),
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateLifetime = true,
                        ClockSkew = TimeSpan.FromMinutes(5)
                    };
                });
        }
    }
}