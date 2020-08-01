using AutoMapper;
using api_encuesta.Models;
using Domain.Entities;
using infraestructure.Entities;
using System.Collections;
using System.Collections.Generic;

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
            //CreateMap<IEnumerable<SurveyModel>, IEnumerable<Survey>>().ReverseMap();
        }
    }
}