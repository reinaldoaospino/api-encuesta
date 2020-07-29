using Xunit;
using Domain.Abstations.Application;

namespace Application.test
{
    public class JwtServiceTest
    {
        private IJwtService _jwtService;

        public JwtServiceTest()
        {
            _jwtService = new JwtService();
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