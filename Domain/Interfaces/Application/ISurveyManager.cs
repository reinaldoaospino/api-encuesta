using Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

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
