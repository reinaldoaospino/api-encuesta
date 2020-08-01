using api_encuesta.Models;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces.Application;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace api_encuesta.Controllers
{
    [Route("api/survey")]
    [ApiController]
    [Authorize]
    public class SurveyController : Controller
    {
        private readonly ISurveyManager _surveyManager;
        private readonly IMapper _mapper;

        public SurveyController(ISurveyManager surveyManager, IMapper mapper)
        {
            _surveyManager = surveyManager;
            _mapper = mapper;
        }

        public async Task<ActionResult<IEnumerable<SurveyModel>>> Get()
        {
            var survey = await _surveyManager.Get();
            var suveryModel = _mapper.Map<IEnumerable<SurveyModel>>(survey);
            return Ok(suveryModel);
        }

        [HttpPost]
        public async Task<ActionResult> Create(SurveyModel survey)
        {
            var surveyDto = _mapper.Map<Survey>(survey);
            await _surveyManager.Create(surveyDto);
            return Ok();
        }

        [HttpPut]
        public async Task<ActionResult> Update(SurveyModel survey)
        {
            var surveyDto = _mapper.Map<Survey>(survey);
            await _surveyManager.Update(surveyDto);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(string id)
        {
            await _surveyManager.Delete(id);
            return Ok();
        }
    }
}