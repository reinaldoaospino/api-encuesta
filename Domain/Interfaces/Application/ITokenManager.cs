using Domain.Entities;
using System.Threading.Tasks;

namespace Domain.Interfaces.Application
{
    public interface ITokenManager
    {
        Task<TokenResponse> GetToken(TokenRequest request);
        Task Create(AuthUser authUser);
    }
}
