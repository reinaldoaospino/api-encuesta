using Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Interfaces.Infraestructure
{
    public interface ISurveyRepository
    {
        Task<IEnumerable<Survey>> Get();
        Task Create(Survey survey);
        Task Update(Survey survey);
        Task Delete(string id);
    }
}
