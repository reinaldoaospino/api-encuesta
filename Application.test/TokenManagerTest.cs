using Moq;
using Xunit;
using Domain.Entitys;
using Domain.Abstations;
using Domain.Abstations.Application;

namespace Application.test
{

    public class TokenManagerTest
    {
        private readonly Mock<IJwtService> _jtwService;
        private ITokenManager _tokenManager;

        public TokenManagerTest()
        {
            _jtwService = new Mock<IJwtService>();
            _tokenManager = new TokenManager(_jtwService.Object);
        }

        [Fact]
        public void GivenTokenRequest_WhenGetToken_ThenTokenGenerateSuccessul()
        {
            //?GIVEN
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

            _jtwService.Setup(x => x.GenerateToken(request.User))
                .Returns(tokenExpected)
                .Verifiable();

                //?WHEN
            var result = _tokenManager.GetToken(request);

            //?Then
            Assert.Equal(expected.Token, result.Token);

            _jtwService.Verify();
        }
    }
}
