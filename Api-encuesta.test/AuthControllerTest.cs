using Moq;
using Xunit;
using AutoMapper;
using Domain.Entities;
using api_encuesta.Models;
using api_encuesta.Controllers;
using Microsoft.AspNetCore.Mvc;
using Domain.Interfaces.Application;

namespace Api_encuesta.test
{
    public class AuthControllerTest
    {
        private readonly Mock<ITokenManager> _tokenManager;
        private readonly Mock<IMapper> _mapper;
        private AuthController _authController;

        public AuthControllerTest()
        {
            _tokenManager = new Mock<ITokenManager>();
            _mapper = new Mock<IMapper>();
            _authController = new AuthController(_tokenManager.Object,_mapper.Object);
        }

        [Fact]
        public void GivenTokenRequest_WhenGetToken_ThenGetTokenSuccessful()
        {
            //?Given
            var tokenExpected = "1234AA";

            var request = new TokenRequestModel
            {
                User = "reinaldo",
                Password = "1234"
            };

            var expected = new TokenResponseModel
            {
                 Token = tokenExpected
            };

            var tokenReponse = new TokenResponse
            {
                Token = tokenExpected
            };


            _tokenManager.Setup(x => x.GetToken(It.IsAny<TokenRequest>()))
                .Returns(tokenReponse)
                .Verifiable();

            _mapper.Setup(x => x.Map<TokenResponseModel>(tokenReponse))
                .Returns(expected);

            //?When
            var actual =  _authController.GetToken(request);

            //?Then
            Assert.IsAssignableFrom<OkObjectResult>(actual);
            Assert.Equal(((OkObjectResult)actual).Value, expected);

            _tokenManager.Verify();
            _mapper.Verify();
        }
    }
}
