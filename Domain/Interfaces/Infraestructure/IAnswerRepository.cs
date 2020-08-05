using Domain.Entities;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Domain.Interfaces.Infraestructure
{
    public interface IAnswerRepository
    {
        Task<IEnumerable<Answer>> Get();
        Task<Answer> Get(string id);
        Task Create(Answer answer);
        Task Update(Answer answer);
        Task Delete(string id);
    }
}
