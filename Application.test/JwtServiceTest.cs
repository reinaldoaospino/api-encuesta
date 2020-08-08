using Moq;
using Xunit;
using System;
using Domain.Interfaces.Application;
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
            _mockConfiguration.SetupGet(x => x[It.Is<string>(s => s == "AppSettings:SecretKey")]).Returns(Guid.NewGuid().ToString());
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