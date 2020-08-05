using Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Interfaces.Infraestructure
{
    public interface IAuthRepository
    {
        Task<IEnumerable<AuthUser>> GetAuthUser();
        Task Create(AuthUser authUser);
    }
}
