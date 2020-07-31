using Domain.Entities;
using Domain.Interfaces.Application;

namespace Application
{
    public class TokenManager : ITokenManager
    {
        private readonly IJwtService _jwtService;

        public TokenManager(IJwtService jwtService)
        {
            _jwtService = jwtService;
        }

        public TokenResponse GetToken(TokenRequest request)
        {
            var token = _jwtService.GenerateToken(request.User);

            return new TokenResponse
            {
                Token = token
            };         
        }
    }
}
