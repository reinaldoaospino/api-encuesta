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
using Domain.Interfaces.Infraestructure;
using Microsoft.Extensions.DependencyInjection;

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
            services.AddScoped<IJwtService, JwtService>();
            services.AddScoped<ITokenManager, TokenManager>();
            services.AddScoped<ISurveyManager, SurveyManager>();
            services.AddScoped<IAnswerManager, AnswerManager>();
        }

        public static void ConfigureInfraestructureModule(IServiceCollection services)
        {
            services.AddScoped<ISurveyRepository, SurveyRepository>();
            services.AddScoped<IAnswerRepository, AnswerRepository>();
            services.AddScoped<ISurveyVerificationService, SurveyVerificationService>();
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