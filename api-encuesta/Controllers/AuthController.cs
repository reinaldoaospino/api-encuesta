using System;
using AutoMapper;
using Domain.Entitys;
using Domain.Abstations;
using api_encuesta.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace api_encuesta.Controllers
{
    [Route("api/authentication")]
    [ApiController]
    public class AuthController : Controller
    {
        private readonly ITokenManager _tokenManager;
        private readonly IMapper _mapper;

        public AuthController(ITokenManager tokenManager, IMapper mapper)
        {
            _tokenManager = tokenManager;
            _mapper = mapper;
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("token")]
        public IActionResult GetToken(TokenRequestModel request)
        {
            try
            {
                var tokenDto = _mapper.Map<TokenRequest>(request);

                var token = _tokenManager.GetToken(tokenDto);

                var tokenReponse = _mapper.Map<TokenResponseModel>(token);

                return Ok(tokenReponse);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}