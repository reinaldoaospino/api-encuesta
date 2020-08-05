using AutoMapper;
using Domain.Entities;
using api_encuesta.Models;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Domain.Interfaces.Application;
using Microsoft.AspNetCore.Authorization;

namespace api_encuesta.Controllers
{
    [Route("api/answer")]
    [ApiController]
    [Authorize]
    public class AnswerController : Controller
    {
        private readonly IAnswerManager _answerManager;
        private readonly IMapper _mapper;

        public AnswerController(IAnswerManager answerManager, IMapper mapper)
        {
            _answerManager = answerManager;
            _mapper = mapper;
        }

        public async Task<ActionResult<IEnumerable<AnswerModel>>> Get()
        {
            var answers = await _answerManager.Get();
            var answersModel = _mapper.Map<IEnumerable<AnswerModel>>(answers);
            return Ok(answersModel);
        }

        [HttpPost]
        public async Task<ActionResult> Create(AnswerModel answer)
        {
            var answerDto = _mapper.Map<Answer>(answer);
            await _answerManager.Create(answerDto);
            return Ok();
        }

        [HttpPut]
        public async Task<ActionResult> Update(AnswerModel answer)
        {
            var answerDto = _mapper.Map<Answer>(answer);
            await _answerManager.Update(answerDto);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(string id)
        {
            await _answerManager.Delete(id);
            return Ok();
        }
    }
}