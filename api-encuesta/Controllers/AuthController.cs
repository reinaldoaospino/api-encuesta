using System;
using Domain.Entitys;
using Domain.Abstations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace api_encuesta.Controllers
{
    [Route("api/authentication")]
    [ApiController]
    public class AuthController : Controller
    {
        private readonly ITokenManager _tokenManager;

        public AuthController(ITokenManager tokenManager)
        {
            _tokenManager = tokenManager;
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("token")]
        public IActionResult GetToken(TokenRequest request)
        {
            try
            {
                var token = _tokenManager.GetToken(request);
                return Ok(token);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
