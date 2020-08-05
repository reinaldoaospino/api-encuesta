using System;
using AutoMapper;
using api_encuesta.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Domain.Interfaces.Application;
using Domain.Entities;
using System.Threading.Tasks;

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
        public async Task<IActionResult> GetToken(TokenRequestModel request)
        {
            try
            {
                var tokenDto = _mapper.Map<TokenRequest>(request);

                var token = await _tokenManager.GetToken(tokenDto);

                var tokenReponse = _mapper.Map<TokenResponseModel>(token);

                return Ok(tokenReponse);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateAuth(AuthUserModel authUserModel)
        {
            var authUserDto = _mapper.Map<AuthUser>(authUserModel);
            await _tokenManager.Create(authUserDto);
            return Ok();
        }
    }
}