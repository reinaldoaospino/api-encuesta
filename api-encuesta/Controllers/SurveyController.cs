using api_encuesta.Models;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces.Application;
using Microsoft.AspNetCore.Authorization;
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

        [Authorize]
        [HttpPost]
        [Route("create")]
        public async void Create(SurveyRequest survey)
        {
            var surveyDto = _mapper.Map<Survey>(survey);
            await _surveyManager.Create(surveyDto);
        }
    }
}