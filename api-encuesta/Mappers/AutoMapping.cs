using AutoMapper;
using api_encuesta.Models;
using Domain.Entities;

namespace api_encuesta.Mappers
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            CreateMap<TokenRequestModel, TokenRequest>().ReverseMap();
            CreateMap<TokenResponseModel, TokenResponse>().ReverseMap();
            CreateMap<SurveyRequest, Survey>().ReverseMap();
            CreateMap<SurveyOptionModel, SurveyOption>().ReverseMap();
        }
    }
}