using AutoMapper;
using Domain.Entities;
using api_encuesta.Models;
using infraestructure.Entities;

namespace api_encuesta.Mappers
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            CreateMap<TokenRequestModel, TokenRequest>().ReverseMap();
            CreateMap<TokenResponseModel, TokenResponse>().ReverseMap();
            CreateMap<SurveyModel, Survey>().ReverseMap();
            CreateMap<SurveyOptionModel, SurveyOption>().ReverseMap();
            CreateMap<SurveyEntity, Survey>().ReverseMap();
            CreateMap<SurveyOptionEntity, SurveyOption>().ReverseMap();
        }
    }
}