using Domain.Entitys;
using System.Threading.Tasks;

namespace Domain.Abstations
{
    public interface ITokenManager
    {
       TokenResponse GetToken(TokenRequest request);
    }
}
