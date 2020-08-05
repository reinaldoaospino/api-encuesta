using System;
using System.Linq;
using Domain.Entities;
using System.Threading.Tasks;
using Domain.Interfaces.Application;
using Domain.Interfaces.Infraestructure;

namespace Application.Managers
{
    public class TokenManager : ITokenManager
    {
        private readonly IJwtService _jwtService;
        private readonly IAuthRepository _authRepository;

        public TokenManager(IJwtService jwtService, IAuthRepository authRepository)
        {
            _jwtService = jwtService;
            _authRepository = authRepository;
        }

        public async Task<TokenResponse> GetToken(TokenRequest request)
        {
            var autoUsers = await _authRepository.GetAuthUser();

            var user = autoUsers.ToList().Find(x => x.User == request.User && x.Password == request.Password);

            if (user == null)
                throw new UnauthorizedAccessException("Not Authorized");

            var token = _jwtService.GenerateToken(request.User);

            return new TokenResponse
            {
                Token = token
            };
        }

        public async Task Create(AuthUser authUser)
        {
            authUser.Id = authUser.GenerateGuid();

            await _authRepository.Create(authUser);
        }
    }
}