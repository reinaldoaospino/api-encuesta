﻿using AutoMapper;
using Domain.Entities;
using api_encuesta.Models;
using Infraestructure.Entities;
using System.Diagnostics.CodeAnalysis;

namespace api_encuesta.Mappers
{
    [ExcludeFromCodeCoverage]
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            CreateMap<EmailModel, Email>().ReverseMap();
            CreateMap<EmailEntity, Email>().ReverseMap();
            CreateMap<SurveyModel, Survey>().ReverseMap();
            CreateMap<AnswerModel, Answer>().ReverseMap();
            CreateMap<SurveyEntity, Survey>().ReverseMap();
            CreateMap<AnswerEntity, Answer>().ReverseMap();
            CreateMap<AuthUserModel, AuthUser>().ReverseMap();
            CreateMap<AuthUserEntity, AuthUser>().ReverseMap();
            CreateMap<TokenRequestModel, TokenRequest>().ReverseMap();
            CreateMap<SurveyOptionModel, SurveyOption>().ReverseMap();
            CreateMap<SurveyOptionEntity, SurveyOption>().ReverseMap();
            CreateMap<TokenResponseModel, TokenResponse>().ReverseMap();
            CreateMap<AnswerSelectedModel, AnswerSelected>().ReverseMap();
            CreateMap<AnswerSelectedEntity, AnswerSelected>().ReverseMap();
        }
    }
}