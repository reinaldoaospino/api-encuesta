using Domain.Entities;
using System.Threading.Tasks;

namespace Domain.Interfaces.Application
{
    public interface ISurveyManager
    {
        Task Create(Survey obj);
    }
}
