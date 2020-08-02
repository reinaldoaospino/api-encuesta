using Domain.Entities;
using System.Threading.Tasks;
using System.Collections.Generic;
using Domain.Interfaces.Application;
using Domain.Interfaces.Infraestructure;

namespace Application.Managers
{
    public class SurveyManager : ISurveyManager
    {
        private readonly ISurveyRepository _repository;

        public SurveyManager(ISurveyRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Survey>> Get()
        {
            return await _repository.Get();
        }

        public async Task Create(Survey survey)
        {
            survey.Id = survey.GenerateGuid();
      
            await _repository.Create(survey);
        }

        public async Task Update(Survey survey)
        {
            await _repository.Update(survey);
        }

        public async Task Delete(string id)
        {
            await _repository.Delete(id);
        }
    }
}