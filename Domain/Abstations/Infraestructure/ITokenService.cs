using Domain.Entitys;
using System.Threading.Tasks;

namespace Domain.Abstations.Infraestructure
{
    public interface ITokenService
    {
       string GenerateToken(string user);
    }
}
