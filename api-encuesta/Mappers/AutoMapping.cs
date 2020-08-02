using AutoMapper;
using Domain.Entities;
using api_encuesta.Models;
using Infraestructure.Entities;

namespace api_encuesta.Mappers
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            CreateMap<SurveyModel, Survey>().ReverseMap();
            CreateMap<SurveyEntity, Survey>().ReverseMap();
            CreateMap<TokenRequestModel, TokenRequest>().ReverseMap();
            CreateMap<SurveyOptionModel, SurveyOption>().ReverseMap();
            CreateMap<SurveyOptionEntity, SurveyOption>().ReverseMap();
            CreateMap<TokenResponseModel, TokenResponse>().ReverseMap();
        }
    }
}