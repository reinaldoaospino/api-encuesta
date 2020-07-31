using api_encuesta.Models;
using AutoMapper;
using Domain.Abstations.Application;
using Domain.Entitys;
using Microsoft.AspNetCore.Mvc;

namespace api_encuesta.Controllers
{
    [Route("api/survey")]
    [ApiController]
    public class SurveyController : Controller
    {
        private readonly ISurveyManager _surveyManager;
        private readonly IMapper _mapper;

        public SurveyController(ISurveyManager surveyManager, IMapper mapper)
        {
            _surveyManager = surveyManager;
            _mapper = mapper;
        }

        [HttpPost]
        [Route("create")]
        public async void Create(SurveyRequest survey)
        {
            var surveyDto = _mapper.Map<Survey>(survey);
            await _surveyManager.Create(surveyDto);
        }
    }
}