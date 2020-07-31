using Domain.Entities;
using Domain.Interfaces.Application;
using Domain.Interfaces.Infraestructure;
using System.Threading.Tasks;

namespace Application
{
    public class SurveyManager : ISurveyManager
    {
        private readonly ISurveyRepository _repository;

        public SurveyManager(ISurveyRepository repository)
        {
            _repository = repository;
        }

        public async Task Create(Survey survey)
        {
            await _repository.Create(survey);
        }
    }
}
