using AutoMapper;
using Domain.Entitys;
using api_encuesta.Models;

namespace api_encuesta.Mappers
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            CreateMap<TokenRequestModel, TokenRequest>().ReverseMap();
            CreateMap<TokenResponseModel, TokenResponse>().ReverseMap();
        }
    }
}