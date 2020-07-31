using Domain.Entities;

namespace Domain.Interfaces.Application
{
    public interface ITokenManager
    {
       TokenResponse GetToken(TokenRequest request);
    }
}
