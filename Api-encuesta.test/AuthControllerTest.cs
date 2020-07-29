using Moq;
using Xunit;
using Domain.Entitys;
using Domain.Abstations;
using api_encuesta.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace Api_encuesta.test
{
    public class AuthControllerTest
    {
        private readonly Mock<ITokenManager> _tokenManager;
        private AuthController _authController;

        public AuthControllerTest()
        {
            _tokenManager = new Mock<ITokenManager>();
            _authController = new AuthController(_tokenManager.Object);
        }

        [Fact]
        public void GivenTokenRequest_WhenGetToken_ThenGetTokenSuccessful()
        {
            //?Given

            var tokenExpected = "1234AA";

            var request = new TokenRequest
            {
                User = "reinaldo",
                Password = "1234"
            };

            var expected = new TokenResponse
            {
                 Token = tokenExpected
            };

            _tokenManager.Setup(x => x.GetToken(request))
                .Returns(expected)
                .Verifiable();

            //?When
            var actual =  _authController.GetToken(request);

            //?Then
            Assert.IsAssignableFrom<OkObjectResult>(actual);
            Assert.Equal(((OkObjectResult)actual).Value,expected);

            _tokenManager.Verify();
        }
    }
}
