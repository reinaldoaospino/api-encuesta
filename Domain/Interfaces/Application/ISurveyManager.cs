using Domain.Entities;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Domain.Interfaces.Application
{
    public interface ISurveyManager
    {
        Task<IEnumerable<Survey>> Get();
        Task Create(Survey obj);
        Task Update(Survey survey);
        Task Delete(string id);
    }
}
