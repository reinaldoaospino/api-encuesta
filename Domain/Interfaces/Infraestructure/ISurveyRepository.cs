using Domain.Entities;
using System.Threading.Tasks;

namespace Domain.Interfaces.Infraestructure
{
    public interface ISurveyRepository
    {
        Task Create(Survey survey);
    }
}
