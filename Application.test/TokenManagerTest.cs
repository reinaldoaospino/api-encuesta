using Moq;
using Xunit;
using Domain.Entities;
using Application.Managers;
using Domain.Interfaces.Application;
using System.Threading.Tasks;
using Domain.Interfaces.Infraestructure;

namespace Application.test
{

    public class TokenManagerTest
    {
        private readonly Mock<IJwtService> _jtwService;
        private readonly Mock<IAuthRepository> _mockRepository;
        private ITokenManager _tokenManager;

        public TokenManagerTest()
        {
            _jtwService = new Mock<IJwtService>();
            _mockRepository = new Mock<IAuthRepository>();
            _tokenManager = new TokenManager(_jtwService.Object, _mockRepository.Object);
        }

        [Fact]
        public async Task GivenTokenRequest_WhenGetToken_ThenTokenGenerateSuccessul()
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
            var result = await _tokenManager.GetToken(request);

            //?Then
            Assert.Equal(expected.Token, result.Token);

            _jtwService.Verify();
        }
    }
}