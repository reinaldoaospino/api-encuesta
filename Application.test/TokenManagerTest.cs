using Moq;
using Xunit;
using System;
using Domain.Entities;
using Application.Managers;
using Domain.Interfaces.Application;
using System.Threading.Tasks;
using Domain.Interfaces.Infraestructure;
using System.Collections.Generic;

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
            var user = "reinaldo";
            var password = "1234";

            var request = new TokenRequest
            {
                User = user,
                Password = password
            };

            var expected = new TokenResponse
            {
                Token = tokenExpected
            };

            var authUser = new List<AuthUser>
            {
                new AuthUser
                {
                    User = user,
                    Password = password
                }
            };

            _mockRepository.Setup(x => x.GetAuthUser())
                .ReturnsAsync(authUser)
                .Verifiable();

            _jtwService.Setup(x => x.GenerateToken(request.User))
                .Returns(tokenExpected)
                .Verifiable();

                //?WHEN
            var result = await _tokenManager.GetToken(request);

            //?Then
            Assert.Equal(expected.Token, result.Token);

            _jtwService.Verify();
            _mockRepository.Verify();
        }

        [Fact]
        public async Task GivenTokenRequest_WhenGetToken_ThenThrowUnauthorizedAccessException()
        {
            //?GIVEN
            var user = "reinaldo";
            var password = "1234";

            var request = new TokenRequest
            {
                User = user,
                Password = password
            };

            var authUser = new List<AuthUser>();

            _mockRepository.Setup(x => x.GetAuthUser())
                .ReturnsAsync(authUser)
                .Verifiable();

            //?WHEN
            Func<Task> geToken = () => _tokenManager.GetToken(request);

            //?Then
            await Assert.ThrowsAsync<UnauthorizedAccessException>(geToken);
            _mockRepository.Verify();
        }
    }
}