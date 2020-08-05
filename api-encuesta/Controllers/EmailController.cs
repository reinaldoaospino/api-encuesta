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
    [Route("api/email")]
    [ApiController]
    [Authorize]
    public class EmailController : Controller
    {
        private readonly IEmailManager _emailManager;
        private readonly IMapper _mapper;


        public EmailController(IEmailManager emailManager, IMapper mapper)
        {
            _emailManager = emailManager;
            _mapper = mapper;
        }

        public async Task<ActionResult<IEnumerable<EmailModel>>> Get()
        {
            var emails = await _emailManager.Get();
            var emailsModel = _mapper.Map<IEnumerable<EmailModel>>(emails);
            return Ok(emailsModel);
        }

        [HttpPost]
        public async Task<ActionResult> Create(EmailModel email)
        {
            var emailDto = _mapper.Map<Email>(email);
            await _emailManager.Create(emailDto);
            return Ok();
        }
    }
}