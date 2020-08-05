using Xunit;
using Domain.Interfaces.Application;
using Moq;
using Microsoft.Extensions.Configuration;

namespace Application.test
{
    public class JwtServiceTest
    {
        private readonly Mock<IConfiguration> _mockConfiguration;
        private IJwtService _jwtService;

        public JwtServiceTest()
        {
            _mockConfiguration = new Mock<IConfiguration>();
            _jwtService = new JwtService(_mockConfiguration.Object);
        }

        [Fact]
        public void GivenUser_WhenGenerateToken_ThenGenerateSuccessul()
        {
            //?Given
            var user = "reinadldo";

            //?When
            var result = _jwtService.GenerateToken(user);

            //?Then
            Assert.NotNull(result);
        }
    }
}