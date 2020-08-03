using Domain.Entities;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Domain.Interfaces.Infraestructure
{
    public interface ISurveyRepository
    {
        Task<IEnumerable<Survey>> Get();
        Task<Survey> Get(string id);
        Task Create(Survey survey);
        Task Update(Survey survey);
        Task Delete(string id);
    }
}
